namespace Sharpbox.Api.Core.Persistence;

public interface IEntity<TIdentifier>
    where TIdentifier : notnull
{
    TIdentifier Id { get; }
}