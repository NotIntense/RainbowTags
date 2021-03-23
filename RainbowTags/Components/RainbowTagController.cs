namespace RainbowTags.Components
{
    using System.Collections.Generic;
    using UnityEngine;

    public class RainbowTagController : MonoBehaviour
    {
        private static ServerRoles roles;
        private static string originalColor;

        private static int position;
        private static float nextCycle;

        private static List<string> colors = new List<string>();

        private static float Interval { get; set; } = RainbowTagMod.Instance.Config.TagInterval;

        internal void AwakeFunc(List<string> sequence, ServerRoles serverRoles)
        {
            colors = sequence;
            roles = serverRoles;
            originalColor = roles.NetworkMyColor;
            nextCycle = Time.time;
        }

        private void OnDestroy()
        {
            roles.NetworkMyColor = originalColor;
        }

        private void Update()
        {
            if (Time.time >= nextCycle)
            {
                nextCycle += Interval;
                roles.NetworkMyColor = colors[position];

                if (++position >= colors.Count)
                    position = 0;
            }
        }
    }
}