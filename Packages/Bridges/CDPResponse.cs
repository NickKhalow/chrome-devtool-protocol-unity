using System;
using Newtonsoft.Json;
using REnum;

namespace CDPBridges
{
    [Serializable]
    public struct CDPResponseRaw
    {
        public int id;
        public string result;

        public CDPResponseRaw(int id, string result)
        {
            this.id = id;
            this.result = result;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public readonly struct CDPResponse
    {
        public readonly int Id;
        public readonly CDPResult Result;

        public CDPResponse(int id, CDPResult result)
        {
            Id = id;
            Result = result;
        }

        public CDPResponseRaw Into()
        {
            return new CDPResponseRaw(Id, Result.ToJson());
        }

        public string ToJson()
        {
            return Into().ToJson();
        }

        public override string ToString()
        {
            return $"({nameof(CDPResponse)} {{ id: {Id}, method: {Result.ToString()} }})";
        }
    }


    [REnum]
    [REnumFieldEmpty("Network_enable")]
    public partial struct CDPResult
    {
        private const string EmptyJson = "{}";

        public string ToJson()
        {
            return Match(
                onNetwork_enable: static () => EmptyJson
            );
        }
    }
}