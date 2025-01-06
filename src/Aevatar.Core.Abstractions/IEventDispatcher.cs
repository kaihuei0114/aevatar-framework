namespace Aevatar.Core.Abstractions;

public interface IEventDispatcher
{
    Task PublishAsync(StateBase state, string id);
    Task PublishAsync(GEventBase eventBase, string id);
}