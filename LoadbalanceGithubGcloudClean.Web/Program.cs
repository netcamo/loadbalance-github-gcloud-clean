using Hangfire;
using LoadbalanceGithubGcloudClean.Web.GCP;
using LoadBalanceGithubGcloudClean.Web.ServerRoleAccessors;
using LoadBalanceGithubGcloudClean.Web.TvMazeShow;
using Umbraco.Cms.Infrastructure.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


var umbracoBuilder = builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers();

builder.Services.AddScoped<MetadataService>();

if (builder.Environment.EnvironmentName.Equals("Subscriber"))
{
    umbracoBuilder.SetServerRegistrar<SubscriberServerRoleAccessor>()
        .AddAzureBlobMediaFileSystem()
        .AddAzureBlobImageSharpCache();
}
else if (builder.Environment.IsProduction())
{
    umbracoBuilder.SetServerRegistrar<SchedulingPublisherServerRoleAccessor>();
    RecurringJob.AddOrUpdate<TvMazeUtility>("MoveTvShowsFromTvMazeToUmbraco", x => x.MoveTvShowsFromTvMazeToUmbraco(), Cron.Monthly);
    
}
else
{
    umbracoBuilder.SetServerRegistrar<SingleServerRoleAccessor>();
}

umbracoBuilder.Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
