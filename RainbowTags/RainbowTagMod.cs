using System;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using RainbowTags.Components;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;

namespace RainbowTags
{
    public class RainbowTagMod : Plugin<Config>
    {
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 5);

        public override void OnEnabled()
        {
            Player.ChangingGroup += OnPlayerChangingGroup;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.ChangingGroup -= OnPlayerChangingGroup;
            base.OnDisabled();
        }

        private bool TryGetColors(string rank, out string[] availableColors)
        {
            availableColors = null;
            return !string.IsNullOrEmpty(rank) && Config.Sequences.TryGetValue(rank, out availableColors);
        }

        // https://github.com/Exiled-Team/EXILED/pull/470/files#diff-747f05669db353bffb9b1e84a59fe28547e078c59575089c2494cfb83c6b376e
        private bool EqualsTo(UserGroup @this, UserGroup other)
            => @this.BadgeColor == other.BadgeColor
               && @this.BadgeText == other.BadgeText
               && @this.Permissions == other.Permissions
               && @this.Cover == other.Cover
               && @this.HiddenByDefault == other.HiddenByDefault
               && @this.Shared == other.Shared
               && @this.KickPower == other.KickPower
               && @this.RequiredKickPower == other.RequiredKickPower;

        // Remind me to include it into Exiled
        private string GetGroupKey(UserGroup group)
        { 
            if (group == null)
                return string.Empty;

            return ServerStatic.GetPermissionsHandler()._groups.FirstOrDefault(g => EqualsTo(g.Value, group)).Key ??
                   string.Empty;
        }

        private void OnPlayerChangingGroup(ChangingGroupEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            var hasColors = TryGetColors(GetGroupKey(ev.NewGroup), out var colors);

            // Happens on first join -> add controller if the player has colors
            if (ev.NewGroup != null && ev.Player.Group == null && hasColors)
            {
                var controller = ev.Player.GameObject.AddComponent<RainbowTagController>();
                controller.Colors = colors;
                controller.Interval = Config.TagInterval;
            }
            // Happens on group update -> update group colors or remove the controller
            else
            {
                if (hasColors)
                    ev.Player.GameObject.GetComponent<RainbowTagController>().Colors = colors;
                else
                    GameObject.Destroy(ev.Player.GameObject.GetComponent<RainbowTagController>());
            }
        }
    }
}