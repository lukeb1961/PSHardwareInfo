#Install-PSmodule.ps1
$ModuleName = 'PSHardwareInfo'

function Test-WindowsOS {
  [bool]$IsOnWindows = [Runtime.InteropServices.RuntimeInformation]::IsOSPlatform([Runtime.InteropServices.OSPlatform]::Windows)
  return $IsOnWindows
}
function Test-AdminRights {
  if (Test-WindowsOS) {
    $currentUser = [Security.Principal.WindowsPrincipal]([Security.Principal.WindowsIdentity]::GetCurrent())
    [bool]$isAdminProcess = $currentUser.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
  }
  else {
    [bool]$isAdminProcess = 0 -eq (id -u)
  }
  $isAdminProcess
}

if (-NOT (Test-Path -Path .\PSHardwareInfoModule)) { & .\New-PSmodule.ps1 }

if (Test-AdminRights) {
  $modulePathPattern = if (Test-WindowsOS) { '\\Program Files\\' } else { '/usr/local' }
}
else { 
  $modulePathPattern = if (Test-WindowsOS) { ($HOME).replace('\', '\\') } else { $HOME }
}

$PathSeparator = [IO.Path]::PathSeparator
$modulePaths = $ENV:PSModulePath -split $PathSeparator

foreach ($modulePath in $modulePaths) {
  if ($modulePath -match $modulePathPattern) {
    $DeploymentPath = Join-Path -path $modulePath -ChildPath $ModuleName
    if (-NOT (Test-Path -Path $DeploymentPath)) { 
      $null = New-Item  -Path $DeploymentPath -ItemType Directory -Verbose
    }
    Write-Output "Installed: $DeploymentPath"
    Copy-Item -Path .\PSHardwareInfoModule\* -Destination $DeploymentPath -Force
  }
}
