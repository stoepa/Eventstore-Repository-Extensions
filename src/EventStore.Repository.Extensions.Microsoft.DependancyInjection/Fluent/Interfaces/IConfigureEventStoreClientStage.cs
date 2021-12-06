using System;

namespace EventStore.Repository
{
    public interface IConfigureEventStoreClientStage<T> where T : IAggregateMarker
    {
        IConfigureEventRepositoryOptionsStage<T> WithConfiguration(Action<EventRepositoryOptions> configure);
    }
}
