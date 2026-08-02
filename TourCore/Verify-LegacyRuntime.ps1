param(
    [string[]] $Hosts = @('TourCore.Admin.Api.Legacy', 'TourCore.Search.Api.Legacy'),
    [string] $Configuration = 'Debug',
    [switch] $SingleHost
)

$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $MyInvocation.MyCommand.Path

if (-not $SingleHost -and $Hosts.Count -gt 1) {
    foreach ($hostName in $Hosts) {
        & powershell.exe -NoProfile -ExecutionPolicy Bypass -File $MyInvocation.MyCommand.Path -Hosts $hostName -Configuration $Configuration -SingleHost
        if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
    }
    Write-Host 'Legacy runtime dependencies: OK'
    exit 0
}

$failures = New-Object System.Collections.Generic.List[string]

foreach ($hostName in $Hosts) {
    $hostPath = Join-Path $root $hostName
    $binPath = Join-Path $hostPath 'bin'
    $configPath = Join-Path $hostPath 'Web.config'
    $packagesPath = Join-Path $hostPath 'packages.config'
    if (-not (Test-Path -LiteralPath $binPath)) {
        $failures.Add("$hostName`: отсутствует bin после сборки.")
        continue
    }

    [xml] $webConfig = Get-Content -LiteralPath $configPath
    [xml] $packagesConfig = Get-Content -LiteralPath $packagesPath
    $packageIds = @{}
    $packagesConfig.packages.package | ForEach-Object { $packageIds[$_.id] = $true }
    $namespaceManager = New-Object System.Xml.XmlNamespaceManager($webConfig.NameTable)
    $namespaceManager.AddNamespace('asm', 'urn:schemas-microsoft-com:asm.v1')
    $localAssemblies = @{}

    Get-ChildItem -LiteralPath $binPath -Filter '*.dll' -File | ForEach-Object {
        try {
            $assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName($_.FullName)
            $localAssemblies[$assemblyName.Name] = @{ Name = $assemblyName; Path = $_.FullName }
        } catch {
            $failures.Add("$hostName`: не удалось прочитать $($_.Name): $($_.Exception.Message)")
        }
    }

    if (-not $localAssemblies.ContainsKey($hostName)) {
        $failures.Add("$hostName`: отсутствует основная сборка $hostName.dll.")
        continue
    }

    $pending = New-Object 'System.Collections.Generic.Queue[object]'
    $visited = New-Object 'System.Collections.Generic.HashSet[string]'
    $pending.Enqueue($localAssemblies[$hostName])

    while ($pending.Count -gt 0) {
        $local = $pending.Dequeue()
        if (-not $visited.Add($local.Name.Name)) { continue }
        try {
            $assembly = [System.Reflection.Assembly]::ReflectionOnlyLoadFrom($local.Path)
            foreach ($reference in $assembly.GetReferencedAssemblies()) {
                if ($localAssemblies.ContainsKey($reference.Name)) {
                    $pending.Enqueue($localAssemblies[$reference.Name])
                    $actual = $localAssemblies[$reference.Name].Name.Version
                    if ($actual -ne $reference.Version) {
                        $node = $webConfig.SelectSingleNode("//asm:dependentAssembly[asm:assemblyIdentity/@name='$($reference.Name)']", $namespaceManager)
                        $redirectVersion = if ($node) { $node.bindingRedirect.newVersion } else { $null }
                        if ($redirectVersion -ne $actual.ToString()) {
                            $failures.Add("$hostName`: $($local.Name.Name) требует $($reference.Name) $($reference.Version), в bin $actual, корректный bindingRedirect отсутствует.")
                        }
                    }
                    continue
                }

                $isFrameworkAssembly =
                    $reference.Name -eq 'mscorlib' -or
                    $reference.Name -eq 'Microsoft.CSharp' -or
                    $reference.Name.StartsWith('System', [System.StringComparison]::Ordinal)
                if ($isFrameworkAssembly -and -not $packageIds.ContainsKey($reference.Name)) {
                    continue
                }

                try { [void][System.Reflection.Assembly]::ReflectionOnlyLoad($reference.FullName) }
                catch { $failures.Add("$hostName`: отсутствует runtime-зависимость $($reference.FullName), требуемая $($local.Name.Name).") }
            }
        } catch {
            $failures.Add("$hostName`: невозможно проверить $($local.Name.Name): $($_.Exception.Message)")
        }
    }
}

if ($failures.Count -gt 0) {
    $failures | ForEach-Object { Write-Error $_ }
    exit 1
}

if (-not $SingleHost) { Write-Host 'Legacy runtime dependencies: OK' }
