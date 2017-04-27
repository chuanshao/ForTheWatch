using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

public enum CurrentStatu
{
    PlayerReady,//玩家准备中
    SetScore,//设置分数
    BankerBuckleCard,//庄家扣底
}
/// <summary>
/// 游戏状态数据
/// </summary>
public class PlayFrameData : WData
{
    public List<Player> Players = new List<Player>();//玩家
    public CurrentStatu Cst;
    public int CurrentScore;
    public override void ParseJson(JsonData jdata)
    {
        this.Cst = (CurrentStatu)jdata.GetValue("cst", 0);
        this.CurrentScore = jdata.GetValue("currentScore", 0);
        var players = jdata["players"];
        for (int i = 0; i < players.Count; i++) {
            var player = new Player();
            player.ParseJson(players[i]);
            Players.Add(player);
        }
    }
}
