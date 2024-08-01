using System.Reflection;
using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.Exceptions;
using DSharpPlus.Commands.Processors.TextCommands;
using DSharpPlus.Commands.Processors.TextCommands.Parsing;
using DSharpPlus.Entities;
using DSharpPlus.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DiscordClient;


public static class ServiceExt
{
    public static IServiceCollection AddDiscord(this IServiceCollection services, string token)
    {
        // var logFactory = new LoggerFactory().AddSerilog(Log.Logger);
        return services.AddDiscordClient(token, DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents);
    }

    public static DSharpPlus.DiscordClient UseCommandsAndEvents(this IServiceProvider services, ulong debugGuildId, params Type[] commandHandlers)
    {
        var client = services.GetRequiredService<DSharpPlus.DiscordClient>();

        var commandsExtension = client.UseCommands(new CommandsConfiguration
        {
            UseDefaultCommandErrorHandler = false,
            DebugGuildId = debugGuildId,
        });

        commandsExtension.AddChecks(Assembly.GetExecutingAssembly());
        commandsExtension.AddCommands(commandHandlers);

        commandsExtension.AddProcessor(new TextCommandProcessor(new TextCommandConfiguration
        {
            IgnoreBots = true,
            PrefixResolver = new DefaultPrefixResolver(true, "!").ResolvePrefixAsync,
        }));

        commandsExtension.CommandErrored += async (_, e) =>
        {
            if (e.Exception is CommandNotFoundException)
            {
                Log.Logger.Error(e.Exception, "command not found");
                return;
            }

            // TODO better scope incident IDs
            var incidentId = Ulid.NewUlid();

            await e.Context.RespondAsync(new DiscordEmbedBuilder()
                .WithTitle("Something went wrong!")
                .WithColor(DiscordColor.Red)
                .WithDescription($"```js\n{e.Exception.Message}```")
                .WithUrl("https://github.com/ddjerqq/bugi/issues")
                .WithFooter($"Incident id: {incidentId}")
            );

            Log.Logger.Error(e.Exception, $"Incident {incidentId} has occured while executing command {e.Context.Command.Name}");

            throw new Exception($"Incident {incidentId} has occured while executing command {e.Context.Command.Name}", e.Exception);
        };

        return client;
    }
}