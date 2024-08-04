using Domain.Aggregates;

namespace Domain.ValueObjects;

public record Wallet(decimal Balance)
{

    public decimal Balance { get; private set; } = Balance;

    public bool HasBalance(decimal amount)
    {
        if (amount == 0) throw new ArgumentException("amount can't be 0");
        return Balance >= amount;
    }

    public bool TryTransfer(Wallet other, decimal amount)
    {
        
        // check if user has enough balance if yes, transfer return true. if not return false
        if (HasBalance(amount))
        {
            other.Balance += amount;
            Balance -= amount;
            return true;
        }

        return false;
    }
}