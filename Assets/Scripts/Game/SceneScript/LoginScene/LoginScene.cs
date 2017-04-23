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
        SocketManager.Instance.StartService("127.0.0.1", 3014, delegate ()
        {
            Ass.Instance.GetPrefab("ui/LoginPanel", delegate (GameObject go)
            {
                var panel = GameObject.Instantiate(go);
                Game.script.uiCanvas.OpenGui(panel.GetComponent<GuiBase>());
            });
        });
    }
}
