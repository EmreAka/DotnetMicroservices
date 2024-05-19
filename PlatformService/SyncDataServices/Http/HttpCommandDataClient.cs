using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration config;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        => (this.httpClient, this.config) = (httpClient, config);

    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        var baseUrl = config["CommandService"];
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/c/platforms", platform);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to Command Service was OK");
        }
        else
        {
            Console.WriteLine("--> Sync POST to Command Service was NOT OK");
        }
    }
}
