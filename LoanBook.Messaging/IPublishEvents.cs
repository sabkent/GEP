namespace LoanBook.Messaging
{
    public interface IPublishEvents
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}