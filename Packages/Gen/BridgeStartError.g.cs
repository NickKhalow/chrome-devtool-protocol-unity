#nullable enable
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using REnum;
namespace CDPBridges {
public partial struct BridgeStartError {
    public enum Kind {
        WebSocketError,
        BrowserOpenError,
    }
    private readonly Kind _kind;
    private readonly WebSocketError _websocketerror;
    private readonly BrowserOpenError _browseropenerror;
private BridgeStartError(
Kind kind
,
WebSocketError websocketerror = default,
BrowserOpenError browseropenerror = default
){
_kind = kind;
_websocketerror = websocketerror;
_browseropenerror = browseropenerror;
}
    public static BridgeStartError FromWebSocketError(WebSocketError value) => new BridgeStartError(Kind.WebSocketError, websocketerror: value);
    public static BridgeStartError FromBrowserOpenError(BrowserOpenError value) => new BridgeStartError(Kind.BrowserOpenError, browseropenerror: value);
    public bool IsWebSocketError(out WebSocketError? value) {
        value = _websocketerror;
        return _kind == Kind.WebSocketError;
    }
    public bool IsBrowserOpenError(out BrowserOpenError? value) {
        value = _browseropenerror;
        return _kind == Kind.BrowserOpenError;
    }
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, WebSocketError, T> onWebSocketError,
        System.Func<TCtx, BrowserOpenError, T> onBrowserOpenError
    )
    {
        return _kind switch
        {
            Kind.WebSocketError => onWebSocketError(ctx, _websocketerror),
            Kind.BrowserOpenError => onBrowserOpenError(ctx, _browseropenerror),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<WebSocketError, T> onWebSocketError,
        System.Func<BrowserOpenError, T> onBrowserOpenError
    )
    {
        return _kind switch
        {
            Kind.WebSocketError => onWebSocketError(_websocketerror),
            Kind.BrowserOpenError => onBrowserOpenError(_browseropenerror),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx, WebSocketError> onWebSocketError,
        System.Action<TCtx, BrowserOpenError> onBrowserOpenError
    )
    {
        switch (_kind)
        {
            case Kind.WebSocketError: onWebSocketError(ctx, _websocketerror); break;
            case Kind.BrowserOpenError: onBrowserOpenError(ctx, _browseropenerror); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action<WebSocketError> onWebSocketError,
        System.Action<BrowserOpenError> onBrowserOpenError
    )
    {
        switch (_kind)
        {
            case Kind.WebSocketError: onWebSocketError(_websocketerror); break;
            case Kind.BrowserOpenError: onBrowserOpenError(_browseropenerror); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.WebSocketError => _websocketerror.ToString() ?? "null",
        Kind.BrowserOpenError => _browseropenerror.ToString() ?? "null",
        _ => "<invalid>"
    };
    public bool Equals(BridgeStartError other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.WebSocketError)
            return System.Collections.Generic.EqualityComparer<WebSocketError>.Default.Equals(_websocketerror, other._websocketerror);
        if (_kind == Kind.BrowserOpenError)
            return System.Collections.Generic.EqualityComparer<BrowserOpenError>.Default.Equals(_browseropenerror, other._browseropenerror);
        return false;
    }
    public override bool Equals(object? obj) => obj is BridgeStartError other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.WebSocketError => System.HashCode.Combine((int)_kind, _websocketerror),
            Kind.BrowserOpenError => System.HashCode.Combine((int)_kind, _browseropenerror),
            _ => 0
        };
    }
    public static bool operator ==(BridgeStartError left, BridgeStartError right) => left.Equals(right);
    public static bool operator !=(BridgeStartError left, BridgeStartError right) => !(left == right);
}
}
