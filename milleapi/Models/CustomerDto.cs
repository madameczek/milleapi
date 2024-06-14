namespace milleapi.Models;

public class CustomerDto
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string LastName { get; init; }
    public bool IsDeleted { get; init; }
    public DateTime CreatedOn { get; init; }

    public CustomerDto(int id, string? firstName, string lastName, bool isDeleted, DateTime createdOn)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        IsDeleted = isDeleted;
        CreatedOn = createdOn;
    }
}