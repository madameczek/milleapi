using System.Data;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.Customers.Queries;
using WebApi.Models.Responses;

namespace WebApi.Features.Customers.Handlers;

public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerWithOrdersResponse>>
{
    private readonly AppDbContext _context;

    public GetAllCustomersHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerWithOrdersResponse>> Handle(GetAllCustomersQuery request, CancellationToken ct)
    {
        var customers = await _context.Customers
            .Where(c => c.IsDeleted == false)
            .Include(c => c.Orders)
            .Select(c => new CustomerWithOrdersResponse(
                c.Id,
                c.FirstName,
                c.LastName,
                c.CreatedAt,
                c.Orders.Select(o => new OrderResponse(
                    o.Id,
                    o.OrderNumber,
                    o.TotalAmount,
                    o.OrderDate,
                    o.CustomerId)).ToList()))
            .ToListAsync(ct);

        return customers;
    }
}
