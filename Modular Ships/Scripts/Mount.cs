using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Now that the ship wiring is created, we can add the components for wiring when they are mounted
public class Mount : MonoBehaviour
{
	GameObject currentComponent;

	public void SetCurrentComponent(GameObject _component)
	{
		//delete the current component adn replace it with the requested component
		if (currentComponent)
		{
			ShipWiring.Instance.RemoveComponent(currentComponent.GetComponent<ShipComponent>());

			Destroy(currentComponent);
		}
		currentComponent = _component;
		ShipWiring.Instance.AddComponent(currentComponent.GetComponent<ShipComponent>());
	}

	public GameObject GetCurrentComponent()
	{
		return currentComponent;
	}
}
