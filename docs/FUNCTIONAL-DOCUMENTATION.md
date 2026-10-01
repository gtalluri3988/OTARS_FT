# MTAoars Family – Functional Documentation

## 1. Purpose and scope

MTAoars Family is a web-based administration system for managing insurance/takaful intermediary operations. It supports agency and member registration, agency maintenance, renewals, terminations, compliance and training processes, invoicing, referrals, conflicts, reporting, master-data administration, notifications, and operational uploads.

This document describes the behavior visible from the current source code and Razor views. It is intended for business users, product owners, testers, support staff, and operations teams.

The main application surface is `MTAoarsFamily.Web`. The solution also contains `MTAoarsFamily.Shell`, which exposes a smaller/older MVC surface and should be treated as an alternate host until deployment ownership is confirmed.

## 2. Users and access model

The application uses authenticated users associated with a company/takaful operator. A user identity contains:

- User and login identifiers, name, company, and company code.
- Assigned roles.
- All available menu definitions and the menus granted to the user.
- General/Family scope flags derived from the user’s company.
- An MTA/ISM indicator used for cross-company access rules.

Most operational controllers use the custom `MTAoarsFamilyAuthorize` filter. A request is redirected to Unauthorized when there is no current identity or when the requested URL is not present in the user’s permitted menu tree. Some actions add an additional company ownership check, especially agency search, termination, renewal, and administrative operations.

## 3. Main functional areas

### 3.1 Login and session

Users can log in, log out, view an unauthorized page, and request a CAPTCHA image. The login flow validates the submitted model, authenticates the credentials through the user service, loads the user identity, and stores it in scoped session data. The current code contains both a local forms-login flow and an OpenID Connect/SSO configuration path; the active path must be confirmed per deployment.

### 3.2 Home and dashboard

The Home area provides the landing page and print view. The home service supplies the dashboard model. Errors are routed to a common error page and are logged by the application.

### 3.3 Agency search and enquiry

Users can search for agencies/intermediaries and open details. Enquiry functions include general agency details, TBE examination information, and photo-upload enquiry. Results can include current or archived agency data, status, company, principal/member information, registration identifiers, examinations, and referral-related information.

Typical flow:

1. Enter search criteria such as agency number or principal/member information.
2. Submit the search.
3. Review a paged result set.
4. Open the agency detail or specialist enquiry view.

### 3.4 Registration

Registration is a multi-step process. Supported registration categories in the current web controller include:

- Individual.
- Partnership.
- Sole proprietorship.
- Corporate/private limited.
- Public limited.
- Cooperative.
- Government agency.

The flow includes starting a registration, selecting a category, entering the main agency/company information, entering related people and qualifications, uploading supporting files, checking/validating, confirming, submitting, handling conflicts, and completing the registration.

Depending on agency type, the process can include:

- Address.
- Principal/member details.
- Directors and shareholders.
- Partners.
- Spouse.
- Guarantor.
- Qualification and training details.
- Agency banker.
- Corporate nominee and additional corporate nominee.
- Photo and supporting documents.

The service layer creates the appropriate domain structure for each registration type. Validation is performed at model level and through business validators before changes are saved.

### 3.5 Agency maintenance

Maintenance allows authorized users to search for an existing agency and update selected information. Current maintenance actions include:

- Agency/company information.
- Address.
- Nominee.
- Guarantor.
- Partners.
- Board members, directors, and shareholders.
- Additional corporate nominee.
- Photo.
- Agency type.
- Administrative activity/audit information.

The controller uses AJAX responses for many save operations so validation errors can be returned without leaving the current page. Changes are sent through registration and administrative services, which apply business checks and persistence rules.

### 3.6 Renewal

Renewal supports listing renewal records, viewing details, editing renewal details, uploading renewal information, submitting records, approving records, accepting or rejecting details, and completing the process. Administrative users have additional header/detail approval functions.

The renewal lifecycle is status-driven. Background scheduler operations can generate renewable records, send reminders, and process renewal batches.

### 3.7 Termination, reinstatement, and recovery

Users can search for agents, inspect termination details, upload termination data, and process termination actions. The system also supports reinstatement and renewal/recovery flows for terminated records.

The termination service supports termination, reinstatement, renewal, and inserting records into renewal detail processing. The system checks company ownership for non-MTA users before allowing relevant actions.

### 3.8 Invoice and payment processing

The Invoice area supports:

- Invoice list and paging.
- Invoice detail.
- Save and pay operations.
- Approve/reject.
- Search.
- Regeneration.
- Export of invoice detail.

Invoices may also be generated in batch by the scheduler for the current period or for a specified year and month.

### 3.9 Compliance and training

The application includes the following compliance-oriented areas:

- CPD/training list, search, validation, and scheduled processing.
- CBC creation, listing, searching, submission, approval, and rejection.
- ALC listing, validation, and save operations.
- TBE examination and exemption enquiry/reporting.
- IBFIM and registration file uploads.

These processes use lookup values for courses, categories, training status, examinations, grades, exemptions, and related classifications.

### 3.10 Referrals

Referrals can be created, modified, listed, submitted, approved, displayed, and completed. A referral can have a header, detail records, and attachments. Administrative users can update referred records and view referral-related details.

Referral classification is driven by configurable lookup data such as reason, category, action taken, status, police-report indicator, and referred member. General/Family applicability is represented in the referral data model and company scope.

### 3.11 Conflicts

The Conflict area supports listing conflict records, viewing attachments, downloading attachments, closing conflicts, rejecting conflicts, and auto-closing eligible cases. Scheduled jobs send first and second reminders, default open cases, and auto-close cases based on configured durations.

### 3.12 Complaints

Users can list, search, create, edit, display, save, and validate complaints. Complaint validation is performed before persistence.

### 3.13 Internal mail and notifications

Internal mail supports inbox and outbox views, received/sent paging, reading mail, replying, and composing new mail. The notification service and variable-source classes support generated operational notifications for registration, renewal, termination, conflict, invoice, upload, and maintenance events.

### 3.14 Reports

The reporting area exposes operational, compliance, financial, and audit reports. Current report actions include invoice, invoice detail, ASCII exports, renewal, user login, family registration/renewal/active/terminated agents, CPD, bulk registration, summary, agents, ALC, auto termination, termination list/summary, consolidation, TBE results/examination/exemption, training, referrals, special agents, administrative audit trail, photo upload summary, and resigned active agents.

Many reports are backed by database views, stored-procedure result shapes, or report-specific repositories and may render through RDLC/report viewer components.

### 3.15 Setup and master data

Authorized administrators can maintain users, roles, menus, role-menu assignments, companies, agency types, activities, courses and categories, designations, qualifications, member levels, races, religions, states, guarantor types, TBE exemptions, referral reasons/categories/actions/members, and MTA awards.

Lookup endpoints provide dropdown values used throughout the UI. Changes to master data affect validation, filtering, workflow choices, and report labels.

## 4. General and Family scope

General/Family behavior is controlled by the company associated with the logged-in identity. The codebase uses:

- Company `IsGeneral` and `IsFamily` flags.
- General/Family flags on referral-related records.
- `IntermediaryTypeID` values on principal/history-related records.
- Lookup intermediary codes `GEN` and `FAM`.

Operational screens and searches should only expose data allowed by the user’s company and scope. The exact screen-by-screen matrix should be confirmed with business owners because the repository contains an implementation plan as well as live filtering logic.

## 5. Common workflow conventions

- List screens are commonly paged and may return JSON for grid controls.
- Create/edit screens use server-side model binding and FluentValidation rules.
- Save and validate operations often return a common AJAX response containing success state and field-level errors.
- Statuses are represented by lookup entities rather than hard-coded display text.
- Attachments and photos are handled through configured upload directories and download actions.
- Created/modified audit information is automatically populated when entities are saved with a current identity.

## 6. Business rules and limitations observed

- Access depends on the current identity and menu permissions; users should not rely on URL hiding alone.
- Non-MTA users are generally restricted to their own company’s records.
- Registration and maintenance behavior varies by agency type.
- Several older documents in the repository describe test setup rather than production behavior; they should not be treated as current acceptance criteria.
- The repository contains both old and newer host projects and mixed framework/package versions. Deployment teams must confirm which host is released.

