using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Object = UnityEngine.Object;

namespace RainbowTags;

public class MainClass : Plugin<Config>
{
    public static MainClass Instance { get; set; }
    public override string Author => "NotIntense : (Ported From Build & xNexus-ACS)";
    public override string Name => "RainbowTags";
    public override string Prefix => "RainbowTags";
    public override Version Version { get; } = new(1, 0, 0);
    public override Version RequiredExiledVersion { get; } = new(7, 0, 0);

    public static List<Player> PlayersWithoutRTags { get; } = new();

    public override void OnEnabled()
    {
        Instance = this;
        Exiled.Events.Handlers.Player.ChangingGroup += OnChangingGroup;
        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.ChangingGroup -= OnChangingGroup;
        base.OnDisabled();
        Instance = null;
    }

    private bool TryGetColors(string rank, out string[] availableColors)
    {
        availableColors = Config.Sequences;
        return !string.IsNullOrEmpty(rank) && Config.RanksWithRTags.Contains(rank);
    }

    private bool TryGetCustomColors(string rank, out string[] availableColors)
    {
        if (Config.GroupSequences[rank] == null)
        {
            availableColors = null;
            return false;
        }
        availableColors = Config.GroupSequences[rank].ToArray();
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
        if (Config.GroupSpecificSequences)
        {
            if (!PlayersWithoutRTags.Contains(ev.Player) && ev.NewGroup != null && ev.Player.Group == null && TryGetCustomColors(GetGroupKey(ev.NewGroup), out var colors))
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
        else
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
}