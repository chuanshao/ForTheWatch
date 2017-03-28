using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//事件数据
public class UEventData
{

	public BaseEventData baseEventData;
	public string eventArg = string.Empty;
	public WData udata = null;


	public UEventData(BaseEventData bED) {
		this.baseEventData = bED;
	}

	public UEventData(BaseEventData bED, string eventArg) {
		this.baseEventData = bED;
		this.eventArg = eventArg;

	}

	public UEventData(BaseEventData bED, WData eventArg) {
		this.baseEventData = bED;
		this.udata = eventArg;
		
	}


	public UEventData() {
	}
}

