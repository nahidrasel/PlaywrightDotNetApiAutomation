using System.Text.Json.Nodes;
using FluentAssertions;
using PlaywrightDotNetApiAutomation.Api;
using PlaywrightDotNetApiAutomation.Fixtures;

namespace PlaywrightDotNetApiAutomation.Tests;

[TestFixture]
public class UsersTests : BaseTest
{
    private UsersApi _userApi = null!;
    [SetUp]
    public void CreateUserApi()
    {
        _userApi = new UsersApi(ApiContext);
    }

    [Test]
    public async Task GetUser_ShouldReturn200()
    {
        var userNumber = 2;
        var response = await _userApi.GetUser(userNumber);

        response.Status.Should().Be(200, "Test Passed ");
        var body = await response.JsonAsync<JsonNode>();
        body.Should().NotBeNull();
        var userId = body?["data"]?["id"]?.GetValue<int>();
        userId.Should().Be(userNumber);
    }
    [Test]
    public async Task GetUser_WithInvalidId_ShouldReturn404()
    {
        var response = await ApiContext.GetAsync("/api/users/99999");
        response.Status.Should().Be(404, "Test Should Return 404");
    }

    [Test]
    public async Task CreateUser_ShouldReturn201()
    {
        var request = new CreateUserRequest
        {
            Name = "Nahid",
            Job = "QA Automation Engineer"
        };

        var response = await _userApi.CreateUser(request);

        response.Status.Should().Be(201);

        var body = await response.JsonAsync<JsonNode>();

        body.Should().NotBeNull();

        body?["name"]?.GetValue<string>()
            .Should().Be(request.Name);

        body?["job"]?.GetValue<string>()
            .Should().Be(request.Job);
    }
}