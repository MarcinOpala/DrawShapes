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
  //cPolygon polygon;
    cPolygon polygon;
    public cPolygonFactory factory;

  public string width;
  public string height;
  public string marginV;
  public string marginH;
  private static float x;
  private static float y;
   public static PointF point = new PointF(x, y);
  public cPoint o = new cPoint(point);
  public string diametr;
  public string slides;


  public int pMaxValue;                                   //maksymalna wartość w tablicy
  public int pIndexOfMaxItem;                             //indeks maksymalnej wartości w tablicy
  public int[] pArrayOfInput = { };                       //tablica wsystkich INPUT do liczenia skali
  public double pScale;                                   //przeliczona skala całego rysunku

  public MainForm(cPolygonFactory factor, cPolygon polygon) {
  //

  //this.polygon = polygon;
      this.polygon = polygon;
      factory = factor;
  InitializeComponent();
          
  //przypisanie konkretnych tekstów do okna programu głównego
  this.lblUnitVertical.Text = "px";
  this.lblUnitHorizontal.Text = "px";
  this.txtMarginV.Text = "40";
  this.txtMarginH.Text = "40";
  this.lblMarginVertical.Text = "Margines V:";
  this.lblMarginHorizontal.Text = "Margines H:";
  this.btnDrawRectangle.Text = "Rysuj Prostokąt";
  this.lblUnitH.Text = "mm";
  this.lblUnitW.Text = "mm";
  this.txtHeight.Text = "50";
  this.txtWidth.Text = "50";
  this.lblHeight.Text = "Wysokość:";
  this.lblWidth.Text = "Szerokość:";
  this.txtDiameter.Text = "50";
  this.lblDiametr.Text = "Średnica:";
  this.lblSide.Text = "Ilość boków:";
  this.txtSlides.Text = "10";
  this.lblUnitDiametr.Text = "mm";
  this.txtSelectSide.Text = "1";
  this.lblSelectSide.Text = "Bok numer:";
  this.btnDrawArc.Text = "Rysuj Łuk";
  this.btnDrawRegularPolygon.Text = "Rysuj Wielokąt";

  //włączenie funkcji blokującej wpisanie liter i znaków szczególnych w wybranych polach
  this.txtMarginV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
  this.txtMarginH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
  this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
  this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
  this.txtSlides.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
  this.txtDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);
  this.txtSelectSide.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onlyNumberKeyPress);

  this.btnDrawRectangle.Click += new System.EventHandler(this.btnDraw_Click);        //uruchomienie funkcji rysuj prostokąt
  this.btnDrawArc.Click += new System.EventHandler(this.btnDrawArc_Click);               //uruchomienie funkcji rysuj prostokąt
  this.btnDrawRegularPolygon.Click += new System.EventHandler(this.btnDrawRegularPolygon_Click);   //uruchomienie funkcji rysuj prostokąt

  this.ResizeEnd += new System.EventHandler(this.CalibrationForm_Resize);   //odświeżanie canvas po zakończeniu powiększania programu
  this.Resize += new System.EventHandler(this.CalibrationForm_Resize);      //odświeżanie canvas przy powiększaniu programu


  }



    private void CalibrationForm_Resize(object sender, EventArgs e) {
      //funkcja przypisująca dane z "menu"    

      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
      CalculateScale();
      this.pnlCanvas.Refresh();
    }

  public void pnlCanvas_Paint(object sender, PaintEventArgs e) {
    //funkcja przekazująca dane z "menu" głównego do rysowania figury na obszarze canvas

      // do opisania!!!!  rysujemy prostyokąt, to edit!!!

      double width = double.Parse(txtWidth.Text) * (pScale);
      double height = double.Parse(txtHeight.Text) * (pScale);
      double radius = double.Parse(txtDiameter.Text)/2 * (pScale);
      double angle = 360 / int.Parse(txtSlides.Text);


      //to edit
      o.x = (pnlCanvas.Width - (int)width) / 2;
      o.y = (pnlCanvas.Height - (int)height)/2 + (int)height;
      factory.CreateRectangle(o, (int)width, (int)height);                  // (cPoint xPoint, int xWidth, int xHeight)


      // To edit!!
      o.x = pnlCanvas.Width / 2;
      o.y = pnlCanvas.Height / 2;
      factory.CreateRegularPolygon(o, (int)radius, -angle);
      polygon.DrawRectangle(e);

      CalculateScale();
    }

  private void onlyNumberKeyPress(object sender, KeyPressEventArgs e)  {
    //funkcja blokująca wpisanie liter i znaków specjalnych do pól z "menu" programu głównego

    //58 to 127 is alphabets that will be blocked
    for (int h = 58; h <= 127; h++) {
      if (e.KeyChar == h) {
        e.Handled = true;
      }
    }

    //32 to 47 are special characters that will be blocked
    for (int k = 32; k <= 47; k++) {
      if (e.KeyChar == k) {
        e.Handled = true;
      }
    }
  }
   
  private void btnDraw_Click(object sender, EventArgs e) {
  //funkca wywołująca narysowanie figury w obszarze canvas

      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
      CalculateScale();
      this.pnlCanvas.Refresh();
  }

  private void btnDrawRegularPolygon_Click(object sender, EventArgs e)
  {
    throw new NotImplementedException();
  }

  private void btnDrawArc_Click(object sender, EventArgs e)
  {
    throw new NotImplementedException();
  }

  public int TakeMaxValueOfArrey(int [] xArray) {
    //funkcja zwracająca index maxymalnej wartość z wybranej tablicy
    //xArray - tablica zawierająca dane do porównania

    pMaxValue = xArray.Max();
    pIndexOfMaxItem = xArray.ToList().IndexOf(pMaxValue);
    return pIndexOfMaxItem;
  }

  public double CalculateScale() {
    //funkcja obliczająca skale według największej wartości INPUT
      
    pArrayOfInput = pArrayOfInput.Concat(new int[] { int.Parse(txtWidth.Text) }).ToArray();
    pArrayOfInput = pArrayOfInput.Concat(new int[] { int.Parse(txtHeight.Text) }).ToArray();
    pArrayOfInput = pArrayOfInput.Concat(new int[] { int.Parse(txtDiameter.Text) }).ToArray();
    TakeMaxValueOfArrey(pArrayOfInput);

    //ustawiam pScale według rozmiaru pola pnlCanvas
    if (pIndexOfMaxItem == 0 && txtMarginH.Text == txtMarginV.Text) {                           //txtWidth jest największe
        if (pnlCanvas.Height >= pnlCanvas.Width)
        {
          pScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / pMaxValue;
        }
        else if (pnlCanvas.Height <= pnlCanvas.Width)
        {
          pScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text) * 2) / pMaxValue;
        }
        
    }
    else if (pIndexOfMaxItem == 0 && txtMarginH.Text != txtMarginV.Text) {                           //txtWidth jest największe
      pScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / pMaxValue;
    }
    else if (pIndexOfMaxItem == 1) {                      //txtHeight jest największe
      pScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text)*2)/ pMaxValue;
    }
    else if (pIndexOfMaxItem == 2) {                      //txtDiameter jest największe
      if (pnlCanvas.Height >= pnlCanvas.Width) {        
        pScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text)*2)/ pMaxValue;
      }
      else if (pnlCanvas.Height <= pnlCanvas.Width) {
        pScale =  ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text)*2) / pMaxValue;
      }
    }
    else {                                                //kontrola skalowania
      Console.WriteLine("Error in Calculate Scale. Check it!!!");
    }
    pArrayOfInput = new int[pArrayOfInput.Length - 3];    //czyszczę tablicę

    return pScale;
  }


}
}
