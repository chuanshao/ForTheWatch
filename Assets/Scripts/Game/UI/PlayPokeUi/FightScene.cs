using UnityEngine;
using System.Collections.Generic;
using LitJson;

public class FightScene : BaseWindow
{
    private string PlayerEnter = "onPlayerEnter";
    private string PlayerExit = "onPlayerExit";
    private string SendPokes = "onSendPokes";
    private string GameStart = "onGameStart";
    private Dictionary<string, UserData> _users = new Dictionary<string, UserData>();
    public PlayerUIPanel PlayerPanel;
    private void Awake()
    {
        OnRegiest();
    }
    // Use this for initialization
    void Start()
    {
        BeReady();
    }
    public void Init(WData totleDatas) {

    }
    void OnRegiest() {
        SocketManager.Instance.On(PlayerEnter, delegate (JsonData jdata) { //新玩家进入
            UserData user = new UserData();
            user.ParseJson(jdata);
            _users.Add(user.UserUid , user);
            PlayerPanel.AddUser(user);
        });
        SocketManager.Instance.On(PlayerExit, delegate (JsonData jdata) {//玩家离开
            PlayerPanel.UserLeave(jdata.GetValue("pos" , -1));
        });
        SocketManager.Instance.On(SendPokes, delegate (JsonData jdata) {

        });
        SocketManager.Instance.On(GameStart, delegate (JsonData jdata) {//开始

        });
    }
    void BeReady() {
        //SocketManager.Instance.Request("game.gameHandler.ready", delegate (JsonData data)
        //{

        //});
    }
    // Update is called once per frame
    void Update()
    {

    }
}
