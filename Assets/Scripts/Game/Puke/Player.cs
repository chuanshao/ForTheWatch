using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player
{
	protected int _pos;
	protected Player _upPlayer;
	protected Player _nextPlayer;
	protected List<Puke> _pukes;
	protected Stack<PlayedCard> _playedPuke = new Stack<PlayedCard> ();
	public Player(Player upPlayer , Player nextPlayer , List<Puke> pukes)
	{
		this._pukes = pukes;
		this._upPlayer = upPlayer;
		this._nextPlayer = nextPlayer;
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

