// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="FruitBoi &amp; Build &amp; iRebbok">
// Copyright (c) FruitBoi &amp; Build &amp; iRebbok. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace RainbowTags
{
    using System.Collections.Generic;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public sealed class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the time, in seconds, between switching to the next color in a sequence.
        /// </summary>
        public float TagInterval { get; set; } = 0.5f;

        /// <summary>
        /// Gets or sets a collection of group names with their respective color sequences.
        /// </summary>
        public Dictionary<string, string[]> Sequences { get; set; } = new Dictionary<string, string[]>
        {
            ["owner"] = new[]
            {
                "red",
                "orange",
                "yellow",
                "green",
                "blue_green",
                "magenta",
            },
            ["admin"] = new[]
            {
                "green",
                "silver",
                "crimson",
            },
        };
    }
}