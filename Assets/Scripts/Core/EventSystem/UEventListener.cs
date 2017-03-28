using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UEventListener : MonoBehaviour, IUEventListener {

	public bool EventHandler(string eventType,
	                  UEventData eventData, 
	                  GameObject eventGameObject) {
	
		return false;
	}
}
