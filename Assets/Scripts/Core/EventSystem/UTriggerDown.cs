using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UTriggerDown : UTriggerBase, IPointerDownHandler {

	public override string GetDefaultEventName ()
	{
		return "Down";
	}


	public virtual void OnPointerDown(PointerEventData eventData)
	{
		BubbleEvent (UEventType.DOWN, new UEventData(eventData, this.m_eventArg));
	}
}
