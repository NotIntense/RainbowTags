namespace RainbowTags
{
    using System.Collections.Generic;
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool EnableDebug { get; set; } = false;

        public float TagInterval { get; set; } = 0.5f;

        public Dictionary<string, List<string>> Sequences { get; set; } = new Dictionary<string, List<string>>
        {
            ["Owner"] = new List<string>
            {
                "red",
                "orange",
                "yellow",
                "green",
                "blue_green",
                "magenta"
            },
            ["Admin"] = new List<string>
            {
                "green",
                "silver",
                "crimson",
            },
        };
    }
}