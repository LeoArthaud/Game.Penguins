﻿using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    /// <summary>
    /// Test the function PlacePenguin()
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguin
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_AIEasy()
        {
            CustomGame customGame = InitGame();
            customGame.PlacePenguin();

            bool isPlace = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin)
                    {
                        isPlace = true;
                    }
                }
            }
            Assert.IsTrue(isPlace);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_AIMedium()
        {
            CustomGame customGame = InitGame();
            customGame.CurrentPlayer = customGame.Players[1];
            customGame.PlacePenguin();

            bool isPlace = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin)
                    {
                        isPlace = true;
                    }
                }
            }
            Assert.IsTrue(isPlace);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_AIHard()
        {
            CustomGame customGame = InitGame();
            customGame.CurrentPlayer = customGame.Players[2];
            customGame.PlacePenguin();

            bool isPlace = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin)
                    {
                        isPlace = true;
                    }
                }
            }
            Assert.IsTrue(isPlace);
        }

        //    /// <summary>
        //    /// Test, in the function PlacePenguin(), if the type of the cell and the current penguin on the cell is change
        //    /// </summary>
        //    [TestMethod]
        //    public void Test_PlacePenguin_CellStatus()
        //    {
        //        // Init game
        //        Mock<IRandom> randomMock = new Mock<IRandom>();
        //        randomMock.SetupSequence(e => e.Next(0, 8)).Returns(0);
        //        randomMock.SetupSequence(e => e.Next(0, 4)).Returns(0).Returns(1);

        //        CustomGame customGame = InitGame(randomMock);

        //        var playerInitial = customGame.Players[0];

        //        // Position of cell
        //        int x = 0;
        //        int y = 0;

        //        Cell cell = (Cell)customGame.Board.Board[x, y];
        //        cell.FishCount = 1;

        //        // Launch function
        //        customGame.PlacePenguin();

        //        // Tests si le penguin a bien été placé sur la cellule
        //        Assert.IsTrue(customGame.Board.Board[x, y].CellType == CellType.FishWithPenguin);
        //        Assert.IsTrue(customGame.Board.Board[x, y].FishCount == 1);
        //        Assert.IsTrue(customGame.Board.Board[x, y].CurrentPenguin.Player == playerInitial);
        //    }

        //    /// <summary>
        //    /// Test, in the function PlacePenguin(), if the number of penguins decrease when a AI place a penguin on the board
        //    /// </summary>
        //    [TestMethod]
        //    public void Test_PlacePenguin_NumberPenguinDecrease()
        //    {
        //        // Init game
        //        Mock<IRandom> randomMock = new Mock<IRandom>();
        //        randomMock.SetupSequence(e => e.Next(0, 8)).Returns(0);
        //        randomMock.SetupSequence(e => e.Next(0, 4)).Returns(0).Returns(1);
        //        CustomGame customGame = InitGame(randomMock);

        //        // Position of cell
        //        int x = 0;
        //        int y = 0;

        //        Cell cell = (Cell)customGame.Board.Board[x, y];
        //        cell.FishCount = 1;

        //        // Launch function
        //        customGame.PlacePenguin();

        //        // Test if decrease of number penguins work
        //        Assert.IsTrue(customGame.Players[0].Penguins == 3);
        //    }

        //    /// <summary>
        //    /// Test, in the function PlacePenguin(), if the CurrentPlayer is changed at the end of the function
        //    /// </summary>
        //    [TestMethod]
        //    public void Test_PlacePenguin_ChangeCurrentPlayer()
        //    {
        //        // Init game
        //        Mock<IRandom> randomMock = new Mock<IRandom>();
        //        randomMock.SetupSequence(e => e.Next(0, 8)).Returns(0);
        //        randomMock.SetupSequence(e => e.Next(0, 4)).Returns(0).Returns(1);
        //        CustomGame customGame = InitGame(randomMock);

        //        // Position of cell
        //        int x = 0;
        //        int y = 0;

        //        Cell cell = (Cell)customGame.Board.Board[x, y];
        //        cell.FishCount = 1;

        //        // Launch function
        //        customGame.PlacePenguin();

        //        // Test if CurrentPlayer is changed
        //        Assert.IsTrue(customGame.CurrentPlayer == customGame.Players[1]);
        //    }

        //    /// <summary>
        //    /// Test, in the function PlacePenguin(), if the CurrentPlayer doesn't have all his penguins on the board, the NextAction is to PlacePenguin
        //    /// </summary>
        //    [TestMethod]
        //    public void Test_PlacePenguin_NextActionPlace()
        //    {
        //        // Init game
        //        Mock<IRandom> randomMock = new Mock<IRandom>();
        //        randomMock.SetupSequence(e => e.Next(0, 8)).Returns(0);
        //        randomMock.SetupSequence(e => e.Next(0, 4)).Returns(0).Returns(1);

        //        CustomGame customGame = InitGame(randomMock);

        //        // Position of cell
        //        int x = 0;
        //        int y = 0;

        //        Cell cell = (Cell)customGame.Board.Board[x, y];
        //        cell.FishCount = 1;

        //        // Launch function
        //        customGame.PlacePenguin();

        //        Assert.IsTrue(customGame.NextAction == NextActionType.PlacePenguin);
        //    }

        //    /// <summary>
        //    /// Test, in the function PlacePenguin(), if the CurrentPlayer has all his penguins on the board, the NextAction is to MovePenguin
        //    /// </summary>
        //    [TestMethod]
        //    public void Test_PlacePenguin_NextActionMove()
        //    {
        //        // Init game
        //        Mock<IRandom> randomMock = new Mock<IRandom>();
        //        randomMock.SetupSequence(e => e.Next(0, 8))
        //                    .Returns(0)
        //                    .Returns(0)
        //                    .Returns(1)
        //                    .Returns(1)
        //                    .Returns(2)
        //                    .Returns(2)
        //                    .Returns(3)
        //                    .Returns(3)
        //                    .Returns(4)
        //                    .Returns(4)
        //                    .Returns(5)
        //                    .Returns(5)
        //                    .Returns(6)
        //                    .Returns(6)
        //                    .Returns(7)
        //                    .Returns(7);
        //        randomMock.SetupSequence(e => e.Next(0, 4)).Returns(0).Returns(1);
        //        CustomGame customGame = InitGame(randomMock);

        //        // Position of cell
        //        for (int i = 0; i < 8; i++)
        //        {
        //            int x = i;
        //            int y = i;
        //            Cell cell = (Cell)customGame.Board.Board[x, y];
        //            cell.FishCount = 1;
        //        }

        //        // Launch function
        //        while (customGame.CurrentPlayer.Penguins != 0)
        //        {
        //            customGame.PlacePenguin();
        //        }

        //        Assert.IsTrue(customGame.CurrentPlayer.Penguins == 0);
        //        Assert.IsTrue(customGame.NextAction == NextActionType.MovePenguin);
        //    }

        #region Private Functions

        public CustomGame InitGame(/*Mock<IRandom> randomMock*/)
        {
            CustomGame customGame = new CustomGame(/*randomMock.Object*/new AppRandom());

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
