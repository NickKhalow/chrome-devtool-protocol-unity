#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Fleck;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using REnum;
using ILogger = Microsoft.Extensions.Logging.ILogger;
namespace CDPBridges {
public partial struct SendResult {
    public enum Kind {
        WebSocketError,
        Success,
        ConnectionIsNotEstablished,
    }
    private readonly Kind _kind;
    private readonly WebSocketError _websocketerror;
private SendResult(
Kind kind
,
WebSocketError websocketerror = default
){
_kind = kind;
_websocketerror = websocketerror;
}
    public static SendResult FromWebSocketError(WebSocketError value) => new SendResult(Kind.WebSocketError, websocketerror: value);
    public static SendResult Success() => new SendResult(Kind.Success);
    public static SendResult ConnectionIsNotEstablished() => new SendResult(Kind.ConnectionIsNotEstablished);
    public bool IsWebSocketError(out WebSocketError? value) {
        value = _websocketerror;
        return _kind == Kind.WebSocketError;
    }
    public bool IsSuccess() => _kind == Kind.Success;
    public bool IsConnectionIsNotEstablished() => _kind == Kind.ConnectionIsNotEstablished;
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, WebSocketError, T> onWebSocketError,
        System.Func<TCtx, T> onSuccess,
        System.Func<TCtx, T> onConnectionIsNotEstablished
    )
    {
        return _kind switch
        {
            Kind.WebSocketError => onWebSocketError(ctx, _websocketerror),
            Kind.Success => onSuccess(ctx),
            Kind.ConnectionIsNotEstablished => onConnectionIsNotEstablished(ctx),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<WebSocketError, T> onWebSocketError,
        System.Func<T> onSuccess,
        System.Func<T> onConnectionIsNotEstablished
    )
    {
        return _kind switch
        {
            Kind.WebSocketError => onWebSocketError(_websocketerror),
            Kind.Success => onSuccess(),
            Kind.ConnectionIsNotEstablished => onConnectionIsNotEstablished(),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx, WebSocketError> onWebSocketError,
        System.Action<TCtx> onSuccess,
        System.Action<TCtx> onConnectionIsNotEstablished
    )
    {
        switch (_kind)
        {
            case Kind.WebSocketError: onWebSocketError(ctx, _websocketerror); break;
            case Kind.Success: onSuccess(ctx); break;
            case Kind.ConnectionIsNotEstablished: onConnectionIsNotEstablished(ctx); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action<WebSocketError> onWebSocketError,
        System.Action onSuccess,
        System.Action onConnectionIsNotEstablished
    )
    {
        switch (_kind)
        {
            case Kind.WebSocketError: onWebSocketError(_websocketerror); break;
            case Kind.Success: onSuccess(); break;
            case Kind.ConnectionIsNotEstablished: onConnectionIsNotEstablished(); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.WebSocketError => _websocketerror.ToString() ?? "null",
        Kind.Success => "Success",
        Kind.ConnectionIsNotEstablished => "ConnectionIsNotEstablished",
        _ => "<invalid>"
    };
    public bool Equals(SendResult other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.WebSocketError)
            return System.Collections.Generic.EqualityComparer<WebSocketError>.Default.Equals(_websocketerror, other._websocketerror);
        if (_kind == Kind.Success)
            return true;
        if (_kind == Kind.ConnectionIsNotEstablished)
            return true;
        return false;
    }
    public override bool Equals(object? obj) => obj is SendResult other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.WebSocketError => System.HashCode.Combine((int)_kind, _websocketerror),
            Kind.Success => (int)_kind,
            Kind.ConnectionIsNotEstablished => (int)_kind,
            _ => 0
        };
    }
    public static bool operator ==(SendResult left, SendResult right) => left.Equals(right);
    public static bool operator !=(SendResult left, SendResult right) => !(left == right);
}
}
