namespace RainbowTags
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features;

    public static class Extensions
    {
        public static bool IsRainbowTagUser(this Player ply, out List<string> sequence)
        {
            string group = ply.Group.GetGroupName();

            sequence = null;
            return !string.IsNullOrEmpty(group) &&
                   RainbowTagMod.Instance.Config.Sequences.TryGetValue(group, out sequence);
        }

        private static string GetGroupName(this UserGroup group) => ServerStatic.GetPermissionsHandler().GetAllGroups()
            .FirstOrDefault(p => p.Value == group).Key;
    }
}