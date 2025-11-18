using MediatR;
using WebApi.Models.Responses;

namespace WebApi.Features.Customers.Queries;

public record GetCustomerQuery(int Id) : IRequest<CustomerResponse>;

public record GetAllCustomersQuery : IRequest<List<CustomerWithOrdersResponse>>;
