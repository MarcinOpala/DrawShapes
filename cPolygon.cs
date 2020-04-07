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

      pCountMax = mSegments.Keys.Min() + mSegments.Count - 1;

      pSegmentIndex = mSegments.Keys.Min() + xIndex - 1;

      if (xIndex > pCountMax)
        pSegmentIndex = mSegments.Keys.Min();

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

    internal void SetPolygonToMullion(Dictionary<int, cPolygon> xCln_Polygons, int xMullionPosition_X,
                                      int xMullionPosition_Y, int xMullionWidth, int xC) {
      //funkcja ustawiająca poszczególne parametry na parametry typowe dla Polygon_Mullion - pionowy
      //xPolygon - Polygon bazowy
      //xMullionPosition_X - współrzędna X osi słupka
      //xMullionWidth - szerokość słupka
      //xWidth_Profile - szerokość profilu
      //xC - odległość C dla słupka

      cAssemblyItem pAssemblyItem;
      cSegment pSegment;
      cPolygon pPolygon_A, pPolygon_B;
      cStraightLine pStraightLine, pStraightLine_A, pStraightLine_B, pStraightLine_Segment;
      int pIdx;
      cPoint pPoint;

      pPolygon_A = xCln_Polygons[1];
      pPolygon_B = xCln_Polygons[2];
      pIdx = 0;
      
      foreach (cSegment pSegment_A in pPolygon_A.Segments.Values) {
        foreach (cSegment pSegment_B in pPolygon_B.Segments.Values) {
          if (pSegment_A.Point.X == pSegment_B.Point.X && pSegment_A.Point.Y == pSegment_B.Point.Y) {
            pSegment = new cSegment(pSegment_A.Point, pIdx+20);
            AddSegment(pSegment);
            pSegment.Polygon_Parent = this;
            pIdx++;
          }
        }
      }
      pStraightLine = new cStraightLine(mSegments[20].Point, mSegments[21].Point);

      foreach (cSegment pSegment_A in pPolygon_A.Segments.Values) {

        pPoint = new cPoint();
        pStraightLine_Segment = new cStraightLine(pSegment_A);

        pStraightLine_A = pStraightLine.Get_Parallel((xMullionWidth*1000));

        pPoint = pStraightLine_A.Get_PointFromCrossLines(pStraightLine_Segment);
        if (pPoint == null) continue;                           //jeśli prosta jest równoległa

        pSegment = new cSegment(pPoint, pIdx+20);
        AddSegment(pSegment);
        pSegment.Polygon_Parent = this;
        pIdx++;
      }
      foreach (cSegment pSegment_B in pPolygon_B.Segments.Values) {

        pPoint = new cPoint();
        pStraightLine_Segment = new cStraightLine(pSegment_B);

        pStraightLine_B = pStraightLine.Get_Parallel(-(xMullionWidth *1000));

        pPoint = pStraightLine_B.Get_PointFromCrossLines(pStraightLine_Segment);
        if (pPoint == null) continue;                           //jeśli prosta jest równoległa

        pSegment = new cSegment(pPoint, pIdx+20);
        pSegment.Polygon_Parent = this;
        AddSegment(pSegment);
        pIdx++;
      }

      pAssemblyItem = new cAssemblyItem();
    //  pAssemblyItem.CreateAssemblyItem_Mullion(this, xMullionWidth, xC, xMullionPosition_X, xMullionPosition_Y);
      pAssemblyItem.Axis_Symmetry = pStraightLine;

      mAssemblyItem = pAssemblyItem;
      mParent = this.Clone();

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

      cVector pVector_1, pVector_2;
      cVector pC_Vector;
      int pC_1, pC_2;
      double pC, pY;
      double pCos, pSin;
      double pAlfa, pAngleInRadian;
      cAssemblyItem pAssemblyItem;
      cStraightLine pStraightLine, pStraightLine_2;
      cSegment pSegment_Oryginal;

      float pOffset_X, pOffset_Y;
      
      foreach (cSegment pSegment in mSegments.Values) {

        pC_1 = pSegment.Polygon_Parent.Parent.Assembly.AssemblyItems[pSegment.Index].C;
        pC_2 = pSegment.Polygon_Parent.Parent.Assembly.AssemblyItems[pSegment.Segment_Before.Index].C;

        pSegment_Oryginal = pSegment.Polygon_Parent.Parent.Segments[pSegment.Index];
        pStraightLine = new cStraightLine(pSegment_Oryginal.Segment_Before);

        pSegment.CalculatePointOffset(pSegment, pC_1, pC_2, pStraightLine, out pOffset_X, out pOffset_Y);



        pSegment.Point.X += pOffset_X;
        pSegment.Point.Y += pOffset_Y;

        /*     pAssemblyItem = pSegment.Polygon_Parent.Parent.Assembly.AssemblyItems[pSegment.Index];
             pSegment_Oryginal = pSegment.Polygon_Parent.Parent.Segments[pSegment.Index];
             pC = pAssemblyItem.C;
             pStraightLine_1 = new cStraightLine(pSegment_Oryginal.Segment_Before);
             pStraightLine_2 = new cStraightLine(pSegment_Oryginal);

             pAlfa = pStraightLine_1.Get_Angle(pStraightLine_2);
             pAngleInRadian = (pAlfa * (Math.PI)) / 180;
             pCos = Math.Cos(pAngleInRadian);
             pSin = Math.Sin(pAngleInRadian);
             pVector_1 = new cVector(pC * pCos, pC*pSin);


             pAssemblyItem = pSegment.Polygon_Parent.Parent.Assembly.AssemblyItems[pSegment.Segment_Before.Index];
             pC = pAssemblyItem.C;
             pVector_2 = new cVector(pC * pSin, pC * pCos);

             pC_Vector = pVector_1.AddVectors(pVector_1, pVector_2);

             pSegment.Point.X += (float)pC_Vector.X;
             pSegment.Point.Y += (float)pC_Vector.Y;

             pY = 0;*/
      }

    }


    public Dictionary<int, cPolygon> Split_PolygonByStreightLine(cStraightLine xStraightLine) {
      // !!!  UWAGA FUNKJCA NIE DZIAŁA JEST W TRAKCIE TWORZENIA!!!
      //funkcja zwracająca kolekcję podzielonych wielokątów prostą stworząną z wektora
      //

      Dictionary<int, cPolygon> pCln_Polygon;
      cPolygon pPolygon_A, pPolygon_B;
      cPoint pPoint;
      float pWidth, pHeight;
      cSegment pSegmentNew;
      double pDistance;
      Dictionary<int, cPoint> pCln_Points;
      cStraightLine pStraightLine;

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

      int pIdx = 0;
      pWidth = mSegments[2].Point.X - mSegments[1].Point.X;     //szerokość wielokąta - będzie działać tylko dla prostokątów
      pHeight = mSegments[3].Point.Y - mSegments[2].Point.Y;    //wysokość wielokąta

      pCln_Points = new Dictionary<int, cPoint>();
      
      //obliczenie punktów na wielokącie przeciętym prostą
      foreach (cSegment pSegment in mSegments.Values) {
        pStraightLine = new cStraightLine(pSegment);
        pPoint = xStraightLine.Get_PointFromCrossLines(pStraightLine);
        if (pPoint == null) continue;               //jeśli prosta jest równoległa
        if (pPoint.X > pWidth || pPoint.X < 0 ||    //jeśli prosta przecina drugą prostą poza obszarem wielokąta
            pPoint.Y > pHeight || pPoint.Y < 0)
          continue;
        pIdx++;
        pCln_Points[pIdx] = pPoint;

        //Polygon_A
        pSegmentNew = new cSegment(pPoint, pPolygon_A.Segments.Count + 20);    //uzyskane punkty dodajemy do nowych poligonów ...
        pSegmentNew.Polygon_Parent = pPolygon_A;                               //... +20 dla łatwiejszego porządkowania w Organize_Segment()
        pPolygon_A.AddSegment(pSegmentNew);
        //Polygon_B
        pSegmentNew = new cSegment(pPoint, pPolygon_B.Segments.Count + 20);
        pSegmentNew.Polygon_Parent = pPolygon_B;
        pPolygon_B.AddSegment(pSegmentNew);

      } //każdy bok wielokąta bazowego kopiujemy i sprawdzamy do którego wielokąta należy
      foreach (cSegment pSegment in mSegments.Values) {
        pSegmentNew = pSegment.Clone();
        pStraightLine = new cStraightLine(pCln_Points);
        pDistance = pStraightLine.Get_DistanceToLine(pSegment.Point);
        if (pDistance > 0) { //bok leży powyżej prostej dzielącej wielokąt
          pSegmentNew.Polygon_Parent = pPolygon_B;
          pSegmentNew.Index = pPolygon_B.Segments.Count + 20; //+20 dla łatwiejszego porządkowania w Organize_Segment()
          pPolygon_B.AddSegment(pSegmentNew);

        } else {             //bok leży poniżej prostej dzielącej wielokąt (w tym przypadku nie możliwe, żęby leżał na prostej)
          pSegmentNew.Polygon_Parent = pPolygon_A;
          pSegmentNew.Index = pPolygon_A.Segments.Count + 20;
          pPolygon_A.AddSegment(pSegmentNew);
        }
      }   //uporządkowanie boków zgodnie z całym projektem
      Organize_Segments(pPolygon_A);
      Organize_Segments(pPolygon_B);

      pCln_Polygon = new Dictionary<int, cPolygon>(); //dodanie wielokątów utworzonych z podziału bazowego
      pCln_Polygon.Add(1, pPolygon_B);    
      pCln_Polygon.Add(2, pPolygon_A);

      return pCln_Polygon;

    }

    internal void Organize_Segments(cPolygon xPolygon) {
      //funkcja porządkująca boki w wielokącie.
      //Pierwszy najbliższy (0; 0), kolejne przeciwnie do ruchu zegara 
      //xPolygon - wielokąt do uporządkowania

      double pDistance;
      Dictionary<int, cPoint> pCln_Points;
      int pIdx_Cln_Points; 
      int pNewIdx_Segment;
      cSegment pSegment;
      int pCountSegments;
      cStraightLine pStraightLine;
      cPolygon pPolygon;

      //pPolygon = new cPolygon()

      //dodajemy do listy wszystkie punkty wielokąta
      pCln_Points = new Dictionary<int, cPoint>();
      foreach (cSegment pSegment_Base in xPolygon.Segments.Values) {
        pCln_Points[pSegment_Base.Index] = pSegment_Base.Point;
      }
      //wybieramy pierwszy punkt (położony najbliżej 0;0 ) i usuwamy go z listy
      pNewIdx_Segment = 1;
      var dict = pCln_Points.OrderBy(x => x.Value.X).ThenBy(x => x.Value.Y).ToDictionary(x => x.Key, x => x.Value);   //sortowanie listy: najmniejszy X, Y
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
          pStraightLine = new cStraightLine(pSegment_Parent);
          pDistance = pStraightLine.Get_DistanceToLine(pPoint);

          if (pDistance == 0 ) {        //jeśli prawda to pPoint leży na prostej
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
        xPolygon.Segments.Remove(i+19);
        xPolygon.Segments[i].Index = i;
      }
    }

  }

}
