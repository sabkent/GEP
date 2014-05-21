namespace LoanBook.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq.Expressions;

    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    public sealed class MongoDbDocumentStore : IDocumentStore
    {
        private readonly MongoDatabase _mongoDatabase;

        public MongoDbDocumentStore()
        {
            var serverSetting = ConfigurationManager.AppSettings["mongodb:server"];
            var databaseSetting = ConfigurationManager.AppSettings["mongodb:database"];

            var client = new MongoClient(serverSetting);
            var server = client.GetServer();
            _mongoDatabase = server.GetDatabase(databaseSetting);
        }

        public IEnumerable<T> Get<T>(Expression<Func<T, bool>> query)
        {
            var collection = _mongoDatabase.GetCollection<T>(typeof(T).ToString());
            return collection.Find(Query<T>.Where(query));
        }

        public void Save<T>(T projection)
        {
            var collection = _mongoDatabase.GetCollection<T>(typeof(T).ToString());
            collection.Save(projection);
        }
    }
}
