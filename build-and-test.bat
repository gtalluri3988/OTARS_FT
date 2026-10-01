@echo off
REM Build and Test Script for MTAoars.Tests
REM This script builds the test project and provides instructions for running tests

echo ============================================
echo MTAoars Unit Test Build Script
echo ============================================
echo.

REM Set the MSBuild path (adjust if needed)
set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"

REM Check if MSBuild exists at the specified path
if not exist %MSBUILD_PATH% (
    echo MSBuild not found at %MSBUILD_PATH%
    echo Please install Visual Studio or update the MSBUILD_PATH in this script
    echo.
    echo Attempting to use dotnet build instead...
    dotnet build MTAoars.Tests\MTAoars.Tests.csproj
    goto :EOF
)

echo Building MTAoars.Tests project...
echo.

REM Restore NuGet packages
echo Restoring NuGet packages...
nuget restore MTAoars.sln

REM Build the test project
echo Building the test project...
%MSBUILD_PATH% MTAoars.Tests\MTAoars.Tests.csproj /t:Build /p:Configuration=Debug /v:minimal

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo Build FAILED!
    exit /b %ERRORLEVEL%
)

echo.
echo ============================================
echo Build completed successfully!
echo ============================================
echo.
echo To run the tests:
echo.
echo Option 1: Using Visual Studio
echo   1. Open MTAoars.sln in Visual Studio
echo   2. Go to Test ^> Windows ^> Test Explorer
echo   3. Click "Run All"
echo.
echo Option 2: Using NUnit Console Runner
echo   1. Install: nuget install NUnit.Console -Version 2.6.4
echo   2. Run: .\NUnit.Console.2.6.4\tools\nunit-console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll
echo.
echo Option 3: Using VSTest Console
echo   vstest.console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll
echo.
echo ============================================

pause

