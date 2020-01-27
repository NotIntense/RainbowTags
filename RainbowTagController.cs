using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Settings.Models.Versioning;
using UnityEngine;

namespace ARainbowTags
{
    public class RainbowTagController : MonoBehaviour
    {
        private ServerRoles _roles;
        private string _originalColor;
        private const float _interval = .2f;

        private string[] _possibleColors =
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

        private int _position = 0;
        private float _nextCycle = 0f;

        private void Start()
        {
            _nextCycle = Time.time;
            _originalColor = _roles.NetworkMyColor;
        }

        private void OnEnable()
        {
            _roles = GetComponent<ServerRoles>();
        }

        private void OnDisable()
        {
            _roles.NetworkMyColor = _originalColor;
        }

        private void Update()
        {
            if (Time.time < _nextCycle) return;
            _nextCycle += _interval;

            _roles.NetworkMyColor = _possibleColors[_position];

            if (++_position >= _possibleColors.Length)
                _position = 0;

        }
    }
}
