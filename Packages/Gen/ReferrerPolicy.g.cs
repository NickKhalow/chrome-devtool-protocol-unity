#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using REnum;
namespace CDPBridges {
public partial struct ReferrerPolicy {
    public enum Kind {
        UnsafeUrl,
        NoReferrerWhenDowngrade,
        NoReferrer,
        Origin,
        OriginWhenCrossOrigin,
        SameOrigin,
        StrictOrigin,
        StrictOriginWhenCrossOrigin,
    }
    private readonly Kind _kind;
private ReferrerPolicy(
Kind kind

){
_kind = kind;
}
    public static ReferrerPolicy UnsafeUrl() => new ReferrerPolicy(Kind.UnsafeUrl);
    public static ReferrerPolicy NoReferrerWhenDowngrade() => new ReferrerPolicy(Kind.NoReferrerWhenDowngrade);
    public static ReferrerPolicy NoReferrer() => new ReferrerPolicy(Kind.NoReferrer);
    public static ReferrerPolicy Origin() => new ReferrerPolicy(Kind.Origin);
    public static ReferrerPolicy OriginWhenCrossOrigin() => new ReferrerPolicy(Kind.OriginWhenCrossOrigin);
    public static ReferrerPolicy SameOrigin() => new ReferrerPolicy(Kind.SameOrigin);
    public static ReferrerPolicy StrictOrigin() => new ReferrerPolicy(Kind.StrictOrigin);
    public static ReferrerPolicy StrictOriginWhenCrossOrigin() => new ReferrerPolicy(Kind.StrictOriginWhenCrossOrigin);
    public bool IsUnsafeUrl() => _kind == Kind.UnsafeUrl;
    public bool IsNoReferrerWhenDowngrade() => _kind == Kind.NoReferrerWhenDowngrade;
    public bool IsNoReferrer() => _kind == Kind.NoReferrer;
    public bool IsOrigin() => _kind == Kind.Origin;
    public bool IsOriginWhenCrossOrigin() => _kind == Kind.OriginWhenCrossOrigin;
    public bool IsSameOrigin() => _kind == Kind.SameOrigin;
    public bool IsStrictOrigin() => _kind == Kind.StrictOrigin;
    public bool IsStrictOriginWhenCrossOrigin() => _kind == Kind.StrictOriginWhenCrossOrigin;
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, T> onUnsafeUrl,
        System.Func<TCtx, T> onNoReferrerWhenDowngrade,
        System.Func<TCtx, T> onNoReferrer,
        System.Func<TCtx, T> onOrigin,
        System.Func<TCtx, T> onOriginWhenCrossOrigin,
        System.Func<TCtx, T> onSameOrigin,
        System.Func<TCtx, T> onStrictOrigin,
        System.Func<TCtx, T> onStrictOriginWhenCrossOrigin
    )
    {
        return _kind switch
        {
            Kind.UnsafeUrl => onUnsafeUrl(ctx),
            Kind.NoReferrerWhenDowngrade => onNoReferrerWhenDowngrade(ctx),
            Kind.NoReferrer => onNoReferrer(ctx),
            Kind.Origin => onOrigin(ctx),
            Kind.OriginWhenCrossOrigin => onOriginWhenCrossOrigin(ctx),
            Kind.SameOrigin => onSameOrigin(ctx),
            Kind.StrictOrigin => onStrictOrigin(ctx),
            Kind.StrictOriginWhenCrossOrigin => onStrictOriginWhenCrossOrigin(ctx),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<T> onUnsafeUrl,
        System.Func<T> onNoReferrerWhenDowngrade,
        System.Func<T> onNoReferrer,
        System.Func<T> onOrigin,
        System.Func<T> onOriginWhenCrossOrigin,
        System.Func<T> onSameOrigin,
        System.Func<T> onStrictOrigin,
        System.Func<T> onStrictOriginWhenCrossOrigin
    )
    {
        return _kind switch
        {
            Kind.UnsafeUrl => onUnsafeUrl(),
            Kind.NoReferrerWhenDowngrade => onNoReferrerWhenDowngrade(),
            Kind.NoReferrer => onNoReferrer(),
            Kind.Origin => onOrigin(),
            Kind.OriginWhenCrossOrigin => onOriginWhenCrossOrigin(),
            Kind.SameOrigin => onSameOrigin(),
            Kind.StrictOrigin => onStrictOrigin(),
            Kind.StrictOriginWhenCrossOrigin => onStrictOriginWhenCrossOrigin(),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx> onUnsafeUrl,
        System.Action<TCtx> onNoReferrerWhenDowngrade,
        System.Action<TCtx> onNoReferrer,
        System.Action<TCtx> onOrigin,
        System.Action<TCtx> onOriginWhenCrossOrigin,
        System.Action<TCtx> onSameOrigin,
        System.Action<TCtx> onStrictOrigin,
        System.Action<TCtx> onStrictOriginWhenCrossOrigin
    )
    {
        switch (_kind)
        {
            case Kind.UnsafeUrl: onUnsafeUrl(ctx); break;
            case Kind.NoReferrerWhenDowngrade: onNoReferrerWhenDowngrade(ctx); break;
            case Kind.NoReferrer: onNoReferrer(ctx); break;
            case Kind.Origin: onOrigin(ctx); break;
            case Kind.OriginWhenCrossOrigin: onOriginWhenCrossOrigin(ctx); break;
            case Kind.SameOrigin: onSameOrigin(ctx); break;
            case Kind.StrictOrigin: onStrictOrigin(ctx); break;
            case Kind.StrictOriginWhenCrossOrigin: onStrictOriginWhenCrossOrigin(ctx); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action onUnsafeUrl,
        System.Action onNoReferrerWhenDowngrade,
        System.Action onNoReferrer,
        System.Action onOrigin,
        System.Action onOriginWhenCrossOrigin,
        System.Action onSameOrigin,
        System.Action onStrictOrigin,
        System.Action onStrictOriginWhenCrossOrigin
    )
    {
        switch (_kind)
        {
            case Kind.UnsafeUrl: onUnsafeUrl(); break;
            case Kind.NoReferrerWhenDowngrade: onNoReferrerWhenDowngrade(); break;
            case Kind.NoReferrer: onNoReferrer(); break;
            case Kind.Origin: onOrigin(); break;
            case Kind.OriginWhenCrossOrigin: onOriginWhenCrossOrigin(); break;
            case Kind.SameOrigin: onSameOrigin(); break;
            case Kind.StrictOrigin: onStrictOrigin(); break;
            case Kind.StrictOriginWhenCrossOrigin: onStrictOriginWhenCrossOrigin(); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.UnsafeUrl => "UnsafeUrl",
        Kind.NoReferrerWhenDowngrade => "NoReferrerWhenDowngrade",
        Kind.NoReferrer => "NoReferrer",
        Kind.Origin => "Origin",
        Kind.OriginWhenCrossOrigin => "OriginWhenCrossOrigin",
        Kind.SameOrigin => "SameOrigin",
        Kind.StrictOrigin => "StrictOrigin",
        Kind.StrictOriginWhenCrossOrigin => "StrictOriginWhenCrossOrigin",
        _ => "<invalid>"
    };
    public bool Equals(ReferrerPolicy other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.UnsafeUrl)
            return true;
        if (_kind == Kind.NoReferrerWhenDowngrade)
            return true;
        if (_kind == Kind.NoReferrer)
            return true;
        if (_kind == Kind.Origin)
            return true;
        if (_kind == Kind.OriginWhenCrossOrigin)
            return true;
        if (_kind == Kind.SameOrigin)
            return true;
        if (_kind == Kind.StrictOrigin)
            return true;
        if (_kind == Kind.StrictOriginWhenCrossOrigin)
            return true;
        return false;
    }
    public override bool Equals(object? obj) => obj is ReferrerPolicy other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.UnsafeUrl => (int)_kind,
            Kind.NoReferrerWhenDowngrade => (int)_kind,
            Kind.NoReferrer => (int)_kind,
            Kind.Origin => (int)_kind,
            Kind.OriginWhenCrossOrigin => (int)_kind,
            Kind.SameOrigin => (int)_kind,
            Kind.StrictOrigin => (int)_kind,
            Kind.StrictOriginWhenCrossOrigin => (int)_kind,
            _ => 0
        };
    }
    public static bool operator ==(ReferrerPolicy left, ReferrerPolicy right) => left.Equals(right);
    public static bool operator !=(ReferrerPolicy left, ReferrerPolicy right) => !(left == right);
}
}
