using System.Data;
using milleapi.Models;

namespace milleapi.App.DataSource;

public class CustomerService : ICustomerService
{
    public Task<int> Create(CustomerDto dto, CancellationToken cancellationToken)
    {
        return Task.FromResult(3);
    }

    public Task<CustomerDto> Get(int id, CancellationToken cancellationToken)
    {
        if (id % 2 == 0)
            return Task.FromResult(
                new CustomerDto { FirstName = "Jan", LastName = "Wąski", IsDeleted = false });
        
        throw new RowNotInTableException();
    }

    public Task Update(int id, CustomerDto dto, CancellationToken cancellationToken)
    {
        throw new RowNotInTableException();
    }

    public Task Remove(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}