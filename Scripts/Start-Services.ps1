# Constant Variables
$restApiServiceName = "React-Budget-API"
$restApiExe = "\run\Rest\Rest.exe"
$restApiPort = 5000
$restApiUrl = "http://localhost:$restApiPort"

# Set our working directory to the base project directory
Set-Location ((Split-Path $MyInvocation.MyCommand.Path) + "\..")

# If the service already exists, make sure that it is stopped
$serviceExists = (Get-Service -Name $restApiServiceName -ErrorAction SilentlyContinue).Length -gt 0
if ($serviceExists)
{
    Write-Output "Stopping Services..."
    Stop-Service -Name $restApiServiceName
}

# Publish the solution to get the most recent changes
Write-Output "Publishing Solution..."
dotnet publish --nologo -v q

# If the service doesn't already exist, create it
if (!($serviceExists))
{
    Write-Output "Creating Services..."
    New-Service -Name $restApiServiceName -BinaryPathName ("$PWD$restApiExe --urls=$restApiUrl") `
     -DisplayName "React Budget App API" -Description "REST API for the React Budget App" -StartupType Manual
}

# Start the services
Write-Output "Starting Services..."
Start-Service -Name $restApiServiceName