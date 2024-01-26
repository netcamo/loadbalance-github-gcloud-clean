
namespace LoadbalanceGithubGcloudClean.Web.GCP;

public class MetadataService
{
    private readonly IHttpClientFactory _factory;


    public MetadataService(
        IHttpClientFactory factory)
    {
        _factory = factory;
    }


    public async Task<string> GetRegion()
    {
        var client = _factory.CreateClient();

        client.DefaultRequestHeaders.Add("Metadata-Flavor", "Google");
        client.BaseAddress = new Uri("http://metadata.google.internal/");


        var response = await client
            .GetAsync($"computeMetadata/v1/instance/region");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine((await response.Content.ReadAsStringAsync()).ToString());
            return content;
        }

        else
        {
            Console.WriteLine("Some error while getting data from metadata");
            return "Unknown";
        }
    }
}