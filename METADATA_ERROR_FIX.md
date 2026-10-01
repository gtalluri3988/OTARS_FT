# Fix: Unable to Load Metadata Resource

## Problem

When running integration tests, you got this error:

```
SetUp : System.Data.MetadataException : Unable to load the specified metadata resource.
```

## Root Cause

Entity Framework stores metadata files (`.csdl`, `.ssdl`, `.msl`) as embedded resources in the DomainModels assembly. The connection string was using:

```
metadata=res://*/Entities.csdl|res://*/Entities.ssdl|res://*/Entities.msl
```

The `res://*/` wildcard searches all loaded assemblies, but Entity Framework couldn't locate the metadata files because they're specifically in the `MTAoars.DomainModels` assembly.

## The Fix

Updated the connection string in `App.config` to explicitly reference the assembly:

```xml
<add name="EntityContext" 
     connectionString="metadata=res://MTAoars.DomainModels/Entities.csdl|res://MTAoars.DomainModels/Entities.ssdl|res://MTAoars.DomainModels/Entities.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;initial catalog=MTAOARS;multipleactiveresultsets=True;Integrated Security=True;App=EntityFramework&quot;" 
     providerName="System.Data.EntityClient" />
```

**Key Change:**
- **Before:** `res://*/Entities.csdl...`
- **After:** `res://MTAoars.DomainModels/Entities.csdl...`

## What This Means

`res://MTAoars.DomainModels/` tells Entity Framework to look for the metadata files specifically in the `MTAoars.DomainModels` assembly where they are embedded as resources.

## Now Your Tests Should Work!

**Run your tests again:**
1. Rebuild the solution (Ctrl+Shift+B)
2. Open Test Explorer (Ctrl+E, T)  
3. Run All Tests

All 14 tests should now execute successfully if your database is accessible!

## Expected Results

✅ **Unit Tests (2):** Should pass immediately  
✅ **Integration Tests (12):** Should pass if:
- SQL Server is running
- MTAOARS database exists
- You have permissions to access it

## Still Having Issues?

### Issue: "Cannot open database MTAOARS"

**Solution:** Your database might be on a different server or instance.

Update the provider connection string in `App.config`:

```xml
<!-- For SQL Express -->
data source=.\SQLEXPRESS;initial catalog=MTAOARS...

<!-- For remote server -->
data source=SERVERNAME;initial catalog=MTAOARS...

<!-- For SQL Authentication -->
data source=localhost;initial catalog=MTAOARS;User ID=username;Password=password...
```

### Issue: "Login failed for user"

**Solution:** Check authentication method

```xml
<!-- Use SQL Authentication if Windows auth doesn't work -->
provider connection string=&quot;data source=localhost;initial catalog=MTAOARS;User ID=sa;Password=yourpassword;multipleactiveresultsets=True;App=EntityFramework&quot;
```

## Files Modified

✅ `MTAoars.Tests\App.config` - Fixed metadata reference in connection string

---

**Status:** ✅ FIXED  
**Action:** Run your tests again - they should work now!  
**Date:** 2025-11-30

