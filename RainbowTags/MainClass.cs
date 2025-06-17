using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RainbowTags;

public class MainClass : Plugin<Config>
{
    public static MainClass Instance { get; set; }
    public override string Author => "NotIntense";
    public override string Name => "RainbowTags";
    public override string Prefix => "RainbowTags";
    public override Version Version { get; } = new(2, 0, 5);
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

        return ServerStatic.PermissionsHandler.Groups.FirstOrDefault(g => EqualsTo(g.Value, group)).Key ?? string.Empty;
    }

    public void OnChangingGroup(ChangingGroupEventArgs ev)
    {
        string[] colors;

        if (!PlayersWithoutRTags.Contains(ev.Player) && ev.NewGroup != null)
        {
            if (Config.GroupSpecificSequences)
                TryGetCustomColors(GetGroupKey(ev.NewGroup), out colors);
            else
                TryGetColors(GetGroupKey(ev.NewGroup), out colors);

            if (colors == null)
            {
                //Something went wrong
                Log.Error($"Failed to assign RainbowTag to {ev.Player}!\n NewGroup Null? : {ev.NewGroup == null}\n PlayerGroup Null? : {ev.Player?.Group == null}\n Could get colors? : {(Config.GroupSpecificSequences ? TryGetCustomColors(GetGroupKey(ev.NewGroup), out _) : TryGetColors(GetGroupKey(ev.NewGroup), out _))}");
                Object.Destroy(ev.Player.GameObject.GetComponent<TagController>());
                return;
            }

            Log.Debug("RainbowTags: Added to " + ev.Player.Nickname);

            if (ev.Player.GameObject.TryGetComponent(out TagController controller))
                Object.Destroy(controller);

            TagController rtController = ev.Player.GameObject.AddComponent<TagController>();
            rtController.Colors = colors;
            rtController.Interval = Config.ColorInterval;
            return;
        }
    }
}
