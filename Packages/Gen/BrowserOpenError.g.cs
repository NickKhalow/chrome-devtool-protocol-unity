#nullable enable
using System;
using REnum;
namespace CDPBridges {
public partial struct BrowserOpenError {
    public enum Kind {
        Exception,
        ErrorChromeNotInstalled,
    }
    private readonly Kind _kind;
    private readonly Exception _exception;
private BrowserOpenError(
Kind kind
,
Exception exception = default
){
_kind = kind;
_exception = exception;
}
    public static BrowserOpenError FromException(Exception value) => new BrowserOpenError(Kind.Exception, exception: value);
    public static BrowserOpenError ErrorChromeNotInstalled() => new BrowserOpenError(Kind.ErrorChromeNotInstalled);
    public bool IsException(out Exception? value) {
        value = _exception;
        return _kind == Kind.Exception;
    }
    public bool IsErrorChromeNotInstalled() => _kind == Kind.ErrorChromeNotInstalled;
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, Exception, T> onException,
        System.Func<TCtx, T> onErrorChromeNotInstalled
    )
    {
        return _kind switch
        {
            Kind.Exception => onException(ctx, _exception),
            Kind.ErrorChromeNotInstalled => onErrorChromeNotInstalled(ctx),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<Exception, T> onException,
        System.Func<T> onErrorChromeNotInstalled
    )
    {
        return _kind switch
        {
            Kind.Exception => onException(_exception),
            Kind.ErrorChromeNotInstalled => onErrorChromeNotInstalled(),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx, Exception> onException,
        System.Action<TCtx> onErrorChromeNotInstalled
    )
    {
        switch (_kind)
        {
            case Kind.Exception: onException(ctx, _exception); break;
            case Kind.ErrorChromeNotInstalled: onErrorChromeNotInstalled(ctx); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action<Exception> onException,
        System.Action onErrorChromeNotInstalled
    )
    {
        switch (_kind)
        {
            case Kind.Exception: onException(_exception); break;
            case Kind.ErrorChromeNotInstalled: onErrorChromeNotInstalled(); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.Exception => _exception.ToString() ?? "null",
        Kind.ErrorChromeNotInstalled => "ErrorChromeNotInstalled",
        _ => "<invalid>"
    };
    public bool Equals(BrowserOpenError other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.Exception)
            return System.Collections.Generic.EqualityComparer<Exception>.Default.Equals(_exception, other._exception);
        if (_kind == Kind.ErrorChromeNotInstalled)
            return true;
        return false;
    }
    public override bool Equals(object? obj) => obj is BrowserOpenError other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.Exception => System.HashCode.Combine((int)_kind, _exception),
            Kind.ErrorChromeNotInstalled => (int)_kind,
            _ => 0
        };
    }
    public static bool operator ==(BrowserOpenError left, BrowserOpenError right) => left.Equals(right);
    public static bool operator !=(BrowserOpenError left, BrowserOpenError right) => !(left == right);
}
}
