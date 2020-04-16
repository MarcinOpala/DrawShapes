using System;
using System.Collections.Generic;
using System.Drawing;
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
    private Dictionary<int, cPoint> mCln_Points;

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
      this.btnAddMullionVertical.Text = "Wstaw słupek";
      this.btnAddMullionHorizontal.Text = "";
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
      this.btnCreateProject.Click += new System.EventHandler(this.CreateProject);            
      this.btnSetToCurve.Click += new System.EventHandler(this.SetToCurve_Click);                  
      this.btnDrawAssembly.Click += new System.EventHandler(this.InsertProfile);       
      
      //funkcje inne
      this.Resize += new System.EventHandler(this.MainForm_Resize);                                
      this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
      this.pnlCanvas.Click += new System.EventHandler(this.GetPositon_onMouseClick);
      this.pnlCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
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

      mProject.ProjectRegions.CreateProjectRegions(mProject.PolygonsEnv);

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

    private void CreateProject(object sender, EventArgs e) {
      //funkca tworząca projekt

      int pHeight, pWidth;
      string pNe;

      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);
      pNe = "Nowy Projekt";
      
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

      //pobieranie wartości z Input txt w settings
      pC_Frame = int.Parse(txtCWidth_Profile.Text);
      pWidth_Profile = int.Parse(txtProfileSize.Text);      

      pPolygon.CreateAssembly(pWidth_Profile, pPolygon, pC_Frame);

      pPolygon.CntPF = PolygonFunctionalityEnum.FrameOutline;

      mProject.PolygonsEnv.CreatePolygon_Virtual(pPolygon);

      mCln_Points = new Dictionary<int, cPoint>();

      this.pnlCanvas.Refresh();

    }

    private void InsertMullion(object sender, EventArgs e) {
      //funkcja wstawiająca słupek

      int pC_Mullion;
      Dictionary<int, cPolygon> pCln_Polygons_Virtual, pCln_Polygons_Tangential, pCln_Polygons_Mullion;
      Dictionary<int, cPoint> pCln_Points;
      cPoint pPoint;
      cPolygon pPolygon;
      cLine pLine_Axis_Symetry_Mullion;
      int pWidth_Mullion;

      //pobieranie wartości Input z menu - settings
      pC_Mullion = int.Parse(txtCWidth_Mullion.Text);
      pWidth_Mullion = int.Parse(txtMullionWidth.Text);

      // TODO!!!
      //sprawdzenie czy słupek już w tym miejscu istnieje, graniczenia pozycji słupka 

      pCln_Points = mCln_Points;
      if (pCln_Points.Count != 2) return;     //musimy mieć 2 punkty, żeby podzielić wielokąt

      pPoint = pCln_Points[1];

      //wybranie wielokąta wirtualnego po kliknięciu myszką - !  Narazie nie jest to wykorzystywane  !
      pPolygon = mProject.PolygonsEnv.GetPolygonVirtual_ByPoint(pPoint);

      pLine_Axis_Symetry_Mullion = new cLine(pCln_Points);
      pLine_Axis_Symetry_Mullion.Simplify(pLine_Axis_Symetry_Mullion);

      //pobranie wielokątów wirtualnych, potrzebnych do podziału prostą (os słupka)
      pCln_Polygons_Virtual = mProject.PolygonsEnv.GetPolygonsVirtual_CrossedBy_AxisSymmetry(pLine_Axis_Symetry_Mullion);
        
      foreach(cPolygon pPolygon_Virtual in pCln_Polygons_Virtual.Values) {

        //dzielimy wielokąt wirtualny prostą (oś słupka) i dodajemy oba do kolekcji
        pCln_Polygons_Tangential = mProject.PolygonsEnv.SplitPolygonVirtual_ByLine(pPolygon_Virtual, pLine_Axis_Symetry_Mullion, pC_Mullion);

        //pobieramy wszystkie słupki styczne do wybranych wielokątów wirtualnych
        pCln_Polygons_Mullion = mProject.PolygonsEnv.GetPolygonsMullion_Tangential_To_PolygonsVirtual(pCln_Polygons_Tangential);

        //utworzenie słupka na bazie wirtualnych wielokątów, w których się znajduje
        mProject.PolygonsEnv.CreatePolygon_Mullion(pCln_Polygons_Tangential, pCln_Polygons_Mullion, pWidth_Mullion, pC_Mullion);

        pCln_Polygons_Tangential.Clear();
        pCln_Polygons_Mullion.Clear();
      }

      //punkty tworzące oś słupka już można wyczyścić
      pCln_Points.Clear();
     
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
      //funkcja wstawiająca do projektu krzyżyk oznaczająca miejsce położenia środek słupka

      int pMullionPosition_X;
      int pMullionPosition_Y;
      int pCross_Size;
      cPolygon pPolygon;
      cPoint pPoint;
      int pIdx;
      int pHeight, pWidth;

      //Wymiary projektu
      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);

      //wstawienie ograniczeń - krzyżyk musi być w obrębie projektu
      pMullionPosition_X = int.Parse(txtMullionLocationX.Text);
      if (pMullionPosition_X <= 0 || pMullionPosition_X >= pWidth) return;

      pMullionPosition_Y = int.Parse(txtMullionLocationY.Text);
      if (pMullionPosition_Y <= 0 || pMullionPosition_Y >= pHeight) return;

      pCross_Size = 20; //wymiary krzyżyka

      pPolygon = new cPolygon();

      pPoint = new cPoint(pMullionPosition_X, pMullionPosition_Y);

      pPolygon.Assembly.CreateCross(pPoint, pCross_Size);

      mProject.PolygonsEnv.AddPolygon(pPolygon);

      //odświeżamy obszar, ale odrazu usuwamy, żeby nie zakłócał pracy projektu
      this.pnlCanvas.Refresh();

      pIdx = mProject.PolygonsEnv.Polygons.Count;
      mProject.PolygonsEnv.Polygons.Remove(pIdx);

    }

    private void AddPointToSplit(cPoint xPoint) {

      int pIdx;

      pIdx = mCln_Points.Count + 1;

      mCln_Points.Add(pIdx, xPoint);
      
    }

    private void GetPositon_onMouseClick(object sender, EventArgs e) {
      //

      Point point = pnlCanvas.PointToClient(Cursor.Position);

      int pX;
      int pY;
      string pStr;
      cPoint pPt_Base, pPoint;
      int pHeight, pWidth;

      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);

      pPt_Base = new cPoint();
      pPt_Base.X = (float)(mMarginH + (pnlCanvas.Width - 2 * mMarginH - pWidth * mScale) / 2);
      pPt_Base.Y = (float)(pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - pHeight * mScale) / 2));

      pX = (int)((point.X - pPt_Base.X) / mScale);
      pY = -(int)((point.Y - pPt_Base.Y) / mScale);
      pPoint = new cPoint(pX, pY);

      AddPointToSplit(pPoint);

      pStr = $"Pozycja Myszki: X: {pX}, Y: {pY}  Skala: {mScale}, Baze Point: ( {pPt_Base.X} ; {pPt_Base.Y} )\n";

      Console.WriteLine(pStr);

    }

    private void MouseMove(object sender, MouseEventArgs e) {
      //

      Point point = pnlCanvas.PointToClient(Cursor.Position);
      int pX;
      int pY;
      string pStr;
      cPoint pPt_Base, pPoint;
      int pHeight, pWidth;

      pHeight = int.Parse(txtHeight.Text);
      pWidth = int.Parse(txtWidth.Text);

      pPt_Base = new cPoint();
      pPt_Base.X = (float)(mMarginH + (pnlCanvas.Width - 2 * mMarginH - pWidth * mScale) / 2);
      pPt_Base.Y = (float)(pnlCanvas.Height - (mMarginV + (pnlCanvas.Height - 2 * mMarginV - pHeight * mScale) / 2));

      pX = (int)((point.X - pPt_Base.X) / mScale);
      pY = -(int)((point.Y - pPt_Base.Y) / mScale);
      pPoint = new cPoint(pX, pY);

      pStr = $"Mouse move: X: {pX}, Y: {pY}  Skala: {mScale}, Baze Point: ( {pPt_Base.X} ; {pPt_Base.Y} )\n";

      Console.WriteLine(pStr);
    }

    }

  }