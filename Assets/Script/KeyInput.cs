using System;
using System.Collections.Generic;

public static class KeyInput
{
    private static readonly int WH_KEYBOARD_LL = 13;

    public static IntPtr hookId = IntPtr.Zero;

    public static Action<Key> onKeyDown;
    public static Action<Key> onKeyUp;

    public static HashSet<Key> PressedKeys = new HashSet<Key>();

    public static bool isActive
    {
        get { return hookId != IntPtr.Zero; }
    }

    public static void EnableHook()
    {
        if (!isActive)
            SetHook();
    }

    private static void SetHook()
    {
        hookId = WindowsAPI.SetWindowsHookEx(WH_KEYBOARD_LL, LowLevelHookProc, IntPtr.Zero, 0);
    }

    public static void DisableHook()
    {
        if (isActive)
        {
            WindowsAPI.UnhookWindowsHookEx(hookId);
            hookId = IntPtr.Zero;
        }
    }

    private static int LowLevelHookProc(int code, IntPtr wParam, IntPtr lParam) // lParam is KBDLLHookStruct and wParam is a keyState
    {
        if (code >= 0)
        {
            var kb = KBDLLHookStruct.CreateStruct(lParam);
            var keyState = (KeyState)wParam;
            var key = (Key)kb.vkCode;

            if (keyState == KeyState.KeyDown || keyState == KeyState.SysKeyDown)
                OnKeyDown(key);
            else OnKeyUp(key);
        }
        return WindowsAPI.CallNextHookEx(hookId, code, wParam, lParam);
    }

    private static void OnKeyDown(Key k)
    {
        if (!PressedKeys.Contains(k))
        {
            PressedKeys.Add(k);
            if (onKeyDown != null)
                onKeyDown.Invoke(k);
        }
    }

    private static void OnKeyUp(Key k)
    {
        PressedKeys.Remove(k);
        if (onKeyUp != null)
            onKeyUp.Invoke(k);
    }
}