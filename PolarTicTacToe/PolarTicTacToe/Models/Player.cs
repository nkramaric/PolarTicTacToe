using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolarTicTacToe.Models
{
    public partial class Player
    {
        internal static Player Get(long facebookID)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            return (from p in dataContext.Players where p.FacebookID == facebookID select p).FirstOrDefault();
        }
    }
}