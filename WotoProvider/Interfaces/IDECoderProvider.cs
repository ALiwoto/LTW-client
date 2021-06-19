// WotoProvider (for LTW)
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.

using System.Text;

namespace WotoProvider.Interfaces
{
    public interface IDECoderProvider<T1, T2>
    {
        Encoding TheEncoderValue { get; }
        T1 GetDecodedValue(T2 value);
    }
}
