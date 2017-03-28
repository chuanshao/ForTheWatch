using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using UnityEngine;
using System.Net.Sockets;

public enum ProtocolState
{
    Start,
    Working,
    HandShaking,
    Closed
}
public class Protocol
{
    private SocketClient _client;
    private ProtocolState _state = ProtocolState.Closed;
    private HeartBeatService _heartBeatService;//心跳服务
    private MessageProtocol _messageProtocol;
    private Transporter _transporter;
    private HandShakeService _handshake;
    public Protocol(SocketClient client , Socket socket)
    {
        this._client = client;
        this._transporter = new Transporter(socket, this.ProcessMessage);
        this._transporter.onDisconnect = OnDisconnect;

        this._handshake = new HandShakeService(this);
        this._state = ProtocolState.Start;
    }
    internal void start(JsonData user, Action<JsonData> callback)
    {
        this._transporter.start();
        this._handshake.request(user, callback);

        this._state = ProtocolState.HandShaking;
    }
    private void OnDisconnect()
    {
        this._client.Disconnect();
    }


    public void ProcessMessage(byte[] bytes)
    {
        Package pkg = PackageProtocol.decode(bytes);

        //Ignore all the message except handshading at handshake stage
        if (pkg.type == PackageType.PKG_HANDSHAKE && this._state == ProtocolState.HandShaking)
        {

            //Ignore all the message except handshading
            JsonData data = JsonMapper.ToObject(Encoding.UTF8.GetString(pkg.body));

            processHandshakeData(data);

            this._state = ProtocolState.Working;

        }
        else if (pkg.type == PackageType.PKG_HEARTBEAT && this._state == ProtocolState.Working)
        {
            this._heartBeatService.resetTimeout();
        }
        else if (pkg.type == PackageType.PKG_DATA && this._state == ProtocolState.Working)
        {
            this._heartBeatService.resetTimeout();
            this._client.processMessage(_messageProtocol.Decode(pkg.body));
        }
        else if (pkg.type == PackageType.PKG_KICK)
        {
            this.getPomeloClient().Disconnect();
            this.Close();
        }
    }
    public SocketClient getPomeloClient()
    {
        return this._client;
    }
    private void processHandshakeData(JsonData msg)
    {
        //Handshake error
        if (!msg.Keys.Contains("code") || !msg.Keys.Contains("sys") || msg.GetValue("code" , 0) != 200)
        {
            throw new Exception("Handshake error! Please check your handshake config.");
        }
        //Set compress data
        JsonData sys = (JsonData)msg["sys"];

        JsonData dict = new JsonData();
        if (sys.Keys.Contains("dict")) dict = (JsonData)sys["dict"];

        JsonData protos = new JsonData();
        JsonData serverProtos = new JsonData();
        JsonData clientProtos = new JsonData();

        if (sys.Keys.Contains("protos"))
        {
            protos = (JsonData)sys["protos"];
            serverProtos = (JsonData)protos["server"];
            clientProtos = (JsonData)protos["client"];
        }

        _messageProtocol = new MessageProtocol(dict, serverProtos, clientProtos);

        //Init heartbeat service
        int interval = 0;
        if (sys.Keys.Contains("heartbeat")) interval = Convert.ToInt32(sys["heartbeat"]);
        _heartBeatService = new HeartBeatService(interval , this);
        if (interval > 0)
        {
            _heartBeatService.start();
        }

        //send ack and change protocol state
        _handshake.ack();
        this._state = ProtocolState.Working;

        //Invoke handshake callback
        JsonData user = new JsonData();
        if (msg.Keys.Contains("user")) user = (JsonData)msg["user"];
        _handshake.invokeCallback(user);
    }
    //Send notify, do not need id
    internal void Send(string route, JsonData msg)
    {
        Send(route, 0, msg);
    }

    internal void Send(PackageType type)
    {
        if (this._state == ProtocolState.Closed) return;
        _transporter.send(PackageProtocol.encode(type));
    }

    public void Send(string route, uint id , JsonData msg)
    {
        if (this._state != ProtocolState.Working) return;

        byte[] body = _messageProtocol.Encode(route, id, msg);

        Send(PackageType.PKG_DATA, body);
    }
    internal void Send(PackageType type, JsonData msg)
    {
        //This method only used to send system package
        if (type == PackageType.PKG_DATA) return;

        byte[] body = Encoding.UTF8.GetBytes(JsonMapper.ToJson(msg));

        Send(type, body);
    }
    internal void Send(PackageType type, byte[] body)
    {
        if (this._state == ProtocolState.Closed) return;

        byte[] pkg = PackageProtocol.encode(type, body);

        _transporter.send(pkg);
    }
    internal void Close()
    {
        _transporter.close();

        if (_heartBeatService != null) _heartBeatService.stop();

        this._state = ProtocolState.Closed;
    }
}
