using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PolarTicTacToe.Utils;

namespace PolarTicTacToe.Models
{
    public class Move
    {
        public int? UserID;
        public Coordinate position;
        public DateTime time;
    }
}