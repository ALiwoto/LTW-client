// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Drawing;
using LTW.Controls;
using LTW.Constants;
using LTW.Security;
//using LTW.Controls.Elements.ChatElements;
using LTW.GameObjects.Resources;

namespace LTW.GameObjects.Players.Avataring
{
    public sealed partial class AvatarFrame : IRes
    {
        //-------------------------------------------------
        #region Constants Region
        public const string COMMAND_Separator   = "_";
        public const int INDEX_COMPARER         = 1;
        private const float F_0_X_COST          = 0;
        private const float F_0_Y_COST          = 0;
        private const float F_0_W_COST          = 0;
        private const float F_0_H_COST          = 0;
        private const float F_0_X_EDGE          = 0;
        private const float F_0_Y_EDGE          = 0;
        private const float F_0_W_EDGE          = 0;
        private const float F_0_H_EDGE          = 0;
        private const float F_1_TO_8_X_COST = -5.75f;
        private const float F_1_TO_8_Y_COST = -5.75f;
        private const float F_1_TO_8_W_COST = 18.0f;
        private const float F_1_TO_8_H_COST = 18.0f;
        private const float F_1_TO_8_X_EDGE = 6.0f;
        private const float F_1_TO_8_Y_EDGE = 6.0f;
        private const float F_1_TO_8_W_EDGE = -6.0f;
        private const float F_1_TO_8_H_EDGE = -6.0f;
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public WotoRes MyRes { get; set; }
        public string FrameFullName { get; }
        public bool IsDefault
        {
            get => FrameType == AvatarFrames.F_0;
        }
        public AvatarFrames FrameType { get; }
        #endregion
        //-------------------------------------------------
        #region Constructor Region
        private AvatarFrame(AvatarFrames frame)
        {
            FrameFullName = frame.ToString();
            FrameType = frame;
            InitializeComponent();
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region
        public StrongString GetForServer()
        {
            return ((int)FrameType).ToString().ToStrong();
        }

		#if (OLD_LTW)
        public Image GetImage(AvatarFormat format)
        {
            Image myImage = null;
            if (FrameType == AvatarFrames.F_0)
            {
                // return null image, cuz F_0 
                // means player has no avatar frame equiped.
                return myImage;
            }
            myImage = Image.FromFile(ThereIsConstants.Path.Datas_Path +
                        ThereIsConstants.Path.DoubleSlash +
                    MyRes.GetString(FrameFullName).GetValue());
            Size mySize = GetSize(format);
            switch (format)
            {
                case AvatarFormat.Format01:
                    
                    break;
                case AvatarFormat.Format03:
                    var another = myImage;
                    myImage = new Bitmap(myImage, mySize);
                    another.Dispose();
                    break;
                default:
                    // ?:|
                    break;
            }
            return myImage;
        }
        #endif
		
		public float GetXCost()
        {
            switch (FrameType)
            {
                case AvatarFrames.F_0:
                    return F_0_X_COST;       
                case AvatarFrames.F_1:
                case AvatarFrames.F_2:
                case AvatarFrames.F_3:
                case AvatarFrames.F_4:
                case AvatarFrames.F_5:
                case AvatarFrames.F_6:
                case AvatarFrames.F_7:
                case AvatarFrames.F_8:
                    return F_1_TO_8_X_COST;
                default:
                    return F_0_X_COST;
            }
        }
        public float GetYCost()
        {
            switch (FrameType)
            {
                case AvatarFrames.F_0:
                    return F_0_Y_COST;
                case AvatarFrames.F_1:
                case AvatarFrames.F_2:
                case AvatarFrames.F_3:
                case AvatarFrames.F_4:
                case AvatarFrames.F_5:
                case AvatarFrames.F_6:
                case AvatarFrames.F_7:
                case AvatarFrames.F_8:
                    return F_1_TO_8_Y_COST;
                default:
                    return F_0_Y_COST;
            }
        }
        public float GetWCost()
        {
            switch (FrameType)
            {
                case AvatarFrames.F_0:
                    return F_0_W_COST;
                case AvatarFrames.F_1:
                case AvatarFrames.F_2:
                case AvatarFrames.F_3:
                case AvatarFrames.F_4:
                case AvatarFrames.F_5:
                case AvatarFrames.F_6:
                case AvatarFrames.F_7:
                case AvatarFrames.F_8:
                    return F_1_TO_8_W_COST;
                default:
                    return F_0_W_COST;
            }
        }
        public float GetHCost()
        {
            switch (FrameType)
            {
                case AvatarFrames.F_0:
                    return F_0_H_COST;
                case AvatarFrames.F_1:
                case AvatarFrames.F_2:
                case AvatarFrames.F_3:
                case AvatarFrames.F_4:
                case AvatarFrames.F_5:
                case AvatarFrames.F_6:
                case AvatarFrames.F_7:
                case AvatarFrames.F_8:
                    return F_1_TO_8_H_COST;
                default:
                    return F_0_H_COST;
            }
        }
        public float GetXEdge()
        {
            switch (FrameType)
            {
                case AvatarFrames.F_0:
                    return F_0_X_EDGE;
                case AvatarFrames.F_1:
                case AvatarFrames.F_2:
                case AvatarFrames.F_3:
                case AvatarFrames.F_4:
                case AvatarFrames.F_5:
                case AvatarFrames.F_6:
                case AvatarFrames.F_7:
                case AvatarFrames.F_8:
                    return F_1_TO_8_X_EDGE;
                default:
                    return F_0_X_EDGE;
            }
        }
        public float GetYEdge()
        {
            switch (FrameType)
            {
                case AvatarFrames.F_0:
                    return F_0_Y_EDGE;
                case AvatarFrames.F_1:
                case AvatarFrames.F_2:
                case AvatarFrames.F_3:
                case AvatarFrames.F_4:
                case AvatarFrames.F_5:
                case AvatarFrames.F_6:
                case AvatarFrames.F_7:
                case AvatarFrames.F_8:
                    return F_1_TO_8_Y_EDGE;
                default:
                    return F_0_Y_EDGE;
            }
        }
        public float GetWEdge()
        {
            switch (FrameType)
            {
                case AvatarFrames.F_0:
                    return F_0_W_EDGE;
                case AvatarFrames.F_1:
                case AvatarFrames.F_2:
                case AvatarFrames.F_3:
                case AvatarFrames.F_4:
                case AvatarFrames.F_5:
                case AvatarFrames.F_6:
                case AvatarFrames.F_7:
                case AvatarFrames.F_8:
                    return F_1_TO_8_W_EDGE;
                default:
                    return F_0_W_EDGE;
            }
        }
        public float GetHEdge()
        {
            switch (FrameType)
            {
                case AvatarFrames.F_0:
                    return F_0_H_EDGE;
                case AvatarFrames.F_1:
                case AvatarFrames.F_2:
                case AvatarFrames.F_3:
                case AvatarFrames.F_4:
                case AvatarFrames.F_5:
                case AvatarFrames.F_6:
                case AvatarFrames.F_7:
                case AvatarFrames.F_8:
                    return F_1_TO_8_H_EDGE;
                default:
                    return F_0_H_EDGE;
            }
        }
        private Size GetSize(AvatarFormat format)
        {
            switch (format)
            {
                case AvatarFormat.Format01:
                    return default;
                case AvatarFormat.Format02:
                    return default;
                case AvatarFormat.Format03:
                    //return
                        //Size.Round(new SizeF(ChatElement.AVATAR_SIZE, ChatElement.AVATAR_SIZE));
                default:
                    break;
            }
            return default;
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static AvatarFrame GetDefaultAvatarFrame()
        {
            return new AvatarFrame(AvatarFrames.F_0);
        }
        public static AvatarFrame GenerateAvatarFrame(in AvatarFrames frame)
        {
            try
            {
                return new AvatarFrame(frame);
            }
            catch
            {
                return GetDefaultAvatarFrame();
            }
        }
        public static AvatarFrame ParseToAvatarFrame(in StrongString serverValueString)
        {
            try
            {
                StrongString[] myStrings = serverValueString.Split(COMMAND_Separator);
                return 
                    GenerateAvatarFrame(
                        (AvatarFrames)myStrings[myStrings.Length - INDEX_COMPARER].ToInt32());
            }
            catch
            {
                return GetDefaultAvatarFrame();
            }
           
        }
        #endregion
        //-------------------------------------------------
    }
}
