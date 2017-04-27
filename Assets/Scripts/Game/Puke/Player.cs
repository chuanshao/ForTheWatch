using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class Player : WData
{
    protected UserData _userData;
	protected int _pos;
	protected Player _upPlayer;
	protected Player _nextPlayer;
	protected List<Puke> _pukes;
	protected Stack<PlayedCard> _playedPuke = new Stack<PlayedCard> ();
	public Player()
	{

	}
    public UserData UData
    {
        get {
            return _userData;
        }
        set {
            _userData = value;
        }
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

