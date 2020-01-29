using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ARainbowTags
{
    /// <summary>
    /// Represents a sequence of colour changes and an interval which to switch trough
    /// </summary>
    public class TagSequence
    {
        /// <summary>
        /// String list of colours to iterate through.
        /// </summary>
        [JsonProperty("colors")]
        public string[] Colors { get; set; }
        
        /// <summary>
        /// How long to spend on each colour before changing.
        /// </summary>
        [JsonProperty("interval")]
        public float Interval { get; set; }

        public TagSequence()
        {
            //Default parameters.
            Colors = new[] {
                "pink",
                "red",
                "brown",
                "silver",
                "light_green",
                "crimson",
                "cyan",
                "aqua",
                "deep_pink",
                "tomato",
                "yellow",
                "magenta",
                "blue_green",
                "orange",
                "lime",
                "green",
                "emerald",
                "carmine",
                "nickel",
                "mint",
                "army_green",
                "pumpkin"
            };
            Interval = 2f;

        }

    }
}
