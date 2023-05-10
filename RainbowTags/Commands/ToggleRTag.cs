using System;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;

namespace RainbowTags.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ToggleRTag : ICommand
{
    public string Command { get; } = "togglerainbowtag";
    public string[] Aliases { get; } = { "trt" };
    public string Description { get; } = "Toggles your rainbow tag on or off";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (sender is PlayerCommandSender playerCommandSender)
        {
            var player = Player.Get(playerCommandSender);
            if (player == null)
            {
                response = "You must be in-game to use this command!";
                return false;
            }

            if (player.GameObject.TryGetComponent(out TagController rainbowTag))
            {
                player.RemoveComponent(rainbowTag);
                MainClass.PlayersWithoutRTags.Add(player);
                response = "Your rainbow tag has been disabled!";
                return true;
            }

            player.GameObject.AddComponent<TagController>();
            MainClass.PlayersWithoutRTags.Remove(player);
            response = "Your rainbow tag has been enabled!";
            return true;
        }

        response = "You must be in-game to use this command!";
        return false;
    }
}