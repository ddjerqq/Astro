using Astro.Generated;
using Domain.Abstractions;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Entities;

[StrongId(typeof(Ulid))]
public sealed class Item(ItemId id) : Entity<ItemId>(id)
{
    public ItemTypeId TypeId { get; init; }
    public ItemType Type { get; init; } = default!;
    public ItemQuality Quality { get; init; } = default!;
    public UserId OwnerId { get; init; } = default!;
    public User Owner { get; init; } = default!;
}