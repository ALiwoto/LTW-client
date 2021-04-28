using System;
using LTW.Security;
using LTW.Constants;

namespace LTW.LoadingService
{
    [Serializable]
    public class GameVersion
    {
        private int num1;
        private int num2;
        private int num3;
        private int num4;
        private int lastCode;

        public GameVersion() : this(1, 1, 1, 5014, 5014)
        {
            
        }
        public GameVersion(int num1Value, int num2Value, int num3Value, int num4Value, 
            int lastCodeValue)
        {
            Num1 = num1Value;
            Num2 = num2Value;
            Num3 = num3Value;
            Num4 = num4Value;
            LastCode = lastCodeValue;
        }
        public int Num1
        {
            get
            {
                return num1;
            }
            set
            {
                if(value > 1)
                {
                    num1 = value;
                }
                else
                {
                    num1 = 1;
                }
            }
        }
        public int Num2
        {
            get
            {
                return num2;

            }
            set
            {
                if (value > 1)
                {
                    num2 = value;
                }
                else
                {
                    num2 = 1;
                }
            }
        }
        public int Num3
        {
            get
            {
                return num3;

            }
            set
            {
                if (value > 1)
                {
                    num3 = value;
                }
                else
                {
                    num3 = 1;
                }
            }
        }
        public int Num4
        {
            get
            {
                return num4;

            }
            set
            {
                if (value > 1)
                {
                    num4 = value;
                }
                else
                {
                    num4 = 1;
                }
            }
        }
        public int LastCode
        {
            get
            {
                return lastCode;

            }
            set
            {
                if (value > 1)
                {
                    lastCode = value;
                }
                else
                {
                    lastCode = 1;
                }
            }
        }
        //-------------------------------------
        public const string CharSeparator = ".";
        public StrongString GetForServer()
        {
            return ParseToString(this);
        }
        public static GameVersion ParseToVersion(StrongString verString)
        {
            GameVersion myVer = new GameVersion();
            StrongString[] myString = verString.Split(CharSeparator);
            myVer.Num1      = myString[0].ToInt32();
            myVer.Num2      = myString[1].ToInt32();
            myVer.Num3      = myString[2].ToInt32();
            myVer.Num4      = myString[3].ToInt32();
            myVer.LastCode  = myString[3].ToInt32();
            return myVer;
        }
        public static StrongString ParseToString(GameVersion myVer)
        {
            if(myVer == null)
            {
                return null;
            }
            StrongString myString = 
                myVer.Num1.ToString() + CharSeparator +
                myVer.Num2.ToString() + CharSeparator +
                myVer.Num3.ToString() + CharSeparator +
                myVer.Num4.ToString();
            return myString;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static GameVersion BasicVersion
        {
            get => new GameVersion();
        }
        public static bool operator ==(GameVersion left, GameVersion right)
        {
            if(left is null)
            {
                if(right is null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(right is null)
            {
                if(left is null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if(ParseToString(left) == ParseToString(right))
            {
                return true;
            }
            else if(left.Num1 == right.Num1 && left.Num2 == right.Num2 && left.Num3 == right.Num3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(GameVersion left, GameVersion right)
        {
            if (ParseToString(left) != ParseToString(right))
            {
                return true;
            }
            else if (left.Num1 == right.Num1 && left.Num2 == right.Num2 && left.Num3 == right.Num3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
