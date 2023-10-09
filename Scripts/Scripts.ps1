<#  .Description 
    Builds the solution and deploys the new local version
#>  
function Deploy-Local-Version
{
    python $PSScriptRoot\Scripts.py Deploy
}

<#  .Description 
    Builds the solution, deploys the new local version, then publishes the new version
#>  
function Deploy-Published-Version
{
    python $PSScriptRoot\Scripts.py Deploy --published
}

<#  .Description 
    Creates a new Entity Framework Core migration in the Schema directory with the given name.

    .Parameter Name
    Name of the new migration to create.
#>  
function New-Migration
{
    param( [Parameter(Mandatory)] [String] $Name )
    python $PSScriptRoot\Scripts.py New-Migration --name $Name
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
    if ($RemoveAll)
    {
        python $PSScriptRoot\Scripts.py Remove-Migration --remove-all
    }
    else
    {
        python $PSScriptRoot\Scripts.py Remove-Migration
    }
    
}