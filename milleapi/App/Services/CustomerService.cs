using milleapi.App.Interfaces;
using milleapi.Entities;
using milleapi.Shared.Pagination;

namespace milleapi.App.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Customer> Create(Customer customer, CancellationToken ct)
    {
        return await _repository.Add(customer, ct);
    }

    public async Task<Customer> Get(int id, CancellationToken ct)
    {
        return await _repository.Get(id, ct);
    }

    public async Task<PagedList<Customer>> GetAll(PaginationRequestParameters paginationParams, CancellationToken ct)
    {
        var customers = _repository.GetAll();
        var pagedCustomers = await PagedList<Customer>.Create(
            customers,
            paginationParams.PageNumber,
            paginationParams.PageSize,
            ct);
        return pagedCustomers;
    }
        
    public async Task Update(Customer customer, CancellationToken ct)
    {
        await _repository.Update(customer, ct);
    }

    public async Task Remove(int id, CancellationToken ct)
    {
        await _repository.Delete(id, ct);
    }
}