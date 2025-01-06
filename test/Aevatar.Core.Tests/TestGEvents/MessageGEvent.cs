using Aevatar.Core.Abstractions;

namespace Aevatar.Core.Tests.TestGEvents;

[GenerateSerializer]
public class MessageGEvent : GEventBase
{
    [Id(0)] public Guid Id { get; set; } = Guid.NewGuid();
}