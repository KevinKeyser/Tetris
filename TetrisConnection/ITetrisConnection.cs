using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TetrisConnection
{
    [ServiceContract]
    public interface ITetrisConnection
    {
        [OperationContract]
        string[] GetPlayers(int matchID);
        
        [OperationContract]
        void AddPlayer(int matchID);


    }
}
