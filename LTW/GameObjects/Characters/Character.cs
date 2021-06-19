// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using LTW.Controls;
using LTW.Constants;
using LTW.GameObjects.Resources;

namespace LTW.GameObjects.Characters
{
    public abstract partial class Character : IRes
    {

        /// <summary>
        /// this parameter will give you the name with number,
        /// for example: "Kotomine1"
        /// </summary>
        public string CharacterName { get; protected set; }
        /// <summary>
        /// this parameter will give you the Name without Status,
        /// for example: "Kotomine"
        /// </summary>
        public string CharacterBlankName { get; protected set; }
        /// <summary>
        /// Status of this Character.
        /// </summary>
        public uint Status { get; protected set; }
        public WotoRes MyRes { get; set; }
        //------------------------------------------
        /// <summary>
        /// CharacterName + this value.
        /// </summary>
        public const string ImageEndFileName = "_I";
        public Character()
        {
            InitializeComponent();
        }
		#if (OLD_LTW)
        /// <summary>
        /// Get The Character Image which should be displayed in
        /// <see cref="DialogBoxProvider"/> class.
        /// </summary>
        /// <returns></returns>
        public Image GetCharacterImage()
        {
            return Image.FromFile(ThereIsConstants.Path.Datas_Path +
                ThereIsConstants.Path.DoubleSlash +
                MyRes.GetString(strName:CharacterName + ImageEndFileName));
        }
		#endif
    }
}
