// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Collections.Generic;
using LTW.Security;
using LTW.GameObjects.Players;

namespace LTW.GameObjects.Chatting
{
    /// <summary>
    /// using this as a list for checking 
    /// if a player is in blocklist or not.
    /// </summary>
    public class ChatBlockList
    {
        //-------------------------------------------------
        #region Constants Region
        public const string CharSeparator = "＆";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public List<StrongString> Names { get; private set; }
        public int Length
        {
            get
            {
                return Names.Count;
            }
        }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private ChatBlockList(StrongString[] names)
        {
            Names = new List<StrongString>(names);
        }
        public int this[StrongString playerName]
        {
            get
            {
                for (int i = 0; i < Names.Count; i++)
                {
                    if (playerName == Names[i])
                    {
                        return i;
                    }
                }
                return -1;
            }
        }
        public int this[PlayerInfo player]
        {
            get
            {
                return this[player.PlayerName];
            }
        }
        public StrongString this[int index]
        {
            get
            {
                if (index < Names.Count && index >= 0)
                {
                    return Names[index];
                }
                else
                {
                    return null; // it means the index is out of the range.
                }
            }
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region
        public StrongString GetForServer()
        {
            // please don't use string.Empty
            StrongString myString = CharSeparator;
            for (int i = 0; i < Names.Count; i++)
            {
                myString += Names[i] + CharSeparator;
            }
            return myString;
        }
        public bool Exist(PlayerInfo player)
        {
            return Exist(player.PlayerName);
        }
        public bool Exist(StrongString playerName)
        {
            for (int i = 0; i < Names.Count; i++)
            {
                if (playerName == Names[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void Add(PlayerInfo player)
        {
            Add(player.PlayerName);
        }
        public void Add(StrongString playerName)
        {
            Names.Add(playerName);
        }
        public void Remove(PlayerInfo player)
        {
            Remove(player.PlayerName);
        }
        public void Remove(StrongString playerName)
        {
            if (Exist(playerName))
            {
                Names.Remove(playerName);
            }
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static ChatBlockList Parse(QString serverStringValue)
        {
            ChatBlockList blockList;
            StrongString[] myStrings =
                serverStringValue.GetStrong().Split(CharSeparator);
            blockList = new ChatBlockList(myStrings);
            return blockList;
        }
        public static ChatBlockList Parse(StrongString serverStringValue)
        {
            ChatBlockList blockList;
            StrongString[] myStrings =
                serverStringValue.Split(CharSeparator);
            blockList = new ChatBlockList(myStrings);
            return blockList;
        }
        public static ChatBlockList GenerateBlankChatBlockList()
        {
            return new ChatBlockList(new StrongString[0]);
        }
        #endregion
        //-------------------------------------------------
    }
}
