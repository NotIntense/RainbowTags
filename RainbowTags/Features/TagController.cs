using System;
using Exiled.API.Features;
using UnityEngine;

namespace RainbowTags;

public class TagController : MonoBehaviour
{
    private Player _player;
    private int _position;
    private string[] _colors;
    private float _timer;
    
    public string[] Colors
    {
        set
        {
            _colors = value ?? new []{ "" };
            _position = 0;
        }
    }

    public float interval;

    private void Awake() => _player = Player.Get(gameObject);

    private void Update()
    {
        _timer += Time.deltaTime;

        if (!(_timer >= interval)) 
            return;
        
        string text = RollNext();

        if (!string.IsNullOrEmpty(text))
            _player.RankColor = text;

        _timer = 0;
    }

    private string RollNext()
    {
        if (_colors.Length == 0)
            return string.Empty;

        _position = (_position + 1) % _colors.Length;
        return _colors[_position];
    }
}