using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelComponent : ShipComponent
{

	[SerializeField] GameObject wheelMesh;
	[SerializeField] WheelCollider wheelCollider;
	[SerializeField] float maxSteer = 30f;
	Quaternion originalWheelPosition;

	[SerializeField] float forceFactor = 100f;

	void Awake()
	{
		actions.Add(Throttle);
		actions.Add(Control);
	}
	public override void Activate(Ship _ship)
	{
		base.Activate(_ship);
		wheelCollider.enabled = true;
		originalWheelPosition = transform.localRotation;
	}

	public void Throttle(float throttleAmount)
	{
		//Apply a force with the current throttle, throttle will be a value between 0 and 1
		wheelCollider.motorTorque = forceFactor * throttleAmount * (invert ? -1f : 1f);
	}

	public void Control(float controlAmount)
	{
		wheelCollider.steerAngle = controlAmount * maxSteer * (invert ? -1f : 1f);
	}

	public void Update()
	{
		if (!ship || !ship.active)
		{
			return;
		}
		Vector3 wheelPosition = Vector3.zero;
		Quaternion wheelRotation = Quaternion.identity;
		wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);
		wheelMesh.transform.position = wheelPosition;
		wheelMesh.transform.rotation = wheelRotation * originalWheelPosition;

	}

}
