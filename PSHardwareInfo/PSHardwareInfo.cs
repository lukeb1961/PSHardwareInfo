//  leverages this assembly:  https://github.com/jinjinov/hardware.info
//
using System.Management.Automation;  //  PowerShell assembly from PowerShellStandardLibrary
using Hardware.Info;

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
}
// PS> Import-Module PSHardwareInfo
// PS> $hwi=Get-HardwareInfo
// PS> $hwi | Get-Member -MemberType Property
// PS> $hwi.CpuList.Name
