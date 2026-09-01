using Microsoft.Playwright;
using PlaywrightDotNetApiAutomation.Models;

namespace PlaywrightDotNetApiAutomation.Api;

public class UsersApi
{
    private readonly ApiClient _client;

    public UsersApi(ApiClient client)
    {
        _client = client;
    }

    public async Task<IAPIResponse> GetUser(int id)
    {
        return await _client.GetAsync($"/api/users/{id}");
    }

    public async Task<IAPIResponse> GetUsersPage(int page)
    {
        return await _client.GetAsync("/api/users", new Dictionary<string, string>
        {
            ["page"] = page.ToString()
        });
    }

    public async Task<IAPIResponse> CreateUser(CreateUserRequest request)
    {
        return await _client.PostAsync("/api/users", request);
    }

    public async Task<IAPIResponse> UpdateUser(int id, CreateUserRequest request)
    {
        return await _client.PutAsync($"/api/users/{id}", request);
    }

    public async Task<IAPIResponse> DeleteUser(int id)
    {
        return await _client.DeleteAsync($"/api/users/{id}");
    }
}