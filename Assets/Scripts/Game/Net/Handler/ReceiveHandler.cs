//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//public abstract class ReceiveHandler
//{
//    protected Action<byte[]> _receiveCallbackHandler;
//    protected short _type;
//    protected short _cmd;
//    public ReceiveHandler(short type , short cmd , Action<byte[]> cb)
//    {
//        this._receiveCallbackHandler = cb;
//        this._type = type;
//        this._cmd = cmd;
//    }
//    public string GetKeyCode()
//    {
//        return "";
//    }
//    public abstract void ReceiveCenter(byte[] bytes);
//}
//public class ReceiveNormalHandler : ReceiveHandler
//{
//    private bool _listenerOnce;
//    public ReceiveNormalHandler(short type, short cmd, bool once = false, Action < byte[] > cb = null) : base(type, cmd, cb)
//    {
//        this._listenerOnce = once;
//    }
//    public override void ReceiveCenter(byte[] bytes)
//    {
//        if (this._receiveCallbackHandler != null)
//        {
//            this._receiveCallbackHandler.Invoke(bytes);
//            if (this._listenerOnce)
//            {
//                SocketManager.Instance.RemoveListener(this.GetKeyCode(), this.ReceiveCenter);//完成之后把侦听去掉
//            }
//        }
//    }
//}