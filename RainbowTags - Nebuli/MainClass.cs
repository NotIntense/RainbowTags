using InventorySystem.Items.Usables;
using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace RainbowTags;

public class MainClass : Plugin<Config>
{
    public static MainClass Instance { get; set; }
    public override string Creator => "NotIntense : (Ported From Build & xNexus-ACS)";
    public override string Name => "RainbowTags";
    public override Version Version { get; } = new(1, 0, 2);
    public override Version NebuliVersion { get; } = new(0, 0, 0);

    public static List<NebuliPlayer> PlayersWithoutRTags { get; } = new();

    public override void OnEnabled()
    {
        Instance = this;
        PlayerHandlers.UserChangingUserGroup += OnChangingGroup;
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        PlayerHandlers.UserChangingUserGroup -= OnChangingGroup;
        base.OnDisabled();
        Instance = null;
    }

    private bool TryGetColors(string rank, out string[] availableColors)
    {
        availableColors = Config.Sequences;
        return !string.IsNullOrEmpty(rank) && Config.RanksWithRTags.Contains(rank);
    }
    private bool EqualsTo(UserGroup thisGroup, UserGroup otherGroup)
    {
        return thisGroup.BadgeColor == otherGroup.BadgeColor && thisGroup.BadgeText == otherGroup.BadgeText &&
               thisGroup.Permissions == otherGroup.Permissions && thisGroup.Cover == otherGroup.Cover &&
               thisGroup.HiddenByDefault == otherGroup.HiddenByDefault && thisGroup.Shared == otherGroup.Shared &&
               thisGroup.KickPower == otherGroup.KickPower &&
               thisGroup.RequiredKickPower == otherGroup.RequiredKickPower;
    }
    private string GetGroupKey(UserGroup group)
    {
        if (group == null)
            return string.Empty;

        return ServerStatic.PermissionsHandler._groups.FirstOrDefault(g => EqualsTo(g.Value, group)).Key ??
               string.Empty;
    }
    private void OnChangingGroup(PlayerChangingUserGroupEvent ev)
    {
        if (!PlayersWithoutRTags.Contains(ev.Player) && ev.Group != null && ev.Player.Group == null && TryGetColors(GetGroupKey(ev.Group), out var colors))
        {
            Log.Debug("RainbowTags: Added to " + ev.Player.Nickname);
            var rtController = ev.Player.GameObject.AddComponent<TagController>();
            rtController.Colors = colors;
            rtController.Interval = Config.ColorInterval;
            return;
        }
        if (TryGetColors(GetGroupKey(ev.Group), out colors))
        {
            ev.Player.GameObject.GetComponent<TagController>().Colors = colors;
            return;
        }
        Object.Destroy(ev.Player.GameObject.GetComponent<TagController>());
    }
}