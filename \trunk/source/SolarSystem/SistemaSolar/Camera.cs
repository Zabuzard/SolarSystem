/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - heavily more security
 * - common standard naming
*/

using System;
using Tao.OpenGl;

namespace SolarSystem
{
    internal static class Camera
    {
        const double Div1 = Math.PI / 180;
        static float sEyex, sEyey, sEyez;
        static float sCenterx, sCentery, sCenterz;
        private const float ForwardSpeed = 0.4f;
        static float sYaw, sPitch;
        private const float RotationSpeed = 0.25f;
        static double sI, sJ, sK;

        private const int CenterWidth = 668;
        private const int CenterHeight = 369;

        internal static void InitCamera()
        {
            sEyex = 0f;
            sEyey = 10f;
            sEyez = 15f;
            sCenterx = 0;
            sCentery = 2;
            sCenterz = 0;
            Look();
        }

        private static void Look()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluLookAt(sEyex, sEyey, sEyez, sCenterx, sCentery, sCenterz, 0, 1, 0);
        }

        private static float ConvertDegreeToRadian(double degAngle)
        {
            return (float)(degAngle * Div1);
        }

        private static void UpdateDirVector()
        {
            //Eye Z
            sK = Math.Cos(ConvertDegreeToRadian(sYaw));
            //Eye X
            sI = -Math.Sin(ConvertDegreeToRadian(sYaw));
            //Eye Y
            sJ = Math.Sin(ConvertDegreeToRadian(sPitch));

            //Camera view vector
            sCenterz = sEyez - (float)sK;
            sCenterx = sEyex - (float)sI;
            sCentery = sEyey - (float)sJ;
        }

        internal static void CenterMouse()
        {
            Winapi.SetCursorPos(MainForm.FormPos.X + CenterWidth, MainForm.FormPos.Y + CenterHeight);
        }

        internal static void Update(int pressedButton)
        {
            var position = new Pointer();
            Winapi.GetCursorPos(ref position);

            var difX = MainForm.FormPos.X + CenterWidth - position.mX;
            var difY = MainForm.FormPos.Y + CenterHeight - position.mY;

            if (position.mY < CenterHeight)
            {
                sPitch -= RotationSpeed * difY;
            }
            else
                if (position.mY > CenterHeight)
                {
                    sPitch += RotationSpeed * -difY;
                }
            if (position.mX < CenterWidth)
            {
                sYaw += RotationSpeed * -difX;
            }
            else
                if (position.mX > CenterWidth)
                {
                    sYaw -= RotationSpeed * difX;
                }
            UpdateDirVector();
            CenterMouse();


            switch (pressedButton)
            {
                case 1:
                    sEyex -= (float)sI * ForwardSpeed;
                    sEyey -= (float)sJ * ForwardSpeed;
                    sEyez -= (float)sK * ForwardSpeed;
                    break;
                case -1:
                    sEyex += (float)sI * ForwardSpeed;
                    sEyey += (float)sJ * ForwardSpeed;
                    sEyez += (float)sK * ForwardSpeed;
                    break;
            }

            Look();
        }
    }
}
