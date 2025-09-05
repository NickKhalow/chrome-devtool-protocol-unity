#nullable enable
using System;
using Newtonsoft.Json;
using REnum;
namespace CDPBridges {
public partial struct CDPResult {
    public enum Kind {
        Network_enable,
    }
    private readonly Kind _kind;
private CDPResult(
Kind kind

){
_kind = kind;
}
    public static CDPResult Network_enable() => new CDPResult(Kind.Network_enable);
    public bool IsNetwork_enable() => _kind == Kind.Network_enable;
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, T> onNetwork_enable
    )
    {
        return _kind switch
        {
            Kind.Network_enable => onNetwork_enable(ctx),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<T> onNetwork_enable
    )
    {
        return _kind switch
        {
            Kind.Network_enable => onNetwork_enable(),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx> onNetwork_enable
    )
    {
        switch (_kind)
        {
            case Kind.Network_enable: onNetwork_enable(ctx); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action onNetwork_enable
    )
    {
        switch (_kind)
        {
            case Kind.Network_enable: onNetwork_enable(); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.Network_enable => "Network_enable",
        _ => "<invalid>"
    };
    public bool Equals(CDPResult other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.Network_enable)
            return true;
        return false;
    }
    public override bool Equals(object? obj) => obj is CDPResult other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.Network_enable => (int)_kind,
            _ => 0
        };
    }
    public static bool operator ==(CDPResult left, CDPResult right) => left.Equals(right);
    public static bool operator !=(CDPResult left, CDPResult right) => !(left == right);
}
}
