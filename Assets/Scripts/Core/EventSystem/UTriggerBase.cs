using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UTriggerBase : MonoBehaviour {

	//public bool m_bubble = false;//是否冒泡

	protected Transform m_transform;
	//private IUEventListener thisEventListener;
	public string m_eventName;
	public string m_eventArg;

	public delegate void TriggerListener(string eventType, UEventData eventData);
	public event TriggerListener Listener;




	void Start () {
		m_transform = this.transform;
		//thisEventListener = gameObject.GetComponent<IUEventListener> ();
	}

	
	public void BubbleEvent(string eventType, UEventData eventData) {
		Transform tf = m_transform;
		if (Listener != null) {
			Listener(eventType, eventData);
		}

		while(tf != null ) {
			IUEventListener eventListener = tf.GetComponent<IUEventListener>();
			if (eventListener != null) {
				string mEventType = (string.IsNullOrEmpty(m_eventName) ? eventType : m_eventName);
				bool handHandlerValue = eventListener.EventHandler(mEventType, eventData, gameObject);
				if (handHandlerValue == true) {
					break;
				}
			}
			tf = tf.parent;
		}
	}


	protected T GetInParent<T>() {
		Transform tf = m_transform;
		while (tf != null) {
			T obj = tf.GetComponent<T>();
			if (obj != null) {
				return obj;
			}
			tf = transform.parent;
		}
		return default(T);
	}



	//编辑器使用
	public virtual string GetDefaultEventName() {
		return "";
	}

}
