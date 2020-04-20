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
    Profile = 4,                                            //wielokąt jest profilem
    Sash = 5,                                               //wielokąt jest skrzydłem
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
    internal cAssemblyItem AssemblyItem { get { return mAssemblyItem; } set { mAssemblyItem = value; } }
    internal cPolygon Child { get { return mChild; } set { mChild = value; } }
    internal PolygonFunctionalityEnum CntPF { get { return mCntPF; } set { mCntPF = value; } } 
    internal int Index { get { return mIndex; } set { mIndex = value; } }
    internal cPolygon Parent { get { return mParent; } set { mParent = value; } }
    internal Dictionary<int, cSegment> Segments { get { return mSegments; } set { mSegments = value; } }
    internal string SegmentsList { get { return GetSegmentsList(); } }

    public cPolygon() {

      mSegments = new Dictionary<int, cSegment>();
      mCntPF = PolygonFunctionalityEnum.Undefined;

    }

    public cPolygon(int xIndex) {

      mSegments = new Dictionary<int, cSegment>();
      mIndex = xIndex;
      mCntPF = PolygonFunctionalityEnum.Undefined;

    }

    internal void AddSegment(cSegment xSegment) {
      //funkcja dodająca nowy bok do listy
      //xSegment - wybrany bok

      mSegments.Add(xSegment.Index, xSegment);

    }

    internal void CreateAssembly(int xWidth, cPolygon xPolygon) {
      //funkcja przeładowana
      //xWidth - szerokość profilu
      //xPolygon - wielokąt do przeprowadzenia assembly

      Dictionary<int, int> pC_Cln;

      pC_Cln = new Dictionary<int, int>();

      foreach (cSegment pSegment in xPolygon.Segments.Values) {
        pC_Cln.Add(pSegment.Index, 0);

      }

      CreateAssembly(xWidth, xPolygon, pC_Cln);

    }

    internal void CreateAssembly(int xWidth, cPolygon xPolygon, int xC) {
      //funkcja przeładowana
      //xWidth - szerokość profilu
      //xPolygon - wielokąt do przeprowadzenia assembly
      //xC - stała C dla każdego AssemblyItemu

      mAssembly = new cAssembly();
      mAssembly.Polygon_Parent = xPolygon;
      mAssembly.CreateMe(xWidth, xPolygon, xC);

    }

    internal void CreateAssembly(int xWidth, cPolygon xPolygon, Dictionary<int, int> xC_Cln) {
      //funkcja tworząca konstrukcję dla wielokąta
      //xWidth - szerokość profilu
      //xPolygon - poligon do przeprowadzenia assembly
      //xC_Cln - kolekcja stałych C - odległość od krawędzi elementu

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
      //funkcja zwracająca bok po jego indeksie
      //xIndex - indeks boku 

      int pSegmentIndex;
      int pCountMax;

      pCountMax = mSegments.Keys.Min() + mSegments.Count - 1;

      pSegmentIndex = mSegments.Keys.Min() + xIndex - 1;

      if (xIndex > pCountMax)
        pSegmentIndex = mSegments.Keys.Min();

      return mSegments[pSegmentIndex];

    }

    internal void FillMeByObject(cPolygon xPolygon) {
      //funkcja wypełniająca podstawowe dane według wybranego wielokąta
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
      //funkcja zwracająca kopie wielokąta (wypełniamy tylko podstawowe pola)

      cPolygon pPolygon;

      pPolygon = new cPolygon();

      pPolygon.FillMeByObject(this);

      return pPolygon;

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

      int pC;
      cLine pLine_SegmentOryg;
      cLine pLine_Parallel;
      cPoint pPoint;
      Dictionary<int, cLine> pCln;

      pCln = new Dictionary<int, cLine>();

      //tworzymy kolekcję prostych równoległych oddaloną o stałą C do środka wielokąta
      foreach(cSegment pSegment in mSegments.Values) {
        pC = pSegment.Polygon_Parent.Parent.Assembly.AssemblyItems[pSegment.Index].C;

        pLine_SegmentOryg = new cLine(pSegment);          //prosta pokrywająca się z bokiem
        pLine_SegmentOryg.Simplify(pLine_SegmentOryg);    //uproszczenie równania prostej
        pLine_Parallel = pLine_SegmentOryg.Get_Parallel(pC);       //prosta oddalona o C

        pLine_SegmentOryg = new cLine(pSegment.Segment_Before);   //prosta pokrywająca się z poprzednim bokiem
        pLine_SegmentOryg.Simplify(pLine_SegmentOryg);            //uproszczenie równania prostej

        pPoint = pLine_SegmentOryg.Get_PointFromCrossLines(pLine_Parallel);//punkt przecięcia się prostych

        if (pPoint.X >= pSegment.Segment_Before.Point.X && pPoint.X <= pSegment.Point.X || //jeśli punkt należy do wielokąta to dodajemy prostą
           pPoint.X <= pSegment.Segment_Before.Point.X && pPoint.X >= pSegment.Point.X) {
          if (pPoint.Y >= pSegment.Segment_Before.Point.Y && pPoint.Y <= pSegment.Point.Y ||
              pPoint.Y <= pSegment.Segment_Before.Point.Y && pPoint.Y >= pSegment.Point.Y) {
            pCln.Add(pSegment.Index, pLine_Parallel);

          } else {  //jeśli punkt nie należał to pobieramy prostą równoległą w drugą stronę
            pLine_SegmentOryg = new cLine(pSegment);
            pLine_SegmentOryg.Simplify(pLine_SegmentOryg);
            pLine_Parallel = pLine_SegmentOryg.Get_Parallel(-pC );

            pLine_SegmentOryg = new cLine(pSegment.Segment_Before);
            pLine_SegmentOryg.Simplify(pLine_SegmentOryg);

            pPoint = pLine_SegmentOryg.Get_PointFromCrossLines(pLine_Parallel);
            pCln.Add(pSegment.Index, pLine_Parallel);
          }
        } else {      //jeśli punkt nie należał to pobieramy prostą równoległą w drugą stronę
          pLine_SegmentOryg = new cLine(pSegment);
          pLine_SegmentOryg.Simplify(pLine_SegmentOryg);
          pLine_Parallel = pLine_SegmentOryg.Get_Parallel(-pC );

          pLine_SegmentOryg = new cLine(pSegment.Segment_Before);
          pLine_SegmentOryg.Simplify(pLine_SegmentOryg);

          pPoint = pLine_SegmentOryg.Get_PointFromCrossLines(pLine_Parallel);
          pCln.Add(pSegment.Index, pLine_Parallel);
        }
      }
      //przeniesienie punktów oryginalnych na punkty utworzone z przecięcia prostych
      foreach(cSegment pSegment in mSegments.Values) {
        pPoint = pCln[pSegment.Index].Get_PointFromCrossLines(pCln[pSegment.Segment_Before.Index]);
        pPoint = new cPoint(pPoint.X, pPoint.Y);
        pSegment.Point = pPoint;
        
      }
    }

    public Dictionary<int, cPolygon> Split_Polygon_By_Line(cLine xLine) {
      //funkcja zwracająca kolekcję podzielonych wielokątów prostą
      //xLine - równanie prostej dzielącej wielokąty

      Dictionary<int, cPolygon> pCln_Polygon;
      cPolygon pPolygon_A, pPolygon_B;
      cPoint pPoint;
      cSegment pSegmentNew;
      double pDistance;
      Dictionary<int, cPoint> pCln_Points;
      cLine pLine;

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

      pCln_Points = new Dictionary<int, cPoint>();
      
      //obliczenie punktów na wielokącie przeciętym prostą
      foreach (cSegment pSegment in mSegments.Values) {
        pLine = new cLine(pSegment);
        pLine.Simplify(pLine);
        pPoint = xLine.Get_PointFromCrossLines(pLine);
        if (pPoint == null) continue;               //jeśli prosta jest równoległa
        if (pPoint.X >= pSegment.Segment_Next.Point.X && pPoint.X <= pSegment.Point.X || //jeśli punkt należy do wielokąta to dodajemy prostą
            pPoint.X <= pSegment.Segment_Next.Point.X && pPoint.X >= pSegment.Point.X) {
          if (pPoint.Y >= pSegment.Segment_Next.Point.Y && pPoint.Y <= pSegment.Point.Y ||
              pPoint.Y <= pSegment.Segment_Next.Point.Y && pPoint.Y >= pSegment.Point.Y) {

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
          }
        }
      } //każdy bok wielokąta bazowego kopiujemy i sprawdzamy do którego wielokąta należy
      foreach (cSegment pSegment in mSegments.Values) {
        pSegmentNew = pSegment.Clone();
        pLine = new cLine(pCln_Points);
        pDistance = pLine.Get_DistanceToLine(pSegment.Point);
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

      double pAlfa, pCosAlfa;
      Dictionary<int, cPoint> pCln_Points;
      Dictionary<int, Tuple<double, double>> pCln;
      cSegment pSegment_New;
      cVector pVectorToPoint, pVectorOX;
      cPoint pBasePoint;
      int pCount;
      int pIdx;
      double pLength;
      Tuple<double, double> pTuple;

      //dodajemy do listy wszystkie punkty wielokąta
      pCln_Points = new Dictionary<int, cPoint>();
      foreach (cSegment pSegment_Base in xPolygon.Segments.Values) {
        pCln_Points[pSegment_Base.Index] = pSegment_Base.Point;

      }
      var pBasePt = pCln_Points.OrderBy(x => x.Value.Y).ThenBy(x => x.Value.X).First();

      pBasePoint = new cPoint(pBasePt.Value.X, pBasePt.Value.Y);
      pVectorOX = new cVector(pBasePoint, new cPoint (pBasePoint.X+1, pBasePoint.Y));
      pCln = new Dictionary<int, Tuple<double, double>>();
      
      pIdx = 1;
      foreach (cPoint pPoint in pCln_Points.Values) {
        pVectorToPoint = new cVector(pBasePoint, pPoint);
        pCosAlfa = pVectorOX.CosAlfa(pVectorOX, pVectorToPoint);
        pAlfa = Math.Round(((Math.Acos(pCosAlfa)) * 180 / Math.PI), 4);
        pLength = pVectorToPoint.Vector.Length;
        pTuple = new Tuple<double, double>(pAlfa, pLength);

        pCln.Add(pIdx, pTuple);
        pIdx++;

      }
      var pSortCLN = pCln.OrderBy(x => x.Value.Item1).ThenBy(x => x.Value.Item2).ToDictionary(x => x.Key, x => x.Value);
      pCount = pSortCLN.Values.Count;

      if (pSortCLN.Values.ElementAt(pCount - 1).Item1 == pSortCLN.Values.ElementAt(pCount - 2).Item1 &&
          pSortCLN.Values.ElementAt(pCount - 1).Item2 > pSortCLN.Values.ElementAt(pCount - 2).Item2)

      pSortCLN = pCln.OrderBy(x => x.Value.Item1).ThenByDescending(x => x.Value.Item2).ToDictionary(x => x.Key, x => x.Value);

      pIdx = 1;
      foreach (int i in pSortCLN.Keys) {
        pSegment_New = xPolygon.Segments[pCln_Points.Keys.ElementAt(i-1)];
        pSegment_New.Index = pIdx;
        xPolygon.Segments.Add(pIdx, pSegment_New);
        pIdx++;

      }
      pCount = xPolygon.Segments.Count / 2;
      for (int i = 1; i <= pCount; i++) {  
        xPolygon.Segments.Remove(xPolygon.Segments.Keys.ElementAt(0));

      }
    }
      
    internal bool IsInclude(cPoint xPoint) {
      //

      double pAlfa, pBeta;
      cVector pVector_SegmentThis, pVector_SegmentBefore, pVector_SegmentToPoint;
      bool pCheck;

      pCheck = false;

      //dla każdego boku porównujemy kąt między: (bokiem obecnym i bokiem poprzednim) oraz (bok poprzedni, a analizowany punkt)
      //jeżeli dla wszystkich nierówność dla każdego boku się zgadza to punkt jest wewnątrz wielokąta
      foreach (cSegment pSegment in mSegments.Values) {
        pVector_SegmentBefore = new cVector(pSegment.Point, pSegment.Segment_Before.Point);
        pVector_SegmentThis = new cVector(pSegment.Point, pSegment.Segment_Next.Point);
        pVector_SegmentToPoint = new cVector(pSegment.Point, xPoint);

        pAlfa = Math.Round((Math.Acos(pVector_SegmentBefore.CosAlfa(pVector_SegmentBefore, pVector_SegmentThis)) * 180 / Math.PI), 4);
        pBeta = Math.Round((Math.Acos(pVector_SegmentBefore.CosAlfa(pVector_SegmentBefore, pVector_SegmentToPoint)) * 180 / Math.PI), 4);

        if (pAlfa >= pBeta)
          pCheck = true;
        else {
          pCheck = false;
          break;
        }
      }

      return pCheck;

    }

  }
}
