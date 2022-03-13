// -----------------------------------------------------------------------
// <copyright file="RainbowTagController.cs" company="FruitBoi &amp; Build &amp; iRebbok">
// Copyright (c) FruitBoi &amp; Build &amp; iRebbok. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace RainbowTags.Components
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using MEC;
    using UnityEngine;

    /// <summary>
    /// Controls the color rotation in tag sequences for players that have a rainbow tag.
    /// </summary>
    public sealed class RainbowTagController : MonoBehaviour
    {
        private Player player;
        private int position;
        private float interval;
        private float intervalInFrames;
        private string[] colors;
        private CoroutineHandle coroutineHandle;

        /// <summary>
        /// Gets or sets the colors in the sequence.
        /// </summary>
        public string[] Colors
        {
            get => colors;
            set
            {
                colors = value ?? Array.Empty<string>();
                position = 0;
            }
        }

        /// <summary>
        /// Gets or sets the time, in seconds, between switching to the next color in a sequence.
        /// </summary>
        public float Interval
        {
            get => interval;
            set
            {
                interval = value;
                intervalInFrames = value * 50f;
            }
        }

        private void Awake()
        {
            player = Player.Get(gameObject);
        }

        private void Start()
        {
            coroutineHandle = Timing.RunCoroutine(UpdateColor().CancelWith(player.GameObject).CancelWith(this));
        }

        private void OnDestroy()
        {
            Timing.KillCoroutines(coroutineHandle);
        }

        private string RollNext()
        {
            if (++position >= colors.Length)
                position = 0;

            return colors.Length != 0 ? colors[position] : string.Empty;
        }

        private IEnumerator<float> UpdateColor()
        {
            while (true)
            {
                for (int z = 0; z < intervalInFrames; z++)
                    yield return 0f;

                string nextColor = RollNext();
                if (string.IsNullOrEmpty(nextColor))
                {
                    Destroy(this);
                    break;
                }

                player.RankColor = nextColor;
            }
        }
    }
}