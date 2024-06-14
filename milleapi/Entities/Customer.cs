using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace milleapi.Entities;

public class Customer : BaseEntity<int>
{
    [Required]
    [MaxLength(200)]
    public string? LastName { get; set; }
    
    [MaxLength(200)]
    public string? FirstName { get; set; }
    
    public bool IsDeleted { get; set; }

    [SetsRequiredMembers]
    public Customer(int id, string lastName)
    {
        Id = id;
        LastName = lastName;
    }
}