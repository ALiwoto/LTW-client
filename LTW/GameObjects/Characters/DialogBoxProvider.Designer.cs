// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Drawing;
#if (OLD_LTW)
using System.Windows.Forms;
#endif
using LTW.Constants;
using LTW.Controls;
using LTW.SandBox;
using LTW.GameObjects.Resources;

namespace LTW.GameObjects.Characters
{
    partial class DialogBoxProvider
    {
        //---------------------------------------------
        #region Initialize Region
        private void InitializeComponent()
        {
            this.CurrentStepOfDialog = 1;
            this.IsRightPictureBoxActive = 
                (bool)(CharactersHashTable[IsRight_CODE + CurrentStepOfDialog.ToString()]);
            //----------------------------------------
            //News:
            this.MyRes = new WotoRes(typeof(DialogBoxProvider));
#if DIALOG

            this.DialogLabel = new GameControls.DialogBoxBackGround(this);
            this.CharacterNameLabel = new GameControls.LabelControl(this, GameControls.LabelControlSpecies.CharacterNameInDialog);
            this.LeftCharacterPicture = new GameControls.PictureBoxControl(this);
            this.RightCharacterPicture = new GameControls.PictureBoxControl(this);
            //Names:

            //TabIndexes:
            //Fonts And TextAligns:
            this.DialogLabel.Font = new Font(
                ThereIsConstants.Forming.GameClient.PrivateFonts.Families[1], 19, FontStyle.Bold);
            this.CharacterNameLabel.Font =
                new Font(ThereIsConstants.Forming.GameClient.PrivateFonts.Families[1], 14, FontStyle.Bold);
            this.DialogLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.CharacterNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            //Sizes:

            if (IsRightPictureBoxActive)
            {
                this.RightCharacterPicture.Size =
                    new Size(ThereIsConstants.Forming.GameClient.Width / 5,
                    ThereIsConstants.Forming.GameClient.Height / 3);
                this.LeftCharacterPicture.Size =
                    new Size(ThereIsConstants.Forming.GameClient.Width / 6,
                    ThereIsConstants.Forming.GameClient.Height / 4);
            }
            else
            {
                this.LeftCharacterPicture.Size =
                    new Size(ThereIsConstants.Forming.GameClient.Width / 5,
                    ThereIsConstants.Forming.GameClient.Height / 3);
                this.RightCharacterPicture.Size =
                    new Size(ThereIsConstants.Forming.GameClient.Width / 6,
                    ThereIsConstants.Forming.GameClient.Height / 4);
            }
            //this.LeftCharacterPicture.BackColor = Color.Red;
            //Locations:
            this.DialogLabel.Location = 
                new Point((ThereIsConstants.Forming.GameClient.Width / 2) -
                (DialogLabel.Width / 2), ThereIsConstants.Forming.GameClient.Height - DialogLabel.Height);
            this.LeftCharacterPicture.Location =
                new Point(DialogLabel.Location.X +
                    (4 * (DialogLabel.Width / 80)) +
                (2 * (DialogLabel.Width / 80)) +
                (2 * (DialogLabel.Height / 16)),
                DialogLabel.Location.Y -
                LeftCharacterPicture.Height);
            this.RightCharacterPicture.Location =
                new Point(DialogLabel.Location.X + DialogLabel.Width -
                RightCharacterPicture.Width -
                (4 * (DialogLabel.Width / 80)) -
                (2 * (DialogLabel.Width / 80)) -
                (2 * (DialogLabel.Height / 16)),
                DialogLabel.Location.Y -
                RightCharacterPicture.Height);
            if (IsRightPictureBoxActive)
            {
                this.CharacterNameLabel.Location =
                    new Point(RightCharacterPicture.Location.X -
                    (CharacterNameLabel.Width + NoInternetConnectionSandBox.from_the_edge),
                    DialogLabel.Location.Y - (CharacterNameLabel.Height / 2));
            }
            else
            {
                this.CharacterNameLabel.Location =
                    new Point(LeftCharacterPicture.Location.X +
                    LeftCharacterPicture.Width + NoInternetConnectionSandBox.from_the_edge,
                    DialogLabel.Location.Y - (CharacterNameLabel.Height / 2));
            }
            //Colors:
            /* this.DialogLabel.BackColor = Color.FromArgb(179, Color.Black); */
            /*this.DialogLabel.ForeColor = Color.White;*/
            //this.CharacterNameLabel.BackColor = Color.White;
            //ComboBoxes:
            //Enableds:
            this.DialogLabel.SingleClick = true;
            //Texts:
            this.DialogLabel.SetLabelText(
                (string)(CharactersHashTable[DialogStrings_CODE + CurrentStepOfDialog.ToString()]));
            this.CharacterNameLabel.SetLabelText(
                ((CharacterInfo)CharactersHashTable[
                    ((string)CharactersHashTable[CharHashTable_CODE + CurrentStepOfDialog.ToString()])]).CharacterBlankName);
            //Images:
            this.LeftCharacterPicture.SizeMode =
                this.RightCharacterPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            if (IsRightPictureBoxActive)
            {
                this.RightCharacterPicture.Image = 
                    ((CharacterInfo)CharactersHashTable[
                    ((string)CharactersHashTable[CharHashTable_CODE + CurrentStepOfDialog.ToString()])]).GetCharacterImage();
            }
            else
            {
                this.LeftCharacterPicture.Image =
                    ((CharacterInfo)CharactersHashTable[
                    ((string)CharactersHashTable[CharHashTable_CODE + CurrentStepOfDialog.ToString()])]).GetCharacterImage();
            }
            //AddRanges:
            //ToolTipSettings:
            //Events:
            this.DialogLabel.Click += Owner_Click;
            this.DialogLabel.MessageLabel1.Click += Owner_Click;
            this.Father_ControlCollection.Owner.Click += Owner_Click;
            this.CharacterNameLabel.Click += Owner_Click;
            this.LeftCharacterPicture.Click += Owner_Click;
            this.RightCharacterPicture.Click += Owner_Click;
            //----------------------------------------

            //----------------------------------------
            Father_ControlCollection.AddRange(new Control[]
            {
                this.CharacterNameLabel,
                this.DialogLabel,
                this.RightCharacterPicture,
                this.LeftCharacterPicture
            });
#endif
        }
        #endregion
        //---------------------------------------------
        #region Events Region
        public void Owner_Click(object sender, EventArgs e)
        {
#if DIALOG

            if(CurrentStepOfDialog + 1 <= (int)CharactersHashTable[CharHashTable_CODE])
            {
                if (CurrentStepOfDialog + 1 == (int)CharactersHashTable[CharHashTable_CODE]) 
                {
                    this.AfterDialogEndedEvent?.Invoke(sender, e);
                }
                CurrentStepOfDialog++;
                this.IsRightPictureBoxActive =
                    (bool)(CharactersHashTable[IsRight_CODE + CurrentStepOfDialog.ToString()]);
                //----------------------------------------
                //News:
                //Names:

                //TabIndexes:
                //Fonts And TextAligns:
                //Sizes:
                /*
                this.DialogLabel.Size = 
                    new Size(8 * (ThereIsConstants.Forming.GameClient.Width / 10),
                    ThereIsConstants.Forming.GameClient.Height / 4);
                */
                /*
                this.CharacterNameLabel.Size = new Size(ThereIsConstants.Forming.GameClient.Width / 8,
                    ThereIsConstants.Forming.GameClient.Height / 7); */
                if (IsRightPictureBoxActive)
                {
                    this.RightCharacterPicture.Size =
                        new Size(ThereIsConstants.Forming.GameClient.Width / 5,
                        ThereIsConstants.Forming.GameClient.Height / 3);
                    this.LeftCharacterPicture.Size =
                        new Size(ThereIsConstants.Forming.GameClient.Width / 6,
                        ThereIsConstants.Forming.GameClient.Height / 4);
                }
                else
                {
                    this.LeftCharacterPicture.Size =
                        new Size(ThereIsConstants.Forming.GameClient.Width / 5,
                        ThereIsConstants.Forming.GameClient.Height / 3);
                    this.RightCharacterPicture.Size =
                        new Size(ThereIsConstants.Forming.GameClient.Width / 6,
                        ThereIsConstants.Forming.GameClient.Height / 4);
                }
                //this.LeftCharacterPicture.BackColor = Color.Red;
                //Locations:
                this.LeftCharacterPicture.Location =
                new Point(DialogLabel.Location.X +
                    (4 * (DialogLabel.Width / 80)) +
                (2 * (DialogLabel.Width / 80)) +
                (2 * (DialogLabel.Height / 16)),
                DialogLabel.Location.Y -
                LeftCharacterPicture.Height);
                this.RightCharacterPicture.Location =
                    new Point(DialogLabel.Location.X + DialogLabel.Width -
                    RightCharacterPicture.Width -
                    (4 * (DialogLabel.Width / 80)) -
                    (2 * (DialogLabel.Width / 80)) -
                    (2 * (DialogLabel.Height / 16)),
                    DialogLabel.Location.Y -
                    RightCharacterPicture.Height);
                if (IsRightPictureBoxActive)
                {
                    this.CharacterNameLabel.Location =
                        new Point(RightCharacterPicture.Location.X -
                        (CharacterNameLabel.Width + NoInternetConnectionSandBox.from_the_edge),
                        DialogLabel.Location.Y - (CharacterNameLabel.Height / 2));
                }
                else
                {
                    this.CharacterNameLabel.Location =
                        new Point(LeftCharacterPicture.Location.X +
                        LeftCharacterPicture.Width + NoInternetConnectionSandBox.from_the_edge,
                        DialogLabel.Location.Y - (CharacterNameLabel.Height / 2));
                }
                

                //Colors:
                /* this.DialogLabel.BackColor = Color.FromArgb(179, Color.Black); */
                /*this.DialogLabel.ForeColor = Color.White;*/
                //this.CharacterNameLabel.BackColor = Color.White;
                //ComboBoxes:
                //Enableds:
                //Texts:
                this.DialogLabel.SetLabelText(
                    (string)(CharactersHashTable[DialogStrings_CODE + CurrentStepOfDialog.ToString()]));
                this.CharacterNameLabel.SetLabelText(
                    ((CharacterInfo)CharactersHashTable[
                        ((string)CharactersHashTable[CharHashTable_CODE + CurrentStepOfDialog.ToString()])]).CharacterBlankName);
                //Images:
                if (IsRightPictureBoxActive)
                {
                    this.RightCharacterPicture.Image =
                        ((CharacterInfo)CharactersHashTable[
                        ((string)CharactersHashTable[CharHashTable_CODE + CurrentStepOfDialog.ToString()])]).GetCharacterImage();
                }
                else
                {
                    this.LeftCharacterPicture.Image =
                        ((CharacterInfo)CharactersHashTable[
                        ((string)CharactersHashTable[CharHashTable_CODE + CurrentStepOfDialog.ToString()])]).GetCharacterImage();
                }
                //AddRanges:
                //ToolTipSettings:
            }
            else
            {
                if (sender is Control myCon) 
                {
                    myCon.Click -= Owner_Click;
                }
            }
            if (sender is GameControls.LabelControl myLA) 
            {
                myLA.HasMouseClickedOnce = false;
            }
#endif
        }
        #endregion
        //---------------------------------------------
    }
}
