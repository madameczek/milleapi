using System.ComponentModel.DataAnnotations;

namespace milleapi.Entities;

public abstract class BaseEntity<T>
{
    [Key]
    public T Id { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
}