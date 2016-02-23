/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - heavily more security
 * - common standard naming
 * - disabled lighting for the sun in order to set it visible in all conditions
 * - ability to stop the rotation animation by flag
 * - more content and several bug fixes
*/

using ShadowEngine;
using Tao.OpenGl;

namespace SolarSystem
{
    sealed class Sun
    {
        int mList;
        float mRotacion;

        public void Create()
        {
            var quadratic = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(quadratic, Glu.GLU_SMOOTH);
            Glu.gluQuadricTexture(quadratic, Gl.GL_TRUE);

            mList = Gl.glGenLists(1);
            Gl.glNewList(mList, Gl.GL_COMPILE);
            Gl.glPushMatrix();
            Gl.glRotated(90, 1, 0, 0);
            Gl.glDisable(Gl.GL_LIGHTING);
            Glu.gluSphere(quadratic, 5, 32, 32);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glPopMatrix();
            Gl.glEndList();
        }

        public void Paint(bool stopAnim)
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, ContentManager.GetTextureByName("sun.jpg"));
            Gl.glPushMatrix();
            if (!stopAnim)
            {
                mRotacion += 0.05f;
            }
            Gl.glRotatef(mRotacion, 0, 1, 0);
            Gl.glCallList(mList);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }
    }
}
