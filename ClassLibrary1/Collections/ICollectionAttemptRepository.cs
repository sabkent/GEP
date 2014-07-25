using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Collections.Entity;

namespace ClassLibrary1.Collections
{
    public interface ICollectionAttemptRepository
    {
        void Add(CollectionAttempt collectionAttempt);
    }

    public class CollectionAttempRepository : ICollectionAttemptRepository
    {

        public void Add(CollectionAttempt collectionAttempt)
        {
            
        }
    }
}
