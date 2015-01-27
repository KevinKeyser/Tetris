using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConnection
{
    [DataContract]
    public class Spectator
    {
        [DataMember]
        public int SpectatorID;

        [DataMember]
        public int MatchID;

        [DataMember]
        public int AccountID;

        [DataMember]
        public DateTime TimeStarted;

        [DataMember]
        public DateTime? TimeEnded;
    }
}
