using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//socket数据包
public interface IPackage
{
    byte[] ToBytes();
}
