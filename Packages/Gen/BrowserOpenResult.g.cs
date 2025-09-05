#nullable enable
using System;
using REnum;
namespace CDPBridges {
public partial struct BrowserOpenResult {
    public enum Kind {
        BrowserOpenError,
        Success,
    }
    private readonly Kind _kind;
    private readonly BrowserOpenError _browseropenerror;
private BrowserOpenResult(
Kind kind
,
BrowserOpenError browseropenerror = default
){
_kind = kind;
_browseropenerror = browseropenerror;
}
    public static BrowserOpenResult FromBrowserOpenError(BrowserOpenError value) => new BrowserOpenResult(Kind.BrowserOpenError, browseropenerror: value);
    public static BrowserOpenResult Success() => new BrowserOpenResult(Kind.Success);
    public bool IsBrowserOpenError(out BrowserOpenError? value) {
        value = _browseropenerror;
        return _kind == Kind.BrowserOpenError;
    }
    public bool IsSuccess() => _kind == Kind.Success;
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, BrowserOpenError, T> onBrowserOpenError,
        System.Func<TCtx, T> onSuccess
    )
    {
        return _kind switch
        {
            Kind.BrowserOpenError => onBrowserOpenError(ctx, _browseropenerror),
            Kind.Success => onSuccess(ctx),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<BrowserOpenError, T> onBrowserOpenError,
        System.Func<T> onSuccess
    )
    {
        return _kind switch
        {
            Kind.BrowserOpenError => onBrowserOpenError(_browseropenerror),
            Kind.Success => onSuccess(),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx, BrowserOpenError> onBrowserOpenError,
        System.Action<TCtx> onSuccess
    )
    {
        switch (_kind)
        {
            case Kind.BrowserOpenError: onBrowserOpenError(ctx, _browseropenerror); break;
            case Kind.Success: onSuccess(ctx); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action<BrowserOpenError> onBrowserOpenError,
        System.Action onSuccess
    )
    {
        switch (_kind)
        {
            case Kind.BrowserOpenError: onBrowserOpenError(_browseropenerror); break;
            case Kind.Success: onSuccess(); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.BrowserOpenError => _browseropenerror.ToString() ?? "null",
        Kind.Success => "Success",
        _ => "<invalid>"
    };
    public bool Equals(BrowserOpenResult other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.BrowserOpenError)
            return System.Collections.Generic.EqualityComparer<BrowserOpenError>.Default.Equals(_browseropenerror, other._browseropenerror);
        if (_kind == Kind.Success)
            return true;
        return false;
    }
    public override bool Equals(object? obj) => obj is BrowserOpenResult other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.BrowserOpenError => System.HashCode.Combine((int)_kind, _browseropenerror),
            Kind.Success => (int)_kind,
            _ => 0
        };
    }
    public static bool operator ==(BrowserOpenResult left, BrowserOpenResult right) => left.Equals(right);
    public static bool operator !=(BrowserOpenResult left, BrowserOpenResult right) => !(left == right);
}
}
