using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularShipsComplete
{
	public class ShipCreator : MonoBehaviour
	{
		public Ship ship;
		public Camera creatorCamera;
		public LayerMask mountLayer;

		public GameObject[] allComponents;
		public GameObject currentComponent;
		public int currentComponentNum;

		public float currentComponentRotation;
		GameObject hiddenObject;
		// Start is called before the first frame update
		void Start()
		{
			ship = FindObjectOfType<Ship>();
			ship.transform.parent = transform;
			currentComponent = Instantiate(allComponents[0]);

		}

		// Update is called once per frame
		void Update()
		{
			transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * 60f);
			Ray ray = creatorCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, 10f, mountLayer))
			{
				//IMPORTANT: CHANGE
				Mount mount = hit.collider.transform.GetComponent<Mount>();
				//snap object to the mount
				currentComponent.transform.position = mount.transform.position;

				//"Add" one quaternion to another rotate the obejct 90 degrees around the mount axis
				//Ordering matters!
				currentComponent.transform.rotation = Quaternion.AngleAxis(currentComponentRotation, mount.transform.forward) * mount.transform.rotation;

				//when the LMB button click we want to create a copy of the object
				if (Input.GetMouseButtonDown(0))
				{
					//this will directly parent it to the ship
					GameObject copiedComponent = Instantiate(currentComponent, ship.transform);

					//set the copied component position and rotation to the mount
					copiedComponent.transform.position = mount.transform.position;
					//ah yes that's some good coding practices right there
					copiedComponent.transform.rotation = Quaternion.AngleAxis(currentComponentRotation, mount.transform.forward) * mount.transform.rotation;
					mount.SetCurrentComponent(copiedComponent);

				}

				//if the current mount has an existing object, hide it while moused over
				if (mount.GetCurrentComponent() != null)
				{
					//before swapping objects, reshow any existing object
					if (hiddenObject != null && hiddenObject != mount.GetCurrentComponent())
					{
						hiddenObject.SetActive(true);
					}
					//we want to be able to access this later
					hiddenObject = mount.GetCurrentComponent();
					hiddenObject.SetActive(false);

				}
			}
			else
			{
				//have the object follow the mouse cursor
				currentComponent.transform.position = creatorCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f * Input.mousePosition.y / Screen.height));
				currentComponent.transform.rotation = Quaternion.identity;

				//reshow any hidden object when moused away
				if (hiddenObject != null)
				{
					hiddenObject.SetActive(true);
				}
			}

			int mouseScroll = Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 10f);
			if (mouseScroll != 0f)
			{
				Vector3 currentPosition = currentComponent.transform.position;
				currentComponentNum = Mathf.RoundToInt(Mathf.Repeat(currentComponentNum + mouseScroll, allComponents.Length));
				Destroy(currentComponent);
				currentComponent = Instantiate(allComponents[currentComponentNum], currentPosition, Quaternion.identity);
			}

			//rotate the object if the right click is pressed
			if (Input.GetMouseButtonDown(1))
			{
				currentComponentRotation = (currentComponentRotation + 90f) % 360f;
			}
		}


	}
}
