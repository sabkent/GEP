using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace ClassLibrary1.EventSourced
{
    [TestFixture]
    public class EventSourcedTests
    {
        [Test]
        public void Test()
        {
            var eventStore = new Mock<IEventStore>();
            eventStore.Setup(store => store.LoadEvents(It.IsAny<Guid>()))
                .Returns(new List<IAggregateEvent> {new LoanCreated(), new PaymentAccepted()});

            var loan = new Loan(100m);

            var agg = new EventSourceRepository(eventStore.Object).GetById<Loan>(Guid.NewGuid());
        }
    }
}
