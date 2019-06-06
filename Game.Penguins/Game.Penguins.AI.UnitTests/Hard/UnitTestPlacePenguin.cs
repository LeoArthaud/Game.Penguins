using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Game.Penguins.AI.UnitTests.Hard
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguin : GlobalFunctions
    {
        /// <summary>
        /// Test if the function return a table of 10 cells
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_getThreePoints()
        {
            // Init game
            CustomGame customGame = InitGame(null);

            // Init AI Hard
            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer);

            // Test
            Assert.IsTrue(aiHard.GetThreePoints().Count == 10);
        }

        /// <summary>
        /// Test if there are cells with 1 point around the origin cell
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_getFishNear()
        {
            // Init Game
            CustomGame customGame = InitGame(null);

            // Init AI Hard
            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            IList<Coordinates> fishThreePointsList = new List<Coordinates>();

            // Set origin cell with 3 points
            Cell cellThreePoints = (Cell)customGame.Board.Board[1, 1];
            cellThreePoints.FishCount = 3;
            Coordinates coordinates = new Coordinates(1, 1);

            // Set up right cell of the origin cell
            Cell cellUpRight = (Cell)customGame.Board.Board[2, 0];
            cellUpRight.FishCount = 1;

            // Set right cell of the origin cell
            Cell cellRight = (Cell)customGame.Board.Board[2, 1];
            cellRight.FishCount = 1;

            // Set down right cell of the origin cell
            Cell cellDownRight = (Cell)customGame.Board.Board[2, 2];
            cellDownRight.FishCount = 1;

            // Set down left cell of the origin cell
            Cell cellDownLeft = (Cell)customGame.Board.Board[1, 2];
            cellDownLeft.FishCount = 1;

            // Set left cell of the origin cell
            Cell cellLeft = (Cell)customGame.Board.Board[0, 1];
            cellLeft.FishCount = 1;

            //Set up left cell of the origin cell
            Cell cellUpLeft = (Cell)customGame.Board.Board[1, 0];
            cellUpLeft.FishCount = 1;

            // Add cell with 3 points to the list
            fishThreePointsList.Add(coordinates);

            // Launch function
            IList<Coordinates> list = aiHard.GetFishNear(fishThreePointsList);

            // Test
            Assert.IsTrue(list[0].X == 0 && list[0].Y == 1);
            Assert.IsTrue(list[1].X == 2 && list[1].Y == 1);
            Assert.IsTrue(list[2].X == 1 && list[2].Y == 0);
            Assert.IsTrue(list[3].X == 2 && list[3].Y == 0);
            Assert.IsTrue(list[4].X == 1 && list[4].Y == 2);
            Assert.IsTrue(list[5].X == 2 && list[5].Y == 2);
        }

        /// <summary>
        /// Test if there are not cells with 1 point around the origin cell
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_getFishNearEmpty()
        {
            // Init game
            CustomGame customGame = InitGame(null);

            // Init AI Hard
            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            IList<Coordinates> fishThreePointsList = new List<Coordinates>();

            // Set cell origin with three points
            Cell cellThreePoints = (Cell)customGame.Board.Board[1, 1];
            cellThreePoints.FishCount = 3;
            Coordinates coordinates = new Coordinates(1, 1);

            // Set others cells
            SetFish(customGame);

            // Add cell with 3 points to the list
            fishThreePointsList.Add(coordinates);

            // Launch function
            IList<Coordinates> list = aiHard.GetFishNear(fishThreePointsList);

            // Test
            Assert.IsTrue(list.Count == 0);
        }

        /// <summary>
        /// Test if there is no cells with 3 points on the board
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_isNearFalse()
        {
            // Mock 1;0 then 2;2
            Mock<IRandom> mock = new Mock<IRandom>();
            mock.SetupSequence(e => e.Next(0, 8))
                .Returns(1)
                .Returns(0)
                .Returns(2)
                .Returns(2);
            //Init Game
            CustomGame customGame = InitGame(null);
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Set all cells of the board with 2 points
                    Cell cell2 = (Cell)customGame.Board.Board[i,j];
                    cell2.FishCount = 2;
                }
                // Set 8 cells of the board with 1 points
                Cell cell = (Cell)customGame.Board.Board[i, i];
                cell.FishCount = 1;
            }

            // Launch function with mock
            AIHard aiHard = new AIHard(customGame.Board, mock.Object, customGame.CurrentPlayer);
            Coordinates coordinates = aiHard.PlacePenguin();

            // Test
            Assert.IsTrue(coordinates.X == 2 && coordinates.Y == 2);

        }

        #region Private Functions
        
        /// <summary>
        /// Set some cells of the board
        /// </summary>
        /// <param name="customGame">instance of game</param>
        /// <returns>custom game with modifications</returns>
        private void SetFish(CustomGame customGame)
        {
            // Set cell up right of origin
            Cell cellUpRight = (Cell)customGame.Board.Board[2, 0];
            cellUpRight.FishCount = 2;
            cellUpRight.CellType = CellType.FishWithPenguin;

            // Set cell right of origin
            Cell cellRight = (Cell)customGame.Board.Board[2, 1];
            cellRight.FishCount = 2;
            cellRight.CellType = CellType.FishWithPenguin;

            // Set cell down right of origin
            Cell cellDownRight = (Cell)customGame.Board.Board[2, 2];
            cellDownRight.FishCount = 2;
            cellDownRight.CellType = CellType.FishWithPenguin;

            // Set cell down left of origin
            Cell cellDownLeft = (Cell)customGame.Board.Board[1, 2];
            cellDownLeft.FishCount = 2;
            cellDownLeft.CellType = CellType.FishWithPenguin;

            // Set cell left of origin
            Cell cellLeft = (Cell)customGame.Board.Board[0, 1];
            cellLeft.FishCount = 2;
            cellLeft.CellType = CellType.FishWithPenguin;

            // Set cell up left of origin
            Cell cellUpLeft = (Cell)customGame.Board.Board[1, 0];
            cellUpLeft.FishCount = 2;
            cellUpLeft.CellType = CellType.FishWithPenguin;

        }


        #endregion
    }
}
