// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using WotoProvider.Enums;
using LTW.Security;
using LTW.GameObjects.WMath;

namespace LTW.GameObjects.GameResources
{
    /// <summary>
    /// The Order:
    /// <see cref="Coupon"/>, <see cref="Diamond"/>,
    /// <see cref="Stone"/>, <see cref="Silver"/>,
    /// <see cref="Coin"/>, <see cref="Mana"/>,
    /// <see cref="MP"/>.
    /// </summary>
    public class Resources
    {
        public const string InCharSeparator = PlayerResources.InCharSeparator;
        //---------------------------------
        /// <summary>
        /// Coupon Value.
        /// Code is 1.
        /// </summary>
        public Unit Coupon { get; set; }
        /// <summary>
        /// Diamond Value.
        /// Code is 2.
        /// </summary>
        public Unit Diamond { get; set; }
        /// <summary>
        /// Stone Value.
        /// Code is 3.
        /// </summary>
        public Unit Stone { get; set; }
        /// <summary>
        /// Silver Value.
        /// Code is 4.
        /// </summary>
        public Unit Silver { get; set; }
        /// <summary>
        /// Coin Value.
        /// Code is 5.
        /// </summary>
        public Unit Coin { get; set; }
        /// <summary>
        /// Mana Value.
        /// Code is 6.
        /// </summary>
        public Unit Mana { get; set; }
        /// <summary>
        /// The Messy Point.
        /// this value should be the times of Mana usage for each attacking the monsters.
        /// Code is 7.
        /// </summary>
        public Unit MP { get; set; }
        //---------------------------------
        public Resources(Unit couponnValue, Unit diamondValue, Unit stoneValue, Unit silverValue,
            Unit coinValue, Unit manaValue, Unit mpValue)
        {
            Coupon  = couponnValue;
            Diamond = diamondValue;
            Stone   = stoneValue;
            Silver  = silverValue;
            Coin    = coinValue;
            Mana    = manaValue;
            MP      = mpValue;
        }
        public Unit this[PlayerResourceType type]
        {
            get
            {
                switch (type)
                {
                    case PlayerResourceType.NaN:
                        return null;
                    case PlayerResourceType.Coupon:
                        return Coupon;
                    case PlayerResourceType.Diamond:
                        return Diamond;
                    case PlayerResourceType.Stone:
                        return Stone;
                    case PlayerResourceType.Silver:
                        return Silver;
                    case PlayerResourceType.Coin:
                        return Coin;
                    case PlayerResourceType.Mana:
                        return Mana;
                    case PlayerResourceType.MP:
                        return MP;
                    default:
                        // nothing ... 
                        break;
                }
                return null;
            }
        }
        //---------------------------------
        public static Resources ParseToResources(StrongString theString)
        {
            StrongString[] myStrings = theString.Split(InCharSeparator);
            Resources resources = 
                new Resources(
                    Unit.ConvertToUnit(myStrings[0]), // Grain
                    Unit.ConvertToUnit(myStrings[1]), // Wood
                    Unit.ConvertToUnit(myStrings[2]), // Stone
                    Unit.ConvertToUnit(myStrings[3]), // Silver
                    Unit.ConvertToUnit(myStrings[4]), // Coin
                    Unit.ConvertToUnit(myStrings[5]), // Mana
                    Unit.ConvertToUnit(myStrings[6]) // MP
                );
            return resources;
        }
        public static Resources GetBasicResources()
        {
            Resources myRes = new Resources(
                Unit.GetBasicUnit(),        // Grain
                    Unit.GetBasicUnit(),    // Wood
                    Unit.GetBasicUnit(),    // Stone
                    Unit.GetBasicUnit(),    // Silver
                    Unit.GetBasicUnit(),    // Coin
                    Unit.GetBasicUnit(),    // Mana
                    Unit.GetBasicUnit()     // MP
                );
            return myRes;
        }
        //---------------------------------
        public StrongString GetForServer()
        {
            StrongString myString =
                Coupon.GetForServer()   + InCharSeparator + // 1
                Diamond.GetForServer()  + InCharSeparator + // 2
                Stone.GetForServer()    + InCharSeparator + // 3
                Silver.GetForServer()   + InCharSeparator + // 4
                Coin.GetForServer()     + InCharSeparator + // 5
                Mana.GetForServer()     + InCharSeparator + // 6
                MP.GetForServer()       + InCharSeparator;  // 7
            return myString;
        }
        //---------------------------------
    }
}
