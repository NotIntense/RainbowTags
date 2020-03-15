using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ARainbowTags
{
	public class RainbowTagController : MonoBehaviour
	{
		private ServerRoles _roles;
		private string _originalColor;

		private int _position = 0;
		private float _nextCycle = 0f;

		public static string[] Colors =
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

		public static float interval { get; set; } = 0.5f;


		private void Start()
		{
			_roles = GetComponent<ServerRoles>();
			_nextCycle = Time.time;
			_originalColor = _roles.NetworkMyColor;
		}


		private void OnDisable()
		{
			_roles.NetworkMyColor = _originalColor;
		}


		private void Update()
		{
			if (Time.time < _nextCycle) return;
			_nextCycle += interval; 

			_roles.NetworkMyColor = Colors[_position];

			if (++_position >= Colors.Length) 
				_position = 0;
		}
	}
}