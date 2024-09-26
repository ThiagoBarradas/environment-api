using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Flurl.Http;
using Newtonsoft.Json;
using EnvironmentApi.Models.Response;

namespace EnvironmentApi.IntegrationTests
{
    public class EnvironmentControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public EnvironmentControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Startup>(); // Adjust as necessary
            _client = webAppFactory.CreateClient();
        }

        [Theory]
        [InlineData("ASPNETCORE_ENVIRONMENT|TESTXX")]
        public async Task GetEnv_ReturnsEnvironmentVariables(string envVariables)
        {
            // Arrange
            var expectedResponse = new ApiResponse
            {
                Content = new Dictionary<string, string>
                {
                    { "ASPNETCORE_ENVIRONMENT", "Development" },
                    { "TESTXX", "Some Value XX" }
                },
                StatusCode = HttpStatusCode.OK
            };

            // Act
            var response = await _client.GetAsync($"/environment/{envVariables}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponse.Content.Should().BeEquivalentTo(expectedResponse.Content);
        }

        [Theory]
        [InlineData("NON_EXISTENT_ENV_VAR")]
        public async Task GetEnv_ReturnsNotFound_WhenVariableDoesNotExist(string envVariable)
        {
            // Act
            var response = await _client.GetAsync($"/environment/{envVariable}");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actualResponse.Content[envVariable].Should().Be("[not found]");
        }

        [Fact]
        public async Task Home_ReturnsApiName()
        {
            // Act
            var response = await _client.GetAsync("/");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().Contain("Environment API - ");
        }
    }
}
