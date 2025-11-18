namespace WebApi.Models.Responses;

public record OrderResponse(
    int Id,
    string OrderNumber,
    decimal TotalAmount,
    DateTime OrderDate,
    int CustomerId);
