using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShape {
  partial class MainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
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

    private void InitializeComponent() {
      this.pnlAllForm = new System.Windows.Forms.Panel();
      this.pnlInput = new System.Windows.Forms.Panel();
      this.btnSetToCurve = new System.Windows.Forms.Button();
      this.lblSelectSide = new System.Windows.Forms.Label();
      this.txtSelectSide = new System.Windows.Forms.TextBox();
      this.btnDrawProfile = new System.Windows.Forms.Button();
      this.lblUnitDiametr = new System.Windows.Forms.Label();
      this.lblDiametr = new System.Windows.Forms.Label();
      this.lblSide = new System.Windows.Forms.Label();
      this.txtDiameter = new System.Windows.Forms.TextBox();
      this.txtSides = new System.Windows.Forms.TextBox();
      this.lblUnitVertical = new System.Windows.Forms.Label();
      this.lblUnitHorizontal = new System.Windows.Forms.Label();
      this.txtMarginV = new System.Windows.Forms.TextBox();
      this.txtMarginH = new System.Windows.Forms.TextBox();
      this.lblMarginVertical = new System.Windows.Forms.Label();
      this.lblMarginHorizontal = new System.Windows.Forms.Label();
      this.btnDrawRegularPolygon = new System.Windows.Forms.Button();
      this.lblUnitH = new System.Windows.Forms.Label();
      this.lblUnitW = new System.Windows.Forms.Label();
      this.txtHeight = new System.Windows.Forms.TextBox();
      this.txtWidth = new System.Windows.Forms.TextBox();
      this.lblHeight = new System.Windows.Forms.Label();
      this.lblWidth = new System.Windows.Forms.Label();
      this.pnlDraw = new System.Windows.Forms.Panel();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.lblProfileUnit = new System.Windows.Forms.Label();
      this.lblProfileSize = new System.Windows.Forms.Label();
      this.txtProfileSize = new System.Windows.Forms.TextBox();
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
      this.pnlInput.Controls.Add(this.lblProfileUnit);
      this.pnlInput.Controls.Add(this.lblProfileSize);
      this.pnlInput.Controls.Add(this.txtProfileSize);
      this.pnlInput.Controls.Add(this.btnSetToCurve);
      this.pnlInput.Controls.Add(this.lblSelectSide);
      this.pnlInput.Controls.Add(this.txtSelectSide);
      this.pnlInput.Controls.Add(this.btnDrawProfile);
      this.pnlInput.Controls.Add(this.lblUnitDiametr);
      this.pnlInput.Controls.Add(this.lblDiametr);
      this.pnlInput.Controls.Add(this.lblSide);
      this.pnlInput.Controls.Add(this.txtDiameter);
      this.pnlInput.Controls.Add(this.txtSides);
      this.pnlInput.Controls.Add(this.lblUnitVertical);
      this.pnlInput.Controls.Add(this.lblUnitHorizontal);
      this.pnlInput.Controls.Add(this.txtMarginV);
      this.pnlInput.Controls.Add(this.txtMarginH);
      this.pnlInput.Controls.Add(this.lblMarginVertical);
      this.pnlInput.Controls.Add(this.lblMarginHorizontal);
      this.pnlInput.Controls.Add(this.btnDrawRegularPolygon);
      this.pnlInput.Controls.Add(this.lblUnitH);
      this.pnlInput.Controls.Add(this.lblUnitW);
      this.pnlInput.Controls.Add(this.txtHeight);
      this.pnlInput.Controls.Add(this.txtWidth);
      this.pnlInput.Controls.Add(this.lblHeight);
      this.pnlInput.Controls.Add(this.lblWidth);
      this.pnlInput.Location = new System.Drawing.Point(425, 25);
      this.pnlInput.Name = "pnlInput";
      this.pnlInput.Size = new System.Drawing.Size(255, 400);
      this.pnlInput.TabIndex = 7;
      // 
      // btnSetToCurve
      // 
      this.btnSetToCurve.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnSetToCurve.Location = new System.Drawing.Point(14, 356);
      this.btnSetToCurve.Name = "btnSetToCurve";
      this.btnSetToCurve.Size = new System.Drawing.Size(222, 40);
      this.btnSetToCurve.TabIndex = 41;
      this.btnSetToCurve.Text = "btnDrawArc";
      this.btnSetToCurve.UseVisualStyleBackColor = true;
      // 
      // lblSelectSide
      // 
      this.lblSelectSide.Location = new System.Drawing.Point(17, 329);
      this.lblSelectSide.Name = "lblSelectSide";
      this.lblSelectSide.Size = new System.Drawing.Size(80, 30);
      this.lblSelectSide.TabIndex = 40;
      this.lblSelectSide.Text = "lblSelectSide";
      // 
      // txtSelectSide
      // 
      this.txtSelectSide.Location = new System.Drawing.Point(103, 328);
      this.txtSelectSide.Name = "txtSelectSide";
      this.txtSelectSide.Size = new System.Drawing.Size(70, 22);
      this.txtSelectSide.TabIndex = 38;
      this.txtSelectSide.Text = "txtSelectSide";
      // 
      // btnDrawProfile
      // 
      this.btnDrawProfile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnDrawProfile.Location = new System.Drawing.Point(14, 277);
      this.btnDrawProfile.Name = "btnDrawProfile";
      this.btnDrawProfile.Size = new System.Drawing.Size(222, 40);
      this.btnDrawProfile.TabIndex = 37;
      this.btnDrawProfile.Text = "btnDrawProfile";
      this.btnDrawProfile.UseVisualStyleBackColor = true;
      // 
      // lblUnitDiametr
      // 
      this.lblUnitDiametr.Location = new System.Drawing.Point(182, 219);
      this.lblUnitDiametr.Name = "lblUnitDiametr";
      this.lblUnitDiametr.Size = new System.Drawing.Size(55, 30);
      this.lblUnitDiametr.TabIndex = 36;
      this.lblUnitDiametr.Text = "lblUnitDiametr";
      // 
      // lblDiametr
      // 
      this.lblDiametr.Location = new System.Drawing.Point(16, 222);
      this.lblDiametr.Name = "lblDiametr";
      this.lblDiametr.Size = new System.Drawing.Size(80, 30);
      this.lblDiametr.TabIndex = 34;
      this.lblDiametr.Text = "lblDiametr";
      // 
      // lblSide
      // 
      this.lblSide.Location = new System.Drawing.Point(16, 192);
      this.lblSide.Name = "lblSide";
      this.lblSide.Size = new System.Drawing.Size(80, 30);
      this.lblSide.TabIndex = 33;
      this.lblSide.Text = "lblSide";
      // 
      // txtDiameter
      // 
      this.txtDiameter.Location = new System.Drawing.Point(106, 216);
      this.txtDiameter.Name = "txtDiameter";
      this.txtDiameter.Size = new System.Drawing.Size(70, 22);
      this.txtDiameter.TabIndex = 32;
      this.txtDiameter.Text = "txtDiameter";
      // 
      // txtSides
      // 
      this.txtSides.Location = new System.Drawing.Point(106, 189);
      this.txtSides.Name = "txtSides";
      this.txtSides.Size = new System.Drawing.Size(70, 22);
      this.txtSides.TabIndex = 31;
      this.txtSides.Text = "txtSlides";
      // 
      // lblUnitVertical
      // 
      this.lblUnitVertical.Location = new System.Drawing.Point(181, 164);
      this.lblUnitVertical.Name = "lblUnitVertical";
      this.lblUnitVertical.Size = new System.Drawing.Size(35, 20);
      this.lblUnitVertical.TabIndex = 30;
      this.lblUnitVertical.Text = "lblUnitVertical";
      // 
      // lblUnitHorizontal
      // 
      this.lblUnitHorizontal.Location = new System.Drawing.Point(181, 134);
      this.lblUnitHorizontal.Name = "lblUnitHorizontal";
      this.lblUnitHorizontal.Size = new System.Drawing.Size(35, 20);
      this.lblUnitHorizontal.TabIndex = 29;
      this.lblUnitHorizontal.Text = "lblUnitHorizontal";
      // 
      // txtMarginV
      // 
      this.txtMarginV.Location = new System.Drawing.Point(106, 161);
      this.txtMarginV.Name = "txtMarginV";
      this.txtMarginV.Size = new System.Drawing.Size(70, 22);
      this.txtMarginV.TabIndex = 28;
      this.txtMarginV.Text = "txtMarginV";
      // 
      // txtMarginH
      // 
      this.txtMarginH.Location = new System.Drawing.Point(106, 134);
      this.txtMarginH.Name = "txtMarginH";
      this.txtMarginH.Size = new System.Drawing.Size(70, 22);
      this.txtMarginH.TabIndex = 27;
      this.txtMarginH.Text = "txtMarginH";
      // 
      // lblMarginVertical
      // 
      this.lblMarginVertical.Location = new System.Drawing.Point(16, 164);
      this.lblMarginVertical.Name = "lblMarginVertical";
      this.lblMarginVertical.Size = new System.Drawing.Size(90, 20);
      this.lblMarginVertical.TabIndex = 26;
      this.lblMarginVertical.Text = "lblMarginVertical";
      // 
      // lblMarginHorizontal
      // 
      this.lblMarginHorizontal.Location = new System.Drawing.Point(16, 134);
      this.lblMarginHorizontal.Name = "lblMarginHorizontal";
      this.lblMarginHorizontal.Size = new System.Drawing.Size(90, 20);
      this.lblMarginHorizontal.TabIndex = 25;
      this.lblMarginHorizontal.Text = "lblMarginHorizontal";
      // 
      // btnDrawRegularPolygon
      // 
      this.btnDrawRegularPolygon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnDrawRegularPolygon.Location = new System.Drawing.Point(16, 82);
      this.btnDrawRegularPolygon.Name = "btnDrawRegularPolygon";
      this.btnDrawRegularPolygon.Size = new System.Drawing.Size(222, 40);
      this.btnDrawRegularPolygon.TabIndex = 24;
      this.btnDrawRegularPolygon.Text = "btnDrawRegularPolygon";
      this.btnDrawRegularPolygon.UseVisualStyleBackColor = true;
      // 
      // lblUnitH
      // 
      this.lblUnitH.Location = new System.Drawing.Point(178, 55);
      this.lblUnitH.Name = "lblUnitH";
      this.lblUnitH.Size = new System.Drawing.Size(70, 39);
      this.lblUnitH.TabIndex = 23;
      this.lblUnitH.Text = "lblUnitH";
      // 
      // lblUnitW
      // 
      this.lblUnitW.Location = new System.Drawing.Point(178, 25);
      this.lblUnitW.Name = "lblUnitW";
      this.lblUnitW.Size = new System.Drawing.Size(70, 22);
      this.lblUnitW.TabIndex = 22;
      this.lblUnitW.Text = "lblUnitW";
      // 
      // txtHeight
      // 
      this.txtHeight.Location = new System.Drawing.Point(103, 52);
      this.txtHeight.Name = "txtHeight";
      this.txtHeight.Size = new System.Drawing.Size(70, 22);
      this.txtHeight.TabIndex = 21;
      this.txtHeight.Text = "txtHeight";
      // 
      // txtWidth
      // 
      this.txtWidth.Location = new System.Drawing.Point(103, 25);
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new System.Drawing.Size(70, 22);
      this.txtWidth.TabIndex = 20;
      this.txtWidth.Text = "txtWidth";
      // 
      // lblHeight
      // 
      this.lblHeight.Location = new System.Drawing.Point(13, 55);
      this.lblHeight.Name = "lblHeight";
      this.lblHeight.Size = new System.Drawing.Size(80, 30);
      this.lblHeight.TabIndex = 19;
      this.lblHeight.Text = "lblHeight";
      // 
      // lblWidth
      // 
      this.lblWidth.Location = new System.Drawing.Point(13, 25);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(80, 30);
      this.lblWidth.TabIndex = 18;
      this.lblWidth.Text = "lblWidth";
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
      // lblProfileUnit
      // 
      this.lblProfileUnit.Location = new System.Drawing.Point(183, 249);
      this.lblProfileUnit.Name = "lblProfileUnit";
      this.lblProfileUnit.Size = new System.Drawing.Size(55, 19);
      this.lblProfileUnit.TabIndex = 44;
      this.lblProfileUnit.Text = "lblProfileUnit";
      // 
      // lblProfileSize
      // 
      this.lblProfileSize.Location = new System.Drawing.Point(17, 252);
      this.lblProfileSize.Name = "lblProfileSize";
      this.lblProfileSize.Size = new System.Drawing.Size(80, 22);
      this.lblProfileSize.TabIndex = 43;
      this.lblProfileSize.Text = "lblProfileSize";
      // 
      // txtProfileSize
      // 
      this.txtProfileSize.Location = new System.Drawing.Point(107, 246);
      this.txtProfileSize.Name = "txtProfileSize";
      this.txtProfileSize.Size = new System.Drawing.Size(70, 22);
      this.txtProfileSize.TabIndex = 42;
      this.txtProfileSize.Text = "txtProfileSize";
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
    private Button btnDrawRegularPolygon;
    private TextBox txtDiameter;
    private TextBox txtSides;
    private Label lblUnitDiametr;
    private Label lblDiametr;
    private Label lblSide;
    private Button btnSetToCurve;
    private Label lblSelectSide;
    private TextBox txtSelectSide;
    private Button btnDrawProfile;
    private Label lblProfileUnit;
    private Label lblProfileSize;
    private TextBox txtProfileSize;
  }
}

