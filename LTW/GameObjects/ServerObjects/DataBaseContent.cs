// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Collections;
using System.Collections.Generic;
using Octokit;
using Octokit.Internal;
using LTW.Security;
using LTW.Constants;

namespace LTW.GameObjects.ServerObjects
{
    /// <summary>
    /// The individual DataBaseContent which is also IReadOnlyList,
    /// but don't forget that the Count is always 1.
    /// </summary>
    public sealed class DataBaseContent : IReadOnlyList<DataBaseContent>
    {
        //-------------------------------------------------
        #region Constants Region
        public const string WotoNameString =
            "Data Base Content, © By wotoTeam Cor. ";
        #endregion
        //-------------------------------------------------
        #region Properties Region
        /// <summary>
        /// Name of the content.
        /// </summary>
        public QString Name { get; }
        /// <summary>
        /// SHA of this content.
        /// </summary>
        public QString Sha { get; }
        /// <summary>
        /// Size of the content.
        /// </summary>
        public int Size { get; }
        /// <summary>
        /// The encoding of the content if this is a file. Typically "base64". Otherwise
        /// it's null.
        /// </summary>
        public QString TheEncoding { get; }
        /// <summary>
        /// Summary:
        /// The Base64 encoded content if this is a file. Otherwise it's null.
        /// </summary>
        [Parameter(Key = "content")]
        public QString EncodedContent { get; }
        /// <summary>
        /// The unencoded content. Only access this if the content is expected to be text
        /// and not binary content.
        /// </summary>
        public QString Content { get; }
        /// <summary>
        /// The Target of the Data.
        /// it'll be setted to null, so you should set it after that.
        /// </summary>
        public QString Target { get; private set; }
        /// <summary>
        /// Woto Generation Procedural
        /// </summary>
        public QString WotoGenerationProcedural { get; }
        public bool IsDeadCallBack { get; }
        public bool DoesNotExist { get; private set; }
        public int Count => 1;
        #endregion
        //-------------------------------------------------
        #region Constructors Region
        private DataBaseContent(RepositoryContent content, QString procedural)
        {
            Name                        = QString.Parse(WotoNameString.ToStrong(), false);
            Sha                         = QString.Parse(content.Sha.ToStrong(), false);
            Size                        = content.Size;
            TheEncoding                    = QString.Parse(content.Encoding.ToStrong(), false);
            EncodedContent              = QString.Parse(content.EncodedContent.ToStrong());
            Content                     = QString.Parse(content.Content.ToStrong(), true);
            Target                      = null;
            WotoGenerationProcedural    = procedural;
            IsDeadCallBack              = false;
        }
        /// <summary>
        /// create a dead call back of the database content.
        /// </summary>
        private DataBaseContent()
        {
            IsDeadCallBack = true;
        }
        public DataBaseContent this[int index]
        {
            get
            {
                return this;
            }
        }
        #endregion
        //-------------------------------------------------
        #region Ordinary Methods Region
        public IEnumerator<DataBaseContent> GetEnumerator()
        {
            return null;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }
        public StrongString Decode()
        {
            if (EncodedContent != null)
            {
                return ThereIsConstants.AppSettings.DECoder.GetDecodedValue(EncodedContent);
            }
            else
            {
                return Content.GetStrong();
            }
        }
        public void NotExists()
        {
            if (IsDeadCallBack)
            {
                DoesNotExist = true;
            }
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static DataBaseContent GetBaseContent(RepositoryContent content, string procedural)
        {
            return new DataBaseContent(content, QString.Parse(procedural.ToStrong()));
        }
        /// <summary>
        /// get the first place of the IReadOnly list,
        /// and create the content with that.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="procedural"></param>
        /// <returns></returns>
        public static DataBaseContent GetBaseContent(IReadOnlyList<RepositoryContent> content, string procedural)
        {
            return GetBaseContent(content[0], procedural);
        }
        /// <summary>
        /// this internal method gives you a dead call back of the
        /// database contents.
        /// </summary>
        /// <returns>
        /// dead call back.
        /// </returns>
        internal static DataBaseContent GetDeadCallBack()
        {
            return new DataBaseContent();
        }
        #endregion
        //-------------------------------------------------
    }
}
