using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas;
using Object = UnityEngine.Object;

namespace ARainbowTags
{
    [MetadataAttribute(
        "Rainbow Tags",
        "Adds rainbow scrolling colours to tags",
        "AgentBlackout", "Androxanik"
    )]

    public class RainbowTagMod : Mod<RainbowSettings>
    {
        public RainbowTagMod(ModLoadInfo data) : base(data)
        {
            
        }

        private RainbowTagController AddRainbowController(ReferenceHub references, RainbowSettings settings)
        {
            throw new NotImplementedException();
        }

        private void OnPlayerAwake(ReferenceHub player)
        {
            Debug("Loaded init");
            player.gameObject.AddComponent<RainbowTagController>();
        }

        protected override void Awake()
        {
            foreach(var player in PlayerManager.players)
            {
                player.gameObject.AddComponent<RainbowTagController>();
            }
        }

        protected override void Destroy()
        {
            foreach (var player in PlayerManager.players)
            {
                Object.Destroy(player.GetComponent<RainbowTagController>());
            }
        }
    }
}
