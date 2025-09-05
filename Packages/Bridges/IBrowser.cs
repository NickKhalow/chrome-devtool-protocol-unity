using System;
using REnum;

namespace CDPBridges
{
    public interface IBrowser
    {
        BrowserOpenResult OpenUrl(string url);
    }


    [REnum]
    [REnumPregenerated]
    [REnumFieldEmpty("Success")]
    [REnumField(typeof(BrowserOpenError))]
    public partial struct BrowserOpenResult
    {
    }


    [REnum]
    [REnumPregenerated]
    [REnumFieldEmpty("ErrorChromeNotInstalled")]
    [REnumField(typeof(Exception))]
    public partial struct BrowserOpenError
    {
    }
}