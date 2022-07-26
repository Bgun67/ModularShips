using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularShipsComplete
{
	public class GunComponent : ShipComponent
	{
		[SerializeField] GameObject gunMesh;
		//This will be the direction along which our force will be applied, it changes as we turn the engine
		[SerializeField] Transform shotSpawn;
		[SerializeField] GameObject bulletPrefab;
		[SerializeField] float steerAngle = 60f;
		Quaternion startLocalRotation;
		float steering;
		bool firing;
		float lastFireTime;
		[SerializeField] float fireRate = 0.2f;

		void Awake()
		{
			actions.Add(Control);
			actions.Add(Fire);
		}

		public override void Activate(Ship _ship)
		{
			base.Activate(_ship);
			//Make sure any starting offset is taken into account
			startLocalRotation = gunMesh.transform.localRotation;
		}


		//THis will handle any steering inputs (Forward back left right)
		public void Control(float controlAmount)
		{
			steering = controlAmount;
		}
		public void Fire(float on)
		{
			firing = on > 0f;

		}

		void FixedUpdate()
		{
			if (!ship || !ship.active)
			{
				return;
			}

			float direction = invert ? -1f : 1f;

			//Turn the engine based on control input
			gunMesh.transform.localRotation = startLocalRotation * Quaternion.Euler(0f, 0f, direction * steering * steerAngle);
			if (firing)
			{
				if (Time.time > lastFireTime + fireRate)
				{
					lastFireTime = Time.time;
					GameObject bullet = Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
					bullet.GetComponent<Rigidbody>().AddForce(shotSpawn.forward * 10f);
					Destroy(bullet, 3f);
				}
			}
		}

	}
}