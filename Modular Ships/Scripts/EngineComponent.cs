using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the turbofan engine component seen in the last video, let's add some functionality to it by 
//inheriting from our base ship component class
public class EngineComponent : ShipComponent
{
	//First we want to have our child engine mesh rotate to angle the engine's thrust
	//let's create a variable for that
	[SerializeField] GameObject engineMesh;
	//We also need to assign a transform that will act as our thrust vector, which will tell us the direction that
	//the force is being applied
	[SerializeField] Transform thrustVector;
	//Adjustment variables, I'm using a ship mass of 1000 kg, so this should be quite high, but we can adjust this 
	//later
	[SerializeField] float forceFactor = 10000f;
	//max Angle the engine should tilt
	[SerializeField] float steerAngle = 30f;

	//Factor in intial engine offset
	Quaternion startLocalRotation;
	float throttle;
	float steering;

	//Finally, we want to add our actions to the action list so that we can connect them up later in our 
	//ship wiring script
	public void Awake()
	{
		actions.Add(Throttle);
		actions.Add(Control);
	}
	public override void Activate(Ship _ship)
	{
		//Make sure the base ShipComponent class still runs
		base.Activate(_ship);
		startLocalRotation = engineMesh.transform.localRotation;
	}

	//these will set our throttle and steering based on inputs from the ship
	//I set them here so that I can do the actual movement in fixedupdate
	public void Throttle(float _throttle)
	{
		throttle = _throttle;
	}
	public void Control(float _steering)
	{
		steering = _steering;
	}

	void FixedUpdate()
	{
		//check if this component has no ship, or the ship is inactive
		if (!ship || !ship.active)
		{
			//skip the rest
			return;
		}

		//apply the thrust force at the position of the engine
		ship.rb.AddForceAtPosition(throttle * forceFactor * thrustVector.forward, thrustVector.position);
		//Also add a little debug line so I can show you where the force comes from
		Debug.DrawRay(thrustVector.position, thrustVector.forward, Color.red);

		//invert the direction of the engine's turn based on whether invert is checked or not
		float direction = invert ? -1f : 1f;
		//Rotate the enginemesh around the z axis, taking into account the intial rotation
		//order is important here!
		engineMesh.transform.localRotation = startLocalRotation * Quaternion.Euler(0f, 0f, direction * steering * steerAngle);
	}
}
