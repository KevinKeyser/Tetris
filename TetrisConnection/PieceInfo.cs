using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TetrisConnection
{
    [DataContract]
    public class PieceInfo
    {
        [DataMember]
        public int PieceID;

        [DataMember]
        public int OwnerID;

        [DataMember]
        public int?[][] PackedColors;
    }
}
