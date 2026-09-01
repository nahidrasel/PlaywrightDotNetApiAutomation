using Microsoft.Playwright;

namespace PlaywrightDotNetApiAutomation.Api;

public sealed record CreateUserRequest
{
    public string Name { get; init; } = string.Empty;

    public string Job { get; init; } = string.Empty;
}