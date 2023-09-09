using Nebuli.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace RainbowTags;

public class Config : IConfiguration
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    public bool GroupSpecificSequences { get; set; } = false;

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

    public Dictionary<string, List<string>> GroupSequences { get; set; } = new Dictionary<string, List<string>>
    {
    { "owner", new List<string>() { "red", "orange", "yellow", "green", "blue_green", "magenta", "silver", "crimson" } },
    { "moderator", new List<string>() { "red", "orange", "yellow", "green", "blue_green", "magenta", "silver", "crimson" } },
    { "admin", new List<string>() { "red", "orange", "yellow", "green", "blue_green", "magenta", "silver", "crimson" } }
    };

}