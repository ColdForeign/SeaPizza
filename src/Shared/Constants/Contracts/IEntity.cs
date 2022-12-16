namespace SeaPizza.Shared.Constants.Contracts;

public interface IEntity
{
}

public interface IEntity<TId> : IEntity
{
    TId Id { get; }
}
