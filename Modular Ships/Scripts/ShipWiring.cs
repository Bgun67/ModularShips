using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//One of the last things we want is for the component buttons to be automatically created when a new component
//is mounted on one of the mounts, this script handles that
public class ShipWiring : MonoBehaviour
{
	//Create a singleton so that we can reference this from anywhere
	static ShipWiring instance;
	Ship ship;
	//This is the scrollview that we will add all the unconnected component buttons to
	[SerializeField] RectTransform componentsParent;
	//This is the prefab we made earlier
	[SerializeField] GameObject componentButtonPrefab;
	//So we can destroy all the component buttons when we're done with them
	List<ComponentButton> componentButtons = new List<ComponentButton>();

	//All that's left now is launching the ship! Well that and the ability to unwire components
	[SerializeField] Button launchButton;
	[SerializeField] Button returnButton;
	[SerializeField] GameObject wiringPanel;

	//Made a mistake
	public static ShipWiring Instance{
		get
		{
			if (!instance)
			{
				instance = FindObjectOfType<ShipWiring>();
			}
			return instance;
		}
	}

	void Awake()
	{
		ship = FindObjectOfType<Ship>();

		//Let's also just set up our buttons
		launchButton.onClick.AddListener(Launch);
		returnButton.onClick.AddListener(Return);
		//and hide the return button on start
		returnButton.gameObject.SetActive(false);
	}

	//When a new component is mounted, we want to add one component button for each of the component's
	//actions
	public void AddComponent(ShipComponent component)
	{
		print(component.actions);
		for (int i = 0; i < component.actions.Count; i++)
		{
			//Add a button
			//THis is a pretty hacking way of doing it, fr the buttontype, I'm passing in the actual name of the 
			//method that will be bound
			AddButton(component, component.actions[i].Method.Name, component.actions[i]);
		}
	}

	void AddButton(ShipComponent component, string buttonType, UnityAction<float> action)
	{
		//Instantiate on the components parent and get a reference to the componentbutton at the same time
		ComponentButton button = Instantiate(componentButtonPrefab, componentsParent).GetComponent<ComponentButton>();
		//Set the buttons ship component
		button.component = component;
		//set the button's bound action
		button.boundAction = action;
		//Set the text
		button.text.text = component.componentName + " " + buttonType;
		//add it to our components list so that we can destroy it later if necessary
		componentButtons.Add(button);
	}

	//We also need a way to remove buttons if a component on the ship is swapped out
	public void RemoveComponent(ShipComponent component)
	{
		//Sort through every component to see if the button is there
		for (int i = 0; i < componentButtons.Count;)//and here
		{
			//remove and destroy the button if it's a match
			if (componentButtons[i].component == component)
			{
				componentButtons.Remove(componentButtons[i]);
				Destroy(componentButtons[i]);
			}
			else
			{
				//notice that I do something kind of special here, because I am modifying the size of the 
				//list while iterating over it, the indices get messed up, so I only advance when I don't
				//remove anything
				i++;
			}
		}
	}

	public void Launch()
	{
		//launch the ship and hide the launch button and wiring panel
		ship.Activate();
		wiringPanel.SetActive(false);
		returnButton.gameObject.SetActive(true);
		launchButton.gameObject.SetActive(false);
	}

	public void Return()
	{
		//return the ship to the platform, deactivate it and enable/disable buttons
		ship.Deactivate(FindObjectOfType<ShipCreator>().transform);
		wiringPanel.SetActive(true);
		returnButton.gameObject.SetActive(false);
		launchButton.gameObject.SetActive(true);
	}
}

