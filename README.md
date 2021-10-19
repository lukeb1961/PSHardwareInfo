# PSGetHardwareInfo
a Powershell CMDLET wrapper of https://github.com/Jinjinov/Hardware.Info
The Hardware.Info .netstandard code gets hardware info for Windows/Linux/Mac.
This (tiny) cmdlet acts as a wrapper.

PS> Import-Module PSHardwareInfo

PS> $HWI=Get-HardwareInfo

PS> $HWI.BiosList.Manufacturer

PS> $HWI.CpuList.Name

PS> $HWI.MemoryStatus.TotalPhysical

PS> $HWI.DriveList.PartitionList.VolumeList

PS> $HWI.MotherboardList

PS> $HWI.VideoControllerList.Name

PS> $HWI.NetworkAdapterList.Name
