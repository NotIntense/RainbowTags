using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas;
using Atlas.Versioning;
using Harmony;
using UnityEngine;

using Object = UnityEngine.Object;

namespace ARainbowTags
{
    [MetadataAttribute(
        "Rainbow Tags",
        "Adds rainbow scrolling colours to tags",
        "AgentBlackout", "Androxanik"
    )]
    /*[GitHubVersionCheck("AgentBlackout", "ARainbowTags", "%{0}\\.%{1}\\.%{2}")]*/ //Temporarily disabled for testing. 
    public class RainbowTagMod : Mod<RainbowSettings>
    {
        private readonly HarmonyInstance _harmony; //Harmony for patching methods
        public static RainbowTagMod Instance;

        public RainbowTagMod(ModLoadInfo data) : base(data)
        {
            Instance = this;

            _harmony = HarmonyInstance.Create(Id);

            //Add a hook to the end of the authentication method so
            //we only try to add the rainbow controller after we're
            //sure that the user has a uid set.
            _harmony.Patch(
                AccessTools.Method(
                    typeof(ServerRoles), nameof(ServerRoles.CallCmdServerSignatureComplete)
                    ),
                null,
                new HarmonyMethod(
                    AccessTools.Method(typeof(RainbowTagMod), nameof(CallCmdServerSignatureCompletePostFix))
                    )
            );
        }

        /// <summary>
        /// Attempts to add a Rainbow controller to the game object. 
        /// </summary>
        /// <param name="playerObject">Player Object</param>
        private void AddRainbowController(GameObject playerObject)
        {
            var component = playerObject.GetComponent<RainbowTagController>();

            if (component != null) return;
            //Component already exists on the player, they already have a RainbowTag.

            var refHub = ReferenceHub.GetHub(playerObject);
            var sequence = Settings.GetTagSequence(refHub);

            if (sequence == null) return;

            playerObject.AddComponent<RainbowTagController>();
            playerObject.GetComponent<RainbowTagController>().Sequence = sequence;
        }

        /// <summary>
        /// Postfix which gets called after the authentication is complete.
        /// </summary>
        /// <param name="__instance">Instance of the ServerRoles component</param>
        private static void CallCmdServerSignatureCompletePostFix(ServerRoles __instance)
        {
            Instance.AddRainbowController(__instance.gameObject);
        }

        /// <summary>
        /// If the mod is loaded mid-game, will add rainbow controller to all in-game players.
        /// </summary>
        protected override void Start()
        {
            base.Start();

            foreach (var player in PlayerManager.players)
            {
                AddRainbowController(player);
            }
        }

        /// <summary>
        /// Gets called when the Mod unloads. Removes any already set components.
        /// </summary>
        protected override void Destroy()
        {
            foreach (var player in PlayerManager.players)
            {
                Object.Destroy(player.GetComponent<RainbowTagController>());
            }

            base.Destroy();
        }
    }
}
