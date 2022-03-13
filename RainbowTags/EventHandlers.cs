// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="FruitBoi &amp; Build &amp; iRebbok">
// Copyright (c) FruitBoi &amp; Build &amp; iRebbok. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace RainbowTags
{
    using Exiled.API.Extensions;
    using Exiled.Events.EventArgs;
    using RainbowTags.Components;
    using UnityEngine;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnChangingGroup(ChangingGroupEventArgs)"/>
        public void OnChangingGroup(ChangingGroupEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            bool hasColors = TryGetColors(ev.NewGroup?.GetKey(), out string[] colors);

            if (ev.NewGroup != null && ev.Player.Group == null && hasColors)
            {
                RainbowTagController controller = ev.Player.GameObject.AddComponent<RainbowTagController>();
                controller.Colors = colors;
                controller.Interval = plugin.Config.TagInterval;
                return;
            }

            if (!ev.Player.GameObject.TryGetComponent(out RainbowTagController rainbowTagController))
                return;

            if (hasColors)
                rainbowTagController.Colors = colors;
            else
                Object.Destroy(rainbowTagController);
        }

        private bool TryGetColors(string rank, out string[] availableColors)
        {
            availableColors = null;
            return !string.IsNullOrEmpty(rank) && plugin.Config.Sequences.TryGetValue(rank, out availableColors);
        }
    }
}