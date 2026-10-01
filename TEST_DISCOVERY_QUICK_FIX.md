# Quick Fix: Test Discovery in VS 2017

## Problem
```
Discover test finished: 0 found (0:00:00.4354868)
```

## ⚡ Quick Solution

### Method 1: Restore Packages (Fastest)

1. **In Visual Studio 2017:**
   - Right-click on the **Solution** → **Restore NuGet Packages**
   - **Build** → **Rebuild Solution** (Ctrl+Shift+B)
   - **Close and Restart Visual Studio**
   - Open **Test Explorer** (Ctrl+E, T)
   - Tests should now appear!

### Method 2: Run the Install Script

```powershell
# From the MTAoars root directory
.\install-test-adapter.ps1
```

Then restart Visual Studio.

### Method 3: Install Manually via Package Manager Console

1. In Visual Studio, open **Package Manager Console**
   - Tools → NuGet Package Manager → Package Manager Console

2. Run:
   ```powershell
   Install-Package NUnitTestAdapter -Version 2.0.0 -Project MTAoars.Tests
   ```

3. Rebuild solution and restart VS

## What Was Added

Added **NUnit Test Adapter** to `packages.config`:
```xml
<package id="NUnitTestAdapter" version="2.0.0" targetFramework="net40" />
```

This adapter enables Visual Studio Test Explorer to discover NUnit 2.x tests.

## ✅ Expected Result

After fixing, you should see in Test Explorer:

```
MTAoars.Tests (16 tests)
  ├─ ReferredRepositoryTests (16 tests)
     ├─ Constructor_WithValidContext_ShouldCreateInstance ✓
     ├─ Constructor_WithNullContext_ShouldThrowArgumentNullException ✓
     ├─ GetDetail_WithExistingICNumber_ShouldReturnReferredDetail ✓
     ├─ GetDetail_WithNonExistingICNumber_ShouldReturnNull ✓
     └─ ... (12 more tests)
```

## Alternative: Use NUnit Console Runner

If VS Test Explorer still doesn't work:

```powershell
# Install NUnit Console Runner
nuget install NUnit.Runners -Version 2.6.4

# Run tests
.\NUnit.Runners.2.6.4\tools\nunit-console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll
```

## Troubleshooting

**Still not working?**
1. Clean solution: **Build → Clean Solution**
2. Delete `bin` and `obj` folders in MTAoars.Tests
3. Rebuild: **Build → Rebuild Solution**
4. Restart Visual Studio
5. Check Test Explorer again

**Need more help?** See `TEST_DISCOVERY_FIX.md` for detailed troubleshooting.

---
**Files Modified:** `packages.config`, `install-test-adapter.ps1` (new), `TEST_DISCOVERY_FIX.md` (new)

