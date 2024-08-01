using DiscordClient;
using Domain.Common;
using dotenv.net;
using Microsoft.Extensions.DependencyInjection;

DotEnv.Fluent()
    .WithProbeForEnv(6)
    .Load();

var token = "DISCORD__BOT_TOKEN".FromEnvRequired();
// this is the guild id where the bot will be in debug mode.
// debug mode means the application commands will be synced instantly
var debugGuildId = ulong.Parse("DISCORD__DEBUG_GUILD_ID".FromEnvRequired());

var services = new ServiceCollection()
    .AddDiscord(token)
    .BuildServiceProvider();

var discordClient = services.UseCommandsAndEvents(debugGuildId, typeof(CommandHandlers));

// var waClient = services.GetRequiredService<Client>();
// await waClient.InitializeAsync();

await discordClient.ConnectAsync();
await Task.Delay(-1);