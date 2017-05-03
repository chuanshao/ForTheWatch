using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class Player : WData
{
    protected string _uid;
	protected int _pos;
	protected string _picUrl;
	protected Player _upPlayer;
	protected Player _nextPlayer;
	protected List<Puke> _pukes;
	protected Stack<PlayedCard> _playedPuke = new Stack<PlayedCard> ();
	public Player()
	{
        
	}
    public string UID
    {
        get {
            return _uid;
        }
        set {
            _uid = value;
        }
    }
	public string PicUrl()
	{
		return this._picUrl;
	}
	public int GetPos()
	{
		return this._pos;
	}
	public List<Puke> GetPukes()
	{
		return this._pukes;
	}
	public void PlayPuke (PlayedCard pukes)
	{
		this._playedPuke.Push (pukes);
	}
    public override void ParseJson(JsonData jdata)
    {
        this._pos = jdata.GetValue("pos", 0);
        this._uid = jdata.GetValue("uid", "");
    }
    /// <summary>
    /// 获取打出去的牌
    /// </summary>
    public Stack<PlayedCard> GetPlayedPukes()
	{
		return _playedPuke;
	}
	public Player GetUpPlayer()
	{
		return this._upPlayer;
	}
}

