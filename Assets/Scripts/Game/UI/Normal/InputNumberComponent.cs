using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class InputNumberComponent : MonoBehaviour , IUEventListener
{
    public Action OnDeleteClick;
    public Action OnRetryClick;
    public Action<string> OnNumClick;
    public bool EventHandler(string eventType,
                  UEventData eventData,
                  GameObject eventGameObject)
    {
        if (eventType == "onClick") {
            OnButtonClick(eventData.eventArg);
            return false;
        }
        return true;
    }
    void OnButtonClick(string clickArg)
    {
        if (clickArg == "delete")
        {
            if (OnDeleteClick != null)
                OnDeleteClick.Invoke();
        }
        else if (clickArg == "retry")
        {
            if (OnRetryClick != null)
                OnRetryClick.Invoke();
        }
        else
        {
            if (OnNumClick != null)
                OnNumClick.Invoke(clickArg);
        }
    }
}