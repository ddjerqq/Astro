using Domain.Aggregates;
namespace Domain.ValueObjects;

public class Wallet
{
    public User User { get; set; } = default!;
    private decimal Balance { get; set; }
    
    public bool HasBalance(decimal amount)
    {
        if (amount == Balance) return true;
        return false;
    }

    public bool TryTranfer(Wallet other)
    {
         
    }
}