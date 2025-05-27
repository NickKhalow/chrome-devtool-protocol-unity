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
        private readonly ILogger logger;
        private readonly WebSocketServer webSocketServer;
        private readonly CancellationTokenSource lifetimeCancellationTokenSource;
        private IWebSocketConnection? webSocketConnection;

        public Bridge(int port = 1473, ILogger? logger = null)
        {
            this.logger = logger ?? NullLogger.Instance;
            lifetimeCancellationTokenSource = new CancellationTokenSource();
            webSocketServer = new WebSocketServer($"ws://127.0.0.1:{port}");
        }

        public void Start()
        {
            logger.LogInformation("WebSocket start");
            webSocketServer.Start(
                socket =>
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
                    socket.OnClose += () =>
                    {
                        logger.LogInformation("Socket closed");

                        //TODO
                    };
                    socket.OnError += exception => { logger.LogError(exception, "Error in CDP Bridge"); };
                    socket.OnOpen += () => { logger.LogInformation("Socket opened"); };
                    //TODO
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