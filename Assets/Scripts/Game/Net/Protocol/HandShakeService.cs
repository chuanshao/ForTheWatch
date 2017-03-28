using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
public class HandShakeService
{
    private Protocol protocol;
    private Action<JsonData> callback;

    public const string Version = "0.3.0";
    public const string Type = "unity-socket";


    public HandShakeService(Protocol protocol)
    {
        this.protocol = protocol;
    }

    public void request(JsonData user, Action<JsonData> callback)
    {
        string userString = JsonMapper.ToJson(BuildMsg(user));
        byte[] body = Encoding.UTF8.GetBytes(userString);
        protocol.Send(PackageType.PKG_HANDSHAKE, body);

        this.callback = callback;
    }

    internal void invokeCallback(JsonData data)
    {
        //Invoke the handshake callback
        if (callback != null) callback.Invoke(data);
    }

    public void ack()
    {
        protocol.Send(PackageType.PKG_HANDSHAKE_ACK, new byte[0]);
    }

    private JsonData BuildMsg(JsonData user)
    {
        if (user == null) user = new JsonData("{}");

        JsonData msg = new JsonData();

        //Build sys option
        JsonData sys = new JsonData();
        sys["version"] = Version;
        sys["type"] = Type;

        //Build handshake message
        msg["sys"] = sys;
        msg["user"] = user;

        return msg;
    }
}