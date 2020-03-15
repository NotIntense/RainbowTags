using System;
using System.Linq;

namespace ARainbowTags
{
	public static class Extensions
	{
		public static string GetGroupName(this UserGroup group)
			=> ServerStatic.GetPermissionsHandler().GetAllGroups().Where(p => p.Value == group).Select(p => p.Key)
				.FirstOrDefault();

		public static bool IsRainbowTagUser(this ReferenceHub hub)
		{
			string group = ServerStatic.GetPermissionsHandler().GetUserGroup(hub.characterClassManager.UserId)
				.GetGroupName();
			
			return !string.IsNullOrEmpty(group) && RainbowTagMod.ActiveRoles.Contains(group);
		}
	}
}