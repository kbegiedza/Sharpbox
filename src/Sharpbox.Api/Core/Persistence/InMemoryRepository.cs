using System.Collections.Concurrent;

namespace Sharpbox.Api.Core.Persistence;

public class InMemoryRepository<TIdentifier, TItem>
    where TIdentifier : notnull
    where TItem : IEntity<TIdentifier>
{
    private readonly ConcurrentDictionary<TIdentifier, TItem> _items;

    public InMemoryRepository()
    {
        _items = new ConcurrentDictionary<TIdentifier, TItem>();
    }

    public Task<TItem?> GetAsync(TIdentifier id)
    {
        if (_items.TryGetValue(id, out var item))
        {
            return Task.FromResult((TItem?)item);
        }

        return Task.FromResult(default(TItem));
    }

    public Task<TItem> CreateAsync(TItem entity)
    {
        if(_items.TryAdd(entity.Id, entity))
        {
            return Task.FromResult(entity);
        }
        
        if (_items.ContainsKey(entity.Id))
        {
            throw new Exception("Item already exists.");
        }

        throw new Exception("Failed to add item.");
    }

    public async Task<TItem> UpdateAsync(TItem entity)
    {
        if (_items.ContainsKey(entity.Id))
        {
            _items[entity.Id] = entity;

            return entity;
        }

        return await CreateAsync(entity);
    }

    public Task DeleteAsync(TIdentifier id)
    {
        if (_items.TryRemove(id, out _))
        {
            return Task.CompletedTask;
        }

        if (_items.ContainsKey(id))
        {
            throw new Exception("Failed to remove item.");
        }

        return Task.CompletedTask;
    }
}