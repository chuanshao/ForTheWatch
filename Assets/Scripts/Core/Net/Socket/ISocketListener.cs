using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ISocketListener
{
    void OnConnectStart();
    void OnConnectEnd();
    void OnReceiveData(byte[] bytes);
}
