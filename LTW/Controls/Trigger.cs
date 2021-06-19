// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System;
using System.Timers;
using System.ComponentModel;
using WotoProvider.EventHandlers;
using LTW.Constants;

namespace LTW.Controls
{
    /// <summary>
    /// The Trigger for creating an AnimationFactory.
    /// </summary>
    [DesignerCategory("")]
    public class Trigger : Timer
    {
        //-------------------------------------------------
        #region Constant's Region
        /// <summary>
        /// ToString value used in ToString() method.
        /// </summary>
        public const string ToStringValue = "-- Trigger -- By wotoTeam Cor.";
		public const int ONCE_INTERVAL = 0b1010;
        #endregion
        //-------------------------------------------------
        #region Properties Region
        public string Name { get; set; }
        public uint Index { get; set; }
        /// <summary>
        /// When you want only one worker work on this Trigger,
        /// then set this to true in your Worker's method.
        /// </summary>
        public bool Running_Worker { get; set; }
        public bool SingleLineWorker { get; set; } = true;
        public bool IsOnceUsing { get; }
		public bool IsDisposed { get; private set; }
        /// <summary>
        /// The Tag of this Trigger.
        /// <!--
        /// ReSharper disable once UnusedAutoPropertyAccessor.Global
        /// -->
        /// </summary>
        public object Tag { get; set; }
        #endregion
        //-------------------------------------------------
        #region Events Region
        public event TickHandler<Trigger> Tick;
        public TickHandlerEventArgs<Trigger> EventArg { get; private set; }
        #endregion
        //-------------------------------------------------
        #region Constructor's Region
        public Trigger()
        {
            EventArg =
                new TickHandlerEventArgs<Trigger>(ThereIsConstants.AppSettings.WotoCreation, this);
            IsOnceUsing = false;
            Elapsed += OnTick;
			Disposed += OnDisposed;
        }
        /// <summary>
        /// crete a new once using trigger, if the once using value is true,
        /// and set the interval to 10 ms.
        /// </summary>
        /// <param name="isOnceUsing">
        /// if this trigger is once-using, you should pass true.
        /// </param>
        internal Trigger(bool isOnceUsing)
        {
            EventArg =
                new TickHandlerEventArgs<Trigger>(ThereIsConstants.AppSettings.WotoCreation, this);
            IsOnceUsing = isOnceUsing;
            Interval = ONCE_INTERVAL;
            Elapsed += OnTick;
        }
        ~Trigger()
        {
            if (Name != null)
            {
                Name        = null;
                Tick        = null;
                EventArg    = null;
            }
        }
        #endregion
        //-------------------------------------------------
        #region overrided Method's Region
        
		public override string ToString()
        {
            return ToStringValue;
        }
        #endregion
        //-------------------------------------------------
		#region ordinary Method's Region
		
		protected void OnTick(Object source, ElapsedEventArgs e)
        {
            // check if Running_Worker is true or not,
            // then check the single line worker value,
            // if both of them are set to true, you are not allowed
            // to trigger the event handler.
            if (Running_Worker && SingleLineWorker)
            {
                // it means the previous handler is not still running,
                // and this trigger is single line worker.
                // ReSharper disable once RedundantJumpStatement
                return;
            }
            else
            {
                // check if this trigger status enable or not
                if (!Enabled)
                {
                    // if this trigger is not enabled, return
                    return;
                }
                // WARNING: do not set the Running_Worker to true
                // in this method.
                // DO NOT do this here, but you should set it in the event
                // handler you defined...
                // Running_Worker = true;

                //check if this trigger is once using.
                if (IsOnceUsing)
                {
                    // it means this trigger is once using, so you should
                    // set the enables property to false.
                    Enabled = false;
                    Running_Worker = true;
                    SingleLineWorker = true;
                }
                // invoke the event handler and rise the events
                Tick?.Invoke(this, EventArg);
                // do not set the Running_Worker to false in this method.
                // Running_Worker = false;

                //check if this trigger is once using.
                if (IsOnceUsing)
                {
                    // it means this trigger is once using, so you should
                    // stop ticking and dispose this trigger.
                    Stop();
                    Dispose();
                }
            }

        }
		private void OnDisposed(object sender, EventArgs e)
		{
			if (!IsDisposed)
			{
				IsDisposed = true;
			}
		}
		
		#endregion
        //-------------------------------------------------
		#region Set Method's Region
		public void SetInterval(in int _interval)
		{
			if (!IsDisposed)
			{
				if (Interval != _interval)
				{
					Interval = _interval;
				}
			}
		}
		#endregion
        //-------------------------------------------------
    }
}
