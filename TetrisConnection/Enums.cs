using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TetrisConnection
{
    public enum GameMode
    {
        Classic     = 1,
        TimeAttack  = 2
    }

    public enum UserStatus
    {
        Offline     = 1,
        Online      = 2,
        Away        = 3,
        Hidden      = 4,
        Spectating  = 5,
        InLobby     = 6,
        InGame      = 7
    }
}