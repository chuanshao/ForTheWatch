using UnityEngine;
using System.Collections;
using System;
using System.Timers;
/// <summary>
/// 心跳规则,发送一个心跳包  添加发送时间，服务器会把这个时间转发回来，成为最后一次接收到心跳包的时间。
/// 如果长时间没有收到心跳包，则认为服务器断开了
/// </summary>
public class HeartBeatService
{
    int interval;
    public int timeout;
    Timer timer;
    DateTime lastTime;

    Protocol protocol;

    public HeartBeatService(int interval, Protocol protocol)
    {
        this.interval = interval * 1000;
        this.protocol = protocol;
    }

    internal void resetTimeout()
    {
        this.timeout = 0;
        lastTime = DateTime.Now;
    }

    public void sendHeartBeat(object source, ElapsedEventArgs e)
    {
        TimeSpan span = DateTime.Now - lastTime;
        timeout = (int)span.TotalMilliseconds;

        //check timeout
        if (timeout > interval * 2)
        {
            protocol.getPomeloClient().Disconnect();
            //stop();
            return;
        }

        //Send heart beat
        protocol.Send(PackageType.PKG_HEARTBEAT);
    }

    public void start()
    {
        if (interval < 1000) return;

        //start hearbeat
        this.timer = new Timer();
        timer.Interval = interval;
        timer.Elapsed += new ElapsedEventHandler(sendHeartBeat);
        timer.Enabled = true;

        //Set timeout
        timeout = 0;
        lastTime = DateTime.Now;
    }

    public void stop()
    {
        if (this.timer != null)
        {
            this.timer.Enabled = false;
            this.timer.Dispose();
        }
    }
}
