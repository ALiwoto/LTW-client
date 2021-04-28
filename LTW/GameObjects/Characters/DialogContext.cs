// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
#if (OLD_LTW)
using System.Windows.Forms;
#endif
using System.Runtime.Serialization.Formatters.Binary;
using LTW.Controls;
using LTW.Constants;
using LTW.Client;
using LTW.GameObjects;

namespace LTW.GameObjects.Characters
{
    [Serializable]
    public class DialogContext
    {
        //------------------------------------
        public const string Separater = "う";
        public const string LeftOrRightSeparater = "ｒｌ";
        public const string Left = "L";
        public const string Right = "R";
        /// <summary>
        /// After this, you should right the CurrentStatus of the character
        /// </summary>
        public const string CharacterNameSeparater = "ｄ";
        public const string MainContextSeparater = "ｍ";
        public const string DialogFirstName = "d";
        public const char DialogNameSeparater = '_';
        //------------------------------------
        private string context;
        private string dialogCaption;
        /// <summary>
        /// Create a new Dialog.
        /// </summary>
        public DialogContext(string DialogCaptionValue, bool createNew)
        {
            if (createNew)
            {
                context = "";
                DialogCaption = DialogCaptionValue;
            }
            
        }
        /// <summary>
        /// Load the Dialog from file.
        /// </summary>
        /// <param name="DialogCaption"></param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        public DialogContext(string DialogCaption)
        {
            FileStream myFile = new FileStream(ThereIsConstants.Path.Datas_Path +
                DialogCaption + ".bin", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable 618
            DialogContext myCon = null; // = (DialogContext)formatter.Deserialize(myFile);
#pragma warning restore 618
            context = myCon.Context;
            dialogCaption = myCon.DialogCaption;
            myFile.Close();
            myFile.Dispose();
        }
        public string Context
        {
            get
            {
                if (context != null)
                {
                    return context;
                }
                else
                {
                    return "";
                }
            }
        }
        public string DialogCaption
        {
            get
            {
                return dialogCaption;
            }
            set
            {
                if(value != null)
                {
                    dialogCaption = value;
                }
                else
                {
                    dialogCaption = DialogFirstName + DialogNameSeparater +
                        DateTime.Now.Year.ToString("0000") + DialogNameSeparater +
                        DateTime.Now.Month.ToString("00") + DialogNameSeparater +
                        DateTime.Now.Day.ToString("00") + DialogNameSeparater +
                        DateTime.Now.Hour.ToString("00") + DialogNameSeparater +
                        DateTime.Now.Minute.ToString("00") + DialogNameSeparater +
                        DateTime.Now.Second.ToString("00") + DialogNameSeparater;
                }
            }
        }

        /// <summary>
        /// Method for adding the dialog. <code> sample: </code>
        /// <see cref="Separater"/>(う) + Kotomine + <see cref="CharacterNameSeparater"/>(ｄ) + 1 +
        /// <see cref="LeftOrRightSeparater"/>(ｒｌ) + <see cref="Left"/>(L) OR <see cref="Right"/>(R) +
        /// <see cref="MainContextSeparater"/>(ｍ) + Hello, I am Kotomine! + <see cref="Separater"/>(う)
        /// </summary>
        /// <param name="characterName"></param>
        /// <param name="dialogText"></param>
        /// <param name="isRight">
        /// determine whether this character should be right side of the dialog or not.
        /// <c>default is false</c>
        /// </param>
        /// <param name="characterStatus"></param>
        public void AddDialog(string characterName, ushort characterStatus, string dialogText,
            bool isRight = false)
        {
            string mine = Separater + characterName + CharacterNameSeparater + characterStatus.ToString() +
                LeftOrRightSeparater + (isRight ? Right : Left) + MainContextSeparater + dialogText +
                Separater;
            context += mine;

        }
        public void AddDialog(GameCharacters character, ushort CharacterStatus, string DialogText,
            bool IsRight = false)
        {
            AddDialog(character.ToString(), CharacterStatus, DialogText,
            IsRight);
        }
        /// <summary>
        /// Use this method to update the Dialog
        /// and Serialize it in the file.
        /// </summary>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        public void UpdateDialog()
        {
            FileStream myFile = new FileStream(ThereIsConstants.Path.Datas_Path +
                dialogCaption + ".bin", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable 618
            //formatter.Serialize(myFile, this);
#pragma warning restore 618
            myFile.Close();
            myFile.Dispose();
        }
        public void EditDialog(int index, string CharacterName, ushort CharacterStatus, string DialogText,
            bool IsRight = false)
        {
            string[] myContexts = Context.Split(Separater.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            myContexts[index] =  CharacterName + CharacterNameSeparater + CharacterStatus.ToString() +
                LeftOrRightSeparater + (IsRight ? Right : Left) + MainContextSeparater + DialogText;
            context = "";
            foreach(string theString in myContexts)
            {
                context += theString + Separater;
            }
        }
        public void DeleteDialog(int index)
        {
            string[] myContexts = Context.Split(Separater.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            myContexts[index] = "";
            context = "";
            foreach (string theString in myContexts)
            {
                context += theString.Length > 0 ? (theString + Separater) : "";
            }
        }
        //-----------------------------------------------
        public static DialogContext FromFile(string DialogName)
        {
            FileStream myFile = new FileStream(ThereIsConstants.Path.Datas_Path +
                DialogName + ".bin", FileMode.OpenOrCreate, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable 618
            DialogContext myCon = null;// = (DialogContext)formatter.Deserialize(myFile);
#pragma warning restore 618
            myFile.Close();
            myFile.Dispose();
            return myCon;
        }
    }
}
