using System.Data;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.Customers.Commands;

namespace WebApi.Features.Customers.Handlers;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly AppDbContext _context;

    public DeleteCustomerHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken ct)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct);

        if (customer == null)
            throw new RowNotInTableException($"Customer with id {request.Id} not found");

        customer.IsDeleted = true;
        await _context.SaveChangesAsync(ct);
        return Unit.Value;
    }
}