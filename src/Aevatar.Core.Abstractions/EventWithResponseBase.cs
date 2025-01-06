namespace Aevatar.Core.Abstractions;

[GenerateSerializer]
public abstract class EventWithResponseBase<TResponseEvent> : EventBase where TResponseEvent : EventBase
{
}