using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIPanel : MonoBehaviour
{
    public UserHead Left;
    public UserHead Right;
    public UserHead Up;
    public UserHead Down;
    private void Awake() {
        Down.gameObject.SetActive(true);
        Down.SetHeadUrl(MainModel.Instance.User.PicUrl);
    }
    public void AddUser(UserData user) {
        var userHead = GetUserHeadByOtherPos(user.Pos);
        userHead.gameObject.SetActive(true);
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
    UserHead GetUserHeadByOtherPos(int pos) {
        var mine = MainModel.Instance.User;
        var minePos = mine.Pos;
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
