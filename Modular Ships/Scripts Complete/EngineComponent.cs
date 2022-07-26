using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularShipsComplete
{
	public class EngineComponent : ShipComponent
	{
		[SerializeField] GameObject engineMesh;
		//This will be the direction along which our force will be applied, it changes as we turn the engine
		[SerializeField] Transform thrustVector;
		[SerializeField] float forceFactor = 100f;
		[SerializeField] float steerAngle = 30f;
		Quaternion startLocalRotation;
		float throttle;
		float steering;

		void Awake()
		{
			actions.Add(Throttle);
			actions.Add(Control);
		}
		public override void Activate(Ship _ship)
		{
			base.Activate(_ship);
			//Make sure any starting offset is taken into account
			startLocalRotation = engineMesh.transform.localRotation;
		}
		//This will handle any throttle inputs
		public void Throttle(float _throttle)
		{
			throttle = _throttle;

		}

		//THis will handle any steering inputs (Forward back left right)
		public void Control(float controlAmount)
		{
			steering = controlAmount;
		}

		void FixedUpdate()
		{
			if (!ship || !ship.active)
			{
				return;
			}
			//Apply a force with the current throttle, throttle will be a value between 0 and 1
			ship.rb.AddForceAtPosition(thrustVector.forward * forceFactor * throttle, thrustVector.position);
			Debug.DrawRay(thrustVector.position, thrustVector.forward, Color.red);

			float direction = invert ? -1f : 1f;
			
			//Turn the engine based on control input
			engineMesh.transform.localRotation = startLocalRotation * Quaternion.Euler(0f, 0f, direction * steering * steerAngle);

		}

	}
}
