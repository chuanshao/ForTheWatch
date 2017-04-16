using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
/// <summary>
/// 进来之后请求当前房间的状态
/// </summary>
public class PlayPukeController : MonoBehaviour
{
    private Button ReadyBtn;
	void Awake()
	{
        SocketManager.Instance.On("entry.handler.startGame", this.StartGame);//人全了之后开始游戏
        SocketManager.Instance.On("room.handler.otherPoke", this.OtherPoke);//其他玩家出牌
        ReadyBtn.onClick.AddListener(delegate ()
        {
            SocketManager.Instance.Request("entry.hand.gameReady", delegate (JsonData data) { });
        });
    }

	// Use this for initialization
	void Start ()
	{
		
	}
    void StartGame(JsonData jdata)
    {
        for (int i = 0; i < jdata.Count; i++)
        {
            var num = jdata[i].GetValue("num" , 0);
            var color = (PukeColor)jdata[i].GetValue("num", 0);
            TSWPuke pukeItem = new TSWPuke(num , color);
        }
    }
    void OtherPoke(JsonData jdata)
    {

    }
    // Update is called once per frame
    void Update ()
	{
		
	}
}

