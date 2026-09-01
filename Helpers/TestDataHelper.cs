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
}