using MediatR;
using WebApi.Models.Requests;

namespace WebApi.Features.Customers.Commands;

public record UpdateCustomerCommand(int Id, UpdateCustomerRequest Request) : IRequest<Unit>;
