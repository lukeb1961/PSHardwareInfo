using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Globalization;

namespace OS.Info
{

    internal interface IOSinfoRetrieval
    {
        List<DotNet> GetDotNetList();
    }

    public interface IOSinfo
    {
        List<DotNet> DotNetList { get; }
        void RefreshDotNetList();
    }

    public class OSinfo : IOSinfo
    {
        public List<DotNet> DotNetList { get; private set; } = new List<DotNet>();

        private readonly OSinfoRetrieval _OSInfoRetrieval = null!;
        public OSinfo()
        {
            _OSInfoRetrieval = new OS.Info.OSinfoRetrieval();
        }


        public void RefreshDotNetList() => DotNetList = _OSInfoRetrieval.GetDotNetList();

        public void RefreshAll()
        {
            RefreshDotNetList();
        }

    }

    internal class OSinfoRetrieval : IOSinfoRetrieval
    {
        public List<DotNet> GetDotNetList()
        {
            List<DotNet> DotNetList = new List<DotNet>();

            DotNet dotnet = new DotNet();

            dotnet.architecture = RuntimeInformation.OSArchitecture;
            dotnet.description = RuntimeInformation.OSDescription;
            dotnet.processArchitecture = RuntimeInformation.ProcessArchitecture;
            dotnet.localTimeZone = TimeZoneInfo.Local;
            dotnet.OSversion = Environment.OSVersion;
            dotnet.machineName = Environment.MachineName;
            dotnet.systemDirectory = Environment.SystemDirectory;
            dotnet.Is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
            dotnet.SystemPageSize = Environment.SystemPageSize;
            dotnet.upTime = TimeSpan.FromMilliseconds(Environment.TickCount);
            dotnet.lastBootUpTime = DateTime.Now.Subtract(dotnet.upTime);
            dotnet.FrameworkDescription = RuntimeInformation.FrameworkDescription;
            dotnet.CurrentCulture = CultureInfo.CurrentCulture;

            string OStype = "FreeBSD";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) { OStype = "Windows"; }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))   { OStype = "Linux"; }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))     { OStype = "OSX"; }
            dotnet.type = OStype;

            DotNetList.Add(dotnet);

            return DotNetList;
        }
    }

    public class DotNet
    {
        public string type { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string machineName { get; set; } = string.Empty;
        public OperatingSystem? OSversion { get; set; }
        public bool Is64BitOperatingSystem { get; set; }
        public DateTime lastBootUpTime { get; set; }
        public TimeSpan upTime { get; set; }
        public TimeZoneInfo localTimeZone { get; set; } = TimeZoneInfo.Local;
        public string systemDirectory { get; set; } = string.Empty;
        public Architecture architecture { get; set; }
        public Architecture processArchitecture { get; set; }
        public int SystemPageSize { get; set; }
        public string? FrameworkDescription { get; set; }
        public CultureInfo? CurrentCulture { get; set; }

        public override string ToString()
        {
            return
                "Type: " + type + Environment.NewLine +
                "description: " + description + Environment.NewLine +
                "machineName: " + machineName + Environment.NewLine +
                "version: " + OSversion + Environment.NewLine +
                "Is64BitOperatingSystem:" + Is64BitOperatingSystem + Environment.NewLine +
                "lastBootUpTime: " + lastBootUpTime + Environment.NewLine +
                "UpTime: " + upTime + Environment.NewLine +
                "localTimeZone: " + localTimeZone + Environment.NewLine +
                "systemDirectory: " + systemDirectory + Environment.NewLine +
                "architecture: " + architecture + Environment.NewLine +
                "processArchitecture: " + processArchitecture + Environment.NewLine +
                "SystemPageSize:" + SystemPageSize + Environment.NewLine +
                "Framework:" + FrameworkDescription + Environment.NewLine +
                "CurrentCulture:" + CurrentCulture + Environment.NewLine;
        }
    }

}

