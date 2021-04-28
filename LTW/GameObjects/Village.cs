// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTW.GameObjects.Troops;
using LTW.GameObjects.Players;

namespace LTW.GameObjects
{
    public class Village
    {
        public Troop[] TroopsInVillage { get; set; }
        public MagicalTroop[] MagicalTroopsInVillage { get; set; }
        /// <summary>
        /// Notice: This Parameter should have Only the 
        /// <see cref="PlayerInfo.PlayerName"/>,
        /// except the player required the PlayerInfo, that time, you should
        /// use <see cref="PlayerInfo.GetPlayerInfo(string)"/>,
        /// Also you can use this in Player: <code>
        /// Owner = this.
        /// </code>
        /// </summary>
        public Player Owner { get; set; }



        public static Village GetTheVillageInfo()
        {





            return new Village()
            {

            };
        }
    }
}
