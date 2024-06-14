using milleapi.Models;

namespace milleapi.App.DataSource;

public interface ICustomerService
{
    Task<int> Create(CustomerDto customer, CancellationToken cancellationToken);
    Task<CustomerDto> Get(int id, CancellationToken cancellationToken);
    Task Update(string id, CustomerDto customer, CancellationToken cancellationToken);
    Task Remove(string id, CancellationToken cancellationToken);
}