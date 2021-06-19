// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using LTW.Security;
using LTW.GameObjects.WMath;

namespace LTW.GameObjects.Troops
{
    partial class Troop
    {
        public class Saber : Troop
        {
            //-----------------------------------

            //-----------------------------------
            private Saber(Unit countValue, uint levelValue, Unit powerValue)
            {
                Count = countValue;
                Level = levelValue;
                Power = powerValue;
            }
            //-----------------------------------
            //-----------------------------------
            //-----------------------------------
            //-----------------------------------
            public static Saber ParseToSaber(StrongString theString)
            {
                StrongString[] myStrings = theString.Split(InCharSeparator);
                Saber mySaber = new Saber(Unit.ConvertToUnit(myStrings[0]),
                    myStrings[1].ToUInt16(), Unit.ConvertToUnit(myStrings[2]));
                return mySaber;
            }
            public static Saber GetBasicSaber()
            {
                return new Saber(Unit.GetBasicUnit(), BasicLevel,
                    BasicPower);
            }
            //-----------------------------------
            protected override StrongString GetForServer()
            {
                return base.GetForServer();
            }
            //-----------------------------------

        }
    }
    
}
