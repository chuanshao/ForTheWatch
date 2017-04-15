using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using LitJson;

/// <summary>
/// 这个类是前端与后端交互的入口，主要负责接口请求，服务器数据回调，以及服务器状态转发。
/// StartService 开启服务
/// </summary>
public class SocketManager : MonoBehaviour{
    private SocketClient _client;
    private static SocketManager _instance;
    private string _host;
    private int _port;
    private Action _connectedCallBack;
    public static SocketManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Loom l = Loom.Current;
                _instance = Singleton.Instance.AddComponent<SocketManager>();
            }
            return _instance;
        }
    }
    public void StartService(string host , int port , Action callback = null)
    {
        this._connectedCallBack = callback;
        this._host = host;
        this._port = port;
        _client = new SocketClient();
        _client.NetWorkStateChangedEvent += this.NetWorkStateChanged;
        _client.initClient(host, port , ()=> {
            _client.Connect(null, data =>
            {
                Debug.Log("on data back" + data.ToString());
                JsonData msg = new JsonData();
                msg["uid"] = 1234456;
                Request("gate.gateHandler.queryEntry", msg, OnQuery);
            });
        });
    }
    private void NetWorkStateChanged(NetWorkState state)
    {
        Debug.Log(state);
        switch (state)
        {
            case NetWorkState.CONNECTED:
                //提示连接成功
                break;
            case NetWorkState.CLOSED:
                break;
            case NetWorkState.DISCONNECTED:
                break;
            case NetWorkState.ERROR:
                break;
            case NetWorkState.TIMEOUT:
                //提示连接 超时是否从新连接
                break;
            case NetWorkState.CONNECTING:
                //显示加载进度条
                break;
        }
    }
    private void OnQuery(JsonData result)
    {
        if (result.GetValue("code" , 0) == 200)
        {
            _client.Disconnect();

            string host = result.GetValue("host", "");
            int port = result.GetValue("port", 0);
            _client = new SocketClient();
            _client.NetWorkStateChangedEvent += this.NetWorkStateChanged;
            _client.initClient(host, port, () =>
            {
                _client.Connect(null, (data) =>
                {
                    if (_connectedCallBack != null)
                    {
						Loom.QueueOnMainThread(delegate {
							_connectedCallBack.Invoke();
						});
                    }
                });
            });
        }
    }
    private JsonData emptyMsg = new JsonData();
    public void Request(string route, Action<JsonData> action)
    {
        this.Request(route, emptyMsg, action);
    }

    public void Request(string route, JsonData msg, Action<JsonData> action)
    {
        this._client.Request(route, msg , action);
    }

    public void Notify(string route, JsonData msg)
    {
        this._client.Notify(route, msg);
    }

    public void On(string eventName, Action<JsonData> action)
    {
        this._client.On(eventName, action);
    }
}
