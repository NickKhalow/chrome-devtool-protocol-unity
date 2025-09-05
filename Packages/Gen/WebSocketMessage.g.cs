#nullable enable
using REnum;
namespace CDPBridges {
public partial struct WebSocketMessage {
    public enum Kind {
        Text,
        Binary,
    }
    private readonly Kind _kind;
    private readonly Text _text;
    private readonly Binary _binary;
private WebSocketMessage(
Kind kind
,
Text text = default,
Binary binary = default
){
_kind = kind;
_text = text;
_binary = binary;
}
    public static WebSocketMessage FromText(Text value) => new WebSocketMessage(Kind.Text, text: value);
    public static WebSocketMessage FromBinary(Binary value) => new WebSocketMessage(Kind.Binary, binary: value);
    public bool IsText(out Text? value) {
        value = _text;
        return _kind == Kind.Text;
    }
    public bool IsBinary(out Binary? value) {
        value = _binary;
        return _kind == Kind.Binary;
    }
    public T Match<TCtx, T>(
        TCtx ctx,
        System.Func<TCtx, Text, T> onText,
        System.Func<TCtx, Binary, T> onBinary
    )
    {
        return _kind switch
        {
            Kind.Text => onText(ctx, _text),
            Kind.Binary => onBinary(ctx, _binary),
            _ => throw new System.InvalidOperationException()
        };
    }
    public T Match<T>(
        System.Func<Text, T> onText,
        System.Func<Binary, T> onBinary
    )
    {
        return _kind switch
        {
            Kind.Text => onText(_text),
            Kind.Binary => onBinary(_binary),
            _ => throw new System.InvalidOperationException()
        };
    }
    public void Match<TCtx>(
        TCtx ctx,
        System.Action<TCtx, Text> onText,
        System.Action<TCtx, Binary> onBinary
    )
    {
        switch (_kind)
        {
            case Kind.Text: onText(ctx, _text); break;
            case Kind.Binary: onBinary(ctx, _binary); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public void Match(
        System.Action<Text> onText,
        System.Action<Binary> onBinary
    )
    {
        switch (_kind)
        {
            case Kind.Text: onText(_text); break;
            case Kind.Binary: onBinary(_binary); break;
            default: throw new System.InvalidOperationException();
        }
    }
    public override string ToString() => _kind switch
    {
        Kind.Text => _text.ToString() ?? "null",
        Kind.Binary => _binary.ToString() ?? "null",
        _ => "<invalid>"
    };
    public bool Equals(WebSocketMessage other)
    {
        if (_kind != other._kind) return false;
        if (_kind == Kind.Text)
            return System.Collections.Generic.EqualityComparer<Text>.Default.Equals(_text, other._text);
        if (_kind == Kind.Binary)
            return System.Collections.Generic.EqualityComparer<Binary>.Default.Equals(_binary, other._binary);
        return false;
    }
    public override bool Equals(object? obj) => obj is WebSocketMessage other && Equals(other);
    public override int GetHashCode()
    {
        return _kind switch
        {
            Kind.Text => System.HashCode.Combine((int)_kind, _text),
            Kind.Binary => System.HashCode.Combine((int)_kind, _binary),
            _ => 0
        };
    }
    public static bool operator ==(WebSocketMessage left, WebSocketMessage right) => left.Equals(right);
    public static bool operator !=(WebSocketMessage left, WebSocketMessage right) => !(left == right);
}
}
