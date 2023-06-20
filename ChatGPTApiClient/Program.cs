using Newtonsoft.Json;
using System.Text;
using ChatGPTApiClient;

if (args.Length > 0)
{
    var apiKey = AppSecretsReader.ReadSection("OpenAI_ChatGPTApiClientKey");

    //todo:just for test, use IHttpClientFactory 
    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

    var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + args[0] + "\",\"temperature\": 1,\"max_tokens\": 100}",
       Encoding.UTF8, "application/json");

    HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);
    string responseStr = await response.Content.ReadAsStringAsync();

    try
    {
        //todo: just for test, use typed objects / not dynamic
        var dynamicData = JsonConvert.DeserializeObject<dynamic>(responseStr);
        var reply = (string)dynamicData!.choices[0].text;

        Console.ForegroundColor = ConsoleColor.Green;
        var cleanedReply = reply.Replace("\n", "").Replace("\r", "").Replace("?", "");

        Console.WriteLine($"--> API response: {cleanedReply}");
        TextCopy.ClipboardService.SetText(cleanedReply);
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine( $"--> Could nort deserialize the JSON: {ex.Message}");
        Console.ResetColor();
    }

    //Console.WriteLine(responseStr);
}
else
{
    Console.WriteLine("--> You need to provide some input");
}
