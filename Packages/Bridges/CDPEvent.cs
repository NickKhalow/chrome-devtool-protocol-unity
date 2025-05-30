using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using REnum;
// ReSharper disable All

namespace CDPBridges
{
    [Serializable]
    public struct ParamsRaw
    {
        public int requestId;
        public string loaderId;
        public string documentURL;
        public Request.Raw request;
        public double timestamp; //MonotonicTime
        public double wallTime; //TimeSinceEpoch
        public Initiator.Raw initiator;

        public string type; //ResourceType
        public NetworkResponse.Raw response;
    }


    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE,
        HEAD,
        OPTIONS,
        PATCH,
        TRACE,
        CONNECT
    }


    [REnum]
    [REnumFieldEmpty("UnsafeUrl")]
    [REnumFieldEmpty("NoReferrerWhenDowngrade")]
    [REnumFieldEmpty("NoReferrer")]
    [REnumFieldEmpty("Origin")]
    [REnumFieldEmpty("OriginWhenCrossOrigin")]
    [REnumFieldEmpty("SameOrigin")]
    [REnumFieldEmpty("StrictOrigin")]
    [REnumFieldEmpty("StrictOriginWhenCrossOrigin")]
    public partial struct ReferrerPolicy
    {
        public string Value => Match(
            onUnsafeUrl: static () => "unsafe-url",
            onNoReferrerWhenDowngrade: static () => "no-referrer-when-downgrade",
            onNoReferrer: static () => "no-referrer",
            onOrigin: static () => "origin",
            onOriginWhenCrossOrigin: static () => "origin-when-cross-origin",
            onSameOrigin: static () => "same-origin",
            onStrictOrigin: static () => "strict-origin",
            onStrictOriginWhenCrossOrigin: static () => "strict-origin-when-cross-origin"
        );
    }


    [REnum]
    [REnumFieldEmpty("Unknown")]
    [REnumFieldEmpty("Neutral")]
    [REnumFieldEmpty("Insecure")]
    [REnumFieldEmpty("Secure")]
    [REnumFieldEmpty("Info")]
    [REnumFieldEmpty("InsecureBroken")]
    public partial struct SecurityState
    {
        public string Value => Match(
            onUnknown: static () => "unknown",
            onNeutral: static () => "neutral",
            onInsecure: static () => "insecure",
            onSecure: static () => "secure",
            onInfo: static () => "info",
            onInsecureBroken: static () => "insecure-broken"
        );
    }


    public enum ResourceType
    {
        Document,
        Stylesheet,
        Image,
        Media,
        Font,
        Script,
        TextTrack,
        XHR,
        Fetch,
        Prefetch,
        EventSource,
        WebSocket,
        Manifest,
        SignedExchange,
        Ping,
        CSPViolationReport,
        Preflight,
        FedCM,
        Other
    }


    public readonly struct NetworkResponse
    {
        public readonly string url;
        public readonly int status;
        public readonly string statusText;
        public readonly Dictionary<string, string> headers;
        public readonly string mimeType;
        public readonly string charset;
        public readonly Dictionary<string, string> requestHeaders;
        public readonly bool connectionReused;
        public readonly int connectionId;
        public readonly long encodedDataLength;
        public readonly TimeSinceEpoch responseTime;
        public readonly string cacheStorageCacheName;
        public readonly string protocol;
        public readonly SecurityState securityState;

        public NetworkResponse(
            string url,
            int status,
            string statusText,
            Dictionary<string, string> headers,
            string mimeType,
            string charset,
            Dictionary<string, string> requestHeaders,
            bool connectionReused,
            int connectionId,
            long encodedDataLength,
            TimeSinceEpoch responseTime,
            string cacheStorageCacheName,
            string protocol,
            SecurityState securityState)
        {
            this.url = url;
            this.status = status;
            this.statusText = statusText;
            this.headers = headers;
            this.mimeType = mimeType;
            this.charset = charset;
            this.requestHeaders = requestHeaders;
            this.connectionReused = connectionReused;
            this.connectionId = connectionId;
            this.encodedDataLength = encodedDataLength;
            this.responseTime = responseTime;
            this.cacheStorageCacheName = cacheStorageCacheName;
            this.protocol = protocol;
            this.securityState = securityState;
        }

        public Raw ToRaw()
        {
            return new Raw()
            {
                url = url,
                status = status,
                statusText = statusText,
                headers = headers,
                mimeType = mimeType,
                charset = charset,
                requestHeaders = requestHeaders,
                connectionReused = connectionReused,
                connectionId = connectionId,
                encodedDataLength = encodedDataLength,
                responseTime = responseTime.Seconds,
                cacheStorageCacheName = cacheStorageCacheName,
                protocol = protocol,
                securityState = securityState.Value
            };
        }


        [Serializable]
        public struct Raw
        {
            public string url;
            public int status;
            public string statusText;
            public Dictionary<string, string> headers;
            public string mimeType;
            public string charset;
            public Dictionary<string, string> requestHeaders;
            public bool connectionReused;
            public int connectionId;
            public long encodedDataLength;
            public double responseTime;
            public string cacheStorageCacheName;
            public string protocol;
            public string securityState;
        }
    }


    public readonly struct Request
    {
        public readonly string url;
        public readonly HttpMethod method;
        public readonly Dictionary<string, string> headers;
        public readonly ReferrerPolicy referrerPolicy;

        public Request(string url, HttpMethod method, Dictionary<string, string> headers, ReferrerPolicy referrerPolicy)
        {
            this.url = url;
            this.method = method;
            this.headers = headers;
            this.referrerPolicy = referrerPolicy;
        }

        public Raw ToRaw()
        {
            return new Raw()
            {
                url = url,
                method = method.ToString(),
                headers = headers,
                referrerPolicy = referrerPolicy.Value
            };
        }


        [Serializable]
        public struct Raw
        {
            public string url;
            public string method;
            public Dictionary<string, string> headers;
            public string referrerPolicy;
        }
    }


    public struct MonotonicTime
    {
        public double Seconds { get; }

        public MonotonicTime(double seconds)
        {
            Seconds = seconds;
        }

        public static MonotonicTime Now => new MonotonicTime(Stopwatch.GetTimestamp() / (double) Stopwatch.Frequency);
    }


    public struct TimeSinceEpoch
    {
        public double Seconds { get; }

        public TimeSinceEpoch(double seconds)
        {
            Seconds = seconds;
        }

        public static TimeSinceEpoch Now => new TimeSinceEpoch((DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds);
    }


    public readonly struct Initiator
    {
        public enum Type
        {
            parser,
            script,
            preload,
            SignedExchange,
            preflight,
            other
        }


        public readonly Type CurrentType;

        public Initiator(Type currentType)
        {
            CurrentType = currentType;
        }

        public Raw ToRaw()
        {
            return new Raw() { type = CurrentType.ToString() };
        }


        [Serializable]
        public struct Raw
        {
            public string type;
        }
    }


    [REnum]
    [REnumField(typeof(Network_requestWillBeSent))]
    [REnumField(typeof(Network_responseReceived))]
    public partial struct CDPEvent
    {
        public readonly struct Network_requestWillBeSent
        {
            public readonly int requestId;
            public readonly string loaderId;
            public readonly string documentURL;
            public readonly Request request;
            public readonly MonotonicTime timestamp;
            public readonly TimeSinceEpoch wallTime;
            public readonly Initiator Initiator;

            public Network_requestWillBeSent(
                int requestId,
                string loaderId,
                string documentURL,
                Request request,
                MonotonicTime timestamp,
                TimeSinceEpoch wallTime,
                Initiator initiator)
            {
                this.requestId = requestId;
                this.loaderId = loaderId;
                this.documentURL = documentURL;
                this.request = request;
                this.timestamp = timestamp;
                this.wallTime = wallTime;
                Initiator = initiator;
            }

            public ParamsRaw ToRaw()
            {
                return new ParamsRaw
                {
                    requestId = requestId,
                    loaderId = loaderId,
                    documentURL = documentURL,
                    request = request.ToRaw(),
                    timestamp = timestamp.Seconds,
                    wallTime = wallTime.Seconds,
                    initiator = Initiator.ToRaw()
                };
            }
        }


        public readonly struct Network_responseReceived
        {
            public readonly int requestId;
            public readonly string loaderId;
            public readonly MonotonicTime timestamp;
            public readonly ResourceType type;
            public readonly NetworkResponse response;

            public Network_responseReceived(
                int requestId,
                string loaderId,
                MonotonicTime timestamp,
                ResourceType type,
                NetworkResponse response)
            {
                this.requestId = requestId;
                this.loaderId = loaderId;
                this.timestamp = timestamp;
                this.type = type;
                this.response = response;
            }

            public ParamsRaw ToRaw()
            {
                return new ParamsRaw
                {
                    requestId = requestId,
                    loaderId = loaderId,
                    timestamp = timestamp.Seconds,
                    type = type.ToString(),
                    response = response.ToRaw()
                };
            }
        }


        private string MethodName() => Match(
            onNetwork_requestWillBeSent: static _ => "Network.requestWillBeSent",
            onNetwork_responseReceived: static _ => "Network.responseReceived"
        );

        private ParamsRaw ParamsRaw() => Match(
            onNetwork_requestWillBeSent: static e => e.ToRaw(),
            onNetwork_responseReceived: static e => e.ToRaw()
        );

        public CDPEventRaw ToRaw()
        {
            string method = MethodName();
            ParamsRaw paramsRaw = ParamsRaw();
            return new CDPEventRaw(method, paramsRaw);
        }


        [Serializable]
        public struct CDPEventRaw
        {
            public string method;
            public ParamsRaw @params;

            public CDPEventRaw(string method, ParamsRaw @params) : this()
            {
                this.method = method;
                this.@params = @params;
            }

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}