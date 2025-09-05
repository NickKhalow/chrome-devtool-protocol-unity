#nullable enable
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using REnum;
namespace CDPBridges {
public partial struct CDPMethod {
    public enum Kind {
        Unknown,
        Network_enable,
    }
    private readonly Kind _kind;
    private readonly Unknown _unknown;
private CDPMethod(
Kind kind
,
Unknown unknown = default
){
_kind = kind;
_unknown = unknown;
}
    public static CDPMethod FromUnknown(Unknown value) => new CDPMethod(Kind.Unknown, unknown: value);
    public static CDPMethod Network_enable() => new CDPMethod(Kind.Network_enable);
    public bool IsUnknown(out Unknown? value) {
        value = _unknown;
        return _kind == Kind.Unknown;
    }
    public bool IsNetwork_enable() => _kind == Kind.Network_enable;
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, Unknown, T> onUnknown,
        System.Func<TCtx, T> onNetwork_enable
    )
    {
        return _kind switch
        {
            Kind.Unknown => onUnknown(ctx, _unknown),
            Kind.Network_enable => onNetwork_enable(ctx),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<Unknown, T> onUnknown,
        System.Func<T> onNetwork_enable
    )
    {
        return _kind switch
        {
            Kind.Unknown => onUnknown(_unknown),
            Kind.Network_enable => onNetwork_enable(),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx, Unknown> onUnknown,
        System.Action<TCtx> onNetwork_enable
    )
    {
        switch (_kind)
        {
            case Kind.Unknown: onUnknown(ctx, _unknown); break;
            case Kind.Network_enable: onNetwork_enable(ctx); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action<Unknown> onUnknown,
        System.Action onNetwork_enable
    )
    {
        switch (_kind)
        {
            case Kind.Unknown: onUnknown(_unknown); break;
            case Kind.Network_enable: onNetwork_enable(); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.Unknown => _unknown.ToString() ?? "null",
        Kind.Network_enable => "Network_enable",
        _ => "<invalid>"
    };
    public bool Equals(CDPMethod other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.Unknown)
            return System.Collections.Generic.EqualityComparer<Unknown>.Default.Equals(_unknown, other._unknown);
        if (_kind == Kind.Network_enable)
            return true;
        return false;
    }
    public override bool Equals(object? obj) => obj is CDPMethod other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.Unknown => System.HashCode.Combine((int)_kind, _unknown),
            Kind.Network_enable => (int)_kind,
            _ => 0
        };
    }
    public static bool operator ==(CDPMethod left, CDPMethod right) => left.Equals(right);
    public static bool operator !=(CDPMethod left, CDPMethod right) => !(left == right);
}
}
