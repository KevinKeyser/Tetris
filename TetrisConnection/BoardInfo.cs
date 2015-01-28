using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConnection
{
    [DataContract]
    public class BoardInfo
    {
        [DataMember]
        public int AccountID;

        [DataMember]
        public int BoardID;

        [DataMember]
        public int BoardWidth;

        [DataMember]
        public int BoardHeight;

        [DataMember]
        public int?[][] PackedColors;

        [DataMember]
        public int? Ranking;

        [DataMember]
        public DateTime? TimeFinished;

        [DataMember]
        public int CurrentPieceID;

        [DataMember]
        public int PieceX;

        [DataMember]
        public int PieceY;

        [DataMember]
        public int Rotation;

        [DataMember]
        public int? HeldPieceID;

        [DataMember]
        public int Combo;

        [DataMember]
        public float Multiplier;

        [DataMember]
        public int Level;
        
        [DataMember]
        public PowerUp PowerUp;

        [DataMember]
        public int[] QueuedPieceID;

        [DataMember]
        public int Score;
    }
}
