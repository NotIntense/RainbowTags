using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.Settings.Models.Versioning;
using UnityEngine;

namespace ARainbowTags
{
    /// <summary>
    /// Game component which handles changing of role colours automatically. 
    /// </summary>
    public class RainbowTagController : MonoBehaviour
    {
        private ServerRoles _roles;
        private string _originalColor; //Keep original colour to allow returning to after component unloads.

        private int _position = 0;
        private float _nextCycle = 0f;
        public TagSequence Sequence { get; internal set; }

        /// <summary>
        /// Runs before the first update.
        /// </summary>
        private void Start()
        {
            _roles = GetComponent<ServerRoles>();
            _nextCycle = Time.time;
            _originalColor = _roles.NetworkMyColor;
        }

        /// <summary>
        /// When component is destroyed.
        /// </summary>
        private void OnDisable()
        {
            _roles.NetworkMyColor = _originalColor;
        }

        /// <summary>
        /// Runs on every game frame.
        /// </summary>
        private void Update()
        {
            if (Time.time < _nextCycle) return; //Not time to update. 
            _nextCycle += Sequence.Interval; //Set the next run time.

            _roles.NetworkMyColor = Sequence.Colors[_position];
            
            if (++_position >= Sequence.Colors.Length) //Return to first position if overflowing.
                _position = 0;

        }
    }
}
