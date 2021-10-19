#New-PSmodule.ps1

$ModuleFolder = '.\PSHardwareInfoModule'

# create sub-folder for Module
if (-NOT (Test-Path -Path $ModuleFolder)) {
  $null=New-Item -Path $ModuleFolder -ItemType Directory
}

# create module manifest
if (-NOT (Test-Path -Path "$ModuleFolder\PSHardwareInfo.psd1")) {

  $moduleSettings = @{
    Author             = 'ThisUser'
    CopyRight          = '(c) ThisUser. No rights reserved.'
    RequiredAssemblies = 'Hardware.Info.dll'
    NestedModules      = '.\PSHardwareInfo.dll'
    CmdletsToExport    = 'Get-HardwareInfo'
    Path               = "$ModuleFolder\PSHardwareInfo.psd1"
  }
  New-ModuleManifest  @moduleSettings
}

# update the DLLs in the Module folder
$DLLs=(Get-ChildItem -Path .\PSHardwareInfo\bin\Debug\netstandard2.0 -Filter '*.dll' -Recurse).FullName
Copy-Item -Path $DLLs -Destination $ModuleFolder 
