using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace ModularShipsComplete
{
	public class InputModule : MonoBehaviour, IDropHandler
	{
		public enum ModuleType
		{
			Throttle,
			Vertical,
			Horizontal,
			Fire
		}
		[SerializeField] ModuleType moduleType;
		Ship ship;

		void Awake()
		{
			ship = FindObjectOfType<Ship>();
		}

		public void OnDrop(PointerEventData eventData)
		{
			GameObject selected = eventData.selectedObject;
			selected.transform.SetParent(transform);
			ComponentButton component = selected.GetComponent<ComponentButton>();

			switch (moduleType)
			{
				case ModuleType.Throttle:
					ship.throttleAction += component.boundAction;
					break;
				case ModuleType.Vertical:
					ship.verticalSteerAction += component.boundAction;
					break;
				case ModuleType.Horizontal:
					ship.horizontalSteerAction += component.boundAction;
					break;
				case ModuleType.Fire:
					ship.fireAction += component.boundAction;
					break;
			}
		}
	}
}
