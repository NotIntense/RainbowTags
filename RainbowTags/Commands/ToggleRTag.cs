using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
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
            Player player = Player.Get(playerCommandSender);

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
                    response = "Your rainbow tag has been disabled!";
                    return true;
                }
                else
                {
                    rainbowTag.enabled = true;
                    response = "Your rainbow tag has been enabled!";
                    return true;
                }
            }
            else
            {
                MainClass.Instance.OnChangingGroup(new ChangingGroupEventArgs(player, player.Group));
                response = "Your rainbow tag has been enabled!";
                return true;
            }
        }

        response = "You must be in-game to use this command!";
        return false;
    }
}
