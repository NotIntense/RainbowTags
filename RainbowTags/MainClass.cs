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
    public override Version Version { get; } = new(2, 0, 5);
    public override Version RequiredExiledVersion { get; } = new(8, 0, 0);

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
        return thisGroup == otherGroup;
    }

    private string GetGroupKey(UserGroup group)
    {
        if (group == null)
            return string.Empty;

        return ServerStatic.PermissionsHandler.Groups.FirstOrDefault(g => EqualsTo(g.Value, group)).Key ?? string.Empty;
    }

    public void OnChangingGroup(ChangingGroupEventArgs ev)
    {
        bool isRainbow = false;
        string[] colors = null;
        string groupKey = GetGroupKey(ev.NewGroup);

        if (Config.GroupSpecificSequences)
        {
            isRainbow = TryGetCustomColors(groupKey, out colors);
        }
        else
        {
            isRainbow = TryGetColors(groupKey, out colors);
        }


        if (isRainbow)
        {
            Log.Debug("RainbowTags: Added to " + ev.Player.Nickname);

            if (ev.Player.GameObject.TryGetComponent(out TagController controller))
                Object.Destroy(controller);

            TagController rtController = ev.Player.GameObject.AddComponent<TagController>();
            rtController.Colors = colors;
            rtController.Interval = Config.ColorInterval;
        }
        else
        {
            if (ev.Player.GameObject.TryGetComponent(out TagController controller))
            {
                Log.Debug($"RainbowTags: Removing from {ev.Player.Nickname} because their new group '{groupKey}' is not in the list.");
                Object.Destroy(controller);
            }
        }
    }
}
