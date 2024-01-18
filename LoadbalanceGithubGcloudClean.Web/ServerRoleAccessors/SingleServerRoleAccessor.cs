using Umbraco.Cms.Core.Sync;
namespace LoadBalanceGithubGcloudClean.Web.ServerRoleAccessors;

public class SingleServerRoleAccessor : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.Single;
}