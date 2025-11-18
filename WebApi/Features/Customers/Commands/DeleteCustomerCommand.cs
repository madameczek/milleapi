using MediatR;

namespace WebApi.Features.Customers.Commands;

public record DeleteCustomerCommand(int Id) : IRequest<Unit>;