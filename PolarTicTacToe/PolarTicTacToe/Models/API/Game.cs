using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolarTicTacToe.Models.API
{
    public class Game
    {
        public int ChallengerID;
        public long? ChallengerFBID;
        public string ChallengerFirstName;
        public string ChallengerLastName;
        public int? OpponentID;
        public long? OpponentFBID;
        public string OpponentFirstName;
        public string OpponentLastName;
        public List<Move> Moves;
        public int? WinnerID;
        public string GameState;
        public long? curAppRequest;
        public int ID;
        public int? PendingPlayerID;
        public long? PendingPlayerFBID;
    }
}