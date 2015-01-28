using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TetrisConnection
{
    [DataContract]
    public class AccountInfo
    {
        [DataMember]
        public int AccountID;

        [DataMember]
        public Guid ExternalAccountID;

        [DataMember]
        public string Username;

        [DataMember]
        public string FirstName;

        [DataMember]
        public string LastName;

        [DataMember]
        public int? CustomPieceID;

        [DataMember]
        public int? CurrentMatchID;

        [DataMember]
        public UserStatus UserStatus;

        [DataMember]
        public DateTime? LastLoggedIn;
    }
}