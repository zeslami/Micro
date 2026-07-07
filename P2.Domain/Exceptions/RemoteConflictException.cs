namespace P2.Domain.Exceptions;

/// <summary>Raised when P1 responded with 409 for a forwarded request (e.g. duplicate username).</summary>
public class RemoteConflictException : Exception
{
    public RemoteConflictException(string message) : base(message)
    {
    }
}
