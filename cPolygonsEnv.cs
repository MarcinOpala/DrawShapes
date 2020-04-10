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

    internal void CreatePolygon_Mullion(Dictionary<int, cPolygon> xCln_PolygonsVirtual, Dictionary<int, cPolygon> xCln_PolygonsMullion,
                                        int xMullionWidth, int xC) {
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

      pPolygon.SetPolygonToMullion(xCln_PolygonsVirtual, xCln_PolygonsMullion, xMullionWidth, xC);

      

      AddPolygon(pPolygon);

      pPolygon.Parent = xCln_PolygonsVirtual[1].Parent.Parent; //rodzicem zostaje Outframe (rodzic wirtualki)

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
      //funkcja zwracająca wielokąt wirtualny za pomocą wskazanego punktu
      //xPoint - punkt na płaszczyźnie do analizowania

      cPolygon pPolygon_Virtual;

      pPolygon_Virtual = new cPolygon();

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue; //sprawdzamy tylko wirtualne
        if (pPolygon.Child != null) continue;                                  //sprawdzamy bez wstawionych skrzydeł

        pPolygon.IsInclude(xPoint); // sprawdzenie czy wielokąt zawiera w sobie punkt
        {
          //TODO

        }
      }

      return pPolygon_Virtual;

    }

    internal Dictionary<int, cPolygon> GetPolygonsMullion_Tangential_To_PolygonsVirtual(Dictionary<int, cPolygon> xCln_PolygonsVirtual) {
      //funkcja zwracająca kolekcję wielokątów typu słupek, których oś symetrii jest styczna do boku wielokąta wirtualnego
      //xLine - prosta względem której szukamy wielokątów

      Dictionary<int, cPolygon> pCln;
      cLine pLine, pLine_Axis_Symmetry;

      pCln = new Dictionary<int, cPolygon>();

      foreach (cPolygon pPolygonVirtual in xCln_PolygonsVirtual.Values) {
        foreach (cPolygon pPolygon_Env in mPolygons.Values) {
          if (pPolygon_Env.CntPF != PolygonFunctionalityEnum.Mullion) continue; //sprawdzamy tylko słupki
          pLine_Axis_Symmetry = pPolygon_Env.AssemblyItem.Axis_Symmetry;

          foreach (cSegment pSegment in pPolygonVirtual.Segments.Values) {
            pLine = new cLine(pSegment);
            pLine.Simplify(pLine);
            if (pLine.IsCover(pLine_Axis_Symmetry)) {   //jeśli prosta pokrywa się z bokiem 
              if (pCln.Count == 0)
                pCln.Add(pPolygon_Env.Index, pPolygon_Env);

              if (pCln.ContainsKey(pPolygon_Env.Index)) continue;     //jeśli słupek już jest dodany
                  
              pCln.Add(pPolygon_Env.Index, pPolygon_Env);

            }
          }
        }
      }

      return pCln;

    }

  }
}
