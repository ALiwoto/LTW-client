// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;
using LTW.GameObjects.WMath;

namespace LTW.GameObjects.Troops
{
    public class TroopManager
    {
        //-------------------------------------------------
        #region Properties Region
        public Troop[] Troops { get; protected set; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private TroopManager()
        {

        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Offline Methods
        public Unit GetTotalTroopsPower()
        {
            Unit myUnit = Unit.GetBasicUnit();
            for (int i = 0; i < Troops.Length; i++)
            {
                myUnit += Troops[i].Power;
            }
            return myUnit;
        }
        #endregion
        //-------------------------------------------------
        #region static Offline Methods
        public static TroopManager GetTroopManager(Troop[] troops)
        {
            return new TroopManager()
            {
                Troops = troops,
            };
        }
        #endregion
        //-------------------------------------------------
        #region static Online Methods
        public static async Task<TroopManager> LoadTroopManager()
        {
            return new TroopManager()
            {
                Troops = await Troop.LoadPlayerTroops(),
            };

        }
        #endregion
        //-------------------------------------------------
    }
}
