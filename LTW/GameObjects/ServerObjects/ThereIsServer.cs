// Last Testamnt of Wanderers 
// Copyright (C) 2019 - 2021 wotoTeam, TeaInside
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Threading.Tasks;
using Octokit;
using WotoProvider.Interfaces;
using WotoProvider.EventHandlers;
using LTW.Client;
using LTW.SandBox;
using LTW.Security;
using LTW.Controls;
using LTW.Constants;
using LTW.LoadingService;
using LTW.GameObjects.Players;
using LTW.GameObjects.Kingdoms;
using LTW.SandBox.ErrorSandBoxes;

namespace LTW.GameObjects.ServerObjects
{
    public static class ThereIsServer
    {
        public struct Actions
        {
            //-------------------------------------------------
            /// <summary>
            /// Creating the Prifle for the Player.
            /// check here:
            /// <see cref="Me.CreateMeWorker(object, EventArgs)"/>
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public  static void TimeWorkerWorksForCreating(object sender, EventArgs e)
            {
#if SERVER

                if (ThereIsConstants.Forming.TheMainForm.IsShowingSandBox)
                {
                    if (ThereIsConstants.Forming.TheMainForm.ShowingSandBox is CreateProfileSandBox mySandBox)
                    {
                        if (mySandBox.Enabled)
                        {
                            mySandBox.Enabled = false;
                        }
                        if (!mySandBox.LoadingSandBox.YuiWaitingPictureBox.IsDisposed)
                        {
                            if (!mySandBox.LoadingSandBox.YuiWaitingPictureBox.Focused)
                            {
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Focus();
                            }
                        }
                        if (mySandBox.IsCheckingForExisting &&
                            !mySandBox.IsCheckingForExistingEnded)
                        {
                            return;
                        }
                        else if (mySandBox.IsCheckingForExistingEnded)
                        {
                            mySandBox.IsCheckingForExisting = false;
                            mySandBox.IsCheckingForExistingEnded = false;
                            if (mySandBox.DoesPlayerExists)
                            {
                                ((Timer)sender).Enabled = false;
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                mySandBox.LoadingSandBox.Close(true);
                                mySandBox.Enabled = false;
                                mySandBox.IsShowingAnotherSandBox = true;
                                NoInternetConnectionSandBox errSandBox =
                                    new NoInternetConnectionSandBox(SandBoxMode.UserAlreadyExistMode,
                                    mySandBox.UnderForm);
                                ServerSettings.TimeWorker.Dispose();
                                ServerSettings.TimeWorker = null;
                                mySandBox.ShowingAnotherSandBox = errSandBox;
                                errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                errSandBox.Show();
                                mySandBox.DoesPlayerExists = false;
                            }
                            else
                            {
                                ServerSettings.MyProfile.CreatePlayerProfile();
                                mySandBox.IsWaitingForCreating = true;
                                mySandBox.DoesPlayerExists = false;
                            }
                            /*
                            ThereIsConstants.Forming.TheMainForm.Focus();
                            mySandBox.Focus(); */
                        }
                        else if (mySandBox.IsWaitingForCreating && !mySandBox.IsCreatingEnded)
                        {
                            //nothing
                            return;
                        }
                        else if (mySandBox.IsCreatingEnded)
                        {
                            if (ServerSettings.MyProfile.IsWaitingForSecuredWorking &&
                                    !ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                            {
                                //
                                return;
                            }
                            else if (ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                            {
                                ((Timer)sender).Enabled                             = false;
                                mySandBox.IsCreatingEnded                           = false;
                                ServerSettings.MyProfile.IsWaitingForSecuredWorking = false;
                                ServerSettings.MyProfile.IsSecuredMeWorkingOver     = false;
                                //mySandBox.LoadingSandBox.YuiWaitingPictureBox.Hide();
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                mySandBox.LoadingSandBox.Close(true);
                                mySandBox.Close(true);
                                ThereIsConstants.AppSettings.GameClient = new GameClient(true);
                                ThereIsConstants.AppSettings.GameClient.Show();
                                ThereIsConstants.AppSettings.GameClient.FirstTimeDesigning();
                                await Task.Run(() =>
                                {
                                    System.Threading.Thread.Sleep(3000);
                                });
                                ThereIsConstants.Forming.TheMainForm.ShowInTaskbar = false;
                                ThereIsConstants.Forming.TheMainForm.Hide();
                                //GlobalTimingWorker should stop displaying that bullshit Date and Time on Label of MainMenu.
                                ThereIsConstants.Forming.TheMainForm.MainMenuLoaded = false;
                                ThereIsConstants.Forming.TheMainForm.ReleaseAllResources();

                            }
                        }
                    }


                }
#endif
            }
            /// <summary>
            /// SigingIn, values: <see cref="CreateProfileSandBox.IsSignInEnded"/>
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public  static void TimeWorkerWorksForSigningIn(object sender, EventArgs e)
            {
#if SERVER

                if (ThereIsConstants.Forming.TheMainForm.IsShowingSandBox)
                {
                    if (ThereIsConstants.Forming.TheMainForm.ShowingSandBox is CreateProfileSandBox mySandBox)
                    {
                        if (mySandBox.Enabled)
                        {
                            mySandBox.Enabled = false;
                        }
                        if (!mySandBox.LoadingSandBox.YuiWaitingPictureBox.IsDisposed)
                        {
                            if (!mySandBox.LoadingSandBox.YuiWaitingPictureBox.Focused)
                            {
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Focus();
                            }
                        }
                        if (mySandBox.IsCheckingForExisting &&
                            !mySandBox.IsCheckingForExistingEnded)
                        {
                            return;
                        }
                        else if (mySandBox.IsCheckingForExistingEnded)
                        {
                            mySandBox.IsCheckingForExisting = false;
                            mySandBox.IsCheckingForExistingEnded = false;
                            if (mySandBox.DoesPlayerExists)
                            {
                                ServerSettings.MyProfile.Login();
                                mySandBox.IsWaitingForSignIn = true;
                                mySandBox.DoesPlayerExists = false;
                            }
                            else
                            {
                                ((Timer)sender).Enabled = false;
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                mySandBox.LoadingSandBox.Close(true);
                                mySandBox.Enabled = false;
                                mySandBox.IsShowingAnotherSandBox = true;
                                NoInternetConnectionSandBox errSandBox =
                                    new NoInternetConnectionSandBox(SandBoxMode.UserNameOrPasswordWrongMode,
                                    mySandBox.UnderForm);
                                ServerSettings.TimeWorker.Dispose();
                                ServerSettings.TimeWorker = null;
                                mySandBox.ShowingAnotherSandBox = errSandBox;
                                errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                errSandBox.Show();
                                mySandBox.DoesPlayerExists = false;
                            }
                            /*
                            ThereIsConstants.Forming.TheMainForm.Focus();
                            mySandBox.Focus(); */
                        }
                        else if (mySandBox.IsWaitingForSignIn && !mySandBox.IsSignInEnded)
                        {
                            if (ServerSettings.MyProfile.IsWaitingForSecuredWorking &&
                                    !ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                            {
                                //
                                return;
                            }
                            else if (ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                            {
                                if (ServerSettings.MyProfile.HasLogin)
                                {
                                    ServerSettings.MyProfile.IsWaitingForSecuredWorking =
                                        ServerSettings.MyProfile.IsSecuredMeWorkingOver =
                                        false;
                                    ServerSettings.MyProfile.Login(ServerSettings.MyProfile.HasLogin);
                                }
                                else
                                {
                                    ((Timer)sender).Enabled = false;
                                    mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                    mySandBox.LoadingSandBox.Close(true);
                                    mySandBox.Enabled = false;
                                    mySandBox.IsShowingAnotherSandBox = true;
                                    NoInternetConnectionSandBox errSandBox =
                                        new NoInternetConnectionSandBox(SandBoxMode.UserNameOrPasswordWrongMode,
                                        mySandBox.UnderForm);
                                    ServerSettings.TimeWorker.Dispose();
                                    ServerSettings.TimeWorker = null;
                                    mySandBox.ShowingAnotherSandBox = errSandBox;
                                    errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                    errSandBox.Show();
                                    mySandBox.DoesPlayerExists = false;
                                }
                            }

                            return;
                        }
                        else if (mySandBox.IsSignInEnded)
                        {

                            ((Timer)sender).Enabled                             = false;
                            mySandBox.IsSignInEnded                             = false;
                            ServerSettings.MyProfile.IsWaitingForSecuredWorking = false;
                            ServerSettings.MyProfile.IsSecuredMeWorkingOver     = false;
                            //mySandBox.LoadingSandBox.YuiWaitingPictureBox.Hide();
                            mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                            mySandBox.LoadingSandBox.Close(true);
                            mySandBox.Close(true);
                            ThereIsConstants.AppSettings.GameClient = new GameClient(false);
                            ThereIsConstants.AppSettings.GameClient.Show();
                            await Task.Run(() =>
                            {
                                System.Threading.Thread.Sleep(300);
                            });
                            ThereIsConstants.Forming.TheMainForm.ShowInTaskbar = false;
                            ThereIsConstants.Forming.TheMainForm.Hide();
                            //GlobalTimingWorker should stop displaying that bullshit Date and Time on Label of MainMenu.
                            ThereIsConstants.Forming.TheMainForm.MainMenuLoaded = false;
                            ThereIsConstants.Forming.TheMainForm.ReleaseAllResources();
                        }
                    }
                }
#endif
            }
            /// <summary>
            /// LinkStart, check here: <see cref="Me.Link_Start(bool)"/>
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public  static void TimeWorkerWorksForPriLinkStart(object sender, EventArgs e)
            {
#if SERVER

                if (ThereIsConstants.Forming.TheMainForm.IsShowingSandBox)
                {
                    if (ThereIsConstants.Forming.TheMainForm.ShowingSandBox is CreateProfileSandBox mySandBox)
                    {

                        if (ServerSettings.MyProfile.IsOnSecondStepOfLinkStart)
                        {
                            if (mySandBox.IsCheckingForExisting &&
                                !mySandBox.IsCheckingForExistingEnded)
                            {
                                return;
                            }
                            else if (mySandBox.IsCheckingForExistingEnded)
                            {
                                mySandBox.IsCheckingForExisting = false;
                                mySandBox.IsCheckingForExistingEnded = false;
                                if (mySandBox.DoesPlayerExists)
                                {
                                    ServerSettings.MyProfile.Link_Start();
                                    mySandBox.DoesPlayerExists = false;
                                }
                                else
                                {
                                    ((Timer)sender).Enabled = false;
                                    ServerSettings.TimeWorker.Dispose();
                                    ServerSettings.TimeWorker = null;
                                    mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                    //mySandBox.LoadingSandBox.Hide();
                                    mySandBox.LoadingSandBox.Close(true);
                                    mySandBox.Enabled = false;
                                    mySandBox.IsShowingAnotherSandBox = true;
                                    NoInternetConnectionSandBox errSandBox =
                                        new NoInternetConnectionSandBox(SandBoxMode.Cant_LoadYourProfileMode,
                                        mySandBox.UnderForm);
                                    ServerSettings.TimeWorker.Dispose();
                                    ServerSettings.TimeWorker = null;
                                    mySandBox.ShowingAnotherSandBox = errSandBox;
                                    errSandBox.FormClosed -= mySandBox.ErrorSandBox_FormClosed;
                                    errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                    errSandBox.Show();
                                    mySandBox.DoesPlayerExists = false;
                                }
                                /*
                                ThereIsConstants.Forming.TheMainForm.Focus();
                                mySandBox.Focus(); */
                            }
                            else if (ServerSettings.MyProfile.IsWaitingForSecuredWorking &&
                                    !ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                            {
                                ; //nothing.
                                return;
                            }
                            else if (ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                            {
                                if (ServerSettings.MyProfile.HasLogin)
                                {
                                    ServerSettings.MyProfile.IsSecuredMeWorkingOver     = false;
                                    ServerSettings.MyProfile.IsWaitingForSecuredWorking = false;
                                    mySandBox.IsWaitingForSignIn                        = true;
                                    mySandBox.IsSignInEnded                             = false;
                                    ServerSettings.MyProfile.Login(ServerSettings.MyProfile.HasLogin);
                                }
                                else
                                {
                                    ((Timer)sender).Enabled = false;
                                    ServerSettings.TimeWorker.Dispose();
                                    ServerSettings.TimeWorker = null;
                                    mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                    ServerSettings.MyProfile.IsWaitingForSecuredWorking = false;
                                    ServerSettings.MyProfile.IsSecuredMeWorkingOver = false;
                                    //mySandBox.LoadingSandBox.Hide();
                                    mySandBox.LoadingSandBox.Close(true);
                                    mySandBox.Enabled = false;
                                    mySandBox.IsShowingAnotherSandBox = true;
                                    NoInternetConnectionSandBox errSandBox =
                                        new NoInternetConnectionSandBox(SandBoxMode.Cant_LoadYourProfileMode,
                                        mySandBox.UnderForm);
                                    ServerSettings.TimeWorker.Dispose();
                                    ServerSettings.TimeWorker = null;
                                    mySandBox.ShowingAnotherSandBox = errSandBox;
                                    mySandBox.IsShowingAnotherSandBox = true;
                                    errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                    errSandBox.Show();
                                    mySandBox.DoesPlayerExists = false;
                                    ServerSettings.MyProfile = null;
                                }
                                /*
                                ((Timer)sender).Enabled = false;
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                //mySandBox.LoadingSandBox.Hide();
                                mySandBox.LoadingSandBox.Close();
                                ServerSettings.TimeWorker.Dispose();
                                ServerSettings.TimeWorker = null;
                                mySandBox.DoesPlayerExists = false;
                                mySandBox.CallMe(true);
                                */
                            }
                            else if(mySandBox.IsWaitingForSignIn && !mySandBox.IsSignInEnded)
                            {
                                return;
                            }
                            else if (mySandBox.IsSignInEnded)
                            {
                                ((Timer)sender).Enabled = false;
                                ServerSettings.TimeWorker.Dispose();
                                ServerSettings.TimeWorker = null;
                                mySandBox.IsSignInEnded = false;
                                ServerSettings.MyProfile.IsWaitingForSecuredWorking = false;
                                ServerSettings.MyProfile.IsSecuredMeWorkingOver = false;
                                //mySandBox.LoadingSandBox.YuiWaitingPictureBox.Hide();
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                //mySandBox.LoadingSandBox.Hide();
                                mySandBox.LoadingSandBox.AnimationFactory.Dispose();
                                mySandBox.LoadingSandBox.Close(true);
                                mySandBox.Close(true);
                                ThereIsConstants.AppSettings.GameClient = new GameClient(false);
                                ThereIsConstants.AppSettings.GameClient.Show();
                                //ThereIsConstants.AppSettings.GameClient.FirstTimeDesigning();
                                await Task.Delay(300);
                                ThereIsConstants.Forming.TheMainForm.ShowInTaskbar = false;
                                ThereIsConstants.Forming.TheMainForm.Hide();
                                //GlobalTimingWorker should stop displaying that bullshit Date and Time on Label of MainMenu.
                                ThereIsConstants.Forming.TheMainForm.MainMenuLoaded = false;
                                ThereIsConstants.Forming.TheMainForm.ReleaseAllResources();
                            }
                        }
                        else
                        {
                            if (mySandBox.IsCheckingForExisting &&
                            !mySandBox.IsCheckingForExistingEnded)
                            {
                                return;
                            }
                            else if (mySandBox.IsCheckingForExistingEnded)
                            {
                                mySandBox.IsCheckingForExisting = false;
                                mySandBox.IsCheckingForExistingEnded = false;
                                if (mySandBox.DoesPlayerExists)
                                {
                                    ServerSettings.MyProfile.Link_Start();
                                    mySandBox.DoesPlayerExists = false;
                                }
                                else
                                {
                                    ((Timer)sender).Enabled = false;
                                    mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                    //mySandBox.LoadingSandBox.Hide();
                                    mySandBox.LoadingSandBox.Close(true);
                                    mySandBox.Enabled = false;
                                    mySandBox.IsShowingAnotherSandBox = true;
                                    NoInternetConnectionSandBox errSandBox =
                                        new NoInternetConnectionSandBox(SandBoxMode.Cant_LoadYourProfileMode,
                                        mySandBox.UnderForm);
                                    ServerSettings.TimeWorker.Dispose();
                                    ServerSettings.TimeWorker = null;
                                    mySandBox.ShowingAnotherSandBox = errSandBox;
                                    errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                    errSandBox.Show();
                                    mySandBox.DoesPlayerExists = false;
                                }
                            }
                            else if (ServerSettings.MyProfile.IsWaitingForSecuredWorking &&
                                    !ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                            {
                                ; //nothing.
                                return;
                            }
                            else if (ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                            {
                                if (ServerSettings.MyProfile.HasLogin)
                                {
                                    ((Timer)sender).Enabled = false;
                                    ServerSettings.TimeWorker.Dispose();
                                    ServerSettings.TimeWorker = null;
                                    ServerSettings.MyProfile.IsWaitingForSecuredWorking = false;
                                    ServerSettings.MyProfile.IsSecuredMeWorkingOver = false;
                                    ServerSettings.MyProfile.HasLogin = false;
                                    mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                    //mySandBox.LoadingSandBox.Hide();
                                    mySandBox.LoadingSandBox.AnimationFactory.Dispose();
                                    mySandBox.LoadingSandBox.Close(true);
                                    mySandBox.LoadingSandBox = null;
                                    mySandBox.IsShowingAnotherSandBox = false;
                                    mySandBox.ShowingAnotherSandBox = null;
                                    mySandBox.DoesPlayerExists = false;
                                    mySandBox.CallMe(true);
                                }
                                else
                                {
                                    ((Timer)sender).Enabled = false;
                                    ServerSettings.TimeWorker.Dispose();
                                    ServerSettings.TimeWorker = null;
                                    ServerSettings.MyProfile.IsWaitingForSecuredWorking = false;
                                    ServerSettings.MyProfile.IsSecuredMeWorkingOver = false;
                                    ServerSettings.MyProfile = null;
                                    mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                    mySandBox.LoadingSandBox.Close(true);
                                    mySandBox.Enabled = false;
                                    mySandBox.IsShowingAnotherSandBox = true;
                                    NoInternetConnectionSandBox errSandBox =
                                        new NoInternetConnectionSandBox(SandBoxMode.Cant_LoadYourProfileMode,
                                        mySandBox.UnderForm);
                                    mySandBox.ShowingAnotherSandBox = errSandBox;
                                    errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                    errSandBox.Show();
                                    mySandBox.DoesPlayerExists = false;
                                    GC.Collect();
                                }
                            }
                        }
                        
                    }
                }
#endif
            }
            /// <summary>
            /// Loging out Timer Worker, setted in <see cref="Me.LogOut(bool, bool)"/>
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public static void TimeWorkerWorksForLogingOut(object sender, EventArgs e)
            {
#if SERVER

                if (ThereIsConstants.Forming.TheMainForm.IsShowingSandBox)
                {
                    if (ThereIsConstants.Forming.TheMainForm.ShowingSandBox is CreateProfileSandBox mySandBox)
                    {

                        if (mySandBox.IsCheckingForExisting &&
                            !mySandBox.IsCheckingForExistingEnded)
                        {
                            return;
                        }
                        else if (mySandBox.IsCheckingForExistingEnded)
                        {
                            mySandBox.IsCheckingForExisting = false;
                            mySandBox.IsCheckingForExistingEnded = false;
                            if (mySandBox.DoesPlayerExists)
                            {
                                ServerSettings.MyProfile.LogOut(true);
                                mySandBox.DoesPlayerExists = false;
                            }
                            else
                            {
                                ((Timer)sender).Enabled = false;
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                mySandBox.LoadingSandBox.Close(true);
                                mySandBox.Enabled = false;
                                mySandBox.IsShowingAnotherSandBox = true;
                                NoInternetConnectionSandBox errSandBox =
                                    new NoInternetConnectionSandBox(SandBoxMode.Cant_LoadYourProfileMode,
                                    mySandBox.UnderForm);
                                ServerSettings.TimeWorker.Dispose();
                                ServerSettings.TimeWorker = null;
                                mySandBox.ShowingAnotherSandBox = errSandBox;
                                errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                errSandBox.Show();
                                mySandBox.DoesPlayerExists = false;
                            }
                            /*
                            ThereIsConstants.Forming.TheMainForm.Focus();
                            mySandBox.Focus(); */
                        }
                        else if (ServerSettings.MyProfile.IsWaitingForSecuredWorking &&
                                !ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                        {
                            ; //nothing.
                            return;
                        }
                        else if (ServerSettings.MyProfile.IsSecuredMeWorkingOver)
                        {
                            if (ServerSettings.MyProfile.HasLogOut)
                            {
                                ((Timer)sender).Enabled = false;
                                ServerSettings.TimeWorker.Dispose();
                                ServerSettings.TimeWorker = null;
                                ServerSettings.MyProfile.IsSecuredMeWorkingOver     = false;
                                ServerSettings.MyProfile.IsWaitingForSecuredWorking = false;
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                //mySandBox.LoadingSandBox.Hide();
                                mySandBox.LoadingSandBox.Close(true);
                                mySandBox.Enabled                   = false;
                                mySandBox.IsShowingAnotherSandBox   = true;
                                mySandBox.HasLoggedOut              = true;
                                NoInternetConnectionSandBox errSandBox =
                                    new NoInternetConnectionSandBox(SandBoxMode.LoggedOutSuccessfullyMode,
                                    mySandBox.UnderForm);
                                mySandBox.ShowingAnotherSandBox = errSandBox;

                                errSandBox.FormClosed -= mySandBox.ErrorSandBox_FormClosed;
                                errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;

                                errSandBox.Show();
                                mySandBox.DoesPlayerExists = false;
                            }
                            else
                            {
                                ((Timer)sender).Enabled = false;
                                mySandBox.LoadingSandBox.YuiWaitingPictureBox.Image.Dispose();
                                mySandBox.LoadingSandBox.Close(true);
                                mySandBox.Enabled = false;
                                mySandBox.IsShowingAnotherSandBox = true;
                                NoInternetConnectionSandBox errSandBox =
                                    new NoInternetConnectionSandBox(SandBoxMode.Cant_LoadYourProfileMode,
                                    mySandBox.UnderForm);
                                ServerSettings.TimeWorker.Dispose();
                                ServerSettings.TimeWorker = null;
                                mySandBox.ShowingAnotherSandBox = errSandBox;
                                errSandBox.FormClosed += mySandBox.ErrorSandBox_FormClosed;
                                errSandBox.Show();
                                mySandBox.DoesPlayerExists = false;
                            }
                            /*
                             * 
                            */
                        }

                    }
                }
#endif
            }
            /// <summary>
            /// Worker for <see cref="GameClient.AnimationFactory"/>.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public static void TimeWorkerWorksForGameClientLoading(object sender, EventArgs e)
            {
#if SERVER

                if (ThereIsConstants.Forming.GameClient.IsLoading && 
                    !ThereIsConstants.Forming.GameClient.IsLoadingEnded)
                {
                    if(ThereIsConstants.Forming.GameClient.MessageLabel1.Text.IndexOf("...") != -1)
                    {
                        ThereIsConstants.Forming.GameClient.MessageLabel1.SetLabelText();
                    }
                    else
                    {
                        ThereIsConstants.Forming.GameClient.MessageLabel1.Text += ".";
                    }
                }
                else if (ThereIsConstants.Forming.GameClient.IsLoadingEnded)
                {
                    ((Timer)sender).Enabled = false;
                    ((Timer)sender).Dispose();
                    if(GameObjects.MyProfile.PlayerKingdom != LTW_Kingdoms.NotSet)
                    {
                        GameObjects.MyProfile.KingdomInfo =
                        await KingdomInfo.GetKingdomInfo((uint)GameObjects.MyProfile.PlayerKingdom);
                    }
                    ThereIsConstants.Forming.GameClient.AnimationFactory = null;
                    ThereIsConstants.Forming.GameClient.GameClientHandler();
                    GC.Collect();
                }
#endif
            }
            //-------------------------------------------------
            /// <summary>
            /// Checking the server status.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public static async void CheckingServerWorker(Trigger sender, TickHandlerEventArgs<Trigger> handler)
            {
                if (ServerSettings.IsWaitingForServerChecking || handler == null)
                {
                    return;
                }
                else
                {
                    ServerSettings.IsWaitingForServerChecking = true;
                }
                //-----------------------------------------
                var _s = ServersInfo.ServerManager.Get_Main_Server();
                var existing = await GetAllContentsByRef(_s, ServerSettings.ServerChecking_File_Name);
                //-----------------------------------------
                if (ServerSettings.HasConnectionClosed)
                {
                    sender.Enabled = false;
                    ServerSettings.ServerChecker.Dispose();
                    ServerSettings.ServerChecker = null;
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return; // don't set ServerSettings.IsWaitingForServerChecking = false;
                }
                if (existing.IsDeadCallBack)
                {
                    sender.Enabled              = false;
                    ServerSettings.ServerChecker.Dispose();
                    ServerSettings.ServerChecker = null;
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return; // don't set ServerSettings.IsWaitingForServerChecking = false;
                }
                if(existing.Decode().IndexOf(ServerSettings.Y_S) == -1)
                {
                    sender.Enabled = false;
                    ServerSettings.ServerChecker.Dispose();
                    ServerSettings.ServerChecker = null;
                    NoInternetConnectionSandBox.PrepareConnectionClosedSandBox();
                    return;
                }
                ServerSettings.IsWaitingForServerChecking = false;
            }
            //-------------------------------------------------
            /// <summary>
            /// create a new space for data in the database, using an specified 
            /// path in the database.
            /// </summary>
            /// <param name="serverInfo">
            /// the server info which is for accessing the database.
            /// </param>
            /// <param name="path">
            /// the path of the specific space in the database.
            /// </param>
            /// <param name="request">
            /// the request.
            /// </param>
            /// <returns></returns>
            public static async Task<DataBaseDataChangedInfo> CreateData(IServerProvider<QString, DataBaseClient> serverInfo, 
                StrongString path, DataBaseCreation request)
            {
                RepositoryContentChangeSet myChangeSet = null;
                uint trys = 0;
                for (; ; )
                {
                    try
                    {
                        myChangeSet = await serverInfo.GetClient().
                            Repository.Content.CreateFile(serverInfo.Owner.GetValue(),
                            serverInfo.Repo.GetValue(), path.GetValue(),
                            request);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (ex != null)
                        {
                            if (ex.Message.IndexOf(ServerSettings.AE_ERR_MSF) != -1)
                            {
                                return DataBaseDataChangedInfo.GetInfo(true);
                            }
                        }
                        if (trys >= ServerSettings.MAXIMUM_TRY)
                        {
                            ServerSettings.HasConnectionClosed = true;
                            break;
                        }
                        else
                        {
                            trys++;
                            continue;
                        }

                    }
                }
                return DataBaseDataChangedInfo.GetInfo(myChangeSet);
            }
            
            public static async Task<DataBaseContent> GetAllContentsByRef(IServerProvider<QString, DataBaseClient> serverInfo, StrongString path)
            {
                DataBaseContent existings = DataBaseContent.GetDeadCallBack();
                uint trys = 0;
                for (; ; )
                {
                    try
                    {
                        string owner = serverInfo.Owner.GetValue();
                        string repo = serverInfo.Repo.GetValue();
                        string mypath = path.GetValue();
                        string thevranch = serverInfo.Branch.GetValue();
                        GitHubClient huhu = serverInfo.GetClient();
                        string haha = ThereIsConstants.AppSettings.WotoCreation.Procedural;
                        existings = DataBaseContent.GetBaseContent(
                            await serverInfo.GetClient().
                         Repository.Content.GetAllContentsByRef(serverInfo.Owner.GetValue(),
                         serverInfo.Repo.GetValue(), path.GetValue(), serverInfo.Branch.GetValue()), 
                            ThereIsConstants.AppSettings.WotoCreation.Procedural);

                        break;
                    }
                    catch (Exception exp)
                    {
                        if (exp.Message.IndexOf(ServerSettings.NF_ERR_MSG) != -1)
                        {
                            existings.NotExists();
                            break;
                        }
                        if (exp.Message.IndexOf(ServerSettings.CC_ERR_MSG) != 0)
                        {
                            if (trys >= ServerSettings.MAXIMUM_TRY)
                            {
                                ServerSettings.HasConnectionClosed = true;
                                break;
                            }
                            else
                            {
                                trys += 10;
                                continue;
                            }
                        }
                        if (trys >= ServerSettings.MAXIMUM_TRY)
                        {
                            ServerSettings.HasConnectionClosed = true;
                            break;
                        }
                        else
                        {
                            trys++;
                            continue;
                        }

                    }
                }
                return existings;
            }
            
            public static async Task<DataBaseDataChangedInfo> UpdateData(IServerProvider<QString, DataBaseClient> serverInfo,
                StrongString path, DataBaseUpdateRequest request)
            {

                RepositoryContentChangeSet myChangeSet = null;
                uint trys = 0;
                for (; ; )
                {
                    try
                    {
                        myChangeSet = await serverInfo.GetClient().Repository.Content.
                          UpdateFile(serverInfo.Owner.GetValue(),
                            serverInfo.Repo.GetValue(), path.GetValue(),
                            request);

                        break;
                    }
                    catch
                    {
                        if (trys >= ServerSettings.MAXIMUM_TRY)
                        {
                            ServerSettings.HasConnectionClosed = true;
                            break;
                        }
                        else
                        {
                            trys++;
                            continue;
                        }

                    }
                }
                return DataBaseDataChangedInfo.GetInfo(myChangeSet);
            }
            public static async Task<DataBaseDataChangedInfo> UpdateDataOnce(IServerProvider<QString, DataBaseClient> serverInfo,
                StrongString path, DataBaseUpdateRequest request)
            {
                RepositoryContentChangeSet myChangeSet = null;
                uint trys = 0;
                for (; ; )
                {
                    try
                    {
                        myChangeSet = await serverInfo.GetClient().Repository.Content.
                          UpdateFile(serverInfo.Owner.GetValue(),
                            serverInfo.Repo.GetValue(), path.GetValue(),
                            request);

                        break;
                    }
                    catch(ApiException ex)
                    {
                        if (ex is null)
                        {
                            ; //
                        }
                        break;
                    }
                    catch
                    {
                        if (trys >= ServerSettings.MAXIMUM_TRY)
                        {
                            ServerSettings.HasConnectionClosed = true;
                            break;
                        }
                        else
                        {
                            trys++;
                            continue;
                        }

                    }
                }
                return DataBaseDataChangedInfo.GetInfo(myChangeSet);
            }
            public static async Task<bool> DeleteData(IServerProvider<QString, DataBaseClient> serverInfo, StrongString path,
                DataBaseDeleteRequest request)
            {
                uint trys = 0;
                bool mayushii = false;
                for (; ; )
                {
                    try
                    {
                         await serverInfo.GetClient().Repository.Content.
                          DeleteFile(serverInfo.Owner.GetValue(),
                            serverInfo.Repo.GetValue(), path.GetValue(),
                            request);
                        mayushii = true;
                        break;
                    }
                    catch(NotFoundException)
                    {
                        //mayushii = false;
                        break;
                    }
                    catch(ApiException myErr)
                    {
                        if(myErr.Message.IndexOf(request.Sha) != -1)
                        {
                            mayushii = true;
                            break;
                        }
                        else
                        {
                            if (trys >= ServerSettings.MAXIMUM_TRY)
                            {
                                ServerSettings.HasConnectionClosed = true;
                                break;
                            }
                            else
                            {
                                trys++;
                                continue;
                            }
                        }
                    }
                    catch(Exception err)
                    {
                        if (err.Message.IndexOf(request.Sha) != -1)
                        {
                            mayushii = true;
                            break;
                        }
                        else
                        {
                            if (trys >= ServerSettings.MAXIMUM_TRY)
                            {
                                ServerSettings.HasConnectionClosed = true;
                                break;
                            }
                            else
                            {
                                trys++;
                                continue;
                            }
                        }
                    }
                }
                return mayushii;
            }
            public static async Task<bool> Exists(IServerProvider<QString, DataBaseClient> serverInfo, StrongString path)
            {
                return await DeleteData(serverInfo, path,
                    new DataBaseDeleteRequest(ServerManager.DEFAULT_MESSAGE, ServerManager.NO_SHA));
            }
        }
        public struct ServerSettings
        {
            //---------------------------------------------
            public static Trigger TimeWorker { get; set; }
            public static Trigger ServerChecker { get; set; }
            public static Me MyProfile { get; set; }
            //---------------------------------------------
            public static StrongString TokenObj { get; set; }
            //---------------------------------------------
            internal const string Y_S = "Y";
            internal const string N_S = "N";
            internal const string ServerChecking_File_Name = "Status.LTW";
            /// <summary>
            /// network connection Error for sending a request to the server.
            /// </summary>
            internal const string CC_ERR_MSG = "An error occurred while sending the request.";
            /// <summary>
            /// Not found Error for deleting.
            /// </summary>
            internal const string NF_ERR_MSG = "was not found";
            /// <summary>
            /// Already exists Error for creating new data in the server.
            /// </summary>
            internal const string AE_ERR_MSF = "wasn't supplied";
            internal const uint MAXIMUM_TRY = 50;
            //---------------------------------------------
            public static bool HasConnectionClosed { get; internal set; }
            public static bool IsWaitingForServerChecking { get; set; }
            //---------------------------------------------
            public static void CloseConnection()
            {
                HasConnectionClosed = true;
            }
            //---------------------------------------------
        }
        public struct GameObjects
        {
            //--------------------------------------
            //--------------------------------------
            public static Me MyProfile
            {
                get
                {
                    return ServerSettings.MyProfile;
                }
                set
                {
                    ServerSettings.MyProfile = value;
                }
            }
            public static StrongString TokenObj
            {
                get
                {
                    return ServerSettings.TokenObj;
                }
                set
                {
                    ServerSettings.TokenObj = value;
                }
            }
            //--------------------------------------
        }
        public struct ServersInfo
        {
            public static IServerManager ServerManager { get; set; }
            /// <summary>
            /// username of user + _LogedIn
            /// </summary>
            public const string EndCheckingFileName = "_LogedIn";
			
        }
    }
}
