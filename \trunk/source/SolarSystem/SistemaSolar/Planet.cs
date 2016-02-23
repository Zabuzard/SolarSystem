/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - heavily more security
 * - common standard naming
 * - ability to have different random rotation and orbit velocities
 * - removed hardcoded moon and added modular satellites
 * - added some satellites for many planet types (easy expandable for custom planets)
 * - abillity to stop rotation and orbit animation
 * - more content and several bug fixes
*/

using System;
using System.Collections.Generic;
using ShadowEngine;
using Tao.OpenGl;

namespace SolarSystem
{
    sealed class Planet
    {
        Position mPos;
        float mAngleRotation;
        float mAngleOrbit;
        readonly float mRadius;
        int mList;
        static readonly Random sRand = new Random();
        readonly float mVelocityOrbit;
        readonly float mVelocityRotation;
        readonly string mTexture;
        readonly List<Satellite> mMoons;


        public Planet(float radius, Planets type, Position posision,
            string texture)
        {
            mRadius = radius;
            var type1 = type;
            mPos = posision;
            mAngleOrbit = sRand.Next(360);
            mVelocityOrbit = (float)sRand.NextDouble() * 0.3f + 0.1f;
            mVelocityRotation = (float) sRand.NextDouble() * 0.6f + 0.3f;
            mTexture = texture;

            mMoons = new List<Satellite>();
            switch (type1)
            {
                case Planets.Earth:
                    mMoons.Add(new Satellite(0.25f, 2.5f, mPos, "moon.jpg", false));
                    break;
                case Planets.Mars:
                    mMoons.Add(new Satellite(0.1f, 1.5f, mPos, "phobos.jpg", false));
                    mMoons.Add(new Satellite(0.1f, 1.5f, mPos, "deimos.jpg", false));
                    break;
                case Planets.Jupiter:
                    mMoons.Add(new Satellite(0.25f, 2.5f, mPos, "io.jpg", false));
                    mMoons.Add(new Satellite(0.25f, 3f, mPos, "europa.jpg", false));
                    mMoons.Add(new Satellite(0.6f, 4f, mPos, "ganymed.jpg", false));
                    mMoons.Add(new Satellite(0.6f, 4.5f, mPos, "callisto.jpg", false));
                    break;
                case Planets.Saturn:
                    mMoons.Add(new Satellite(0.6f, 4f, mPos, "titan.jpg", false));
                    break;
                case Planets.Uranus:
                    mMoons.Add(new Satellite(0.2f, 2f, mPos, "ariel.jpg", true));
                    break;
                case Planets.Neptune:
                    mMoons.Add(new Satellite(0.25f, 2.5f, mPos, "triton.jpg", false));
                    break;
            }
        }

        public void Create()
        {
            var quadratic = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(quadratic, Glu.GLU_SMOOTH);
            Glu.gluQuadricTexture(quadratic, Gl.GL_TRUE);

            mList = Gl.glGenLists(1);
            Gl.glNewList(mList, Gl.GL_COMPILE);
            Gl.glPushMatrix();
            Gl.glRotated(270, 1, 0, 0);
            Glu.gluSphere(quadratic, mRadius, 32, 32);
            Gl.glPopMatrix();
            Gl.glEndList();
            foreach (var satellite in mMoons)
            {
                satellite.Create();
            }
        }

        private void DrawOrbit()
        {
            Gl.glBegin(Gl.GL_LINE_STRIP);

            for (var i = 0; i < 361; i++)
            {
                Gl.glVertex3f(mPos.mX * (float)Math.Sin(i * Math.PI / 180),
                    0, mPos.mX * (float)Math.Cos(i * Math.PI / 180));
            }
            Gl.glEnd();
        }

        public void Paint(bool stopAnim)
        {
            if (MainForm.ShowOrbit)
            {
                DrawOrbit();
            }
            foreach (var satellite in mMoons)
            {
                satellite.Paint(mPos, mAngleOrbit, stopAnim);
            }
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName(mTexture));
            Gl.glPushMatrix();
            if (!stopAnim)
            {
                mAngleOrbit += mVelocityOrbit;
                mAngleRotation += mVelocityRotation;
            }
            Gl.glRotatef(mAngleOrbit, 0, 1, 0);
            Gl.glTranslatef(-mPos.mX, -mPos.mY, -mPos.mZ);

            Gl.glRotatef(mAngleRotation, 0, 1, 0);

            Gl.glCallList(mList);

            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }
    }
}
