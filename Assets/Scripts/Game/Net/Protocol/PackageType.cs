using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PackageType
{
    PKG_HANDSHAKE = 1,
    PKG_HANDSHAKE_ACK = 2,
    PKG_HEARTBEAT = 3,
    PKG_DATA = 4,
    PKG_KICK = 5
}
