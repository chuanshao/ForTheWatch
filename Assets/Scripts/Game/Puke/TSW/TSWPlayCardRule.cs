using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TSWPlayCardRule
{
	/// <summary>
	/// 判别出牌是否符合规矩
	/// </summary>
	/// <returns><c>true</c>, if rule was complianced, <c>false</c> otherwise.</returns>
	/// <param name="me">Me.</param>
	/// <param name="firstPlayer">First player.</param>
	/// <param name="played">Played.</param>
	/// <param name="setting">Setting.</param>
	public bool ComplianceRule(TSWPlayer me , TSWPlayer firstPlayer , TSWPlayedCard played , TSWSetting setting)
	{
		var firstPlayedPuke = firstPlayer.GetPlayedPukes ().Peek() as TSWPlayedCard;
		if (me != firstPlayer && firstPlayedPuke.PlayedPukes.Count != played.PlayedPukes.Count)
			return false;
		switch (firstPlayedPuke.PType) {
		case PlayCardType.APair:
			return APairConfirm (me, firstPlayer, played);
			break;
		case PlayCardType.Single:
			return SingleConfirm (me, firstPlayer, played);
		case PlayCardType.Rejection:
			break;
		case PlayCardType.Tractor://拖拉机
			return TractorConfirm(me, firstPlayer, played , setting);
			break;
		}
		return false;
	}
	bool TractorConfirm(TSWPlayer me , TSWPlayer firstPlayer , TSWPlayedCard played , TSWSetting setting)
	{
		var upPlayed = firstPlayer.GetPlayedPukes ().Peek ();
		var firstPlayPuke = upPlayed.PlayedPukes [0] as TSWPuke;
		var playedPukes = played.PlayedPukes;
		var diffentL = 0;
		var playedPair = 0;//打出的对子
		for (int i = 0; i < playedPukes.Count; i++) {
			var playedItem = playedPukes [i] as TSWPuke;
			if (!firstPlayPuke.IsSameType (playedItem)) {
				diffentL++;
			}
		}

		for (int i = 0; i < playedPukes.Count - 1; i++) {
			var playedItem = playedPukes [i] as TSWPuke;
			var nextPlayedItem = playedPukes [i + 1] as TSWPuke;
			if (!playedItem.CompleteSame (nextPlayedItem)) {
				playedPair++;
				i++;
			}
		}
		if (diffentL != 0) {
			if (me.GetPukesByType (firstPlayPuke.PColor, firstPlayPuke.IsGod).Count == (2 - diffentL)) {
				return true;
			} else {
				return false;
			}
		} else {
			if (me.GetPairByType (firstPlayPuke.PColor, firstPlayPuke.IsGod).Count == playedPair) {//确实就只有这几对
				return true;
			} else {
				return false;
			}
		}
	}
	/// <summary>
	/// 是否符合一对
	/// </summary>
	/// <returns><c>true</c>, if pair confirm was Aed, <c>false</c> otherwise.</returns>
	/// <param name="me">Me.</param>
	/// <param name="firstPlayer">First player.</param>
	/// <param name="played">Played.</param>
	bool APairConfirm(TSWPlayer me , TSWPlayer firstPlayer , TSWPlayedCard played)
	{
		var upPlayed = firstPlayer.GetPlayedPukes ().Peek ();
		var upFirst = upPlayed.PlayedPukes [0] as TSWPuke;
		var playedFirst = played.PlayedPukes [0] as TSWPuke;
		var playedSecond = played.PlayedPukes [1] as TSWPuke;
		var diffentL = 0;
		if(!upFirst.IsSameType(playedFirst)){
			diffentL++;
		}
		if(!upFirst.IsSameType(playedSecond)){
			diffentL++;
		}
		if (diffentL != 0) {
			if (me.GetPukesByType (upFirst.PColor, upFirst.IsGod).Count == (2 - diffentL)) {
				return true;
			} else {
				return false;
			}
		} else {
			if (playedFirst.CompleteSame (playedSecond)) {
				return true;
			} else {
				if (me.GetPairByType (upFirst.PColor, upFirst.IsGod).Count == 0) {//确实没有一对
					return true;
				} else {
					return false;
				}
			}
		}
	}
	/// <summary>
	/// 判断是否符合单张的出发
	/// </summary>
	/// <returns><c>true</c>, if confirm was singled, <c>false</c> otherwise.</returns>
	/// <param name="me">Me.</param>
	/// <param name="upPlayed">Up played.</param>
	/// <param name="played">Played.</param>
	bool SingleConfirm(TSWPlayer me , TSWPlayer firstPlayer , TSWPlayedCard played)
	{
		var upPlayerLastPlayedPuke = firstPlayer.GetPlayedPukes ().Peek ();
		var upPlayPuke = upPlayerLastPlayedPuke.PlayedPukes [0] as TSWPuke;
		var playedPuke = played.PlayedPukes [0] as TSWPuke;
		if (upPlayPuke.IsSameType (playedPuke)) {
			return true;
		} else {
			if (me.GetPukesByType (upPlayPuke.PColor, upPlayPuke.IsGod).Count == 0) {
				return true;
			} else {
				return false;
			}
		}
	}
}

