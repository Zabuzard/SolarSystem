/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * 
 * Used external libraries:
 * - Tao.OpenGl.dll (OpenGL for C#) - mainly for camera, scene and lighting
 * - glut32.dll (Open GL Utility Toolkit) - mainly for objects like spheres
 * - ShadowEngine.dll (by Vasily Tserekh) - only for content management
 *   like loading textures into a for OpenGL readable format.
 * Used internal libraries:
 * - System.Data, System.Deployment, System.Drawing, System.Windows.Forms,
 *   System.Xml (given Windows libraries) - mainly for GUI
 * - GDI32.dll (given Windows library) - to implement double buffering
 * - user32.dll (given Windows library) - to set and get mouse cursor position
*/

using System;
using System.Windows.Forms;

namespace SolarSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
