using System.Text.Json.Serialization;

namespace milleapi.Entities;

public abstract class BaseEntity<T>
{
    [JsonIgnore]
    public T Id { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
}