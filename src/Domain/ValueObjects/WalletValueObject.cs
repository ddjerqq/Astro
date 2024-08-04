using Domain.Aggregates;
namespace Domain.ValueObjects;

public record Wallet(decimal Balance){
    public User User { get; set; }
    private decimal Balance { get; set; } = Balance;
    
    public bool HasBalance(decimal amount)
    {
        if (amount == Balance) return true;
        return false;
    }

    public bool TryTranfer(Wallet other)
    {
          //todo add transfer logic
          throw new NotImplementedException();
    }
}