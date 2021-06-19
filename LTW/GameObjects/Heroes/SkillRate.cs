// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using LTW.GameObjects.WMath;
using LTW.Security;

namespace LTW.GameObjects.Heroes
{
    public sealed class SkillRate
    {
        //-------------------------------------------------
        /// <summary>
        /// The Value is Shu,
        /// using for separate the values of the SkillRate
        /// in this class.
        /// </summary>
        public const string OutCharSeparator = "主";
        //-------------------------------------------------
        //-------------------------------------------------
        public Unit HP_Rate { get; private set; }
        public Unit ATK_Rate { get; private set; }
        public Unit INT_Rate { get; private set; }
        public Unit DEF_Rate { get; private set; }
        public Unit RES_Rate { get; private set; }
        public Unit SPD_Rate { get; private set; }
        public Unit PEN_Rate { get; private set; }
        public Unit Block_Rate { get; private set; }
        //-------------------------------------------------
        private SkillRate()
        {

        }
        //-------------------------------------------------
        public static SkillRate ParseToSkillRate(string stringValue)
        {
            string[] shiro = stringValue.Split(OutCharSeparator.ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries);
            SkillRate skillRate = new SkillRate()
            {
                HP_Rate = Unit.ConvertToUnit(shiro[0]),
                ATK_Rate = Unit.ConvertToUnit(shiro[1]),
                INT_Rate = Unit.ConvertToUnit(shiro[2]),
                DEF_Rate = Unit.ConvertToUnit(shiro[3]),
                RES_Rate = Unit.ConvertToUnit(shiro[4]),
                SPD_Rate = Unit.ConvertToUnit(shiro[5]),
                PEN_Rate = Unit.ConvertToUnit(shiro[6]),
                Block_Rate = Unit.ConvertToUnit(shiro[7]),
            };

            return skillRate;
        }
        public static SkillRate GenerateSkillRate(
            Unit hP_Rate,
            Unit aTK_Rate,
            Unit iNT_Rate,
            Unit dEF_Rate,
            Unit rES_Rate,
            Unit sPD_Rate,
            Unit pEN_Rate,
            Unit bLock_Rate)
        {
            return new SkillRate()
            {
                HP_Rate = hP_Rate,
                ATK_Rate = aTK_Rate,
                INT_Rate = iNT_Rate,
                DEF_Rate = dEF_Rate,
                RES_Rate = rES_Rate,
                SPD_Rate = sPD_Rate,
                PEN_Rate = pEN_Rate,
                Block_Rate = bLock_Rate,
            };
        }
        //-------------------------------------------------
        public override string ToString()
        {
            StrongString myString =
                HP_Rate.GetForServer()      + OutCharSeparator +    // index: 0
                ATK_Rate.GetForServer()     + OutCharSeparator +    // index : 1
                INT_Rate.GetForServer()     + OutCharSeparator +    // index : 2
                DEF_Rate.GetForServer()     + OutCharSeparator +    // index : 3
                RES_Rate.GetForServer()     + OutCharSeparator +    // index : 4
                SPD_Rate.GetForServer()     + OutCharSeparator +    // index : 5
                PEN_Rate.GetForServer()     + OutCharSeparator +    // index : 6
                Block_Rate.GetForServer()   + OutCharSeparator;     // index : 7
            return myString.GetValue();
        }
        //-------------------------------------------------

    }
}
