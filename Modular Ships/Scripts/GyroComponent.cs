using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroComponent : ShipComponent
{

	[SerializeField] float forceFactor = 10f;

	void FixedUpdate()
	{
		if (ship && ship.active)
		{
			ship.rb.angularVelocity = Vector3.Lerp(
				ship.rb.angularVelocity,
				-Vector3.Cross(ship.transform.up, ship.transform.up - Vector3.up) * forceFactor,
				0.3f
			);
		}
	}

}
