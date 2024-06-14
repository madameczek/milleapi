using milleapi.Entities;
using milleapi.Models;

namespace milleapi.Shared.Mappers;

public static class DtoMapper
{
    public static CustomerDto AsDto(Customer customer) =>
        new CustomerDto(customer.Id, customer.FirstName, customer.LastName, customer.IsDeleted, customer.CreatedOn);
}