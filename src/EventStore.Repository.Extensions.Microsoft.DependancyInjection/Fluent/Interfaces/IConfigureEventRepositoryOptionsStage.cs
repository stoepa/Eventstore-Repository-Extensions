namespace EventStore.Repository
{
    public interface IConfigureEventRepositoryOptionsStage<T> where T: IAggregateMarker
    {
        IConfigureEventRepositorySnapshottingStage<T> AddSnapshots(IEventStoreSnapshotService<T> eventStoreSnapshotService);
        public IEventRepository<T> Build();
    }
}
