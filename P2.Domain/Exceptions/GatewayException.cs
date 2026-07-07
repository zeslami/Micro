namespace P2.Domain.Exceptions;

/// <summary>
/// Raised when P2 (the gateway) cannot get a valid/expected response while forwarding
/// a request to P1 (e.g. P1 is down, returned malformed data, or an unexpected status code).
/// </summary>
public class GatewayException : Exception
{
    public GatewayException(string message) : base(message)
    {
    }

    public GatewayException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
