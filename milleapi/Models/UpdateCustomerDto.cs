using System.ComponentModel.DataAnnotations;

namespace milleapi.Models;

public class UpdateCustomerDto
{
   [StringLength(200)] 
    public string? FirstName { get; set; }
    
    [MinLength(2)]
    [StringLength(200)]
    public string LastName { get; set; } = null!;
    
    public bool IsDeleted { get; set; }
}