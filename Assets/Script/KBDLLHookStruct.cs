using System;
using System.Runtime.InteropServices;

public struct KBDLLHookStruct
{
    public uint vkCode;
    public uint scanCode;
    public KBDLLHookStructFlags flags;
    public uint time;
    public UIntPtr dwExtraInfo;

    public static KBDLLHookStruct CreateStruct(IntPtr param)
    {
        return (KBDLLHookStruct) Marshal.PtrToStructure(param, typeof(KBDLLHookStruct));
    }
}

[Flags]
public enum KBDLLHookStructFlags
{
    LLKHF_EXTENDED = 0x01,
    LLKHF_INJECTED = 0x10,
    LLKHF_ALTDOWN = 0x20,
    LLKHF_UP = 0x80
}
