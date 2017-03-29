using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 记牌器
/// </summary>
public class CardRecorder
{
	private Dictionary<int , List<PlayedCard>>  _playedPukes = new Dictionary<int, List<PlayedCard>>();
	public void PlayingPuke(int player , PlayedCard playedCard)
	{
		if (!this._playedPukes.ContainsKey (player)) {
			this._playedPukes.Add (player, new List<PlayedCard> ());
		}
		List<PlayedCard> playCards = this._playedPukes [player];
		playCards.Add (playedCard);
	}
	/// <summary>
	/// 获取某个玩家最后出的牌
	/// </summary>
	/// <returns>The player last card.</returns>
	/// <param name="player">Player.</param>
	public PlayedCard GetPlayerLastCard(int player)
	{
		if (!this._playedPukes.ContainsKey (player)) {
			return null;
		}
		var listP = this._playedPukes [player];
		if (listP.Count == 0) {
			return null;
		}

		return listP[listP.Count - 1];
	}
}

