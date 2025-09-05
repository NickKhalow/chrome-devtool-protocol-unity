using System;
using System.Diagnostics;

namespace CDPBridges
{
    public class ProcessBrowser : IBrowser
    {
        public BrowserOpenResult OpenUrl(string url)
        {

// ProcessBrowser uses Mono API and it's not available in IL2CPP builds
#if !ENABLE_IL2CPP && !CDP_EXCLUDE_PROCESS_BROWSER

            try
            {
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                const string chromePath = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";
                if (!System.IO.File.Exists(chromePath))
                {
                    return BrowserOpenResult.FromBrowserOpenError(
                        BrowserOpenError.ErrorChromeNotInstalled()
                    );
                }

                Process.Start(
                    "open",
                    $"-a \"Google Chrome\" \"{url}\""
                );
#else
                // Windows: check if Chrome is registered in the registry
                string chromeKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";
                object? path = Microsoft.Win32.Registry.GetValue(chromeKey, "", null);

                if (path is not string chromeExePath || !System.IO.File.Exists(chromeExePath))
                {
                    return BrowserOpenResult.ErrorChromeNotInstalled();
                }

                Process.Start(
                    "chrome",
                    $"\"{url}\""
                );
#endif // UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                return BrowserOpenResult.Success();
            }
            catch (Exception e)
            {
                return BrowserOpenResult.FromBrowserOpenError(
                    BrowserOpenError.FromException(e)
                );
            }
#else

            return BrowserOpenResult.FromBrowserOpenError(BrowserOpenError.FromException(new PlatformNotSupportedException("IL2CPP is not supported")));
#endif // !ENABLE_IL2CPP
        }
    }
}
