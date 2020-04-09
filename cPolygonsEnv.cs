using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cPolygonsEnv {

    private Dictionary<int, cPolygon> mPolygons;            //lista Polygonów

    internal Dictionary<int, cPolygon> Polygons { get { return mPolygons; } set { mPolygons = value; } }

    public cPolygonsEnv(){

      mPolygons = new Dictionary<int, cPolygon>();
      
    }

    internal void AddPolygon(cPolygon xPolygon) {
      //funkcja dodająca wielokąt do listy
      //xPolygon - zadany wielokąt

      xPolygon.Index = GetEmptyIndex();
      mPolygons.Add(GetEmptyIndex(), xPolygon);
     
    }

    internal void AddPolygon(int xIndex, cPolygon xPolygon) {
      //funkcja dodająca wielokąt do listy
      //xPolygon - zadany wielokąt

      mPolygons.Add(xIndex, xPolygon);

    }

    internal void SplitPolygonVirtual_ByLine(cPolygon xPolygon, cLine xLine, int xC_Mullion) {
      //funkcja dzieląca wielokąt wirtualny na dwa za pomocą prostej
      //xPolygon - wielokąt do podziału
      //xStraight - prosta dzieląca
      //xC_Mullion - stała C dla słupka

      Dictionary<int, cPolygon> pPolygons;
      cPolygon pPolygon_A, pPolygon_B;
      Dictionary<int, int> pC_Cln_A, pC_Cln_B;
      int pWidht_Profile;
      int pIdx;

      //szerokość profilu (dla wszystkich obiektów taka sama)
      pWidht_Profile = xPolygon.Assembly.AssemblyItems[1].Width_Profile;

      pPolygons = xPolygon.Split_Polygon_By_Line(xLine);  //utworzenie kolekcji wielokątów z podziału prostą
      pPolygon_A = pPolygons[1];
      pPolygon_B = pPolygons[2];

      pC_Cln_A = Prepare_Cln_C(pPolygon_A, xLine, xC_Mullion);    //przygotowanie stałych C do wirtualanego Assembly
      pC_Cln_B = Prepare_Cln_C(pPolygon_B, xLine, xC_Mullion);

      pIdx = xPolygon.Index;
      mPolygons.Remove(xPolygon.Index);

      AddPolygon(pIdx, pPolygon_B);
      AddPolygon(pPolygon_A);

      pPolygon_A.CreateAssembly(pWidht_Profile, pPolygon_A, pC_Cln_A);
      pPolygon_B.CreateAssembly(pWidht_Profile, pPolygon_B, pC_Cln_B);

    }

    internal int GetEmptyIndex() {
      //funkcja zwracająca pierwszy wolny Index z listy mPolygons

      int pIndex;

      pIndex = mPolygons.Count + 1;

      return pIndex;

    }

    internal cPolygon GetPolygonVirtual() {
      //funkcja zwracająca pierwszy wirtualny Polygon

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
        return pPolygon;
      }

      return null;

    }

    internal Dictionary<int, cPolygon> GetPolygonVirtual_By_Point(cPoint xPoint) {
      //funkcja zwracająca wirtualny wielokąt w zależności od pozycji słupka UWAGA - może nie działać na ukosach!
      //xPoint - pozycja słupka

      Dictionary<int, cPolygon> pCln;
      int pIdx;

      pCln = new Dictionary<int, cPolygon>();
      pIdx = 1;

      //dla każdego wirtualnego wielokąta w projekcie sprawdzamy czy punkt należy do niego
      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
        if (pPolygon.Segments[1].Point.X <= xPoint.X && pPolygon.Segments[3].Point.X >= xPoint.X &&
            pPolygon.Segments[1].Point.Y <= xPoint.Y && pPolygon.Segments[3].Point.Y >= xPoint.Y) {
          pCln.Add(pIdx, pPolygon);
          pIdx++;
        }
      }

      return pCln;

    }

    internal Dictionary<int, cPolygon> GetPolygonsVirtual_Tangential_To_AxisSymmetry(cLine xLine) {
      //funkcja zwracająca kolekcję wirtualnych wielokątów, w których jeden bok pokrywa się z prostą
      //xLine - prosta względem której szukamy wielokątów

      Dictionary<int, cPolygon> pCln;
      int pIdx;
      cLine pLine;

      pCln = new Dictionary<int, cPolygon>();
      pIdx = 1;

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue; //sprawdzamy tylko wirtualne
        foreach (cSegment pSegment in pPolygon.Segments.Values) {
          pLine = new cLine(pSegment);
          pLine.Simplify(pLine);
          if (pLine.IsCover(xLine)) {  //jeśli prosta pokrywa się z bokiem to dodajemy
            pCln.Add(pIdx, pPolygon);
            pIdx++;
          }
        }
      }

      return pCln;

    }

    internal Dictionary<int, cPolygon> GetPolygonsVirtual_CrossedBy_AxisSymmetry(cLine xAxisSymmetry) {
      //funkcja zwracająca kolekcję wirtualnych wielokątów, które są przecięte osią słupka
      //xLine - prosta względem której szukamy wielokątów

      Dictionary<int, cPolygon> pCln;
      int pIdx;
      cLine pLine_Segment;
      cPoint pPoint;
      pCln = new Dictionary<int, cPolygon>();
      pIdx = 1;

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue; //sprawdzamy tylko wirtualne
        if (pPolygon.Child != null) continue;
        foreach (cSegment pSegment in pPolygon.Segments.Values) {
          pLine_Segment = new cLine(pSegment);
          pLine_Segment.Simplify(pLine_Segment);
          if (pLine_Segment.IsCover(xAxisSymmetry)) continue;   //jeśli prosta pokrywa się z bokiem szukamy dalej

          pPoint = pLine_Segment.Get_PointFromCrossLines(xAxisSymmetry);
          if (pPoint.X >= pSegment.Segment_Next.Point.X && pPoint.X <= pSegment.Point.X || //jeśli punkt należy do wielokąta to dodajemy prostą
              pPoint.X <= pSegment.Segment_Next.Point.X && pPoint.X >= pSegment.Point.X) {
            if (pPoint.Y >= pSegment.Segment_Next.Point.Y && pPoint.Y <= pSegment.Point.Y ||
                pPoint.Y <= pSegment.Segment_Next.Point.Y && pPoint.Y >= pSegment.Point.Y) {

              pCln.Add(pIdx, pPolygon);
              pIdx++;
              break;
            }
          }
           
        }
      }

      return pCln;

    }

    internal cPolygon GetPolygonMullion() {
      //funkcja zwracająca wielokąt pierwszego słupka

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.Mullion) continue;
        return pPolygon;
      }
      return null;

    }

    internal cPolygon GetPolygonOutline() {
      //funkcja zwracająca pierwszy wielokąt Outline

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameOutline) continue;
        return pPolygon;
      }
      return null;

    }

    internal cPolygon GetPolygonVirtual_WithoutChild() {
      //funkcja zwracająca pierwszy wielokąt wirtualny bez dziecka

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
          if (pPolygon.Child == null)
            return pPolygon;
      }
      return null;

    }

    internal cPolygon GetPolygonVirtual_WithChild() {
      //funkcja zwracająca pierwszy wielokąt wirtualny z dzieckiem

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
        if (pPolygon.Child != null)
          return pPolygon;
      }
      return null;

    }

    internal void CreatePolygon_Virtual(cPolygon xPolygon) {
      //funkcja tworząca wirtualny wielokąt
      //xPolygon - wielokąt, z którego ma zostać utworzony wirtualny

      cPolygon pPolygon_Virtual;
      int pWidth_Profile;
      int pC;

      pPolygon_Virtual = xPolygon.Clone();

      pPolygon_Virtual.Parent = xPolygon;    //rodzicem staje się wielokąt wzorcowy

      //szerokość, C  - dla wszystkich obiektów takie same
      pWidth_Profile = pPolygon_Virtual.Parent.Assembly.AssemblyItems[1].Width_Profile;
      pC = pPolygon_Virtual.Parent.Assembly.AssemblyItems[1].C;

      //utworzenie konstrukcji w wielokącie wirtualnym
      pPolygon_Virtual.CreateAssembly(pWidth_Profile, pPolygon_Virtual, pC);

      pPolygon_Virtual.CntPF = PolygonFunctionalityEnum.FrameVirtual;

      AddPolygon(pPolygon_Virtual);

    }

    internal void CreatePolygon_Mullion(Dictionary<int, cPolygon> xCln_Polygons, int xMullionPosition_X, int xMullionPosition_Y,
                                        int xMullionWidth, float xWidth_Profile, int xC) {
      //funkcja tworząca  wielokąt słupka - !!! Brakuje jeszcze AI !!!
      //xCln_Polygons - kolekcja wielokątów, z których będzie tworzony słupek
      //xMullionPosition_X - pozycja X słupka
      //xMullionPosition_Y - pozycja Y słupka
      //xMullionWidth - szerokość słupka
      //xWidth_Profile - szerokość profilu
      //xC - stała C słupka

      cPolygon pPolygon;

      pPolygon = new cPolygon();

      pPolygon.CntPF = PolygonFunctionalityEnum.Mullion;

      pPolygon.SetPolygonToMullion(xCln_Polygons, xMullionPosition_X, xMullionPosition_Y, xMullionWidth, xC);

      pPolygon.Organize_Segments(pPolygon);

      AddPolygon(pPolygon);

      pPolygon.Parent = xCln_Polygons[1].Parent.Parent;

    }

    internal void CreatePolygon_Sash(cPolygon xPolygon, int xWidth_Profile) {
      //funkcja tworząca wielokąt skrzydła wraz z konstrukcją
      //xPolygon - wielokąt, do którego wstawiamy skrzydło (Polygon_Sash)
      //xWidth_Profile - szerokość profilu skrzydła

      cPolygon pPolygon_Sash;

      xPolygon.AddChild();

      pPolygon_Sash = xPolygon.Child;
      pPolygon_Sash.CntPF = PolygonFunctionalityEnum.FrameOutline;
      pPolygon_Sash.Index = GetEmptyIndex();

      //zmniejszam boki wielokąta o stałą C
      pPolygon_Sash.SetSegmentsPointBy_C();

      pPolygon_Sash.CreateAssembly(xWidth_Profile, pPolygon_Sash);

    }


    private Dictionary<int, int> Prepare_Cln_C(cPolygon xPolygon, cLine xAxis_Symmetry_Mullion, int xC_Mullion) {
      //funkcja zwracająca kolekcję przygotowanych stałych C
      //xPolygon - wielokąt 
      //xAxis_Symmetry_Mullion - oś symetrii słupka
      //xC_Mullion - stała C dla słupka

      Dictionary<int, int> pC_Cln;
      int pIdx;
      cLine pLine;

      pC_Cln = new Dictionary<int, int>();

      pIdx = 1;
      //każdy bok sprawdzamy, czy pokrywa się z osią słupka
      foreach (cSegment pSegment in xPolygon.Segments.Values) {
        pLine = new cLine(pSegment);
        pLine.Simplify(pLine);
        if (pLine.IsCover(xAxis_Symmetry_Mullion)) //jeśli prosta pokrywa się z osią słupka wstawiamy C słupka
          pC_Cln[pIdx] = xC_Mullion;
        else
          pC_Cln[pIdx] = xPolygon.Parent.Assembly.AssemblyItems[1].C;  //w innym przypadku C profilu
        pIdx++;
      }
      return pC_Cln;

      }


    internal cPolygon GetPolygonVirtual_ByPoint(cPoint xPoint) {

      cPolygon pPolygon_Virtual;
      cLine pLine_SegmentThis, pLine_SegmentBefore, pLine_SegmentToPoint;
      cVector pVector_SegmentThis, pVector_SegmentBefore, pVector_SegmentToPoint;
      double pAlfa, pBeta, pBeta2;

      pPolygon_Virtual = new cPolygon();

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
        if (pPolygon.Child != null) continue;

        foreach (cSegment pSegment in pPolygon.Segments.Values) {



          pVector_SegmentBefore = new cVector(pSegment.Point, pSegment.Segment_Before.Point);
          pVector_SegmentThis = new cVector(pSegment.Point, pSegment.Segment_Next.Point);
          pVector_SegmentToPoint = new cVector(pSegment.Point, xPoint);

          pAlfa = (Math.Acos(pVector_SegmentBefore.CosAlfa(pVector_SegmentBefore, pVector_SegmentThis))) * 180 / Math.PI;
          pBeta = (Math.Acos(pVector_SegmentBefore.CosAlfa(pVector_SegmentBefore, pVector_SegmentToPoint))) * 180 / Math.PI;

         // pAlfa = (Math.Acos(pCos)) * 180 / Math.PI;

          /*          pLine_SegmentThis = new cLine(pSegment);
                    pLine_SegmentThis.Simplify(pLine_SegmentThis);
                    pLine_SegmentBefore = new cLine(pSegment.Segment_Before);
                    pLine_SegmentBefore.Simplify(pLine_SegmentBefore);         
                    pLine_SegmentToPoint = new cLine(pSegment.Point, xPoint);
                    pLine_SegmentToPoint.Simplify(pLine_SegmentToPoint);*/

          /*         pAlfa = pLine_SegmentThis.Get_Angle(pLine_SegmentBefore);
                   pBeta = pLine_SegmentToPoint.Get_Angle(pLine_SegmentThis);
                   pBeta2 = pLine_SegmentToPoint.Get_Angle(pLine_SegmentBefore);*/

          if (pAlfa < pBeta) { }


        }

        Console.WriteLine();

      }


      return pPolygon_Virtual;

    }

  }
}
