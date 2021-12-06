using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventStore.Repository
{
    public static class EventRepositoryExtensions
    {
        public static void AddEventRepository<T>(this IServiceCollection services, Func<IServiceProvider, IEventRepository<T>> builder)
            where T : IAggregateMarker, new()
        {
            services.AddSingleton(serviceProvider =>
            {
                var eventRepositoryBuilder = builder(serviceProvider);
                return eventRepositoryBuilder;
            });
        }
    }
}
