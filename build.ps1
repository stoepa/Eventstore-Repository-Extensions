# Taken from MediatR https://github.com/jbogard/MediatR

<#
.SYNOPSIS
  This is a helper function that runs a scriptblock and checks the PS variable $lastexitcode
  to see if an error occcured. If an error is detected then an exception is thrown.
  This function allows you to run command-line programs without having to
  explicitly check the $lastexitcode variable.
.EXAMPLE
  exec { svn info $repository_trunk } "Error executing SVN. Please verify SVN command-line client is installed"
#>
function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

$artifacts = ".\artifacts"

if(Test-Path $artifacts) { Remove-Item $artifacts -Force -Recurse }

exec { & dotnet clean .\src\EventStore.Repository -c Release }

exec { & dotnet build .\src\EventStore.Repository -c Release }

exec { dotnet pack .\src\EventStore.Repository.Extensions.Microsoft.DependancyInjection -c Release -o $artifacts --no-build }