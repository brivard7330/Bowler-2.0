using System;

namespace Bowler2._0
{
    class Program
    {
        private const string ErrorMessage = "An error occurred while calculating your score, please start again";

        static void Main()
        {
            Console.WriteLine("Let's get started");
            Console.WriteLine("");

            try
            {
                GameManager game = new GameManager();
                game.NewGame();
            }
            catch (Exception)
            {
                Console.WriteLine(ErrorMessage);  // If an error occurs, alert the user
            }

            Console.WriteLine("");
            Console.WriteLine("Your game has ended");
        }
    }
}
