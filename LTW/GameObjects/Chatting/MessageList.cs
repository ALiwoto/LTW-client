// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Collections.Generic;
using LTW.Security;
using LTW.Constants;

namespace LTW.GameObjects.Chatting
{
    /// <summary>
    /// List of Messages.
    /// </summary>
    public class MessageList : List<ChatMessage>
    {
        //-------------------------------------------------
        #region Constants Region
        public const string CharSeparator = ChatManager.CharSeparator;
        private const string ToStringValue =
            ThereIsConstants.AppSettings.CompanyCopyRight +
            " MessageList BY mrwoto";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public int Length { get => Count; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        public MessageList(ChatMessage[] messages) :
            base(messages)
        {

        }
        public bool this[ChatMessage message]
        {
            get
            {
                for (int i = 0; i < Length; i++)
                {
                    if (this[i] == message)
                    {
                        return true;
                    }
                }
                return false;
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
                myString += this[i].GetForServer() + CharSeparator;
            }
            return myString;
        }
        public void ReviewMessagesAvatar()
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i].IsMe)
                {
                    this[i].ReloadPlayerAvatar();
                }
            }
        }
        public void ReviewMessagesAvatarFrame()
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i].IsMe)
                {
                    this[i].ReloadPlayerAvatarFrame();
                }
            }
        }
        public void ReviewMessagesPos()
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i].IsMe)
                {
                    this[i].ReloadPlayerPos();
                }
            }
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static MessageList GetEmptyList()
        {
            return new MessageList(new ChatMessage[0]);
        }
        #endregion
        //-------------------------------------------------
        #region overrided Methods Region
        public override string ToString()
        {
            return ToStringValue;
        }
        #endregion
        //-------------------------------------------------
    }
}
