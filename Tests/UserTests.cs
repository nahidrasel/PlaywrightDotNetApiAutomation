using FluentAssertions;
using PlaywrightDotNetApiAutomation.Api;
using PlaywrightDotNetApiAutomation.Fixtures;
using PlaywrightDotNetApiAutomation.Helpers;
using PlaywrightDotNetApiAutomation.Models;

namespace PlaywrightDotNetApiAutomation.Tests;

[TestFixture]
public class UsersTests : BaseTest
{
    private ApiClient _client = null!;
    private UsersApi _userApi = null!;

    [SetUp]
    public void CreateApiClients()
    {
        _client = new ApiClient(ApiContext);
        _userApi = new UsersApi(_client);
    }

    [Test]
    public async Task GetUser_ShouldReturnCorrectUser()
    {
        var userNumber = 2;
        var response = await _userApi.GetUser(userNumber);

        response.Status.Should().Be(200, "ReqRes should return 200 for an existing user");

        var json = await response.TextAsync();
        var result = JsonHelper.Deserialize<ApiResponse<UserResponse>>(json);

        result.Should().NotBeNull();
        result!.Data.Should().NotBeNull();
        result.Data!.Id.Should().Be(userNumber);
        result.Data.Email.Should().NotBeNullOrWhiteSpace();
        result.Data.FirstName.Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public async Task GetUser_WithInvalidId_ShouldReturn404()
    {
        var response = await ApiContext.GetAsync("/api/users/99999");
        response.Status.Should().Be(404, "Invalid user id should return 404");
    }

    [Test]
    public async Task CreateUser_ShouldReturn201()
    {
        var request = TestDataHelper.BuildCreateUserRequest();
        var response = await _userApi.CreateUser(request);

        response.Status.Should().Be(201, "the API should create a new user successfully");

        var result = await ResponseAssertions.ReadJsonAsync<CreateUserResponse>(response);

        result.Should().NotBeNull();
        result!.Name.Should().Be(request.Name);
        result.Job.Should().Be(request.Job);
        result.Id.Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public async Task UpdateUser_ShouldReturn200()
    {
        var request = TestDataHelper.BuildCreateUserRequest(overrideJob: "Senior QA Engineer");
        var response = await _userApi.UpdateUser(2, request);

        response.Status.Should().Be(200, "the API should update the user successfully");

        var result = await ResponseAssertions.ReadJsonAsync<CreateUserResponse>(response);

        result.Should().NotBeNull();
        result!.Name.Should().Be(request.Name);
        result.Job.Should().Be(request.Job);
    }

    [Test]
    public async Task DeleteUser_ShouldReturn204()
    {
        var response = await _userApi.DeleteUser(2);

        response.Status.Should().Be(204, "the API should delete the user successfully");
    }
}

public static class ResponseAssertions
{
    public static async Task<T?> ReadJsonAsync<T>(IAPIResponse response)
    {
        var json = await response.TextAsync();
        return JsonHelper.Deserialize<T>(json);
    }
}
