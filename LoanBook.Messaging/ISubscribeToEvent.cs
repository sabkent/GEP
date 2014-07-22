namespace LoanBook.Messaging
{
    public interface ISubscribeToEvent<T> //where T : IEvent
    {
        void Notify(T @event);
    }
}