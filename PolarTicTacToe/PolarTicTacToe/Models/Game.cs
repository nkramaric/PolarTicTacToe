using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PolarTicTacToe.Utils;
using System.Globalization;

namespace PolarTicTacToe.Models
{
    public partial class Game
    {
        internal static Game Get(int id)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            return (from p in dataContext.Games where p.ID == id select p).FirstOrDefault();
        }

        internal static bool PlayMove(int id, int player, Tuple<int, int> spot, out int? winner, out string message)
        {
            PolarTicTacToeDataContext dataContext = new PolarTicTacToeDataContext();

            var game = (from p in dataContext.Games where p.ID == id select p).FirstOrDefault();
            var gameRules = new GameRules();

            if (gameRules.HasPlayed(game, spot))
            {
                message = "Spot already played";
                winner = null;
                return false;
            }
            else
            {
                string move = spot.Item1 + ";" + spot.Item2 + ";" + String.Format("{0:g}", DateTime.Now) + ","; 
                game.Moves = game.Moves ?? "";
                game.Moves += move;
                

                if (gameRules.IsFinished(game, out winner))
                {
                    winner = player;
                    game.WinnerID = winner;
                    game.GameState = Utils.GameState.Finished.ToString();
                }

                dataContext.SubmitChanges();
                message = "";

                return true;
            }

        }


        internal List<Move> MoveList
        {
            get
            {
                if (this.Moves == null)
                {
                    return new List<Move>();
                }

                CultureInfo provider = CultureInfo.InvariantCulture;
                var splitMoves = Moves.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
                var MoveList = new List<Move>();
                int? userID = ChallengerID;

                foreach (var curMove in splitMoves)
                {
                    string[] splitMove = curMove.Split(';');
                    int x = int.Parse(splitMove[0]);
                    int y = int.Parse(splitMove[1]);
                    DateTime time = DateTime.Parse(splitMove[2], provider);
                    MoveList.Add(new Move() { position = new Coordinate(x, y), time = time, UserID = userID.Value });
                    userID = userID == ChallengerID ? OpponentID : ChallengerID;
                }

                return MoveList;
            }
        }


        public int PendingPlayerID 
        { 
            get 
            {
                if (String.IsNullOrWhiteSpace(Moves))
                {
                    return ChallengerID;
                }
                else
                {
                    return MoveList.Last().UserID == ChallengerID ? OpponentID.Value : ChallengerID;
                }
            } 
        }

        public Move GetPosition(int x, int y)
        {
            Move item;

            if ((item = MoveList.FirstOrDefault(p => p.position.X == x && p.position.Y == y)) == null)
            {
                item = new Move() { position = new Coordinate(x, y), UserID = null };
            }

            return item;
        }

        public int? this[int x, int y]
        {
            get
            {
                return GetPosition(x, y).UserID;
            }
        }

        public int? this[Coordinate coordinate]
        {
            get
            {
                return GetPosition(coordinate.X, coordinate.Y).UserID;
            }
        }

    }
}