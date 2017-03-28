using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UTriggerSubmit : UTriggerBase , ISubmitHandler
{

    // Use this for initialization

    public override string GetDefaultEventName()
    {
        return "Submit";
    }
    public virtual void OnSubmit(BaseEventData eventData)
    {
        BubbleEvent(UEventType.SUBMIT, new UEventData(eventData, this.m_eventArg));
    }
}
