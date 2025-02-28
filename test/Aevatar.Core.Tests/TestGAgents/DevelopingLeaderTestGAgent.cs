using Aevatar.Core.Abstractions;
using Aevatar.Core.Tests.TestEvents;
using Microsoft.Extensions.Logging;

namespace Aevatar.Core.Tests.TestGAgents;

public interface IDevelopingLeaderTestGAgent: IGAgent
{
    
}

[GAgent]
public class DevelopingLeaderTestGAgent : GAgentBase<NaiveTestGAgentState, NaiveTestGEvent>, IDevelopingLeaderTestGAgent
{
    public DevelopingLeaderTestGAgent(ILogger<DevelopingLeaderTestGAgent> logger) : base(logger)
    {
    }

    public override Task<string> GetDescriptionAsync()
    {
        return Task.FromResult("This GAgent acts as a developing leader.");
    }
    
    public async Task HandleEventAsync(NewDemandTestEvent eventData)
    {
        await PublishAsync(new DevelopTaskTestEvent
        {
            Description = $"This is the demand for the task: {eventData.Description}"
        });
    }

    public async Task HandleEventAsync(NewFeatureCompletedTestEvent eventData)
    {
        if (State.Content.IsNullOrEmpty())
        {
            State.Content = [];
        }

        State.Content.Add(eventData.PullRequestUrl);

        if (State.Content.Count == 3)
        {
            await PublishAsync(new NewFeatureCompletedTestEvent
            {
                PullRequestUrl = string.Join("\n", State.Content)
            });
        }
    }
}