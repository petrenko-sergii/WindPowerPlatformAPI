using Subcriber.Dtos;
using System.Net.Http.Json;



Console.WriteLine("Press ESC to stop");
do
{
	HttpClient client = new HttpClient();
    Console.WriteLine("Listening...");

	while (!Console.KeyAvailable)
	{
		List<int> confirmedIds = await GetMessagesAsync(client);

		Thread.Sleep(2000);

		if(confirmedIds.Count > 0)
		{
			await ConfirmMessagesAsync(client, confirmedIds);
		}
	}

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

static async Task<List<int>> GetMessagesAsync(HttpClient httpClient)
{
    List<int> confirmedIds = new List<int>();

    List<MessageReadDto>? newMessages = new List<MessageReadDto>();

	try
	{
		int subscriptionId = 1;

		newMessages = await httpClient.GetFromJsonAsync<List<MessageReadDto>>($"https://localhost:7100/api/subscriptions/{subscriptionId}/messages");
	}
	catch
	{
		return confirmedIds;
	}

	if( newMessages != null )
	{
        foreach (var msg in newMessages)
        {
            Console.Out.WriteLine($"{msg.Id} - {msg.TopicMessage} - {msg.MessageStatus}");
            confirmedIds.Add(msg.Id);
        }
    }

	return confirmedIds;
}

static async Task ConfirmMessagesAsync(HttpClient httpClient, List<int> confirmedIds)
{
    int subscriptionId = 1;

    var response = await httpClient.PostAsJsonAsync($"https://localhost:7100/api/subscriptions/{subscriptionId}/messages", confirmedIds);

	var returnMessage = await response.Content.ReadAsStringAsync();

    Console.WriteLine(returnMessage);
}