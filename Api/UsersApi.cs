using Microsoft.Playwright;

namespace PlaywrightDotNetApiAutomation.Api;

public class UsersApi
{
    private readonly IAPIRequestContext _apiContext;

    public UsersApi(IAPIRequestContext apiContext)
    {
        _apiContext = apiContext;
    }

    public async Task<IAPIResponse> GetUser(int id)
    {
        return await _apiContext.GetAsync($"/api/users/{id}");
    }

    public async Task<IAPIResponse> CreateUser(object requestBody)
    {
        return await _apiContext.PostAsync("/api/users", new APIRequestContextOptions
        {
            DataObject = requestBody
        });
    }
}