using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LTW.Constants;

namespace LTW.LoadingService
{
    [Serializable]
    public class AccountInfo
    {
        private int profilesCount;
        private byte[] lastLogin;
        public AccountInfo() : this(0, 
            ThereIsConstants.AppSettings.GlobalTiming.GetString(false).GetValue())
        {

        }
        public AccountInfo(int profilesCountValue, string lastLoginValue)
        {
            ProfilesCount = profilesCountValue;
            LastLogin = lastLoginValue;
        }
        public int ProfilesCount
        {
            get
            {
                return profilesCount;
            }
            set
            {
                if(value >= 0)
                {
                    profilesCount = value;
                }
                else
                {
                    profilesCount = 0;
                }
            }
        }
        public string LastLogin
        {
            get
            {
                return Encoding.UTF8.GetString(lastLogin);
            }
            set
            {
                if (value == null)
                {
                    lastLogin = Encoding.ASCII.GetBytes(DateTime.Now.ToString());
                }
                else
                {
                    lastLogin = Encoding.ASCII.GetBytes(value);
                }
            }
        }
        //-------------------------------------------
        public static AccountInfo FromFile(string filePath)
        {
            FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            AccountInfo myInfo = null; // = (AccountInfo)formatter.Deserialize(myFile);
            myFile.Close();
            myFile.Dispose();
            return myInfo;
        }
        /// <summary>
        /// Consider this function will update the <see cref="AccountInfo"/> object in the
        /// <see cref="ThereIsConstants.Path.AccountInfo_File_Path"/>.
        /// </summary>
        /// <param name="myInfo"></param>
        /// <param name="filePath"></param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void UpdateInfo(AccountInfo myInfo, string filePath)
        {
            FileStream myFile = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            //formatter.Serialize(myFile, myInfo);
            myFile.Close();
            myFile.Dispose();
        }
        /// <summary>
        /// Consider this function will update the <see cref="AccountInfo"/> object in the
        /// <see cref="ThereIsConstants.Path.AccountInfo_File_Path"/>.
        /// </summary>
        /// <param name="myInfo">
        /// The object of <see cref="AccountInfo"/> that should be Serialized in the 
        /// file.
        /// </param>
        /// <param name="myFile"></param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void UpdateInfo(AccountInfo myInfo, FileStream myFile)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            //formatter.Serialize(myFile, myInfo);
            myFile.Close();
            myFile.Dispose();
        }
        //-------------------------------------------
    }
}
