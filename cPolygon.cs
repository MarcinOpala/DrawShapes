using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrawShape {

  public enum PolygonFunctionalityEnum {                    //numerator funkcjonalności wielokąta
    Undefined = 0,                                          //nieokreślony
    FrameOutline = 1,                                       //poligon posiada assembly
    FrameVirtual = 2,                                       //wirualny kształt ramy
    Mullion = 3,                                            //słupek
    Profile = 4,                                             //poligon jest profilem
  }

  public class cPolygon {

    private cAssembly mAssembly;                            //Assembly wielokąta
    private cAssemblyItem mAssemblyItem;                    //AssemblyItem wielokąta
    private cPolygon mChild;                                //dziecko wielokąta
    private PolygonFunctionalityEnum mCntPF;                //typ funkcji
    private int mIndex;                                     //numer wielokąta
    private cPolygon mParent;                               //rodzic wielokąta
    private Dictionary<int, cSegment> mSegments;            //lista boków

    internal cAssembly Assembly { get { return mAssembly; } }
    internal cAssemblyItem AssemblyItem { get { return mAssemblyItem; } }
    internal cPolygon Child { get { return mChild; } set { mChild = value; } }
    internal PolygonFunctionalityEnum CntPF { get { return mCntPF; } set { mCntPF = value; } } 
    internal int Index { get { return mIndex; } set { mIndex = value; } }
    internal cPolygon Parent { get { return mParent; } set { mParent = value; } }
    internal Dictionary<int, cSegment> Segments { get { return mSegments; } set { mSegments = value; } }
    internal string SegmentsList { get { return GetSegmentsList(); } }

    public cPolygon() {

      mSegments = new Dictionary<int, cSegment>();
      mCntPF = PolygonFunctionalityEnum.Undefined;
      mAssembly = new cAssembly();

    }

    public cPolygon(int xIndex) {

      mSegments = new Dictionary<int, cSegment>();
      mIndex = xIndex;
      mCntPF = PolygonFunctionalityEnum.Undefined;

    }

    internal void AddSegment(cSegment xSegment) {
      //funkcja dodająca nowy segment do listy
      //xSegment - wybrany segment

      mSegments.Add(xSegment.Index, xSegment);

    }

    internal void CreateAssembly(int xWidth, cPolygon xPolygon) {
      //funkcja przeładowana

      Dictionary<int, int> pC_Cln;

      pC_Cln = new Dictionary<int, int>();

      foreach (cSegment pSegment in xPolygon.Segments.Values) {
        pC_Cln.Add(pSegment.Index, 0);

      }

      CreateAssembly(xWidth, xPolygon, pC_Cln);

    }

    internal void CreateAssembly(int xWidth, cPolygon xPolygon, int xC) {
      //funkcja tworząca Assembly dla poligonu
      //xWidth - szerokość profilu
      //xPolygon - poligon do przeprowadzenia assembly
      //xC_Cln - kolekcja C - odległość od krawędzi elementu

      mAssembly = new cAssembly();

      mAssembly.CreateMe(xWidth, xPolygon, xC);

    }

    internal void CreateAssembly(int xWidth, cPolygon xPolygon, Dictionary<int, int> xC_Cln) {
      //funkcja tworząca Assembly dla poligonu
      //xWidth - szerokość profilu
      //xPolygon - poligon do przeprowadzenia assembly
      //xC_Cln - kolekcja C - odległość od krawędzi elementu

      mAssembly = new cAssembly();

      mAssembly.CreateMe(xWidth, xPolygon, xC_Cln);

    }

    internal void SetSegmentToCurve(int xIndex) {
      //funkcja zamieniająca bok w łuk
      //xIndex - numer obsługiwanego boku

      mSegments[xIndex].IsCurve = true;

    }

    public string GetSegmentsList() {
      //funkcja zwracająca listę segmentów

      string pStr = string.Empty;

      foreach (var i in mSegments) {
        pStr += $"cSegment: {i} Punkt: ( {i.Value.Point.X} ; {i.Value.Point.Y} Numer:  {i.Value.Index} Type: {i.Value.IsCurve} \n";
      }

      Console.WriteLine(pStr);

      return pStr;

    }

    public void ShowSegmentsList() {
      //funkcja wyświetlająca listę segmentów

      Console.WriteLine(GetSegmentsList());

    }

    internal cSegment GetSegmentByIndex(int xIndex) {
      //funkcja zwracająca segment
      //xIndex - numer segmentu

      int pSegmentIndex;
      int pCountMax;

      pCountMax = mSegments.Count;

      pSegmentIndex = xIndex;

      if (xIndex > pCountMax)
        pSegmentIndex = 1;

      return mSegments[pSegmentIndex];

    }

    internal void FillMeByObject(cPolygon xPolygon) {
      //funkcja wypełniająca podstawowe dane według wybranego boku
      //xPolygon - wielokąt bazowy

      cSegment pSegment;

      //składniki, których nie ustawiamy, ani nie kopiujemy - powinny być ustawione w innym miejscu - na zewnątrz
      //mIndex;
      //mAssembly = null;                                           //do rozbudowy
      //mParent;                                                    //rodzic wielokąta

      mCntPF = xPolygon.CntPF; 

      mSegments = new Dictionary<int, cSegment>();

      foreach (cSegment pSegment_Oryginal in xPolygon.Segments.Values) {

        pSegment = pSegment_Oryginal.Clone();
        pSegment.SetPolygon_Parent(this);
        
        mSegments.Add(pSegment.Index, pSegment);

      }

    }

    internal cPolygon Clone() {
      //funkcja zwracająca kopie boku (wypełnionionia tylko podstawowe pola)

      cPolygon pPolygon;

      pPolygon = new cPolygon();

      pPolygon.FillMeByObject(this);

      return pPolygon;

    }

    public Dictionary <int, cPolygon> Split_PolygonByWidth(int xWidth) {
      //funkcja zwracająca kolekcję podzielonych prostokątów po szerokości
      //xWidth - szerokości względem której dzielimy prostokąt

      Dictionary<int, cPolygon> pCln;
      cPolygon pPolygon;
      
      //Polygon A
      pPolygon = Clone();

      pPolygon.Parent = this;
      pPolygon.Index = mIndex;
      pPolygon.Segments[2].Point.X = xWidth;
      pPolygon.Segments[3].Point.X = xWidth;
      
      pCln = new Dictionary<int, cPolygon>();
      pCln.Add(1, pPolygon);

      //Polygon B
      pPolygon = Clone();

      pPolygon.Parent = this;
      pPolygon.Index = mIndex + 2;
      pPolygon.Segments[1].Point.X = xWidth;
      pPolygon.Segments[4].Point.X = xWidth;
      
      pCln.Add(2, pPolygon);

      return pCln;

    }

    public Dictionary<int, cPolygon> Split_PolygonByHeight(int xHeight) {
      //funkcja zwracająca kolekcję podzielonych prostokątów po wysokości
      //xHeight - wysokość względem której dzielimy prostokąt

      Dictionary<int, cPolygon> pCln;
      cPolygon pPolygon;

      //Polygon A
      pPolygon = Clone();

      pPolygon.Parent = this;
      pPolygon.Index = mIndex;
      pPolygon.Segments[3].Point.Y = xHeight;
      pPolygon.Segments[4].Point.Y = xHeight;

      pCln = new Dictionary<int, cPolygon>();
      pCln.Add(1, pPolygon);

      //Polygon B
      pPolygon = Clone();

      pPolygon.Parent = this;
      pPolygon.Index = mIndex + 2;
      pPolygon.Segments[1].Point.Y = xHeight;
      pPolygon.Segments[2].Point.Y = xHeight;

      pCln.Add(2, pPolygon);

      return pCln;

    }

    internal void SetPolygonToMullion(cPolygon xPolygon, int xMullionPosition_X, int xMullionPosition_Y, int xMullionWidth, int xC) {
      //funkcja ustawiająca poszczególne parametry na parametry typowe dla Polygon_Mullion - pionowy
      //xPolygon - Polygon bazowy
      //xMullionPosition_X - współrzędna X osi słupka
      //xMullionWidth - szerokość słupka
      //xWidth_Profile - szerokość profilu
      //xC - odległość C dla słupka

      cAssemblyItem pAssemblyItem;

      mCntPF = PolygonFunctionalityEnum.Mullion;
      mParent = xPolygon;

      pAssemblyItem = null;

      //ustawiamy boki względem pozycji słupka + przesunięte o połowę jego szerokości
      if (xMullionPosition_Y == 0) {
        mSegments[1].Point.X = xMullionPosition_X - xMullionWidth / 2;
        mSegments[2].Point.X = xMullionPosition_X + xMullionWidth / 2;
        mSegments[3].Point.X = xMullionPosition_X + xMullionWidth / 2;
        mSegments[4].Point.X = xMullionPosition_X - xMullionWidth / 2;

        pAssemblyItem = new cAssemblyItem();

        pAssemblyItem.CreateAssemblyItem_Mullion(this, xMullionWidth, xC, xMullionPosition_X, xMullionPosition_Y);
        pAssemblyItem.Axis_Symmetry = xMullionPosition_X;

      } else if (xMullionPosition_X == 0) {
        mSegments[1].Point.Y = xMullionPosition_Y - xMullionWidth / 2;
        mSegments[2].Point.Y = xMullionPosition_Y - xMullionWidth / 2;
        mSegments[3].Point.Y = xMullionPosition_Y + xMullionWidth / 2;
        mSegments[4].Point.Y = xMullionPosition_Y + xMullionWidth / 2;

        pAssemblyItem = new cAssemblyItem();
        pAssemblyItem.CreateAssemblyItem_Mullion(this, xMullionWidth, xC, xMullionPosition_X, xMullionPosition_Y);

        pAssemblyItem.Axis_Symmetry = xMullionPosition_Y;
      }

      mAssemblyItem = pAssemblyItem;

    }

    internal void AddChild() {
      //funkcja dodająca wielokąt dziecko, parametry podstawowe zostają bez zmian

      cPolygon pPolygon_Child;

      pPolygon_Child = Clone();

      mChild = pPolygon_Child;

      pPolygon_Child.Parent = this;
      
    }

    internal void SetSegmentsPointBy_C() {
      //funkcja ustawiająca punkty boków w zależności od C

      cVector pC_Vector;
      int pC_1, pC_2, pC_3, pC_4;

      pC_1 = Parent.Assembly.AssemblyItems[1].C;
      pC_2 = Parent.Assembly.AssemblyItems[2].C;
      pC_3 = Parent.Assembly.AssemblyItems[3].C;
      pC_4 = Parent.Assembly.AssemblyItems[4].C;

      pC_Vector = new cVector(pC_4, pC_1);
      mSegments[1].MovePointInwardsPolygonByVector(pC_Vector);

      pC_Vector = new cVector(pC_2, pC_1);
      mSegments[2].MovePointInwardsPolygonByVector(pC_Vector);

      pC_Vector = new cVector(pC_2, pC_3);
      mSegments[3].MovePointInwardsPolygonByVector(pC_Vector);

      pC_Vector = new cVector(pC_4, pC_3);
      mSegments[4].MovePointInwardsPolygonByVector(pC_Vector);

    }


    public Dictionary<int, cPolygon> Split_PolygonByVector(cVector xVector_Mullion, int xOffserVector) {
      // !!!  UWAGA FUNKJCA NIE DZIAŁA JEST W TRAKCIE TWORZENIA!!!
      //funkcja zwracająca kolekcję podzielonych wielokątów wektorem
      //xVector_Mullion - wektor słupka

      Dictionary<int, cPolygon> pCln;
      cPolygon pPolygon_A, pPolygon_B, pParent;
      cPoint pPoint;
      float pWidth, pHeight;
      cSegment pSegmentNew;

      pWidth = mSegments[2].Point.X - mSegments[1].Point.X;
      pHeight = mSegments[3].Point.Y - mSegments[2].Point.Y;


      pParent = this;

      //Polygon A
      pPolygon_A = new cPolygon();

      pPolygon_A.CntPF = this.CntPF;
      pPolygon_A.Parent = this;
      pPolygon_A.Index = mIndex;


      //Polygon B
      pPolygon_B = new cPolygon();

      pPolygon_B.Parent = this;
      pPolygon_A.CntPF = this.CntPF;
      pPolygon_B.Index = mIndex + 2;
      int pIdx = 0;

      foreach (cSegment pSegment in mSegments.Values) {
        
        
        pPoint = Get_CrossPoint(pSegment.Index, xVector_Mullion, xOffserVector);
        if (pPoint == null) continue;
        if (pPoint.X > pWidth || pPoint.X < 0 ||
            pPoint.Y > pHeight || pPoint.Y < 0)
          continue;

        pIdx++;
        pSegmentNew = new cSegment(pPoint, pIdx);
        pSegmentNew.Polygon_Parent = pPolygon_A;
        pPolygon_A.AddSegment(pSegmentNew);

        pSegmentNew = new cSegment(pPoint, pIdx);
        pSegmentNew.Polygon_Parent = pPolygon_B;
        pPolygon_B.AddSegment(pSegmentNew);

      }
      
      foreach (cSegment pSegment in pParent.Segments.Values) {

        bool pCheck;
        pSegmentNew = pSegment.Clone();

        pCheck = CheckPoint_BelowVector(pSegment.Point, xVector_Mullion, xOffserVector);
        if (pCheck == true) {
          pSegmentNew.Polygon_Parent = pPolygon_B;
          pSegmentNew.Index = pPolygon_B.Segments.Count + 1;
          pPolygon_B.AddSegment(pSegmentNew);

        } else {
          pSegmentNew.Polygon_Parent = pPolygon_A;
          pSegmentNew.Index = pPolygon_A.Segments.Count + 1;
          pPolygon_A.AddSegment(pSegmentNew);

        }

      }




      pCln = new Dictionary<int, cPolygon>();
      pCln.Add(1, pPolygon_B);
      
      pCln.Add(2, pPolygon_A);

      Console.WriteLine(".........<<><><><>......");
      foreach (cPolygon pPoly in pCln.Values) {
        Console.WriteLine("\n" + pPoly.CntPF + " " + pPoly.Index);
        foreach (cSegment pseg in pPoly.Segments.Values) {
          Console.WriteLine(pseg.Index + ": (" + pseg.Point.X + " ; " + pseg.Point.Y + ")");
        }
      }

      return pCln;

    }

    private cPoint Get_CrossPoint(int xIdx, cVector xVector, int xOffsetVector) {
      //zrobić z punktem zaczepienia wektora!!

      cPoint pPoint;
      double pX, pY, pX_A, pX_B, pY_A, pY_B;
      double pA_1, pB_1, pC_1;
      double pA_2, pB_2, pC_2;
      double pW, pWx, pWy;

     

      pX_A = mSegments[xIdx].Point.X;
      pY_A = mSegments[xIdx].Point.Y;

      pX_B = mSegments[xIdx].Segment_Next.Point.X;
      pY_B = mSegments[xIdx].Segment_Next.Point.Y;

      if (pX_B - pX_A == 0) {
        pA_1 = 1;
        pB_1 = 0;
        pC_1 = - pX_B;
      }
      else {
        pA_1 = -(pY_B - pY_A) / (pX_B - pX_A);
        pB_1 = 1;
        pC_1 = (((pY_B - pY_A) / (pX_B - pX_A) * pX_A) - pY_A);

      }
      pA_2 = -xVector.X;
      pB_2 = xVector.Y;
      pC_2 = -(xOffsetVector); 

      if (pA_1 == pA_2 && pB_1 == pB_2) return null;

      pW = (pA_1 * pB_2) - (pA_2 * pB_1);
      pWx = ((-pC_1) * pB_2) - ((-pC_2) * pB_1);
      pWy = (pA_1 * (-pC_2)) - (pA_2 * (-pC_1));

      if (pW == 0) {
        pX = pWx;
        pY = pWy;
      } else {
        pX = pWx / pW;
        pY = pWy / pW;
      }

      pPoint = new cPoint((float)pX, (float)pY);

      return pPoint;

    }

    private bool CheckPoint_BelowVector(cPoint xPoint, cVector xVector, int xOffsetVector) {
      bool check;

      double pA, pB, pC;
      double rownanieProstej;

      pA = -xVector.X;
      pB = xVector.Y;
      pC = -(xOffsetVector);


      rownanieProstej = ((pA * xPoint.X) + (pB * xPoint.Y) + pC);

      if (rownanieProstej < 0) {
        check = true;

      } else if (rownanieProstej > 0) {
        check = false;
      }
      else
        check = false;
    
      return check;
      
    }


  }

}
