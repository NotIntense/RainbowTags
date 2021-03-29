namespace RainbowTags.Components
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using UnityEngine;

    public class RainbowTagController : MonoBehaviour
    {
        private static readonly Config Config = RainbowTagMod.Instance.Config;

        private static string originalColor;
        
        private static int position;

        private static List<string> colors = new List<string>();
        
        private static Player player;

        private static CoroutineHandle coroutineHandle;

        private void Awake()
        {
            player = Player.Get(gameObject);
            Exiled.Events.Handlers.Player.ChangingGroup += OnChangingGroup;
        }

        private void OnDestroy()
        {
            Timing.KillCoroutines(coroutineHandle);
            Exiled.Events.Handlers.Player.ChangingGroup -= OnChangingGroup;
        }

        private static void OnChangingGroup(ChangingGroupEventArgs ev)
        {
            if (ev.Player != player)
                return;

            Timing.CallDelayed(0.2f, () =>
            {
                if (ev.NewGroup == ev.Player.Group)
                {
                    originalColor = player.ReferenceHub.serverRoles.NetworkMyColor;
                    Timing.KillCoroutines(coroutineHandle);
                    coroutineHandle = Timing.RunCoroutine(UpdateColor());
                }
            });
        }

        private static IEnumerator<float> UpdateColor()
        {
            if (!player.IsRainbowTagUser(out colors))
            {
                Log.Debug($"{player.Nickname} does not have a rainbow tag, skipping.", Config.EnableDebug);
                player.ReferenceHub.serverRoles.NetworkMyColor = originalColor;
                yield break;
            }

            Log.Debug($"Granted {player.Nickname} their rainbow tag with the following sequence: {string.Join(", ", colors)}", Config.EnableDebug);
            while (true)
            {
                yield return Timing.WaitForSeconds(Config.TagInterval);
                if (++position >= colors.Count)
                    position = 0;
                
                player.ReferenceHub.serverRoles.NetworkMyColor = colors[position];
            }
        }
    }
}