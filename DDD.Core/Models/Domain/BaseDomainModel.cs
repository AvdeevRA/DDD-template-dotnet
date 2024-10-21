namespace DDD.Core.Models.Domain;

public abstract class BaseDomainModel
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
