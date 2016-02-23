/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - heavily more security
 * - common standard naming
 * - better working lighting
 * - support for more lights (ShadowEngine is pretty buggy here)
 * - more content and several bug fixes
*/

using System;
using System.Windows.Forms;
using ShadowEngine;
using ShadowEngine.OpenGL;
using Tao.OpenGl;

namespace SolarSystem
{
    internal sealed partial class MainForm : Form
    {
        readonly uint mHdc;
        readonly SolarSystem mSystem = new SolarSystem();
        static bool sShowOrbit = true;
        static bool sStopAnim;
        static Vector2 sFormPos;
        int mZoom;
        private readonly float[] mLight0Position = { 0.0f, 0.0f, 0.0f, 1.0f };
        internal static readonly float[] sLight1Position = { 1.0f, 0.0f, 0.0f, 1.0f };
        private bool mIsMenuOn;

        internal static Vector2 FormPos
        {
            get { return sFormPos; }
        }

        internal static bool ShowOrbit
        {
            get { return sShowOrbit; }
        }

        internal MainForm()
        {
            mIsMenuOn = false;
            InitializeComponent();
            mHdc = (uint)viewPortPanel.Handle;
            var error = "";
            OpenGLControl.OpenGLInit(ref mHdc, viewPortPanel.Width, viewPortPanel.Height, ref error);

            if (error != "")
            {
                MessageBox.Show(error);
            }

            Camera.InitCamera();

            float[] materialSpecular = { 0.1f, 0.1f, 0.1f, 1.0f };
            float[] materialDiffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] redLightDiffuse = { 0.9f, 0.1f, 0.1f, 1.0f };
            float[] materialShininess = { 50.0f };
            float[] materialAmbient = { 0.2f, 0.2f, 0.2f, 1.0f };
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, materialSpecular);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, materialDiffuse);
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, materialShininess);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, materialAmbient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, mLight0Position);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_AMBIENT, materialAmbient);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_DIFFUSE, redLightDiffuse);
            Gl.glLightfv(Gl.GL_LIGHT1, Gl.GL_POSITION, sLight1Position);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_LIGHT1);
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            ContentManager.SetTextureList("textures\\");
            ContentManager.LoadTextures();
            mSystem.CreateScene();
            Camera.CenterMouse();
            Gl.glClearColor(0, 0, 0, 1);
        }

        private void tmrPaint_Tick(object sender, EventArgs e)
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            if (!mIsMenuOn)
            {
                Camera.Update(mZoom);
            }

            mSystem.DrawScene(sStopAnim);

            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, mLight0Position);

            Winapi.SwapBuffers(mHdc);

            Gl.glFlush();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sFormPos = new Vector2(Left, Top);
        }

        private void pnlViewPort_MouseDown(object sender, MouseEventArgs e)
        {
            if (!mIsMenuOn && e.Button == MouseButtons.Left)
            {
                mZoom = 1;
            }
            else if (!mIsMenuOn)
            {
                mZoom = -1;
            }

        }

        private void pnlViewPort_MouseUp(object sender, MouseEventArgs e)
        {
            mZoom = 0;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if (!mIsMenuOn && e.KeyCode == Keys.O)
            {
                sShowOrbit = !sShowOrbit;
            }
            if (!mIsMenuOn && e.KeyCode == Keys.R)
            {
                sStopAnim = !sStopAnim;
            }
            if (!mIsMenuOn && e.KeyCode == Keys.M)
            {
                menuOn();
            }
        }
    }
}