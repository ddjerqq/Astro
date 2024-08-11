using Application.Services;
using Application.Users.Commands;
using Domain.Aggregates;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
namespace Application.Economy.Commands;


public sealed record BalanceTransactionCommand(User sender, User reciver, decimal amount) : IRequest<bool>;

public sealed class TransactionValidator : AbstractValidator<BalanceTransactionCommand>
{
    public TransactionValidator()
    {
        RuleFor(x => x.amount).LessThanOrEqualTo(0).WithMessage("amount can not be 0 or less");
    }
}



public sealed class BalanceTransactionCommandHandler(IAppDbContext dbContext, CancellationToken ct)
    : IRequestHandler<BalanceTransactionCommand, bool>
{
    public Task<bool> Handle(BalanceTransactionCommand request,CancellationToken ct)
    {
        // request.reciver.Wallet.Balance + request.amount;
        // request.sender.Wallet.Balance - request.amount;
    };
};