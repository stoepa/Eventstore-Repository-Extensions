namespace EventStore.Repository
{
    public interface IConfigureEventRepositorySnapshottingOptionsStage<T> where T : IAggregateMarker
    {
        IEventRepository<T> Build();
    }
}
