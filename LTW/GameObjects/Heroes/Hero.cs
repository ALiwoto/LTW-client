// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using WotoProvider.Enums;
using LTW.SandBox;
using LTW.Controls;
using LTW.Security;
using LTW.Constants;
using LTW.GameObjects.WMath;
using LTW.GameObjects.Players;
using LTW.GameObjects.Resources;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Heroes
{
    public partial class Hero : IRes, IHeroSocket
    {
        //-------------------------------------------------
        #region Constants Region
        /// <summary>
        /// Use this to separate the heroes type from each other.
        /// </summary>
        public const string OutCharSeparator = "よ";
        /// <summary>
        /// But use this in each type, for separate their parameters.
        /// </summary>
        public const string InCharSeparator = "つ";
        /// <summary>
        /// The End of The File which you should
        /// save it in the server and get the data from it.
        /// </summary>
        public const string SERVER_LOC = "_日ロー";
        public const string RangeStringInRes = "_Range";
        public const string LOC_SEPARATOR = "_";
        public const string HERO_MESSAGE = "HERO -- LTW";
        //-------------------------------------------------
        public const uint BasicLevel = 1;
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public WotoRes MyRes { get; set; }
        //-------------------------------------------------
        /// <summary>
        /// The RealName of this Hero(for player).
        /// </summary>
        public StrongString Name { get; private set; }
        /// <summary>
        /// The Hero's name that setted by player.
        /// </summary>
        public StrongString CustomName { get; private set; }
        /// <summary>
        /// Use this for Programming.
        /// </summary>
        public StrongString HeroID { get; private set; }
        /// <summary>
        /// The Total Power of The Hero.
        /// this parameter is not that usefull
        /// in the live battles (pvp).
        /// </summary>
        public Unit Power { get; protected set; }
        /// <summary>
        /// The level of Hero.
        /// </summary>
        public uint Level { get; protected set; }
        public uint Stars { get; private set; }
        public int ServerIndex { get; protected set; }
        public HeroType HeroType { get; private set; }
        public PlayerElement HeroElement { get; private set; }
        //-------------------------------------------------
        #region Hero's particularities Battling
        /// <summary>
        /// 
        /// </summary>
        public virtual Unit HP { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Unit ATK { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Unit INT { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Unit DEF { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Unit RES { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Unit SPD { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Unit PEN { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Unit Block { get; protected set; }
        public virtual HeroSkill HeroSkill { get; protected set; }
        public virtual HeroSerialize HeroSerialize { get; protected set; }
        #endregion
        //-------------------------------------------------
        #region Hero's particularities Not Battling
        public Unit HeroCurrentExp { get; protected set; }
        public Unit SkillPoint { get; protected set; }
        #endregion
        //-------------------------------------------------


        #endregion Properties Region
        //-------------------------------------------------
        #region Constructor Region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="heroIDValue"></param>
        /// <param name="customNameValue"></param>
        /// <param name="levelValue"></param>
        /// <param name="powerValue"></param>
        /// <param name="skillStringValue"></param>
        protected Hero(
            StrongString customNameValue,
            StrongString heroIDValue,
            uint levelValue,
            Unit powerValue,
            Unit skillPoint,
            uint stars,
            StrongString skillStringValue
            )
        {
            CustomName = customNameValue;
            HeroID = heroIDValue;
            Level = levelValue;
            Power = powerValue;
            SkillPoint = skillPoint;
            Stars = stars;
            LoadMe(heroIDValue); // Load values which should be loaded locally.
            HeroSkill.LoadInfo(skillStringValue.GetValue()); // Load from the server string.

        }
        /// <summary>
        /// using to create a Blank Hero obj.
        /// </summary>
        /// <param name="heroID"></param>
        private Hero()
        {
            LoadMe();
        }
        #endregion
        //-------------------------------------------------
        #region Online Method's Region
        /// <summary>
        /// Update the hero's info.
        /// even if you did not change any information, 
        /// this method will do it's work.
        /// so don't use it if you did not change any information.
        /// </summary>
        /// <returns></returns>
        public async Task<DataBaseDataChangedInfo> UpdateHero()
        {
            StrongString myString = GetForServer();
            //---------------------------------------------
            var _p = ThereIsServer.GameObjects.MyProfile;
            var _target = _p.UID.GetValue() + LOC_SEPARATOR + HeroID + SERVER_LOC;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_HeroInfo_Server(this);
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            //---------------------------------------------
            if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                return null; // don't set ServerSettings.IsWaitingForServerChecking = false;
            }
            //---------------------------------------------
            var _req = new DataBaseUpdateRequest(HERO_MESSAGE, QString.Parse(myString), _existing.Sha);
            return await ThereIsServer.Actions.UpdateData(_s, _target, _req);
            //---------------------------------------------
        }
        public async Task<bool> DeleteHero(UID _uid_)
        {
            //---------------------------------------------
            var _target = _uid_.GetValue() + LOC_SEPARATOR + HeroID + SERVER_LOC;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_HeroInfo_Server(this);
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
            var _req = new DataBaseDeleteRequest(HERO_MESSAGE, existing.Sha.GetStrong());
            return await ThereIsServer.Actions.DeleteData(_s, _target, _req);
            //---------------------------------------------
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Offline Methods
        /// <summary>
        /// Get the total Unit that this Hero needs 
        /// to upgrade its level.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalExp()
        {
            return new Unit(); // Not Compeleted yet.
        }
		#if (OLD_LTW)
        public Image GetHeroImage(HeroImageTypes type)
        {
            switch (type)
            {
                case HeroImageTypes.Type_580_500:
                    {
                        return Image.FromFile(ThereIsConstants.Path.Datas_Path +
                            ThereIsConstants.Path.DoubleSlash +
                            HeroSerialize.FirstNameOfImage_580_500_File +
                            HeroID.GetValue() + HeroSerialize.EndNameOfFile);
                    }
                default:
                    {
                        return null;
                    }
            }
            
        }
        
		#endif
		public BattlingHero ConvertToBattlingHero()
        {
            // TODO.
            return null;
        }
        /// <summary>
        /// Get The Hero by it's ID and load it from the file.
        /// use this when you want to generate a new hero.
        /// </summary>
        /// <param name="heroID"></param>
        /// <returns></returns>
        private void SetHeroFromBlank(string heroID)
        {
            LoadMe(heroID);
            HeroSkill.SetUpSkills();
            CustomName = Name;
            HeroID = heroID;
            // The Basic level of the hero should be 1
            Level = 1;

            // Look, the Skills should be summed with the following,
            // But the level of the skills are zero, so they will be zero.
            HP      = (Level * HeroSerialize.HP_Rate)    + HeroSkill.GetTotalHPOfSkills();
            ATK     = (Level * HeroSerialize.ATK_Rate)   + HeroSkill.GetTotalATKOfSkills();
            INT     = (Level * HeroSerialize.INT_Rate)   + HeroSkill.GetTotalINTOfSkills();
            DEF     = (Level * HeroSerialize.DEF_Rate)   + HeroSkill.GetTotalDEFOfSkills();
            RES     = (Level * HeroSerialize.RES_Rate)   + HeroSkill.GetTotalRESOfSkills();
            SPD     = (Level * HeroSerialize.SPD_Rate)   + HeroSkill.GetTotalSPDOfSkills();
            PEN     = (Level * HeroSerialize.PEN_Rate)   + HeroSkill.GetTotalPENOfSkills();
            Block   = (Level * HeroSerialize.Block_Rate) + HeroSkill.GetTotalBlockOfSkills();
            ReloadPower();


            HeroCurrentExp = Unit.GetBasicUnit();
            SkillPoint = Unit.GetBasicUnit();
            Stars = 0;



            return;
        }
        /// <summary>
        /// Reload the Power (Offiline),
        /// with this formula:
        /// <see cref="Power"/> =  <see cref="HP"/> + <see cref="ATK"/> +
        /// <see cref="INT"/> + <see cref="DEF"/> + <see cref="RES"/> +
        /// <see cref="SPD"/> + <see cref="PEN"/> + <see cref="Block"/>
        /// </summary>
        public void ReloadPower()
        {
            Power = HP + ATK + INT + DEF + RES + SPD + PEN + Block;
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        /// <summary>
        /// Create player's troops for the first time.
        /// Notice: you shoul use this function when player has already selected his 
        /// Element, so he should has at least one hero to load after it.
        /// </summary>
        /// <param name="troops"></param>
        public static async Task<DataBaseDataChangedInfo> CreatePlayerHero(Hero hero)
        {
            StrongString myString = hero.GetForServer();
            var _p = ThereIsServer.GameObjects.MyProfile;
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_HeroInfo_Server(hero);
            var _target = _p.UID.GetValue() + LOC_SEPARATOR + hero.HeroID + SERVER_LOC;
            var _creation = new DataBaseCreation(HERO_MESSAGE, QString.Parse(myString));
            return await ThereIsServer.Actions.CreateData(_s, _target, _creation);
        }
        public static async Task<Hero> LoadHero(StrongString _id_, Player _p, IHeroSocket _socket_)
        {
            var _s = ThereIsServer.ServersInfo.ServerManager.Get_HeroInfo_Server(_socket_);
            var _target = _p.UID.GetValue() + LOC_SEPARATOR + _id_ + SERVER_LOC;
            var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
            if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
            {
                return null;
            }
            return Parse(_existing.Decode());
        }
        /// <summary>
        /// Generate a new random Hero with
        /// the Element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Hero GenerateHero(PlayerElement element)
        {
            Hero myHero = new Hero();
            int Range1 =
                Convert.ToInt32(
                    myHero.MyRes.GetString(element.ToString() + RangeStringInRes + 1));
            int Range2 =
                Convert.ToInt32(
                    myHero.MyRes.GetString(element.ToString() + RangeStringInRes + 2));
            int nowInt = Randomic.GetRandom(Range1, Range2);
            myHero.SetHeroFromBlank(nowInt.ToString());
            myHero.ServerIndex = Randomic.GetRandom(ServerManager.MIN_T, ServerManager.MAX_HEROES_INFO_SERVERS);
            return myHero; // not completed yet.
        }
        /// <summary>
        /// Create a new hero in the Game.
        /// This method will create a new hero just in the 
        /// game data.
        /// </summary>
        public static void CreateHero(HeroSerialize heroSerialize)
        {
            heroSerialize.Serialize();
        }
        private static Hero Parse(StrongString theString)
        {
            StrongString[] myStrings = theString.Split(InCharSeparator);
            Hero myHero =
                new Hero(
                    myStrings[0],                       // custom name
                    myStrings[1],                       // hero ID
                    myStrings[2].ToUInt16(),            // level
                    Unit.ConvertToUnit(myStrings[3]),   // Power
                    Unit.ConvertToUnit(myStrings[4]),   // Skill Point
                    myStrings[5].ToUInt16(),            // Stars
                    myStrings[6]                        // This is HeroSkill String value, 
                                                        // do NOT convert it here.
                    )
                {
                    HP = Unit.ConvertToUnit(myStrings[7]),              // HP
                    ATK = Unit.ConvertToUnit(myStrings[8]),             // ATK
                    INT = Unit.ConvertToUnit(myStrings[9]),             // INT
                    DEF = Unit.ConvertToUnit(myStrings[10]),            // DEF
                    RES = Unit.ConvertToUnit(myStrings[11]),            // RES
                    SPD = Unit.ConvertToUnit(myStrings[12]),            // SPD
                    PEN = Unit.ConvertToUnit(myStrings[13]),            // PEN
                    Block = Unit.ConvertToUnit(myStrings[14]),          // Block
                    HeroCurrentExp = Unit.ConvertToUnit(myStrings[15]), // Exp
                    ServerIndex = myStrings[16].ToInt32(),              // Server Index
                };

            return myHero;
        }
        #endregion
        //-------------------------------------------------
        #region overrided Methods Region
        public override string ToString()
        {
            return (Name + " - " + CustomName + " - " + HeroID).GetValue();
        }
        #endregion
        //-------------------------------------------------
        #region virtual Methods Region
        public virtual StrongString GetForServer()
        {
            StrongString myString = 
                CustomName                      + InCharSeparator + // index : 0
                HeroID                          + InCharSeparator + // index : 1
                Level.ToString()                + InCharSeparator + // index : 2
                Power.GetForServer()            + InCharSeparator + // index : 3
                SkillPoint.GetForServer()       + InCharSeparator + // index : 4
                Stars.ToString()                + InCharSeparator + // index : 5
                HeroSkill.GetForServer()        + InCharSeparator + // index : 6
                HP.GetForServer()               + InCharSeparator + // index : 7
                ATK.GetForServer()              + InCharSeparator + // index : 8
                INT.GetForServer()              + InCharSeparator + // index : 9
                DEF.GetForServer()              + InCharSeparator + // index : 10
                RES.GetForServer()              + InCharSeparator + // index : 11
                SPD.GetForServer()              + InCharSeparator + // index : 12
                PEN.GetForServer()              + InCharSeparator + // index : 13
                Block.GetForServer()            + InCharSeparator + // index : 14
                HeroCurrentExp.GetForServer()   + InCharSeparator + // index : 15
                ServerIndex.ToString()          + InCharSeparator;  // index : 16
            return myString;
        }

        #endregion
        //-------------------------------------------------
    }
}
