using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ReckonMe.Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services,
            string connectionString, string database)
        {
            var mongoClient = new MongoClient(connectionString);
            var db = mongoClient.GetDatabase(database);
            return services.AddScoped<IMongoDatabase>(_ => db);
        }
    }
}