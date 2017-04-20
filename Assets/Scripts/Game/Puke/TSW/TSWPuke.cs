using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class TSWPuke : Puke
{
    public int SizeNum;
	private bool _isGod;
	public bool IsGod
	{
		get{
			return this._isGod;
		}
		set
		{
			this._isGod = value;
		}
	}
	public TSWPuke(int num, PukeColor pcolor) : base(num, pcolor)
    {

    }
    void InitCompareNum(int num)
    {

    }
    /// <summary>
    /// 都是主 或者花色相同
    /// </summary>
    /// <returns><c>true</c> if this instance is same type the specified puke; otherwise, <c>false</c>.</returns>
    /// <param name="poke">Puke.</param>
    public bool IsSameType(TSWPuke poke)
	{
        if (this.SameColor(poke))
        {
            return true;
        }
        if (poke.IsGod && this.IsGod) {
            return true;
        }
		return false;
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
        return o1.PukeNum - o2.PukeNum;
    }
}
