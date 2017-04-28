using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class PlayerUIPanel : MonoBehaviour
{
    public UserHead Left;
    public UserHead Right;
    public UserHead Up;
    public UserHead Down;
	private Player _mine;
    private void Awake() {
        Down.gameObject.SetActive(true);
    }
	public void Init(Player mine){
		Down.SetHeadUrl(mine.PicUrl());
		_mine = mine;
	}
    public void AddPlayer(Player player) {
		if (_mine == null) {
			return;
		}
        var userHead = GetUserHeadByOtherPos(player.GetPos());
        userHead.gameObject.SetActive(true);
    }
	public void AddPlayer(List<Player> players){
		for (int i = 0; i < players.Count; i++) {
			AddPlayer (players [i]);
		}
	}
    public void UserLeave(int pos) {
        if (pos <= 0) return;
        var userHead = GetUserHeadByOtherPos(pos);
        userHead.gameObject.SetActive(false);
    }
    /// <summary>
    /// 玩家状态变化
    /// </summary>
    /// <param name="pos"></param>
    public void UserStatusChange(int pos) {

    }
	void FindMine(List<Player> players){
		for (int i = 0; i < players.Count; i++) {
			var player = players [i];
			if (player.UData.UserUid == MainModel.Instance.User.UserUid) {
				_mine = player;
				return;
			}
		}
	}
    UserHead GetUserHeadByOtherPos(int pos) {
		var minePos = _mine.GetPos();
        var minePosMinus = minePos - pos;
        if (minePosMinus == -1 || minePosMinus == 2)
        { //右边
            return Right;
        }
        else if (minePosMinus == 0) {
            return Down;
        }
        else
        {
            return Left;
        }
    }
}
