// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WotoProvider.Enums;
using LTW.Constants;
using LTW.GameObjects.Players;
using LTW.GameObjects.WMath;

namespace LTW.GameObjects.Heroes
{
#pragma warning disable IDE0060
#pragma warning disable IDE0055
#pragma warning disable IDE0052
#pragma warning disable IDE0051
#pragma warning disable IDE0044
#pragma warning disable IDE0032
#pragma warning disable IDE0025
    [Serializable]
    public class HeroSerialize
    {
        //-------------------------------------------------
        /// <summary>
        /// use like this to get the file of the hero:
        /// <see cref="FirstNameOfFile"/> + <see cref="heroID"/> + 
        /// <see cref="EndNameOfFile"/>.
        /// </summary>
        public const string FirstNameOfFile = "h_";
        /// <summary>
        /// use this to get the image file of this hero:
        /// <see cref="FirstNameOfImageFile"/> + <see cref="heroID"/> +
        /// <see cref="EndNameOfFile"/>.
        /// </summary>
        public const string FirstNameOfImage_580_500_File = "f_h_";
        public const string EndNameOfFile = ".bin";
        //-------------------------------------------------
        private string heroID;
        private string heroName;
        private string heroSkillsString;
        private uint heroSkillsCount;
        private HeroType heroType;
        private PlayerElement heroElement;
        //-------------------------------------------------
        /* Increasing Rates.
         * The following field's are called 
         * increased rates, so you should convert them
         * to Unit.
         * each level the hero upgrades, you should
         * beat these Unit in the current level and
         * add them to the current specified value.
         * 
             */
        private string hP_Rate;
        private string aTK_Rate;
        private string iNT_Rate;
        private string dEF_Rate;
        private string rES_Rate;
        private string sPD_Rate;
        private string pEN_Rate;
        private string block_Rate;
        //-------------------------------------------------

        private HeroSerialize()
        {

        }
        //-------------------------------------------------
        public void Serialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream myFile = 
                new FileStream(ThereIsConstants.Path.Datas_Path + 
					FirstNameOfFile + 
                	HeroID + EndNameOfFile, 
                	FileMode.OpenOrCreate, FileAccess.Write);
            //formatter.Serialize(myFile, this);
            myFile.Close();
            myFile.Dispose();
        }
        //-------------------------------------------------
        public string HeroID
        {
            get
            {
                return heroID;
            }
        }
        public string HeroName
        {
            get
            {
                return heroName;
            }
        }
        public string HeroSkillsString
        {
            get
            {
                return heroSkillsString;
            }
        }
        public uint HeroSkillsCount
        {
            get
            {
                return heroSkillsCount;
            }
        }
        public HeroType HeroType
        {
            get
            {
                return heroType;
            }
        }
        public PlayerElement HeroElement
        {
            get
            {
                return heroElement;
            }
        }
        //-------------------------------------------------
        /* Rates Properties are here.
         * 23 / 11 / 2020, ALiwoto
         */
        public Unit HP_Rate
        {
            get
            {
                return Unit.ConvertToUnit(hP_Rate);
            }
            set
            {
                if (value != null) 
                {
                    hP_Rate = value.GetForServer().GetValue();
                }
                
            }
        }
        public Unit ATK_Rate
        {
            get
            {
                return Unit.ConvertToUnit(aTK_Rate);
            }
            set
            {
                if (value != null)
                {
                    aTK_Rate = value.GetForServer().GetValue();
                }

            }
        }
        public Unit INT_Rate
        {
            get
            {
                return Unit.ConvertToUnit(iNT_Rate);
            }
            set
            {
                if (value != null)
                {
                    iNT_Rate = value.GetForServer().GetValue();
                }

            }
        }
        public Unit DEF_Rate
        {
            get
            {
                return Unit.ConvertToUnit(dEF_Rate);
            }
            set
            {
                if (value != null)
                {
                    dEF_Rate = value.GetForServer().GetValue();
                }

            }
        }
        public Unit RES_Rate
        {
            get
            {
                return Unit.ConvertToUnit(rES_Rate);
            }
            set
            {
                if (value != null)
                {
                    rES_Rate = value.GetForServer().GetValue();
                }

            }
        }
        public Unit SPD_Rate
        {
            get
            {
                return Unit.ConvertToUnit(sPD_Rate);
            }
            set
            {
                if (value != null)
                {
                    sPD_Rate = value.GetForServer().GetValue();
                }

            }
        }
        public Unit PEN_Rate
        {
            get
            {
                return Unit.ConvertToUnit(pEN_Rate);
            }
            set
            {
                if (value != null)
                {
                    pEN_Rate = value.GetForServer().GetValue();
                }

            }
        }
        public Unit Block_Rate
        {
            get
            {
                return Unit.ConvertToUnit(block_Rate);
            }
            set
            {
                if (value != null)
                {
                    block_Rate = value.GetForServer().GetValue();
                }

            }
        }
        //-------------------------------------------------
        public static void Serialize(HeroSerialize heroSerialize)
        {
            heroSerialize.Serialize();
        }
        public static HeroSerialize GetHeroSerialize(string heroID)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream myFile =
                new FileStream(ThereIsConstants.Path.Datas_Path+
				FirstNameOfFile +
                heroID + EndNameOfFile,
                FileMode.Open, FileAccess.Read);
            HeroSerialize heroSerialize = null; // = (HeroSerialize)formatter.Deserialize(myFile);
            myFile.Close();
            myFile.Dispose();
            return heroSerialize;
        }
        /// <summary>
        /// Create a new HeroSerialize.
        /// Use this method if and only if you wanna create a new hero
        /// in the game.
        /// </summary>
        /// <param name="heroID">
        /// the HeroID, we will save and load this hero's inforamation
        /// with this ID to the specified File name in the 
        /// Data files.
        /// check here for more information: <see cref="Serialize()"/>
        /// </param>
        /// <param name="heroName"></param>
        /// <param name="heroSkillsString">
        /// HeroSkillsString, you should get is from 
        /// <see cref="HeroSkill.GetForSerialize()"/>.
        /// </param>
        /// <param name="heroSkillsCount"></param>
        /// <param name="type"></param>
        /// <param name="heroElement"></param>
        /// <param name="hP_rate"></param>
        /// <param name="aTK_rate"></param>
        /// <param name="iNT_rate"></param>
        /// <param name="dEF_rate"></param>
        /// <param name="rES_rate"></param>
        /// <param name="sPD_rate"></param>
        /// <param name="pEN_rate"></param>
        /// <param name="bLock_rate"></param>
        /// <param name="imagePath">
        /// The path of the Image of this hero.
        /// </param>
        /// <returns></returns>
        public static HeroSerialize GenerateHeroSerialize(
            string heroID, 
            string heroName, 
            string heroSkillsString, 
            uint heroSkillsCount, 
            HeroType type, 
            PlayerElement heroElement,
            Unit hP_rate,
            Unit aTK_rate,
            Unit iNT_rate,
            Unit dEF_rate,
            Unit rES_rate,
            Unit sPD_rate,
            Unit pEN_rate,
            Unit bLock_rate,
            string imagePath
            )
        {
            HeroSerialize heroSerialize = new HeroSerialize()
            {
                heroID = heroID,
                heroName = heroName,
                heroSkillsString = heroSkillsString,
                heroSkillsCount = heroSkillsCount,
                heroType = type,
                heroElement = heroElement,
                //--------------------------
                HP_Rate = hP_rate,
                ATK_Rate = aTK_rate,
                INT_Rate = iNT_rate,
                DEF_Rate = dEF_rate,
                RES_Rate = rES_rate,
                SPD_Rate = sPD_rate,
                PEN_Rate = pEN_rate,
                Block_Rate = bLock_rate,
            };
            File.Copy(imagePath, ThereIsConstants.Path.Datas_Path +
                FirstNameOfImage_580_500_File + heroID + EndNameOfFile, true);
            return heroSerialize;
        }


    }
#pragma warning restore IDE0025
#pragma warning restore IDE0032
#pragma warning restore IDE0044
#pragma warning restore IDE0051
#pragma warning restore IDE0052
#pragma warning restore IDE0055
#pragma warning restore IDE0060
}
