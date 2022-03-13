// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="FruitBoi &amp; Build &amp; iRebbok">
// Copyright (c) FruitBoi &amp; Build &amp; iRebbok. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace RainbowTags
{
    using System;
    using Exiled.API.Features;
    using PlayerHandlers = Exiled.Events.Handlers.Player;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        private EventHandlers eventHandlers;

        /// <inheritdoc/>
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            eventHandlers = new EventHandlers(this);
            PlayerHandlers.ChangingGroup += eventHandlers.OnChangingGroup;
            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            PlayerHandlers.ChangingGroup -= eventHandlers.OnChangingGroup;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}