using System;
using System.Collections.Generic;
using System.Text;

namespace Bowler2._0
{
    // ScoreManager - class that records the results for the frames in a game
    class ScoreManager
    {
        private int finalScore;
        internal Frame[] frames = new Frame[11];

        // Constructor -- initialize the array with Frame objects
        public ScoreManager()
        {
            for(int i = 0; i < frames.Length; i++)
            {
                frames[i] = new Frame(i);
            }
        }

        // Private methods
        // UpdateFrameScores - cycle through the frames and recalculate scores for each frame
        private void UpdateFrameScores()
        {
            for(int i = 1; i < frames.Length; i++)
            {
                if((i < 9) && (frames[i].HasStrike == true))
                {
                    if(frames[i + 1].HasStrike == false)
                    {
                        frames[i].Score = 10 + frames[i + 1].Roll1 + frames[i + 1].Roll2;
                    }
                    else
                    {
                        frames[i].Score = 10 + frames[i + 1].Roll1 + frames[i + 2].Roll1;
                    }
                }
                else if ((i == 9) && (frames[i].HasStrike == true))
                {
                     frames[i].Score = 10 + frames[i + 1].Roll1 + frames[i + 1].Roll2;
                }
                else if ((i == 10) && (frames[i].HasStrike == true))
                {
                    frames[i].Score = frames[i].TotalPins;
                }
                else if ((i < 10) && (frames[i].HasSpare == true))
                {
                    frames[i].Score = 10 + frames[i + 1].Roll1;
                }
                else if ((i == 10) && (frames[i].HasSpare == true))
                {
                    frames[i].Score = 10 + frames[i].Roll3;
                }
                else
                {
                    frames[i].Score = frames[i].TotalPins;
                }
            }
        }
        
        //Public Methods
        // AddAttempt - Records the attempt and its results for a specified frame
        public void AddAttempt(int fNum, int aNum, int pCount)
        {
            if (aNum == 1)
            {
                frames[fNum].Roll1 = pCount;
            }
            else if (aNum == 2)
            {
                frames[fNum].Roll2 = pCount;
            }
            else if (aNum == 3)
            {
                frames[fNum].Roll3 = pCount;
            }

            // Have the current frame update after the most recent attempt
            frames[fNum].RefreshFrame();

            // Update the score to show most recent attempt
            UpdateFrameScores();
        }

        // GetFinalScore - returns the score earned during the game
        public int GetFinalScore()
        {
            // Reset the score to zero
            finalScore = 0;

            foreach (Frame f in frames)
            {
                finalScore += f.Score;
            }

            return finalScore;
        }

        // ShowScores - lists the score from each frame to the console window
        public void ShowScores()
        {
            foreach (Frame f in frames)
            {
                if (f.FrameNumber >= 1 && f.FrameNumber <= 10)
                {
                    Console.WriteLine("Frame {0} score was {1}", f.FrameNumber, f.Score);
                }  
            }
        }
    }
}
