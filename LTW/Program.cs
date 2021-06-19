using System;
using LTW.Client;
using LTW.Security;
using LTW.Constants;

namespace LTW
{
    public static class Program
    {
        /// <summary>
        /// Memory Map File Name.
        /// <!--
        /// ReSharper disable once InconsistentNaming
        /// -->
        /// </summary>
        public const string MMF_NAME = "LTW_MMF_LOADER_FILE_ASSIST";
        [STAThread]
        private static void Main()
        {
            Universe.SetUpUniverse();
            // check if the game is the single one process or not.
            if (ThereIsConstants.Actions.IsSingleOne())
            {
                ThereIsConstants.AppSettings.DECoder = new DECoder();
                // check if we can manage to create a single-one
                // provider peeker or not.
                // this method should try to create a memory space for us,
                // which will be visible to another instances.
                // ReSharper disable once InvertIf
                if (ThereIsConstants.Actions.CreateSingleOne())
                {
                    // it means the game is the single-instance,
                    // so you can now run the game.
                    // so, create a new instance of the game client.
                    // ReSharper disable once HeapView.ObjectAllocation.Evident
                    using var game = new GameClient
                    {
                        // set the verified property to true,
                        // to show the single-instance has been verified.
                        Verified = true,
                    };
                    // run the game client and
                    // start the main menu.
                    game.Run();
                }
            }
            else
            {
                try
                {
                    // it means this process is not the single-one,
                    // and another game process is already running, so
                    // send a request to another universe and tell them:
                    // make your ass up and active your planet :/
                    Universe.Universe_Request();
                }
                catch
                {
                    // do nothing here.
                    // ReSharper disable once RedundantJumpStatement
                    return;
                }
            }
        }
    }
}