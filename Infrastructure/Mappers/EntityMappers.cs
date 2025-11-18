using Domain.Entities;

namespace Infrastructure.Mappers;

public static class EntityMappers
{
    public static Customer ToEntity(this Domain.Models.Customer domainModel)
    {
        return new Customer
        {
            Id = domainModel.Id,
            FirstName = domainModel.FirstName,
            LastName = domainModel.LastName,
            IsDeleted = domainModel.IsDeleted,
            CreatedAt = domainModel.CreatedAt
        };
    }

    public static Domain.Models.Customer ToDomainModel(this Customer entity)
    {
        return new Domain.Models.Customer
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            IsDeleted = entity.IsDeleted,
            CreatedAt = entity.CreatedAt
        };
    }

    public static Order ToEntity(this Domain.Models.Order domainModel)
    {
        return new Order
        {
            Id = domainModel.Id,
            OrderNumber = domainModel.OrderNumber,
            TotalAmount = domainModel.TotalAmount,
            OrderDate = domainModel.OrderDate,
            CustomerId = domainModel.CustomerId
        };
    }

    public static Domain.Models.Order ToDomainModel(this Order entity)
    {
        return new Domain.Models.Order
        {
            Id = entity.Id,
            OrderNumber = entity.OrderNumber,
            TotalAmount = entity.TotalAmount,
            OrderDate = entity.OrderDate,
            CustomerId = entity.CustomerId
        };
    }
}