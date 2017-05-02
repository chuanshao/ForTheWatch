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
    Playing
}
/// <summary>
/// 游戏状态数据
/// </summary>
public class PlayFrameData : WData
{
    public List<Player> Players = new List<Player>();//玩家
    public CurrentStatu Cst;//当前状态
    public string CurrentSetScoreUser;//当前设置分数的玩家
    public string BankerUserId;//庄家ID
    public List<Puke> BottomCard;//底牌
    public override void ParseJson(JsonData jdata)
    {
        var players = jdata["players"];
        for (int i = 0; i < players.Count; i++) {
            var player = new Player();
            player.ParseJson(players[i]);
            Players.Add(player);
        }
        this.Cst = (CurrentStatu)jdata.GetValue("cst", 0);
        this.CurrentSetScoreUser = jdata.GetValue("currentSetUser", "");
        this.BankerUserId = jdata.GetValue("bankerUserId", "");
        if (jdata.ContainsKey("bottomCard")) {
            InitBottomCard(jdata["bottomCard"]);//只有庄家才会有底牌
        }
    }

	public Player GetMine(){
		return GetPlayer(MainModel.Instance.User.UserUid);
	}
    public Player GetBanker() {
        return GetPlayer(BankerUserId);
    }
    void InitBottomCard(JsonData data) {

    }
    Player GetPlayer(string uid) {
        for (int i = 0; i < Players.Count; i++) {
            Player p = Players[i];
            if (p.UID == uid) {
                return p;
            }
        }
        return null;
    }
}
