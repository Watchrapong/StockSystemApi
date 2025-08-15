namespace StockSystem.Api.Exception
{
    public class AppException : System.Exception
    {
        public AppException(string message)
            : base(message)
        {
        }
    }
}