using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TSWPlayer : Player
{
	private TSWSetting _setting;
	public TSWPlayer(List<Puke> pukes , TSWSetting setting , TSWPlayer upPlayer , TSWPlayer nextPlayer) : base(upPlayer , nextPlayer , pukes)
	{
		this._setting = setting;
	}
	void Init()
	{
		
	}
	/// <summary>
	/// 对puke进行排序
	/// </summary>
	void SortPukes()
	{
		
	}
	public void PlayingPukes()
	{
		
	}
	public List<TSWPuke> GetPukesByType(PukeColor col , bool isGod = false)
	{
		List<TSWPuke> returnPuke = new List<TSWPuke>();
		for (int i = 0; i < this._pukes.Count; i++) {
			TSWPuke pukeItem = _pukes [i] as TSWPuke;
			if (pukeItem != null) {
				if (pukeItem.PColor == col) {
					returnPuke.Add (pukeItem);
				} else if (isGod && pukeItem.IsGod) {
					returnPuke.Add (pukeItem);
				}
			}
		}
		return returnPuke;
	}

	public List<TSWPuke[]> GetPairByType(PukeColor col , bool isGod = false){
		
		return new List<TSWPuke[]> ();
	}
	/// <summary>
	/// 取当前玩家拥有的主
	/// </summary>
	/// <returns>The god.</returns>
	public List<TSWPuke> GetGod()
	{
		List<TSWPuke> returnPuke = new List<TSWPuke>();
		for (int i = 0; i < this._pukes.Count; i++) {
			TSWPuke pukeItem = _pukes [i] as TSWPuke;
			if (pukeItem != null && pukeItem.IsGod) {
				returnPuke.Add (pukeItem);
			}
		}
		return returnPuke;
	}
}

