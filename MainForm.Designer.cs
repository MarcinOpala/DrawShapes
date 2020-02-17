﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShape
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 

        private void InitializeComponent()
        {
            this.pnlAllForm = new System.Windows.Forms.Panel();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.lblUnitDiametr = new System.Windows.Forms.Label();
            this.lblDiametr = new System.Windows.Forms.Label();
            this.lblSide = new System.Windows.Forms.Label();
            this.txtDiameter = new System.Windows.Forms.TextBox();
            this.txtSlides = new System.Windows.Forms.TextBox();
            this.lblUnitVertical = new System.Windows.Forms.Label();
            this.lblUnitHorizontal = new System.Windows.Forms.Label();
            this.txtMarginV = new System.Windows.Forms.TextBox();
            this.txtMarginH = new System.Windows.Forms.TextBox();
            this.lblMarginVertical = new System.Windows.Forms.Label();
            this.lblMarginHorizontal = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.lblUnitH = new System.Windows.Forms.Label();
            this.lblUnitW = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlDraw = new System.Windows.Forms.Panel();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.pnlAllForm.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlDraw.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAllForm
            // 
            this.pnlAllForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAllForm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlAllForm.Controls.Add(this.pnlInput);
            this.pnlAllForm.Controls.Add(this.pnlMenu);
            this.pnlAllForm.Controls.Add(this.pnlDraw);
            this.pnlAllForm.Location = new System.Drawing.Point(0, 0);
            this.pnlAllForm.Name = "pnlAllForm";
            this.pnlAllForm.Size = new System.Drawing.Size(705, 450);
            this.pnlAllForm.TabIndex = 0;
            // 
            // pnlInput
            // 
            this.pnlInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlInput.Controls.Add(this.lblUnitDiametr);
            this.pnlInput.Controls.Add(this.lblDiametr);
            this.pnlInput.Controls.Add(this.lblSide);
            this.pnlInput.Controls.Add(this.txtDiameter);
            this.pnlInput.Controls.Add(this.txtSlides);
            this.pnlInput.Controls.Add(this.lblUnitVertical);
            this.pnlInput.Controls.Add(this.lblUnitHorizontal);
            this.pnlInput.Controls.Add(this.txtMarginV);
            this.pnlInput.Controls.Add(this.txtMarginH);
            this.pnlInput.Controls.Add(this.lblMarginVertical);
            this.pnlInput.Controls.Add(this.lblMarginHorizontal);
            this.pnlInput.Controls.Add(this.btnDraw);
            this.pnlInput.Controls.Add(this.lblUnitH);
            this.pnlInput.Controls.Add(this.lblUnitW);
            this.pnlInput.Controls.Add(this.txtHeight);
            this.pnlInput.Controls.Add(this.txtWidth);
            this.pnlInput.Controls.Add(this.lblHeight);
            this.pnlInput.Controls.Add(this.lblWidth);
            this.pnlInput.Location = new System.Drawing.Point(425, 25);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(255, 274);
            this.pnlInput.TabIndex = 7;
            // 
            // lblUnitDiametr
            // 
            this.lblUnitDiametr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnitDiametr.Location = new System.Drawing.Point(181, 245);
            this.lblUnitDiametr.Name = "lblUnitDiametr";
            this.lblUnitDiametr.Size = new System.Drawing.Size(55, 32);
            this.lblUnitDiametr.TabIndex = 36;
            this.lblUnitDiametr.Text = "lblUnitDiametr";
            // 
            // lblDiametr
            // 
            this.lblDiametr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiametr.Location = new System.Drawing.Point(16, 245);
            this.lblDiametr.Name = "lblDiametr";
            this.lblDiametr.Size = new System.Drawing.Size(80, 83);
            this.lblDiametr.TabIndex = 34;
            this.lblDiametr.Text = "lblDiametr";
            // 
            // lblSide
            // 
            this.lblSide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSide.Location = new System.Drawing.Point(16, 215);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(80, 85);
            this.lblSide.TabIndex = 33;
            this.lblSide.Text = "lblSide";
            this.lblSide.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtDiameter
            // 
            this.txtDiameter.Location = new System.Drawing.Point(106, 239);
            this.txtDiameter.Name = "txtDiameter";
            this.txtDiameter.Size = new System.Drawing.Size(70, 22);
            this.txtDiameter.TabIndex = 32;
            this.txtDiameter.Text = "txtDiameter";
            // 
            // txtSlides
            // 
            this.txtSlides.Location = new System.Drawing.Point(106, 212);
            this.txtSlides.Name = "txtSlides";
            this.txtSlides.Size = new System.Drawing.Size(70, 22);
            this.txtSlides.TabIndex = 31;
            this.txtSlides.Text = "txtSlides";
            // 
            // lblUnitVertical
            // 
            this.lblUnitVertical.Location = new System.Drawing.Point(181, 187);
            this.lblUnitVertical.Name = "lblUnitVertical";
            this.lblUnitVertical.Size = new System.Drawing.Size(35, 20);
            this.lblUnitVertical.TabIndex = 30;
            this.lblUnitVertical.Text = "lblUnitVertical";
            // 
            // lblUnitHorizontal
            // 
            this.lblUnitHorizontal.Location = new System.Drawing.Point(181, 157);
            this.lblUnitHorizontal.Name = "lblUnitHorizontal";
            this.lblUnitHorizontal.Size = new System.Drawing.Size(35, 20);
            this.lblUnitHorizontal.TabIndex = 29;
            this.lblUnitHorizontal.Text = "lblUnitHorizontal";
            // 
            // txtMarginV
            // 
            this.txtMarginV.Location = new System.Drawing.Point(106, 184);
            this.txtMarginV.Name = "txtMarginV";
            this.txtMarginV.Size = new System.Drawing.Size(70, 22);
            this.txtMarginV.TabIndex = 28;
            this.txtMarginV.Text = "txtMarginV";
            // 
            // txtMarginH
            // 
            this.txtMarginH.Location = new System.Drawing.Point(106, 157);
            this.txtMarginH.Name = "txtMarginH";
            this.txtMarginH.Size = new System.Drawing.Size(70, 22);
            this.txtMarginH.TabIndex = 27;
            this.txtMarginH.Text = "txtMarginH";
            // 
            // lblMarginVertical
            // 
            this.lblMarginVertical.Location = new System.Drawing.Point(16, 187);
            this.lblMarginVertical.Name = "lblMarginVertical";
            this.lblMarginVertical.Size = new System.Drawing.Size(90, 20);
            this.lblMarginVertical.TabIndex = 26;
            this.lblMarginVertical.Text = "lblMarginVertical";
            // 
            // lblMarginHorizontal
            // 
            this.lblMarginHorizontal.Location = new System.Drawing.Point(16, 157);
            this.lblMarginHorizontal.Name = "lblMarginHorizontal";
            this.lblMarginHorizontal.Size = new System.Drawing.Size(90, 20);
            this.lblMarginHorizontal.TabIndex = 25;
            this.lblMarginHorizontal.Text = "lblMarginHorizontal";
            // 
            // btnDraw
            // 
            this.btnDraw.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDraw.Location = new System.Drawing.Point(16, 97);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(222, 40);
            this.btnDraw.TabIndex = 24;
            this.btnDraw.Text = "btnDraw";
            this.btnDraw.UseVisualStyleBackColor = true;
            // 
            // lblUnitH
            // 
            this.lblUnitH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnitH.Location = new System.Drawing.Point(178, 55);
            this.lblUnitH.Name = "lblUnitH";
            this.lblUnitH.Size = new System.Drawing.Size(70, 83);
            this.lblUnitH.TabIndex = 23;
            this.lblUnitH.Text = "lblUnitH";
            // 
            // lblUnitW
            // 
            this.lblUnitW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnitW.Location = new System.Drawing.Point(178, 25);
            this.lblUnitW.Name = "lblUnitW";
            this.lblUnitW.Size = new System.Drawing.Size(70, 85);
            this.lblUnitW.TabIndex = 22;
            this.lblUnitW.Text = "lblUnitW";
            // 
            // txtHeight
            // 
            this.txtHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeight.Location = new System.Drawing.Point(103, 52);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(70, 22);
            this.txtHeight.TabIndex = 21;
            this.txtHeight.Text = "txtHeight";
            // 
            // txtWidth
            // 
            this.txtWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWidth.Location = new System.Drawing.Point(103, 25);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(70, 22);
            this.txtWidth.TabIndex = 20;
            this.txtWidth.Text = "txtWidth";
            // 
            // lblHeight
            // 
            this.lblHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeight.Location = new System.Drawing.Point(13, 55);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(80, 83);
            this.lblHeight.TabIndex = 19;
            this.lblHeight.Text = "lblHeight";
            // 
            // lblWidth
            // 
            this.lblWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWidth.Location = new System.Drawing.Point(13, 25);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(80, 85);
            this.lblWidth.TabIndex = 18;
            this.lblWidth.Text = "lblWidth";
            // 
            // pnlMenu
            // 
            this.pnlMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMenu.Location = new System.Drawing.Point(425, 305);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(255, 120);
            this.pnlMenu.TabIndex = 1;
            // 
            // pnlDraw
            // 
            this.pnlDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDraw.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDraw.Controls.Add(this.pnlCanvas);
            this.pnlDraw.Location = new System.Drawing.Point(25, 25);
            this.pnlDraw.Name = "pnlDraw";
            this.pnlDraw.Size = new System.Drawing.Size(400, 400);
            this.pnlDraw.TabIndex = 0;
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCanvas.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCanvas.Location = new System.Drawing.Point(25, 25);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(350, 350);
            this.pnlCanvas.TabIndex = 0;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(705, 450);
            this.Controls.Add(this.pnlAllForm);
            this.MaximumSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(723, 497);
            this.Name = "MainForm";
            this.Text = "Draw Shapes";
            this.pnlAllForm.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.pnlDraw.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAllForm;
        private System.Windows.Forms.Panel pnlDraw;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlCanvas;
        private Panel pnlInput;
        private Label lblUnitH;
        private Label lblUnitW;
        private TextBox txtHeight;
        private TextBox txtWidth;
        private Label lblHeight;
        private Label lblWidth;
        private Label lblUnitVertical;
        private Label lblUnitHorizontal;
        private TextBox txtMarginV;
        private TextBox txtMarginH;
        private Label lblMarginVertical;
        private Label lblMarginHorizontal;
        private Button btnDraw;
        private TextBox txtDiameter;
        private TextBox txtSlides;
        private Label lblUnitDiametr;
        private Label lblDiametr;
        private Label lblSide;
    }
}

