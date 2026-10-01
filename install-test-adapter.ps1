# Script to Install NUnit Test Adapter and Verify Test Discovery
# Run this script from the MTAoars root directory

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "NUnit Test Adapter Installation Script" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Check if we're in the correct directory
if (-not (Test-Path "MTAoars.Tests\MTAoars.Tests.csproj")) {
    Write-Host "Error: Please run this script from the MTAoars solution root directory" -ForegroundColor Red
    exit 1
}

Write-Host "Step 1: Restoring NuGet packages..." -ForegroundColor Yellow
Write-Host ""

# Check for nuget.exe
$nugetExists = Get-Command nuget -ErrorAction SilentlyContinue
if ($nugetExists) {
    Write-Host "Found nuget.exe, restoring packages..." -ForegroundColor Green
    nuget restore MTAoars.sln
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Packages restored successfully" -ForegroundColor Green
    } else {
        Write-Host "✗ Package restore failed" -ForegroundColor Red
    }
} else {
    Write-Host "NuGet command not found. Trying alternative methods..." -ForegroundColor Yellow
    
    # Try dotnet restore
    $dotnetExists = Get-Command dotnet -ErrorAction SilentlyContinue
    if ($dotnetExists) {
        Write-Host "Using dotnet restore..." -ForegroundColor Green
        dotnet restore MTAoars.sln
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✓ Packages restored successfully" -ForegroundColor Green
        }
    } else {
        Write-Host "Neither nuget.exe nor dotnet CLI found." -ForegroundColor Yellow
        Write-Host "Please restore packages manually in Visual Studio:" -ForegroundColor Yellow
        Write-Host "  Right-click on Solution → Restore NuGet Packages" -ForegroundColor White
    }
}

Write-Host ""
Write-Host "Step 2: Checking NUnit Test Adapter..." -ForegroundColor Yellow
Write-Host ""

$adapterPath = "packages\NUnitTestAdapter.2.0.0"
if (Test-Path $adapterPath) {
    Write-Host "✓ NUnit Test Adapter found at: $adapterPath" -ForegroundColor Green
} else {
    Write-Host "✗ NUnit Test Adapter not found" -ForegroundColor Red
    Write-Host "Installing NUnit Test Adapter..." -ForegroundColor Yellow
    
    if ($nugetExists) {
        nuget install NUnitTestAdapter -Version 2.0.0 -OutputDirectory packages
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✓ NUnit Test Adapter installed successfully" -ForegroundColor Green
        }
    } else {
        Write-Host "Please install manually using Package Manager Console:" -ForegroundColor Yellow
        Write-Host "  Install-Package NUnitTestAdapter -Version 2.0.0 -ProjectName MTAoars.Tests" -ForegroundColor White
    }
}

Write-Host ""
Write-Host "Step 3: Checking test assembly..." -ForegroundColor Yellow
Write-Host ""

$testDll = "MTAoars.Tests\bin\Debug\MTAoars.Tests.dll"
if (Test-Path $testDll) {
    Write-Host "✓ Test assembly found: $testDll" -ForegroundColor Green
    
    # Get file info
    $fileInfo = Get-Item $testDll
    Write-Host "  Last modified: $($fileInfo.LastWriteTime)" -ForegroundColor Gray
    Write-Host "  Size: $([math]::Round($fileInfo.Length / 1KB, 2)) KB" -ForegroundColor Gray
} else {
    Write-Host "✗ Test assembly not found" -ForegroundColor Red
    Write-Host "Building the test project..." -ForegroundColor Yellow
    
    $dotnetExists = Get-Command dotnet -ErrorAction SilentlyContinue
    if ($dotnetExists) {
        dotnet build MTAoars.Tests\MTAoars.Tests.csproj --configuration Debug
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✓ Build successful" -ForegroundColor Green
        } else {
            Write-Host "✗ Build failed" -ForegroundColor Red
        }
    } else {
        Write-Host "Please build the solution in Visual Studio (Ctrl+Shift+B)" -ForegroundColor Yellow
    }
}

Write-Host ""
Write-Host "Step 4: Checking for NUnit references..." -ForegroundColor Yellow
Write-Host ""

$nunitDll = "packages\NUnit.2.6.4\lib\nunit.framework.dll"
if (Test-Path $nunitDll) {
    Write-Host "✓ NUnit framework found: $nunitDll" -ForegroundColor Green
} else {
    Write-Host "✗ NUnit framework not found" -ForegroundColor Red
}

Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Next Steps:" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "1. Close Visual Studio 2017 (if open)" -ForegroundColor White
Write-Host "2. Reopen the solution: MTAoars.sln" -ForegroundColor White
Write-Host "3. Rebuild the solution: Build → Rebuild Solution" -ForegroundColor White
Write-Host "4. Open Test Explorer: Test → Windows → Test Explorer (Ctrl+E, T)" -ForegroundColor White
Write-Host "5. You should see 16 tests listed!" -ForegroundColor White
Write-Host ""
Write-Host "If tests still don't appear:" -ForegroundColor Yellow
Write-Host "  Option A: Install NUnit Test Adapter extension" -ForegroundColor Gray
Write-Host "    Tools → Extensions and Updates → Search for 'NUnit Test Adapter'" -ForegroundColor Gray
Write-Host ""
Write-Host "  Option B: Run tests from command line" -ForegroundColor Gray
Write-Host "    .\NUnit.Runners.2.6.4\tools\nunit-console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll" -ForegroundColor Gray
Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Optional: Try to run tests if NUnit Console is available
$nunitConsole = Get-ChildItem -Path "." -Filter "nunit-console.exe" -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1

if ($nunitConsole) {
    Write-Host "Found NUnit Console Runner at: $($nunitConsole.FullName)" -ForegroundColor Green
    Write-Host ""
    $response = Read-Host "Would you like to run the tests now using NUnit Console? (y/n)"
    
    if ($response -eq 'y' -or $response -eq 'Y') {
        Write-Host ""
        Write-Host "Running tests..." -ForegroundColor Yellow
        Write-Host ""
        
        & $nunitConsole.FullName "$testDll" /nologo
    }
} else {
    Write-Host "NUnit Console Runner not found. Install it with:" -ForegroundColor Yellow
    Write-Host "  nuget install NUnit.Runners -Version 2.6.4" -ForegroundColor Gray
}

Write-Host ""
Write-Host "Script completed!" -ForegroundColor Green
Write-Host ""

