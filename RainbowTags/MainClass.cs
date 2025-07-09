using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using static RainbowTags.Extensions;

namespace RainbowTags;

public class MainClass : Plugin<Config>
{
    public static MainClass Instance { get; private set; }
    public override string Author => "NotIntense";
    public override string Name => "RainbowTags";
    public override string Prefix => "RainbowTags";
    public override Version Version { get; } = new(3, 0, 1);
    public override Version RequiredExiledVersion { get; } = new(9, 0, 0);
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

    public void OnChangingGroup(ChangingGroupEventArgs ev)
    {
        if (PlayersWithoutRTags.Contains(ev.Player)) 
            return;
            
        string[] colors = null;

        if (Config!.GroupSpecificSequences)
        {
            if (!TryGetCustomColors(GetGroupKey(ev.NewGroup), out colors))
                return;
        }
        else
        {
            if (!TryGetColors(GetGroupKey(ev.NewGroup), out colors))
                return;
        }
        
        if (colors == null)
        {
            //Something went wrong
            Log.Error($"Failed to assign RainbowTag to {ev.Player}!\n NewGroup Null? : {false}\n Could get colors? : {(Config.GroupSpecificSequences ? TryGetCustomColors(GetGroupKey(ev.NewGroup), out _) : TryGetColors(GetGroupKey(ev.NewGroup), out _))}");
            UnityEngine.Object.Destroy(ev.Player.GameObject.GetComponent<TagController>());
            return;
        }

        Log.Debug("RainbowTags: Added to " + ev.Player.Nickname);

        if (ev.Player.GameObject.TryGetComponent(out TagController controller))
            UnityEngine.Object.Destroy(controller);

        var rtController = ev.Player.GameObject.AddComponent<TagController>();
        rtController.Colors = colors;
        rtController.interval = Config.ColorInterval;
    }
}
        