// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Drawing;
using System.IO;
using LTW.Constants;
using LTW.Security;
using LTW.GameObjects.WMath;

namespace LTW.GameObjects.Heroes
{
    public class Skill
    {
        //-------------------------------------------------
        /// <summary>
        /// The Value is Shoku, used for separating the 
        /// Skill's Properties from each other.
        /// BY: ALi.w
        /// </summary>
        public const string CharSeparator = "職";
        /// <summary>
        /// use it like this:
        /// <see cref="FirstNameOfImageFile"/> + 
        /// <see cref="Hero.HeroID"/> (get HeroID with <see cref="Owner"/>) +
        /// <see cref="OrdinaryFileSeparator"/> + <see cref="SkillIndex"/> + 
        /// <see cref="HeroSerialize.EndNameOfFile"/>
        /// </summary>
        public const string FirstNameOfImageFile = "h_s_";
        /// <summary>
        /// Use this in order to separate the
        /// HeroID and SkillIndex for getting image file of this
        /// skill in this class.
        /// </summary>
        public const string OrdinaryFileSeparator = "_";
        //-------------------------------------------------
        public string SkillName { get; protected set; }
        //-------------------------------------------------
        public uint SkillLevel { get; protected set; }
        /// <summary>
        /// This value should be from zero.
        /// </summary>
        public uint SkillIndex { get; protected set; }
        //-------------------------------------------------
        /// <summary>
        /// Skill Code is 0;
        /// </summary>
        public Unit HP_Plus { get; protected set; }
        /// <summary>
        /// Skill Code is 1;
        /// </summary>
        public Unit ATK_Plus { get; protected set; }
        /// <summary>
        /// Skill Code is 2;
        /// </summary>
        public Unit INT_Plus { get; protected set; }
        /// <summary>
        /// Skill Code is 3;
        /// </summary>
        public Unit DEF_Plus { get; protected set; }
        /// <summary>
        /// Skill Code is 4;
        /// </summary>
        public Unit RES_Plus { get; protected set; }
        /// <summary>
        /// Skill Code is 5;
        /// </summary>
        public Unit SPD_Plus { get; protected set; }
        /// <summary>
        /// Skill Code is 6;
        /// </summary>
        public Unit PEN_Plus { get; protected set; }
        /// <summary>
        /// Skill Code is 7;
        /// </summary>
        public Unit Block_Plus { get; protected set; }
        /// <summary>
        /// This parameter should be loaded from the
        /// serialization.
        /// </summary>
        public SkillRate SkillRate { get; protected set; }
        //-------------------------------------------------
        /// <summary>
        /// Do NOT use it in GetForServer and GetForSerialize
        /// </summary>
        public Hero Owner { get; set; }
        //-------------------------------------------------
        #region Constructor and this[] Region
        private Skill(Hero owner)
        {
            Owner = owner;
            // Owner.HeroSkill.Skills[SkillIndex] == this ; It will return true
        }
        private Skill()
        {

        }
        /// <summary>
        /// Get or Set the Skill Unit by Skill Code.
        /// The Set accessor is protected.
        /// </summary>
        /// <param name="skillCode">
        /// you can see the skill code of each Unit
        /// in this class by Moving the muose on them.
        /// I already wrote all of the Codes.
        /// </param>
        /// <returns></returns>
        public virtual Unit this[uint skillCode]
        {
            get
            {
                switch (skillCode)
                {
                    // HP_Plus
                    case 0:
                        {
                            return HP_Plus;
                        }
                    // ATK_Plus
                    case 1:
                        {
                            return ATK_Plus;
                        }
                    // INT_Plus
                    case 2:
                        {
                            return INT_Plus;
                        }
                    // DEF_Plus
                    case 3:
                        {
                            return DEF_Plus;
                        }
                    // RES_Plus
                    case 4:
                        {
                            return RES_Plus;
                        }
                    // SPD_Plus
                    case 5:
                        {
                            return SPD_Plus;
                        }
                    // PEN_Plus
                    case 6:
                        {
                            return PEN_Plus;
                        }
                    // Block_Plus
                    case 7:
                        {
                            return Block_Plus;
                        }
                    // NULL.
                    default:
                        {
                            return null;
                        }
                }
            }
            protected set
            {
                switch (skillCode)
                {
                    // HP_Plus
                    case 0:
                        {
                            HP_Plus = value;
                            break;
                        }
                    // ATK_Plus
                    case 1:
                        {
                            ATK_Plus = value;
                            break;
                        }
                    // INT_Plus
                    case 2:
                        {
                            INT_Plus = value;
                            break;
                        }
                    // DEF_Plus
                    case 3:
                        {
                            DEF_Plus = value;
                            break;
                        }
                    // RES_Plus
                    case 4:
                        {
                            RES_Plus = value;
                            break;
                        }
                    // SPD_Plus
                    case 5:
                        {
                            SPD_Plus = value;
                            break;
                        }
                    // PEN_Plus
                    case 6:
                        {
                            PEN_Plus = value;
                            break;
                        }
                    // Block_Plus
                    case 7:
                        {
                            Block_Plus = value;
                            break;
                        }
                    // NULL.
                    default:
                        {
                            return;
                        }
                }
            }
        }
        #endregion
        //-------------------------------------------------
        //-------------------------------------------------
        public StrongString GetForServer()
        {
            StrongString myString =
                SkillName                 + CharSeparator + // index : 0
                SkillLevel.ToString()     + CharSeparator + // index : 1
                HP_Plus.GetForServer()    + CharSeparator + // index : 2
                ATK_Plus.GetForServer()   + CharSeparator + // index : 3
                INT_Plus.GetForServer()   + CharSeparator + // index : 4
                DEF_Plus.GetForServer()   + CharSeparator + // index : 5
                RES_Plus.GetForServer()   + CharSeparator + // index : 6
                SPD_Plus.GetForServer()   + CharSeparator + // index : 7
                PEN_Plus.GetForServer()   + CharSeparator + // index : 8
                Block_Plus.GetForServer() + CharSeparator;  // index : 9
            return myString;
        }
        public string GetForSerialize()
        {
            string myString =
                SkillName + CharSeparator +
                SkillIndex + CharSeparator +
                SkillRate.ToString() + CharSeparator;
            return myString;
        }

		#if (OLD_LTW)
        /// <summary>
        /// Getting the 64x64 Image for
        /// common skills and 62x56 Image for uncommon.
        /// </summary>
        /// <returns></returns>
        public Image GetSkillImage()
        {
            if(File.Exists(ThereIsConstants.Path.Datas_Path +
                ThereIsConstants.Path.DoubleSlash +
                FirstNameOfImageFile + Owner.HeroID.GetValue() +
                OrdinaryFileSeparator +
                SkillIndex + HeroSerialize.EndNameOfFile))
            {
                return Image.FromFile(ThereIsConstants.Path.Datas_Path +
                ThereIsConstants.Path.DoubleSlash +
                FirstNameOfImageFile + Owner.HeroID.GetValue() +
                OrdinaryFileSeparator +
                SkillIndex + HeroSerialize.EndNameOfFile);
            }
            return null;
        }
        #endif
		
		
		/// <summary>
        /// You should use this method to set the 
        /// info of this skill from the
        /// server.
        /// </summary>
        /// <param name="serverStringValue"></param>
        public void SetInfoFromServer(string serverStringValue)
        {
            string[] myStrings = serverStringValue.Split(CharSeparator.ToCharArray(),
                 StringSplitOptions.RemoveEmptyEntries);
            SkillName   = myStrings[0];                     // index : 0
            SkillLevel  = Convert.ToUInt32(myStrings[1]);   // index : 1
            HP_Plus     = Unit.ConvertToUnit(myStrings[2]); // index : 2
            ATK_Plus    = Unit.ConvertToUnit(myStrings[3]); // index : 3
            INT_Plus    = Unit.ConvertToUnit(myStrings[4]); // index : 4
            DEF_Plus    = Unit.ConvertToUnit(myStrings[5]); // index : 5
            RES_Plus    = Unit.ConvertToUnit(myStrings[6]); // index : 6
            SPD_Plus    = Unit.ConvertToUnit(myStrings[7]); // index : 7
            PEN_Plus    = Unit.ConvertToUnit(myStrings[8]); // index : 8
            Block_Plus  = Unit.ConvertToUnit(myStrings[9]); // index : 9
        }
        /// <summary>
        /// When you want to generate the hero for first time,
        /// this method will be called.
        /// </summary>
        public void SetUpSkill()
        {
            // Level is zero, so all of them should be basic Unit.
            HP_Plus     = SkillLevel * SkillRate.HP_Rate;
            ATK_Plus    = SkillLevel * SkillRate.ATK_Rate;
            INT_Plus    = SkillLevel * SkillRate.INT_Rate;
            DEF_Plus    = SkillLevel * SkillRate.DEF_Rate;
            RES_Plus    = SkillLevel * SkillRate.RES_Rate;
            SPD_Plus    = SkillLevel * SkillRate.SPD_Rate;
            PEN_Plus    = SkillLevel * SkillRate.PEN_Rate;
            Block_Plus  = SkillLevel * SkillRate.Block_Rate;
        }
        //-------------------------------------------------
        /// <summary>
        /// This mehod will Parse the specified 
        /// serialized string to Skill.
        /// NOTICE: this method won't use <see cref="SetInfoFromServer(string)"/>,
        /// thus the info which should be loaded from
        /// the server, are NULL.
        /// </summary>
        /// <param name="serializedStringValue"></param>
        /// <returns></returns>
        public static Skill Parse(string serializedStringValue, Hero owner)
        {
            string[] myStrings = serializedStringValue.Split(CharSeparator.ToCharArray(),
                 StringSplitOptions.RemoveEmptyEntries);
            Skill skill = new Skill(owner)
            {
                SkillName = myStrings[0],
                SkillIndex = Convert.ToUInt32(myStrings[1]),
                SkillRate = SkillRate.ParseToSkillRate(myStrings[2])
            };
            return skill;
        }
        public static Skill Parse(string serializedStringValue, string serverValue, 
            Hero owner)
        {
            string[] myStrings = serializedStringValue.Split(CharSeparator.ToCharArray(),
                 StringSplitOptions.RemoveEmptyEntries);
            Skill skill = new Skill(owner)
            {
                SkillName = myStrings[0],
                SkillIndex = Convert.ToUInt32(myStrings[1]),
            };
            skill.SetInfoFromServer(serverValue);
            return skill;
        }
        //-------------------------------------------------
        /// <summary>
        /// Generate a new Skill with the leve zero.
        /// All of the Unit Properties are Basic Unit.
        /// NOTICE: You can't Generate a new skill
        /// with level more than zero.
        /// And all of the Hero Skills Generated by this
        /// method are Basic Unit.
        /// </summary>
        /// <param name="skillName">
        /// The name of this Skill,
        /// this will be shown to the player in the interface.
        /// </param>
        /// <param name="skillIndex">
        /// The Index of this Skill in the 
        /// <see cref="HeroSkill.Skills"/> array.
        /// </param>
        /// <param name="skillRate">
        /// The SkillRate of this Skill.
        /// Please NOTICE that we will
        /// beat the next level of the current level of this
        /// skill to the specified value from the skillRate
        /// in order to determine the specified 
        /// value of the next level of the skill.
        /// please read the Example part of
        /// this description for more information.
        /// </param>
        /// <param name="owner">
        /// The <see cref="Hero"/> owner of this Skill.
        /// We won't use this owner in the
        /// <see cref="GetForSerialize()"/> nor
        /// in the <see cref="GetForServer()"/>,
        /// so you should pass it by coding
        /// each time.
        /// </param>
        /// <param name="imagePath">
        /// the Image of this Skill,
        /// please use 64x64 images for the skills with 
        /// index less than (?).
        /// </param>
        /// <returns>
        /// an object of this class, <see cref="Skill"/>
        /// </returns>
        /// <example>
        /// Look, SkillRate Should not be Basic,
        /// which is mean you should a 
        /// good skillRate and when player wants to 
        /// upgrade the level of this skill,
        /// we will beat the level (for example level one)
        /// to the Skill rate specified Rate and add it to the 
        /// current specified value(for example:
        /// (1 * HP(Unit(10nil))) + HP(Unit(0nil))
        /// ** The second one is the current HP value **
        /// </example>
        public static Skill GenerateSkill(
            string skillName,
            uint skillIndex,
            SkillRate skillRate,
            string heroID,
            string imagePath)
        {
            Skill mySkill = new Skill()
            {
                SkillName   = skillName,
                SkillLevel  = 0,
                SkillIndex  = skillIndex,
                SkillRate   = skillRate,
                //-----------------------
                HP_Plus     = Unit.GetBasicUnit(),
                ATK_Plus    = Unit.GetBasicUnit(),
                INT_Plus    = Unit.GetBasicUnit(),
                DEF_Plus    = Unit.GetBasicUnit(),
                RES_Plus    = Unit.GetBasicUnit(),
                SPD_Plus    = Unit.GetBasicUnit(),
                PEN_Plus    = Unit.GetBasicUnit(),
                Block_Plus  = Unit.GetBasicUnit(),
            };
            File.Copy(imagePath,
                ThereIsConstants.Path.Datas_Path +
                FirstNameOfImageFile + heroID +
                OrdinaryFileSeparator +
                skillIndex + HeroSerialize.EndNameOfFile, true);
            return mySkill;
        }

    }
}
