using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System;
using LitJson;
/// <summary>
/// network state enum
/// </summary>
public enum NetWorkState
{
    CLOSED,

    CONNECTING,

    CONNECTED,

    DISCONNECTED,

    TIMEOUT,

    ERROR
}

public class SocketClient : IDisposable
{
    /// <summary>
    /// netwrok changed event
    /// </summary>
    public event Action<NetWorkState> NetWorkStateChangedEvent;


    private NetWorkState netWorkState = NetWorkState.CLOSED;   //current network state

    private EventManager eventManager;
    private Socket socket;
    private Protocol protocol;
    private bool disposed = false;
    private uint reqId = 1;

    private ManualResetEvent timeoutEvent = new ManualResetEvent(false);
    private int timeoutMSec = 8000;    //connect timeout count in millisecond

    public SocketClient()
    {
    }

    /// <summary>
    /// initialize socket client
    /// </summary>
    /// <param name="host">server name or server ip (www.xxx.com/127.0.0.1/::1/localhost etc.)</param>
    /// <param name="port">server port</param>
    /// <param name="callback">socket successfully connected callback(in network thread)</param>
    public void initClient(string host, int port, Action callback = null)
    {
        timeoutEvent.Reset();
        eventManager = new EventManager();
        NetWorkChanged(NetWorkState.CONNECTING);

        IPAddress ipAddress = null;

        try
        {
            IPAddress[] addresses = Dns.GetHostEntry(host).AddressList;
            foreach (var item in addresses)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = item;
                    break;
                }
            }
        }
        catch (Exception e)
        {
            NetWorkChanged(NetWorkState.ERROR);
            return;
        }

        if (ipAddress == null)
        {
            throw new Exception("can not parse host : " + host);
        }

        this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ie = new IPEndPoint(ipAddress, port);

        socket.BeginConnect(ie, new AsyncCallback((result) =>
        {
            try
            {
                this.socket.EndConnect(result);
                this.protocol = new Protocol(this, this.socket);
                NetWorkChanged(NetWorkState.CONNECTED);
                if (callback != null)
                {
                    callback();
                }
            }
            catch (SocketException e)
            {
                if (netWorkState != NetWorkState.TIMEOUT)
                {
                    NetWorkChanged(NetWorkState.ERROR);
                }
                Dispose();
            }
            finally
            {
                timeoutEvent.Set();
            }
        }), this.socket);

        if (timeoutEvent.WaitOne(timeoutMSec, false))
        {
            if (netWorkState != NetWorkState.CONNECTED && netWorkState != NetWorkState.ERROR)
            {
                NetWorkChanged(NetWorkState.TIMEOUT);
                Dispose();
            }
        }
    }

    /// <summary>
    /// 网络状态变化
    /// 跳转到主线程
    /// </summary>
    /// <param name="state"></param>
    private void NetWorkChanged(NetWorkState state)
    {
        netWorkState = state;

        if (NetWorkStateChangedEvent != null)
        {
            Loom.QueueOnMainThread(() =>
            {
                NetWorkStateChangedEvent(state);
            });
        }
    }

    public void Connect()
    {
        Connect(null, null);
    }

    public void Connect(JsonData user)
    {
        Connect(user, null);
    }

    public void Connect(Action<JsonData> handshakeCallback)
    {
        Connect(null, handshakeCallback);
    }

    public bool Connect(JsonData user, Action<JsonData> handshakeCallback)
    {
        try
        {
            protocol.start(user, handshakeCallback);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
        }
    }

    private JsonData emptyMsg = new JsonData();
    public void Request(string route, Action<JsonData> action)
    {
        this.Request(route, emptyMsg, action);
    }

    public void Request(string route, JsonData msg, Action<JsonData> action)
    {
        this.eventManager.AddOnceEvent(reqId, action);
        protocol.Send(route, reqId, msg);

        reqId++;
    }

    public void Notify(string route, JsonData msg)
    {
        protocol.Send(route, msg);
    }

    public void On(string eventName, Action<JsonData> action)
    {
        eventManager.AddEvent(eventName, action);
    }

    internal void processMessage(Message msg)
    {
        Loom.QueueOnMainThread(() => {
            if (msg.MType == MessageType.MSG_RESPONSE)
            {
                //msg.data["__route"] = msg.route;
                //msg.data["__type"] = "resp";
                eventManager.InvokeOnCallBack(msg.MId, msg.Msg);
            }
            else if (msg.MType == MessageType.MSG_PUSH)
            {
                //msg.data["__route"] = msg.route;
                //msg.data["__type"] = "push";
                eventManager.InvokeOnEvent(msg.Route, msg.Msg);
            }
        });
    }

    public void Disconnect()
    {
        Dispose();
        NetWorkChanged(NetWorkState.DISCONNECTED);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // The bulk of the clean-up code
    protected virtual void Dispose(bool disposing)
    {
        if (this.disposed)
            return;

        if (disposing)
        {
            // free managed resources
            if (this.protocol != null)
            {
                this.protocol.Close();
            }

            if (this.eventManager != null)
            {
                this.eventManager.Dispose();
            }

            try
            {
                this.socket.Shutdown(SocketShutdown.Both);
                this.socket.Close();
                this.socket = null;
            }
            catch (Exception)
            {
                
            }

            this.disposed = true;
        }
    }
}
