# MTAoars Family – Technical Documentation

## 1. System overview

MTAoars Family is a classic ASP.NET MVC application on the .NET Framework. The primary web project is `MTAoarsFamily.Web`; the solution also contains `MTAoarsFamily.Shell`, an alternate MVC host with a reduced controller surface.

The solution uses a layered architecture:

`MVC Controllers → Services → Builders/Mappers/Validators → Repositories/Unit of Work → Entity Framework ObjectContext → SQL Server`

Views use Razor, JavaScript, jQuery, Kendo UI widgets, and AJAX/JSON endpoints. Dependency injection is provided by Unity.

## 2. Solution projects

| Project | Responsibility |
|---|---|
| `MTAoarsFamily.Web` | Primary ASP.NET MVC web host, controllers, views, startup, reports, static content, uploads. Targets .NET Framework 4.6.1 in the project file; configuration also contains 4.7.1-era settings. |
| `MTAoarsFamily.Shell` | Alternate/legacy MVC host with a smaller controller set. Targets .NET Framework 4.6.1. |
| `MTAoarsFamily.DomainModels` | Entity Framework generated entities, `EntityContext`, entity extensions, lookup entities, complex report result types. |
| `MTAoarsFamily.Repositories` | Repository implementations, database queries, report repositories, and unit-of-work implementations. |
| `MTAoarsFamily.Services` | Application/business services for accounts, agency operations, registration, renewals, termination, invoices, conflicts, compliance, mail, notifications, reports, and lookups. |
| `MTAoarsFamily.Builders` | Builds view models and registration-specific object graphs. Includes registration creators for corporate, individual, partnership, and sole-proprietorship flows. |
| `MTAoarsFamily.Mappers` | AutoMapper profiles, entity/view-model transformations, conditional resolvers, and mapping actions. |
| `MTAoarsFamily.ViewModels` | MVC request/response models and notification variable sources. |
| `MTAoarsFamily.Validators` | FluentValidation and custom validation providers for registrations, maintenance, compliance, reports, accounts, and master data. |
| `MTAoarsFamily.Utilities` | IoC, MVC binders, authorization, identity/session scope, encryption, Excel handling, CSV, constants, response helpers, and shared extensions. |
| `MTAoarsFamily.Schedulers` | Console batch process for invoice, renewal, CPD, CBC, and conflict jobs. |
| `MTAoarsFamily.Tests` | NUnit-based repository tests and Entity Framework test helpers. |

## 3. Runtime request flow

1. IIS/ASP.NET loads `MTAoarsFamily.Web`.
2. `Application_Start` registers routes, global filters, Unity registrations, MVC model binders, validators, and the Razor view engine.
3. The default route is `{controller}/{action}/{id}` with `Home/Index` as the default.
4. A controller receives a request and dependencies are resolved by Unity.
5. The custom authorization filter checks current identity and menu permissions.
6. MVC model binders convert dates, decimals, long values, JSON, Excel, and custom form data into view models.
7. FluentValidation and service-level business checks populate model/context validation messages.
8. Services invoke builders, mappers, repositories, and unit-of-work implementations.
9. The Entity Framework context persists changes and applies audit fields.
10. The controller returns a Razor view, partial view, JSON response, file, redirect, or common error response.

## 4. Dependency injection and composition root

`MTAoarsFamily.Utilities.IoC.ObjectContainer` holds the Unity container. `ServiceLoader.Load()` initializes service/repository registrations. `MTAoarsFamily.Web.Global.asax.cs` registers controllers and MVC infrastructure, then installs `UnityContainerResolver` as the MVC dependency resolver.

The application also replaces the default MVC filter provider with `UnityContainerFilterAttributeFilterProvider`, allowing filter attributes and related dependencies to be resolved through Unity.

## 5. Persistence

The domain model is generated from an Entity Framework model and uses `EntityContext : ObjectContext`. The current web configuration points to SQL Server database `MTAOARS` using the `EntityContext` EntityClient connection string. The exact server and authentication must be supplied per environment; connection values should not be committed with credentials.

The model includes core entities such as `Agency`, `AgencyPrincipal`, `AgencyMember`, `Member`, `Company`, `User`, `Invoice`, `RenewalHeader`, `RenewalDetail`, `TerminationSchedule`, `CPDHeader`, `CPDDetail`, `CBCHeader`, `CBCDetail`, `ALCHeader`, `ALCMember`, referrals, conflicts, mail, notifications, upload history, and many lookup tables. It also includes database-backed report result types and views.

Repositories follow generic repository and specialized repository patterns. Unit-of-work classes group changes for registration, agency, and administrative operations.

### Audit fields

The partial `EntityContext.SaveChanges` override inspects added and modified `IEntity` instances. When a current identity is present, it writes:

- Added entities: `CreatedBy`, `CreatedDate`.
- Modified entities: `ModifiedBy`, `ModifiedDate`.

Batch scheduler execution registers a scheduler identity before invoking services so these fields can still be populated.

## 6. Validation and model binding

Validation combines MVC model state, FluentValidation providers, and service/context validation messages. Validators cover registration categories, agency details, board members, partners, guarantors, corporate nominees, address, training, complaints, invoices, referrals, users, roles, and setup data.

Custom binders include:

- Default/custom model binder.
- Date and nullable-date binder.
- Decimal and nullable-decimal binder.
- Long and nullable-long binder.
- JSON binder.
- Excel binder/list support.

The configured display date formats are `dd/MM/yyyy` and `dd/MM/yyyy hh:mmtt`.

## 7. Authorization, identity, and authentication

`MTAoarsFamilyAuthorizeAttribute` uses `IScopeDataProvider` to retrieve `GlobalConstants.CurrentIdentity`. It rejects requests without an identity and checks the requested controller/action URL against the user’s permitted menu tree. If the URL is not defined in all menus, it is treated as permitted; if it is defined, it must also exist in the assigned menu tree.

The primary web configuration enables Forms Authentication with login URL `~/Account/LogOn`. The same web project contains OWIN OpenID Connect configuration in `Startup.cs`, including cookie authentication, token validation, an issuer signing certificate, authority, redirect URI, and SSO base URI. The active authentication mode must be verified in the deployed environment because the configuration contains both patterns.

The account controller also exposes local login/logout behavior and stores the loaded identity in scoped data. Identity data is not a replacement for database authorization; menu and company checks remain part of request handling.

## 8. General/Family data partitioning

The implementation represents General/Family scope through multiple mechanisms:

- `Company.IsGeneral` and `Company.IsFamily` determine company capability and are copied into identity-related behavior.
- `ReferredDetail` and `ReferredMember` contain General/Family flags.
- Principal and history records use `IntermediaryTypeID` linked to `LookupIntermediaryType`.
- Lookup codes include `GEN` and `FAM`.

When changing this behavior, review company filtering in repositories, service checks, builders, report repositories, and lookup population together. A database flag alone is not sufficient to change effective access.

## 9. Background scheduler

`MTAoarsFamily.Schedulers` is a console application. It loads services, registers `SchedulerDataProvider`, sets a service identity, and dispatches on the first command-line argument.

Supported scheduler commands are defined by `LookupConstants.Schedulers` and include:

- Generate invoice, optionally for a specified year and month.
- Generate renewable records.
- Send renewal reminders.
- Process renewals.
- Process CPD.
- Process CBC.
- Send first/second conflict reminders.
- Default open conflict cases.
- Auto-close conflict cases.

Conflict timing is configured in scheduler/web settings with durations for first reminder, second reminder, third reminder, and auto-close. The scheduler catches top-level exceptions and writes them through NLog.

## 10. Files, reports, and external concerns

The web project contains upload handling for registrations, IBFIM, photos, and upload history. `UploadBaseDirectory` and `PhotoUrl` are environment-specific and must be changed for deployment. Report viewer/RDLC support is configured in the web project for report rendering.

NLog writes console output and file logs, with daily archival and a configured maximum number of archive files. Application-level errors are logged in `Application_Error`.

## 11. Security and deployment observations

The following items require deployment hardening or explicit environment confirmation:

- Do not commit or reuse database, SSO, certificate, or other secrets from configuration files.
- Rotate any credentials or private connection information previously present in historical configuration comments.
- Use environment transforms/secret storage for connection strings, upload paths, SSO authority, redirect URI, and signing material.
- Confirm TLS certificates and issuer validation before enabling SSO outside localhost.
- Set `compilation debug="false"` in production.
- Verify upload extension, size, content-type, filename, and path traversal controls.
- Verify anti-forgery protection on all state-changing MVC actions, especially AJAX POST actions.
- Review the custom authorization behavior for undefined menu URLs; the current implementation treats URLs absent from `AllMenus` as permitted.
- Confirm session timeout, cookie security flags, and logout invalidation in the deployed authentication mode.
- Avoid exposing detailed exception messages or sensitive data through logs and report exports.

## 12. Build and test

The solution targets a mix of .NET Framework 4.0 libraries and .NET Framework 4.6.1 web hosts. Dependencies are primarily managed with `packages.config`; notable packages include Entity Framework 5, MVC 3-era assemblies, Unity 2.1, AutoMapper 2.2, FluentValidation 3.4, EPPlus 4.5, NLog, and Kendo UI assets.

The test project uses NUnit and repository-focused test helpers. Existing tests cover `ReferredRepository` with mocked Entity Framework objects and separate database integration tests. The repository notes report a known null-handling issue in `GetDetailByCompany` when the referred header is missing; this should be fixed or explicitly accepted before production use.

Recommended verification sequence:

1. Restore packages using the solution’s package configuration.
2. Build the solution with a compatible Visual Studio/MSBuild toolchain.
3. Run unit tests without a database.
4. Run integration tests only against a controlled test database.
5. Validate the deployed connection string, upload directories, report viewer, SMTP/notification settings, and SSO settings.
6. Exercise login, authorization, registration, renewal, invoice, upload, report, and scheduled-job smoke tests.

## 13. Known repository inconsistencies

- `MTAoarsFamily.Web` and `MTAoarsFamily.Shell` coexist and have different feature surfaces.
- Project files and packages show mixed target frameworks and historical MVC/Entity Framework versions.
- Existing test documentation has inconsistent test counts and package versions; use the actual test project files and current build output as the source of truth.
- Several configuration files contain empty or environment-specific connection-string sections; deployment configuration is therefore a required release artifact.

