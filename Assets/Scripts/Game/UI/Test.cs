using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
public class Test : GuiBase
{
    public Button SendBtn;
    public Text ShowMessage;
    public InputField Input;
    public InputField Uid;
    public Button AddBtn;
    private void Awake()
    {
        _uiAnimation = new PopUpAnimation();
    }
    // Use this for initialization
    void Start()
    {
        SocketManager.Instance.On("onChat", delegate (JsonData data)
        {
            var content = data.GetValue("content", "");
            ShowMessage.text += content;
            Debug.Log(JsonMapper.ToJson(data));
        });
        SendBtn.onClick.AddListener(delegate ()
        {
            JsonData jdata = new JsonData();
            jdata["userId"] = Uid.text;
            jdata["channelName"] = "111111";
            jdata["content"] = Input.text;
            SocketManager.Instance.Request("chat.chatHandler.send" ,jdata , delegate(JsonData data) {
                Debug.Log(JsonMapper.ToJson(data));
            });
        });
        AddBtn.onClick.AddListener(delegate ()
        {
            JsonData entryData = new JsonData();
            entryData["userId"] = Uid.text;
            SocketManager.Instance.Request("connector.entryHandler.entry", entryData, delegate (JsonData data) {
                Debug.Log("connector.entryHandler.entry" + JsonMapper.ToJson(data));
                JsonData jdata = new JsonData();
                jdata["userId"] = Uid.text;
                jdata["roomName"] = "111111";
                SocketManager.Instance.Request("chat.chatHandler.add", jdata, delegate (JsonData oData) {
                    Debug.Log(JsonMapper.ToJson(oData));
                });
            });
        });
    }

    public override void OnEnter()
    {
        _uiAnimation.MoveAnimation(this, new Vector2(1920, 0), Vector2.zero);
    }
    public override void OnExit()
    {
        GameObject.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
