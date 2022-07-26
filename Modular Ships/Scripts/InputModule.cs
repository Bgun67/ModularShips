using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//THis script will allow the buttons to be dropped onto the various input modules we created earlier
//so that the component actions can be linked to the ship's inputs
public class InputModule : MonoBehaviour, IDropHandler
{
	//To make th econnection, we need to know which input we want to connec the component's action to.
	//We'll have one type for each input we can connect to (Remember the ship code we did?)
	public enum ModuleType
	{
		Throttle,
		Horizontal,
		Vertical,
		Fire
	}

	//give a variable to assign the input type for this module in the inspector
	[SerializeField] ModuleType moduleType;
	//Reference to the ship to connect to
	Ship ship;

	void Awake()
	{
		ship = FindObjectOfType<Ship>();
	}


	//When we drop the button, we want it to snap to the input module
	public void OnDrop(PointerEventData eventData)
	{
		//Get which button we dropped
		GameObject selected = eventData.selectedObject;
		//Parent it to this module so that it lines up correctly
		selected.transform.SetParent(transform);
		//Get the component button so that we can use it later
		ComponentButton button = selected.GetComponent<ComponentButton>();

		//Now the connection stuff

		switch (moduleType)
		{
			case ModuleType.Throttle:
				//Connect to ship's throttle
				ship.throttleAction += button.boundAction;
				break;
			case ModuleType.Horizontal:
				ship.horizontalSteerAction += button.boundAction;
				break;
			case ModuleType.Vertical:
				ship.verticalSteerAction += button.boundAction;
				break;
			case ModuleType.Fire:
				ship.fireAction += button.boundAction;
				break;
			default:
				break;
		}
		//I'm not very happy with the way this part of the code turned out (I generally don't like switch 
		//statements because they're difficult to expand), if you have any suggestions I'm happy to hear them
	}
}
