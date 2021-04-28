// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Drawing;
using LTW.Security;
using LTW.Controls;
using LTW.Constants;
using LTW.GameObjects.Resources;
using LTW.Controls.GameGraphics;

namespace LTW.GameObjects.Players
{
    public sealed partial class SocialPosition : IRes
    {
        //-------------------------------------------------
        #region Constant's Region
        public const string COMMAND_PREF1 = "pos";
        public const string COMMAND_PREF2 = "position";
        public const string COMMAND_CharSeparator = "_";
        private const string LEFT_PREF  = "[";
        private const string RIGHT_PREF = "]";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public WotoRes MyRes { get; set; }
        public PlayerSocialPosition Position { get; private set; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        private SocialPosition(PlayerSocialPosition _position)
        {
            Position = _position;
            InitializeComponent();
        }
        #endregion
        //-------------------------------------------------
        #region Set Method's Region
        public void ChangePosition(PlayerSocialPosition _position)
        {
            Position = _position;
        }

        #endregion
        //-------------------------------------------------
        #region Get Method's Region
        public StrongString GetForServer()
        {
            return ((int)Position).ToString();
        }
        public ColorW GetColorW()
        {
            switch (Position)
            {
                case PlayerSocialPosition.OrdinaryPlayer:
                    return ColorW.ConvertToColorW(Color.Black);
                case PlayerSocialPosition.King:
                    return ColorW.ConvertToColorW(Color.Goldenrod);
                case PlayerSocialPosition.Queen:
                    return ColorW.ConvertToColorW(Color.Pink);
                case PlayerSocialPosition.MinisterOfWar:
                    return ColorW.ConvertToColorW(Color.DodgerBlue);
                case PlayerSocialPosition.MinisterOfWealth:
                    return ColorW.ConvertToColorW(Color.LightYellow);
                case PlayerSocialPosition.Hierarch:
                    return ColorW.ConvertToColorW(Color.LightGray);
                case PlayerSocialPosition.Guardians_Chief:
                    return ColorW.ConvertToColorW(Color.AliceBlue);
                case PlayerSocialPosition.Clown:
                    return ColorW.ConvertToColorW(Color.IndianRed);
                case PlayerSocialPosition.Thief:
                    return ColorW.ConvertToColorW(Color.Azure);
                case PlayerSocialPosition.Revolutionary:
                    return ColorW.ConvertToColorW(Color.DarkKhaki);
                case PlayerSocialPosition.Magician:
                    return ColorW.ConvertToColorW(Color.HotPink); ;
                case PlayerSocialPosition.Idiot:
                    break;
                case PlayerSocialPosition.Vulgar:
                    break;
                case PlayerSocialPosition.Rogue:
                    break;
                case PlayerSocialPosition.Merchant:
                    break;
                case PlayerSocialPosition.Dragon:
                    return ColorW.ConvertToColorW(Color.DarkGoldenrod);
                case PlayerSocialPosition.Admin:
                    return ColorW.ConvertToColorW(Color.PaleGoldenrod);
                case PlayerSocialPosition.Owner:
                    return ColorW.ConvertToColorW(Color.PaleVioletRed);
                default:
                    return ColorW.ConvertToColorW(Color.Transparent);
            }
            return ColorW.ConvertToColorW(Color.Transparent);
        }
        public StrongString GetString()
        {
            var myString = MyRes.GetString(Position.ToString() +
                ThereIsConstants.ResourcesName.Separate_Character +
                ThereIsConstants.AppSettings.Language);
            if (myString != null)
            {
                return LEFT_PREF + myString + RIGHT_PREF;
            }
            return StrongString.Empty;
        }
        #endregion
        //-------------------------------------------------
        #region overrided Method's Region
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
        #region static Method's Region
        public static SocialPosition GetSocialPosition(PlayerSocialPosition _position)
        {
            return new SocialPosition(_position);
        }
        public static SocialPosition GetSocialPosition(in StrongString value)
        {
            try
            {
                PlayerSocialPosition _pos;
                if (value.ToLower().IndexOf(COMMAND_PREF1, COMMAND_PREF2))
                {
                    StrongString[] myStrings = value.Split(COMMAND_CharSeparator);
                    _pos = (PlayerSocialPosition)myStrings[1].ToInt32();
                    return GetSocialPosition(_pos);
                }
                _pos = (PlayerSocialPosition)value.ToInt32();
                return GetSocialPosition(_pos);
            }
            catch
            {
                return GetSocialPosition();
            }
        }
        /// <summary>
        /// Get the default social position in the game.
        /// </summary>
        /// <returns></returns>
        public static SocialPosition GetSocialPosition()
        {
            return GetSocialPosition(PlayerSocialPosition.OrdinaryPlayer);
        }
        #endregion
        //-------------------------------------------------
        #region operator's Region
        public static bool operator ==(SocialPosition left, SocialPosition right)
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
                else
                {
                    return left.Position == right.Position;
                }
            }
        }
        public static bool operator !=(SocialPosition left, SocialPosition right)
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
                else
                {
                    return left.Position != right.Position;
                }
            }
        }
        #endregion
        //-------------------------------------------------
    }
}
