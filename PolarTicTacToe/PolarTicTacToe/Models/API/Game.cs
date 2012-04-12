using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolarTicTacToe.Models.API
{
    public class Game
    {
        public int ChallengerID;
        public int? OpponentID;
        public string Moves;
        public int? WinnerID;
        public string GameState;
        public long? curAppRequest;
        public int PendingPlayerID;
        public int ID;
    }
}