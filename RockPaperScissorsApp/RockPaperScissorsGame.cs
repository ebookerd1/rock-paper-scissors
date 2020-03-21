using System;
using System.Linq;

namespace RockPaperScissorsApp
{
    public class RockPaperScissorsGame
    {
        private const string TieGameMessage = "This game was a Tie";
        public const int Rock = 0, Paper = 1, Scissors = 2;
        public const int Player = 1, Computer = 2, Tie = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Rock, Paper, Scissors");

            RockPaperScissorsGame rockPaperScissorsGame = new RockPaperScissorsGame();
            rockPaperScissorsGame.PlayRockPaperScisors();
        }

        public void PlayRockPaperScisors()
        {
            do
            {
                int numberOfRounds = GetNumberOfRounds();
                int[] scoreBoard = { 0, 0, 0 };

                for (int round = 0; round < numberOfRounds; round++)
                {
                    Console.WriteLine($"Round {round + 1}");
                    int playerOne = GetPlayerShot();
                    int computer = GetComputerShot();

                    ShowThrowComparison(playerOne, computer);

                    // Assign points to scoreboard
                    int point = GetWinner(playerOne, computer);

                    scoreBoard = UpdateScoreBoard(scoreBoard, point);

                    ShowWinnerText(point);
                }

                //show Final Score
                ShowFinalScore(scoreBoard);

            } while (PlayAgain());
        }

        private void ShowThrowComparison(int playerOne, int computer)
        {
            string shootName = GetShotName(playerOne);
            Console.WriteLine($"Player One threw '{GetShotName(playerOne)}', the computer threw '{GetShotName(computer)}'");
        }

        public static string GetShotName(int playerThrow)
        {
            switch (playerThrow)
            {
                case Rock:
                    return "Rock";
                case Paper:
                    return "Paper";
                case Scissors:
                    return "Scissors";
            }

            return "";
        }

        /***************************************
        * get number of rounds from user
        ***************************************/
        public static int GetNumberOfRounds()
        {
            int rounds = 0;

            do
            {
                rounds = GetNumericalInput("Please enter the number of rounds you wish to play? 1 to 10");

            } while (rounds < 1 || rounds > 10);

            return rounds;
        }

        public static int GetPlayerShot()
        {
            int hand = -1;
            do
            {
                hand = GetNumericalInput("ROCK, PAPER, SCISSORS, ... SHOOT!!\n\t Rock = 0, Paper = 1, Scissor = 2");
            } while (hand < 0 || hand >= 3);

            return hand;
        }

        public static int GetComputerShot()
        {
            var rand = new Random();
            return rand.Next(0, 3);
        }

        /***************************************
        * winner Rule 
        * The rules of the game are as follows:
        *   Each player chooses Rock, Paper, or Scissors.
        *    If both players choose the same thing, the round is a tie.
        *    Otherwise:
        *        Paper wraps Rock to win
        *        Scissors cut Paper to win
        *        Rock breaks Scissors to win
        *   Return the Winner
        ***************************************/
        public static int GetWinner(int playerOneThrow, int playerTwoThrow)
        {
            int winner = 0;

            if (playerOneThrow == playerTwoThrow)
            {
                winner = 0;
            }
            else if (IsPlayerOneWinningRound(playerOneThrow, playerTwoThrow))
            {
                winner = 1;
            }
            else
            {
                winner = 2;
            }

            return winner;
        }

        public static int[] UpdateScoreBoard(int[] scoreBoard, int point)
        {
            scoreBoard[point]++;
            return scoreBoard;
        }

        public static void ShowWinnerText(int point)
        {
            string text = "";


            if (point == Player)
            {
                text = "Player One";
            }
            else if (point == Computer)
            {
                text = "The Computer";
            }

            if (point == Tie)
            {
                Console.WriteLine(RockPaperScissorsGame.TieGameMessage);
            }
            else
            {
                Console.WriteLine($"The winner of this round is '{text}'.");
            }

        }

        public static void ShowFinalScore(int[] scoreBoard)
        {
            int tie = scoreBoard[Tie];
            int player = scoreBoard[Player];
            int computer = scoreBoard[Computer];

            var message = $"And the Final Tally is...\n{tie} Ties\nThe Computer score is {computer}, and your score is {player}";
            if (player > computer)
            {
                //player wins message
                message = message + "\nYou Win!!!";
            }
            else if (player < computer)
            {
                //Computer wins
                message = message + "\nYou Lose...";
            }
            else
            {
                message = message + "\nThis is unprecidented.... Its a Tie!! Unbelivable.";
            }

            Console.WriteLine(message);
        }

        public static bool PlayAgain()
        {
            string again = "";
            string[] options = { "no", "n", "yes", "y" };

            do
            {
                Console.WriteLine("Would you like to play again (Yes/No)?");
                again = Console.ReadLine();

            } while (!options.Contains(again.ToLower()));

            return again.ToLower().StartsWith("y");
        }

        /***************************************
        * Helper function:
        *  - Arrow function to check if player has met a Single winning combination
        *  Usage: GetWinner
        ***************************************/
        public static bool IsPlayerOneWinningRound(int playerOneThrow, int playerTwoThrow) => (
              (playerOneThrow == Rock && playerTwoThrow == Paper) ||
              (playerOneThrow == Paper && playerTwoThrow == Scissors) ||
              (playerOneThrow == Scissors && playerTwoThrow == Rock)
            );

        /**************************************
        * Helper function:
        *  GetNumericalInput
        *  - function gets number inputs if the input is not a number if will retry 
        *  Usage: GetWinner
        ***************************************/
        private static int GetNumericalInput(string message)
        {
            int rounds = 0;
            bool valid = false;

            do
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out rounds))
                {
                    valid = !valid; //Toggle the state.
                }
                else
                {
                    Console.WriteLine($"Your input '{input}' is not a number please enter a valid number");
                }

            } while (!valid);

            return rounds;
        }

    }
}
