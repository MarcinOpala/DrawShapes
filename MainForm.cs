using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape {

  public partial class MainForm : Form{

    private int[] mArrayOfInput = { };                       //tablica wsystkich INPUT do liczenia skali
    private double mBaseAngle;                                   //kąt pomiędzy: punkt1 segmentu - środek koła - punkt2 segmentu
    private cPolygonFactory mFactory;
    private double mHeightOfRectangle;                                  //przeskalowana wysokość prostokąta
    private int mIndexOfMaxItem;                             //indeks maksymalnej wartości w tablicy
    private int mMaxValue;                                   //maksymalna wartość w tablicy
    private static PointF mPoint;                            //inicjacja zmiennej pomocniczej
    private cPolygon mPolygon;
    private double mRadiusOfCircle;                                  //przeskalowany promień koła
    private double mScale;                                   //przeliczona skala całego rysunku
    private double mWidthOfRectangle;                                   //przeskalowana szerokość prostokąta
      
    public MainForm(cPolygonFactory xFactory, cPolygon xPolygon) {
      //

      this.mFactory = xFactory;
      this.mPolygon = xPolygon;
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
      this.txtMarginV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberKeyPress);
      this.txtMarginH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberKeyPress);
      this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberKeyPress);
      this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberKeyPress);
      this.txtSlides.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberKeyPress);
      this.txtDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberKeyPress);
      this.txtSelectSide.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumberKeyPress);

      this.btnDrawRectangle.Click += new System.EventHandler(this.btnDrawRectangle_Click);                //uruchomienie funkcji rysuj prostokąt
      this.btnDrawArc.Click += new System.EventHandler(this.btnDrawArc_Click);                            //uruchomienie funkcji rysuj prostokąt
      this.btnDrawRegularPolygon.Click += new System.EventHandler(this.btnDrawRegularPolygon_Click);      //uruchomienie funkcji rysuj prostokąt
      this.ResizeEnd += new System.EventHandler(this.CalibrationForm_Resize);                             //odświeżanie canvas po zakończeniu powiększania programu
      this.Resize += new System.EventHandler(this.CalibrationForm_Resize);                                //odświeżanie canvas przy powiększaniu programu
    }

    public void pnlCanvas_PaintRectangle(object sender, PaintEventArgs e) {
      //funkcja rysująca prostokąt 

      mPolygon.DrawRectangle(e);
    }

    public void pnlCanvas_PaintRegularPolygon(object sender, PaintEventArgs e)  {
      //funkcja rysująca wielokąt formeny
         
      mPolygon.DrawRegularPolygon(e);
    }

    private void btnDrawRectangle_Click(object sender, EventArgs e) {
      //funkca wywołująca narysowanie prostokąta w obszarze canvas

      CalculateScale();
      cPoint pStartPoint = new cPoint(mPoint);
      mWidthOfRectangle = double.Parse(txtWidth.Text) * (mScale);
      mHeightOfRectangle = double.Parse(txtHeight.Text) * (mScale);
      pStartPoint.X = (pnlCanvas.Width - (int)mWidthOfRectangle) / 2;
      pStartPoint.Y = (pnlCanvas.Height - (int)mHeightOfRectangle) / 2 + (int)mHeightOfRectangle;
      cDrawingAdapter.IsSelected = int.Parse(txtSelectSide.Text);
      mFactory.CreateRectangle(pStartPoint, (int)mWidthOfRectangle, (int)mHeightOfRectangle);                                    // (cPoint xPoint, int xWidth, int xHeight)

      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_PaintRectangle);
      this.pnlCanvas.Refresh();
    }

    private void btnDrawRegularPolygon_Click(object sender, EventArgs e) {
      //funkca wywołująca narysowanie wielokąta foremnego w obszarze canvas

      CalculateScale();
      cPoint pCircleCenter = new cPoint(mPoint);
      pCircleCenter.X = pnlCanvas.Width / 2;
      pCircleCenter.Y = pnlCanvas.Height / 2;
      mRadiusOfCircle = double.Parse(txtDiameter.Text) / 2 * (mScale);
      mBaseAngle = 360 / int.Parse(txtSlides.Text);
      cDrawingAdapter.IsSelected = int.Parse(txtSelectSide.Text);

      cDrawingAdapter.HelperPoint = pCircleCenter;
      mFactory.CreateRegularPolygon(pCircleCenter, (int)mRadiusOfCircle, -mBaseAngle);

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

    private void OnlyNumberKeyPress(object sender, KeyPressEventArgs e) {
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

      mMaxValue = xArray.Max();
      mIndexOfMaxItem = xArray.ToList().IndexOf(mMaxValue);
      return mIndexOfMaxItem;
    }

    public double CalculateScale() {
      //funkcja obliczająca skale według największej wartości INPUT
      
      mArrayOfInput = mArrayOfInput.Concat(new int[] { int.Parse(txtWidth.Text) }).ToArray();
      mArrayOfInput = mArrayOfInput.Concat(new int[] { int.Parse(txtHeight.Text) }).ToArray();
      mArrayOfInput = mArrayOfInput.Concat(new int[] { int.Parse(txtDiameter.Text) }).ToArray();
      TakeMaxValueOfArrey(mArrayOfInput);

      //ustawiam pScale według rozmiaru pola pnlCanvas
      if (mIndexOfMaxItem == 0 && txtMarginH.Text == txtMarginV.Text) {                               //txtWidth jest największe
        if (pnlCanvas.Height >= pnlCanvas.Width) {
          mScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / mMaxValue;
        }
        else if (pnlCanvas.Height <= pnlCanvas.Width) {
          mScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text) * 2) / mMaxValue;
        }
      }

      else if (mIndexOfMaxItem == 0 && txtMarginH.Text != txtMarginV.Text) {                          //txtWidth jest największe
        mScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / mMaxValue;
      }
      else if (mIndexOfMaxItem == 1) {                                                                //txtHeight jest największe
        mScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text)*2)/ mMaxValue;
      }
      else if (mIndexOfMaxItem == 2) {                                                                //txtDiameter jest największe
        if (pnlCanvas.Height >= pnlCanvas.Width) {        
          mScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text)*2)/ mMaxValue;
        }
        else if (pnlCanvas.Height <= pnlCanvas.Width) {
          mScale =  ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text)*2) / mMaxValue;
        }
      }
      else {                                                                                          //kontrola skalowania
        Console.WriteLine("Error in Calculate Scale. Check it!!!");
      }
      mArrayOfInput = new int[mArrayOfInput.Length - 3];                                              //czyszczę tablicę

      return mScale;
    }

  }
}
