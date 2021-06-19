// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace WotoProvider.Interfaces
{
    
    public interface IDateProvider<T1, T2, T3>
        where T1 : struct
        where T2 : class
        where T3 : class
    {
        //-------------------------------------------------
        #region Properties Region
        bool IsTicking { get; }
        T2 TheTrigger { get; }
        int TickingInterval { get; }
        bool IsDisposed { get; }
        bool IsUnlimited { get; }
        #endregion
        //-------------------------------------------------
        #region Method's Region
        T1 GetDateTime();
        void StartTicking();
        void StopTicking();
        void StartTicking(int milliseconds);
        void ChangeTicking(int miliseconds);
        void Dispose();
        T3 GetString(bool onlyHour);
        T3 GetForServer();
        bool IsEqual(IDateProvider<T1, T2, T3> provider);
        bool IsAfterMe(IDateProvider<T1, T2, T3> provider);
        bool IsBeforeMe(IDateProvider<T1, T2, T3> provider);
        #endregion
        //-------------------------------------------------
    }
}
