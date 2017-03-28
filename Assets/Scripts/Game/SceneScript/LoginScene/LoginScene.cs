using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
class LoginScene : SceneBase
{
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
        Ass.Instance.GetPrefab("ui/LoginPanel", delegate (GameObject go)
        {
            GameObject mainPanelGo = GameObject.Instantiate(go);
            GuiBase mainPanel = mainPanelGo.GetComponent<GuiBase>();
            Game.Instance.script.uiCanvas.OpenGui(mainPanel);
        });
    }
}
