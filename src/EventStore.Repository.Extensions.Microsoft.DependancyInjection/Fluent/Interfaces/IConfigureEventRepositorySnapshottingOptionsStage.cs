namespace EventStore.Repository
{
    public interface IConfigureEventRepositorySnapshottingOptionsStage<T> where T : IAggregateMarker
    {
        public IEventRepository<T> Build();
    }
}
