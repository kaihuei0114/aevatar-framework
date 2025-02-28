namespace Aevatar.Core.Tests.TestGEvents;

[GenerateSerializer]
public class ReceiveMessageTestGEvent : MessageGEvent
{
    [Id(0)]  public string MessageId { get; set; }
    [Id(1)]  public string ChatId { get; set; }
    [Id(2)] public string Message { get; set; }
    [Id(3)] public string NeedReplyBotName { get; set; }
}