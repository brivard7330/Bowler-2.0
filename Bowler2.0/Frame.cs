using System;
using System.Collections.Generic;
using System.Text;

namespace Bowler2._0
{
    // Frame - class that records the results for a single frame
    class Frame
    {
        private int frameNumber;
        private int roll1;
        private int roll2;
        private int roll3;
        private bool hasStrike;
        private bool hasSpare;
        private int totalPins;
        private int score;

        // Constructor -- builds empty frame object
        public Frame(int fNum)
        {
            frameNumber = fNum;

            roll1 = 0;
            roll2 = 0;
            roll3 = 0;

            hasStrike = false;
            hasSpare = false;

            totalPins = roll1 + roll2 + roll3;
            score = 0;
        }

        // Properties to retrieve frame values
        public int FrameNumber { get => frameNumber; set => frameNumber = value; }
        public int Roll1 { get => roll1; set => roll1 = value; }
        public int Roll2 { get => roll2; set => roll2 = value; }
        public int Roll3 { get => roll3; set => roll3 = value; }

        public bool HasStrike { get => hasStrike; }
        public bool HasSpare { get => hasSpare; }
        
        public int TotalPins { get => totalPins; set => totalPins = value; }
        public int Score { get => score; set => score = value; }

        // Public Methods
        // RefreshFrame - enables caller to update frame properties after a frame is completed
        public void RefreshFrame()
        {
            if (roll1 == 10)
            {
                hasStrike = true;
            }
            else
            {
                hasStrike = false;
            }

            if ((roll1 < 10) && (roll1 + roll2 == 10))
            {
                hasSpare = true;
            }
            else
            {
                hasSpare = false;
            }

            totalPins = roll1 + roll2 + roll3;

            // Verify the total pins for the frame does not exceed the max for the frame
            if((frameNumber < 10) && (totalPins > 10))
            {
                throw new InvalidOperationException("The total pins cannot be greater than 10");
            }
        }
    }
}
