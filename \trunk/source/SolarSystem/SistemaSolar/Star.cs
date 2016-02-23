/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - heavily more security
 * - common standard naming
 * - disabled lighting for stars in order to set them visible in all conditions
 * - more content and several bug fixes
*/

using System;
using System.Collections.Generic;
using Tao.OpenGl;

namespace SolarSystem
{
    sealed class Star
    {
        readonly List<Position> mStars = new List<Position>();

        public void CreateStars(int amount)
        {
            var r = new Random();
            var count = 0;

            while (count != amount)
            {
                var pos = default(Position);
                pos.mX = (r.Next(110)) * (float)Math.Pow(-1, r.Next());
                pos.mZ = (r.Next(110)) * (float)Math.Pow(-1, r.Next());
                pos.mY = (r.Next(110)) * (float)Math.Pow(-1, r.Next());
                if (!(Math.Pow(Math.Pow(pos.mX, 2) + Math.Pow(pos.mY, 2) + Math.Pow(pos.mZ, 2), 1 / 3f) > 15))
                {
                    continue;
                }
                mStars.Add(pos);
                count++;
            }
        }

        public void Draw()
        {
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glBegin(Gl.GL_POINTS);
            Gl.glColor3f(1, 1, 1);
            Gl.glPointSize(3);
            foreach (var item in mStars)
            {
                Gl.glVertex3f(item.mX, item.mY, item.mZ);
            }
            Gl.glEnd();
            Gl.glEnable(Gl.GL_LIGHTING);
        }
    }
}