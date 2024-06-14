using System.ComponentModel.DataAnnotations;

namespace milleapi.Entities;

public class Customer : BaseEntity<int>
{
    [Required] 
    [MaxLength(200)] 
    public string LastName { get; set; } = null!;
    
    [MaxLength(200)]
    public string? FirstName { get; set; }
    
    public bool IsDeleted { get; set; }
}