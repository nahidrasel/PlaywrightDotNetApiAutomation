# Playwright .NET API Automation

A C# API automation sample using Microsoft Playwright's API request support, NUnit, and FluentAssertions. The suite exercises REST endpoints and demonstrates reusable API clients, typed response models, test fixtures, and configuration.

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

The GitHub Actions workflow runs the suite on Ubuntu and Windows, publishes TRX test results, and supports scheduled and manual runs.

## Project structure

```text
Api/       API client and endpoint wrappers
Config/    app settings and typed configuration
Fixtures/  shared test setup
Helpers/   JSON and test-data helpers
Models/    request and response models
Tests/     NUnit API tests
```

## Configuration

Review `Config/appsettings.json` and `Config/AppSettings.cs` before changing environments. The workflow maps GitHub Actions secrets to the environment variables `APP_Username` and `APP_Password`. For local runs, configure values using the same names if the app settings code reads environment variables. Never commit real credentials or tokens.

## Notes

This is an example framework, not a production-ready test suite for a real service. Verify the configured endpoints and credentials for your environment. API tests require the target service to be reachable.
