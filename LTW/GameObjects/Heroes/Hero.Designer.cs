// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.GameObjects.Resources;
using LTW.Security;

namespace LTW.GameObjects.Heroes
{
    partial class Hero
    {
        /// <summary>
        /// Loading the ordinary Information of this hero from
        /// the file.
        /// loading <see cref="Name"/>,
        /// <see cref="HeroSkill"/> (by: <see cref="HeroSkill.GetHeroSkill(string, uint)"/>
        /// which string is <see cref="HeroSerialize.HeroSkillsString"/> and
        /// uint is <see cref="HeroSerialize.HeroSkillsCount"/>),
        /// <see cref="HeroType"/>.
        /// </summary>
        /// <param name="heroID"></param>
        protected virtual void LoadMe(StrongString heroID)
        {
            if(this.MyRes is null)
            {
                this.MyRes = new WotoRes(typeof(Hero));
            }
            //---------------------------------------------
            this.HeroSerialize =
                HeroSerialize.GetHeroSerialize(heroID.GetValue());
            this.Name                   = this.HeroSerialize.HeroName;
            this.HeroSkill              = HeroSkill.GetHeroSkill(this.HeroSerialize.HeroSkillsString,
                this.HeroSerialize.HeroSkillsCount, this);
            this.HeroType               = this.HeroSerialize.HeroType;
            this.HeroElement            = this.HeroSerialize.HeroElement;
            //---------------------------------------------
        }
        /// <summary>
        /// Call this method to load a new and blank hero obj.
        /// </summary>
        private void LoadMe()
        {
            if(this.MyRes is null)
            {
                this.MyRes = new WotoRes(typeof(Hero));
            }
            //---------------------------------------------
        }
    }
}
