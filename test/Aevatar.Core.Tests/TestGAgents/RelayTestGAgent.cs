using Aevatar.Core.Abstractions;
using Aevatar.Core.Tests.TestEvents;
using Microsoft.Extensions.Logging;

namespace Aevatar.Core.Tests.TestGAgents;

[GAgent]
public class RelayTestGAgent : GAgentBase<NaiveTestGAgentState, NaiveTestGEvent>
{
    public RelayTestGAgent(ILogger logger) : base(logger)
    {
    }

    public override Task<string> GetDescriptionAsync()
    {
        return Task.FromResult("This is a GAgent used to test relay of events.");
    }

    public async Task HandleEventAsync(SocialTestEvent eventData)
    {
        // var chatIdKey = $"{typeof(ReceiveMessageTestEvent).FullName}.ChatId";
        // if (eventData.TryGetContext("ChatId", out var chatId)
        //     && chatId != null)
        // {
        //     await PublishAsync(new SendMessageTestEvent
        //     {
        //         ChatId = (string)chatId!,
        //         Message = "I handled a social event: " + eventData.Message
        //     });
        // }
    }
}