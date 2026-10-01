# Fixing Test Discovery in Visual Studio Test Explorer

## Problem
Visual Studio 2017 Test Explorer shows:
```
Discover test finished: 0 found
```

## Root Cause
Visual Studio Test Explorer requires a **Test Adapter** to discover and run tests from different testing frameworks like NUnit. Without the NUnit Test Adapter, VS cannot find NUnit tests.

## Solution

### Option 1: Install NUnit Test Adapter via NuGet (Recommended)

I've already added it to your `packages.config`. Now you need to restore the package:

**Step 1: Restore NuGet Packages**
```
Right-click on Solution → Restore NuGet Packages
```

Or in Package Manager Console:
```powershell
Update-Package -reinstall -ProjectName MTAoars.Tests
```

**Step 2: Rebuild the Solution**
```
Build → Rebuild Solution (or Ctrl+Shift+B)
```

**Step 3: Restart Visual Studio** (Important!)
- Close Visual Studio 2017
- Reopen the solution
- Open Test Explorer (Test → Windows → Test Explorer)

**Step 4: Discover Tests**
- Test Explorer should automatically discover tests
- You should now see 16 tests listed

### Option 2: Install NUnit Test Adapter Extension

If Option 1 doesn't work, install the Visual Studio extension:

**Step 1: Install Extension**
1. In Visual Studio, go to: `Tools → Extensions and Updates`
2. Search for "NUnit Test Adapter"
3. Install "NUnit 3 Test Adapter" or "NUnit Test Adapter"
4. Restart Visual Studio

**Step 2: Rebuild and Discover**
1. Rebuild the solution
2. Open Test Explorer
3. Tests should now appear

### Option 3: Using NUnit Console Runner

If Visual Studio Test Explorer still doesn't work:

**Step 1: Install NUnit Console Runner**
```powershell
cd D:\SrikanthPilla\ExWork\MTAoars
nuget install NUnit.Runners -Version 2.6.4
```

**Step 2: Run Tests**
```powershell
.\NUnit.Runners.2.6.4\tools\nunit-console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll
```

## Verification Steps

After applying the solution:

1. **Check Test Explorer**
   - Open: Test → Windows → Test Explorer (Ctrl+E, T)
   - You should see a list like:
     ```
     ✓ MTAoars.Tests
       ✓ MTAoars.Tests.Repositories.Maintenance
         ✓ ReferredRepositoryTests
           - Constructor_WithValidContext_ShouldCreateInstance
           - Constructor_WithNullContext_ShouldThrowArgumentNullException
           - GetDetail_WithExistingICNumber_ShouldReturnReferredDetail
           ... (13 more tests)
     ```

2. **Run All Tests**
   - Click "Run All" in Test Explorer
   - All 16 tests should pass (green checkmarks)

3. **Expected Output**
   ```
   Test Run Successful.
   Total tests: 16
        Passed: 16
        Failed: 0
     Total Time: < 1 second
   ```

## Troubleshooting

### Tests Still Not Showing?

**1. Check Build Output**
```
Build → Build Solution
Check Output window for errors
```

**2. Clean and Rebuild**
```powershell
# Clean
Build → Clean Solution

# Rebuild
Build → Rebuild Solution
```

**3. Check Test Assembly**
Verify the DLL exists:
```
D:\SrikanthPilla\ExWork\MTAoars\MTAoars.Tests\bin\Debug\MTAoars.Tests.dll
```

**4. Check NUnit Version**
Make sure you're using NUnit 2.6.4 (which we are).

**5. Restart Visual Studio**
Sometimes VS needs a full restart to recognize new test adapters.

**6. Update Test Adapter**
If using the NuGet package approach, try updating:
```powershell
Update-Package NUnitTestAdapter -ProjectName MTAoars.Tests
```

### Alternative: Use Visual Studio 2019 or Later

Visual Studio 2019+ has better test discovery. If possible, consider upgrading.

### Command-Line Alternative

You can always run tests via command line:
```powershell
# Using dotnet (if available)
dotnet test MTAoars.Tests\MTAoars.Tests.csproj

# Using NUnit Console
.\NUnit.Runners.2.6.4\tools\nunit-console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll
```

## What I've Changed

Added to `packages.config`:
```xml
<package id="NUnitTestAdapter" version="2.0.0" targetFramework="net40" />
```

This package enables Visual Studio Test Explorer to discover and run NUnit 2.x tests.

## Next Steps

1. **Restore NuGet packages** (right-click solution → Restore NuGet Packages)
2. **Rebuild solution** (Ctrl+Shift+B)
3. **Restart Visual Studio**
4. **Open Test Explorer** (Ctrl+E, T)
5. **Run All Tests** - you should see all 16 tests!

## Additional Resources

- [NUnit Test Adapter Documentation](https://github.com/nunit/docs/wiki/Visual-Studio-Test-Adapter)
- [Visual Studio Test Explorer Guide](https://docs.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer)

---

**Status**: Solution Applied ✓
**Required**: Restore NuGet Packages + Restart VS
**Expected**: 16 tests discovered and ready to run

