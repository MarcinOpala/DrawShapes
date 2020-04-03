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


    public Dictionary<int, cPolygon> Split_PolygonByVector(cVector xVector_Mullion, cPoint xPt_Vector) {
      // !!!  UWAGA FUNKJCA NIE DZIAŁA JEST W TRAKCIE TWORZENIA!!!
      //funkcja zwracająca kolekcję podzielonych wielokątów prostą stworząną z wektora
      //xVector_Mullion - wektor słupka
      //xPt_Vector - punkt zaczepienia wektora

      Dictionary<int, cPolygon> pCln_Polygon;
      cPolygon pPolygon_A, pPolygon_B;
      cPoint pPoint;
      float pWidth, pHeight;
      cSegment pSegmentNew;
      int pCheck;
      float xOffsetVector;
      Dictionary<int, cPoint> pCln_Points;

      //Polygon A
      pPolygon_A = new cPolygon();
      pPolygon_A.Parent = this;
      pPolygon_A.CntPF = PolygonFunctionalityEnum.FrameVirtual;
      pPolygon_A.Index = mIndex;

      //Polygon B
      pPolygon_B = new cPolygon();
      pPolygon_B.Parent = this;
      pPolygon_B.CntPF = PolygonFunctionalityEnum.FrameVirtual;
      pPolygon_B.Index = mIndex + 2;

      if (xPt_Vector.X == 0) xOffsetVector = xPt_Vector.Y;  //obliczenie przesunięcia wektora
      else xOffsetVector = xPt_Vector.X;

      int pIdx = 0;
      pWidth = mSegments[2].Point.X - mSegments[1].Point.X;     //szerokość wielokąta - będzie działać tylko dla prostokątów
      pHeight = mSegments[3].Point.Y - mSegments[2].Point.Y;    //wysokość wielokąta

      pCln_Points = new Dictionary<int, cPoint>();
      
      //obliczenie punktów na wielokącie przeciętym prostą
      foreach (cSegment pSegment in mSegments.Values) {
        pPoint = Get_CrossPoint(pSegment.Index, xVector_Mullion, xOffsetVector);
        if (pPoint == null) continue;               //jeśli prosta jest równoległa
        if (pPoint.X > pWidth || pPoint.X < 0 ||    //jeśli prosta przecina drugą prostą poza obszarem wielokąta
            pPoint.Y > pHeight || pPoint.Y < 0)
          continue;
        pIdx++;
        pCln_Points[pIdx] = pPoint;

        //Polygon_A
        pSegmentNew = new cSegment(pPoint, pPolygon_A.Segments.Count + 10);    //uzyskane punkty dodajemy do nowych poligonów
        pSegmentNew.Polygon_Parent = pPolygon_A;
        pPolygon_A.AddSegment(pSegmentNew);
        //Polygon_B
        pSegmentNew = new cSegment(pPoint, pPolygon_B.Segments.Count + 10);
        pSegmentNew.Polygon_Parent = pPolygon_B;
        pPolygon_B.AddSegment(pSegmentNew);

      } //każdy bok wielokąta bazowego kopiujemy i sprawdzamy do którego wielokąta należy
      foreach (cSegment pSegment in mSegments.Values) {
        pSegmentNew = pSegment.Clone();
        pCheck = Check_PointBelongsToLine(pSegment.Point, pCln_Points);
        if (pCheck > 0) { //bok leży powyżej prostej dzielącej wielokąt
          pSegmentNew.Polygon_Parent = pPolygon_B;
          pSegmentNew.Index = pPolygon_B.Segments.Count + 10; //+10 dla łatwiejszego porządkowania w Organize_Segment()
          pPolygon_B.AddSegment(pSegmentNew);

        } else {         //bok leży poniżej prostej dzielącej wielokąt (w tym przypadku nie możliwe, żęby leżał na prostej)
          pSegmentNew.Polygon_Parent = pPolygon_A;
          pSegmentNew.Index = pPolygon_A.Segments.Count + 10;
          pPolygon_A.AddSegment(pSegmentNew);
        }
      } //uporządkowanie boków zgodnie z całym projektem
      Organize_Segments(pPolygon_A);
      Organize_Segments(pPolygon_B);

      pCln_Polygon = new Dictionary<int, cPolygon>(); //dodanie wielokątów utworzonych z podziału bazowego
      pCln_Polygon.Add(1, pPolygon_B);    
      pCln_Polygon.Add(2, pPolygon_A);

      return pCln_Polygon;

    }

    private cPoint Get_CrossPoint(int xIdx, cVector xVector, float xOffsetVector) {
      //funkcja zwracająca punkt przecięcia dwóch prostych w postaci ogólnej Ax + By + C = 0
      //xIdx - numer boku pokrywającego się z pierwszą prostą
      //xVector - wektor nadający kierunek drugiej prostej
      //xOffsetVector - przesunięcie wektora

      cPoint pPoint;
      double pX, pY;
      double pX_1, pY_1, pA_1, pB_1, pC_1;
      double pX_2, pY_2, pA_2, pB_2, pC_2;
      double pW, pWx, pWy;

      pX_1 = mSegments[xIdx].Point.X;
      pY_1 = mSegments[xIdx].Point.Y;

      pX_2 = mSegments[xIdx].Segment_Next.Point.X;
      pY_2 = mSegments[xIdx].Segment_Next.Point.Y;

      //przygotowanie składników do ogólnego równania prostej Ax + By + C = 0
      if (pX_2 - pX_1 == 0) {   //prosta jest równoległa do osi Y
        pA_1 = 1;
        pB_1 = 0;
        pC_1 = - pX_2;
      }
      else {
        pA_1 = (pY_2 - pY_1) / (pX_2 - pX_1);
        pB_1 = 1;
        pC_1 = (((pY_2 - pY_1) / (pX_2 - pX_1) * pX_1) - pY_1);

      }
      pA_2 = -xVector.X;
      pB_2 = xVector.Y;
      pC_2 = -(xOffsetVector); 

      if (pA_1 == pA_2 && pB_1 == pB_2) return null; //proste są równoległe

      pW = (pA_1 * pB_2) - (pA_2 * pB_1);            //obliczenie wyznaczników
      pWx = ((-pC_1) * pB_2) - ((-pC_2) * pB_1);
      pWy = (pA_1 * (-pC_2)) - (pA_2 * (-pC_1));

      if (pW == 0) {        //prosta jest prostopadła
        pX = pWx;
        pY = pWy;
      } else {
        pX = pWx / pW;
        pY = pWy / pW;
      }

      pPoint = new cPoint((float)pX, (float)pY);

      return pPoint;

    }

    internal void Organize_Segments(cPolygon xPolygon) {
      //funkcja porządkująca boki w wielokącie.
      //Pierwszy najbliższy (0; 0), kolejne przeciwnie do ruchu zegara 
      //xPolygon - wielokąt do uporządkowania

      int pCheck;
      Dictionary<int, cPoint> pCln_Points, pCln_LinePoints;
      int pIdx_Cln_Points; 
      int pNewIdx_Segment;
      cSegment pSegment;
      int pCountSegments;

      //dodajemy do listy wszystkie punkty wielokąta
      pCln_Points = new Dictionary<int, cPoint>();
      foreach (cSegment pSegment_Base in xPolygon.Segments.Values) {
        pCln_Points[pSegment_Base.Index] = pSegment_Base.Point;
      }
      //wybieramy pierwszy punkt (położony najbliżej 0;0 ) i usuwamy go z listy
      pNewIdx_Segment = 1;
      var dict = pCln_Points.OrderBy(x => x.Value.X).ThenBy(x => x.Value.Y).ToDictionary(x => x.Key, x => x.Value);
      xPolygon.Segments.Add(pNewIdx_Segment, xPolygon.Segments[dict.Keys.First()]);
      pCln_Points.Remove(dict.Keys.First());
      
      pNewIdx_Segment++;
      //boki numerujemy zgodnie z kolejnością jak u rodzica
      foreach (cSegment pSegment_Parent in xPolygon.Parent.Segments.Values) {
        pIdx_Cln_Points = 0;
        foreach (cPoint pPoint in pCln_Points.Values) {   //każdy punkt sprawdzam, czy należy do danego boku rodzica
          pIdx_Cln_Points++;
          if (pPoint.X == pSegment_Parent.Point.X && pPoint.Y == pSegment_Parent.Point.Y) { //jeśli punkt pokrywa się
            pSegment = xPolygon.Segments[pCln_Points.Keys.ElementAt(pIdx_Cln_Points-1)];
            xPolygon.Segments.Add(pNewIdx_Segment, pSegment);
            pCln_Points.Remove(pCln_Points.Keys.ElementAt(pIdx_Cln_Points-1));   //usuwam znaleziony punkt z listy
            
            pNewIdx_Segment++;
            break;
          }
        }
        pIdx_Cln_Points = 0;
        foreach (cPoint pPoint in pCln_Points.Values) {  //każdy punkt z listy sprawdzamy, czy leży na prostej
          pIdx_Cln_Points++;
          pCln_LinePoints = new Dictionary<int, cPoint>();
          pCln_LinePoints[1] = pSegment_Parent.Point;
          pCln_LinePoints[2] = pSegment_Parent.Segment_Next.Point;

          pCheck = Check_PointBelongsToLine(pPoint, pCln_LinePoints); //obliczenie równania prostej w postaci ogólnej

          if (pCheck == 0 ) {        //jeśli prawda to leży na prostej
            pSegment = xPolygon.Segments[pCln_Points.Keys.ElementAt(pIdx_Cln_Points - 1)];
            xPolygon.Segments.Add(pNewIdx_Segment, pSegment);
            pCln_Points.Remove(pCln_Points.Keys.ElementAt(pIdx_Cln_Points - 1));  //usuwamy znaleziony punkt z listy

            pNewIdx_Segment++;
            break;
          }
        }
      }
      pCountSegments = xPolygon.Segments.Count / 2;

      for (int i = 1; i <= pCountSegments; i++) {  // usuwamy niepotrzebne boki
        xPolygon.Segments.Remove(i+9);
        xPolygon.Segments[i].Index = i;
      }
    }

    private int Check_PointBelongsToLine(cPoint xPoint, Dictionary<int, cPoint> xCln) {
      //funkcja zwracająca obliczone równanie prostej w postaci ogólnej Ax+By+C
      //jeśli wynik jest > 0 punkt leży powyżęj prostej; =0 leży na prostej; < 0 poniżej prostej
      //xPoint - punkt, dla którego obliczamy równanie prostej
      //xCln - kolekcja dwóch punktów tworzących prostą

      double pA, pB, pC;
      double pGeneralEquation_StraightLine;
      double pX_1, pY_1;
      double pY_2, pX_2;

      pX_1 = xCln[1].X;
      pY_1 = xCln[1].Y;

      pX_2 = xCln[2].X;
      pY_2 = xCln[2].Y;

      if (pX_2 - pX_1 == 0) { //prosta jest równoległa do osi Y
        pA = 1;
        pB = 0;
        pC = -pX_2;

      } else {
        pA = -(pY_2 - pY_1) / (pX_2 - pX_1);
        pB = 1;
        pC = (((pY_2 - pY_1) / (pX_2 - pX_1) * pX_1) - pY_1);
      } 
      pGeneralEquation_StraightLine = ((pA * xPoint.X) + (pB * xPoint.Y) + pC); // Ax+Bx+C

      return (int)pGeneralEquation_StraightLine;

    }

  }

}
