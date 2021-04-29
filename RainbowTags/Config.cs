using System.Collections.Generic;
using Exiled.API.Interfaces;

namespace RainbowTags
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool EnableDebug { get; set; } = false;
        public float TagInterval { get; set; } = 0.5f;

        public Dictionary<string, string[]> Sequences { get; set; } = new Dictionary<string, string[]>
        {
            ["Owner"] = new[]
            {
                "red",
                "orange",
                "yellow",
                "green",
                "blue_green",
                "magenta"
            },
            ["Admin"] = new[]
            {
                "green",
                "silver",
                "crimson",
            }
        };
    }
}