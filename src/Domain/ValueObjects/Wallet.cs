namespace Domain.ValueObjects;

public record Wallet(decimal Balance)
{
    public decimal Balance { get; private set; } = Balance >= 0
        ? Balance
        : throw new ArgumentException(nameof(Balance), "Wallet balance cannot be negative!");

    public bool HasBalance(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException(nameof(amount), "amount must be positive and non zero");
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