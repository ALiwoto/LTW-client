// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Drawing;
using System.ComponentModel;
using LTW.Controls;
using LTW.Security;
using LTW.Constants;
using LTW.GameObjects.Resources;

namespace LTW.GameObjects.Players.Avataring
{
    /// <summary>
    /// The Avatar of the player.
    /// </summary>
    public sealed partial class Avatar : IRes
    {
        //-------------------------------------------------
        #region Constants Region
        public const string CharSeparater       = "_";
        public const char OrdinaryFirst         = 'A';
        public const char SpecialFirst          = 'S';
        public const char TheNPCFirst           = 'N';
        public const int ORDINARY_AVATAR_MAX    = 13;
        public const int SPECIAL_AVATAR_MAX     = 4;
        public const int NPC_AVATAR_MAX         = 2;
        public const int INDEX_COMPARER         = 1;
        public const int BASE_INDEX             = 0;
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public string AvatarFullName { get; }
        public WotoRes MyRes { get; set; }
        public bool IsSpecial
        {
            get
            {
                return Kind == Avatar_Kind.Special;
            }
        }
        public Avatars AvatarType { get; } = Avatars.A_0; // default avatar.
        public Avatars_S AvatarType_S { get; }
        internal Avatars_N AvatarType_N { get; }
        public Avatar_Kind Kind { get; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private Avatar(Avatars avatarType)
        {
            AvatarFullName  = avatarType.ToString();
            AvatarType      = avatarType;
            Kind = Avatar_Kind.Normal;
            InitializeComponent();
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private Avatar(Avatars_S avatarType)
        {
            AvatarFullName  = avatarType.ToString();
            AvatarType_S    = avatarType;
            Kind = Avatar_Kind.Special;
            InitializeComponent();
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private Avatar(Avatars_N avatarType)
        {
            AvatarFullName  = avatarType.ToString();
            AvatarType_N    = avatarType;
            Kind = Avatar_Kind.NPC;
            InitializeComponent();
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region
        public StrongString GetForServer()
        {
            switch (Kind)
            {
                case Avatar_Kind.Normal:
                    return AvatarType.ToString();
                case Avatar_Kind.Special:
                    return AvatarType_S.ToString();
                case Avatar_Kind.NPC:
                    return AvatarType_N.ToString();
                default:
                    return StrongString.Empty;
            }
        }
		#if (OLD_LTW)
        /// <summary>
        /// return the avatar Image of the player
        /// by the format.
        /// if the format was not described, this 
        /// method will return null,
        /// so be carefull.
        /// </summary>
        /// <param name="format">
        /// The format of the image of this avatar.
        /// <code>---------------------</code>
        /// NOTICE: by Avatar format, I don't mean
        /// Image format(Like png or jpg or ...),
        /// I mean the format of the avatar itself that you want to 
        /// get the image of it,
        /// for example <see cref="AvatarFormat.Format01"/>
        /// should be used in <see cref="GameControls.ThroneLabel"/>
        /// for getting the Avatar of the royal memebers.
        /// </param>
        /// <returns></returns>
        public Image GetImage(AvatarFormat format)
        {
            Image myImage = null;
            switch (format)
            {
                case AvatarFormat.Format01:
                case AvatarFormat.Format02:
                case AvatarFormat.Format03:
                    myImage = Image.FromFile(ThereIsConstants.Path.Datas_Path +
                        ThereIsConstants.Path.DoubleSlash +
                        MyRes.GetString(strName:AvatarFullName));
                    break;
                default:
                    // ?:|
                    break;
            }
            return myImage;
        }
        
		#endif
		
		/// <summary>
        /// get the number, indicating the avatar's index.
        /// before use this for comparing two avatar, please
        /// check the equality of the avatars.
        /// </summary>
        /// <returns></returns>
        internal int GetNum()
        {
            switch (Kind)
            {
                case Avatar_Kind.Normal:
                    return (int)AvatarType;
                case Avatar_Kind.Special:
                    return (int)AvatarType_S;
                case Avatar_Kind.NPC:
                    return (int)AvatarType_N;
                default:
                    return BASE_INDEX;
            }
        }
        public bool IsNPC()
        {
            return Kind == Avatar_Kind.NPC;
        }
        #endregion
        //-------------------------------------------------
        #region overrided Methods Region
        public override string ToString()
        {
            return GetForServer().GetValue();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static Avatar ConvertToAvatar(Avatars theAvatar)
        {
            return new Avatar(theAvatar);
        }
        public static Avatar ConvertToAvatar(Avatars_S theAvatar)
        {
            return new Avatar(theAvatar);
        }
        internal static Avatar ConvertToAvatar(Avatars_N theAvatar)
        {
            return new Avatar(theAvatar);
        }
        public static Avatar ConvertToAvatar(StrongString theString)
        {
            try
            {
                switch (theString[true].ToUpper()[BASE_INDEX])
                {
                    case OrdinaryFirst:
                        return ConvertToAvatar(ConvertToAvatars(theString));
                    case SpecialFirst:
                        return ConvertToAvatar(ConvertToAvatars_S(theString));
                    case TheNPCFirst:
                        return ConvertToAvatar(ConvertToAvatars_N(theString));
                    default:
                        return GetDefaultAvatar();
                }
            }
            catch
            {
                // if there was any error, get the default avatar for the player.
                return GetDefaultAvatar();
            }
        }
        public static Avatars ConvertToAvatars(StrongString theString)
        {
            try
            {
                StrongString[] myStrings = theString.Split(CharSeparater);
                var myInt = myStrings[myStrings.Length - INDEX_COMPARER].ToInt32();
                if (myInt > ORDINARY_AVATAR_MAX)
                {
                    return Avatars.A_0;
                }
                return (Avatars)myInt;
            }
            catch
            {
                return Avatars.A_0;
            }
            
        }
        public static Avatars_S ConvertToAvatars_S(StrongString theString)
        {
            try
            {
                StrongString[] myStrings = theString.Split(CharSeparater);
                var myInt = myStrings[myStrings.Length - INDEX_COMPARER].ToInt32();
                if (myInt > SPECIAL_AVATAR_MAX)
                {
                    return Avatars_S.S_0;
                }
                return (Avatars_S)myInt;
            }
            catch
            {
                return Avatars_S.S_0;
            }
            
        }
        internal static Avatars_N ConvertToAvatars_N(StrongString theString)
        {
            try
            {
                StrongString[] myStrings = theString.Split(CharSeparater);
                var myInt = myStrings[myStrings.Length - INDEX_COMPARER].ToInt32();
                if (myInt > NPC_AVATAR_MAX)
                {
                    return Avatars_N.N_0;
                }
                return (Avatars_N)myInt;
            }
            catch
            {
                return Avatars_N.N_0;
            }

        }
        public static Avatar GetDefaultAvatar()
        {
            return new Avatar(Avatars.A_0);
        }
        #endregion
        //-------------------------------------------------
        #region operators Region
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="left">
        /// left avatar value
        /// </param>
        /// <param name="right">
        /// right avatar value
        /// </param>
        /// <returns></returns>
        public static bool operator ==(in Avatar left, in Avatar right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (right is null)
                {
                    return false;
                }
            }
            if (left.Kind != right.Kind)
            {
                return false;
            }
            return left.GetNum() == right.GetNum();
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="avatar">
        /// avatar value
        /// </param>
        /// <param name="type">
        /// type value
        /// </param>
        /// <returns></returns>
        public static bool operator ==(in Avatar avatar, in Avatars type)
        {
            if (avatar is null || avatar.Kind != Avatar_Kind.Normal)
            {
                return false;
            }
            return avatar.AvatarType == type;
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="type">
        /// type value
        /// </param>
        /// <param name="avatar">
        /// avatar value
        /// </param>
        /// <returns></returns>
        public static bool operator ==(in Avatars type, in Avatar avatar)
        {
            return avatar == type;
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="type">
        /// type value
        /// </param>
        /// <param name="avatar">
        /// avatar value
        /// </param>
        /// <returns></returns>
        public static bool operator ==(in Avatars_S type, in Avatar avatar)
        {
            return avatar == type;
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="avatar">
        /// avatar value
        /// </param>
        /// <param name="type">
        /// type value
        /// </param>
        /// <returns></returns>
        public static bool operator ==(in Avatar avatar, in Avatars_S type)
        {
            if (avatar is null || avatar.Kind != Avatar_Kind.Special)
            {
                return false;
            }
            return avatar.AvatarType_S == type;
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="left">
        /// left avatar value
        /// </param>
        /// <param name="right">
        /// right avatar value
        /// </param>
        /// <returns></returns>
        public static bool operator !=(in Avatar left, in Avatar right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (right is null)
                {
                    return true;
                }
            }
            if (left.Kind != right.Kind)
            {
                return true;
            }
            return left.GetNum() != right.GetNum();
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="avatar">
        /// avatar value
        /// </param>
        /// <param name="type">
        /// type value
        /// </param>
        /// <returns></returns>
        public static bool operator !=(in Avatar avatar, in Avatars type)
        {
            if (avatar is null || avatar.Kind != Avatar_Kind.Normal)
            {
                return true;
            }
            return avatar.AvatarType != type;
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="type">
        /// type value
        /// </param>
        /// <param name="avatar">
        /// avatar value
        /// </param>
        /// <returns></returns>
        public static bool operator !=(in Avatars type, in Avatar avatar)
        {
            return avatar != type;
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="avatar">
        /// avatar value
        /// </param>
        /// <param name="type">
        /// type value
        /// </param>
        /// <returns></returns>
        public static bool operator !=(in Avatar avatar, in Avatars_S type)
        {
            if (avatar is null || avatar.Kind != Avatar_Kind.Special)
            {
                return true;
            }
            return avatar.AvatarType_S != type;
        }
        /// <summary>
        /// check if these two values are the same or not.
        /// </summary>
        /// <param name="type">
        /// type value
        /// </param>
        /// <param name="avatar">
        /// avatar value
        /// </param>
        /// <returns></returns>
        public static bool operator !=(in Avatars_S type, in Avatar avatar)
        {
            return avatar != type;
        }
        #endregion
        //-------------------------------------------------
    }
}
