/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - heavily more security
 * - common standard naming
 * - interactable menu to create new planets
 * - implemented some focus control
 * - more content and several bug fixes
*/

using System;
using System.Drawing;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace SolarSystem
{
    internal sealed partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel viewPortPanel;
        private System.Windows.Forms.Timer tmrPaint;
        private System.Windows.Forms.Panel legendPanel;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Label legendMenuLabel;
        private System.Windows.Forms.Label legendAnimLabel;
        private System.Windows.Forms.Label legendOrbitLabel;
        private System.Windows.Forms.Label legendExitLabel;
        private System.Windows.Forms.Label menuLabelTitle;
        private System.Windows.Forms.Label menuLabelRadius;
        private System.Windows.Forms.NumericUpDown menuNumUpDownRadius;
        private System.Windows.Forms.Label menuLabelDistance;
        private System.Windows.Forms.NumericUpDown menuNumUpDownDistance;
        private System.Windows.Forms.Label menuLabelTexture;
        private System.Windows.Forms.TextBox menuTextBoxTexture;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCreate;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Camera.CenterMouse();
            menuOff();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            float radius = (float)menuNumUpDownRadius.Value;
            int distance = (int)menuNumUpDownDistance.Value;
            String texture = menuTextBoxTexture.Text;
            mSystem.AddPlanet(radius, distance, texture);
        }

        private void menuOn()
        {
            mIsMenuOn = true;
            menuPanel.Visible = true;
            btnClose.Visible = true;
            btnCreate.Visible = true;
            menuLabelTitle.Visible = true;
            menuLabelRadius.Visible = true;
            menuNumUpDownRadius.Visible = true;
            menuLabelDistance.Visible = true;
            menuNumUpDownDistance.Visible = true;
            menuLabelTexture.Visible = true;
            menuTextBoxTexture.Visible = true;
        }

        private void menuOff()
        {
            menuPanel.Visible = false;
            btnClose.Visible = false;
            btnCreate.Visible = false;
            menuLabelTitle.Visible = false;
            menuLabelRadius.Visible = false;
            menuNumUpDownRadius.Visible = false;
            menuLabelDistance.Visible = false;
            menuNumUpDownDistance.Visible = false;
            menuLabelTexture.Visible = false;
            menuTextBoxTexture.Visible = false;

            this.Focus();
            mIsMenuOn = false;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.viewPortPanel = new System.Windows.Forms.Panel();
            this.legendPanel = new System.Windows.Forms.Panel();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.legendMenuLabel = new System.Windows.Forms.Label();
            this.legendAnimLabel = new System.Windows.Forms.Label();
            this.legendOrbitLabel = new System.Windows.Forms.Label();
            this.legendExitLabel = new System.Windows.Forms.Label();
            this.menuLabelTitle = new System.Windows.Forms.Label();
            this.menuLabelRadius = new System.Windows.Forms.Label();
            this.menuNumUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.menuLabelDistance = new System.Windows.Forms.Label();
            this.menuNumUpDownDistance = new System.Windows.Forms.NumericUpDown();
            this.menuLabelTexture = new System.Windows.Forms.Label();
            this.menuTextBoxTexture = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.tmrPaint = new System.Windows.Forms.Timer(this.components);
            this.viewPortPanel.SuspendLayout();
            this.legendPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewPortPanel
            // 
            this.viewPortPanel.Controls.Add(this.legendPanel);
            this.viewPortPanel.Controls.Add(this.menuPanel);
            this.viewPortPanel.Location = new System.Drawing.Point(0, 0);
            this.viewPortPanel.Name = "viewPortPanel";
            this.viewPortPanel.Size = new System.Drawing.Size(1336, 738);
            this.viewPortPanel.TabIndex = 0;
            this.viewPortPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlViewPort_MouseDown);
            this.viewPortPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlViewPort_MouseUp);
            // 
            // legendPanel
            // 
            this.legendPanel.BackColor = System.Drawing.Color.FromArgb(0xc0, 0xf6, 0xff);
            this.legendPanel.Controls.Add(this.legendMenuLabel);
            this.legendPanel.Controls.Add(this.legendAnimLabel);
            this.legendPanel.Controls.Add(this.legendOrbitLabel);
            this.legendPanel.Controls.Add(this.legendExitLabel);
            this.legendPanel.Location = new System.Drawing.Point(1240, 8);
            this.legendPanel.Name = "legendPanel";
            this.legendPanel.Size = new System.Drawing.Size(90, 70);
            this.legendPanel.TabIndex = 12;
            // 
            // legendMenuLabel
            // 
            this.legendMenuLabel.AutoSize = true;
            this.legendMenuLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legendMenuLabel.Location = new System.Drawing.Point(5, 50);
            this.legendMenuLabel.Name = "legendMenuLabel";
            this.legendMenuLabel.Size = new System.Drawing.Size(100, 16);
            this.legendMenuLabel.TabIndex = 0;
            this.legendMenuLabel.Text = "M - Menu";
            // 
            // legendAnimLabel
            // 
            this.legendAnimLabel.AutoSize = true;
            this.legendAnimLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legendAnimLabel.Location = new System.Drawing.Point(5, 34);
            this.legendAnimLabel.Name = "legendAnimLabel";
            this.legendAnimLabel.Size = new System.Drawing.Size(100, 16);
            this.legendAnimLabel.TabIndex = 1;
            this.legendAnimLabel.Text = "R - Anim";
            // 
            // legendOrbitLabel
            // 
            this.legendOrbitLabel.AutoSize = true;
            this.legendOrbitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legendOrbitLabel.Location = new System.Drawing.Point(5, 18);
            this.legendOrbitLabel.Name = "legendOrbitLabel";
            this.legendOrbitLabel.Size = new System.Drawing.Size(51, 9);
            this.legendOrbitLabel.TabIndex = 1;
            this.legendOrbitLabel.Text = "O - Orbit";
            // 
            // legendExitLabel
            // 
            this.legendExitLabel.AutoSize = true;
            this.legendExitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legendExitLabel.Location = new System.Drawing.Point(5, 2);
            this.legendExitLabel.Name = "legendExitLabel";
            this.legendExitLabel.Size = new System.Drawing.Size(83, 16);
            this.legendExitLabel.TabIndex = 0;
            this.legendExitLabel.Text = "Esc - Exit";
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.FromArgb(0xde, 0xde, 0xde);
            this.menuPanel.Controls.Add(this.btnClose);
            this.menuPanel.Controls.Add(this.menuLabelTitle);
            this.menuPanel.Controls.Add(this.menuLabelRadius);
            this.menuPanel.Controls.Add(this.menuNumUpDownRadius);
            this.menuPanel.Controls.Add(this.menuLabelDistance);
            this.menuPanel.Controls.Add(this.menuNumUpDownDistance);
            this.menuPanel.Controls.Add(this.menuLabelTexture);
            this.menuPanel.Controls.Add(this.menuTextBoxTexture);
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(150, 250);
            this.menuPanel.TabIndex = 12;
            this.menuPanel.BorderStyle = BorderStyle.FixedSingle;
            menuPanel.Visible = false;
            // 
            // menuLabelTitle
            // 
            this.menuLabelTitle.AutoSize = true;
            this.menuLabelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuLabelTitle.Location = new System.Drawing.Point(15, 2);
            this.menuLabelTitle.Name = "menuLabelTitle";
            this.menuLabelTitle.Size = new System.Drawing.Size(90, 16);
            this.menuLabelTitle.TabIndex = 0;
            this.menuLabelTitle.Text = "Custom planet";
            // 
            // menuLabelRadius
            // 
            this.menuLabelRadius.AutoSize = true;
            this.menuLabelRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuLabelRadius.Location = new System.Drawing.Point(5, 32);
            this.menuLabelRadius.Name = "menuLabelRadius";
            this.menuLabelRadius.Size = new System.Drawing.Size(90, 16);
            this.menuLabelRadius.TabIndex = 0;
            this.menuLabelRadius.Text = "Radius:";
            // 
            // menuNumUpDownRadius
            // 
            this.menuNumUpDownRadius.AutoSize = true;
            this.menuNumUpDownRadius.Location = new System.Drawing.Point(90, 32);
            this.menuNumUpDownRadius.Size = new System.Drawing.Size(40, 16);
            this.menuNumUpDownRadius.Value = 1;
            this.menuNumUpDownRadius.Minimum = .1m;
            this.menuNumUpDownRadius.Maximum = 5;
            this.menuNumUpDownRadius.Increment = .1m;
            this.menuNumUpDownRadius.DecimalPlaces = 1;
            this.menuNumUpDownRadius.TextAlign = HorizontalAlignment.Right;
            // 
            // menuLabelDistance
            // 
            this.menuLabelDistance.AutoSize = true;
            this.menuLabelDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuLabelDistance.Location = new System.Drawing.Point(5, 62);
            this.menuLabelDistance.Name = "menuLabelDistance";
            this.menuLabelDistance.Size = new System.Drawing.Size(90, 16);
            this.menuLabelDistance.TabIndex = 0;
            this.menuLabelDistance.Text = "Distance:";
            // 
            // menuNumUpDownDistance
            // 
            this.menuNumUpDownDistance.AutoSize = true;
            this.menuNumUpDownDistance.Location = new System.Drawing.Point(90, 62);
            this.menuNumUpDownDistance.Size = new System.Drawing.Size(40, 16);
            this.menuNumUpDownDistance.Value = 32;
            this.menuNumUpDownDistance.Minimum = 7;
            this.menuNumUpDownDistance.Maximum = 151;
            this.menuNumUpDownDistance.Increment = 2;
            this.menuNumUpDownDistance.DecimalPlaces = 0;
            this.menuNumUpDownDistance.TextAlign = HorizontalAlignment.Right;
            // 
            // menuLabelTexture
            // 
            this.menuLabelTexture.AutoSize = true;
            this.menuLabelTexture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuLabelTexture.Location = new System.Drawing.Point(5, 92);
            this.menuLabelTexture.Name = "menuLabelTexture";
            this.menuLabelTexture.Size = new System.Drawing.Size(90, 16);
            this.menuLabelTexture.TabIndex = 0;
            this.menuLabelTexture.Text = "Texture:";
            // 
            // menuTextBoxTexture
            // 
            this.menuTextBoxTexture.Name = "menuTextBoxTexture";
            this.menuTextBoxTexture.Location = new System.Drawing.Point(5, 112);
            this.menuTextBoxTexture.Size = new System.Drawing.Size(137, 20);
            this.menuTextBoxTexture.Text = "custom.jpg";
            this.menuTextBoxTexture.TabIndex = 1;
            //
            // btnCreate
            //
            this.btnCreate.Text = "Create Planet";
            btnCreate.Parent = this;
            btnCreate.Location = new Point(35, 162);
            btnCreate.BackColor = System.Drawing.Color.FromArgb(0xaa, 0xaa, 0xaa);
            btnCreate.MouseClick += new System.Windows.Forms.MouseEventHandler(btnCreate_Click);
            btnCreate.Visible = false;
            //
            // btnClose
            //
            this.btnClose.Text = "Close";
            btnClose.Parent = this;
            btnClose.Location = new Point(35, 202);
            btnClose.BackColor = System.Drawing.Color.FromArgb(0xaa, 0xaa, 0xaa);
            btnClose.MouseClick += new System.Windows.Forms.MouseEventHandler(btnClose_Click);
            btnClose.Visible = false;
            // 
            // tmrPaint
            // 
            this.tmrPaint.Enabled = true;
            this.tmrPaint.Interval = 25;
            this.tmrPaint.Tick += new System.EventHandler(this.tmrPaint_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1336, 700);
            this.Controls.Add(this.viewPortPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solar System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.viewPortPanel.ResumeLayout(false);
            this.legendPanel.ResumeLayout(false);
            this.legendPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

