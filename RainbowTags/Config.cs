using System.Collections.Generic;
using Exiled.API.Interfaces;

namespace RainbowTags
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool UseCustomSequence { get; set; } = false;
        public float TagInterval { get; set; } = 0.5f;
        public List<string> ActiveGroups { get; set; } = new List<string>() { "owner", "admin", "moderator" };
        public List<string> CustomSequence { get; set; } = new List<string>() { "red", "orange", "yellow", "green", "blue_green", "magenta" };
    }
}
