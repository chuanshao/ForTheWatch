using UnityEngine;
using System;
using UnityEngine.UI;
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
        if (RoomId.text.Length >= 6) RoomId.text = "";
        RoomId.text += num;
    }
    void OnDelete() {
        var length = RoomId.text.Length;
        if (length <= 0) return;
        RoomId.text = RoomId.text.Remove(length - 1 , 1);
    }
}