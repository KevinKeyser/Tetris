using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConnection
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public int SenderID;

        [DataMember]
        public int GroupID;

        [DataMember]
        public int MessageID;

        [DataMember]
        public string MessageString;

        [DataMember]
        public DateTime TimeSent;

        [DataMember]
        public DateTime? DeletionDate;
    }
}
