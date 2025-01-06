using Microsoft.Extensions.Logging;

namespace Aevatar.Core;

public abstract partial class GAgentBase<TState, TEvent>
{
    private readonly IGrainState<List<GrainId>> _subscribers = new GrainState<List<GrainId>>();
    private readonly IGrainState<GrainId> _subscription = new GrainState<GrainId>();
    private IDisposable? _stateSaveTimer;

    private async Task LoadSubscribersAsync()
    {
        if (_subscribers.State.IsNullOrEmpty())
        {
            await GrainStorage.ReadStateAsync(AevatarGAgentConstants.SubscribersStateName, this.GetGrainId(),
                _subscribers);
        }
    }

    private async Task AddSubscriberAsync(GrainId grainId)
    {
        await LoadSubscribersAsync();
        _subscribers.State ??= [];
        if (_subscribers.State.Contains(grainId))
        {
            Logger.LogError($"Cannot add duplicate subscriber {grainId}.");
            return;
        }

        _subscribers.State.Add(grainId);
        await SaveSubscriberAsync(CancellationToken.None);
    }

    private async Task RemoveSubscriberAsync(GrainId grainId)
    {
        await LoadSubscribersAsync();
        if (_subscribers.State.IsNullOrEmpty())
        {
            return;
        }

        if (_subscribers.State.Remove(grainId))
        {
            await GrainStorage.WriteStateAsync(AevatarGAgentConstants.SubscribersStateName, this.GetGrainId(),
                _subscribers);
        }
    }

    private async Task LoadSubscriptionAsync()
    {
        if (_subscription.State.IsDefault)
        {
            await GrainStorage.ReadStateAsync(AevatarGAgentConstants.SubscriptionStateName, this.GetGrainId(),
                _subscription);
        }
    }

    private async Task SetSubscriptionAsync(GrainId grainId)
    {
        var storedSubscription = new GrainState<GrainId>();
        await GrainStorage.ReadStateAsync(AevatarGAgentConstants.SubscriptionStateName, this.GetGrainId(),
            storedSubscription);
        if (!storedSubscription.State.IsDefault)
        {
            await GrainStorage.ClearStateAsync(AevatarGAgentConstants.SubscriptionStateName, this.GetGrainId(),
                storedSubscription);
        }

        _subscription.State = grainId;
        await GrainStorage.WriteStateAsync(AevatarGAgentConstants.SubscriptionStateName, this.GetGrainId(),
            _subscription);
    }

    private async Task SaveSubscriberAsync(CancellationToken cancellationToken)
    {
        if (!_subscribers.State.IsNullOrEmpty())
        {
            await GrainStorage.WriteStateAsync(AevatarGAgentConstants.SubscribersStateName, this.GetGrainId(),
                _subscribers);
        }
    }
}