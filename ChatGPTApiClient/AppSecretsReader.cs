using Microsoft.Extensions.Configuration;

namespace ChatGPTApiClient
{
    public static class AppSecretsReader
    {
        public static string? ReadSection(string sectionName)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Program>();

            var configurationRoot = builder.Build();

            return configurationRoot.GetSection(sectionName).Value;
        }
    }
}
