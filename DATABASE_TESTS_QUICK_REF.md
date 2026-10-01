# Quick Reference: Database Integration Tests

## ✅ Setup Complete!

Your test project now includes **database-connected integration tests** that work with your real MTAOARS database.

## Test Count

**Total: 14 Tests**
- **Unit Tests:** 2 (no database required)
- **Integration Tests:** 12 (requires MTAOARS database)

## Quick Start

### 1. Verify Database Connection

Make sure your database is accessible:
- **Server:** localhost
- **Database:** MTAOARS
- **Auth:** Windows Integrated Security

### 2. Run Tests in Visual Studio

```
1. Rebuild solution (Ctrl+Shift+B)
2. Open Test Explorer (Ctrl+E, T)
3. Click "Run All"
4. Watch tests execute!
```

### 3. Expected Results

✅ **Unit Tests (2):** Should always pass  
✅ **Integration Tests (12):** Should pass if database is accessible  

## Test Categories

### Run Only Unit Tests (Fast, No Database)
```
Filter: Category=Unit
Time: < 1 second
```

### Run Only Integration Tests (Requires Database)
```
Filter: Category=Integration  
Time: 5-10 seconds
```

## Connection String Location

File: `MTAoars.Tests\App.config`

```xml
<connectionStrings>
  <add name="EntityContext" 
       connectionString="...data source=localhost;initial catalog=MTAOARS..." />
</connectionStrings>
```

## If Tests Fail

### Check 1: Is SQL Server Running?
```powershell
# Check if SQL Server is running
Get-Service MSSQLSERVER
```

### Check 2: Does Database Exist?
Open SQL Server Management Studio and verify `MTAOARS` database exists.

### Check 3: Update Connection String
If your SQL Server is on a different server/instance, update `App.config`:
```xml
<!-- For SQL Express -->
data source=.\SQLEXPRESS;initial catalog=MTAOARS...

<!-- For named instance -->
data source=localhost\INSTANCE;initial catalog=MTAOARS...

<!-- For SQL Authentication -->
...User ID=yourusername;Password=yourpassword...
```

## Test Data

Integration tests create temporary test data:
- IC Numbers start with "TEST" (e.g., "TEST12345678")
- Automatically cleaned up after each test
- No permanent changes to your database

## Key Features

✅ Real database operations  
✅ Automatic test data cleanup  
✅ LINQ query verification  
✅ Edge case testing  
✅ Error handling validation  

## Next Steps

1. **Run the tests** - See them work!
2. **Check results** - All should pass
3. **Use as template** - Add more repository tests

## Documentation

- `DATABASE_INTEGRATION_TESTS.md` - Full documentation
- `App.config` - Connection string configuration
- Test files - See inline comments for details

---

**Ready to run!** Open Test Explorer and execute your tests. 🚀

