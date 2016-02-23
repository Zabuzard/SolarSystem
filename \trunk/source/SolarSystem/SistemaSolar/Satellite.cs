/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - heavily more security
 * - common standard naming
 * - ability to have different random rotation and orbit velocities
 * - made class very more modular and abstract
 * - abillity to add a red light source to the satellite
 * - abillity to stop rotation and orbit animation
 * - drawOrbit function which draws satellites orbit relative to its planet
 * - more content and several bug fixes
*/

using System;
using ShadowEngine;
using Tao.OpenGl;

namespace SolarSystem
{
    sealed class Satellite
    {
        readonly Position mPlanetPos;
        readonly Position mSattelitePos;
        float mAngleRotation;
        float mAngleOrbit;
        readonly float mRadius;
        int mList;
        readonly float mVelocityOrbit;
        readonly float mVelocityRotation;
        readonly string mTexture;
        private readonly float mDistance;
        static readonly Random sRand = new Random();
        private readonly bool mRedLight;

        public Satellite(float radius, float distance,
            Position position, string texture, bool redLight)
        {
            mRadius = radius;
            mDistance = distance;
            mPlanetPos = position;
            mSattelitePos = mPlanetPos;
            mSattelitePos.mX += mDistance;
            mAngleOrbit = sRand.Next(360);
            mVelocityOrbit = (float)sRand.NextDouble() * 0.6f + 0.1f;
            mVelocityRotation = (float)sRand.NextDouble() * 0.6f + 0.3f;
            mTexture = texture;
            mRedLight = redLight;
        }

        private void DrawOrbit()
        {
            Gl.glBegin(Gl.GL_LINE_STRIP);

            for (var i = 0; i < 361; i++)
            {
                var flatDist = (mPlanetPos.mX - mSattelitePos.mX);
                Gl.glVertex3f(flatDist * (float)Math.Sin(i * Math.PI / 180),
                    0, flatDist * (float)Math.Cos(i * Math.PI / 180));
            }
            Gl.glEnd();
        }

        public void Create()
        {
            var quadratic = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(quadratic, Glu.GLU_SMOOTH);
            Glu.gluQuadricTexture(quadratic, Gl.GL_TRUE);

            mList = Gl.glGenLists(1);
            Gl.glNewList(mList, Gl.GL_COMPILE);
            Gl.glPushMatrix();
            Gl.glRotated(90, 1, 0, 0);
            if (mRedLight)
            {
                Gl.glDisable(Gl.GL_LIGHTING);
            }
            Glu.gluSphere(quadratic, mRadius, 32, 32);
            if (mRedLight)
            {
                Gl.glEnable(Gl.GL_LIGHTING);
            }
            Gl.glPopMatrix();
            Gl.glEndList();
        }

        public void Paint(Position pos, float angleOrbit, bool stopAnim)
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName(mTexture));

            angleOrbit += mVelocityOrbit;
            if (!stopAnim)
            {
                mAngleOrbit += mVelocityOrbit;
                mAngleRotation += mVelocityRotation;
            }
            Gl.glPushMatrix();
            Gl.glRotatef(angleOrbit, 0, 1, 0);
            Gl.glTranslatef(-pos.mX, -pos.mY, -pos.mZ);
            if (MainForm.ShowOrbit)
            {
                DrawOrbit();
            }
            Gl.glRotatef(mAngleOrbit, 0, 1, 0);
            Gl.glTranslatef(mDistance, 0, 0);
            Gl.glRotatef(mAngleRotation, 0, 1, 0);
            Gl.glCallList(mList);
            if (mRedLight)
            {
                Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, MainForm.sLight1Position);
            }
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }
    }
}
