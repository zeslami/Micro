namespace P2.Domain.Exceptions;

/// <summary>Raised when P1 responded with 401 for a forwarded request (e.g. bad login).</summary>
public class RemoteUnauthorizedException : Exception
{
    public RemoteUnauthorizedException(string message) : base(message)
    {
    }
}
