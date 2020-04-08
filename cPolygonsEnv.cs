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

    internal void SplitPolygonVirtual_ByLine(cPolygon xPolygon, cStraightLine xStraight, int xC_Mullion) {
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

      pPolygons = xPolygon.Split_Polygon_By_Line(xStraight);  //utworzenie kolekcji wielokątów z podziału prostą
      pPolygon_A = pPolygons[1];
      pPolygon_B = pPolygons[2];

      pC_Cln_A = Prepare_Cln_C(pPolygon_A, xStraight, xC_Mullion);    //przygotowanie stałych C do wirtualanego Assembly
      pC_Cln_B = Prepare_Cln_C(pPolygon_B, xStraight, xC_Mullion);

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

    internal Dictionary<int, cPolygon> GetPolygonsVirtual_By_MullionPositon(cPoint xPoint) {
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

    internal Dictionary<int, cPolygon> GetPolygonsVirtual_By_Line(cStraightLine xStraightLine) {
      //funkcja zwracająca kolekcję wirtualnych wielokątów, w których jeden bok pokrywa się z prostą
      //xStraightLine - prosta względem której szukamy wielokątów

      Dictionary<int, cPolygon> pCln;
      int pIdx;
      cStraightLine pStraightLine;

      pCln = new Dictionary<int, cPolygon>();
      pIdx = 1;

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue; //sprawdzamy tylko wirtualne
        foreach (cSegment pSegment in pPolygon.Segments.Values) {
          pStraightLine = new cStraightLine(pSegment);
          if (pStraightLine.IsCover(xStraightLine)) {  //jeśli prosta pokrywa się z bokiem to dodajemy
            pCln.Add(pIdx, pPolygon);
            pIdx++;
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


    private Dictionary<int, int> Prepare_Cln_C(cPolygon xPolygon, cStraightLine xAxis_Symmetry_Mullion, int xC_Mullion) {
      //funkcja zwracająca kolekcję przygotowanych stałych C
      //xPolygon - wielokąt 
      //xAxis_Symmetry_Mullion - oś symetrii słupka
      //xC_Mullion - stała C dla słupka

      Dictionary<int, int> pC_Cln;
      int pIdx;
      cStraightLine pStraightLine;

      pC_Cln = new Dictionary<int, int>();

      pIdx = 1;
      //każdy bok sprawdzamy, czy pokrywa się z osią słupka
      foreach (cSegment pSegment in xPolygon.Segments.Values) {
        pStraightLine = new cStraightLine(pSegment);
        if (pStraightLine.IsCover(xAxis_Symmetry_Mullion)) //jeśli prosta pokrywa się z osią słupka wstawiamy C słupka
          pC_Cln[pIdx] = xC_Mullion;
        else
          pC_Cln[pIdx] = xPolygon.Parent.Assembly.AssemblyItems[1].C;  //w innym przypadku C profilu
        pIdx++;
      }
      return pC_Cln;

      }

  }
}
