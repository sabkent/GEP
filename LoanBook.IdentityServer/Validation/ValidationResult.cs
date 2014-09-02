
namespace LoanBook.IdentityServer.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            IsError = true;
        }

        public bool IsError { get; set; }
        public string Error { get; set; }

        public ErrorTypes ErrorType { get; set; }
    }
}
