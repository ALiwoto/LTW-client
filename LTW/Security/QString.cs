// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using WotoProvider.Interfaces;
using LTW.Constants;

namespace LTW.Security
{
#pragma warning disable IDE0022
    /// <summary>
    /// this class's duty is to provide high-level secure and pure character
    /// working.
    /// </summary>
    public sealed class QString : IQStringProvider<StrongString, QString>, ISecurity
    {
        //-------------------------------------------------
        #region Constants Region
        public const string ToStringValue   = "<-- QString --> || BY: wotoTeam (C) -- " +
            "All Rights Reserved.";
        public const string SecureToString = ToStringValue + " ++ Licensed by Ali.w ++"; 
        private const char preferenceMain   = '-';
        private const string preferences    = "+=#~,;][%$!*-)(û⌐¬µ╖╕╣║";
        private const int preferenceBase    = 0x10;
        #endregion
        //-------------------------------------------------
        #region fields Region
        private readonly ISessionData sessionData;
        private readonly IStringProvider<StrongString> stringProvider;
        #endregion
        //-------------------------------------------------
        #region Properties Region

        #endregion
        //-------------------------------------------------
        #region Constructors Region
        public QString(StrongString strongString)
        {
            sessionData = strongString;
            stringProvider = strongString;
        }
        /// <summary>
        /// converting the value which is in coded format.
        /// </summary>
        /// <param name="codedString">
        /// the string value which is in coded format.
        /// </param>
        public QString(string codedString)
        {
            StrongString strongString   =
                FromString(codedString);
            sessionData                 = strongString;
            stringProvider              = strongString;
        }
        #endregion
        //-------------------------------------------------
        #region Ordianry Methods Region
        /// <summary>
        /// It will give the string in coded format.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string myString = BitConverter.ToString(sessionData.GetByteData());
            string theString = string.Empty;
            for (int i = 0; i < myString.Length; i++)
            {
                if (myString[i] == preferenceMain)
                {
                    theString += preferences[i % preferences.Length];
                }
                else
                {
                    theString += myString[i];
                }
            }
            return theString;
        }
        /// <summary>
        /// It will give you the string in the correct format(not coded).
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            return stringProvider.GetValue();
        }
        public StrongString GetStrong()
        {
            return GetValue().ToStrong();
        }
        public int IndexOf(string value)
        {
            return stringProvider.IndexOf(value);
        }
        public int IndexOf(char value)
        {
            return stringProvider.IndexOf(value);
        }
        public int IndexOf(StrongString value)
        {
            return stringProvider.IndexOf(value);
        }
        public int IndexOf(QString value)
        {
            return stringProvider.IndexOf(value.GetStrong());
        }
        private StrongString FromString(string codedString)
        {
            string[] hexes = codedString.Split(preferences.ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries);
            byte[] raw = new byte[hexes.Length];
            for (int i = 0; i < hexes.Length; i++)
            {
                raw[i] = Convert.ToByte(hexes[i], preferenceBase);

            }
            if (raw is null)
            {

            }
            return new StrongString(raw);
        }
        #endregion
        //-------------------------------------------------
        #region static Methods Region
        public static QString Parse(StrongString strongString, bool coded = false)
        {
            if (coded)
            {
                return new QString(strongString.GetValue());
            }
            return new QString(strongString);
        }
        #endregion
        //-------------------------------------------------
        #region overrided Methods Region
        public override string ToString() => ToStringValue;
        /// <summary>
        /// return the string which contains the information
        /// about this class.
        /// </summary>
        /// <param name="value">
        /// if true,in addition to get the general informations, 
        /// you will get the informations about license.
        /// </param>
        /// <returns>information</returns>
        public StrongString ToString(bool value)
        {
            if (value)
            {
                return SecureToString;
            }
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
        #region Operator's Region
        public static QString operator +(QString left, QString right)
        {
            return Parse(left.GetStrong() + right.GetStrong());
        }
        public static bool operator ==(QString left, QString right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (right is null)
                {
                    return false;
                }
                // don't return anything in this place,
                // you should check their value.
            }
            return left.GetStrong() == right.GetStrong();
        }
        public static bool operator !=(QString left, QString right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (right is null)
                {
                    return false;
                }
                // don't return anything in this place,
                // you should check their value.
            }
            return left.GetStrong() != right.GetStrong();
        }
        #endregion
        //-------------------------------------------------
    }
#pragma warning restore IDE0022
}
