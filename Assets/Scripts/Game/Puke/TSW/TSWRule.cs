using System;
using System.Collections.Generic;
public class TSWRule : BaseRule
{
	private TSWPlayer _me;
	private TSWSetting _setting;
	private TSWPrompt _prompt; //提示器
	private CardRecorder _recorder;//记牌器
	private TSWPlayCardRule _playCardRule;
	public TSWRule ( CardRecorder recorder)
	{
		this._recorder = recorder;
	}
	public override void Setting (WData setting)
	{
		this._setting = (TSWSetting)setting;
	}
	public override bool ComplianceRule (Player first , List<Puke> playingCards)
	{
		PlayCardType type = _prompt.GetPukeType (playingCards);
		TSWPlayedCard cards = new TSWPlayedCard ();
		cards.PType = type;
		return this._playCardRule.ComplianceRule (this._me, (first as TSWPlayer), cards, this._setting);
	}
	public override void PlayingCards (Player player , List<Puke> playingCards)
	{
		PlayCardType type = _prompt.GetPukeType (playingCards);
		TSWPlayedCard cards = new TSWPlayedCard ();
		cards.PType = type;
		this._recorder.PlayingPuke (player.GetPos(), cards);
	}
	public override bool IsOver ()
	{
		throw new NotImplementedException ();
	}
	public override WData GetResult ()
	{
		throw new NotImplementedException ();
	}
	public override List<int> GetPrompt ()
	{
		Player upPlayer = this._me.GetUpPlayer ();
		List<Puke> minePukes = this._me.GetPukes();
		PlayedCard pc = _recorder.GetPlayerLastCard (upPlayer.GetUpPlayer ().GetPos ());
		List<Puke> upPukes = pc.PlayedPukes;
		return _prompt.GetPrompt (minePukes , upPukes);
	}
}