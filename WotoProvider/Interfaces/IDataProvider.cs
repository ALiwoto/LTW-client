// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

namespace WotoProvider.Interfaces
{
    public interface IDataProvider<T1>
        where T1 : class
    {
        T1 GetForServer();
    }
}
