#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using REnum;
namespace CDPBridges {
public partial struct SecurityState {
    public enum Kind {
        Unknown,
        Neutral,
        Insecure,
        Secure,
        Info,
        InsecureBroken,
    }
    private readonly Kind _kind;
private SecurityState(
Kind kind

){
_kind = kind;
}
    public static SecurityState Unknown() => new SecurityState(Kind.Unknown);
    public static SecurityState Neutral() => new SecurityState(Kind.Neutral);
    public static SecurityState Insecure() => new SecurityState(Kind.Insecure);
    public static SecurityState Secure() => new SecurityState(Kind.Secure);
    public static SecurityState Info() => new SecurityState(Kind.Info);
    public static SecurityState InsecureBroken() => new SecurityState(Kind.InsecureBroken);
    public bool IsUnknown() => _kind == Kind.Unknown;
    public bool IsNeutral() => _kind == Kind.Neutral;
    public bool IsInsecure() => _kind == Kind.Insecure;
    public bool IsSecure() => _kind == Kind.Secure;
    public bool IsInfo() => _kind == Kind.Info;
    public bool IsInsecureBroken() => _kind == Kind.InsecureBroken;
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, T> onUnknown,
        System.Func<TCtx, T> onNeutral,
        System.Func<TCtx, T> onInsecure,
        System.Func<TCtx, T> onSecure,
        System.Func<TCtx, T> onInfo,
        System.Func<TCtx, T> onInsecureBroken
    )
    {
        return _kind switch
        {
            Kind.Unknown => onUnknown(ctx),
            Kind.Neutral => onNeutral(ctx),
            Kind.Insecure => onInsecure(ctx),
            Kind.Secure => onSecure(ctx),
            Kind.Info => onInfo(ctx),
            Kind.InsecureBroken => onInsecureBroken(ctx),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<T> onUnknown,
        System.Func<T> onNeutral,
        System.Func<T> onInsecure,
        System.Func<T> onSecure,
        System.Func<T> onInfo,
        System.Func<T> onInsecureBroken
    )
    {
        return _kind switch
        {
            Kind.Unknown => onUnknown(),
            Kind.Neutral => onNeutral(),
            Kind.Insecure => onInsecure(),
            Kind.Secure => onSecure(),
            Kind.Info => onInfo(),
            Kind.InsecureBroken => onInsecureBroken(),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx> onUnknown,
        System.Action<TCtx> onNeutral,
        System.Action<TCtx> onInsecure,
        System.Action<TCtx> onSecure,
        System.Action<TCtx> onInfo,
        System.Action<TCtx> onInsecureBroken
    )
    {
        switch (_kind)
        {
            case Kind.Unknown: onUnknown(ctx); break;
            case Kind.Neutral: onNeutral(ctx); break;
            case Kind.Insecure: onInsecure(ctx); break;
            case Kind.Secure: onSecure(ctx); break;
            case Kind.Info: onInfo(ctx); break;
            case Kind.InsecureBroken: onInsecureBroken(ctx); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action onUnknown,
        System.Action onNeutral,
        System.Action onInsecure,
        System.Action onSecure,
        System.Action onInfo,
        System.Action onInsecureBroken
    )
    {
        switch (_kind)
        {
            case Kind.Unknown: onUnknown(); break;
            case Kind.Neutral: onNeutral(); break;
            case Kind.Insecure: onInsecure(); break;
            case Kind.Secure: onSecure(); break;
            case Kind.Info: onInfo(); break;
            case Kind.InsecureBroken: onInsecureBroken(); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.Unknown => "Unknown",
        Kind.Neutral => "Neutral",
        Kind.Insecure => "Insecure",
        Kind.Secure => "Secure",
        Kind.Info => "Info",
        Kind.InsecureBroken => "InsecureBroken",
        _ => "<invalid>"
    };
    public bool Equals(SecurityState other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.Unknown)
            return true;
        if (_kind == Kind.Neutral)
            return true;
        if (_kind == Kind.Insecure)
            return true;
        if (_kind == Kind.Secure)
            return true;
        if (_kind == Kind.Info)
            return true;
        if (_kind == Kind.InsecureBroken)
            return true;
        return false;
    }
    public override bool Equals(object? obj) => obj is SecurityState other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.Unknown => (int)_kind,
            Kind.Neutral => (int)_kind,
            Kind.Insecure => (int)_kind,
            Kind.Secure => (int)_kind,
            Kind.Info => (int)_kind,
            Kind.InsecureBroken => (int)_kind,
            _ => 0
        };
    }
    public static bool operator ==(SecurityState left, SecurityState right) => left.Equals(right);
    public static bool operator !=(SecurityState left, SecurityState right) => !(left == right);
}
}
