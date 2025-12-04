using System.Data;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.Customers.Commands;

namespace WebApi.Features.Customers.Handlers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly AppDbContext _context;

    public UpdateCustomerHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken ct)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (customer == null)
            throw new RowNotInTableException($"Customer with id {request.Id} not found");

        customer.FirstName = request.Request.FirstName ?? string.Empty;
        customer.LastName = request.Request.LastName;
        customer.IsDeleted = request.Request.IsDeleted;

        await _context.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
