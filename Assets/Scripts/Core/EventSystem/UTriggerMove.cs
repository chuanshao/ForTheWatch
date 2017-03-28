using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class UTriggerMove : UTriggerBase , IMoveHandler
{

	// Use this for initialization

	public override string GetDefaultEventName ()
	{
		return "Move";
	}
	public virtual void OnMove (AxisEventData eventData){
		BubbleEvent (UEventType.MOVE, new UEventData(eventData, this.m_eventArg));
	}
	// Update is called once per frame

}

