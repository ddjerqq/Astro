using Application.Services;
using Application.Users.Commands;
using Astro.Generated;
using Domain.Aggregates;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
namespace Application.Economy.Commands;


public sealed record BalanceTransactionCommand(UserId sender, UserId reciver, decimal amount) : IRequest<bool>;

public sealed class TransactionValidator : AbstractValidator<BalanceTransactionCommand>
{
    public TransactionValidator()
    {
        RuleSet("async", () => { RuleFor(x => x.amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("amount can not be 0 or less");});
       
    }
}



public sealed class BalanceTransactionCommandHandler(IAppDbContext dbContext, CancellationToken ct)
    : IRequestHandler<BalanceTransactionCommand, bool>
{
    public Task<bool> Handle(BalanceTransactionCommand request, CancellationToken ct)
    {
        // request.reciver.Wallet.Balance + request.amount;
        // request.sender.Wallet.Balance - request.amount;
        dbContext.Set<User>();
    };

}