using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModularShipsComplete
{
	public class ComponentModule : MonoBehaviour, IDropHandler
	{
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

			ship.throttleAction -= component.boundAction;
			ship.verticalSteerAction -= component.boundAction;
			ship.horizontalSteerAction -= component.boundAction;
			ship.throttleAction -= component.boundAction;
		}
	}
}
