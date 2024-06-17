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
    public override string Author => "NotIntense";
    public override string Name => "RainbowTags";
    public override string Prefix => "RainbowTags";
    public override Version Version { get; } = new(2, 0, 2);
    public override Version RequiredExiledVersion { get; } = new(8, 0, 0);
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
        if (string.IsNullOrEmpty(rank) || !Config.GroupSequences.ContainsKey(rank))
        {
            Log.Error($"A player with the rank '{rank}' does not have a custom group sequence! They will be given the default sequence colors.");
            availableColors = Config.Sequences;
            return false;
        }

        availableColors = Config.GroupSequences[rank].ToArray();
        return Config.RanksWithRTags.Contains(rank);
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

        return ServerStatic.PermissionsHandler._groups.FirstOrDefault(g => EqualsTo(g.Value, group)).Key ?? string.Empty;
    }

    private void OnChangingGroup(ChangingGroupEventArgs ev)
    {
        AssignGroupColor(ev, Config.GroupSpecificSequences);
    }

    private void AssignGroupColor(ChangingGroupEventArgs ev, bool groupSequence)
    {
        string[] colors;

        if (groupSequence)
        {                                                                                                           //Using custom try get colors
            if (!PlayersWithoutRTags.Contains(ev.Player) && ev.NewGroup != null && ev.Player.Group == null)
            {
                TryGetCustomColors(GetGroupKey(ev.NewGroup), out colors);

                Log.Debug("RainbowTags: Added to " + ev.Player.Nickname);
                TagController rtController = ev.Player.GameObject.AddComponent<TagController>();
                rtController.Colors = colors;
                rtController.Interval = Config.ColorInterval;
                return;
            }

            //Something went wrong
            Log.Error($"Failed to assign RainbowTag to {ev.Player}!\n NewGroup Null? : {ev.NewGroup == null}\n PlayerGroup Null? : {ev.Player?.Group == null}\n Could get colors? : {TryGetCustomColors(GetGroupKey(ev.NewGroup), out _)}");
            Object.Destroy(ev.Player.GameObject.GetComponent<TagController>());
            return;
        }
        else
        {
                                                                                                              //Using normal try get colors
            if (!PlayersWithoutRTags.Contains(ev.Player) && ev.NewGroup != null && ev.Player.Group == null && TryGetColors(GetGroupKey(ev.NewGroup), out colors))
            {
                Log.Debug("RainbowTags: Added to " + ev.Player.Nickname);
                var rtController = ev.Player.GameObject.AddComponent<TagController>();
                rtController.Colors = colors;
                rtController.Interval = Config.ColorInterval;
                return;
            }

            //Something went wrong
            Log.Error($"Failed to assign RainbowTag to {ev.Player}!\n NewGroup Null? : {ev.NewGroup == null}\n PlayerGroup Null? : {ev.Player?.Group == null}\n Could get colors? : {TryGetCustomColors(GetGroupKey(ev.NewGroup), out _)}");
            Object.Destroy(ev.Player.GameObject.GetComponent<TagController>());
            return;
        }
    }
}