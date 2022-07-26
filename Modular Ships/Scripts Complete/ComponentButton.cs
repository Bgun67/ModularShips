using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ModularShipsComplete
{
	public class ComponentButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		RectTransform rt;
		public ShipComponent component;
		public Text text;
		public Text invert;
		[HideInInspector] public UnityAction<float> boundAction;

		public void Start()
		{
			rt = GetComponent<RectTransform>();
		}
		public void OnDrag(PointerEventData pointerEvent)
		{
			rt.anchoredPosition += pointerEvent.delta;
		}
		public void OnBeginDrag(PointerEventData eventData)
		{
			rt.GetComponent<CanvasGroup>().blocksRaycasts = false;
			rt.SetParent(transform.root);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			rt.GetComponent<CanvasGroup>().blocksRaycasts = true;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			component.invert = !component.invert;
			invert.text = component.invert ? "-" : "+";
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			component.transform.localScale = Vector3.one * 1.1f;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			component.transform.localScale = Vector3.one;
		}
	}
}
