# PlaywrightDotNetApiAutomation

C# Playwright API automation framework for validating REST endpoints, checking status codes, parsing JSON responses, and running tests in CI.

## Project structure

- `Api/ApiClient.cs` - low-level HTTP wrapper around `IAPIRequestContext`
- `Api/UsersApi.cs` - endpoint-specific API methods
- `Config/AppSettings.cs` - environment and config-based settings loader
- `Config/appsettings.json` - default application config
- `Fixtures/BaseTest.cs` - shared test setup and teardown
- `Helpers/JsonHelper.cs` - JSON deserialization helper
- `Helpers/TestDataHelper.cs` - dynamic test data generation
- `Models/` - typed API request/response models
- `Tests/UserTests.cs` - API tests using NUnit + FluentAssertions

## Features

- Playwright-based API request context
- Layered architecture for API clients and tests
- Generic response models using `ApiResponse<T>`
- Config-based settings with environment override support
- GitHub Actions CI workflow with test reporting
- Parallel NUnit execution
- Scheduled and PR-triggered runs

## Local setup

1. Restore dependencies:

   ```bash
   dotnet restore
   ```

2. Run tests locally:

   ```bash
   dotnet test --nologo
   ```

## Configuration

The project reads default settings from `Config/appsettings.json`.

Example:

```json
{
  "ApiBaseUrl": "https://reqres.in",
  "Username": "",
  "Password": "",
  "TimeoutMs": 30000
}
```

For CI or secret-based auth, set GitHub Actions secrets such as:

- `APP_USERNAME`
- `APP_PASSWORD`

These values are loaded from environment variables when present.

## CI/CD

The workflow in `.github/workflows/dotnet-api-tests.yml` runs on:

- push to `main` and `development`
- pull requests to `main` and `development`
- nightly cron schedule
- manual workflow dispatch

It also publishes a test report using `dorny/test-reporter`.

## Example test flow

```csharp
var response = await _userApi.GetUser(2);
response.Status.Should().Be(200);

var json = await response.TextAsync();
var result = JsonHelper.Deserialize<ApiResponse<UserResponse>>(json);

result.Should().NotBeNull();
result!.Data.Should().NotBeNull();
result.Data!.Id.Should().Be(2);
```

## Notes

- The URL remains in repo config and is safe to keep in source control.
- Credentials should be stored as GitHub secrets instead of being committed.
- The suite uses NUnit parallel execution to speed up local and CI execution where tests are independent.
