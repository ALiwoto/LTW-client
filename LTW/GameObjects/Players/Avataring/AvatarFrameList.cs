// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.Security;
using LTW.GameObjects.WMath;

namespace LTW.GameObjects.Players.Avataring
{
    /// <summary>
    /// the list of the avatar frames which allowed
    /// to use by player.
    /// </summary>
    public sealed class AvatarFrameList
    {
        //-------------------------------------------------
        #region Constants Region
        /// <summary>
        /// The Char Separator for Avatar Frame List.
        /// the value is: Ayashii.
        /// </summary>
        public const string CharSeparator = "怪";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public ListW<AvatarFrame> AvatarFrames { get; private set; }
        public int Length { get => AvatarFrames.Count; }
        public bool IsEmpty { get; private set; }
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private AvatarFrameList(AvatarFrame[] frames)
        {
            AvatarFrames = new ListW<AvatarFrame>(frames);
            IsEmpty = Length == 0;
        }
        public bool this[AvatarFrames frameType]
        {
            get
            {
                return GetIndex(frameType) != -1;
            }
        }
        public bool this[AvatarFrame frame]
        {
            get
            {
                if (frame == null)
                {
                    return false;
                }
                return this[frame.FrameType];
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
                myString += AvatarFrames[i].GetForServer() + CharSeparator;
            }
            return myString;
        }
        public void AddAvatarFrame(AvatarFrame frame)
        {
            if (AvatarFrames.Exists(frame))
            {
                return;
            }
            AvatarFrames.Add(frame);
            IsEmpty = Length == 0;
        }
        public void AddAvatarFrame(AvatarFrames frame)
        {
            AddAvatarFrame(AvatarFrame.GenerateAvatarFrame(frame));
        }
        public void RemoveAvatarFrame(int index)
        {
            if (index >= Length)
            {
                return;
            }
            AvatarFrames.RemoveAt(index);
            IsEmpty = Length == 0;
        }
        public void RemoveAvatarFrame(AvatarFrames frame_t)
        {
            if (!this[frame_t])
            {
                return;
            }
            RemoveAvatarFrame(GetIndex(frame_t));
            IsEmpty = Length == 0;
        }
        public void RemoveAvatarFrame(AvatarFrame frame)
        {
            RemoveAvatarFrame(frame.FrameType);
        }
        public int GetIndex(AvatarFrames frameType)
        {
            for (int i = 0; i < Length; i++)
            {
                if (AvatarFrames[i].FrameType == frameType)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static AvatarFrameList Parse(StrongString serverStringValue)
        {
            StrongString[] myStrings = serverStringValue.Split(CharSeparator);
            AvatarFrame[] frames = new AvatarFrame[myStrings.Length];
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = AvatarFrame.ParseToAvatarFrame(myStrings[i]);
            }
            return new AvatarFrameList(frames);
        }
        public static AvatarFrameList GenerateEmptyList()
        {
            return new AvatarFrameList(new AvatarFrame[0]);
        }
        #endregion
        //-------------------------------------------------
    }
}
