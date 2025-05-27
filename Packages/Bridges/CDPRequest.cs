using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using REnum;

namespace CDPBridges
{
    [Serializable]
    public struct CDPRequestRaw
    {
        public int id;
        public string method;
        public JObject jsonrpc;
        public JObject @params;

        public static CDPRequestRaw FromJson(string json)
        {
            return JsonConvert.DeserializeObject<CDPRequestRaw>(json);
        }

        public CDPRequest Into()
        {
            CDPMethod cdpMethod = CDPMethod.FromRaw(method, @params);
            return new CDPRequest(id, cdpMethod);
        }
    }


    public readonly struct CDPRequest
    {
        public readonly int Id;
        public readonly CDPMethod Method;

        public CDPRequest(int id, CDPMethod method)
        {
            Id = id;
            Method = method;
        }

        public static CDPRequest FromJson(string json)
        {
            CDPRequestRaw raw = CDPRequestRaw.FromJson(json);
            return raw.Into();
        }

        public override string ToString()
        {
            return $"({nameof(CDPRequest)} {{ id: {Id}, method: {Method.ToString()} }})";
        }
    }


    [REnum]
    [REnumFieldEmpty("Network_enable")]
    [REnumField(typeof(Unknown))]
    public partial struct CDPMethod
    {
        public static CDPMethod FromRaw(string method, JObject @params)
        {
            return method switch
            {
                "Network.enable" => Network_enable(),
                _ => FromUnknown(new Unknown(method))
            };
        }


        public readonly struct Unknown
        {
            public readonly string Method;

            public Unknown(string method)
            {
                Method = method;
            }

            public override string ToString()
            {
                return $"({nameof(Unknown)} {{ method: {Method} }})";
            }
        }
    }
}