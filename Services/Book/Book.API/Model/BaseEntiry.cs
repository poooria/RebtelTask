namespace Book.API.Model;

public abstract class BaseEntity
{
    public virtual int Id { get; protected set; }
}