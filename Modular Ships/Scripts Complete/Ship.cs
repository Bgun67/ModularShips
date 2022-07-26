using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ModularShipsComplete
{
	public class Ship : MonoBehaviour
	{
		//These are delegates that we will fire when the user presses on certain inputs,
		//We can add as many functions as we want to these actions
		[HideInInspector] public UnityAction<float> throttleAction;
		[HideInInspector] public UnityAction<float> verticalSteerAction;
		[HideInInspector] public UnityAction<float> horizontalSteerAction;
		[HideInInspector] public UnityAction<float> fireAction;
		[SerializeField] GameObject followCam;
		public Rigidbody rb;
		public bool active = false;

		void Awake()
		{
			rb = GetComponent<Rigidbody>();
		}

		// Start is called before the first frame update
		public void Activate()
		{
			followCam.SetActive(true);
			rb.isKinematic = false;
			transform.SetParent(null);
			foreach (ShipComponent component in GetComponentsInChildren<ShipComponent>())
			{
				component.Activate(this);
			}
			rb.centerOfMass = Vector3.zero;

			active = true;
		}
		public void Deactivate(Transform _transform)
		{
			followCam.SetActive(false);
			rb.isKinematic = true;
			transform.SetParent(_transform, false);
			transform.position = _transform.position;
			transform.rotation = _transform.rotation;

			active = false;
		}

		// Update is called once per frame
		void Update()
		{
			//Check for input
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			//This is short hand for saying the throttle is postive for q and negative for e
			float thrust = (Input.GetKey("q") ? 1f : 0f) + (Input.GetKey("e") ? -1f : 0f);

			float fire = Input.GetKey(KeyCode.Space) ? 1f : 0f;

			//Here we are invoking each action with the correct inputs, the question mark 
			//checks if the action is null before trying to invoke it
			if (active)
			{
					throttleAction?.Invoke(thrust);
				verticalSteerAction?.Invoke(vertical);
				horizontalSteerAction?.Invoke(horizontal);
				fireAction?.Invoke(fire);
			}
		}

	}
}
