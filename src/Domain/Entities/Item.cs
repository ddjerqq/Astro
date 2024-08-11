using Astro.Generated;
using Domain.Aggregates;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Item
{
    private ItemId Id { get; set; }
    private ItemTypeId TypeId { get; set; } 
    private ItemType Type { get; set; }
    ItemQuality Quality { get; set; }
    UserId OwnerId { get; set; }
    User Owner { get; set; }
}