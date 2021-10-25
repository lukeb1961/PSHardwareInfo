using System.Management.Automation;  //  PowerShell assembly from PowerShellStandardLibrary
using Hardware.Info;                 //  leverages  https://github.com/jinjinov/hardware.info
using OS.Info;

namespace PSHardwareInfo
{
    [Cmdlet(VerbsCommon.Get, "HardwareInfo")]
    [Alias("ghi")]
    [OutputType(typeof(HardwareInfo))]
    public class GetHardwareInfoCmdLet : Cmdlet
    {
        protected internal HardwareInfo? HardwareInfo { get; }

        protected override void BeginProcessing()
        {
            HardwareInfo HardwareInfo = new HardwareInfo();
            HardwareInfo.RefreshAll();

            WriteObject(HardwareInfo);
        }

    }


    [Cmdlet(VerbsCommon.Get, "OSInfo")]
    [Alias("gosi")]
    [OutputType(typeof(OSinfo))]
    public class GetOSInfoCmdLet : Cmdlet
    {
        protected internal OSinfo? OSInfo { get; }

        protected override void BeginProcessing()
        {
            OSinfo OSInfo = new OSinfo();
            OSInfo.RefreshAll();

            WriteObject(OSInfo);
        }

    }
}
// PS> Import-Module PSHardwareInfo
// PS> $hwi=Get-HardwareInfo
// PS> $hwi | Get-Member -MemberType Property
// PS> $hwi.CpuList.Name
//
// PS> $osi = Get-OSinfo
// PS> $osi.DotNetList.localTimeZone
