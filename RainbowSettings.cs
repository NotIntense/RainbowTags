using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Settings.Models;
using Newtonsoft.Json;

namespace ARainbowTags
{
    public class RainbowSettings : ModSettings
    {
        [JsonProperty("template_map")]
        public Dictionary<string, TagSequence> Templates { get; set; }

        [JsonProperty("uid_map")]
        public Dictionary<string, string> UidMap { get; set; }

        public RainbowSettings()
        {
            Templates = new Dictionary<string, TagSequence>();
            UidMap = new Dictionary<string, string>();
        }

        public TagSequence GetTagSequence(string uid)
        {
            if (UidMap.TryGetValue(uid, out var templateName))
            {
                RainbowTagMod.Instance.Warn($"{uid} has rainbow template \"{templateName}\"");
                if (Templates.TryGetValue(templateName, out var sequence))
                    return sequence;

                RainbowTagMod.Instance.Warn($"Template {templateName} which belongs to {uid} is invalid");
            }

            return null;
        }

        public TagSequence GetTagSequence(ReferenceHub hub)
        {
            return GetTagSequence(hub.characterClassManager.UserId ?? "uid_unassigned");
        }
    }
}
