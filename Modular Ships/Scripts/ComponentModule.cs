using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//We also want the components unwire when we drag them back to the components list
public class ComponentModule : MonoBehaviour, IDropHandler
{
	Ship ship;
	void Awake()
	{
		ship = FindObjectOfType<Ship>();
	}

	public void OnDrop(PointerEventData eventData)
	{
		//Same as the input module code
		GameObject selected = eventData.selectedObject;
		selected.transform.SetParent(transform);
		ComponentButton component = selected.GetComponent<ComponentButton>();

		//remove bound action from any and all ship actions
		ship.throttleAction -= component.boundAction;
		ship.horizontalSteerAction -= component.boundAction;
		ship.verticalSteerAction -= component.boundAction;
		ship.fireAction -= component.boundAction;
	}
}
