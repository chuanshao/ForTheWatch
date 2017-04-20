using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : SceneBase{
	// Use this for initialization
	void Start ()
    {
		
	}
    public override void OnSceneLoadStart()
    {

    }
    public override void OnSceneLoadProcess(float process)
    {

    }
    /// <summary>
    /// 场景加载完成执行场景逻辑
    /// </summary>
    public override void OnSceneLoadComplete()
    {
        Ass.Instance.GetPrefab("ui/MainPanel", delegate (GameObject go) 
        {
            GuiBase gui = go.GetComponent<GuiBase>();
            Game.script.uiCanvas.OpenGui(gui);
        });
    }
}
