namespace RainbowTags
{
    using System.Collections.Generic;
    using Exiled.API.Features;

    public static class Extensions
    {
        public static bool IsRainbowTagUser(this Player ply, out List<string> sequence)
        {
            sequence = null;
            if (ply.Group == null)
                return false;
            
            string group = ply.Group.BadgeText;
            return !string.IsNullOrEmpty(group) && RainbowTagMod.Instance.Config.Sequences.TryGetValue(group, out sequence);
        }
    }
}