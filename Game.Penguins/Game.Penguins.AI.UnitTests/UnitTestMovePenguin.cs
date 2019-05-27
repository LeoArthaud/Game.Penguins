using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMovePenguin
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIEasy()
        {
            CustomGame customGame = InitGame();
            customGame.PlacePenguin();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.Move();

            bool isMove = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin && customGame.Board.Board[i, j].CurrentPenguin.Player == customGame.Players[0])
                    {
                        isMove = true;
                    }
                }
            }
            Assert.IsTrue(isMove);
        }
        
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIMedium()
        {
            CustomGame customGame = InitGame();
            customGame.CurrentPlayer = customGame.Players[1];
            customGame.PlacePenguin();
            customGame.CurrentPlayer = customGame.Players[1];
            customGame.Move();

            bool isMove = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin && customGame.Board.Board[i,j].CurrentPenguin.Player == customGame.Players[1])
                    {
                        isMove = true;
                    }
                }
            }
            Assert.IsTrue(isMove);
        }
        
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIHard()
        {
            CustomGame customGame = InitGame();
            customGame.CurrentPlayer = customGame.Players[2];
            customGame.PlacePenguin();
            customGame.CurrentPlayer = customGame.Players[2];
            customGame.Move();

            bool isMove = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin && customGame.Board.Board[i, j].CurrentPenguin.Player == customGame.Players[2])
                    {
                        isMove = true;
                    }
                }
            }
            Assert.IsTrue(isMove);
        }

        #region Private Functions

        public CustomGame InitGame()
        {
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 3 players
            customGame.AddPlayer("Player1", PlayerType.AIEasy);
            customGame.AddPlayer("Player2", PlayerType.AIMedium);
            customGame.AddPlayer("Player3", PlayerType.AIHard);

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
