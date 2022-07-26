using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModularShipsComplete
{
	public class ShipWiring : MonoBehaviour
	{
		Ship ship;
		[SerializeField] GameObject shipWiringPanel;
		[SerializeField] RectTransform componentsParent;
		[SerializeField] GameObject componentButtonPrefab;
		[SerializeField] Button launchButton;
		[SerializeField] Button returnButton;
		List<ComponentButton> componentButtons = new List<ComponentButton>();
		static ShipWiring instance;


		public static ShipWiring Instance
		{
			get
			{
				if (!instance)
				{
					instance = FindObjectOfType<ShipWiring>();
				}
				return instance;
			}
		}

		// Start is called before the first frame update
		void Start()
		{
			ship = FindObjectOfType<Ship>();
			launchButton.onClick.AddListener(Activate);
			returnButton.onClick.AddListener(Deactivate);
			returnButton.gameObject.SetActive(false);
		}

		public void AddComponent(ShipComponent component)
		{
			//Add a new component button for each of the assigneable actions
			for (int i = 0; i < component.actions.Count; i++)
			{
				AddButton(component, component.actions[i].Method.Name, component.actions[i]);
			}
		}
		void AddButton(ShipComponent component, string buttonType, UnityAction<float> action)
		{
			ComponentButton button = Instantiate(componentButtonPrefab, componentsParent).GetComponent<ComponentButton>();
			button.component = component;
			button.boundAction += action;
			button.text.text = component.componentName + " - " + buttonType;
			componentButtons.Add(button);
		}
		public void RemoveComponent(ShipComponent component)
		{
			for (int i = 0; i < componentButtons.Count;)
			{
				ComponentButton button = componentButtons[i];
				if (button.component == component)
				{
					componentButtons.Remove(button);
					Destroy(button.gameObject);
				}
				else
				{
					i++;
				}
			}
		}
		public void Activate()
		{
			shipWiringPanel.gameObject.SetActive(false);
			ship.Activate();
			returnButton.gameObject.SetActive(true);
			launchButton.gameObject.SetActive(false);
		}
		public void Deactivate()
		{
			returnButton.gameObject.SetActive(false);
			launchButton.gameObject.SetActive(true);
			shipWiringPanel.gameObject.SetActive(true);
			ship.Deactivate(FindObjectOfType<ShipCreator>().transform);
		}
	}
}
