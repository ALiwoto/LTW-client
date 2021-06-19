// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using LTW.Security;
using LTW.GameObjects.WMath;

namespace LTW.GameObjects.Heroes
{
#pragma warning disable IDE0060
#pragma warning disable IDE0055
#pragma warning disable IDE0051
    public sealed class HeroSkill
    {
        //-------------------------------------------------
        public Skill[] Skills { get; private set; }
        //-------------------------------------------------
        public bool HasServerInfoLoaded { get; private set; }
        //-------------------------------------------------
        #region Constants Region
        /// <summary>
        /// The value is Baku
        /// </summary>
        public const string OutCharSeparator = "博";
        #endregion
        //-------------------------------------------------
        private HeroSkill(uint count)
        {
            Skills = new Skill[count];
        }
        private HeroSkill()
        {
            ;
        }
        //-------------------------------------------------
        public StrongString GetForServer()
        {
            StrongString myString = "";
            for(int i = 0; i < Skills.Length; i++)
            {
                myString += Skills[i].GetForServer() + OutCharSeparator;
            }
            return myString;
        }
        public string GetForSerialize()
        {
            string myString = "";
            for (int i = 0; i < Skills.Length; i++)
            {
                myString += Skills[i].GetForSerialize() + OutCharSeparator;
            }
            return myString;
        }
        public void LoadInfo(string serverStringInfoValue)
        {
            string[] myStrings =
                serverStringInfoValue.Split(OutCharSeparator.ToCharArray(), 
                StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < Skills.Length; i++)
            {
                Skills[i].SetInfoFromServer(myStrings[i]);
            }
            HasServerInfoLoaded = true;
        }
        public void SetUpSkills()
        {
            for(int i = 0; i < Skills.Length; i++)
            {
                Skills[i].SetUpSkill();
            }
            // Just this time, because I just set up the new skills,
            // so basically there is nothing to be load from the server.
            HasServerInfoLoaded = true;
        }
        /// <summary>
        /// Get the total value of the HP in the Skills
        /// which should be summed with the real HP of
        /// the Hero in order to set the HP of the Hero.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalHPOfSkills()
        {
            Unit sanji = Unit.GetBasicUnit();
            for(int i = 0; i < Skills.Length; i++)
            {
                sanji += Skills[i].HP_Plus;
            }
            return sanji;
        }
        /// <summary>
        /// Get the total value of the ATK in the Skills
        /// which should be summed with the real ATK of
        /// the Hero in order to set the ATK of the Hero.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalATKOfSkills()
        {
            Unit sanji = Unit.GetBasicUnit();
            for (int i = 0; i < Skills.Length; i++)
            {
                sanji += Skills[i].ATK_Plus;
            }
            return sanji;
        }
        /// <summary>
        /// Get the total value of the HP in the Skills
        /// which should be summed with the real HP of
        /// the Hero in order to set the HP of the Hero.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalINTOfSkills()
        {
            Unit sanji = Unit.GetBasicUnit();
            for (int i = 0; i < Skills.Length; i++)
            {
                sanji += Skills[i].INT_Plus;
            }
            return sanji;
        }
        /// <summary>
        /// Get the total value of the HP in the Skills
        /// which should be summed with the real HP of
        /// the Hero in order to set the HP of the Hero.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalDEFOfSkills()
        {
            Unit sanji = Unit.GetBasicUnit();
            for (int i = 0; i < Skills.Length; i++)
            {
                sanji += Skills[i].DEF_Plus;
            }
            return sanji;
        }
        /// <summary>
        /// Get the total value of the HP in the Skills
        /// which should be summed with the real HP of
        /// the Hero in order to set the HP of the Hero.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalRESOfSkills()
        {
            Unit sanji = Unit.GetBasicUnit();
            for (int i = 0; i < Skills.Length; i++)
            {
                sanji += Skills[i].RES_Plus;
            }
            return sanji;
        }
        /// <summary>
        /// Get the total value of the HP in the Skills
        /// which should be summed with the real HP of
        /// the Hero in order to set the HP of the Hero.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalSPDOfSkills()
        {
            Unit sanji = Unit.GetBasicUnit();
            for (int i = 0; i < Skills.Length; i++)
            {
                sanji += Skills[i].SPD_Plus;
            }
            return sanji;
        }
        /// <summary>
        /// Get the total value of the HP in the Skills
        /// which should be summed with the real HP of
        /// the Hero in order to set the HP of the Hero.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalPENOfSkills()
        {
            Unit sanji = Unit.GetBasicUnit();
            for (int i = 0; i < Skills.Length; i++)
            {
                sanji += Skills[i].PEN_Plus;
            }
            return sanji;
        }
        /// <summary>
        /// Get the total value of the HP in the Skills
        /// which should be summed with the real HP of
        /// the Hero in order to set the HP of the Hero.
        /// </summary>
        /// <returns></returns>
        public Unit GetTotalBlockOfSkills()
        {
            Unit sanji = Unit.GetBasicUnit();
            for (int i = 0; i < Skills.Length; i++)
            {
                sanji += Skills[i].Block_Plus;
            }
            return sanji;
        }
        //-------------------------------------------------
        public static HeroSkill GetHeroSkill(string heroSkillString,
            uint count, Hero owner)
        {
            HeroSkill heroSkill = new HeroSkill(count);
            string[] myStrings = heroSkillString.Split(OutCharSeparator.ToCharArray(), 
                StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < heroSkill.Skills.Length; i++)
            {
                heroSkill.Skills[i] = Skill.Parse(myStrings[i], owner);
            }
            return heroSkill;
        }
        /// <summary>
        /// Please use <see cref="Skill.GenerateSkill(string, uint, SkillRate, Hero, string)"/>
        /// in order to generate the array of the Skills.
        /// </summary>
        /// <param name="skills">
        /// use <see cref="Skill.GenerateSkill(string, uint, SkillRate, Hero, string)"/>
        /// for this arg.
        /// </param>
        /// <returns></returns>
        public static HeroSkill GenerateHeroSkill(Skill[] skills)
        {
            return new HeroSkill()
            {
                Skills = skills,

                // Because they are the Basic Unit
                // thus they don't need to be loaded from the server.
                HasServerInfoLoaded = true, 

            };
        }
    }
#pragma warning restore IDE0051
#pragma warning restore IDE0055
#pragma warning restore IDE0060
}
