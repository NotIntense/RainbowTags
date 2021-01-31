using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace RainbowTags
{
    public class EventHandler
    {
        public void OnRoundStartEvent()
        {
            foreach (Player ply in Player.List)
            {
                if (!ply.IsRainbowTagUser())
                    continue;

                AddRainbowController(ply);
            }
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            if (!ev.Player.IsRainbowTagUser())
                return;

            AddRainbowController(ev.Player);
        }

        private static void AddRainbowController(Player ply)
        {
            if (ply.ReferenceHub.TryGetComponent(out RainbowTagController rainbowTagCtrl))
                return;

            ply.GameObject.AddComponent<RainbowTagController>();
        }
    }
}