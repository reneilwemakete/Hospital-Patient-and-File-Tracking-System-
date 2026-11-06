# Hospital Patient and File Tracking System

C# console application for educational demonstration of patient records and role-based access. **Not for use with real patient information.**

## Features

Registered users can log in and have read-only access to patient records and reports. An administrator with credentials configured through environment variables may also add, edit and delete records. Passwords are salted and hashed using PBKDF2. Sample patient records are fictional.

## Requirements and run

Install the .NET 8 SDK.

```sh
dotnet run --project PRG281Assignment.csproj
```

To enable administrative access on macOS or Linux:

```sh
export HOSPITAL_ADMIN_USER="admin"
export HOSPITAL_ADMIN_PASSWORD="use-a-long-unique-password"
dotnet run --project PRG281Assignment.csproj
```

Windows PowerShell:

```powershell
$env:HOSPITAL_ADMIN_USER="admin"
$env:HOSPITAL_ADMIN_PASSWORD="use-a-long-unique-password"
dotnet run --project PRG281Assignment.csproj
```

Passwords must be at least 12 characters. **Never commit credentials.** Public registration always creates read-only accounts. There are no default administrator credentials.

## Regression checks

```sh
dotnet run --project tests/Hospital.Tests.csproj
```

## Limitations

This is a console-only learning project, not a deployed healthcare information system. Patient records and accounts exist in memory and disappear when the process exits. There is no SQL database, web interface, persistence, encryption at rest, audit trail, session management or compliance certification. Do not enter identifiable patient data. It does not claim POPIA compliance.

## Technology

C#, .NET 8, role-based menus, in-memory collections and PBKDF2 password hashing.
