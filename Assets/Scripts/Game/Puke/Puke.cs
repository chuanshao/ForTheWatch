using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public enum PukeColor
{
    Hearts,//红
    Plum, //梅
    Spade, //黑
    King,
    Diamonds//方
}
public abstract class Puke
{
    public int PukeNum;//扑克数字
    public bool IsSameNum(Puke p)//比如两张牌是不是都是3
    {
        return p.PukeNum == this.PukeNum;
    }
    public bool SameColor(Puke p)
    {
        return p.PColor == this.PColor;
    }
    public PukeColor PColor;//扑克颜色
    public Puke(int pukeNum, PukeColor pcolor)
    {
        this.PukeNum = pukeNum;
        this.PColor = pcolor;
    }
}
