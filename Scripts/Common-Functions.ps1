<#  .Description 
    Tests to see if the current user has Administrator privileges. 
#>  
function Test-Administrator  
{
    $user = [Security.Principal.WindowsIdentity]::GetCurrent();
    (New-Object Security.Principal.WindowsPrincipal $user).IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator)  
}

<#  .Description 
    Publishes the RestApi project and starts the React-Budget-API service that hosts the API.
    If the service doesn't already exist, it will be created (requires Administrator priviledges).
#>  
function Publish-API
{
    # Constant Variables
    $restApiServiceName = "React-Budget-API"
    $restApiExe = "$PSScriptRoot\run\RestApi\RestApi.exe"
    $restApiPort = 5000
    $restApiUrl = "http://localhost:$restApiPort"

    # Check if the service already exists. If it doesn't, we need administrator permissions to create it
    $serviceExists = (Get-Service -Name $restApiServiceName -ErrorAction SilentlyContinue).Length -gt 0
    if (!(Test-Administrator))
    {
        throw "The React-Budget-API service does not currently exist. Adminstrator priviledges are required to create it."
    }

    # If the service already exists, make sure that it is stopped
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
}

<#  .Description 
    Creates a new Entity Framework Core migration in the Schema directory with the given name.

    .Parameter Name
    Name of the new migration to create.
#>  
function New-Migration
{
    param( [Parameter(Mandatory)] [String] $Name )

    # Save off the current working directory and move to the Schema directory
    $previousWorkingDirectory = $PWD
    Set-Location $PSScriptRoot\..\Schema

    # Create the new migration
    dotnet ef migrations add $Name --msbuildprojectextensionspath ..\obj\Schema

    # Restore the original working directory
    Set-Location $previousWorkingDirectory
}

<#  .Description 
    Removes an Entity Framework Core migration from the Schema directory. By default, it will remove
    the most recent migration. If the RemoveAll flag is set, all migrations will be removed along with
    the Migrations directory.

    .Parameter RemoveAll
    If set, this function will remove all migrations and delete the Migrations directory
#>  
function Remove-Migration
{
    param( [Switch] $RemoveAll )

    # Save off the current working directory and move to the Schema directory
    $previousWorkingDirectory = $PWD
    Set-Location $PSScriptRoot\..\Schema

    if ($RemoveAll)
    {
        # Drop the Migrations directory
        Remove-Item -LiteralPath "Migrations" -Force -Recurse
    }
    else {
        # Remove the most recent migration
        dotnet ef migrations remove --msbuildprojectextensionspath ..\obj\Schema
    }
    
    # Restore the original working directory
    Set-Location $previousWorkingDirectory
}