using Umbraco.Cms.Core.Sync;
namespace LoadBalanceGithubGcloudClean.Web.ServerRoleAccessors;

public class SchedulingPublisherServerRoleAccessor : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.SchedulingPublisher;
}