using PlaywrightDotNetApiAutomation.Models;

namespace PlaywrightDotNetApiAutomation.Helpers;

public static class TestDataHelper
{
    public static string UniqueName()
    {
        return $"TestUser-{Guid.NewGuid():N}";
    }

    public static string UniqueEmail()
    {
        return $"test-{Guid.NewGuid():N}@demo.com";
    }

    public static CreateUserRequest BuildCreateUserRequest(string? overrideName = null, string? overrideJob = null)
    {
        return new CreateUserRequest
        {
            Name = overrideName ?? UniqueName(),
            Job = overrideJob ?? "QA Automation Engineer"
        };
    }
}