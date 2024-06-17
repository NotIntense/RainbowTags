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
    public bool SanitizeResponse { get; } = false;

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

            if (!MainClass.Instance.Config.RanksWithRTags.Contains(player.GroupName))
            {
                response = "You must be a member of a rank with a rainbow tag to use this command!";
                return false;
            }

            if (player.GameObject.TryGetComponent(out TagController rainbowTag))
            {
                if (rainbowTag.enabled)
                {
                    rainbowTag.enabled = false;
                    player.RankColor = player.Group.BadgeColor;
                    MainClass.PlayersWithoutRTags.Add(player);
                    response = "Your rainbow tag has been disabled!";
                    return true;
                }
                else
                {
                    rainbowTag.enabled = true;
                    MainClass.PlayersWithoutRTags.Remove(player);
                    response = "Your rainbow tag has been enabled!";
                    return true;
                }
            }
            else
            {
                //I dont think this would ever happen
                response = "Error, report to notintense on Discord!";
                return false;
            }
        }

        response = "You must be in-game to use this command!";
        return false;
    }
}
