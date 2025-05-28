#nullable enable

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fleck;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using REnum;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace CDPBridges
{
    public class Bridge : IBridge
    {
        private readonly IBrowser browser;
        private readonly ILogger logger;
        private readonly WebSocketServer webSocketServer;
        private readonly CancellationTokenSource lifetimeCancellationTokenSource;
        private readonly string address;
        private IWebSocketConnection? webSocketConnection;

        public Bridge(int port = 1473, IBrowser? browser = null, ILogger? logger = null)
        {
            this.browser = browser ?? new ProcessBrowser();
            this.logger = logger ?? NullLogger.Instance;
            lifetimeCancellationTokenSource = new CancellationTokenSource();
            address = $"127.0.0.1:{port}";
            webSocketServer = new WebSocketServer($"ws://{address}");
        }

        private void ConfigConnection(IWebSocketConnection socket)
        {
            webSocketConnection = socket;
            socket.OnMessage += message =>
            {
                // logger.LogInformation("Socket message received: {}", message);
                CDPRequest request = CDPRequest.FromJson(message);
                logger.LogInformation("Socket request received: {}", request);

                if (request.Method.IsNetwork_enable())
                {
                    var response = new CDPResponse(request.Id, CDPResult.Network_enable());
                    SendEventAndForgetAsync(response, lifetimeCancellationTokenSource.Token).Forget();
                }
            };
            socket.OnBinary += message => { logger.LogInformation("Socket binary received: {}", message.Length); };
            socket.OnClose += () => { logger.LogInformation("Socket closed"); };
            socket.OnError += exception => { logger.LogError(exception, "Error in CDP Bridge"); };
            socket.OnOpen += () => { logger.LogInformation("Socket opened"); };
        }

        public BridgeStartResult Start()
        {
            logger.LogInformation("WebSocket start");
            try
            {
                webSocketServer.Start(ConfigConnection);
            }
            catch (Exception e)
            {
                return BridgeStartResult.FromBridgeStartError(
                    BridgeStartError.FromWebSocketError(new WebSocketError(e))
                );
            }

            string url = $"devtools://devtools/bundled/inspector.html?ws={address}";
            BrowserOpenResult result = browser.OpenUrl(url);
            return result.Match(
                (logger, url),
                onSuccess: static ctx =>
                {
                    ctx.logger.LogInformation("Browser opened with url: {}", ctx.url);
                    return BridgeStartResult.Success();
                },
                onBrowserOpenError: static (ctx, error) =>
                {
                    error.Match(
                        ctx,
                        onErrorChromeNotInstalled: static ctx =>
                            ctx.logger.LogError("Chrome browser is not installed cannot open url: {}", ctx.url),
                        onException: static (ctx, exception) =>
                            ctx.logger.LogError(exception, "Error on open url by browser with url {}", ctx.url)
                    );
                    return BridgeStartResult.FromBridgeStartError(
                        BridgeStartError.FromBrowserOpenError(error)
                    );
                }
            );
        }

        private UniTaskVoid SendEventAndForgetAsync(CDPResponse response, CancellationToken token)
        {
            string message = response.ToJson();
            return SendEventAndForgetAsync(WebSocketMessage.FromText(new WebSocketMessage.Text(message)), token);
        }

        private async UniTaskVoid SendEventAndForgetAsync(WebSocketMessage message, CancellationToken token)
        {
            SendResult result = await SendEventAsync(message, token);
            result.Match(
                logger,
                onWebSocketError: static (logger, error) =>
                    logger.LogError(error.Exception, $"Exception executing {nameof(SendEventAsync)}"),
                onConnectionIsNotEstablished: static logger =>
                    logger.LogError($"ConnectionIsNotEstablished executing {nameof(SendEventAsync)}"),
                onSuccess: static _ => { }
            );
        }

        public UniTask<SendResult> SendEventAsync(CDPEvent cdpEvent, CancellationToken token)
        {
            string message = cdpEvent.ToRaw().ToJson();
            var text = new WebSocketMessage.Text(message);
            return SendEventAsync(WebSocketMessage.FromText(text), token);
        }

        //TODO ExceptionFree Annotation
        public async UniTask<SendResult> SendEventAsync(WebSocketMessage message, CancellationToken token)
        {
            if (webSocketConnection is null)
            {
                return SendResult.ConnectionIsNotEstablished();
            }

            logger.LogDebug("Send message to websocket: {}", message);

            try
            {
                if (message.IsBinary(out WebSocketMessage.Binary? binary))
                {
                    await webSocketConnection.Send(binary!.Value.Data);
                }
                else if (message.IsText(out WebSocketMessage.Text? text))
                {
                    await webSocketConnection.Send(text!.Value.Message);
                }
            }
            catch (Exception e)
            {
                return SendResult.FromWebSocketError(new SendResult.WebSocketError(e));
            }

            return SendResult.Success();
        }

        public void Dispose()
        {
            webSocketServer.Dispose();
            lifetimeCancellationTokenSource.Dispose();
        }

    }


    [REnum]
    [REnumFieldEmpty("Success")]
    [REnumFieldEmpty("ConnectionIsNotEstablished")]
    [REnumField(typeof(WebSocketError))]
    public partial struct SendResult
    {
        public readonly struct WebSocketError
        {
            public readonly Exception Exception;

            public WebSocketError(Exception exception)
            {
                Exception = exception;
            }
        }
    }
}