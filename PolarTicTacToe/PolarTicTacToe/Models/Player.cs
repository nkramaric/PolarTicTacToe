using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolarTicTacToe.Models
{
    public partial class Player
    {

        internal static void Create(long facebookID, string firstName, string lastName)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            Player newPlayer = new Player();

            newPlayer.FacebookID = facebookID;
            newPlayer.FirstName = firstName;
            newPlayer.LastName = lastName;

            dataContext.Players.InsertOnSubmit(newPlayer);

            dataContext.SubmitChanges();
        }

        internal static Player Get(long facebookID)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            return (from p in dataContext.Players where p.FacebookID == facebookID select p).FirstOrDefault();
        }
    }
}