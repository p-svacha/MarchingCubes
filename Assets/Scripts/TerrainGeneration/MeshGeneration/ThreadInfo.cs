using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ThreadInfo<T>
{
    public readonly Action<T> Callback;
    public readonly T Parameter;

    public ThreadInfo(Action<T> callback, T parameter)
    {
        Callback = callback;
        Parameter = parameter;
    }
}