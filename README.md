# Playwright .NET API Automation

A C# API automation sample using Microsoft Playwright's API request support, NUnit, and FluentAssertions. The suite exercises REST endpoints and demonstrates reusable API clients, typed response models, test fixtures, and environment-based configuration.

## Prerequisites

- .NET SDK 10.0
- Git
- Network access to the configured API under test

## Run the tests

From the repository root:

```bash
dotnet restore
dotnet test
```

The GitHub Actions workflow runs the test suite on Ubuntu and Windows and publishes TRX test results. The workflow also has a scheduled run and can be started manually.

## Project structure

```text
Api/       API client and endpoint wrappers
Config/    app settings and typed configuration
Fixtures/  shared test setup
Helpers/   JSON and test-data helpers
Models/    request and response models
Tests/     NUnit API tests
```

## Configuration and credentials

Review `Config/appsettings.json` and `Config/AppSettings.cs` before running against a different environment. The CI workflow reads optional application credentials from GitHub Actions secrets named `APP_USERNAME` and `APP_PASSWORD`. Do not commit real credentials or tokens. Configure environment-specific values through your local environment or repository secrets.

## Notes

This is an example framework, not a production-ready test suite for a real service. Verify the configured endpoints and credentials for your environment. API tests require the target service to be reachable.
