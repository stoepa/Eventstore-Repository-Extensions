using System;

namespace EventStore.Repository
{
    public interface IConfigureEventRepositorySnapshottingStage<T> where T : IAggregateMarker
    {
        IConfigureEventRepositorySnapshottingOptionsStage<T> WithSnapshotConfiguration(Action<EventRepositorySnapshottingOptions> configure);
    }
}
