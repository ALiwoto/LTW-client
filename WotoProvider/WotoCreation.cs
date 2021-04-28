
namespace WotoProvider
{
    public class WotoCreation
    {
        //-------------------------------------------------
        #region Constants Region
        private const string defaultProcedural =
            "-- { = + ## _ woto _ ## + = } --";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public string Procedural { get; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private WotoCreation()
        {
            Procedural = defaultProcedural;
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        /// <summary>
        /// Generate a new WotoCreation.
        /// </summary>
        /// <returns></returns>
        public static WotoCreation GenerateWotoCreation()
        {
            return new WotoCreation()
            {

            };
        }
        #endregion
        //-------------------------------------------------
    }
}
