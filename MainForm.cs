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
  public partial class MainForm : Form{

    cPolygon pPolygon;
    cPolygonFactory pFactory;
    public static PointF pPoint = new PointF();               //inicjacja zmiennej pomocniczej
    public int pMaxValue;                                     //maksymalna wartość w tablicy
    public int pIndexOfMaxItem;                               //indeks maksymalnej wartości w tablicy
    public int[] pArrayOfInput = { };                         //tablica wsystkich INPUT do liczenia skali
    public double pScale;                                     //przeliczona skala całego rysunku
    public double pWidth;                                     //przeskalowana szerokość prostokąta
    public double pHeight;                                    //przeskalowana wysokość prostokąta
    public double pRadius;                                    //przeskalowany promień koła
    public double pAngle;                                     //kąt pomiędzy: punkt1 segmentu - środek koła - punkt2 segmentu

    public MainForm(cPolygonFactory xFactory, cPolygon xPolygon) {
      //

      this.pPolygon = xPolygon;
      this.pFactory = xFactory;
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

      this.btnDrawRectangle.Click += new System.EventHandler(this.btnDrawRectangle_Click);                //uruchomienie funkcji rysuj prostokąt
      this.btnDrawArc.Click += new System.EventHandler(this.btnDrawArc_Click);                            //uruchomienie funkcji rysuj prostokąt
      this.btnDrawRegularPolygon.Click += new System.EventHandler(this.btnDrawRegularPolygon_Click);      //uruchomienie funkcji rysuj prostokąt
      this.ResizeEnd += new System.EventHandler(this.CalibrationForm_Resize);                             //odświeżanie canvas po zakończeniu powiększania programu
      this.Resize += new System.EventHandler(this.CalibrationForm_Resize);                                //odświeżanie canvas przy powiększaniu programu
    }

    public void pnlCanvas_PaintRectangle(object sender, PaintEventArgs e) {
      //funkcja rysująca prostokąt 

      pPolygon.DrawRectangle(e);
    }

    public void pnlCanvas_PaintRegularPolygon(object sender, PaintEventArgs e)  {
      //funkcja rysująca wielokąt formeny
         
      pPolygon.DrawRegularPolygon(e);
    }

    private void btnDrawRectangle_Click(object sender, EventArgs e) {
      //funkca wywołująca narysowanie prostokąta w obszarze canvas

      CalculateScale();
      cPoint pStartPoint = new cPoint(pPoint);
      pWidth = double.Parse(txtWidth.Text) * (pScale);
      pHeight = double.Parse(txtHeight.Text) * (pScale);
      pStartPoint.x = (pnlCanvas.Width - (int)pWidth) / 2;
      pStartPoint.y = (pnlCanvas.Height - (int)pHeight) / 2 + (int)pHeight;
      pPolygon.pIsSelected = int.Parse(txtSelectSide.Text);
      pFactory.CreateRectangle(pStartPoint, (int)pWidth, (int)pHeight);                                    // (cPoint xPoint, int xWidth, int xHeight)

      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_PaintRectangle);
      this.pnlCanvas.Refresh();
    }

    private void btnDrawRegularPolygon_Click(object sender, EventArgs e) {
      //funkca wywołująca narysowanie wielokąta foremnego w obszarze canvas

      CalculateScale();
      cPoint pCircleCenter = new cPoint(pPoint);
      pCircleCenter.x = pnlCanvas.Width / 2;
      pCircleCenter.y = pnlCanvas.Height / 2;
      pRadius = double.Parse(txtDiameter.Text) / 2 * (pScale);
      pAngle = 360 / int.Parse(txtSlides.Text);
      pPolygon.pIsSelected = int.Parse(txtSelectSide.Text);
      pFactory.CreateRegularPolygon(pCircleCenter, (int)pRadius, -pAngle);

      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_PaintRegularPolygon);
      this.pnlCanvas.Refresh();
    }

    private void btnDrawArc_Click(object sender, EventArgs e)  {
      //funkcja wywołująca narysowanie łuku


    }

    private void CalibrationForm_Resize(object sender, EventArgs e)
    {
      //funkcja odświerzająca rysunek przy resize 
      CalculateScale();
      btnDrawRectangle_Click(sender, e);
      btnDrawRegularPolygon_Click(sender, e);
    }
    private void onlyNumberKeyPress(object sender, KeyPressEventArgs e) {
      //funkcja blokująca wpisanie liter i znaków specjalnych do pól z "menu" programu głównego

      //58 to 127 is alphabets that will be blocked
      for (int h = 58; h <= 127; h++)    {
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
      if (pIndexOfMaxItem == 0 && txtMarginH.Text == txtMarginV.Text) {                               //txtWidth jest największe
        if (pnlCanvas.Height >= pnlCanvas.Width) {
          pScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / pMaxValue;
        }
        else if (pnlCanvas.Height <= pnlCanvas.Width) {
          pScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text) * 2) / pMaxValue;
        }
      }

      else if (pIndexOfMaxItem == 0 && txtMarginH.Text != txtMarginV.Text) {                          //txtWidth jest największe
        pScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / pMaxValue;
      }
      else if (pIndexOfMaxItem == 1) {                                                                //txtHeight jest największe
        pScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text)*2)/ pMaxValue;
      }
      else if (pIndexOfMaxItem == 2) {                                                                //txtDiameter jest największe
        if (pnlCanvas.Height >= pnlCanvas.Width) {        
          pScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text)*2)/ pMaxValue;
        }
        else if (pnlCanvas.Height <= pnlCanvas.Width) {
          pScale =  ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text)*2) / pMaxValue;
        }
      }
      else {                                                                                          //kontrola skalowania
        Console.WriteLine("Error in Calculate Scale. Check it!!!");
      }
      pArrayOfInput = new int[pArrayOfInput.Length - 3];                                              //czyszczę tablicę

      return pScale;
    }
  }
}
