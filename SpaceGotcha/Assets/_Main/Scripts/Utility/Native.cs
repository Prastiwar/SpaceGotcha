using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public struct NativeCustom<T> : IDisposable where T : struct
{
    [NativeDisableUnsafePtrRestriction]
    unsafe void* m_Buffer;
    Allocator m_AllocatorLabel;

    public unsafe T Value
    {
        get
        {
            return UnsafeUtility.ReadArrayElement<T>(m_Buffer, 0);
        }
        set
        {
            UnsafeUtility.WriteArrayElement(m_Buffer, 0, value);
        }
    }

    public NativeCustom(T defaultValue, Allocator allocator)
    {
        long totalSize = UnsafeUtility.SizeOf<T>();

        unsafe
        {
            m_Buffer = UnsafeUtility.Malloc(totalSize, UnsafeUtility.AlignOf<T>(), allocator);
            UnsafeUtility.MemClear(m_Buffer, totalSize);
        }
        m_AllocatorLabel = allocator;
        Value = defaultValue;
    }

    public void Dispose()
    {
        unsafe
        {
            UnsafeUtility.Free(m_Buffer, m_AllocatorLabel);
        }
    }
}
