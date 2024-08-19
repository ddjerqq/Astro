using Astro.Generated;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Aggregates;

[StrongId(typeof(Ulid))]
public sealed class User(UserId id) : AggregateRoot<UserId>(id)
{
    public string Username { get; init; } = default!;

    public string Email { get; init; } = default!;

    public string PasswordHash { get; init; } = default!;
    
    public Wallet Wallet { get; set; } = new Wallet(0);

    public ICollection<Item> Inventory {get; set; } = default!;
}