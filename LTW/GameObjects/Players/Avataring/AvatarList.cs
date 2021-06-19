// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Collections.Generic;
using LTW.Security;

namespace LTW.GameObjects.Players.Avataring
{
    /// <summary>
    /// List of Special Avatars which can be chosen
    /// by player.
    /// </summary>
    public sealed class AvatarList
    {
        //-------------------------------------------------
        #region Constants Region
        /// <summary>
        /// The Char Separator for Avatar Frame List.
        /// the value is: Ayashii.
        /// </summary>
        public const string CharSeparator = "絵";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public List<Avatar> TheAvatars { get; private set; }
        public int Length { get => TheAvatars.Count; }
        public bool IsEmpty { get; private set; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private AvatarList(Avatar[] avatar)
        {
            TheAvatars = new List<Avatar>(avatar);
            IsEmpty = TheAvatars.Count == 0;
        }
        public bool this[Avatars_S avatarType]
        {
            get
            {
                return GetIndex(avatarType) != -1;
            }
        }
        public bool this[Avatar avatar]
        {
            get
            {
                if (avatar == null || !avatar.IsSpecial)
                {
                    return false;
                }
                return this[avatar.AvatarType_S];
            }
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region
        public StrongString GetForServer()
        {
            StrongString myString = CharSeparator;
            for (int i = 0; i < Length; i++)
            {
                myString += TheAvatars[i].GetForServer() + CharSeparator;
            }
            return myString;
        }
        public void AddAvatar(Avatar avatar)
        {
            for (int i = 0; i < Length; i++)
            {
                if (TheAvatars[i] == avatar)
                {
                    return;
                }
            }
            TheAvatars.Add(avatar);
            IsEmpty = Length == 0;
        }
        public void AddAvatarFrame(Avatars_S avatarType)
        {
            AddAvatar(Avatar.ConvertToAvatar(avatarType));
        }
        public void RemoveAvatarFrame(int index)
        {
            if (index >= Length)
            {
                return;
            }
            TheAvatars.RemoveAt(index);
            IsEmpty = Length == 0;
        }
        public void RemoveAvatar(Avatars_S avatarType)
        {
            if (!this[avatarType])
            {
                return;
            }
            TheAvatars.RemoveAt(GetIndex(avatarType));
            IsEmpty = Length == 0;
        }
        public void RemoveAvatar(Avatar avatar)
        {
            RemoveAvatar(avatar.AvatarType_S);
        }
        public int GetIndex(Avatars_S avatar)
        {
            for (int i = 0; i < Length; i++)
            {
                if (TheAvatars[i].AvatarType_S == avatar)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static AvatarList Parse(StrongString serverStringValue)
        {
            StrongString[] myStrings = serverStringValue.Split(CharSeparator);
            Avatar[] avatars = new Avatar[myStrings.Length];
            for (int i = 0; i < avatars.Length; i++)
            {
                avatars[i] = Avatar.ConvertToAvatar(myStrings[i]);
            }
            return new AvatarList(avatars);
        }
        public static AvatarList GenerateEmptyList()
        {
            return new AvatarList(new Avatar[0]);
        }
        #endregion
        //-------------------------------------------------
    }
}
