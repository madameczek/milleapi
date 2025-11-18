using System.Data;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.Customers.Queries;
using WebApi.Models.Responses;

namespace WebApi.Features.Customers.Handlers;

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerResponse>
{
    private readonly AppDbContext _context;

    public GetCustomerHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerResponse> Handle(GetCustomerQuery request, CancellationToken ct)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == request.Id && c.IsDeleted == false, ct);

        if (customer == null)
            throw new RowNotInTableException($"Customer with id {request.Id} not found");

        return new CustomerResponse(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.CreatedAt);
    }
}
