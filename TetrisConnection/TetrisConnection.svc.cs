using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TetrisConnection
{
    public class TetrisConnection : ITetrisConnection
    {
        public string[] GetPlayers(int matchID)
        {
            throw new NotImplementedException();
        }

        public void AddPlayer(int matchID)
        {
            throw new NotImplementedException();
        }
    }
}
