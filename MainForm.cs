using System;
using System.Collections.Generic;
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
      this.txtSelectSide.Text = "3";
      this.lblSelectSide.Text = "Bok numer:";
      this.btnSetToCurve.Text = "Rysuj Łuk";
      this.btnDrawAssembly.Text = "Wstaw Ramę";
      this.lblProfileUnit.Text = "mm";
      this.lblProfileSize.Text = "Profil:";
      this.txtProfileSize.Text = "60";
      this.tabPage1.Text = "Operacje";
      this.tabPage2.Text = "Ustawienia";
      this.lblProjectName.Text = "Nazwa Projektu";
      this.txtProjectName.Text = "Wprowadź nazwę projektu";
      this.txtHeight.Text = "1000";
      this.txtWidth.Text = "2000";
      this.lblHeight.Text = "Wysokość:";
      this.lblWidth.Text = "Szerokość:";
      this.lblUnitWidth.Text = "mm";
      this.lblUnitHeight.Text = "mm";
      this.btnAddMullion.Text = "Wstaw słupek";
      this.txtMullionLocation.Text = "700";
      this.lblMullionLocation.Text = "Pozycja słupeka:";
      this.txtMullionWidth.Text = "50";
      this.lblMullionWidth.Text = "Szerokość słupka:";
      this.btnAddSash.Text = "Wstaw skrzydło";
      this.btnRemoveSash.Text = "Usuń skrzydło";
      this.txtCWidth_Profile.Text = "40";
      this.lblCWidth_Profile.Text = "C Profilu:";
      this.txtCWidth_Mullion.Text = "5";
      this.lblCWidth_Mullion.Text = "C Słupka:";




      //włączenie funkcji blokującej wpisanie liter i znaków szczególnych w wybranych polach
      this.txtMarginV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMarginH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSides.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSelectSide.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtProfileSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMullionLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMullionWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtCWidth_Mullion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtCWidth_Profile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);


      

      //funkcje CLICK
      this.btnCreateProject.Click += new System.EventHandler(this.CreateProject_Click);            
      this.btnSetToCurve.Click += new System.EventHandler(this.SetToCurve_Click);                  
      this.btnDrawAssembly.Click += new System.EventHandler(this.InsertAssembly);       
      
      //funkcje inne
      this.Resize += new System.EventHandler(this.MainForm_Resize);                                
      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
      
    }

    private void pnlCanvas_Paint(object sender, PaintEventArgs e) {
      //funkcja rysująca projekt 

      cDrawing pDrawing;
      cPoint pPt_Base;
      float pRadius;
      int pHeight, pWidth;
    
      if (mProject == null)                         //jeśli nie istnieje projekt
        return;

      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);
           
      pRadius = float.Parse(txtDiameter.Text) / 2;

      mMarginV = double.Parse(txtMarginV.Text);
      mMarginH = double.Parse(txtMarginH.Text);

      //obliczanie współrzędnych punktu bazowego wieloboku foremnego do wyświetlania na Canvas
      pPt_Base = new cPoint();
      pPt_Base.X = (float)(mMarginH + (pnlCanvas.Width - 2 * mMarginH - pWidth * mScale) / 2);
      pPt_Base.Y = (float)(pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - pHeight * mScale) / 2));

      Console.WriteLine(">>>> X: " + pPt_Base.X + " Y: " + pPt_Base.Y);

      mDrawingAdapter = new cDrawingAdapter();

      pDrawing = mDrawingAdapter.GetDrawing(mProject); 

      pDrawing.Draw(mScale, pPt_Base, e);

      Console.WriteLine("....");
      foreach (cPolygon pPoly in mProject.PolygonsEnv.Polygons.Values) {
                
        Console.WriteLine("\n"+pPoly.CntPF + " " + pPoly.Index);
        foreach (cSegment pseg in pPoly.Segments.Values) {
          Console.WriteLine(pseg.Index + ": (" + pseg.Point.X + " ; " + pseg.Point.Y + ")");

        }

      }

      }

    private void CreateProject_Click(object sender, EventArgs e) {
      //funkca tworząca projekt

      int pHeight, pWidth;
      string pNe;

      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);
      pNe = "nowy projekt";
      
      CalculateScale();

      mProject = new cProject();
      mProject.CreateMe(pWidth, pHeight, pNe);

      this.pnlCanvas.Refresh();

    }

    private void InsertAssembly(object sender, EventArgs e) {
      //funkca wywołująca wstawienie profilu

      Dictionary<int, int> pC_Cln;
      cPolygon pPolygon;
      int pWidth_Profile;
      int pC_Frame;

      pC_Cln = new Dictionary<int, int>();
   
      pPolygon = mProject.PolygonsEnv.Polygons[1];

      //wartości z Input txt w settings
      pC_Frame = int.Parse(txtCWidth_Profile.Text);
      pWidth_Profile = int.Parse(txtProfileSize.Text);      

      //tworzymy kolekcję wartości C
      foreach (cSegment pSegment in pPolygon.Segments.Values) {
        pC_Cln.Add(pSegment.Index, pC_Frame);
      }
      //ag - wpuścić tylko jedno C - przeładowanie funkcji
      pPolygon.CreateAssembly(pWidth_Profile, pPolygon, pC_Cln);

      pPolygon.CntPF = PolygonFunctionalityEnum.FrameOutline;

      mProject.PolygonsEnv.CreatePolygon_Virtual(pPolygon);

      this.pnlCanvas.Refresh();

    }

    private void InsertMullion(object sender, EventArgs e) {
      //funkcja wstawiająca słupek 
      
      int pMullionPosition_X, pMullionPosition_Y; 
      cPolygon pPolygon;
      int pWidth_Frame;
      int pWidth_Mullion;
      float pWidth_Profile;
      int pC_Mullion;

      //if (mProject.PolygonsEnv.GetPolygonMullion() != null) return;   //jeśli już jest słupek to koniec

      //pobieramy wartości Input z menu - settings
      pC_Mullion = int.Parse(txtCWidth_Mullion.Text);
      pMullionPosition_X = int.Parse(txtMullionLocation.Text);
      pMullionPosition_Y = 0;
      pWidth_Mullion = int.Parse(txtMullionWidth.Text);
      pWidth_Frame = int.Parse(txtWidth.Text);
      pWidth_Profile = float.Parse(txtProfileSize.Text);

      //ag
      if (pMullionPosition_X + pWidth_Mullion / 2 > pWidth_Frame - pWidth_Profile) return;   //ograniczenie z prawej
      if (pMullionPosition_X - pWidth_Mullion / 2 < pWidth_Profile) return;                  //ograniczenie z lewej


      pPolygon = mProject.PolygonsEnv.GetPolygonVirtual_By_MullionPositon(pMullionPosition_X);

      //pPolygon = mProject.PolygonsEnv.GetPolygonVirtual();

      //utworzenie słupka na bazie Polygon_Outline
      mProject.PolygonsEnv.CreatePolygon_Mullion(pPolygon, pMullionPosition_X, pMullionPosition_Y, pWidth_Mullion, pWidth_Profile, pC_Mullion);



      //pPolygon = mProject.PolygonsEnv.GetPolygonVirtual();

      //dzielenie Polygon_Virtual w miejscu pozycji kolumny
      mProject.PolygonsEnv.Split_Polygon(pMullionPosition_X, pMullionPosition_Y, pPolygon);


      this.pnlCanvas.Refresh();

    }

    private void InsertSash(object sender, EventArgs e) {
      //funkcja wstawiająca skrzydło w pierwszym wolnym wielokącie wirtualnym

      cPolygon pPolygon;
      int pWidth_Profile;

      pPolygon = mProject.PolygonsEnv.GetPolygonVirtual_WithoutChild();

      if (pPolygon == null) return;         //jeśli nie ma wolnego miejsca koniec

      //przypisujemy wartość z Input menu - settings
      pWidth_Profile = int.Parse(txtProfileSize.Text);

      mProject.PolygonsEnv.CreatePolygon_Sash(pPolygon, pWidth_Profile);

      this.pnlCanvas.Refresh();

    }

    private void RemoveSash(object sender, EventArgs e) {
      //funkcja usuwająca pierwsze znalezione skrzydło

      cPolygon pPolygon;

      pPolygon = mProject.PolygonsEnv.GetPolygonVirtual_WithChild();

      if (pPolygon == null) return;

      pPolygon.Child = null;

      this.pnlCanvas.Refresh();

    }

    private void SetToCurve_Click(object sender, EventArgs e) {
      //funkcja zmieniająca AssemblyItem w łuk

      cAssembly pAssembly;
      int pSelect;

      if (mProject == null)                         //jeśli nie istnieje projekt
        return;

     
      pSelect = int.Parse(txtSelectSide.Text)+1;

      pAssembly = mProject.PolygonsEnv.Polygons[1].Assembly;

      if (pAssembly != null) {

        pAssembly.AssemblyItems[pSelect].Polygon.SetSegmentToCurve(1);
        pAssembly.AssemblyItems[pSelect].Polygon.SetSegmentToCurve(3);

      }

      mProject.PolygonsEnv.Polygons[1].SetSegmentToCurve(pSelect - 1);

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

    private int TakeMaxValueOfArrey(int[] xArray) {
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
      int pDiameter;
      int pHeight, pWidth;

      //24.03.2020 MO tymczasowo wyłączone - potrzebne do rysowania wielokąta foremnego 
      //pDiameter = int.Parse(txtDiameter.Text);

      pDiameter = 0;
      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);

      mArrayOfInput = mArrayOfInput.Concat(new int[] { pWidth, pHeight, pDiameter }).ToArray();

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