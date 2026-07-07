namespace P2.Domain.Exceptions;

/// <summary>Raised when P1 responded with 404 for a forwarded request.</summary>
public class RemoteNotFoundException : Exception
{
    public RemoteNotFoundException(string message) : base(message)
    {
    }
}
