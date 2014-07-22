namespace LoanBook.Messaging
{
    public interface ICommand : IMessage
    {
        
    }

    public interface IMessage
    {
        string Topic { get; }
    }
}
