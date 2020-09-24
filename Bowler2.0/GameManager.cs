using System;
using System.Collections.Generic;
using System.Text;

namespace Bowler2._0
{
    // GameManager - class controls the interaction with the user for each frame in the game
    class GameManager
    {
        // Specify max number of frames for a game
        private readonly int maxFrames = 10;

        // Private methods
        // Verify that the user entered a number between 0 and 10 for the pin count value
        private bool ValidateData(String uInput)
        {
            bool validPinCount = true;
            int pCount;

            // Validate that input is a number between 1 and 10
            try
            {
                pCount = Convert.ToInt32(uInput);

                if(pCount > 10 || pCount < 0)
                {
                    validPinCount = false;
                }
            }
            catch (FormatException)
            {
                validPinCount = false;
            }

            return validPinCount;
        }
        
        // Get the pin count from each attempt
        // Validate the user input and prompt for re-entry if pin count is not between 0 and 10
        private int PinCount(int attempt)
        {
            String userInput;
            int retries = 0;

            do
            {
                if(retries == 0)
                {
                    Console.WriteLine("Enter pin count for attempt {0}", attempt);
                }
                else
                {
                    Console.WriteLine("The entered values must be a number between 0 and 10, please re-enter pin count for attempt {0}", attempt);
                }

                userInput = Console.ReadLine();

                retries++;
            }
            while (!ValidateData(userInput));
            
            return Convert.ToInt32(userInput);
        }

        // Public Methods
        // NewGame -Manage the frames for the game and the attempts for each frame
        // Shows final results at the end of the game
        public void NewGame()
        {
            // Create ScoreManager object to record scoring info for each frame
            ScoreManager frameScores = new ScoreManager();

            // Record the scores for each frame in the game
            for (int i = 1; i <= maxFrames; i++)
            {
                int maxAttempts;
                
                Console.WriteLine("For frame {0}", i);

                // Specify the number of attempts for each frame
                // Frames 1 - 9 get 2 attempts and frame 10 can have 3
                if (i < maxFrames)
                {
                    maxAttempts = 2;
                }
                else
                {
                    maxAttempts = 3;
                }

                // Record attempts for each frame
                for (int n = 1; n <= maxAttempts; n++)
                {
                    // Get pin count from the attempt
                    int pins = PinCount(n);

                    // Record the attempt and pin count
                    try
                    {
                        frameScores.AddAttempt(i, n, pins);
                    }
                    catch(InvalidOperationException e)
                    {
                        Console.WriteLine("{0}, please reenter the pin count", e.Message);  // if frame error occurs, prompt for re-enter of attempt 2
                        pins = PinCount(n);
                        frameScores.AddAttempt(i, n, pins);
                    }

                    // Limit attempts in frames 1 - 9 if strike occurs on attempt 1
                    // Limit attempts for non-strike or non-spare in frame 10
                    if((i < maxFrames) && (frameScores.frames[i].HasStrike == true))
                    {
                        break;
                    }
                    else if((i == maxFrames) && (frameScores.frames[i].HasStrike == false) && (frameScores.frames[i].HasSpare == false) && (n == 2))
                    {
                        break;
                    }
                }

                // Display the pin count for the current frame
                Console.WriteLine("Total pins this frame {0}", frameScores.frames[i].TotalPins);
                Console.WriteLine("");
            }

            // Display the scores for each frame
            frameScores.ShowScores();
            Console.WriteLine("");

            // Display the final score for the game
            Console.WriteLine("Final score for the game {0}", frameScores.GetFinalScore());
        }
    }
}
