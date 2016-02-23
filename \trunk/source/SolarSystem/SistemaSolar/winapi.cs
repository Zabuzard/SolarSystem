/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - more security
*/

using System.Runtime.InteropServices;

namespace SolarSystem
{
    internal struct Pointer
    {
        // ReSharper disable UnassignedField.Compiler
        internal int mX;
        internal int mY;
        // ReSharper restore UnassignedField.Compiler
    }

    internal static class Winapi
    {
        [DllImport("GDI32.dll")]
        public static extern void SwapBuffers(uint hdc);
        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern void GetCursorPos(ref Pointer point);
    }
}
