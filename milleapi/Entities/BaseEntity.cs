using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace milleapi.Entities;

public abstract class BaseEntity<T>
{
    [Key]
    [JsonIgnore]
    public T Id { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
}