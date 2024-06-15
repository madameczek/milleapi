using milleapi.Entities;
using milleapi.Shared.Pagination;

namespace milleapi.App.Interfaces;

public interface ICustomerService
{
    Task<Customer> Create(Customer customer, CancellationToken cancellationToken);
    Task<Customer> Get(int id, CancellationToken cancellationToken);
    Task<PagedList<Customer>> GetAll(PaginationRequestParameters paginationParams, CancellationToken cancellationToken);
    Task Update(Customer customer, CancellationToken cancellationToken);
    Task Remove(int id, CancellationToken cancellationToken);
}