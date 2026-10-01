# Build and Test Script for MTAoars.Tests
# This script builds the test project using PowerShell

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "MTAoars Unit Test Build Script" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Find MSBuild
Write-Host "Searching for MSBuild..." -ForegroundColor Yellow

$msbuildPaths = @(
    "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"
)

$msbuild = $null
foreach ($path in $msbuildPaths) {
    if (Test-Path $path) {
        $msbuild = $path
        Write-Host "Found MSBuild at: $msbuild" -ForegroundColor Green
        break
    }
}

if (-not $msbuild) {
    Write-Host "MSBuild not found in common locations. Trying to use dotnet build..." -ForegroundColor Yellow
    
    try {
        dotnet build MTAoars.Tests\MTAoars.Tests.csproj --configuration Debug
        if ($LASTEXITCODE -eq 0) {
            Write-Host ""
            Write-Host "Build completed successfully using dotnet!" -ForegroundColor Green
        } else {
            Write-Host "Build failed!" -ForegroundColor Red
            exit 1
        }
    }
    catch {
        Write-Host "Error: Neither MSBuild nor dotnet CLI found!" -ForegroundColor Red
        Write-Host "Please install Visual Studio or .NET SDK" -ForegroundColor Red
        exit 1
    }
}
else {
    # Restore NuGet packages
    Write-Host ""
    Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
    
    $nugetExists = Get-Command nuget -ErrorAction SilentlyContinue
    if ($nugetExists) {
        nuget restore MTAoars.sln
    }
    else {
        Write-Host "NuGet command not found. Packages may need to be restored manually." -ForegroundColor Yellow
    }

    # Build the test project
    Write-Host ""
    Write-Host "Building MTAoars.Tests project..." -ForegroundColor Yellow
    
    & $msbuild "MTAoars.Tests\MTAoars.Tests.csproj" /t:Build /p:Configuration=Debug /v:minimal /nologo

    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "============================================" -ForegroundColor Green
        Write-Host "Build completed successfully!" -ForegroundColor Green
        Write-Host "============================================" -ForegroundColor Green
    }
    else {
        Write-Host ""
        Write-Host "Build FAILED!" -ForegroundColor Red
        exit 1
    }
}

Write-Host ""
Write-Host "To run the tests:" -ForegroundColor Cyan
Write-Host ""
Write-Host "Option 1: Using Visual Studio" -ForegroundColor White
Write-Host "  1. Open MTAoars.sln in Visual Studio" -ForegroundColor Gray
Write-Host "  2. Go to Test > Windows > Test Explorer" -ForegroundColor Gray
Write-Host "  3. Click 'Run All'" -ForegroundColor Gray
Write-Host ""
Write-Host "Option 2: Using NUnit Console Runner" -ForegroundColor White
Write-Host "  1. Install: nuget install NUnit.Console -Version 2.6.4" -ForegroundColor Gray
Write-Host "  2. Run: .\NUnit.Console.2.6.4\tools\nunit-console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll" -ForegroundColor Gray
Write-Host ""
Write-Host "Option 3: Using VSTest Console" -ForegroundColor White
Write-Host "  vstest.console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll" -ForegroundColor Gray
Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan

# Check if test assembly exists
$testDll = "MTAoars.Tests\bin\Debug\MTAoars.Tests.dll"
if (Test-Path $testDll) {
    Write-Host ""
    Write-Host "Test assembly found at: $testDll" -ForegroundColor Green
    
    # Try to get test count
    try {
        $assembly = [System.Reflection.Assembly]::LoadFrom((Resolve-Path $testDll))
        $testClasses = $assembly.GetTypes() | Where-Object { $_.GetCustomAttributes([NUnit.Framework.TestFixtureAttribute], $false).Count -gt 0 }
        $testCount = 0
        foreach ($class in $testClasses) {
            $testCount += ($class.GetMethods() | Where-Object { $_.GetCustomAttributes([NUnit.Framework.TestAttribute], $false).Count -gt 0 }).Count
        }
        Write-Host "Found $testCount test(s) in $($testClasses.Count) test fixture(s)" -ForegroundColor Green
    }
    catch {
        Write-Host "Could not analyze test assembly (this is OK)" -ForegroundColor Yellow
    }
}
else {
    Write-Host ""
    Write-Host "Warning: Test assembly not found at $testDll" -ForegroundColor Yellow
}

Write-Host ""

