using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//We want to add some components to the ship that can be called by the actions we just created
//Each of these will inherit from this base class and will contain a list of the actions that can be added
public class ShipComponent : MonoBehaviour
{
	public string componentName;
	//I found that it was nice to be able to invert some of the steering if the component
	//was mounted on an opposite side
	public bool invert;
	protected Ship ship;
	//here's where all of the components actions are stored, we'll add them in the inherited components
	public List<UnityAction<float>> actions = new List<UnityAction<float>>();

	//Called when the player launches the ship, allows us to initialize all the components
	public virtual void Activate(Ship _ship)
	{
		//We want a reference to the ship so that we can access its rb, active setting etc
		ship = _ship;

	}
}
