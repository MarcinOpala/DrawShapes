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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.btnAddMullionHorizontal = new System.Windows.Forms.Button();
      this.btnRemoveSash = new System.Windows.Forms.Button();
      this.btnAddSash = new System.Windows.Forms.Button();
      this.btnAddMullionVertical = new System.Windows.Forms.Button();
      this.txtProjectName = new System.Windows.Forms.TextBox();
      this.lblProjectName = new System.Windows.Forms.Label();
      this.btnSetToCurve = new System.Windows.Forms.Button();
      this.btnDrawAssembly = new System.Windows.Forms.Button();
      this.btnCreateProject = new System.Windows.Forms.Button();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.lblCWidth_Mullion = new System.Windows.Forms.Label();
      this.txtCWidth_Mullion = new System.Windows.Forms.TextBox();
      this.lblCWidth_Profile = new System.Windows.Forms.Label();
      this.txtCWidth_Profile = new System.Windows.Forms.TextBox();
      this.lblMullionWidth = new System.Windows.Forms.Label();
      this.txtMullionWidth = new System.Windows.Forms.TextBox();
      this.lblMullionLocation = new System.Windows.Forms.Label();
      this.txtMullionLocationX = new System.Windows.Forms.TextBox();
      this.lblUnitWidth = new System.Windows.Forms.Label();
      this.lblUnitHeight = new System.Windows.Forms.Label();
      this.txtWidth = new System.Windows.Forms.TextBox();
      this.txtHeight = new System.Windows.Forms.TextBox();
      this.lblWidth = new System.Windows.Forms.Label();
      this.lblHeight = new System.Windows.Forms.Label();
      this.lblSelectSide = new System.Windows.Forms.Label();
      this.txtSelectSide = new System.Windows.Forms.TextBox();
      this.lblProfileUnit = new System.Windows.Forms.Label();
      this.lblProfileSize = new System.Windows.Forms.Label();
      this.txtProfileSize = new System.Windows.Forms.TextBox();
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
      this.pnlDraw = new System.Windows.Forms.Panel();
      this.pnlCanvas = new System.Windows.Forms.Panel();
      this.txtMullionLocationY = new System.Windows.Forms.TextBox();
      this.pnlAllForm.SuspendLayout();
      this.pnlInput.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
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
      this.pnlAllForm.Size = new System.Drawing.Size(1205, 753);
      this.pnlAllForm.TabIndex = 0;
      // 
      // pnlInput
      // 
      this.pnlInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlInput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.pnlInput.Controls.Add(this.tabControl1);
      this.pnlInput.Location = new System.Drawing.Point(731, 12);
      this.pnlInput.Name = "pnlInput";
      this.pnlInput.Size = new System.Drawing.Size(449, 716);
      this.pnlInput.TabIndex = 7;
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.tabControl1.ItemSize = new System.Drawing.Size(200, 35);
      this.tabControl1.Location = new System.Drawing.Point(1, 3);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(446, 688);
      this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabControl1.TabIndex = 46;
      // 
      // tabPage1
      // 
      this.tabPage1.BackColor = System.Drawing.Color.LightGray;
      this.tabPage1.Controls.Add(this.btnAddMullionHorizontal);
      this.tabPage1.Controls.Add(this.btnRemoveSash);
      this.tabPage1.Controls.Add(this.btnAddSash);
      this.tabPage1.Controls.Add(this.btnAddMullionVertical);
      this.tabPage1.Controls.Add(this.txtProjectName);
      this.tabPage1.Controls.Add(this.lblProjectName);
      this.tabPage1.Controls.Add(this.btnSetToCurve);
      this.tabPage1.Controls.Add(this.btnDrawAssembly);
      this.tabPage1.Controls.Add(this.btnCreateProject);
      this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.tabPage1.Location = new System.Drawing.Point(4, 39);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(438, 645);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "tabPage1";
      // 
      // btnAddMullionHorizontal
      // 
      this.btnAddMullionHorizontal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnAddMullionHorizontal.Location = new System.Drawing.Point(32, 280);
      this.btnAddMullionHorizontal.Name = "btnAddMullionHorizontal";
      this.btnAddMullionHorizontal.Size = new System.Drawing.Size(158, 40);
      this.btnAddMullionHorizontal.TabIndex = 53;
      this.btnAddMullionHorizontal.Text = "btnAddMullionHorizontal";
      this.btnAddMullionHorizontal.UseVisualStyleBackColor = true;
      this.btnAddMullionHorizontal.Click += new System.EventHandler(this.InsertMullion_Horizontal);
      // 
      // btnRemoveSash
      // 
      this.btnRemoveSash.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnRemoveSash.Location = new System.Drawing.Point(32, 380);
      this.btnRemoveSash.Name = "btnRemoveSash";
      this.btnRemoveSash.Size = new System.Drawing.Size(158, 40);
      this.btnRemoveSash.TabIndex = 52;
      this.btnRemoveSash.Text = "btnRemoveSash";
      this.btnRemoveSash.UseVisualStyleBackColor = true;
      this.btnRemoveSash.Click += new System.EventHandler(this.RemoveSash);
      // 
      // btnAddSash
      // 
      this.btnAddSash.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnAddSash.Location = new System.Drawing.Point(32, 330);
      this.btnAddSash.Name = "btnAddSash";
      this.btnAddSash.Size = new System.Drawing.Size(158, 40);
      this.btnAddSash.TabIndex = 51;
      this.btnAddSash.Text = "btnAddSash";
      this.btnAddSash.UseVisualStyleBackColor = true;
      this.btnAddSash.Click += new System.EventHandler(this.InsertSash);
      // 
      // btnAddMullionVertical
      // 
      this.btnAddMullionVertical.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnAddMullionVertical.Location = new System.Drawing.Point(32, 230);
      this.btnAddMullionVertical.Name = "btnAddMullionVertical";
      this.btnAddMullionVertical.Size = new System.Drawing.Size(158, 40);
      this.btnAddMullionVertical.TabIndex = 50;
      this.btnAddMullionVertical.Text = "btnAddMullionVertical";
      this.btnAddMullionVertical.UseVisualStyleBackColor = true;
      this.btnAddMullionVertical.Click += new System.EventHandler(this.InsertMullion_Vertical);
      // 
      // txtProjectName
      // 
      this.txtProjectName.BackColor = System.Drawing.SystemColors.ControlLight;
      this.txtProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.txtProjectName.ForeColor = System.Drawing.SystemColors.ControlDark;
      this.txtProjectName.Location = new System.Drawing.Point(188, 24);
      this.txtProjectName.Name = "txtProjectName";
      this.txtProjectName.Size = new System.Drawing.Size(229, 30);
      this.txtProjectName.TabIndex = 49;
      this.txtProjectName.Text = "txtProjectName";
      this.txtProjectName.Click += new System.EventHandler(this.txtProjectName_Click);
      // 
      // lblProjectName
      // 
      this.lblProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblProjectName.ForeColor = System.Drawing.SystemColors.WindowText;
      this.lblProjectName.Location = new System.Drawing.Point(54, 27);
      this.lblProjectName.Name = "lblProjectName";
      this.lblProjectName.Size = new System.Drawing.Size(136, 28);
      this.lblProjectName.TabIndex = 48;
      this.lblProjectName.Text = "lblProjectName";
      // 
      // btnSetToCurve
      // 
      this.btnSetToCurve.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnSetToCurve.BackColor = System.Drawing.Color.DarkGray;
      this.btnSetToCurve.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.btnSetToCurve.Location = new System.Drawing.Point(32, 180);
      this.btnSetToCurve.Name = "btnSetToCurve";
      this.btnSetToCurve.Size = new System.Drawing.Size(158, 40);
      this.btnSetToCurve.TabIndex = 43;
      this.btnSetToCurve.Text = "btnDrawArc";
      this.btnSetToCurve.UseVisualStyleBackColor = false;
      // 
      // btnDrawAssembly
      // 
      this.btnDrawAssembly.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnDrawAssembly.Location = new System.Drawing.Point(32, 130);
      this.btnDrawAssembly.Name = "btnDrawAssembly";
      this.btnDrawAssembly.Size = new System.Drawing.Size(158, 40);
      this.btnDrawAssembly.TabIndex = 42;
      this.btnDrawAssembly.Text = "btnDrawAssembly";
      this.btnDrawAssembly.UseVisualStyleBackColor = true;
      // 
      // btnCreateProject
      // 
      this.btnCreateProject.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btnCreateProject.Location = new System.Drawing.Point(32, 80);
      this.btnCreateProject.Name = "btnCreateProject";
      this.btnCreateProject.Size = new System.Drawing.Size(158, 40);
      this.btnCreateProject.TabIndex = 25;
      this.btnCreateProject.Text = "btnCreateProject";
      this.btnCreateProject.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.BackColor = System.Drawing.Color.LightGray;
      this.tabPage2.Controls.Add(this.txtMullionLocationY);
      this.tabPage2.Controls.Add(this.lblCWidth_Mullion);
      this.tabPage2.Controls.Add(this.txtCWidth_Mullion);
      this.tabPage2.Controls.Add(this.lblCWidth_Profile);
      this.tabPage2.Controls.Add(this.txtCWidth_Profile);
      this.tabPage2.Controls.Add(this.lblMullionWidth);
      this.tabPage2.Controls.Add(this.txtMullionWidth);
      this.tabPage2.Controls.Add(this.lblMullionLocation);
      this.tabPage2.Controls.Add(this.txtMullionLocationX);
      this.tabPage2.Controls.Add(this.lblUnitWidth);
      this.tabPage2.Controls.Add(this.lblUnitHeight);
      this.tabPage2.Controls.Add(this.txtWidth);
      this.tabPage2.Controls.Add(this.txtHeight);
      this.tabPage2.Controls.Add(this.lblWidth);
      this.tabPage2.Controls.Add(this.lblHeight);
      this.tabPage2.Controls.Add(this.lblSelectSide);
      this.tabPage2.Controls.Add(this.txtSelectSide);
      this.tabPage2.Controls.Add(this.lblProfileUnit);
      this.tabPage2.Controls.Add(this.lblProfileSize);
      this.tabPage2.Controls.Add(this.txtProfileSize);
      this.tabPage2.Controls.Add(this.lblUnitDiametr);
      this.tabPage2.Controls.Add(this.lblDiametr);
      this.tabPage2.Controls.Add(this.lblSide);
      this.tabPage2.Controls.Add(this.txtDiameter);
      this.tabPage2.Controls.Add(this.txtSides);
      this.tabPage2.Controls.Add(this.lblUnitVertical);
      this.tabPage2.Controls.Add(this.lblUnitHorizontal);
      this.tabPage2.Controls.Add(this.txtMarginV);
      this.tabPage2.Controls.Add(this.txtMarginH);
      this.tabPage2.Controls.Add(this.lblMarginVertical);
      this.tabPage2.Controls.Add(this.lblMarginHorizontal);
      this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.tabPage2.Location = new System.Drawing.Point(4, 39);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(438, 645);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      // 
      // lblCWidth_Mullion
      // 
      this.lblCWidth_Mullion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblCWidth_Mullion.Location = new System.Drawing.Point(44, 357);
      this.lblCWidth_Mullion.Name = "lblCWidth_Mullion";
      this.lblCWidth_Mullion.Size = new System.Drawing.Size(108, 30);
      this.lblCWidth_Mullion.TabIndex = 74;
      this.lblCWidth_Mullion.Text = "lblCWidth_Mullion";
      // 
      // txtCWidth_Mullion
      // 
      this.txtCWidth_Mullion.Location = new System.Drawing.Point(154, 354);
      this.txtCWidth_Mullion.Name = "txtCWidth_Mullion";
      this.txtCWidth_Mullion.Size = new System.Drawing.Size(84, 27);
      this.txtCWidth_Mullion.TabIndex = 73;
      this.txtCWidth_Mullion.Text = "txtCWidth_Mullion";
      // 
      // lblCWidth_Profile
      // 
      this.lblCWidth_Profile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblCWidth_Profile.Location = new System.Drawing.Point(44, 327);
      this.lblCWidth_Profile.Name = "lblCWidth_Profile";
      this.lblCWidth_Profile.Size = new System.Drawing.Size(94, 30);
      this.lblCWidth_Profile.TabIndex = 72;
      this.lblCWidth_Profile.Text = "lblCWidth_Profile";
      // 
      // txtCWidth_Profile
      // 
      this.txtCWidth_Profile.Location = new System.Drawing.Point(154, 324);
      this.txtCWidth_Profile.Name = "txtCWidth_Profile";
      this.txtCWidth_Profile.Size = new System.Drawing.Size(84, 27);
      this.txtCWidth_Profile.TabIndex = 71;
      this.txtCWidth_Profile.Text = "txtCWidth_Profile";
      // 
      // lblMullionWidth
      // 
      this.lblMullionWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblMullionWidth.Location = new System.Drawing.Point(43, 297);
      this.lblMullionWidth.Name = "lblMullionWidth";
      this.lblMullionWidth.Size = new System.Drawing.Size(108, 30);
      this.lblMullionWidth.TabIndex = 70;
      this.lblMullionWidth.Text = "lblMullionSize";
      // 
      // txtMullionWidth
      // 
      this.txtMullionWidth.Location = new System.Drawing.Point(153, 294);
      this.txtMullionWidth.Name = "txtMullionWidth";
      this.txtMullionWidth.Size = new System.Drawing.Size(84, 27);
      this.txtMullionWidth.TabIndex = 69;
      this.txtMullionWidth.Text = "txtMullionSize";
      // 
      // lblMullionLocation
      // 
      this.lblMullionLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblMullionLocation.Location = new System.Drawing.Point(43, 267);
      this.lblMullionLocation.Name = "lblMullionLocation";
      this.lblMullionLocation.Size = new System.Drawing.Size(94, 30);
      this.lblMullionLocation.TabIndex = 68;
      this.lblMullionLocation.Text = "lblMullionLocation";
      // 
      // txtMullionLocationX
      // 
      this.txtMullionLocationX.Location = new System.Drawing.Point(153, 264);
      this.txtMullionLocationX.Name = "txtMullionLocationX";
      this.txtMullionLocationX.Size = new System.Drawing.Size(84, 27);
      this.txtMullionLocationX.TabIndex = 67;
      this.txtMullionLocationX.Text = "txtMullionLocationX";
      // 
      // lblUnitWidth
      // 
      this.lblUnitWidth.Location = new System.Drawing.Point(246, 174);
      this.lblUnitWidth.Name = "lblUnitWidth";
      this.lblUnitWidth.Size = new System.Drawing.Size(49, 30);
      this.lblUnitWidth.TabIndex = 66;
      this.lblUnitWidth.Text = "lblUnitWidth";
      // 
      // lblUnitHeight
      // 
      this.lblUnitHeight.Location = new System.Drawing.Point(246, 144);
      this.lblUnitHeight.Name = "lblUnitHeight";
      this.lblUnitHeight.Size = new System.Drawing.Size(49, 30);
      this.lblUnitHeight.TabIndex = 65;
      this.lblUnitHeight.Text = "lblUnitHeight";
      // 
      // txtWidth
      // 
      this.txtWidth.Location = new System.Drawing.Point(153, 174);
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new System.Drawing.Size(84, 27);
      this.txtWidth.TabIndex = 64;
      this.txtWidth.Text = "txtWidth";
      // 
      // txtHeight
      // 
      this.txtHeight.Location = new System.Drawing.Point(153, 144);
      this.txtHeight.Name = "txtHeight";
      this.txtHeight.Size = new System.Drawing.Size(84, 27);
      this.txtHeight.TabIndex = 63;
      this.txtHeight.Text = "txtHeight";
      // 
      // lblWidth
      // 
      this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblWidth.Location = new System.Drawing.Point(43, 174);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new System.Drawing.Size(104, 30);
      this.lblWidth.TabIndex = 62;
      this.lblWidth.Text = "lblWidth";
      // 
      // lblHeight
      // 
      this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblHeight.Location = new System.Drawing.Point(43, 144);
      this.lblHeight.Name = "lblHeight";
      this.lblHeight.Size = new System.Drawing.Size(104, 30);
      this.lblHeight.TabIndex = 61;
      this.lblHeight.Text = "lblHeight";
      // 
      // lblSelectSide
      // 
      this.lblSelectSide.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblSelectSide.Location = new System.Drawing.Point(43, 237);
      this.lblSelectSide.Name = "lblSelectSide";
      this.lblSelectSide.Size = new System.Drawing.Size(94, 30);
      this.lblSelectSide.TabIndex = 60;
      this.lblSelectSide.Text = "lblSelectSide";
      // 
      // txtSelectSide
      // 
      this.txtSelectSide.Location = new System.Drawing.Point(153, 234);
      this.txtSelectSide.Name = "txtSelectSide";
      this.txtSelectSide.Size = new System.Drawing.Size(84, 27);
      this.txtSelectSide.TabIndex = 59;
      this.txtSelectSide.Text = "txtSelectSide";
      // 
      // lblProfileUnit
      // 
      this.lblProfileUnit.Location = new System.Drawing.Point(246, 207);
      this.lblProfileUnit.Name = "lblProfileUnit";
      this.lblProfileUnit.Size = new System.Drawing.Size(69, 30);
      this.lblProfileUnit.TabIndex = 58;
      this.lblProfileUnit.Text = "lblProfileUnit";
      // 
      // lblProfileSize
      // 
      this.lblProfileSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblProfileSize.Location = new System.Drawing.Point(43, 207);
      this.lblProfileSize.Name = "lblProfileSize";
      this.lblProfileSize.Size = new System.Drawing.Size(94, 30);
      this.lblProfileSize.TabIndex = 57;
      this.lblProfileSize.Text = "lblProfileSize";
      // 
      // txtProfileSize
      // 
      this.txtProfileSize.Location = new System.Drawing.Point(153, 204);
      this.txtProfileSize.Name = "txtProfileSize";
      this.txtProfileSize.Size = new System.Drawing.Size(84, 27);
      this.txtProfileSize.TabIndex = 56;
      this.txtProfileSize.Text = "txtProfileSize";
      // 
      // lblUnitDiametr
      // 
      this.lblUnitDiametr.Location = new System.Drawing.Point(246, 555);
      this.lblUnitDiametr.Name = "lblUnitDiametr";
      this.lblUnitDiametr.Size = new System.Drawing.Size(69, 30);
      this.lblUnitDiametr.TabIndex = 55;
      this.lblUnitDiametr.Text = "lblUnitDiametr";
      // 
      // lblDiametr
      // 
      this.lblDiametr.Location = new System.Drawing.Point(43, 555);
      this.lblDiametr.Name = "lblDiametr";
      this.lblDiametr.Size = new System.Drawing.Size(94, 30);
      this.lblDiametr.TabIndex = 54;
      this.lblDiametr.Text = "lblDiametr";
      // 
      // lblSide
      // 
      this.lblSide.Location = new System.Drawing.Point(43, 519);
      this.lblSide.Name = "lblSide";
      this.lblSide.Size = new System.Drawing.Size(108, 30);
      this.lblSide.TabIndex = 53;
      this.lblSide.Text = "lblSide";
      // 
      // txtDiameter
      // 
      this.txtDiameter.Location = new System.Drawing.Point(153, 555);
      this.txtDiameter.Name = "txtDiameter";
      this.txtDiameter.Size = new System.Drawing.Size(84, 27);
      this.txtDiameter.TabIndex = 52;
      this.txtDiameter.Text = "txtDiameter";
      // 
      // txtSides
      // 
      this.txtSides.Location = new System.Drawing.Point(153, 519);
      this.txtSides.Name = "txtSides";
      this.txtSides.Size = new System.Drawing.Size(84, 27);
      this.txtSides.TabIndex = 51;
      this.txtSides.Text = "txtSlides";
      // 
      // lblUnitVertical
      // 
      this.lblUnitVertical.Location = new System.Drawing.Point(246, 114);
      this.lblUnitVertical.Name = "lblUnitVertical";
      this.lblUnitVertical.Size = new System.Drawing.Size(49, 30);
      this.lblUnitVertical.TabIndex = 50;
      this.lblUnitVertical.Text = "lblUnitVertical";
      // 
      // lblUnitHorizontal
      // 
      this.lblUnitHorizontal.Location = new System.Drawing.Point(246, 84);
      this.lblUnitHorizontal.Name = "lblUnitHorizontal";
      this.lblUnitHorizontal.Size = new System.Drawing.Size(49, 30);
      this.lblUnitHorizontal.TabIndex = 49;
      this.lblUnitHorizontal.Text = "lblUnitHorizontal";
      // 
      // txtMarginV
      // 
      this.txtMarginV.Location = new System.Drawing.Point(153, 114);
      this.txtMarginV.Name = "txtMarginV";
      this.txtMarginV.Size = new System.Drawing.Size(84, 27);
      this.txtMarginV.TabIndex = 48;
      this.txtMarginV.Text = "txtMarginV";
      // 
      // txtMarginH
      // 
      this.txtMarginH.Location = new System.Drawing.Point(153, 84);
      this.txtMarginH.Name = "txtMarginH";
      this.txtMarginH.Size = new System.Drawing.Size(84, 27);
      this.txtMarginH.TabIndex = 47;
      this.txtMarginH.Text = "txtMarginH";
      // 
      // lblMarginVertical
      // 
      this.lblMarginVertical.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblMarginVertical.Location = new System.Drawing.Point(43, 114);
      this.lblMarginVertical.Name = "lblMarginVertical";
      this.lblMarginVertical.Size = new System.Drawing.Size(104, 30);
      this.lblMarginVertical.TabIndex = 46;
      this.lblMarginVertical.Text = "lblMarginVertical";
      // 
      // lblMarginHorizontal
      // 
      this.lblMarginHorizontal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.lblMarginHorizontal.Location = new System.Drawing.Point(43, 87);
      this.lblMarginHorizontal.Name = "lblMarginHorizontal";
      this.lblMarginHorizontal.Size = new System.Drawing.Size(104, 30);
      this.lblMarginHorizontal.TabIndex = 45;
      this.lblMarginHorizontal.Text = "lblMarginHorizontal";
      // 
      // pnlDraw
      // 
      this.pnlDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlDraw.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.pnlDraw.Controls.Add(this.pnlCanvas);
      this.pnlDraw.Location = new System.Drawing.Point(25, 12);
      this.pnlDraw.Name = "pnlDraw";
      this.pnlDraw.Size = new System.Drawing.Size(700, 716);
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
      this.pnlCanvas.Cursor = System.Windows.Forms.Cursors.Cross;
      this.pnlCanvas.Location = new System.Drawing.Point(25, 42);
      this.pnlCanvas.Name = "pnlCanvas";
      this.pnlCanvas.Size = new System.Drawing.Size(650, 649);
      this.pnlCanvas.TabIndex = 0;
      // 
      // txtMullionLocationY
      // 
      this.txtMullionLocationY.Location = new System.Drawing.Point(250, 264);
      this.txtMullionLocationY.Name = "txtMullionLocationY";
      this.txtMullionLocationY.Size = new System.Drawing.Size(84, 27);
      this.txtMullionLocationY.TabIndex = 75;
      this.txtMullionLocationY.Text = "txtMullionLocationY";
      // 
      // MainForm
      // 
      this.ClientSize = new System.Drawing.Size(1205, 750);
      this.Controls.Add(this.pnlAllForm);
      this.MaximumSize = new System.Drawing.Size(1300, 1201);
      this.MinimumSize = new System.Drawing.Size(923, 597);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Draw Shapes";
      this.pnlAllForm.ResumeLayout(false);
      this.pnlInput.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.pnlDraw.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlAllForm;
    private System.Windows.Forms.Panel pnlDraw;
    private System.Windows.Forms.Panel pnlCanvas;
    private Panel pnlInput;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private Button btnSetToCurve;
    private Button btnDrawAssembly;
    private Button btnCreateProject;
    private TabPage tabPage2;
    private Label lblSelectSide;
    private TextBox txtSelectSide;
    private Label lblProfileUnit;
    private Label lblProfileSize;
    private TextBox txtProfileSize;
    private Label lblUnitDiametr;
    private Label lblDiametr;
    private Label lblSide;
    private TextBox txtDiameter;
    private TextBox txtSides;
    private Label lblUnitVertical;
    private Label lblUnitHorizontal;
    private TextBox txtMarginV;
    private TextBox txtMarginH;
    private Label lblMarginVertical;
    private Label lblMarginHorizontal;
    private Label lblProjectName;
    private TextBox txtProjectName;
    private Button btnAddMullionVertical;
    private Label lblUnitWidth;
    private Label lblUnitHeight;
    private TextBox txtWidth;
    private TextBox txtHeight;
    private Label lblWidth;
    private Label lblHeight;
    private Label lblMullionLocation;
    private TextBox txtMullionLocationX;
        private Label lblMullionWidth;
        private TextBox txtMullionWidth;
    private Button btnRemoveSash;
    private Button btnAddSash;
    private Label lblCWidth_Mullion;
    private TextBox txtCWidth_Mullion;
    private Label lblCWidth_Profile;
    private TextBox txtCWidth_Profile;
        private Button btnAddMullionHorizontal;
    private TextBox txtMullionLocationY;
  }
}

