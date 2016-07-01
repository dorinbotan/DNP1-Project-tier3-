using System;                 
using System.ServiceModel;    

namespace Tier3Lib
{
    [ServiceContract (Namespace = "http://Tier3.com")]
    interface ITier3
    {
        [OperationContract]
        bool SendTray(Tray tray);

        [OperationContract]
        bool SignIn(string login, string password);

        [OperationContract]
        bool LogIn(string login, string password);

        [OperationContract]
        int GetLotNumber(string login);
    }
}
