using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolarTicTacToe.Models
{
    public partial class Player
    {

        internal static Player Create(long facebookID, string firstName, string lastName)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            Player newPlayer = new Player();

            newPlayer.FacebookID = facebookID;
            newPlayer.FirstName = firstName;
            newPlayer.LastName = lastName;

            dataContext.Players.InsertOnSubmit(newPlayer);

            dataContext.SubmitChanges();

            return newPlayer;
        }

        internal static Player Create(long facebookID)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            Player newPlayer = new Player();

            newPlayer.FacebookID = facebookID;

            dataContext.Players.InsertOnSubmit(newPlayer);

            dataContext.SubmitChanges();

            return newPlayer;
        }

        internal static void Edit(long facebookID, string firstName, string lastName)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            var curPlayer = (from p in dataContext.Players
                             where p.FacebookID == facebookID
                             select p).FirstOrDefault();

            
            curPlayer.FirstName = firstName;
            curPlayer.LastName = lastName;

            dataContext.SubmitChanges();
        }

        internal static Player GetByFBID(long facebookID)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            return (from p in dataContext.Players where p.FacebookID == facebookID select p).FirstOrDefault();
        }

        internal static Player GetByID(long ID)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            return (from p in dataContext.Players where p.ID == ID select p).FirstOrDefault();
        }
    }
}