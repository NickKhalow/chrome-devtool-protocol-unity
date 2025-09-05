#nullable enable
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using REnum;
namespace CDPBridges {
public partial struct BridgeStartResult {
    public enum Kind {
        BridgeStartError,
        Success,
    }
    private readonly Kind _kind;
    private readonly BridgeStartError _bridgestarterror;
private BridgeStartResult(
Kind kind
,
BridgeStartError bridgestarterror = default
){
_kind = kind;
_bridgestarterror = bridgestarterror;
}
    public static BridgeStartResult FromBridgeStartError(BridgeStartError value) => new BridgeStartResult(Kind.BridgeStartError, bridgestarterror: value);
    public static BridgeStartResult Success() => new BridgeStartResult(Kind.Success);
    public bool IsBridgeStartError(out BridgeStartError? value) {
        value = _bridgestarterror;
        return _kind == Kind.BridgeStartError;
    }
    public bool IsSuccess() => _kind == Kind.Success;
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, BridgeStartError, T> onBridgeStartError,
        System.Func<TCtx, T> onSuccess
    )
    {
        return _kind switch
        {
            Kind.BridgeStartError => onBridgeStartError(ctx, _bridgestarterror),
            Kind.Success => onSuccess(ctx),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<BridgeStartError, T> onBridgeStartError,
        System.Func<T> onSuccess
    )
    {
        return _kind switch
        {
            Kind.BridgeStartError => onBridgeStartError(_bridgestarterror),
            Kind.Success => onSuccess(),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx, BridgeStartError> onBridgeStartError,
        System.Action<TCtx> onSuccess
    )
    {
        switch (_kind)
        {
            case Kind.BridgeStartError: onBridgeStartError(ctx, _bridgestarterror); break;
            case Kind.Success: onSuccess(ctx); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action<BridgeStartError> onBridgeStartError,
        System.Action onSuccess
    )
    {
        switch (_kind)
        {
            case Kind.BridgeStartError: onBridgeStartError(_bridgestarterror); break;
            case Kind.Success: onSuccess(); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.BridgeStartError => _bridgestarterror.ToString() ?? "null",
        Kind.Success => "Success",
        _ => "<invalid>"
    };
    public bool Equals(BridgeStartResult other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.BridgeStartError)
            return System.Collections.Generic.EqualityComparer<BridgeStartError>.Default.Equals(_bridgestarterror, other._bridgestarterror);
        if (_kind == Kind.Success)
            return true;
        return false;
    }
    public override bool Equals(object? obj) => obj is BridgeStartResult other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.BridgeStartError => System.HashCode.Combine((int)_kind, _bridgestarterror),
            Kind.Success => (int)_kind,
            _ => 0
        };
    }
    public static bool operator ==(BridgeStartResult left, BridgeStartResult right) => left.Equals(right);
    public static bool operator !=(BridgeStartResult left, BridgeStartResult right) => !(left == right);
}
}
