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

  public partial class MainForm : Form {

    private cDrawingAdapter mDrawingAdapter;
    private int mIndexOfMaxItem;                            //indeks maksymalnej wartości w tablicy
    private double mMarginV, mMarginH;                      //marginesy: Vertical, Horizontal - wartości pobrane z INPUT 
    private int mMaxValue;                                  //maksymalna wartość w tablicy
    private cPolygon mPolygon_Rect;                         //inicjacja polygonu prostokąta
    private cPolygon mPolygon_Regular;                      //inicjacja polygonu wieloboku foremnego
    private double mRect_Height, mRect_Width;               //wymiary prostokąta: wysokość, szerokość - wartości pobrane z INPUT 
    private float mCircleRadius;                            //promień prostokąta - wartość pobrana z INPUT 
    private double mScale;                                  //przeliczona skala całego rysunku

    public MainForm() {
      //

      InitializeComponent();

      //przypisanie konkretnych tekstów do okna programu głównego
      this.lblUnitVertical.Text = "px";
      this.lblUnitHorizontal.Text = "px";
      this.txtMarginV.Text = "40";
      this.txtMarginH.Text = "40";
      this.lblMarginVertical.Text = "Margines V:";
      this.lblMarginHorizontal.Text = "Margines H:";
      this.btnDrawRect.Text = "Rysuj Wielokąt";
      this.lblUnitH.Text = "mm";
      this.lblUnitW.Text = "mm";
      this.txtHeight.Text = "50";
      this.txtWidth.Text = "50";
      this.lblHeight.Text = "Wysokość:";
      this.lblWidth.Text = "Szerokość:";
      this.txtDiameter.Text = "50";
      this.lblDiametr.Text = "Średnica:";
      this.lblSide.Text = "Ilość boków:";
      this.txtSides.Text = "10";
      this.lblUnitDiametr.Text = "mm";
      this.txtSelectSide.Text = "1";
      this.lblSelectSide.Text = "Bok numer:";
      this.btnSetToCurve.Text = "Rysuj Łuk";
      this.btnDrawWindow.Text = "Rysuj Okno";

      //włączenie funkcji blokującej wpisanie liter i znaków szczególnych w wybranych polach
      this.txtMarginV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMarginH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSides.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSelectSide.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);

      this.btnDrawRect.Click += new System.EventHandler(this.btnCreateRect_Click);                     //uruchomienie funkcji rysuj prostokąt
      this.btnSetToCurve.Click += new System.EventHandler(this.btnSetToCurve_Click);                 //uruchomienie funkcji rysuj prostokąt
      this.btnDrawWindow.Click += new System.EventHandler(this.btnCreateRegularPolygon_Click); //uruchomienie funkcji rysuj prostokąt
      this.Resize += new System.EventHandler(this.MainForm_Resize);                                  //odświeżanie canvas przy powiększaniu programu
      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);      //

      mDrawingAdapter = new cDrawingAdapter();

    }

    public void pnlCanvas_Paint(object sender, PaintEventArgs e) {
      //funkcja rysująca polygon 

      double pTransfBasePtX, pTransfBasePtY;

      mRect_Height = double.Parse(txtHeight.Text);
      mRect_Width = double.Parse(txtWidth.Text);
      mCircleRadius = float.Parse(txtDiameter.Text) / 2;
      mMarginV = double.Parse(txtMarginV.Text);
      mMarginH = double.Parse(txtMarginH.Text);

      /*                    if (mPolygon_Rect == null)
                            return;



                          //obliczanie współrzędnych na Canvas punktu bazowego prostokąta
                          pTransfBasePtX = mMarginH + (pnlCanvas.Width - 2 * mMarginH - mRect_Width * mScale) / 2;
                          pTransfBasePtY = pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - mRect_Height * mScale) / 2);

                          mDrawingAdapter.DrawPolygon(mPolygon_Rect, mScale, pTransfBasePtX, pTransfBasePtY, e);*/

      if (mPolygon_Regular == null)
        return;

      cDrawing.CircleCenter = new cPoint(new PointF(mCircleRadius, mCircleRadius));

      //obliczanie współrzędnych na Canvas punktu bazowego wieloboku foremnego
      pTransfBasePtX = mMarginH + (pnlCanvas.Width - 2 * mMarginH - 2 * mCircleRadius * mScale) / 2;
      pTransfBasePtY = pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - 2 * mCircleRadius * mScale) / 2);

      mDrawingAdapter.DrawPolygon(mPolygon_Regular, mScale, pTransfBasePtX, pTransfBasePtY, e);

    }

    private void btnCreateRect_Click(object sender, EventArgs e) {
      //funkca tworząca poligony

      CalculateScale();

      CreatePolygons();

      this.pnlCanvas.Refresh();

    }

    private void btnCreateRegularPolygon_Click(object sender, EventArgs e) {
      //funkca tworząca poligon wielokąta foremnego

      this.pnlCanvas.Refresh();

    }

    private void btnSetToCurve_Click(object sender, EventArgs e) {
      //funkcja zmieniająca segment w łuk

      mPolygon_Regular.SetSegmentToCurve(int.Parse(txtSelectSide.Text));

      this.pnlCanvas.Refresh();

    }

    private void MainForm_Resize(object sender, EventArgs e) {
      //funkcja odświerzająca rysunek przy resize

      CalculateScale();

      this.pnlCanvas.Refresh();

    }

    private void CreatePolygons() {
      //funkcja tworząca poligony: 
      // 1) mPolygon_Rect - prostokąt
      // 2) mPolygon_Regular - wielobok foremny

      double pBaseAngle;                                    //kąt pomiędzy: punkt1 segmentu - środek koła - punkt2 segmentu
      int mSidesNumber;                                     //liczba boków figury foremnej

      mRect_Height = double.Parse(txtHeight.Text);
      mRect_Width = double.Parse(txtWidth.Text);
      mCircleRadius = float.Parse(txtDiameter.Text) / 2;

      // mPolygon_Rect = cPolygonFactory.GetPolygon_Rect((int)mRect_Width, (int)mRect_Height);

      mSidesNumber = int.Parse(txtSides.Text);
      pBaseAngle = 360 / mSidesNumber;

      mPolygon_Regular = cPolygonFactory.GetPolygon_Regular((int)mCircleRadius, -pBaseAngle);

    }

    private void CheckKeyPress(object sender, KeyPressEventArgs e) {
      //funkcja blokująca wpisanie liter i znaków specjalnych do pól z "menu" programu głównego

      //pętla blokująca alfabet
      for (int h = 58; h <= 127; h++) {
        if (e.KeyChar == h) {
          e.Handled = true;

        }

      }

      //pętla blokująca znaki szczególne
      for (int k = 32; k <= 47; k++) {
        if (e.KeyChar == k) {
          e.Handled = true;

        }

      }

    }

    public int TakeMaxValueOfArrey(int[] xArray) {
      //funkcja zwracająca index maxymalnej wartość z wybranej tablicy
      //xArray - tablica zawierająca dane do porównania

      mMaxValue = xArray.Max();
      mIndexOfMaxItem = xArray.ToList().IndexOf(mMaxValue);

      return mIndexOfMaxItem;

    }

    public double CalculateScale() {
      //funkcja obliczająca skale według największej wartości INPUT

      int[] mArrayOfInput = { };                            //tablica wsystkich INPUT do liczenia skali

      mArrayOfInput = mArrayOfInput.Concat(new int[] { int.Parse(txtWidth.Text) }).ToArray();
      mArrayOfInput = mArrayOfInput.Concat(new int[] { int.Parse(txtHeight.Text) }).ToArray();
      mArrayOfInput = mArrayOfInput.Concat(new int[] { int.Parse(txtDiameter.Text) }).ToArray();

      TakeMaxValueOfArrey(mArrayOfInput);

      //ustawiam pScale według rozmiaru pola pnlCanvas
      if (mIndexOfMaxItem == 0 && txtMarginH.Text == txtMarginV.Text) {                               //txtWidth jest największe
        if (pnlCanvas.Height >= pnlCanvas.Width) {
          mScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / mMaxValue;

        } else if (pnlCanvas.Height <= pnlCanvas.Width) {
          mScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text) * 2) / mMaxValue;

        }
      } else if (mIndexOfMaxItem == 0 && txtMarginH.Text != txtMarginV.Text) {                          //txtWidth jest największe
        mScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / mMaxValue;

      } else if (mIndexOfMaxItem == 1) {                                                                //txtHeight jest największe
        mScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text) * 2) / mMaxValue;

      } else if (mIndexOfMaxItem == 2) {                                                                //txtDiameter jest największe
        if (pnlCanvas.Height >= pnlCanvas.Width) {
          mScale = ((double)pnlCanvas.Width - int.Parse(txtMarginH.Text) * 2) / mMaxValue;

        } else if (pnlCanvas.Height <= pnlCanvas.Width) {
          mScale = ((double)pnlCanvas.Height - int.Parse(txtMarginV.Text) * 2) / mMaxValue;

        }
      } else {                                                                                          //kontrola skalowania
        Console.WriteLine("Error in Calculate Scale. Check it!!!");

      }

      mArrayOfInput = new int[mArrayOfInput.Length - 3];                                              //czyszczę tablicę

      return mScale;

    }

  }
}
