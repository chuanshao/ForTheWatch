using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class TSWPuke : Puke
{
    public int SizeNum;
    public TSWPuke(int num, PukeColor pcolor) : base(num, pcolor)
    {

    }
    void InitCompareNum(int num)
    {

    }
    public bool CompleteSame(TSWPuke tp)
    {
        return (this.IsSameNum(tp) && this.SameColor(tp));
    }
    /// <summary>
    /// 用来比较两张牌的大小
    /// </summary>
    /// <param name="o1"></param>
    /// <param name="o2"></param>
    /// <returns></returns>
    public static int operator -(TSWPuke o1, TSWPuke o2)
    {
        return 0;
    }
}
