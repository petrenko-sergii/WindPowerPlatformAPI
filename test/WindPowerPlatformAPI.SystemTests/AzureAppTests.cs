namespace WindPowerPlatformAPI.SystemTests
{
    public class AzureAppTests : IDisposable
    {
        private HttpClient _httpClient;
        private readonly string _azureAppBaseUrl = "https://windpowerplatformapi.azurewebsites.net/";

        public AzureAppTests()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        [Fact]
        public async void GetTurbines_AzureAppWorks_ShouldReturnTurbines()
        {
            // Arrange
            // just for testing -- new separate app instance should be created and after testing deleted (or every night)
            var azureAppUrlWithSegments = $"{_azureAppBaseUrl}api/turbines";
            var expectedTurbine = new TurbineDtoStub { Id = 1, SerialNumber = "SG 2.1-122", Price = 203000 };

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(azureAppUrlWithSegments);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);

            string jsonResult = await response.Content.ReadAsStringAsync();
            var turbines = JsonSerializer.Deserialize<List<TurbineDtoStub>>(jsonResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(turbines);
            Assert.True(turbines.Any());
            AssertTurbines(expectedTurbine, turbines.First());
        }

        [Fact]
        public async void GetCommands_AzureAppWorks_ShouldReturnCommands()
        {
            // Arrange
            // just for testing -- new separate app instance should be created and after testing deleted (or every night)
            var azureAppUrlWithSegments = $"{_azureAppBaseUrl}api/commands/";

            var expectedCommand = new CommandDtoStub
            {
                Id = 1,
                HowTo = "Cleans the output of a project",
                Platform = "Entity Framework Core Command Line",
                CommandLine = "dotnet clean"
            };

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(azureAppUrlWithSegments);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);

            string jsonResult = await response.Content.ReadAsStringAsync();
            var commands = JsonSerializer.Deserialize<List<CommandDtoStub>>(jsonResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(commands);
            Assert.True(commands.Any());
            AssertCommands(expectedCommand, commands.First());
        }

        private void AssertTurbines(TurbineDtoStub expectedTurbine, TurbineDtoStub turbine)
        {
            Assert.Equal(expectedTurbine.Id, turbine.Id);
            Assert.Equal(expectedTurbine.SerialNumber, turbine.SerialNumber);
            Assert.Equal(expectedTurbine.Price, turbine.Price);
        }

        private void AssertCommands(CommandDtoStub expectedCommand, CommandDtoStub command)
        {
            Assert.Equal(expectedCommand.Id, command.Id);
            Assert.Equal(expectedCommand.HowTo, command.HowTo);
            Assert.Equal(expectedCommand.Platform, command.Platform);
            Assert.Equal(expectedCommand.CommandLine, command.CommandLine);
        }
    }
}
