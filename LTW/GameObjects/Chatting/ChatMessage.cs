// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using WotoProvider.Interfaces;
using LTW.Security;
using LTW.Controls;
using LTW.Constants;
using LTW.GameObjects.Players;
using LTW.GameObjects.ServerObjects;
using LTW.GameObjects.Players.Avataring;

namespace LTW.GameObjects.Chatting
{
    public sealed class ChatMessage
    {
        //-------------------------------------------------
        #region Constants Region
        public const string CharSeparator = "：空！＊";
        public const string Preference = "/";
        public const string ToStringValue = "Chat Message, containing Chat Data of the " +
            "Chat Messages in LTW || BY: wotoTeam";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// The Sender of this Message.
        /// The Code is: 1.
        /// </summary>
        public StrongString SenderName { get; }
        /// <summary>
        /// The Message Context.
        /// The Code is: 2.
        /// </summary>
        public StrongString MessageContext { get; }
        //-------------------------------------------------
        /// <summary>
        /// The Social Position of the sender.
        /// The Code is: 3.
        /// </summary>
        public SocialPosition SenderSocialPosition { get; private set; }
        /// <summary>
        /// The avatar of the sender.
        /// The Code is: 4.
        /// </summary>
        public Avatar SenderAvatar { get; private set; }
        /// <summary>
        /// The Avatar Frame of the sender.
        /// The Code is: 5.
        /// </summary>
        public AvatarFrame SenderAvatarFrame { get; private set; }
        public IDateProvider<DateTime, Trigger, StrongString> SendDateTime { get; }
        //-------------------------------------------------
        /// <summary>
        /// check if this is a command or not.
        /// </summary>
        public bool IsCommand { get; }
        public bool IsMe { get; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private ChatMessage(StrongString sender,
            StrongString context,
            SocialPosition position,
            Avatar avatar,
            AvatarFrame frame,
            IDateProvider<DateTime, Trigger, StrongString> dateTime = default)
        {
            SenderName              = sender;
            MessageContext          = context;
            SenderSocialPosition    = position;
            SenderAvatar            = avatar;
            SenderAvatarFrame             = frame;
            if (dateTime == default)
            {
                SendDateTime        = ThereIsConstants.AppSettings.GlobalTiming;
            }
            else
            {
                SendDateTime        = dateTime;
            }
            IsCommand               = CheckForCommand();
            IsMe                    = 
                ThereIsServer.GameObjects.MyProfile.PlayerName == SenderName;
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region
        public StrongString GetForServer()
        {
            StrongString myString =
                SenderName                              + CharSeparator + // 1
                MessageContext                          + CharSeparator + // 2
                SenderSocialPosition.GetForServer()     + CharSeparator + // 3
                SenderAvatar.GetForServer()             + CharSeparator + // 4
                SenderAvatarFrame.GetForServer()              + CharSeparator + // 5
                SendDateTime.GetForServer()             + CharSeparator;  // 6
            return myString;
        }
        public void ReloadPlayerAvatar()
        {
            if (!IsMe)
            {
                return;
            }
            if (ThereIsServer.GameObjects.MyProfile.PlayerAvatar != SenderAvatar)
            {
                SenderAvatar = ThereIsServer.GameObjects.MyProfile.PlayerAvatar;
            }
        }
        public void ReloadPlayerAvatarFrame()
        {
            if (!IsMe)
            {
                return;
            }
            if (ThereIsServer.GameObjects.MyProfile.PlayerAvatarFrame != SenderAvatarFrame)
            {
                SenderAvatarFrame = ThereIsServer.GameObjects.MyProfile.PlayerAvatarFrame;
            }
        }
        public void ReloadPlayerPos()
        {
            if (!IsMe)
            {
                return;
            }
            if (ThereIsServer.GameObjects.MyProfile.SocialPosition != SenderSocialPosition)
            {
                SenderSocialPosition = ThereIsServer.GameObjects.MyProfile.SocialPosition;
            }
        }
        public bool IsSameSnder(ChatMessage message)
        {
            return SenderName == message.SenderName;
        }
        private bool CheckForCommand()
        {
            if (MessageContext.Length < Preference.Length)
            {
                return false;
            }
            if (MessageContext.GetValue().Substring(0, Preference.Length) == Preference)
            {
                return true;
            }
            return false;
        }
        #endregion
        //-------------------------------------------------
        #region overrided Method's Region
        public override string ToString()
        {
            return ToStringValue;
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
        public static ChatMessage GenerateChatMessage(Me theMe, StrongString theMessage)
        {
            ChatMessage message;
            message = new ChatMessage(theMe.PlayerName,
                theMessage,
                theMe.SocialPosition,
                theMe.PlayerAvatar,
                theMe.PlayerAvatarFrame,
                ThereIsConstants.AppSettings.GlobalTiming);
            return message;
        }
        public static ChatMessage ParseExact(StrongString serverValueString)
        {
            ChatMessage message;
            StrongString[] myString = serverValueString.Split(CharSeparator);
            message = new ChatMessage(myString[0],                  // 1
                myString[1],                                        // 2
                SocialPosition.GetSocialPosition(myString[2]),        // 3
                Avatar.ConvertToAvatar(myString[3]),                // 4
                AvatarFrame.ParseToAvatarFrame(myString[4]),        // 5
                myString.Length > 5 ? 
                ThereIsConstants.Actions.ToDateTime(myString[5]) : 
                null);                                              // 6
            return message;
        }
        #endregion
        //-------------------------------------------------
        #region operator's Region
        public static bool operator ==(ChatMessage left, ChatMessage right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (right is null)
                {
                    return false;
                }
            }
            if (left.MessageContext == right.MessageContext)
            {
                if (left.SendDateTime.GetDateTime() == right.SendDateTime.GetDateTime())
                {
                    if (left.SenderName == right.SenderName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool operator !=(ChatMessage left, ChatMessage right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (right is null)
                {
                    return true;
                }
            }
            if (left.MessageContext == right.MessageContext)
            {
                if (left.SendDateTime.GetDateTime() == right.SendDateTime.GetDateTime())
                {
                    if (left.SenderName == right.SenderName)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        //-------------------------------------------------
    }
}
