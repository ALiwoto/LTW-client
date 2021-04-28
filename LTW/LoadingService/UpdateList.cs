using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW.LoadingService
{
    class UpdateList
    {
        private const int Char_Array_Length = 400;
        private const int Size_Of_Char = 2;
        private const int Size_Of_Int32 = 4;
        public const int SIZE = 2 * Size_Of_Char * Char_Array_Length;
        public const string CurrentDirectoryReplacement = "ALiWotoIm@";
        public const string DataDirectoryReplacement = "Mr.WotoUR@";
        private char[] elementName;
        private char[] elementPath;
        public UpdateList() : this("notSet", "notSet")
        {

        }
        public UpdateList(string elementNameValue, string elementPathValue)
        {
            ElementName = elementNameValue;
            ElementPath = elementPathValue;
        }
        public string ElementName
        {
            get
            {
                return new string(elementName);
            }
            set
            {
                int StringSize = value.Length;
                string firstnameString = value;
                if (Char_Array_Length >= StringSize)
                {
                    firstnameString = value + new string(' ', Char_Array_Length - StringSize);
                }
                else
                {
                    firstnameString = value.Substring(0, Char_Array_Length);
                }
                elementName = firstnameString.ToCharArray();
            }
        }
        public string ElementPath
        {
            get
            {
                return new string(elementPath);
            }
            set
            {
                int StringSize = value.Length;
                string firstnameString = value;
                if (Char_Array_Length >= StringSize)
                {
                    firstnameString = value + new string(' ', Char_Array_Length - StringSize);
                }
                else
                {
                    firstnameString = value.Substring(0, Char_Array_Length);
                }
                elementPath = firstnameString.ToCharArray();
            }
        }
        //-------------------------------------
        public static string Repair(string theValue)
        {
            char[] charValue = theValue.ToCharArray();
            string myString = "";
            for (int i = charValue.Length - 1; i >= 0; i--)
            {
                if (charValue[i] == ' ')
                {
                    if (i != 0)
                    {
                        continue;
                    }
                    else
                    {
                        charValue[i] = '\0';
                    }
                }
                else
                {
                    if (i != (charValue.Length - 1))
                    {
                        charValue[i + 1] = '\0';
                    }
                    break;
                }
            }
            for (int j = 0; j < charValue.Length; j++)
            {
                if (charValue[j] != '\0')
                {
                    myString += charValue[j];
                }
                else
                {
                    break;
                }
            }
            return myString;
        }
        public override string ToString()
        {
            return ElementName +  "@--@" +
                ElementPath;
        }
        //------------------------------------
    }
}
