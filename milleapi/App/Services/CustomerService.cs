﻿using milleapi.App.Interfaces;
using milleapi.Entities;

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

    public async Task Update(int id, Customer customer, CancellationToken ct)
    {
        await _repository.Update(customer, ct);
    }

    public async Task Remove(int id, CancellationToken ct)
    {
        await _repository.Delete(id, ct);
    }
}