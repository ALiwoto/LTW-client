using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LTW.Security;
using LTW.Constants;
using LTW.GameObjects.Players;

namespace LTW.LoadingService
{
    /// <summary>
    /// You should Serialize object of this class in 
    /// <see cref="ThereIsConstants.Path.ProfileInfo_File_Path"/> which is in
    /// <see cref="ThereIsConstants.Path.Profile_Folder_Path"/>.
    /// </summary>
    [Serializable]
    public sealed class ProfileInfo
    {
        //-------------------------------------------------
        #region Constant's Region
        public const string ToStringValue = "Profile Info -- LTW || " +
            "BY: wotoTeam && ALi.w";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// The Username value.
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (value != null)
                {
                    _username = value;
                }
                else
                {
                    _username = ThereIsConstants.Path.NotSet;
                }
            }
        }
        /// <summary>
        /// The Token Value,
        /// Notice: this is the personal token that this client has Generated,
        /// this is not the Token for Entering.
        /// </summary>
        public string TheToken
        {
            get
            {
                return _theToken;
            }
            set
            {
                if (value != null)
                {
                    _theToken = value;
                }
                else
                {
                    _theToken = ThereIsConstants.Path.NotSet;
                }
            }
        }
        /// <summary>
        /// The last login value.
        /// Notice: this is last login value of this client, not the profile itself,
        /// so look, this last login should be set when player clicked on:
        /// Link Start.
        /// </summary>
        public string LastLogin
        {
            get
            {
                return _lastLogin;
            }
            set
            {
                if (value != null)
                {
                    _lastLogin = value;
                }
                else
                {
                    _lastLogin =
                        ThereIsConstants.AppSettings.GlobalTiming.GetForServer().GetValue();
                }
            }
        }
        public UID UID
        {
            get
            {
                if (_uid == null)
                {
                    return UID.GenerateNullUID();
                }
                return UID.Parse(QString.Parse(_uid, true).GetValue());
            }
            private set
            {
                if (value != null)
                {
                    _uid = QString.Parse(value.GetForServer()).GetString();
                }
            }
        }
        #endregion
        //-------------------------------------------------
        #region field's Region
        private string _username;
        private string _theToken;
        private string _lastLogin;
        private string _uid;
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        /// <summary>
        /// use this for Generatinga new ProfileInfo.
        /// </summary>
        /// <param name="_u">
        /// this value will set the <see cref="UserName"/> property.
        /// </param>
        /// <param name="_t">
        /// this value will set the <see cref="TheToken"/> property.
        /// </param>
        /// <param name="_lL">
        /// this value will set the <see cref="LastLogin"/> property.
        /// </param>
        public ProfileInfo(string _u, string _t, string _lL, UID _uid_)
        {
            UserName = _u;
            TheToken = _t;
            LastLogin = _lL;
            UID = _uid_;
        }
        #endregion
        //-------------------------------------------------
        #region overrided Region
        public override string ToString()
        {
            return ToStringValue;
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static ProfileInfo FromFile(string filePath)
        {
            FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            ProfileInfo myInfo = null;// = (ProfileInfo)formatter.Deserialize(myFile);
            //test = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ProfileInfo));
            myFile.Close();
            myFile.Dispose();
            return myInfo;
        }
        public static void UpdateInfo(ProfileInfo myInfo, string filePath)
        {
            FileStream myFile = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            //formatter.Serialize(myFile, myInfo);
            myFile.Close();
            myFile.Dispose();
        }
        #endregion
        //-------------------------------------------------
    }
}
