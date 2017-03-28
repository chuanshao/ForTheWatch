using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UTriggerUp : UTriggerBase, IPointerUpHandler {

	public override string GetDefaultEventName ()
	{
		return "Up";
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
		BubbleEvent (UEventType.UP, new UEventData(eventData,this.m_eventArg));
	}
}
