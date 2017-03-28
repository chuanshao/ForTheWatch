using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public interface IUEventListener  {
	
	/// <summary>
	/// Events the handler.
	/// </summary>
	/// <returns><c>true</c>, if handler was evented, <c>false</c> otherwise.</returns>
	/// <param name="eventType">Event type. EventPropagationType 事件类别</param>
	/// <param name="eventData">Event data. 事件数据</param>
	/// <param name="eventGameObject">Event game object. 发生事件的对象</param>
	bool EventHandler(string eventType,
	                  UEventData eventData, 
	                  GameObject eventGameObject);
}