using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Message
{
    public MessageType MType;
    public UInt32 MId;
    public string Route;
    public JsonData Msg;
    public Message(MessageType type, uint id, string route, JsonData msg)
    {
        this.MType = type;
        this.MId = id;
        this.Route = route;
        this.Msg = msg;
    }
}
