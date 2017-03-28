//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//public class SendPackageController
//{
//    public static SendPackageController _instance;
//    public static SendPackageController GetInstance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                _instance = new SendPackageController();
//            }
//            return _instance;
//        }
//    }
//    public void RegisterHandler(ReceiveHandler handler)
//    {
//        SocketManager.GetInstance.RegisterListener(handler.GetKeyCode(), handler.ReceiveCenter);
//    }
//    public void SendPackage(PackageBytes packageHandler , ReceiveHandler handler)
//    {
//        SocketManager.GetInstance.PushNormalBytes(packageHandler.ToBytes());
//        SocketManager.GetInstance.RegisterListener(handler.GetKeyCode(), handler.ReceiveCenter);
//    }
//}
