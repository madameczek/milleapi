using MediatR;
using WebApi.Models.Requests;
using WebApi.Models.Responses;

namespace WebApi.Features.Customers.Commands;

public record CreateCustomerCommand(CreateCustomerRequest Request) : IRequest<CustomerResponse>;
