namespace WebApi.Models.Responses;

public record CustomerWithOrdersResponse(
    int Id,
    string FirstName,
    string LastName,
    DateTime CreatedOn,
    List<OrderResponse> Orders);