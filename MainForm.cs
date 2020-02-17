using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape
{
    public partial class MainForm : Form
    {
        cPolygon polygon;

        public string width;
        public string height;
        public string marginV;
        public string marginH;
        private static int x;
        private static int y;
        public Point o = new Point(x, y);
        public string diametr;
        public string slides;
     
        public MainForm(cPolygon polygon)
        {
            this.polygon = polygon;
            InitializeComponent();

            this.lblUnitVertical.Text = "px";
            this.lblUnitHorizontal.Text = "px";
            this.txtMarginV.Text = "40";
            this.txtMarginH.Text = "40";
            this.lblMarginVertical.Text = "Margines V:";
            this.lblMarginHorizontal.Text = "Margines H:";
            this.btnDraw.Text = "Rysuj Prostokąt";
            this.lblUnitH.Text = "mm";
            this.lblUnitW.Text = "mm";
            this.txtHeight.Text = "0";
            this.txtWidth.Text = "0";
            this.lblHeight.Text = "Wysokość:";
            this.lblWidth.Text = "Szerokość:";
            this.txtDiameter.Text = "10";
            this.lblDiametr.Text = "Średnica:";
            this.lblSide.Text = "Ilość boków";
            this.txtSlides.Text = "10";
            this.lblUnitDiametr.Text = "mm";


            this.txtMarginV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
            this.txtMarginH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
            this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
            this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
            this.txtSlides.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
            this.txtDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);

            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            this.ResizeEnd += new System.EventHandler(this.CalibrationForm_Resize);
            this.Resize += new System.EventHandler(this.CalibrationForm_Resize);


        }









        private void CalibrationForm_Resize(object sender, EventArgs e)
        {
            width = txtWidth.Text;
            height = txtHeight.Text;
            marginV = txtMarginV.Text;
            marginH = txtMarginH.Text;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            this.pnlCanvas.Refresh();
        }

        public void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {

            diametr = txtDiameter.Text;
            slides = txtSlides.Text;
            o.x = pnlCanvas.Width / 2;
            o.y = pnlCanvas.Height / 2;

            polygon.drawRectangle(pnlCanvas, e, width, height, marginV, marginH);
            polygon.drawRegularPolygon(pnlCanvas, e, diametr, slides, o);
          
        }

        private void onlyNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            for (int h = 58; h <= 127; h++)      //58 to 127 is alphabets that will be blocked
            {
                if (e.KeyChar == h)             
                {
                    e.Handled = true;
                }
            }
            for (int k = 32; k <= 47; k++)       //32 to 47 are special characters that will be blocked
            {
                if (e.KeyChar == k)             
                {
                    e.Handled = true;
                }
            }
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            width = txtWidth.Text;
            height = txtHeight.Text;
            marginV = txtMarginV.Text;
            marginH = txtMarginH.Text;

            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            this.pnlCanvas.Refresh();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
