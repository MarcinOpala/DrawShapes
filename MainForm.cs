using System;
using System.Linq;
using System.Windows.Forms;

namespace DrawShape {

  public partial class MainForm : Form {

    private int mIndexOfMaxItem;                            //indeks maksymalnej wartości w tablicy
    private double mMarginV, mMarginH;                      //marginesy: Vertical, Horizontal - wartości pobrane z INPUT 
    private int mMaxValue;                                  //maksymalna wartość w tablicy
    private cProject mProject;                              //inicjacja projektu
    private double mScale;                                  //przeliczona skala całego rysunku
    private cDrawingAdapter mDrawingAdapter;

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
      this.btnCreateProject.Text = "Utwórz projekt";
      this.txtDiameter.Text = "500";
      this.lblDiametr.Text = "Średnica:";
      this.lblSide.Text = "Ilość boków:";
      this.txtSides.Text = "4";
      this.lblUnitDiametr.Text = "mm";
      this.txtSelectSide.Text = "1";
      this.lblSelectSide.Text = "Bok numer:";
      this.btnSetToCurve.Text = "Rysuj Łuk";
      this.btnDrawAssembly.Text = "Wstaw Ramę";
      this.lblProfileUnit.Text = "mm";
      this.lblProfileSize.Text = "Szerokość profilu:";
      this.txtProfileSize.Text = "50";
      this.tabPage1.Text = "Operacje";
      this.tabPage2.Text = "Ustawienia";
      this.lblProjectName.Text = "Nazwa Projektu";
      this.txtProjectName.Text = "Wprowadź nazwę projektu";

      //włączenie funkcji blokującej wpisanie liter i znaków szczególnych w wybranych polach
      this.txtMarginV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMarginH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSides.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSelectSide.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtProfileSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);

      //funkcje CLICK
      this.btnCreateProject.Click += new System.EventHandler(this.CreateProject_Click);            
      this.btnSetToCurve.Click += new System.EventHandler(this.SetToCurve_Click);                  
      this.btnDrawAssembly.Click += new System.EventHandler(this.DrawAssembly_Click);       
      
      //funkcje inne
      this.Resize += new System.EventHandler(this.MainForm_Resize);                                
      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
      
    }

    public void pnlCanvas_Paint(object sender, PaintEventArgs e) {
      //funkcja rysująca projekt 

      cDrawing pDrawing;
      cPoint pPt_Base;
      float pRadius;

      if (mProject == null)                         //jeśli nie istnieje projekt
        return;

      pRadius = float.Parse(txtDiameter.Text) / 2;
      mMarginV = double.Parse(txtMarginV.Text);
      mMarginH = double.Parse(txtMarginH.Text);

      //obliczanie współrzędnych punktu bazowego wieloboku foremnego do wyświetlania na Canvas
      pPt_Base = new cPoint();
      pPt_Base.X = (float)(mMarginH + (pnlCanvas.Width - 2 * mMarginH - 2 * pRadius * mScale) / 2);
      pPt_Base.Y = (float)(pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - 2 * pRadius * mScale) / 2));

      mDrawingAdapter = new cDrawingAdapter();

      pDrawing = mDrawingAdapter.GetDrawing(mProject); 

      pDrawing.Draw(mScale, pPt_Base, e);

    }

    private void CreateProject_Click(object sender, EventArgs e) {
      //funkca tworząca projekt

      int pDiameter;
      string pNe;
      int pSegementsQuantity;                               //liczba boków figury foremnej

      pDiameter = int.Parse(txtDiameter.Text);
      pNe = "nowy projekt";
      pSegementsQuantity = int.Parse(txtSides.Text);

      CalculateScale();

      mProject = new cProject();
      mProject.CreateMe(pDiameter, pSegementsQuantity, pNe);

      DrawProjectShape();

    }

    private void DrawAssembly_Click(object sender, EventArgs e) {
      //funkca wywołująca rysowanie Assembly

      cPolygon pPolygon;
      int pWidth_Profile;                                   //szerokość profilu

      pPolygon = mProject.PolygonsEnv.Polygons[1];
      pWidth_Profile = int.Parse(txtProfileSize.Text);

      mProject.PolygonsEnv.Polygons[1].CreateAssembly(pWidth_Profile, pPolygon);

      this.pnlCanvas.Refresh();

    }

    private void DrawProjectShape() {
      //funkjca rysująca kształt projektu

      

      this.pnlCanvas.Refresh();

    }

    private void SetToCurve_Click(object sender, EventArgs e) {
      //funkcja zmieniająca AssemblyItem w łuk

      cAssembly pAssembly;
      int pSelect;

      if (mProject == null)                         //jeśli nie istnieje projekt
        return;

      pAssembly = mProject.PolygonsEnv.Polygons[1].Assembly;
      pSelect = int.Parse(txtSelectSide.Text);

      pAssembly.AssemblyItems[pSelect].Polygon.SetSegmentToCurve(1);
      pAssembly.AssemblyItems[pSelect].Polygon.SetSegmentToCurve(3);

      this.pnlCanvas.Refresh();

    }

    private void MainForm_Resize(object sender, EventArgs e) {
      //funkcja odświerzająca rysunek przy resize

      CalculateScale();

      this.pnlCanvas.Refresh();

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

    private void txtProjectName_Click(object sender, EventArgs e) {

      this.txtProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.txtProjectName.ForeColor = System.Drawing.SystemColors.WindowText;
      this.txtProjectName.Text = "";
    }

    public double CalculateScale() {
      //funkcja obliczająca skale według największej wartości INPUT

      int[] mArrayOfInput = { };                            //tablica wsystkich INPUT do liczenia skali

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

      mArrayOfInput = new int[mArrayOfInput.Length - 1];                                              //czyszczę tablicę

      return mScale;

    }

  }

}