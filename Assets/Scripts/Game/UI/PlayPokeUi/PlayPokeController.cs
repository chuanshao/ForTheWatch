using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
/// <summary>
/// 进来之后请求当前房间的状态
/// </summary>
public class PlayPokeController : MonoBehaviour
{
    private TSWFriendsTotleData _totalData;
    private PokesAnimation _pokesAnimation;
    private bool IsMyTurn = false;
    public Button ShowReady;
    public Button SendPokesButton;
    void Awake()
	{
        SocketManager.Instance.On("tsw.gameStart", GameStart);
        SocketManager.Instance.On("tsw.newPlayerEnter", NewPlayerEnter);
        SocketManager.Instance.On("tsw.sendPokes", SendPokes);

        ShowReady.onClick.AddListener(delegate ()
        {

        });
        SendPokesButton.onClick.AddListener(delegate ()
        {

        });
    }
    void GameStart(JsonData data) {

    }
    void NewPlayerEnter(JsonData data) {

    }
    void SendPokes(JsonData data){

    }
    void SendPokesButtonOnOrOff(bool on) {

    }
    // Use this for initialization
    void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

