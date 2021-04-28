// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using WotoProvider.Enums;

namespace LTW.GameObjects.WMath
{
    public class Randomic : IRandomable
    {
        //-------------------------------------------------
        #region Properties Region
        public Random TheRand { get; set; }
        public Range[] Ranges { get; }
        //-------------------------------------------------
        public int Length { get; }
        public uint RandomizingNum { get; set; }
        public float[] Chances { get; }
        //-------------------------------------------------
        public Chance_Error Chance_Error { get; }
        #endregion
        //-------------------------------------------------
        #region field's Region
        private static Random _rand;
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        /// <summary>
        /// Create a new instance of the <see cref="Randomic"/>.
        /// </summary>
        /// <param name="ranges">
        /// An array of the <see cref="Range"/>.
        /// </param>
        /// <param name="chances"></param>
        /// <param name="chance_Error"></param>
        public Randomic(Range[] ranges, float[] chances, Chance_Error chance_Error)
        {
            if(ranges.Length != chances.Length)
            {
                throw new Exception();
            }
            else
            {
                Length = ranges.Length;
            }
            int go = 0;
            for(int i = 0; i < chances.Length; i++)
            {
                go += (int)((int)chance_Error * chances[i]);
            }
            if (go != (int)chance_Error) 
            {
                throw new Exception();
            }
            Ranges = ranges;
            Chances = chances;
            Chance_Error = chance_Error;
            RandomizingNum = (uint)DateTime.Now.Millisecond;
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods
        public int GetNumber()
        {
            int[] myInt = new int[(int)Chance_Error], 
                myChances = new int[Chances.Length];
            for(int i = 0; i < myChances.Length; i++)
            {
                myChances[i] = (int)(((int)Chance_Error) * Chances[i]);
            }
            int theJ = 0, latestJ;
            for (int i = 0; i < myChances.Length; i++) 
            {
                for(latestJ = theJ; theJ < latestJ + myChances[i] ; theJ++)
                {
                    myInt[theJ] = i;
                }
            }
            TheRand = new Random(DateTime.Now.Millisecond + 
                theJ % (DateTime.Now.Second != 0 ? DateTime.Now.Second : 1) + 
                Convert.ToInt32(RandomizingNum % 
                (2 * (DateTime.Now.Second != 0 ? DateTime.Now.Second : 1))));
            RandomizingNum += 10;
            if (RandomizingNum >= int.MaxValue)
            {
                RandomizingNum = 0;
            }
            int current = TheRand.Next(0, myInt.Length);
            current = myInt[current];
            Range currentRange = Ranges[current];
            return TheRand.Next(currentRange.Minimum, currentRange.Maximum);
        }
        public int GetNumber(int min, int max)
        {
            if(TheRand is null)
            {
                TheRand = new Random();

            }
            if (min > max)
            {
                return TheRand.Next(max, min);
            }
            else
            {
                return TheRand.Next(min, max);

            }
        }
        public int Next(int min, int max)
        {
            TheRand = new Random((int)((RandomizingNum * DateTime.Now.Millisecond + 10) % 
                (DateTime.Now.Second + 1)));
            RandomizingNum += (uint)DateTime.Now.Second;
            if (RandomizingNum >= int.MaxValue)
            {
                RandomizingNum = 0;
            }
            if (min > max)
            {
                return TheRand.Next(max, min);
            }
            else
            {
                return TheRand.Next(min, max);

            }
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static int GetRandom(int left, int right, bool leftClose = true, bool rightClose = true)
        {
            if (_rand is null)
            {
                _rand = new Random();
            }
            if (!leftClose)
            {
                left++;
            }
            if (rightClose)
            {
                right++;
            }
            if (left > right)
            {
                var temp = right;
                right = left;
                left = temp;
            }
            return _rand.Next(left, right);
        }
        #endregion
        //-------------------------------------------------
    }
}
