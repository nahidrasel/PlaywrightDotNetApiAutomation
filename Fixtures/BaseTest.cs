using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightDotNetApiAutomation.Config;

namespace PlaywrightDotNetApiAutomation.Fixtures;

public abstract class BaseTest
{
    protected IPlaywright Playwright = null!;
    protected IAPIRequestContext ApiContext = null!;

    [SetUp]
    public async Task Setup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        var headers = new Dictionary<string, string>
        {
            ["Accept"] = "application/json"
        };

        if (!string.IsNullOrWhiteSpace(AppSettings.Username) && !string.IsNullOrWhiteSpace(AppSettings.Password))
        {
            var basicAuth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{AppSettings.Username}:{AppSettings.Password}"));
            headers["Authorization"] = $"Basic {basicAuth}";
        }

        ApiContext = await Playwright.APIRequest.NewContextAsync(
            new APIRequestNewContextOptions
            {
                BaseURL = AppSettings.ApiBaseUrl,
                ExtraHTTPHeaders = headers,
                Timeout = AppSettings.TimeoutMs
            });
    }

    [TearDown]
    public async Task Teardown()
    {
        await ApiContext.DisposeAsync();
        Playwright.Dispose();
    }
}