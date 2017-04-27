using UnityEngine;
using System.Collections.Generic;
using LitJson;

public class FightScene : BaseWindow
{
    private string PlayerEnter = "onPlayerEnter";
    private string PlayerExit = "onPlayerExit";
    private string SendPokes = "onSendPokes";
    private string DealingCardOver = "dealingCardOver";
    private PlayFrameData PlayFrame;
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
    public void Init(PlayFrameData frameData) {
        PlayFrame = frameData;
        InitPlayer();
    }
    void OnRegiest() {
        SocketManager.Instance.On(PlayerEnter, delegate (JsonData jdata) { //新玩家进入
            Player player = new Player();
            AddPlayer(player);
        });
        SocketManager.Instance.On(PlayerExit, delegate (JsonData jdata) {//玩家离开
            PlayerPanel.UserLeave(jdata.GetValue("pos" , -1));
        });
        SocketManager.Instance.On(SendPokes, delegate (JsonData jdata) {

        });
        SocketManager.Instance.On(DealingCardOver, delegate (JsonData jdata) {//开始
            Debug.Log(JsonMapper.ToJson(jdata));
        });
    }
    void InitPlayer() {
        var players = PlayFrame.Players;
        for (int i = 0; i < players.Count; i++)
        {
            var Player = players[i];
            AddPlayer(Player);
        }
    }
    //玩家加入
    void AddPlayer(Player player) {
        var players = PlayFrame.Players;
        players.Add(player);
        PlayerPanel.AddPlayer(player);
    }
    /// <summary>
    /// 删除玩家
    /// </summary>
    void DeletePlayer() {

    }

    void PlayerDrop() {

    }
    void BeReady() {
        SocketManager.Instance.Request("game.gameHandler.ready", delegate (JsonData data)
        {

        });
    }
    // Update is called once per frame
    void Update()
    {

    }
}
