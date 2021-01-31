using System.Collections.Generic;
using UnityEngine;

namespace RainbowTags
{
    public class RainbowTagController : MonoBehaviour
    {
        private ServerRoles _roles;
        private string _originalColor;

        private int _position;
        private float _nextCycle;

        public static List<string> Colors = new List<string>
        {
            "pink",
            "red",
            "brown",
            "silver",
            "light_green",
            "crimson",
            "cyan",
            "aqua",
            "deep_pink",
            "tomato",
            "yellow",
            "magenta",
            "blue_green",
            "orange",
            "lime",
            "green",
            "emerald",
            "carmine",
            "nickel",
            "mint",
            "army_green",
            "pumpkin"
        };

        private static float Interval { get; set; } = RainbowTagMod.RainbowTagRef.Config.TagInterval;

        public void Awake()
        {
            _roles = GetComponent<ServerRoles>();
            _nextCycle = Time.time;
            _originalColor = _roles.NetworkMyColor;
        }

        public void OnDestroy()
        {
            _roles.NetworkMyColor = _originalColor;
        }

        public void Update()
        {
            if (Time.time >= _nextCycle)
            {
                _nextCycle += Interval;
                _roles.NetworkMyColor = Colors[_position];

                if (++_position >= Colors.Count)
                    _position = 0;
            }
        }
    }
}