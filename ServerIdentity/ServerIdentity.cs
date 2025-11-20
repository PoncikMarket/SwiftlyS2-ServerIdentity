using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;

using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Misc;

namespace ServerIdentityS2;

public class ServerIdentityConfig
{
    public string Command { get; set; } = "ip";
    public string Prefix { get; set; } = "{green}[ServerIdentity]{white}";
    public string MessageFormat { get; set; } =
        "{prefix} Server Address: {yellow}{ip}:{port}";
    public bool ConsolePrint { get; set; } = true;
}

[PluginMetadata(
    Id = "ServerIdentityS2",
    Version = "1.0.6",
    Name = "Server Identity",
    Author = "PoncikMarket",
    Description = "Shows server IP & port"
)]
public class ServerIdentityS2 : BasePlugin
{
    private ServerIdentityConfig _cfg = new();

    private readonly Dictionary<string, string> ColorMap = new()
    {
        { "{green}", Helper.ChatColors.Green },
        { "{white}", Helper.ChatColors.White },
        { "{yellow}", Helper.ChatColors.Yellow },
        { "{red}", Helper.ChatColors.Red },
        { "{blue}", Helper.ChatColors.Blue },
        { "{lightblue}", Helper.ChatColors.LightBlue },
        { "{orange}", Helper.ChatColors.Orange },
        { "{purple}", Helper.ChatColors.Purple },
        { "{grey}", Helper.ChatColors.Grey },
        { "{gray}", Helper.ChatColors.Grey },
        { "{default}", Helper.ChatColors.Default }
    };

    public ServerIdentityS2(ISwiftlyCore core) : base(core) {}

    public override void Load(bool hotReload)
    {
        Core.Configuration
            .InitializeJsonWithModel<ServerIdentityConfig>("ServerIdentityCFG.jsonc", "ServerIdentity")
            .Configure(builder =>
            {
                builder.AddJsonFile("ServerIdentityCFG.jsonc", optional: false, reloadOnChange: true);
            });

        _cfg = Core.Configuration.Manager
            .GetSection("ServerIdentity")
            .Get<ServerIdentityConfig>()!;

        Console.WriteLine("[ServerIdentity] Loaded.");
    }

    private string ApplyColors(string t)
    {
        foreach (var kv in ColorMap)
            t = t.Replace(kv.Key, kv.Value);
        return t;
    }

    [Command("ip")]
    public void OnIpCommand(ICommandContext ctx)
    {
        var player = ctx.Sender;
        if (player == null || !player.IsValid)
            return;

        string ip = Core.Engine.ServerIP ?? "0.0.0.0";

        int port = Core.ConVar.Find<int>("hostport")?.Value ?? 27015;

        string prefix = ApplyColors(_cfg.Prefix);

        string msg = _cfg.MessageFormat
            .Replace("{prefix}", prefix)
            .Replace("{ip}", ip)
            .Replace("{port}", port.ToString());

        msg = ApplyColors(msg);

        player.SendChat(msg);

        if (_cfg.ConsolePrint)
            player.SendMessage(MessageType.Console, $"Server address: {ip}:{port}");
    }

    public override void Unload()
    {
        Console.WriteLine("[ServerIdentity] Unloaded.");
    }
}
