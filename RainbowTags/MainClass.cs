using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Object = UnityEngine.Object;

namespace RainbowTags;

public class MainClass : Plugin<Config>
{
    public override string Author => "xNexusACS (Ported From Build)";
    public override string Name => "RainbowTags";
    public override string Prefix => "RainbowTags";
    public override Version Version { get; } = new(4, 1, 0);
    public override Version RequiredExiledVersion { get; } = new(7, 0, 0);

    public static List<Player> PlayersWithoutRTags { get; } = new();

    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.ChangingGroup += OnChangingGroup;
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.ChangingGroup -= OnChangingGroup;
        base.OnDisabled();
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
    private void OnChangingGroup(ChangingGroupEventArgs ev)
    {
        if (!PlayersWithoutRTags.Contains(ev.Player) && ev.NewGroup != null && ev.Player.Group == null && TryGetColors(GetGroupKey(ev.NewGroup), out var colors))
        {
            Log.Debug("RainbowTags: Added to " + ev.Player.Nickname);
            var rtController = ev.Player.GameObject.AddComponent<TagController>();
            rtController.Colors = colors;
            rtController.Interval = Config.ColorInterval;
            return;
        }
        if (TryGetColors(GetGroupKey(ev.NewGroup), out colors))
        {
            ev.Player.GameObject.GetComponent<TagController>().Colors = colors;
            return;
        }
        Object.Destroy(ev.Player.GameObject.GetComponent<TagController>());
    }
}