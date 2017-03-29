using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player
{
	protected int _pos;
	protected Player _upPlayer;
	protected Player _nextPlayer;
	protected List<Puke> _pukes;
	public Player(int pos , Player upPlayer , Player nextPlayer)
	{
		this._pos = pos;
		this._upPlayer = upPlayer;
		this._nextPlayer = nextPlayer;
	}
	public int GetPos()
	{
		return this._pos;
	}
	public void SetPukes(List<Puke> pukes)
	{
		
	}
	public List<Puke> GetPukes()
	{
		return this._pukes;
	}
	public void PlayPuke()
	{
		
	}
	public Player GetUpPlayer()
	{
		return this._upPlayer;
	}
}

