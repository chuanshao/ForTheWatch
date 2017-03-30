using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 提示器
/// </summary>
public enum PlayCardType
{
    None,
    Single,  //单张
    APair,   //一对
    Tractor, //拖拉机
    Rejection //甩
}
public class TSWPrompt
{
    private TSWSetting _setting;
    public TSWPrompt(TSWSetting setting)
    {
        this._setting = setting;
    }
    public List<int> GetPrompt(List<Puke> myPukes , List<Puke> upPukes)
    {
        if (upPukes == null || upPukes.Count == 0)
        {
            return null;
        }
        PlayCardType type = GetPukeType(upPukes);
        switch (type)
        {
            case PlayCardType.Tractor:
                return GetTractor(myPukes , upPukes);
                break;
            case PlayCardType.Single:

                break;
        }
        return null;
    }
	List<int> GetTractor(List<Puke> myPukes, List<Puke> upPukes)
    {
		return null;
    }
    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="upPukes"></param>
    /// <returns></returns>
    public PlayCardType GetPukeType(List<Puke> upPukes)
    {
        if (upPukes == null || upPukes.Count == 0)
        {
            return PlayCardType.None;
        }
        if (upPukes.Count == 1)
        {
            return PlayCardType.Single;
        }
        else if (upPukes.Count == 2)
        {
			TSWPuke first = upPukes [0] as TSWPuke;
			TSWPuke second = upPukes [1] as TSWPuke;
			if (first.CompleteSame(second))
            {
                return PlayCardType.APair;
            }
            else
            {
                return PlayCardType.Rejection;
            }
        }
        else if (upPukes.Count % 2 == 0)
        {
            if (JudgePukesIsTractor(upPukes))
            {
                return PlayCardType.Tractor;
            }
            return PlayCardType.Rejection;
        }
        else
        {
            return PlayCardType.Rejection;
        }
    }
    /// <summary>
    /// 这个列表是有顺序的
    /// 递归判断
    /// </summary>
    /// <param name="pukes"></param>
    /// <returns></returns>
    bool JudgePukesIsTractor(List<Puke> pukes)
    {
        if (pukes.Count < 4) return false;
        List<Puke> four = pukes.GetRange(0, 4);
		TSWPuke first = four[0] as TSWPuke;
		TSWPuke second = four[1] as TSWPuke;
		TSWPuke third = four[2] as TSWPuke;
		TSWPuke fourth = four[3] as TSWPuke;
        if (first.CompleteSame(second) && third.CompleteSame(fourth))//先判断两两是不是相同
        {
            return false;
        }
        if ((third - first) != 1) //两个之间大小差值只能是1
        {
            return false;
        }
        if (!this._setting.TractorTurn) //不带拐弯
        {
            if (first.SizeNum >= 11 && !first.IsSameNum(third))
            {
                return false;
            }
        }
        List<Puke> nextFour = pukes.GetRange(2, pukes.Count - 2);
        if (nextFour.Count < 4)
        {
            return true;
        }
        return JudgePukesIsTractor(nextFour);
    }
}
