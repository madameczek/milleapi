using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace milleapi.Models;

public class UpdateCustomerDto
{
    [JsonIgnore]
    public int Id { get; set; }
    
    [StringLength(200)] 
    public string? FirstName { get; set; }
    
    [MinLength(2)]
    [StringLength(200)]
    public string LastName { get; set; } = null!;
    
    public bool IsDeleted { get; set; }
}