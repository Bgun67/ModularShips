using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


//This script will allow us to drag around the buttons, if you run into any issues in this part
//I'll link a CodeMonkey tutorial in the description
//We also want to toggle the invert state on the component when the button is clicked
public class ComponentButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	//Transform for changing the object's position
	RectTransform rt;
	//THis basically makes every child act as one unit
	CanvasGroup group;
	//Text to display what component this is
	public Text text;
	public Text invert;
	//The component that this button represents
	public ShipComponent component;
	//The action that will be bound to this button, (Throttle, Steer, etc..)
	[HideInInspector]
	public UnityAction<float> boundAction;


	void Start()
	{
		rt = GetComponent<RectTransform>();
		group = GetComponent<CanvasGroup>();
	}

	//These come built in with UI elements
	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		rt.anchoredPosition += eventData.delta;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		//Release the object from its parent, so that it can be freely dragged and moved to the other boxes
		//Here I set it to transform.root, so that the button will still be attached to the canvas
		rt.SetParent(transform.root);
		group.blocksRaycasts = false;

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		//We'll set the parent in one of the receiving boxes, but we want to stop this button from blocking
		//raycasts so that events are called on the box that we drop this button on
		group.blocksRaycasts = true;

	}

	public void OnPointerClick(PointerEventData eventData)
	{
		component.invert = !component.invert;
		//Let's do the text too
		invert.text = component.invert?"-":"+";
	}

	//Finally, it would be nice to have a signal for which component this button references, for this all 
	//I'm going to do is a little scale effect when the button is moused over
	public void OnPointerEnter(PointerEventData eventData)
	{
		component.transform.localScale = Vector3.one * 1.1f;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//reset the scale
		component.transform.localScale = Vector3.one;

	}


}
