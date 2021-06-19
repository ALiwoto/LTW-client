// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.IO;
using System.Net;
# if (OLD_LTW)
using System.Windows.Forms;
#endif
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using WotoProvider.EventHandlers;
using LTW.SandBox;
using LTW.Security;
using LTW.Controls;
using LTW.Constants;
using LTW.LoadingService;
using LTW.Controls.Workers;
using LTW.GameObjects.ServerObjects;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.Players
{
    partial class Me
    {
        [ComVisible(false)]
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private class SecuredMe
        {
            //---------------------------------------------
            #region Constant's Region
            private const string endFileName = "_これからもうーさとにまとを";
            private const string SME_MESSAGE = "SME_Player";
            private const string charSeparater = "ろろろ";
            /// <summary>
            /// use these character in the Token,
            /// not for separating them from each other...
            /// In other word, use it in <see cref="GenerateTokne()"/>
            /// </summary>
            private const string charSeparaterInToken = "と験する";
            private const int PASS_INDEX = 1;
            private const int TOKEN_INDEX = 2;
            private const int MAX_TOKEN_LENGTH = 7;
            #endregion
            //---------------------------------------------
            #region Properties Region
            public bool IsSecure { get; private set; }
            public bool IsSecuredProtocol { get; private set; }
            public bool IsOver { get; private set; }
            /// <summary>
            /// this is my Father.
            /// <code>
            /// *f: nice to meet you!
            /// </code>
            /// </summary>
            private Me Father { get; }
            #endregion
            //---------------------------------------------
            #region field's Region
            /// <summary>
            /// username value.
            /// this field is read-only.
            /// </summary>
            private readonly StrongString _username;

            /// <summary>
            /// password value.
            /// this field is read-only.
            /// </summary>
            private readonly StrongString _password;

            /// <summary>
            /// token value.
            /// this field is read-only.
            /// </summary>
            private readonly StrongString _token;
            #endregion
            //---------------------------------------------
            #region Constructor's Region
            /// <summary>
            /// Use this to Create the Security Datas.
            /// </summary>
            /// <param name="wantMeToCreate"></param>
            /// <param name="_u"></param>
            /// <param name="_p"></param>
            /// <param name="father"></param>
            internal SecuredMe(in bool wantMeToCreate, in StrongString _u, in StrongString _p, in Me father)
            {
                if (wantMeToCreate)
                {
                    Father = father;
                    _username = _u;
                    _password = _p;
                    if (_username != null)
                    {
                        IsSecure = true;
                    }
                    if (_password != null)
                    {
                        IsSecuredProtocol = true;
                    }
                    _token = GenerateToken();
                    var _w = new Worker(CreatingTheProfileSecurityWorker);
                    _w.Start();
                }
            }
            /// <summary>
            /// Use this before Link Start to the Profile.
            /// Notice: use this constructor just when the player already loged in to the
            /// profile and want to link start,
            /// if the player wants to login now, use <see cref="SecuredMe(string, string, bool, Me)"/>
            /// </summary>
            /// <param name="_u"></param>
            /// <param name="_t"></param>
            /// <param name="father"></param>
            internal SecuredMe(in StrongString _u, in StrongString _t, in Me father)
            {
                _token = _t;
                _username = _u;
                Father = father;
                if (_username != null)
                {
                    IsSecure = true;
                }
                if (_t != null)
                {
                    IsSecuredProtocol = true;
                }
                //-------------------------
                var _w = new Worker(PriLinkStartWorker);
                _w.Start();
            }
            /// <summary>
            /// Use this to login to the Profile which already exists ( = username_LogedIn should exists)
            /// </summary>
            /// <param name="_u"></param>
            /// <param name="_p"></param>
            /// <param name="WantMeToLogInNow"></param>
            /// <param name="father"></param>
            internal SecuredMe(in StrongString _u, in StrongString _p, in bool WantMeToLogInNow, in Me father)
            {
                Father      = father;
                _username   = _u;
                _password   = _p;
                if (_username != null)
                {
                    this.IsSecure = true;
                }
                if (_password != null)
                {
                    this.IsSecuredProtocol = true;
                }
                //----------------------------------
                var _w = new Worker(LoginToTheProfileWorker);
                _w.Start();
            }
            /// <summary>
            /// Use this one to generate the TokenObj
            /// </summary>
            /// <param name="GiveMeNewToken"></param>
            internal SecuredMe(ref StrongString _t)
            {
                _t = GenerateToken();
                if (_t != null)
                {
                    this.IsSecure = true;
                    this.IsSecuredProtocol = true;
                }
            }
            /// <summary>
            /// Use this one to simply log out from the account.
            /// </summary>
            /// <param name="_u"></param>
            internal SecuredMe(in StrongString _u, in Me father, in StrongString _t)
            {
                _token      = _t;
                _username   = _u;
                Father      = father;
                if (_username != null)
                {
                    this.IsSecure = true;
                }
                if (_token != null)
                {
                    this.IsSecuredProtocol = true;
                }
                //-------------------------
                var _w = new Worker(LogOutWorker);
                _w.Start();
            }
            #endregion
            //---------------------------------------------
            #region Worker Method's Region
            /// <summary>
            /// This function will create the Security data in 
            /// location <see cref="endFileName"/>
            /// at the server.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private async void CreatingTheProfileSecurityWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
            {
                //-----------------------------------------
                var _target = Father.UID.GetValue() + endFileName;
                var _s = ThereIsServer.ServersInfo.ServerManager.Get_SecuredMe_Server(Father.Socket);
                var _creation = new DataBaseCreation(SME_MESSAGE, QString.Parse(GetForServer()));
                var _result = await ThereIsServer.Actions.CreateData(_s, _target, _creation);
                if (_result.AlreadyExists)
                {
                    var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                    var _req = new DataBaseDeleteRequest(SME_MESSAGE, existing.Sha.GetStrong());
                    await ThereIsServer.Actions.DeleteData(_s, _target, _req);
                    _result = await ThereIsServer.Actions.CreateData(_s, _target, _creation);
                    if (_result.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                    {
                        this.IsOver = true;
                        this.Dispose();
                        NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                        return;
                    }
                }
                else
                {
                    if (_result.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                    {
                        this.IsOver = true;
                        this.Dispose();
                        NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                        return;
                    }
                }
                //-----------------------------------------
                Father.IsSecuredMeWorkingOver = true;
                if (!Directory.Exists(ThereIsConstants.Path.Profile_Folder_Path))
                {
                    Directory.CreateDirectory(ThereIsConstants.Path.Profile_Folder_Path);
                }
                ProfileInfo myInfo = new ProfileInfo(_username.GetValue(), _token.GetValue(),
                    ThereIsConstants.AppSettings.GlobalTiming.GetForServer().GetValue(),
                    Father.UID);
                ProfileInfo.UpdateInfo(myInfo, ThereIsConstants.Path.ProfileInfo_File_Path);
                AccountInfo myAccInfo = new AccountInfo(1,
                    ThereIsConstants.AppSettings.GlobalTiming.GetForServer().GetValue());
                AccountInfo.UpdateInfo(myAccInfo, ThereIsConstants.Path.AccountInfo_File_Path);
                GC.Collect();
                this.IsOver = true;
                this.Dispose();
            }
            /// <summary>
            /// LogIn Worker.
            /// </summary>
            /// <param name="sender">
            /// the trigger of the worker.
            /// </param>
            /// <param name="handler"></param>
            private async void LoginToTheProfileWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
            {
                //-----------------------------------------
                var _target = Father.UID.GetValue() + endFileName;
                var _s = ThereIsServer.ServersInfo.ServerManager.Get_SecuredMe_Server(Father.Socket);
                var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                //-----------------------------------------
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    this.IsOver = true;
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return;
                }
                StrongString myString = existing.Decode();
                //string t1 = _password;
                //string t2 = myString.Split(charSeparater)[PASS_INDEX];
                Father.HasLogin = (_password == myString.Split(charSeparater)[PASS_INDEX]);
                if (!Father.HasLogin)
                {
                    this.Father.IsSecuredMeWorkingOver = true;
                    this.IsOver = true;
                    return;
                }
                StrongString[] myStrings = myString.Split(charSeparater);
                StrongString myToken = GenerateToken();
                if (myStrings.Length >= MAX_TOKEN_LENGTH)
                {
                    StrongString myAnotherString;
                    bool ThisTokenAlreadyExists = false;
                    for (int i = 3; i < myStrings.Length; i++)
                    {
                        myAnotherString = myStrings[i];
                        myStrings[i - 1] = myAnotherString;
                        if (myToken == myAnotherString)
                        {
                            ThisTokenAlreadyExists = true;
                        }
                    }
                    if (!ThisTokenAlreadyExists)
                    {
                        myStrings[6] = myToken;
                        myString = charSeparater;
                        for (int i = 0; i < myStrings.Length; i++)
                        {
                            myString += charSeparater + myStrings[i];
                        }
                        var _req = new DataBaseUpdateRequest(SME_MESSAGE, QString.Parse(myString), existing.Sha);
                        await ThereIsServer.Actions.UpdateData(_s, _target, _req);
                    }
                    else
                    {
                        // impossible to reach this branch of the code...
                        this.IsOver = true;
                    }
                }
                else
                {
                    myString = charSeparater;
                    for (int i = 0; i < myStrings.Length; i++)
                    {
                        myString += myStrings[i] + charSeparater;
                    }
                    myString += myToken;
                    var _req = new DataBaseUpdateRequest(SME_MESSAGE, QString.Parse(myString), existing.Sha);
                    await ThereIsServer.Actions.UpdateData(_s, _target, _req);
                }
                this.Father.IsSecuredMeWorkingOver = true;

                if (!Directory.Exists(ThereIsConstants.Path.Profile_Folder_Path))
                {
                    Directory.CreateDirectory(ThereIsConstants.Path.Profile_Folder_Path);
                }
                ProfileInfo myInfo = new ProfileInfo(_username.GetValue(), myToken.GetValue(),
                    ThereIsConstants.AppSettings.GlobalTiming.GetForServer().GetValue(),
                    Father.UID);
                ProfileInfo.UpdateInfo(myInfo, ThereIsConstants.Path.ProfileInfo_File_Path);
                AccountInfo myAccInfo = new AccountInfo(1,
                    ThereIsConstants.AppSettings.GlobalTiming.GetForServer().GetValue());
                AccountInfo.UpdateInfo(myAccInfo, ThereIsConstants.Path.AccountInfo_File_Path);
                GC.Collect();
                this.IsOver = true;
                this.Dispose();
            }
            /// <summary>
            /// Log out Worker.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="handler"></param>
            private async void LogOutWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
            {
                //-----------------------------------------
                var _target = Father.UID.GetValue() + endFileName;
                var _q = ThereIsServer.ServersInfo.ServerManager.Get_SecuredMe_Server(Father.Socket);
                var _existing = await ThereIsServer.Actions.GetAllContentsByRef(_q, _target);
                //-----------------------------------------
                if (_existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    this.IsOver = true;
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return;
                }
                StrongString[] myStrings = _existing.Decode().Split(charSeparater);
                StrongString myString = StrongString.Empty;
                for (int i = 0; i < myStrings.Length; i++)
                {
                    if (_token == myStrings[i])
                    {
                        Father.HasLogOut = true;
                        continue;
                    }
                    else
                    {
                        myString += charSeparater + myStrings[i];
                        continue;
                    }
                }
                //-----------------------------------------
                var _req = new DataBaseUpdateRequest(SME_MESSAGE, QString.Parse(myString), _existing.Sha);
                await ThereIsServer.Actions.UpdateData(_q, _target, _req);
                //-----------------------------------------
                ThereIsConstants.Actions.ClearingPlayerProfile();
                this.Father.IsSecuredMeWorkingOver = true;
                this.IsOver = true;
                this.Dispose();
            }
            /// <summary>
            /// Pri Link Start Worker.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="handler"></param>
            private async void PriLinkStartWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
            {
                //-----------------------------------------
                if (Father.UID == null || Father.UID.IsNullUID)
                {
                    this.Father.IsSecuredMeWorkingOver = true;
                    this.IsOver = true;
                    return;
                }
                var _target = Father.UID.GetValue() + endFileName;
                var _s = ThereIsServer.ServersInfo.ServerManager.Get_SecuredMe_Server(Father.Socket);
                var existing = await ThereIsServer.Actions.GetAllContentsByRef(_s, _target);
                //-----------------------------------------
                if (existing.IsDeadCallBack || ThereIsServer.ServerSettings.HasConnectionClosed)
                {
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    this.Father.IsSecuredMeWorkingOver = true;
                    this.IsOver = true;
                    return;
                }
                StrongString[] myStrings = existing.Decode().Split(charSeparater);
                for (int i = TOKEN_INDEX; i < myStrings.Length; i++)
                {
                    if (_token == myStrings[i])
                    {
                        Father.HasLogin = true;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (Father.IsOnSecondStepOfLinkStart && Father.HasLogin)
                {
                    ProfileInfo myInfo =
                        ProfileInfo.FromFile(ThereIsConstants.Path.ProfileInfo_File_Path);
                    myInfo.LastLogin =
                        ThereIsConstants.AppSettings.GlobalTiming.GetForServer().GetValue();
                    ProfileInfo.UpdateInfo(myInfo, ThereIsConstants.Path.ProfileInfo_File_Path);
                }
                this.Father.IsSecuredMeWorkingOver = true;
                this.IsOver = true;
                this.Dispose();
            }
            #endregion
            //---------------------------------------------
            #region Get Method's Region
            /// <summary>
            /// consider this function will Generate a Token, based of this
            /// Client and System information.
            /// </summary>
            /// <returns></returns>
            private StrongString GenerateToken()
            {
                StrongString myString =
                    ThereIsConstants.AppSettings.GlobalTiming.GetForServer()    + charSeparaterInToken +
                    ThereIsConstants.Actions.OSの伊にファーエー所運()              + charSeparaterInToken +
                    Environment.MachineName                                     + charSeparaterInToken +
                    Environment.UserName                                        + charSeparaterInToken +
                    Dns.GetHostAddresses(Dns.GetHostName())[0].ToString()       + charSeparaterInToken;
                return myString;
            }
            private StrongString GetForServer()
            {
                StrongString myString =
                    _username + charSeparater +
                    _password + charSeparater +
                    _token + charSeparater;
                return myString;
            }
            #endregion
            //---------------------------------------------
            #region Dispose Region
            /// <summary>
            /// Dispose the SecuredMe.
            /// </summary>
            public void Dispose()
            {
                // NOTICE:
                // do NOT dispose the username and password of the 
                // player here, becuase you will need it after these processes.
                _password?.Dispose();
                if (!this.IsOver)
                {
                    this.IsOver = true;
                }
                GC.Collect();
            }
            #endregion
            //---------------------------------------------
        }
    }
}
