using LTW.Security;
using LTW.GameObjects.ServerObjects;
using WotoProvider.Interfaces;

namespace LTW.LoadingService
{
#pragma warning disable IDE0044
    /// <summary>
    /// ServerInfo
    /// </summary>
    public sealed class ServerInfo : IServerProvider<QString, DataBaseClient>, ISecurity
    {
        //-------------------------------------------------
        #region Constants Region
        public const string ToStringValue = "Server Info -- wotoTeam Cor. |" +
            "BY Ali.w && mrwoto";
        public const string SecureToString = ToStringValue + " ++ Licensed BY Ali.w ++";
        private const string CharSeparator1 = ";";
        private const string CharSeparator2 = "-";
        private const string CharSeparator3 = " ";
        private const string CharSeparator4 = "\t";
        private const int M_INDEX0 = 0;
        private const int I_INDEX0 = M_INDEX0;
        private const int I_INDEX1 = 1;
        private const int I_INDEX2 = 2;
        private const int I_INDEX3 = 3;
        private const int I_INDEX4 = 4;
        #endregion
        //-------------------------------------------------
        #region fields Region
        private DataBaseClient serverClient;
        private static ServerInfo _invalid;
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public QString ProductHeaderValue { get; }
        public QString Token { get; }
        public QString Owner { get; }
        public QString Repo { get; }
        public QString Branch { get; }
        public bool IsInvalid { get; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        /// <summary>
        /// Create a new instance of the <see cref="ServerInfo"/>.
        /// </summary>
        /// <param name="productHeaderValue">
        /// the header of the product.
        /// </param>
        /// <param name="tokenValue">
        /// the token needed to login to the database.
        /// </param>
        /// <param name="ownerValue">
        /// the name of the owner of the database.
        /// </param>
        /// <param name="repoValue">
        /// the name of the repo.
        /// </param>
        /// <param name="branchValue">
        /// the name of the branch which you want to save the data into it.
        /// </param>
        private ServerInfo(StrongString productHeaderValue, StrongString tokenValue,
            StrongString ownerValue, StrongString repoValue, StrongString branchValue)
        {
            ProductHeaderValue  = QString.Parse(productHeaderValue);
            Token               = QString.Parse(tokenValue);
            Owner               = QString.Parse(ownerValue);
            Repo                = QString.Parse(repoValue);
            Branch              = QString.Parse(branchValue);
            serverClient        = 
                new DataBaseClient(new DataBaseHeader(ProductHeaderValue), Token);
        }
        private ServerInfo()
        {
            IsInvalid = true;
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region
        /// <summary>
        /// Get the Client of the database.
        /// </summary>
        /// <returns></returns>
        public DataBaseClient GetClient()
        {
            return serverClient;
        }
        /// <summary>
        /// return the string which contains the information
        /// about this class.
        /// </summary>
        /// <param name="value">
        /// if true,in addition to get the general informations, 
        /// you will get the informations about license.
        /// </param>
        /// <returns>information</returns>
        public StrongString ToString(bool value)
        {
            if (value)
            {
                return SecureToString;
            }
            return ToStringValue;
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static ServerInfo Parse(StrongString value)
        {
            // we just need the first index of it.
            StrongString myString = value.Split(CharSeparator1)[M_INDEX0];
            StrongString[] myStrings = 
                myString.Split(CharSeparator2, CharSeparator3, CharSeparator4);
            return new ServerInfo(
                    myStrings[I_INDEX0],
                    myStrings[I_INDEX1],
                    myStrings[I_INDEX2],
                    myStrings[I_INDEX3],
                    myStrings[I_INDEX4]
                    );
        }
        public static ServerInfo GetInvalid()
        {
            if (_invalid is null)
            {
                _invalid = new ServerInfo();
            }
            return _invalid;
        }
        #endregion
        //-------------------------------------------------
        #region operator's Region
        public static implicit operator ServerInfo(string v)
        {
            return Parse(v);
        }
        #endregion
        //-------------------------------------------------
    }
#pragma warning restore IDE0044
}
