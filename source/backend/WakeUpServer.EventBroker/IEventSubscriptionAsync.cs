namespace WakeUpServer.EventBroker
{
    public interface IEventSubscriptionAsync<in T> : IEventSubscriptionBase
    {
        Task HandleAsync(T data);
    }
}