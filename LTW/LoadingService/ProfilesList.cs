using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace LTW.LoadingService
{
    [SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
    /// <summary>
    /// profileName, lastLogin, description.
    /// </summary>
    public class ProfilesList
    {
        private const int Char_Array_Length = 400;
        private const int Size_Of_Char = 2;
        //private const int Size_Of_Int32 = 4;
        public const int SIZE = 3 * Size_Of_Char * Char_Array_Length;
        private char[] profileName;
        private char[] lastLogin;
        private char[] description;
        public ProfilesList() : this("", DateTime.Now.ToString(), "")
        {

        }
        public ProfilesList(string profileNameValue, string lastLoginValue, string descriptionValue)
        {
            ProfileName = profileNameValue;
            LastLogin = lastLoginValue;
            Description = descriptionValue;
        }
        public string ProfileName
        {
            get
            {
                return new string(profileName);
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
                profileName = firstnameString.ToCharArray();
            }
        }
        public string LastLogin
        {
            get
            {
                return new string(lastLogin);
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
                lastLogin = firstnameString.ToCharArray();
            }
        }

        
        public string Description
        {
            get
            {
                return new string(description);
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
                description = firstnameString.ToCharArray();
            }
        }
        //--------------------------------------------
        public static ProfilesList FromFile(int pos, string filePath)
        {
            FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(myFile);
            myFile.Position = pos;
            ProfilesList myListingInfo = new ProfilesList()
            {
                ProfileName = reader.ReadString(),
                LastLogin = reader.ReadString(),
                Description = reader.ReadString(),
            };
            myFile.Close();
            reader.Close();
            reader.Dispose();
            myFile.Dispose();
            return myListingInfo;
        }
        public static void UpdateInfo(ProfilesList myProfileListInfo, int pos, string filePath)
        {
            FileStream myFile = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter writer = new BinaryWriter(myFile);
            myFile.Position = pos;
            writer.Write(myProfileListInfo.ProfileName);
            writer.Write(myProfileListInfo.LastLogin);
            writer.Write(myProfileListInfo.Description);
            myFile.Close();
            writer.Close();
            myFile.Dispose();
            writer.Dispose();
            return;
        }
        public static void DeleteInfo(int pos, string filePath)
        {

        }
        //--------------------------------------------
    }
}
