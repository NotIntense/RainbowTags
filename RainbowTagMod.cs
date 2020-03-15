using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;
using Harmony;
using UnityEngine;

namespace ARainbowTags
{
	public class RainbowTagMod : EXILED.Plugin
	{
		public static RainbowTagMod Instance;

		public const string kCfgPrefix = "rainbowtags_";

		public static void AddRainbowController(ReferenceHub player)
		{
			var component = player.GetComponent<RainbowTagController>();

			if (component != null) return;
			player.gameObject.AddComponent<RainbowTagController>();
		}


		public override void OnEnable()
		{
			if (!Config.GetBool(kCfgPrefix + "enable", true))
				return;

			RainbowTagController.interval = Config.GetFloat(kCfgPrefix + "taginterval", 0.5f);

			if (Config.GetBool(kCfgPrefix + "usecustomsequence"))
				RainbowTagController.Colors = Config.GetStringList(kCfgPrefix + "colorsequence").ToArray();
			
			Events.PlayerJoinEvent += OnPlayerJoinEvent;
			
			foreach (var player in PlayerManager.players)
			{
				AddRainbowController(player.GetPlayer());
			}
		}

		private void OnPlayerJoinEvent(PlayerJoinEvent ev)
		{
			AddRainbowController(ev.Player);
		}

		public override void OnDisable()
		{
			foreach (var player in PlayerManager.players)
			{
				UnityEngine.Object.Destroy(player.GetComponent<RainbowTagController>());
			}
		}

		public override void OnReload()
		{
			OnDisable();
		}

		public override string getName { get; } = "RainbowTags";
	}
}