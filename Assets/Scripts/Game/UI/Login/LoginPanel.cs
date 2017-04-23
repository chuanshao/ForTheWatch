using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
public class LoginPanel : BaseWindow
{
    public InputField UserName;
    public Button Login;
    void Start()
    {
        Login.onClick.AddListener(delegate ()
        {
            JsonData Jdata = new JsonData();
            Jdata["userId"] = UserName.text;
            MainModel.Instance.User.UserUid = UserName.text;
            SocketManager.Instance.Request("connector.entryHandler.entry", Jdata, delegate (JsonData jdata)
            {
                Game.Instance.GoScene("PlayScene");
            });
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
