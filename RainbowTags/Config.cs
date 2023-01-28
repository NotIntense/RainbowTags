using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace RainbowTags
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Tags Configuration")]
        public float ColorInterval { get; set; } = 0.5f;
        public List<string> RanksWithRTags { get; set; } = new()
        {
            "owner",
            "moderator",
            "admin"
        };
        public string[] Sequences { get; set; } = new[]
        {
            "red",
            "orange",
            "yellow",
            "green",
            "blue_green",
            "magenta",
            "silver",
            "crimson"
        };
    }
}