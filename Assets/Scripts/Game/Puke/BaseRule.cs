using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 所有的规则都继承这个类
/// </summary>
public abstract class BaseRule
{
	/// <summary>
	/// 这种方式出牌是否符合规则
	/// </summary>
	/// <returns><c>true</c>, if rule was complianced, <c>false</c> otherwise.</returns>
	/// <param name="playingCards">Playing cards.</param>
	public abstract bool ComplianceRule (List<Puke> playingCards);
	/// <summary>
	/// Playings the cards.
	/// </summary>
	/// <param name="player">Player.</param>
	/// <param name="playingCards">Playing cards.</param>
	public abstract void PlayingCards (Player player , List<Puke> playingCards);
	/// <summary>
	/// Gets the result.
	/// </summary>
	/// <returns>The result.</returns>
	public abstract WData GetResult ();
	/// <summary>
	/// Determines whether this instance is over.
	/// </summary>
	/// <returns><c>true</c> if this instance is over; otherwise, <c>false</c>.</returns>
	public abstract bool IsOver();
	/// <summary>
	/// Gets the prompt.
	/// </summary>
	/// <returns>可以打出的牌的位置</returns>
	public abstract List<int> GetPrompt ();

	public abstract void Setting (WData setting);
}

