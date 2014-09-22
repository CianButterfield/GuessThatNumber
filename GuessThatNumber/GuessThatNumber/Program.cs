using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessThatNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            //greet the user
            Greet();
            GuessThatNumber();
            //keep the console open
            Console.ReadKey();
        }

        static void Greet()
        {
            Console.Title = "Guess That Number";
            //greets the user and gives instructions
            Console.WriteLine("Hello,\nIn this game you will be trying to guess a number between one and one hundred.\nYou will be given feedback about your guesses as you go.\nPlease type in a number between one and one hundred for each guess.");
            Console.WriteLine("\nYou may now begin guessing");
        }

        static void GuessThatNumber()
        {
            //plays the game with the user
            //create the random number generator
            Random rng = new Random();
            //choose a random number
            int answer = rng.Next(1,101);

            //declare a variable for whether the user has won
            bool hasWon = false;
            //declare a varible forfthe number of guesses
            int guesses = 0;

            //loop to play the game
            while (!hasWon)
            {
                //get the users guess and convert to an int
                string input = Console.ReadLine();
                int guess = int.Parse(input);
                
                //check for conditions
                if (guess == answer)
                {
                    guesses++;
                    hasWon = true;
                    Console.Clear();
                    Console.WriteLine("Good job, you have won. The number was " + answer + ".\nIt took you " + guesses + " guesses to find it.");
                    
                }
                else if (guess < answer)
                {
                    guesses++;
                    Console.WriteLine("\nI'm sorry, that is incorrect. " + guess + " was too low, please guess again.");
                }
                else
                {
                    guesses++;
                    Console.WriteLine("\nI'm sorry, that is incorrect. " + guess + " was too high, please guess again.");
                }
            }
            //add high score
            AddHighScore(guesses);
            //give the user a moment to read
            System.Threading.Thread.Sleep(2500);
            //display highscores
            DisplayHighScores();
            
        }

        static void AddHighScore(int playerScore)
        {
            //get player name for high score
            Console.Write("Your name: "); string playerName = Console.ReadLine();

            //create a gateway to the database
            CianEntities db = new CianEntities();
            
            //create a new high score object
            // fill it with our user's data
            HighScore newHighScore = new HighScore();
            newHighScore.DateCreated = DateTime.Now;
            newHighScore.Game = "Guess That Number";
            newHighScore.Name = playerName;
            newHighScore.Score = playerScore;

            //add it to the database
            db.HighScores.Add(newHighScore);
            
            //save our changes
            db.SaveChanges();
        }

        static void DisplayHighScores()
        {
            //clear the console
            Console.Clear();
            Console.Title = "ΦGuess That Number High ScoresΦ";
            Console.WriteLine("Guess That Number High Scores");
            Console.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡");

            //create a new connection to the database
            CianEntities db = new CianEntities();
            //get the high score list
            List<HighScore> highScoreList = db.HighScores.Where(x => x.Game == "Guess That Number").OrderBy(x => x.Score).Take(10).ToList();

            foreach (HighScore highScore in highScoreList)
            {
                Console.WriteLine("{0}. {1} - {2}", highScoreList.IndexOf(highScore) + 1, highScore.Name, highScore.Score);
            }
        }
    }
}
