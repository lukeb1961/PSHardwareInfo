#New-PSmodule.ps1

$ModuleFolder = '.\PSHardwareInfoModule'
$GUID = 'b0704441-695f-487f-8941-175c3b69c2d6'

# create sub-folder for Module
if (-NOT (Test-Path -Path $ModuleFolder)) {
  $null=New-Item -Path $ModuleFolder -ItemType Directory
}

# create module manifest
if (Test-Path -Path "$ModuleFolder\PSHardwareInfo.psd1") {Remove-Item -Path "$ModuleFolder\PSHardwareInfo.psd1" -Force}

$moduleSettings = @{
    Guid               = $GUID
    Author             = 'ThisUser'
    CopyRight          = '(c) ThisUser. No rights reserved.'
    RootModule         = 'PSHardwareInfo.dll'
    RequiredAssemblies = 'Hardware.Info.dll'
    CmdletsToExport    = 'Get-HardwareInfo'
    AliasesToExport    = 'ghi'
    Path               = "$ModuleFolder\PSHardwareInfo.psd1"
  }
New-ModuleManifest  @moduleSettings


# update the DLLs in the Module folder
$DLLs=(Get-ChildItem -Path .\PSHardwareInfo\bin\Debug\netstandard2.0 -Filter '*.dll' -Recurse).FullName
Copy-Item -Path $DLLs -Destination $ModuleFolder 
