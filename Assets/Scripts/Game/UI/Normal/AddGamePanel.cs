using UnityEngine;
using System;
using UnityEngine.UI;
using LitJson;

public class AddGamePanel : BaseWindow
{
    public InputNumberComponent INC;
    public Text RoomId;
    public Button CloseBtn;
    private void Awake()
    {
        INC.OnRetryClick = (Action)Delegate.Combine(INC.OnRetryClick, new Action(OnRetry));
        INC.OnDeleteClick = (Action)Delegate.Combine(INC.OnDeleteClick, new Action(OnDelete));
        INC.OnNumClick = (Action<string>)Delegate.Combine(INC.OnNumClick, new Action<string>(OnNumber));
        CloseBtn.onClick.AddListener(OnCloseClick);
    }
    void OnCloseClick()
    {
        Game.script.uiCanvas.CloseCurrentGui();
    }
    void OnRetry() {
        RoomId.text = "";
    }

    void OnNumber(string num) {
        RoomId.text += num;
        if (RoomId.text.Length >= 6) {
            RoomId.text = "";
            SocketManager.Instance.Request("game.gameHandler.addGame", OnAddGameCallBack);
            return;
        }
    }
    void OnAddGameCallBack(JsonData jdata) {
        Debug.Log(JsonMapper.ToJson(jdata));
        Ass.Instance.GetPrefab("ui/PlaySceneUI", delegate (GameObject go)//进入游戏
        {
            GameObject iGo = GameObject.Instantiate(go);
            Game.script.uiCanvas.OpenGui(iGo.GetComponent<GuiBase>());
        });
    }
    void OnDelete() {
        var length = RoomId.text.Length;
        if (length <= 0) return;
        RoomId.text = RoomId.text.Remove(length - 1 , 1);
    }
}