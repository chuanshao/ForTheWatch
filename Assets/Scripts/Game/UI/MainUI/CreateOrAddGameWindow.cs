using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class CreateOrAddGameWindow : BaseWindow
{
    public Button CreateGame;
    public Button AddGame;
    public Button CloseBtn;
    private void Awake()
    {
        CreateGame.onClick.AddListener(OnCreateGameClick);
        AddGame.onClick.AddListener(OnAddGameClick);
        CloseBtn.onClick.AddListener(OnCloseClick);
    }
    void OnCreateGameClick() {
        SocketManager.Instance.Request("game.gameHandler.createGame", OnCreateGameBack);
    }
    void OnCloseClick() {

    }
    void OnAddGameClick() {
        Ass.Instance.GetPrefab("ui/AddScenePanel", delegate (GameObject go)//弹出输入房间号窗口
        {
            GameObject iGo = GameObject.Instantiate(go);
            Game.script.uiCanvas.OpenGui(iGo.GetComponent<GuiBase>());
        });
    }
    /// <summary>
    /// 创建游戏返回 
    /// </summary>
    /// <param name="jdata"></param>
    void OnCreateGameBack(JsonData jdata) {
        Debug.Log(JsonMapper.ToJson(jdata));
        Ass.Instance.GetPrefab("ui/PlaySceneUI", delegate (GameObject go)//进入游戏
        {
            GameObject iGo = GameObject.Instantiate(go);
            Game.script.uiCanvas.OpenGui(iGo.GetComponent<GuiBase>());
        });
    }
    private void Start()
    {

    }
}