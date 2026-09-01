using System.Text.Json;

namespace PlaywrightDotNetApiAutomation.Config;

public static class AppSettings
{
    private static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");

    private static readonly Lazy<Dictionary<string, string>> Settings = new(() =>
    {
        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        void AddValue(string? key, string? value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            values[CanonicalizeKey(key)] = value.Trim();
        }

        if (File.Exists(ConfigPath))
        {
            using var document = JsonDocument.Parse(File.ReadAllText(ConfigPath));
            foreach (var property in document.RootElement.EnumerateObject())
            {
                AddValue(property.Name, property.Value.ToString());
            }
        }

        foreach (var kvp in Environment.GetEnvironmentVariables().Cast<System.Collections.DictionaryEntry>())
        {
            AddValue(kvp.Key?.ToString(), kvp.Value?.ToString());
        }

        return values;
    });

    public static string ApiBaseUrl => GetValue("ApiBaseUrl", "https://reqres.in");
    public static string Username => GetValue("Username", string.Empty);
    public static string Password => GetValue("Password", string.Empty);
    public static int TimeoutMs => int.TryParse(GetValue("TimeoutMs", "30000"), out var value) ? value : 30000;

    private static string GetValue(string key, string defaultValue)
    {
        var canonicalKey = CanonicalizeKey(key);
        var settings = Settings.Value;

        if (settings.TryGetValue(canonicalKey, out var value) && !string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        return defaultValue;
    }

    private static string CanonicalizeKey(string key)
    {
        var normalized = key.Trim();

        if (normalized.StartsWith("APP_", StringComparison.OrdinalIgnoreCase))
        {
            normalized = normalized[4..];
        }

        normalized = new string(normalized.Where(char.IsLetterOrDigit).ToArray());

        return normalized.ToLowerInvariant();
    }
}
