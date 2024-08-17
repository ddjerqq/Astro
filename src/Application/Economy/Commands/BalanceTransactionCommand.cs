using Application.Services;
using Application.Users.Commands;
using Astro.Generated;
using Domain.Aggregates;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;

namespace Application.Economy.Commands;

public sealed record BalanceTransactionCommand(UserId SenderId, UserId ReceiverId, decimal Amount) : IRequest<bool>;

public sealed class TransactionValidator : AbstractValidator<BalanceTransactionCommand>
{
    public TransactionValidator()
    {
        RuleFor(x => x.Amount)
            .LessThanOrEqualTo(0)
            .WithMessage("amount can not be 0 or less");
    }
}

public sealed class BalanceTransactionCommandHandler(IAppDbContext dbContext)
    : IRequestHandler<BalanceTransactionCommand, bool>
{
    public async Task<bool> Handle(BalanceTransactionCommand request, CancellationToken ct)
    {
        var sender = await dbContext.Set<User>().FindAsync(request.SenderId, ct);
        var receiver = await dbContext.Set<User>().FindAsync(request.ReceiverId, ct);
        // if one of the users is null throw an exception
        if (sender is null || receiver is null)
        {
            throw new InvalidOperationException("sender or receiver is invalid");
        }

        if (sender.Wallet.TryTransfer(receiver.Wallet, request.Amount))
        {
            dbContext.Set<User>().Update(receiver);
            dbContext.Set<User>().Update(sender);
            await dbContext.SaveChangesAsync(ct);
            return true;
        }
        
        return false;
    }
}