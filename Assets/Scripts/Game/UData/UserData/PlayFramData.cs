using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public enum CurrentStatu
{
    SetScore,//设置分数
    BankerBuckleCard,//庄家扣底
    PlayerReady,//玩家准备中
}
public class PlayFramData : WData
{
    public List<Player> Players;//玩家
    public CurrentStatu Cst;
    public int CurrentScore;
}
