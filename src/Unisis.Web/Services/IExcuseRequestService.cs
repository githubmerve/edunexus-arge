using Unisis.Web.Models;

namespace Unisis.Web.Services;

public interface IExcuseRequestService
{
    Task<ExcuseRequest> CreateAsync(ExcuseRequest request, CancellationToken cancellationToken = default);
}
