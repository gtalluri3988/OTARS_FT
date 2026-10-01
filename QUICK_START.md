# Quick Start Guide - MTAoars Unit Tests

## ⚡ Quick Commands

### Build the Test Project
```powershell
# PowerShell (Recommended)
.\build-and-test.ps1

# Command Prompt
build-and-test.bat

# Or using dotnet
dotnet build MTAoars.Tests\MTAoars.Tests.csproj
```

### Run Tests in Visual Studio
1. Open `MTAoars.sln`
2. Press `Ctrl+R, A` (or Test > Run All Tests)
3. View results in Test Explorer (`Ctrl+E, T`)

### Run Tests from Command Line
```powershell
# Using NUnit Console (after installing)
.\NUnit.Console.2.6.4\tools\nunit-console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll

# Using VSTest
vstest.console.exe MTAoars.Tests\bin\Debug\MTAoars.Tests.dll
```

## 📁 Project Structure

```
MTAoars.Tests/
├── Repositories/Maintenance/
│   └── ReferredRepositoryTests.cs    ← 16 unit tests here
├── TestHelpers/
│   ├── FakeDbSet.cs                  ← Mocking helpers
│   └── TestObjectContext.cs
└── README.md                          ← Full documentation
```

## ✅ What's Included

- **16 Unit Tests** for ReferredRepository
  - 2 Constructor tests
  - 5 GetDetail tests  
  - 4 GetDetailByCompany tests
  - 3 Inherited method tests
  - 2 Integration tests

- **Test Helpers** for creating mocks
- **Build Scripts** for automation
- **Full Documentation** in README.md

## 🎯 Test Examples

### Running Specific Tests in Visual Studio
1. Open Test Explorer
2. Search for test name (e.g., "GetDetail")
3. Right-click test > Run Selected Tests

### Expected Output
```
Test Run Successful.
Total tests: 16
     Passed: 16
     Failed: 0
  Total Time: < 1 second
```

## 📝 Adding New Tests

1. Create new test class in appropriate folder
2. Add `[TestFixture]` attribute to class
3. Add `[Test]` attribute to test methods
4. Follow AAA pattern (Arrange, Act, Assert)

Example:
```csharp
[TestFixture]
public class MyRepositoryTests
{
    [Test]
    public void MyTest_Scenario_ExpectedResult()
    {
        // Arrange
        var repository = new MyRepository(mockContext.Object);
        
        // Act
        var result = repository.MyMethod();
        
        // Assert
        Assert.IsNotNull(result);
    }
}
```

## 🔧 Troubleshooting

| Issue | Solution |
|-------|----------|
| Tests not showing in Test Explorer | Clean + Rebuild Solution |
| NuGet packages missing | Right-click solution > Restore NuGet Packages |
| Build errors | Ensure .NET Framework 4.0 is installed |
| MSBuild not found | Use build-and-test.ps1 script |

## 📚 Documentation

- **README.md** - Full documentation
- **PROJECT_SUMMARY.md** - Complete project overview
- **This file** - Quick reference

## 🚀 Ready to Go!

The test project is fully configured and working. Just:
1. Open `MTAoars.sln` in Visual Studio
2. Build the solution
3. Run the tests

All 16 tests should pass! ✓

---
For detailed information, see README.md or PROJECT_SUMMARY.md

