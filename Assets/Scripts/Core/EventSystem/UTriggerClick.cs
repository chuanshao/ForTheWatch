using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UTriggerClick : UTriggerBase, IPointerClickHandler {

	public override string GetDefaultEventName ()
	{
		return "Click";
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		BubbleEvent (UEventType.CLICK, new UEventData(eventData, this.m_eventArg));
	}



}
