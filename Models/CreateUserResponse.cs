using System.Text.Json.Serialization;

namespace PlaywrightDotNetApiAutomation.Models;

public sealed record CreateUserResponse
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Job { get; init; } = string.Empty;
    public string CreatedAt { get; init; } = string.Empty;
}
