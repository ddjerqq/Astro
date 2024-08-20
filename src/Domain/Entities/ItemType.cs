using Astro.Generated;
using Domain.Abstractions;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Entities;

[StrongId(typeof(Ulid))]
public class ItemType(ItemTypeId id) : Entity<ItemTypeId>(id)
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public float QualityMin { get; init; } // the minimum quality this item appears in
    public float QualityMax { get; init; } // the maximum quality this item can have
    public bool StatTrackAvailable { get; init; } // whether or not stat track is available
    public string? ImageUrl { get; init; }

    public Item CreateItem(User owner)
    {
        var random = Random.Shared.NextSingle() * (QualityMax - QualityMin) + QualityMin;
        bool isStatTrack = false;
        if (StatTrackAvailable)
        {
            isStatTrack = Random.Shared.Next(100) < 25;
        }

        return new Item(ItemId.New())
        {
            TypeId = Id,
            Type = this,
            Quality = new ItemQuality(random, isStatTrack),
            OwnerId = owner.Id,
            Owner = owner
        };
    }
}