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

    private int mIndexOfMaxItem;                            //indeks maksymalnej wartości w tablicy
    private double mMarginV, mMarginH;                      //marginesy: Vertical, Horizontal - wartości pobrane z INPUT 
    private int mMaxValue;                                  //maksymalna wartość w tablicy
    private cPolygon mPolygon_Regular;                      //inicjacja polygonu wieloboku foremnego
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
      this.btnDrawRegularPolygon.Text = "Rysuj Wielokąt";
      this.lblUnitH.Text = "mm";
      this.lblUnitW.Text = "mm";
      this.txtHeight.Text = "250";
      this.txtWidth.Text = "250";
      this.lblHeight.Text = "Wysokość:";
      this.lblWidth.Text = "Szerokość:";
      this.txtDiameter.Text = "500";
      this.lblDiametr.Text = "Średnica:";
      this.lblSide.Text = "Ilość boków:";
      this.txtSides.Text = "4";
      this.lblUnitDiametr.Text = "mm";
      this.txtSelectSide.Text = "1";
      this.lblSelectSide.Text = "Bok numer:";
      this.btnSetToCurve.Text = "Rysuj Łuk";
      this.btnDrawProfile.Text = "Rysuj Profil";
      this.lblProfileUnit.Text = "mm";
      this.lblProfileSize.Text = "Szerokość profilu:";
      this.txtProfileSize.Text = "50";

      //włączenie funkcji blokującej wpisanie liter i znaków szczególnych w wybranych polach
      this.txtMarginV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMarginH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSides.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSelectSide.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtProfileSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);

      this.btnDrawRegularPolygon.Click += new System.EventHandler(this.btnCreateRegularPolygon_Click);//uruchomienie funkcji rysuj wielobok foremny
      this.btnSetToCurve.Click += new System.EventHandler(this.btnSetToCurve_Click);                  //uruchomienie funkcji rysuj Bezier
      this.btnDrawProfile.Click += new System.EventHandler(this.btnDrawProfile_Click);                //uruchomienie funkcji rysuj profil
      this.Resize += new System.EventHandler(this.MainForm_Resize);                                   //odświeżanie canvas przy powiększaniu programu
      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);       //rysowanie poligonu



    }

    public void pnlCanvas_Paint(object sender, PaintEventArgs e) {
      //funkcja rysująca polygon 

      cDrawingAdapter pDrawingAdapter;
      cDrawing pDrawing;
      cPoint pPt_Base;
      float pRadius;

      if (mPolygon_Regular == null)                         //jeśli nie istnieje poligon -> koniec
        return;

      pRadius = float.Parse(txtDiameter.Text) / 2;
      mMarginV = double.Parse(txtMarginV.Text);
      mMarginH = double.Parse(txtMarginH.Text);
           
      //obliczanie współrzędnych punktu bazowego wieloboku foremnego do wyświetlania na Canvas
      pPt_Base = new cPoint();
      pPt_Base.X = (float)(mMarginH + (pnlCanvas.Width - 2 * mMarginH - 2 * pRadius * mScale) / 2);
      pPt_Base.Y = (float)(pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - 2 * pRadius * mScale) / 2));

      pDrawingAdapter = new cDrawingAdapter();
      pDrawing = pDrawingAdapter.GetDrawing(mPolygon_Regular);

      pDrawing.Draw(mPolygon_Regular, mScale, pPt_Base, e);

    }

    private void btnCreateRegularPolygon_Click(object sender, EventArgs e) {
      //funkca tworząca poligon wieloboku foremnego

      CalculateScale();

      CreatePolygon();

      this.pnlCanvas.Refresh();

    }

    private void btnDrawProfile_Click(object sender, EventArgs e) {
      //funkca wywołująca rysowanie profila okna

      if (mPolygon_Regular == null)                         //jeśli nie istnieje poligon
        return;

      this.pnlCanvas.Refresh();

    }

    private void btnSetToCurve_Click(object sender, EventArgs e) {
      //funkcja zmieniająca segment w łuk

      int pSelect;

      pSelect = int.Parse(txtSelectSide.Text);

      mPolygon_Regular.Assembly.AssemblyItems[pSelect].Polygon.SetSegmentToCurve(1);
      mPolygon_Regular.Assembly.AssemblyItems[pSelect].Polygon.SetSegmentToCurve(3);

      this.pnlCanvas.Refresh();

    }

    private void MainForm_Resize(object sender, EventArgs e) {
      //funkcja odświerzająca rysunek przy resize

      CalculateScale();

      this.pnlCanvas.Refresh();

    }

    private void CreatePolygon() {
      //funkcja tworząca poligon: 
      // 1) mPolygon_Regular - wielobok foremny

      double pBaseAngle;                                    //kąt pomiędzy: punkt1 segmentu - środek koła - punkt2 segmentu
      int pSidesNumber;                                     //liczba boków figury foremnej
      int pProfileSize;                                     //szerokość profilu
      int pRadius;
      
      pProfileSize = int.Parse(txtProfileSize.Text);
      pRadius = int.Parse(txtDiameter.Text) / 2;
      pSidesNumber = int.Parse(txtSides.Text);
      pBaseAngle = 360 / pSidesNumber;

      mPolygon_Regular = cPolygonFactory.GetPolygon_Regular(pRadius, -pBaseAngle);

      mPolygon_Regular.CreateAssembly(pProfileSize, mPolygon_Regular);

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

/*   17.03.2020 MO funkcja wylączona, żeby działało wkleić do pnlCanvas_Paint()         
  mRect_Height = double.Parse(txtHeight.Text);
  mRect_Width = double.Parse(txtWidth.Text);
  if (mPolygon_Rect == null)
    return;
    
  //obliczanie współrzędnych na Canvas punktu bazowego prostokąta
  pTransfBasePtX = mMarginH + (pnlCanvas.Width - 2 * mMarginH - mRect_Width * mScale) / 2;
  pTransfBasePtY = pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - mRect_Height * mScale) / 2);

  mDrawingAdapter.DrawPolygon(mPolygon_Rect, mScale, pTransfBasePtX, pTransfBasePtY, e);
*/

/*
  16.03.2020 MO tymczasowo wyłączone rysowanie prostokąta zeby działało wkleić do CreatePolygons()

  mRect_Height = double.Parse(txtHeight.Text);
  mRect_Width = double.Parse(txtWidth.Text);

  mPolygon_Rect = cPolygonFactory.GetPolygon_Rect((int)mRect_Width, (int)mRect_Height);
*/