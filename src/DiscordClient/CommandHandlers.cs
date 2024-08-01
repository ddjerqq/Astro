using System.ComponentModel;
using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.ContextChecks;
using DSharpPlus.Entities;

namespace DiscordClient;

public sealed class CommandHandlers(/* IDependency dependency */)
{
    // a check, that enforces that this command can only be used in Guilds (servers)
    [RequireGuild]
    [Command("ping")]
    [Description("Pings the bot")]
    public async Task Ping(CommandContext context)
    {
        var delay = DateTimeOffset.UtcNow - context.Client.GetConnectionLatency(context.Guild!.Id);
        await context.RespondAsync(new DiscordEmbedBuilder()
            .WithDescription($"Pong! 🏓\nLatency: {delay.Millisecond}ms")
            .WithColor(DiscordColor.Green));
    }

    // add two ints
    [Command("add")]
    [Description("Adds two numbers together")]
    public async Task Add(CommandContext context, [Description("The first number")] int a, [Description("The second number")] int b)
    {
        // embed example
        await context.RespondAsync(new DiscordEmbedBuilder()
            .WithDescription($"{a} + {b} = {a + b}")
            .WithColor(DiscordColor.Green));
    }

    public enum PetOption
    {
        Cat,
        Dog,
        Lizard,
    }

    // choose from embed selection
    [Command("choose")]
    [Description("Choose a pet")]
    public async Task Execute(CommandContext context, PetOption mode, [RemainingText] string input)
    {
        await context.RespondAsync(new DiscordEmbedBuilder()
            .WithDescription($"You chose {mode} and the input is {input}")
            .WithColor(DiscordColor.Green));
    }

    [Command("solve")]
    [Description("solves the given equation, and downloads the PDF including the step-by-step solution if applicable.")]
    public async Task Execute(CommandContext context, [RemainingText] [Description("The query to pass to wolfram alpha")] string input)
    {
        // TODO this is an example that uses the injected service and downloads a file or some shit
        // the service is injected inside of the handler class.

        // await context.DeferResponseAsync();
        //
        // var fullTask = client.DownloadFullResultsAsync(input);
        // var stepByStepTask = crawler.DownloadStepByStepSolutionAsync(input);
        //
        // var full = await fullTask;
        // if (full is null)
        // {
        //     throw new InvalidOperationException("Could not download files, please check the query and see if its valid");
        // }
        //
        // await using var fsFull = File.OpenRead(full.FullName);
        // await context.FollowupAsync(new DiscordMessageBuilder()
        //     .WithContent($"{context.User.Mention} here is the full result for {input}")
        //     .WithAllowedMention(new UserMention(context.User))
        //     .AddFile(full.Name, fsFull, AddFileOptions.CopyStream));
        //
        // var stepByStep = await stepByStepTask;
        // if (stepByStep is not null)
        // {
        //     await using var fsStepByStep = File.OpenRead(stepByStep.FullName);
        //     await context.FollowupAsync(new DiscordMessageBuilder()
        //         .WithContent($"{context.User.Mention} here is a step by step result for {input}")
        //         .WithAllowedMention(new UserMention(context.User))
        //         .AddFile(stepByStep.Name, fsStepByStep, AddFileOptions.CopyStream));
        // }
    }
}