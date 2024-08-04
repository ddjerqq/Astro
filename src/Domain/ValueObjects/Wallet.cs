using Domain.Aggregates;

namespace Domain.ValueObjects;

public record Wallet(decimal Balance)
{

    public decimal Balance { get; private set; }

    public bool HasBalance(decimal amount)
    {
        if (Balance >= amount) return true;
        return false;
    }

    public bool TryTranfer(Wallet other, decimal amount)
    {
        
        // check if user has enough balance if not return false
        if (HasBalance(amount))
        {
            return false;
        }

        // if users balance won't be negative after we deduct, then we can say he has enough balance, so we can transfer the money 
        other.Balance += amount;
        Balance -= amount;
        return true;
    }
}