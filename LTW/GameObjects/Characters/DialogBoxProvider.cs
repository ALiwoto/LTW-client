// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Collections;
using LTW.Controls;
using LTW.GameObjects.Resources;

#pragma warning disable

namespace LTW.GameObjects.Characters
{
    public partial class DialogBoxProvider : IRes
    {
        //------------------------------------------------
        #if (OLD_LTW)
        public ControlCollection Father_ControlCollection { get; }
        #endif
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Hashtable CharactersHashTable { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DialogContext Context { get; set; }
#if DIALOG

        public GameControls.DialogBoxBackGround DialogLabel { get; set; }
        public GameControls.LabelControl CharacterNameLabel { get; set; }
        public GameControls.PictureBoxControl LeftCharacterPicture { get; set; }
        public GameControls.PictureBoxControl RightCharacterPicture { get; set; }
#endif
        //public GameControls.PictureBoxControl HexagonPictureBox { get; set; }
        public WotoRes MyRes { get; set; }
        /// <summary>
        /// After the Dialog ended, this event will be raised.
        /// </summary>
        public event DialogEndedEventHandler AfterDialogEndedEvent;
        //------------------------------------------------
        public int CurrentStepOfDialog { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public bool IsRightPictureBoxActive { get; set; }
        public bool InClickedWorking { get; set; }
        //------------------------------------------------
        /// <summary>
        /// this will give you ushort (for charactersCount),
        /// also use it in Hashtable with ushort less than 
        /// this value, after that, you will get the <see cref="Character.CharacterName"/> string,
        /// use this string to get the <see cref="Character"/> object.
        /// for example -> Characters[CharHashTable_CODE + "Kotomine"], it will gives you 
        /// the character object directly.
        /// </summary>
        public const string CharHashTable_CODE  = "Chars_";
        public const string DialogStrings_CODE  = "Dialog_";
        public const string IsRight_CODE        = "IsRight_";
        public const string HexagonImgNameInRes = "HxgnImgFile_Name";
        //------------------------------------------------
#pragma warning disable IDE0028
        #if (OLD_LTW)
        /// <summary>
        /// Creating a new DialogBoxProvider.
        /// Writed by ALiwoto :)
        /// All rights reserved.
        /// </summary>
        /// <param name="father_ControlCollectionValue"></param>
        public DialogBoxProvider(ControlCollection father_ControlCollectionValue,
            ref DialogContext dialogContext)
        {
            InClickedWorking                        = false;
            CharactersHashTable                     = new Hashtable();
            CharactersHashTable[CharHashTable_CODE] = 0;
            Father_ControlCollection                = father_ControlCollectionValue;
            Context                                 = dialogContext;
            CurrentStepOfDialog                     = 0;
        }


#endif



#pragma warning restore IDE0028
        public void SettingUpDialogWorks()
        {
            char[] myChars = (DialogContext.CharacterNameSeparater +
                DialogContext.LeftOrRightSeparater +
                DialogContext.MainContextSeparater +
                DialogContext.Separater).ToCharArray();

            string[] myStrings = Context.Context.Split(myChars,
                StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < myStrings.Length; i += 4)
            {
                // ReSharper disable once HeapView.BoxingAllocation
                // ReSharper disable once PossibleNullReferenceException
                CharactersHashTable[CharHashTable_CODE] = (int)CharactersHashTable[CharHashTable_CODE] + 1;
                CharacterInfo myChar = 
                    new CharacterInfo(myStrings[i] + DialogContext.CharacterNameSeparater +
                    myStrings[i + 1]); 
                CharactersHashTable[CharHashTable_CODE +
                    (int)CharactersHashTable[CharHashTable_CODE]] = myChar.CharacterName;
                if(CharactersHashTable[myChar.CharacterName] == null)
                {
                    CharactersHashTable[myChar.CharacterName] = myChar;
                }
                // ReSharper disable once RedundantToStringCallForValueType
                // ReSharper disable once HeapView.BoxingAllocation
                CharactersHashTable[IsRight_CODE +
                    ((int)CharactersHashTable[CharHashTable_CODE]).ToString()] =
                    myStrings[i + 2] == DialogContext.Right;
                // ReSharper disable once RedundantToStringCallForValueType
                CharactersHashTable[DialogStrings_CODE +
                    ((int)CharactersHashTable[CharHashTable_CODE]).ToString()] = myStrings[i + 3];
            }
            InitializeComponent();
        }
        public void CleaningUp()
        {
#if DIALOG

            Father_ControlCollection.Remove(CharacterNameLabel);
            Father_ControlCollection.Remove(DialogLabel);
            if(Father_ControlCollection.Contains(LeftCharacterPicture))
            {
                Father_ControlCollection.Remove(LeftCharacterPicture);
            }
            if (Father_ControlCollection.Contains(RightCharacterPicture))
            {
                Father_ControlCollection.Remove(LeftCharacterPicture);
            }
            //---------------------------------
            CharacterNameLabel.Dispose();
            DialogLabel.Dispose();
            CharacterNameLabel  = null;
            DialogLabel         = null;
            if(LeftCharacterPicture != null)
            {
                LeftCharacterPicture.Dispose();
                LeftCharacterPicture = null;
            }
            if(RightCharacterPicture != null)
            {
                RightCharacterPicture.Dispose();
                RightCharacterPicture = null;
            }
#endif
            GC.Collect();
        }
    }
}
