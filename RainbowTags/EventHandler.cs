namespace RainbowTags
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using RainbowTags.Components;
    using UnityEngine;

    public static class EventHandler
    {
        public static void ChangingGroup(ChangingGroupEventArgs ev)
        {
            Timing.CallDelayed(0.2f, () =>
            {
                if (!ev.IsAllowed) return;
                if (!ev.Player.IsRainbowTagUser(out List<string> sequence))
                {
                    if (ev.Player.GameObject.TryGetComponent(out RainbowTagController controller))
                    {
                        Object.Destroy(controller);
                    }
                        
                    return;
                }

                AddRainbowController(ev.Player, sequence);
            });
        }

        private static void AddRainbowController(Player ply, List<string> sequence)
        {
            if (!ply.ReferenceHub.TryGetComponent(out RainbowTagController _))
            {
                ply.GameObject.AddComponent<RainbowTagController>().AwakeFunc(sequence, ply.ReferenceHub.serverRoles);
            }
        }
    }
}