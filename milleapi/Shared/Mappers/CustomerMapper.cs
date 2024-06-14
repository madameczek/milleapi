using milleapi.Entities;
using milleapi.Models;

namespace milleapi.Shared.Mappers;

public static class CustomerMapper
{
    public static Customer ToCustomer(this CreateCustomerDto dto) =>
        new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CreatedOn = DateTime.UtcNow
        };
    
    public static Customer ToCustomer(this UpdateCustomerDto dto) =>
        new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsDeleted = dto.IsDeleted
        };

}