// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;

namespace LTW.GameObjects.Characters
{
    public class CharacterInfo : Character
    {
        //-------------------------------------------------
        #region Constructors Region
        /// <summary>
        /// Look, you should use the character blank name +
        /// <see cref="DialogContext.CharacterNameSeparater"/> +
        /// Status. so I can set <see cref="Character.CharacterBlankName"/>
        /// in this characterInfo.
        /// </summary>
        /// <param name="CharNameValue"></param>
        public CharacterInfo(string CharNameValue)
        {
            string[] myStrings =
                CharNameValue.Split(DialogContext.CharacterNameSeparater.ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries);
            CharacterName       = myStrings[0] + myStrings[1];
            CharacterBlankName = MyRes.GetString(strName:CharacterName);
            Status              = Convert.ToUInt32(myStrings[1]);
        }
        private CharacterInfo(string charname, uint status)
        {
            CharacterName       = charname + status;
            CharacterBlankName  = MyRes.GetString(strName:CharacterName);
            Status              = status;
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static CharacterInfo GetCharacterInfo(GameCharacters character, uint status)
        {
            CharacterInfo info = 
                new CharacterInfo(character.ToString(), status);
            switch (character)
            {
                case GameCharacters.Kotomine:
                    // Do Something here..
                    break;
                case GameCharacters.Tohsaka:
                    // Do something here...
                    break;
                case GameCharacters.Dark_Lord:
                    // Do Another thing herer :" ...
                    break;
            }
            return info;
        }
        #endregion
        //-------------------------------------------------
    }
}
