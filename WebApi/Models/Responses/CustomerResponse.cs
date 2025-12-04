namespace WebApi.Models.Responses;

public record CustomerResponse(
    int Id,
    string FirstName,
    string LastName,
    DateTime CreatedOn);
