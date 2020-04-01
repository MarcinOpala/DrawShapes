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
      this.btnAddMullionVertical.Text = "Wstaw słupek |";
      this.btnAddMullionHorizontal.Text = "Wstaw słupek -";
      this.txtMullionLocationX.Text = "700";
      this.txtMullionLocationY.Text = "700";
      this.lblMullionLocation.Text = "Pozycja słupeka (X , Y):";
      this.txtMullionWidth.Text = "50";
      this.lblMullionWidth.Text = "Szerokość słupka:";
      this.btnAddSash.Text = "Wstaw skrzydło";
      this.btnRemoveSash.Text = "Usuń skrzydło";
      this.txtCWidth_Profile.Text = "40";
      this.lblCWidth_Profile.Text = "C Profilu:";
      this.txtCWidth_Mullion.Text = "10";
      this.lblCWidth_Mullion.Text = "C Słupka:";




      //włączenie funkcji blokującej wpisanie liter i znaków szczególnych w wybranych polach
      this.txtMarginV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMarginH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSides.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtSelectSide.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtProfileSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMullionLocationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMullionLocationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtMullionWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtCWidth_Mullion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);
      this.txtCWidth_Profile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckKeyPress);

      this.txtMullionLocationX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Insert_Cross);
      this.txtMullionLocationY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Insert_Cross);


      //funkcje CLICK
      this.btnCreateProject.Click += new System.EventHandler(this.CreateProject_Click);            
      this.btnSetToCurve.Click += new System.EventHandler(this.SetToCurve_Click);                  
      this.btnDrawAssembly.Click += new System.EventHandler(this.InsertProfile);       
      
      //funkcje inne
      this.Resize += new System.EventHandler(this.MainForm_Resize);                                
      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
      
    }

    private void pnlCanvas_Paint(object sender, PaintEventArgs e) {
      //funkcja rysująca projekt 

      cDrawing pDrawing;
      cPoint pPt_Base;
      int pHeight, pWidth;
    
      if (mProject == null)                         //jeśli nie istnieje projekt
        return;

      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);
      mMarginV = double.Parse(txtMarginV.Text);
      mMarginH = double.Parse(txtMarginH.Text);

      //obliczanie współrzędnych punktu bazowego wieloboku foremnego do wyświetlania na Canvas
      pPt_Base = new cPoint();
      pPt_Base.X = (float)(mMarginH + (pnlCanvas.Width - 2 * mMarginH - pWidth * mScale) / 2);
      pPt_Base.Y = (float)(pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - pHeight * mScale) / 2));


      mDrawingAdapter = new cDrawingAdapter();
      pDrawing = mDrawingAdapter.GetDrawing(mProject); 

      pDrawing.Draw(mScale, pPt_Base, e);


          //wypisanie wszystkich polygonsów w ENV
          Console.WriteLine("......................");
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

    private void InsertProfile(object sender, EventArgs e) {
      //funkca wywołująca wstawienie profilu

      cPolygon pPolygon;
      int pWidth_Profile;
      int pC_Frame;
   
      pPolygon = mProject.PolygonsEnv.Polygons[1];

      //wartości z Input txt w settings
      pC_Frame = int.Parse(txtCWidth_Profile.Text);
      pWidth_Profile = int.Parse(txtProfileSize.Text);      

      pPolygon.CreateAssembly(pWidth_Profile, pPolygon, pC_Frame);

      pPolygon.CntPF = PolygonFunctionalityEnum.FrameOutline;

      mProject.PolygonsEnv.CreatePolygon_Virtual(pPolygon);

      //dodajemy krzyżyk wskazujący miejsce słupka

      this.pnlCanvas.Refresh();

    }

    private void InsertMullion_Vertical(object sender, EventArgs e) {
      //funkcja wstawiająca słupek pionowy
      
      int pMullionPosition_X, pMullionPosition_Y; 
      cPolygon pPolygon;
      int pWidth_Frame;
      int pWidth_Mullion;
      float pWidth_Profile;
      int pC_Mullion;

      //pobieramy wartości Input z menu - settings
      pC_Mullion = int.Parse(txtCWidth_Mullion.Text);
      pMullionPosition_X = int.Parse(txtMullionLocationX.Text);
      pMullionPosition_Y = int.Parse(txtMullionLocationY.Text);
      pWidth_Mullion = int.Parse(txtMullionWidth.Text);
      pWidth_Frame = int.Parse(txtWidth.Text);
      pWidth_Profile = float.Parse(txtProfileSize.Text);

      //ograniczenia pozycji słupka
      if (pMullionPosition_X + pWidth_Mullion / 2 > pWidth_Frame - pWidth_Profile) return;   //prawej
      if (pMullionPosition_X - pWidth_Mullion / 2 < pWidth_Profile) return;                  //lewej

      //sprawdzenie czy słupek już w tym miejscu istnieje
      //TODO
      //if ()


      pPolygon = mProject.PolygonsEnv.GetPolygonVirtual_By_MullionPositon(pMullionPosition_X, pMullionPosition_Y);

      //utworzenie słupka na bazie wirtualnego wielokąta, w którym się znajduje
      mProject.PolygonsEnv.CreatePolygon_Mullion(pPolygon, pMullionPosition_X, 0, pWidth_Mullion, pWidth_Profile, pC_Mullion);

      //dzielenie Polygon_Virtual w miejscu pozycji kolumny
      mProject.PolygonsEnv.Split_Polygon(pMullionPosition_X, 0, pPolygon);

      this.pnlCanvas.Refresh();

    }

    private void InsertMullion_Horizontal(object sender, EventArgs e) {
      //funkcja wstawiająca słupek poziomy

      int pMullionPosition_X, pMullionPosition_Y;
      cPolygon pPolygon;
      int pWidth_Frame;
      int pWidth_Mullion;
      float pWidth_Profile;
      int pC_Mullion;

      //pobieramy wartości Input z menu - settings
      pC_Mullion = int.Parse(txtCWidth_Mullion.Text);
      pMullionPosition_X = int.Parse(txtMullionLocationX.Text);
      pMullionPosition_Y = int.Parse(txtMullionLocationY.Text);
      pWidth_Mullion = int.Parse(txtMullionWidth.Text);
      pWidth_Frame = int.Parse(txtWidth.Text);
      pWidth_Profile = float.Parse(txtProfileSize.Text);


      //ograniczenia pozycji słupka TODO!!!


      //sprawdzenie czy słupek już w tym miejscu istnieje
      //TODO

      pPolygon = mProject.PolygonsEnv.GetPolygonVirtual_By_MullionPositon(pMullionPosition_X, pMullionPosition_Y);

      //utworzenie słupka na bazie wirtualnego wielokąta, w którym się znajduje
      mProject.PolygonsEnv.CreatePolygon_Mullion(pPolygon, 0, pMullionPosition_Y, pWidth_Mullion, pWidth_Profile, pC_Mullion);

      //dzielenie Polygon_Virtual w miejscu pozycji kolumny
      mProject.PolygonsEnv.Split_Polygon(0, pMullionPosition_Y, pPolygon);

      this.pnlCanvas.Refresh();

    }

    private void InsertSash(object sender, EventArgs e) {
      //funkcja wstawiająca skrzydło w pierwszym wolnym wielokącie wirtualnym

      cPolygon pPolygon;
      int pWidth_Profile;

      pPolygon = mProject.PolygonsEnv.GetPolygonVirtual_WithoutChild();

      if (pPolygon == null) return;         //jeśli nie ma wolnego miejsca to wyjście

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

    private void Insert_Cross(object sender, KeyPressEventArgs e) {
      //funkcja wstawiająca do projektu krzyż - środek słupka

      int pMullionPosition_X;
      int pMullionPosition_Y;
      int pCross_Size;
      cPolygon pPolygon;
      int pIdx;
      int pHeight, pWidth;

      //Wymiary projektu
      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);

      pMullionPosition_X = int.Parse(txtMullionLocationX.Text);
      if (pMullionPosition_X <= 0 || pMullionPosition_X >= pWidth) return;

      pMullionPosition_Y = int.Parse(txtMullionLocationY.Text);
      if (pMullionPosition_Y <= 0 || pMullionPosition_Y >= pHeight) return;

      pCross_Size = 20;

      pPolygon = new cPolygon();

      pPolygon.Assembly.CreateCross(pMullionPosition_X, pMullionPosition_Y, pCross_Size);

      mProject.PolygonsEnv.AddPolygon(pPolygon);

      this.pnlCanvas.Refresh();

      pIdx = mProject.PolygonsEnv.Polygons.Count;

      mProject.PolygonsEnv.Polygons.Remove(pIdx);


    }

  }

}