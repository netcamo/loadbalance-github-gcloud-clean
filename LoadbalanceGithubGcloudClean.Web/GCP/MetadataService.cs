
namespace LoadbalanceGithubGcloudClean.Web.GCP;

public class MetadataService
{
    private readonly IHttpClientFactory _factory;
	private readonly ILogger<MetadataService> _logger;


	public MetadataService(
        IHttpClientFactory factory,
		ILogger<MetadataService> logger)
    {
        _factory = factory;
        _logger = logger;
    }


    public async Task<string> GetRegion()
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Subscriber")
        {

            try
            {
                var client = _factory.CreateClient();

                client.DefaultRequestHeaders.Add("Metadata-Flavor", "Google");
                client.BaseAddress = new Uri("http://metadata.google.internal/");


                var response = await client
                    .GetAsync($"computeMetadata/v1/instance/region");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    string[] parts = content.Split('/');
                    
                    // Get the last element of the array
                    string region = parts[^1];
                    return ($": Region {region}");
                }

                else
                {
                    _logger.LogError("Some error while getting data from metadata");
                    return "Unknown";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error while getting data from metadata");
            }

            return "ERROR";

        }

        return "";

    }
}