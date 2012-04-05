using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolarTicTacToe.Models
{
    public class Move
    {
        public int UserID;
        public Tuple<int, int> position;
        public DateTime time;
    }
}