namespace RainbowTags
{
    using System.Collections.Generic;
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public float TagInterval { get; set; } = 0.5f;

        public Dictionary<string, List<string>> Sequences { get; set; } = new Dictionary<string, List<string>>
        {
            ["owner"] = new List<string>
            {
                "red",
                "orange",
                "yellow",
                "green",
                "blue_green",
                "magenta"
            },
            ["admin"] = new List<string>
            {
                "green",
                "silver",
                "crimson",
            },
        };
    }
}