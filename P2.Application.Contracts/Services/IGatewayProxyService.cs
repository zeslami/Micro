using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace P2.Application.Contracts.Services;

public interface IGatewayProxyService
{
    Task<HttpResponseMessage> GetAsync(string path, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> PostAsync<T>(string path, T body, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> PutAsync<T>(string path, T body, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> DeleteAsync(string path, CancellationToken cancellationToken = default);
}
