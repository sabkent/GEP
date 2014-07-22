namespace LoanBook.Messaging
{
    public interface IHandleCommand<T>  //where T:ICommand
    {
        void Handle(T command);
    }
}