using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularShipsComplete
{
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
			ShipWiring.Instance.AddComponent(_component.GetComponent<ShipComponent>());

		}

		public GameObject GetCurrentComponent()
		{
			return currentComponent;
		}
	}
}
