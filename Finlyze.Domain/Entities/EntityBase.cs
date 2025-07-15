namespace Finlyze.Domain.Entity;

public abstract class EntityGuid
{
    public Guid Id { get; protected set; }

    public EntityGuid()
    {
        Id = Guid.NewGuid();
    }

    public EntityGuid(Guid id)
    {
        Id = id;
    }
}

public abstract class EntityInt
{
    public int Id { get; protected set; }
    public EntityInt() { }

    public EntityInt(int id)
    {
        Id = id;
    }
}