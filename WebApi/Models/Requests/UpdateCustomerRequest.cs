namespace WebApi.Models.Requests;

public record UpdateCustomerRequest(string? FirstName, string LastName, bool IsDeleted);
