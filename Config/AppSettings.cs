using System.Text.Json;

namespace PlaywrightDotNetApiAutomation.Config;

public static class AppSettings
{
    private static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");

    private static readonly Lazy<Dictionary<string, string>> Settings = new(() =>
    {
        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (File.Exists(ConfigPath))
        {
            using var document = JsonDocument.Parse(File.ReadAllText(ConfigPath));
            foreach (var property in document.RootElement.EnumerateObject())
            {
                values[property.Name] = property.Value.ToString();
            }
        }

        foreach (var kvp in Environment.GetEnvironmentVariables().Cast<System.Collections.DictionaryEntry>())
        {
            var key = kvp.Key?.ToString();
            if (!string.IsNullOrWhiteSpace(key) && key.StartsWith("APP_", StringComparison.OrdinalIgnoreCase))
            {
                var value = kvp.Value?.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    values[key.Replace("APP_", string.Empty, StringComparison.OrdinalIgnoreCase)] = value;
                }
            }
        }

        return values;
    });

    public static string ApiBaseUrl => GetValue("ApiBaseUrl", "https://reqres.in");
    public static string Username => GetValue("Username", string.Empty);
    public static string Password => GetValue("Password", string.Empty);
    public static int TimeoutMs => int.TryParse(GetValue("TimeoutMs", "30000"), out var value) ? value : 30000;

    private static string GetValue(string key, string defaultValue)
    {
        var settings = Settings.Value;
        if (settings.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        return defaultValue;
    }
}
