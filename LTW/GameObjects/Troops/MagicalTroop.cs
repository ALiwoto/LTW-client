// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;
using LTW.SandBox;
using LTW.Security;
using LTW.GameObjects.WMath;
using LTW.GameObjects.Players;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Troops
{
    /// <summary>
    /// There is no Saber or Lancer in the MagicalTroops.
    /// </summary>
    public class MagicalTroop
    {
        //-------------------------------------------------
        #region Constant's Region
        /// <summary>
        /// But use this in each type, for separate their parameters.
        /// </summary>
        public const string InCharSeparator = "つ";
        public const string MT_Server_LOC = "_魔法戦士";
        public const string MTROOPS_MESSAGE = "M_T -- LTW";
        public const uint BasicLevel = 1;
        #endregion
        //-------------------------------------------------
        #region static Properties Region
        /// <summary>
        /// The Basic Power in the BasicLevel.
        /// </summary>
        public static Unit BasicPower
        {
            get
            {
                return new Unit(0, 0, 100, 0);
            }
        }
        #endregion
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// The count of Troops in each types,
        /// for example: mrwoto has 10K Saber, 100K Archer, etc...
        /// </summary>
        public Unit Count { get; set; }
        /// <summary>
        /// The level of Troops in each types,
        /// for example: mrwoto has Saber lvl12 (all of his Sabers should be the same level).
        /// </summary>
        public uint Level { get; set; }
        /// <summary>
        /// The Power of Troops in each types,
        /// for example: mrwoto's Sabers Power is 120K (all of the Sabers should have the same power).
        /// </summary>
        public Unit Power { get; set; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        private MagicalTroop(Unit countValue, uint levelValue, Unit powerValue)
        {
            Count = countValue;
            Level = levelValue;
            Power = powerValue;
        }
        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public StrongString GetForServer()
        {
            StrongString myString =
                Count.GetForServer()    + InCharSeparator +
                Level.ToString()        + InCharSeparator +
                Power.GetForServer()    + InCharSeparator;
            return myString;
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        public static MagicalTroop ParseToMagicalTroop(StrongString theString)
        {
            StrongString[] myStrings = theString.Split(InCharSeparator);
            MagicalTroop myMagicalTroop = new MagicalTroop(Unit.ConvertToUnit(myStrings[0]),
                myStrings[1].ToUInt16(), Unit.ConvertToUnit(myStrings[2]));
            return myMagicalTroop;
        }
        public static MagicalTroop GetBasicMagicalTroop()
        {
            return new MagicalTroop(Unit.GetBasicUnit(), BasicLevel,
                BasicPower);
        }
        public static StrongString GetForServer(MagicalTroop troops)
        {
            StrongString myString =
                troops.Count.GetForServer() + InCharSeparator +
                troops.Level.ToString() + InCharSeparator +
                troops.Power.GetForServer() + InCharSeparator;
            return myString;
        }
        /// <summary>
        /// Create player's troops for the first time.
        /// </summary>
        /// <param name="troops"></param>
        public static async Task<DataBaseDataChangedInfo> CreatePlayerMagicalTroops(MagicalTroop troops)
        {
            StrongString myString = GetForServer(troops);
            //---------------------------------------------
            var _me = ThereIsServer.GameObjects.MyProfile;
            var _target = _me.UID.GetValue() + MT_Server_LOC;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_MagicalTroops_Server(_me.Socket);
            var _req = new DataBaseCreation(MTROOPS_MESSAGE, QString.Parse(myString));
            return await ThereIsServer.Actions.CreateData(_s, _target, _req);
            //---------------------------------------------
        }
        /// <summary>
        /// Save Player's troops(Update them to the server.)
        /// </summary>
        /// <param name="troops"></param>
        public static async void SavePlayerTroops(MagicalTroop troops)
        {
            StrongString myString = GetForServer(troops);
            //---------------------------------------------
            var _p = ThereIsServer.GameObjects.MyProfile;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_MagicalTroops_Server(_p.Socket);
            var _target = _p.UID.GetValue() + MT_Server_LOC;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return;
            }
            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(MTROOPS_MESSAGE, QString.Parse(myString), existing.Sha);
            await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
        }
        public static async Task<MagicalTroop> LoadPlayerMagicalTroop()
        {
            //---------------------------------------------
            var _p = ThereIsServer.GameObjects.MyProfile;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_MagicalTroops_Server(_p.Socket);
            var _target = _p.UID.GetValue() + MT_Server_LOC;
            var existingFile = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existingFile.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null;
            }
            return ParseToMagicalTroop(existingFile.Decode());
        }
        public static async Task<bool> DeleteMagicalTroops(UID _uid_)
        {
            //---------------------------------------------
            var _target = _uid_.GetValue() + MT_Server_LOC;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_MagicalTroops_Server(_uid_.TheSocket);
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.DoesNotExist)
            {
                return true;
            }
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return false;
            }
            //---------------------------------------------
            var _req = new DataBaseDeleteRequest(MTROOPS_MESSAGE, existing.Sha.GetStrong());
            return await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
    }
}
