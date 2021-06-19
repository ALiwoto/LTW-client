// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
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
    /// There is 4 types of Troops:
    /// Saber, Archer, Lancer, Assassin.
    /// NOTICE: Please do the Parsing in order...
    /// <see cref="Saber"/>, <see cref="Archer"/>, <see cref="Lancer"/>, <see cref="Assassin"/>
    /// </summary>
    public abstract partial class Troop
    {
        //-------------------------------------------------
        #region Constant's Region
        /// <summary>
        /// Use this to separate the heroes type from each other.
        /// </summary>
        public const string OutCharSeparator = "よ";
        /// <summary>
        /// But use this in each type, for separate their parameters.
        /// </summary>
        public const string InCharSeparator = "つ";
        public const string EndFileName = "_菟ループす";
        public const string TROOP_MESSAGE = "P_T -- LTW";
        public const uint BasicLevel = 1;
        #endregion
        //-------------------------------------------------
        #region static Properties Region
        /// <summary>
        /// The Basic Power in the level1.
        /// </summary>
        public static Unit BasicPower 
        { 
            get 
            {
                return new Unit(0, 0, 0, 20);
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
        #region protected Method's Region
        protected virtual StrongString GetForServer()
        {
            StrongString myString =
                    Count.GetForServer() + InCharSeparator +
                    Level.ToString() + InCharSeparator +
                    Power.GetForServer() + InCharSeparator;
            return myString;
        }
        #endregion
        //-------------------------------------------------
        #region static Method's Region
        /// <summary>
        /// Parse the string value to Troops in the following order:
        /// <see cref="Saber"/>, <see cref="Archer"/>, <see cref="Lancer"/>, <see cref="Assassin"/>
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static Troop[] ParseToTroops(StrongString theString)
        {
            Troop[] troops = new Troop[4]; //There is 4 types of Troop.
            StrongString[] myStrings = theString.Split(OutCharSeparator);
            troops[0] = Saber.ParseToSaber(myStrings[0]);
            troops[1] = Archer.ParseToArcher(myStrings[1]);
            troops[2] = Lancer.ParseToLancer(myStrings[2]);
            troops[3] = Assassin.ParseToAssassin(myStrings[3]);
            return troops;
        }
        public static Troop[] GetBasicTroops()
        {
            Troop[] troops = new Troop[4];
            troops[0] = Saber.GetBasicSaber();
            troops[1] = Archer.GetBasicArcher();
            troops[2] = Lancer.GetBasicLancer();
            troops[3] = Assassin.GetBasicAssassin();
            return troops;
        }
        /// <summary>
        /// Get the string value of these Troops for the Server DataBase.
        /// </summary>
        /// <param name="myTroops"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <returns></returns>
        public static StrongString GetForServer(Troop[] myTroops)
        {
            if(myTroops == null)
            {
                throw new ArgumentNullException();
            }
            StrongString myString =
                myTroops[0].GetForServer() + OutCharSeparator + // Saber
                myTroops[1].GetForServer() + OutCharSeparator + // Archer
                myTroops[2].GetForServer() + OutCharSeparator + // Lancer
                myTroops[3].GetForServer() + OutCharSeparator; //Assassin
            return myString;
        }
        /// <summary>
        /// Create player's troops for the first time.
        /// </summary>
        /// <param name="troops"></param>
        public async static Task<DataBaseDataChangedInfo> CreatePlayerTroops(Troop[] troops)
        {
            //---------------------------------------------
            StrongString myString = GetForServer(troops);
            var _p = ThereIsServer.GameObjects.MyProfile;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Troops_Server(_p.Socket);
            var _target = _p.UID.GetValue() + EndFileName;
            var _creation = new DataBaseCreation(TROOP_MESSAGE, QString.Parse(myString));
            return await ThereIsServer.Actions.CreateData(_s, _target, _creation);
            //---------------------------------------------
        }
        /// <summary>
        /// Save Player's troops(Update them to the server.)
        /// </summary>
        /// <param name="troops"></param>
        public static async Task<DataBaseDataChangedInfo> SavePlayerTroops(Troop[] troops)
        {
            var _p = ThereIsServer.GameObjects.MyProfile;
             return await SavePlayerTroops(troops, _p);
        }
        public static async Task<DataBaseDataChangedInfo> SavePlayerTroops(Troop[] troops, Player _p)
        {
            StrongString myString = GetForServer(troops);
            //---------------------------------------------
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Troops_Server(_p.Socket);
            var _target = _p.UID.GetValue() + EndFileName;
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null;
            }
            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(TROOP_MESSAGE, QString.Parse(myString), existing.Sha);
            return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
        }
        public static async Task<Troop[]> LoadPlayerTroops()
        {
            var _p = ThereIsServer.GameObjects.MyProfile;
            return await LoadPlayerTroops(_p);
        }
        public static async Task<Troop[]> LoadPlayerTroops(Player _p)
        {
            //---------------------------------------------
            var _target = _p.UID.GetValue() + EndFileName;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Troops_Server(_p.Socket);
            var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null;
            }
            return ParseToTroops(existing.Decode());
        }
        public static async Task<bool> DeleteTroops(UID _uid_)
        {
            //---------------------------------------------
            var _target = _uid_.GetValue() + EndFileName;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_Troops_Server(_uid_.TheSocket);
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
            var _req = new DataBaseDeleteRequest(TROOP_MESSAGE, existing.Sha.GetStrong());
            return await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
    }
}
