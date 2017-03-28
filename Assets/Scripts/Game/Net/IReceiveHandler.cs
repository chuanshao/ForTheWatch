using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IReceiveHandler
{
    void HandlerBytes(byte[] dataArr);
    void OnError(string erroCode);
}
