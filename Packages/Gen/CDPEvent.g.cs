#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using REnum;
namespace CDPBridges {
public partial struct CDPEvent {
    public enum Kind {
        Network_requestWillBeSent,
        Network_responseReceived,
        Network_loadingFinished,
        Network_loadingFailed,
        Network_dataReceived,
    }
    private readonly Kind _kind;
    private readonly Network_requestWillBeSent _network_requestwillbesent;
    private readonly Network_responseReceived _network_responsereceived;
    private readonly Network_loadingFinished _network_loadingfinished;
    private readonly Network_loadingFailed _network_loadingfailed;
    private readonly Network_dataReceived _network_datareceived;
private CDPEvent(
Kind kind
,
Network_requestWillBeSent network_requestwillbesent = default,
Network_responseReceived network_responsereceived = default,
Network_loadingFinished network_loadingfinished = default,
Network_loadingFailed network_loadingfailed = default,
Network_dataReceived network_datareceived = default
){
_kind = kind;
_network_requestwillbesent = network_requestwillbesent;
_network_responsereceived = network_responsereceived;
_network_loadingfinished = network_loadingfinished;
_network_loadingfailed = network_loadingfailed;
_network_datareceived = network_datareceived;
}
    public static CDPEvent FromNetwork_requestWillBeSent(Network_requestWillBeSent value) => new CDPEvent(Kind.Network_requestWillBeSent, network_requestwillbesent: value);
    public static CDPEvent FromNetwork_responseReceived(Network_responseReceived value) => new CDPEvent(Kind.Network_responseReceived, network_responsereceived: value);
    public static CDPEvent FromNetwork_loadingFinished(Network_loadingFinished value) => new CDPEvent(Kind.Network_loadingFinished, network_loadingfinished: value);
    public static CDPEvent FromNetwork_loadingFailed(Network_loadingFailed value) => new CDPEvent(Kind.Network_loadingFailed, network_loadingfailed: value);
    public static CDPEvent FromNetwork_dataReceived(Network_dataReceived value) => new CDPEvent(Kind.Network_dataReceived, network_datareceived: value);
    public bool IsNetwork_requestWillBeSent(out Network_requestWillBeSent? value) {
        value = _network_requestwillbesent;
        return _kind == Kind.Network_requestWillBeSent;
    }
    public bool IsNetwork_responseReceived(out Network_responseReceived? value) {
        value = _network_responsereceived;
        return _kind == Kind.Network_responseReceived;
    }
    public bool IsNetwork_loadingFinished(out Network_loadingFinished? value) {
        value = _network_loadingfinished;
        return _kind == Kind.Network_loadingFinished;
    }
    public bool IsNetwork_loadingFailed(out Network_loadingFailed? value) {
        value = _network_loadingfailed;
        return _kind == Kind.Network_loadingFailed;
    }
    public bool IsNetwork_dataReceived(out Network_dataReceived? value) {
        value = _network_datareceived;
        return _kind == Kind.Network_dataReceived;
    }
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, Network_requestWillBeSent, T> onNetwork_requestWillBeSent,
        System.Func<TCtx, Network_responseReceived, T> onNetwork_responseReceived,
        System.Func<TCtx, Network_loadingFinished, T> onNetwork_loadingFinished,
        System.Func<TCtx, Network_loadingFailed, T> onNetwork_loadingFailed,
        System.Func<TCtx, Network_dataReceived, T> onNetwork_dataReceived
    )
    {
        return _kind switch
        {
            Kind.Network_requestWillBeSent => onNetwork_requestWillBeSent(ctx, _network_requestwillbesent),
            Kind.Network_responseReceived => onNetwork_responseReceived(ctx, _network_responsereceived),
            Kind.Network_loadingFinished => onNetwork_loadingFinished(ctx, _network_loadingfinished),
            Kind.Network_loadingFailed => onNetwork_loadingFailed(ctx, _network_loadingfailed),
            Kind.Network_dataReceived => onNetwork_dataReceived(ctx, _network_datareceived),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<Network_requestWillBeSent, T> onNetwork_requestWillBeSent,
        System.Func<Network_responseReceived, T> onNetwork_responseReceived,
        System.Func<Network_loadingFinished, T> onNetwork_loadingFinished,
        System.Func<Network_loadingFailed, T> onNetwork_loadingFailed,
        System.Func<Network_dataReceived, T> onNetwork_dataReceived
    )
    {
        return _kind switch
        {
            Kind.Network_requestWillBeSent => onNetwork_requestWillBeSent(_network_requestwillbesent),
            Kind.Network_responseReceived => onNetwork_responseReceived(_network_responsereceived),
            Kind.Network_loadingFinished => onNetwork_loadingFinished(_network_loadingfinished),
            Kind.Network_loadingFailed => onNetwork_loadingFailed(_network_loadingfailed),
            Kind.Network_dataReceived => onNetwork_dataReceived(_network_datareceived),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx, Network_requestWillBeSent> onNetwork_requestWillBeSent,
        System.Action<TCtx, Network_responseReceived> onNetwork_responseReceived,
        System.Action<TCtx, Network_loadingFinished> onNetwork_loadingFinished,
        System.Action<TCtx, Network_loadingFailed> onNetwork_loadingFailed,
        System.Action<TCtx, Network_dataReceived> onNetwork_dataReceived
    )
    {
        switch (_kind)
        {
            case Kind.Network_requestWillBeSent: onNetwork_requestWillBeSent(ctx, _network_requestwillbesent); break;
            case Kind.Network_responseReceived: onNetwork_responseReceived(ctx, _network_responsereceived); break;
            case Kind.Network_loadingFinished: onNetwork_loadingFinished(ctx, _network_loadingfinished); break;
            case Kind.Network_loadingFailed: onNetwork_loadingFailed(ctx, _network_loadingfailed); break;
            case Kind.Network_dataReceived: onNetwork_dataReceived(ctx, _network_datareceived); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action<Network_requestWillBeSent> onNetwork_requestWillBeSent,
        System.Action<Network_responseReceived> onNetwork_responseReceived,
        System.Action<Network_loadingFinished> onNetwork_loadingFinished,
        System.Action<Network_loadingFailed> onNetwork_loadingFailed,
        System.Action<Network_dataReceived> onNetwork_dataReceived
    )
    {
        switch (_kind)
        {
            case Kind.Network_requestWillBeSent: onNetwork_requestWillBeSent(_network_requestwillbesent); break;
            case Kind.Network_responseReceived: onNetwork_responseReceived(_network_responsereceived); break;
            case Kind.Network_loadingFinished: onNetwork_loadingFinished(_network_loadingfinished); break;
            case Kind.Network_loadingFailed: onNetwork_loadingFailed(_network_loadingfailed); break;
            case Kind.Network_dataReceived: onNetwork_dataReceived(_network_datareceived); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.Network_requestWillBeSent => _network_requestwillbesent.ToString() ?? "null",
        Kind.Network_responseReceived => _network_responsereceived.ToString() ?? "null",
        Kind.Network_loadingFinished => _network_loadingfinished.ToString() ?? "null",
        Kind.Network_loadingFailed => _network_loadingfailed.ToString() ?? "null",
        Kind.Network_dataReceived => _network_datareceived.ToString() ?? "null",
        _ => "<invalid>"
    };
    public bool Equals(CDPEvent other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.Network_requestWillBeSent)
            return System.Collections.Generic.EqualityComparer<Network_requestWillBeSent>.Default.Equals(_network_requestwillbesent, other._network_requestwillbesent);
        if (_kind == Kind.Network_responseReceived)
            return System.Collections.Generic.EqualityComparer<Network_responseReceived>.Default.Equals(_network_responsereceived, other._network_responsereceived);
        if (_kind == Kind.Network_loadingFinished)
            return System.Collections.Generic.EqualityComparer<Network_loadingFinished>.Default.Equals(_network_loadingfinished, other._network_loadingfinished);
        if (_kind == Kind.Network_loadingFailed)
            return System.Collections.Generic.EqualityComparer<Network_loadingFailed>.Default.Equals(_network_loadingfailed, other._network_loadingfailed);
        if (_kind == Kind.Network_dataReceived)
            return System.Collections.Generic.EqualityComparer<Network_dataReceived>.Default.Equals(_network_datareceived, other._network_datareceived);
        return false;
    }
    public override bool Equals(object? obj) => obj is CDPEvent other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.Network_requestWillBeSent => System.HashCode.Combine((int)_kind, _network_requestwillbesent),
            Kind.Network_responseReceived => System.HashCode.Combine((int)_kind, _network_responsereceived),
            Kind.Network_loadingFinished => System.HashCode.Combine((int)_kind, _network_loadingfinished),
            Kind.Network_loadingFailed => System.HashCode.Combine((int)_kind, _network_loadingfailed),
            Kind.Network_dataReceived => System.HashCode.Combine((int)_kind, _network_datareceived),
            _ => 0
        };
    }
    public static bool operator ==(CDPEvent left, CDPEvent right) => left.Equals(right);
    public static bool operator !=(CDPEvent left, CDPEvent right) => !(left == right);
}
}
