using System;
using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace RainbowTags;

public class TagController : MonoBehaviour
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
        _coroutineHandle = Timing.RunCoroutine(UpdateColor().CancelWith(_player.GameObject));
    }
    private void OnDestroy()
    {
        Timing.KillCoroutines(_coroutineHandle);
    }
    private string RollNext()
    {
        var num = _position + 1;
        _position = num;
        
        if (num >= _colors.Length)
            _position = 0;
        
        if (_colors.Length == 0)
            return string.Empty;
        
        return _colors[_position];
    }
    private IEnumerator<float> UpdateColor()
    {
        for (; ; )
        {
            int num;
            for (int z = 0; z < _intervalInFrames; z = num + 1)
            {
                yield return 0f;
                num = z;
            }
            string text = RollNext();
            if (string.IsNullOrEmpty(text))
            {
                break;
            }
            _player.RankColor = text;
        }
        Destroy(this);
    }
}