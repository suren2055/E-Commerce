namespace E_Commerce.API.Exceptions;

public class OrderingException : Exception
{
    public OrderingException()
    { }

    public OrderingException(string message)
        : base(message)
    { }

    public OrderingException(string message, Exception innerException)
        : base(message, innerException)
    { }
}