using EventStore.Client;
using System;
using System.Collections.Generic;

namespace EventStore.Repository
{

    public class EventRepositoryBuilder<T> :
        IConfigureEventStoreClientStage<T>,
        IConfigureEventRepositoryOptionsStage<T>,
        IConfigureEventRepositorySnapshottingStage<T>,
        IConfigureEventRepositorySnapshottingOptionsStage<T> where T : IAggregateMarker, new()
    {
        private EventStoreClient eventStoreClient;
        private EventRepositoryOptions eventRepositoryOptions;
        private IEventStoreSnapshotService<T> snapshotService;
        private EventRepositorySnapshottingOptions eventRepositorySnapshottingOptions;
        private EventRepositoryBuilder(EventStoreClient eventStoreClient) => this.eventStoreClient = eventStoreClient;

        public static IConfigureEventStoreClientStage<T> CreateEventRepository(EventStoreClient eventStoreClient)
        {
            return new EventRepositoryBuilder<T>(eventStoreClient);
        }

        public IConfigureEventRepositoryOptionsStage<T> WithConfiguration(Action<EventRepositoryOptions> configure)
        {
            eventRepositoryOptions = new EventRepositoryOptions();
            if (configure == null)
                throw new ArgumentNullException(nameof(configure), "You cannot pass a empty or null configure method when adding a Event Repository configuration");
            configure(eventRepositoryOptions);
            return this;
        }

        public IConfigureEventRepositorySnapshottingStage<T> AddSnapshots(IEventStoreSnapshotService<T> eventStoreSnapshotService)
        {
            this.snapshotService = eventStoreSnapshotService;
            return this;
        }

        public IConfigureEventRepositorySnapshottingOptionsStage<T> WithSnapshotConfiguration(Action<EventRepositorySnapshottingOptions> configure)
        {
            eventRepositorySnapshottingOptions = new EventRepositorySnapshottingOptions();
            if (configure == null)
                throw new ArgumentNullException(nameof(configure), "You cannot pass a empty or null configure method when adding a Event Repository Snapshotting configuration");
            configure(eventRepositorySnapshottingOptions);
            return this;
        }

        public IEventRepository<T> Build()
        {
            if (eventRepositorySnapshottingOptions == null)
                eventRepositorySnapshottingOptions = new EventRepositorySnapshottingOptions { IsEnabled = false };

            return new EventRepository<T>(eventStoreClient, eventRepositoryOptions,
                snapshotService, eventRepositorySnapshottingOptions);
        }
    }
}
