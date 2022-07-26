using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ModularShipsComplete
{
	public class ShipComponent : MonoBehaviour
	{
		public string componentName;
		public bool invert;
		protected Ship ship;
		public List<UnityAction<float>> actions = new List<UnityAction<float>>();

		public virtual void Activate(Ship _ship)
		{
			ship = _ship;
		}

	}
}
