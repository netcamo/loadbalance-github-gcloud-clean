using Umbraco.Cms.Core.Sync;
namespace LoadBalanceGithubGcloudClean.Web.ServerRoleAccessors;

public class SubscriberServerRoleAccessor :IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.Subscriber;
}