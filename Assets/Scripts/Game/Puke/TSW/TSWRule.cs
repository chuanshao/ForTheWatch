using System;
using System.Collections.Generic;
public class TSWRule : BaseRule
{
	private Player _me;
	private TSWSetting _setting;
	private TSWPrompt _prompt; //提示器
	private List<TSWPuke> _minePukes;
	private CardRecorder _recorder;//记牌器
	public TSWRule (List<TSWPuke> minePukes , CardRecorder recorder)
	{
		this._minePukes = minePukes;
		this._recorder = recorder;
	}
	public override void Setting (WData setting)
	{
		this._setting = (TSWSetting)setting;
	}
	public override bool ComplianceRule (List<Puke> playingCards)
	{
		return false;
	}
	public override void PlayingCards (Player player , List<Puke> playingCards)
	{
		List<TSWPuke> tswPukes = playingCards.ConvertAll<TSWPuke> (input => input as TSWPuke);
		PlayCardType type = _prompt.GetPukeType (tswPukes);
		TSWPlayedCard cards = new TSWPlayedCard ();
		cards.PType = type;
		_recorder.PlayingPuke (player.GetPos(), cards);
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
		List<TSWPuke> minePukes = this._me.GetPukes().ConvertAll<TSWPuke> (input => input as TSWPuke);
		PlayedCard pc = _recorder.GetPlayerLastCard (upPlayer.GetUpPlayer ().GetPos ());
		List<TSWPuke> upPukes = pc.PlayedPukes.ConvertAll<TSWPuke> (input => input as TSWPuke);
		return _prompt.GetPrompt (minePukes , upPukes);
	}
}