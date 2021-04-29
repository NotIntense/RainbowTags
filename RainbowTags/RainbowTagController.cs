using System;
using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace RainbowTags.Components
{
    public sealed class RainbowTagController : MonoBehaviour
    {
        private Player _player;
        private int _position;
        private float _interval;
        private int _intervalInFrames;
        private string[] _colors;
        private CoroutineHandle _coroutineHandle;

        public string[] Colors
        {
            get => _colors ?? Array.Empty<string>();
            set
            {
                _colors = value ?? Array.Empty<string>();
                _position = 0;
            }
        }

        public float Interval
        {
            get => _interval;
            set
            {
                _interval = value;
                _intervalInFrames = Mathf.CeilToInt(value) * 50;
            }
        }

        private void Awake()
        {
            _player = Player.Get(gameObject);
        }

        private void Start()
        {
            _coroutineHandle = Timing.RunCoroutine(UpdateColor().CancelWith(_player.GameObject).CancelWith(this));
        }

        private void OnDestroy()
        {
            Timing.KillCoroutines(_coroutineHandle);
        }

        private string RollNext()
        {
            if (++_position >= _colors.Length)
                _position = 0;

            return _colors.Length != 0 ? _colors[_position] : string.Empty;
        }
        
        private IEnumerator<float> UpdateColor()
        {
            while (true)
            {
                for (var z = 0; z < _intervalInFrames; z++)
                    yield return 0f;

                var nextColor = RollNext();
                if (string.IsNullOrEmpty(nextColor))
                {
                    Destroy(this);
                    break;
                }

                _player.RankColor = nextColor;
            }
        }
    }
}