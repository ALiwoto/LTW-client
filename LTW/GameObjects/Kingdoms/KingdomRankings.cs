// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Threading.Tasks;

namespace LTW.GameObjects.Kingdoms
{
    public class KingdomRankings
    {
        //-----------------------------------------
        public LevelRankings LevelRankings { get; set; }
        public PowerRankings PowerRankings { get; set; }
        //-------------------------
        public KingdomInfo Kingdom { get; set; }
        //-----------------------------------------
        private KingdomRankings()
        {

        }
        //-----------------------------------------
        //-----------------------------------------
        public static async Task<KingdomRankings> GetKingdomRankings(KingdomInfo kingdom)
        {
            return new KingdomRankings()
            {
                Kingdom = kingdom,
                LevelRankings = await LevelRankings.GetLevelRankings(kingdom),
                PowerRankings = await PowerRankings.GetPowerRankings(kingdom),
            };
        }
        public static async Task<bool> CreateKingdomRankings(KingdomInfo kingdom)
        {
            await PowerRankings.CreatePowerRankings(kingdom);
            await LevelRankings.CreateLevelRankings(kingdom);
            return true;
        }
        public static async Task<bool> DeleteRankings(LTW_Kingdoms _kingdom_)
        {
            var _b1 = await PowerRankings.DeletePowerRankings(_kingdom_);
            var _b2 = await LevelRankings.DeleteLevelRankings(_kingdom_);
            return _b1 && _b2;
        }
        //-----------------------------------------
    }
}
