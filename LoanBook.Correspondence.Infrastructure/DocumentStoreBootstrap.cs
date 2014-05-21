using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBook.Correspondence.Infrastructure
{
    using LoanBook.Correspondence.Core;

    using MongoDB.Bson.Serialization;

    public class DocumentStoreBootstrap
    {
        public void Run()
        {
            BsonClassMap.RegisterClassMap<EmailMessage>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(x => x.Id);
            });
        }
    }
}
