using Xunit;
using System;
using RockPaperScissorsApp;

namespace RockPaperScissorsApp.UnitTests
{
    public class RockPaperScissorsGame_test
    {
        private readonly RockPaperScissorsGame _game;

        public RockPaperScissorsGame_test()
        {
            _game = new RockPaperScissorsGame();
        }

        private static bool IsValidRPSThrow(int rpsThrow)
        {
            int[] testThrowNames = { 0, 1, 2 };
            return Array.IndexOf(testThrowNames, rpsThrow) > -1;
        }

        private static bool checkTheInput(int trial, int result) => trial < 99 && IsValidRPSThrow(result);

        [Fact]
        public void Is_GetComputerShot_tested()
        {
            int trials = 0;
            var result = 0;

            for (trials = 0; checkTheInput(trials, result); trials++)
            {
                result = RockPaperScissorsGame.GetComputerShot();
                Assert.True(IsValidRPSThrow(result),
                            $"This Throw is {result} and {RockPaperScissorsGame.GetShotName(result)}.");
                //Console.WriteLine($"This Throw is {result} and {RockPaperScissorsGame.GetShotName(result)}.");
            }

            Assert.True(trials >= 99, "There should have been 99 trials.");
        }

        [Fact]
        public void Is_GetWinner_Tested()
        {
            int[][] winAttemptOne = new int[][]
                {
                    new int [2] { RockPaperScissorsGame.Rock, RockPaperScissorsGame.Paper },
                    new int [2] { RockPaperScissorsGame.Paper, RockPaperScissorsGame.Scissors },
                    new int [2] { RockPaperScissorsGame.Scissors, RockPaperScissorsGame.Rock }
                };

            int[][] lossAttemptOne = new int[][]
                {
                    new int [2] { RockPaperScissorsGame.Paper, RockPaperScissorsGame.Rock },
                    new int [2] { RockPaperScissorsGame.Scissors, RockPaperScissorsGame.Paper },
                    new int [2] { RockPaperScissorsGame.Rock, RockPaperScissorsGame.Scissors }
                };

            int[][] tie = new int[][]
                {
                    new int[2] { RockPaperScissorsGame.Rock, RockPaperScissorsGame.Rock}
                };

            EvaluateWinnerScore(winAttemptOne, RockPaperScissorsGame.Player);
            EvaluateWinnerScore(lossAttemptOne, RockPaperScissorsGame.Computer);
            EvaluateWinnerScore(tie, RockPaperScissorsGame.Tie);
        }

        private static void EvaluateWinnerScore(int[][] attempt, int expectedScore)
        {
            int playerOneThrow = 0, playerTwoThrow = 0;

            for (int i = 0; i < attempt.Length; i += 1)
            {
                Console.WriteLine($"Index: {i}");

                playerOneThrow = attempt[i][0];
                playerTwoThrow = attempt[i][1];

                var testValue = RockPaperScissorsGame.GetWinner(playerOneThrow, playerTwoThrow);
                Console.WriteLine($"RPS Score: {testValue}");

                Assert.True(testValue == expectedScore, $"Score was {testValue} the expected value is {expectedScore}.");
            }
        }

        [Fact]
        public void Is_ShowFinalScore_Tested()
        {
            
        }
    }
}