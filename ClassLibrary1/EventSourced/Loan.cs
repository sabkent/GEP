using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ClassLibrary1.EventSourced
{
    class Loan : Aggregate
    {
        private decimal _Amount;
        private List<Payment> _payments;

        public Loan(decimal amount) : this()
        {
            Apply(new LoanCreated{Amount = amount});
        }

        private Loan()
        {
            _payments = new List<Payment>();
        }

        public void Activate()
        {
            
        }

        public IEnumerable<IEvent> AcceptPayment(decimal amount, DateTime received)
        {
            var events = new List<IEvent>();
            if (GetBalance() > 0)
            {
                var paymentAccepted = new PaymentAccepted {Amount = amount, Received = received};
                Apply(paymentAccepted);
                events.Add(paymentAccepted);
            }
            return events;
        }

        private decimal GetBalance()
        {
            return 0;
        }

        private void ApplyEvent(LoanCreated loanCreate)
        {
            bool pass = true;
        }

        private void ApplyEvent(PaymentAccepted paymentAccepted)
        {
            _payments.Add(new Payment());
        }
    }

    class Payment
    {
        public decimal Amount { get; set; }
        public DateTime Received { get; set; }
    }

    class LoanCreated : IAggregateEvent
    {
        public decimal Amount { get; set; }
    }

    class PaymentAccepted : IAggregateEvent, IEvent
    {
        public decimal Amount { get; set; }
        public DateTime Received{ get; set; }
    }

    class LoanActivated
    {
        
    }

    class Aggregate
    {
        public void Apply<T>(T @event)
        {
            AggregateUpdater.Update(this, @event);
        }

        public void LoadFromHistory(IEnumerable<IAggregateEvent> events)
        {
            events.ToList().ForEach(Apply);
        }
    }

    public interface IAggregateEvent{}

    interface IEvent{}

    public interface IEventStore
    {
        IEnumerable<IAggregateEvent> LoadEvents(Guid id);
    }

    static class AggregateUpdater
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type>, Action<Aggregate, object>> cache = new ConcurrentDictionary<Tuple<Type, Type>, Action<Aggregate, object>>();

        public static void Update(Aggregate instance, object @event)
        {
            var tuple = new Tuple<Type, Type>(instance.GetType(), @event.GetType());
            var action = cache.GetOrAdd(tuple, ActionFactory);
            action(instance, @event);
        }

        private static Action<Aggregate, object> ActionFactory(Tuple<Type, Type> key)
        {
            var eventType = key.Item2;
            var aggregateType = key.Item1;

            const string methodName = "ApplyEvent";
            var method = aggregateType.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SingleOrDefault(x => x.Name == methodName && x.GetParameters().Single().ParameterType.IsAssignableFrom(eventType));

            if (method == null)
                return (x, y) => { };

            return (instance, @event) => method.Invoke(instance, new[] { @event });
        }
    }

    class EventSourceRepository
    {
        private readonly IEventStore _eventStore;

        public EventSourceRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public T GetById<T>(Guid id) where T : class
        {
            var aggregate = Activator.CreateInstance(typeof (T), true); //second arg is use private ctor
            var events = _eventStore.LoadEvents(id);
            ((Aggregate)aggregate).LoadFromHistory(events);

            return (T)aggregate;
        }
    }
}
