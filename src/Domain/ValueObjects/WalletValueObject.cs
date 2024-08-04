using Domain.Aggregates;

namespace Domain.ValueObjects;

public record Wallet(decimal Balance)
{
    public User User { get; set; }
    private decimal _balance = default;

    public decimal Balance
    {
        get => _balance;
        set
        {
            if (value < 0)
            {
                throw new Exception("number cant be negative");
            }

            _balance += value;
        }
    }

    public bool HasBalance(decimal amount)
    {
        if (amount == Balance || Balance > amount) return true;
        return false;
    }

    public bool TryTranfer(Wallet other, decimal amount)
    {
        // check if user has enough balance if not return false
        if (Balance - amount < 0)
        {
            return false;
        }

        // if users balance won't be negative after we deduct, then we can say he has enough balance, so we can transfer the money 
        other.Balance += amount;
        Balance = Balance - amount;
        return true;
    }
}