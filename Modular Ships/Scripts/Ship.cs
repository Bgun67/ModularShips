using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{

	public Rigidbody rb;
	//We want to check if the ship is being assemble or active to make sure we don't run
	//the components during assembly
	public bool active = false;
	//We also want a separate camera to follow the ship
	[SerializeField] GameObject followCam;

	//Now let's create some events that will fire when the user presses certain inputs
	//we want a throttle, vertical, horizontal, and fire commands that we can link our components to
	[HideInInspector] public UnityAction<float> throttleAction;
	[HideInInspector] public UnityAction<float> horizontalSteerAction;
	[HideInInspector] public UnityAction<float> verticalSteerAction;
	[HideInInspector] public UnityAction<float> fireAction;
	//I kind of cheat here with the fireaction by giving it a float value, I'm going to convert that to a 
	//bool later


	void Awake()
	{
		//assign the rigidbody
		rb = GetComponentInChildren<Rigidbody>();
	}

	//in the activate function, we want to prepare the ship for launch
	public void Activate()
	{
		//we'll activate the followCam (and Ideally deactivate the "creator cam"
		followCam.SetActive(true);
		//unlock the rigidbody
		rb.isKinematic = false;
		//remove the ship from the table
		transform.SetParent(null);
		//reset the center of mass for later
		rb.centerOfMass = Vector3.zero;
		//and activate the ship
		active = true;

		//activate all components
		foreach (ShipComponent component in GetComponentsInChildren<ShipComponent>())
		{
			component.Activate(this);
		}
	}
	//in the deactivate command we want to return the ship to its table
	public void Deactivate(Transform _transform)
	{
		followCam.SetActive(false);
		rb.isKinematic = true;
		//Set the parent as the table which we'll pass in from another script
		transform.SetParent(_transform);
		//Reset the position and rotation
		transform.position = _transform.position;
		transform.rotation = _transform.rotation;
		active = false;
	}

	// Finally in the update loop, we're going to check what inputs the player is pressing, then
	//invoke the relevant action
	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		//Here I'm just trying to get a positive value if the player presses q, or a negative value for e
		//The better way to do this is to set up a new axis in the input settings
		float thrust = (Input.GetKey("q") ? 1f : 0f) + (Input.GetKey("e") ? -1f : 0f);

		//The question mark means that if the statement is true, use the first value, else use the value 
		//afater the colon
		float fire = Input.GetKey("space") ? 1f : 0f;

		//now invoke our actions, if they exist
		if (active)
		{
			//the ? checks if the action is null
			throttleAction?.Invoke(thrust);
			horizontalSteerAction?.Invoke(horizontal);
			verticalSteerAction?.Invoke(vertical);
			fireAction?.Invoke(fire);
		}
	}

}
