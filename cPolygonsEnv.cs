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
      //funkcja dodająca nowy segment do listy
      //xPolygon - zadany Polygon

      xPolygon.Index = GetEmptyIndex();
      mPolygons.Add(GetEmptyIndex(), xPolygon);
     
    }

    internal void AddPolygon(int xIndex, cPolygon xPolygon) {
      //funkcja dodająca nowy segment do listy
      //xPolygon - zadany Polygon

      mPolygons.Add(xIndex, xPolygon);

    }

    internal void Split_Polygon(int xMullionPosition_X, int xMullionPosition_Y, cPolygon xPolygon) {
      // !!!UWAGA!!! fukncja zostaje do puki druga nie będzie działać poprawnie!!!

      //fukncja  dzieląca Polygon na 2 w miejscu słupka 
      //xMullionPosition_X - pozycja X słupka
      //xPolygon - wielokąt do podziału

      Dictionary <int, cPolygon> pPolygons;
      cPolygon pPolygon_A, pPolygon_B;
      Dictionary<int, int> pC_Cln_A, pC_Cln_B;
      int pWidht_Profile;
      int pC;
      int pIdx;

      pC = 0;
      //szerokość profilu (dla wszystkich obiektów taka sama)
      pWidht_Profile = xPolygon.Assembly.AssemblyItems[1].Width_Profile;

      pC_Cln_A = new Dictionary<int, int>();
      pC_Cln_B = new Dictionary<int, int>();
      
      if (xMullionPosition_Y == 0) {              // jeśli słupek jest pionowy
        //szukamy C słupka po wszystkich Mullion i jego osi
        foreach (cPolygon pPolygon in mPolygons.Values) {
          if (pPolygon.CntPF != PolygonFunctionalityEnum.Mullion) continue;
          if (pPolygon.AssemblyItem.Axis_Symmetry == xMullionPosition_X) {
            pC = pPolygon.AssemblyItem.C;
            break;
          }
        } //dzielimy wielokąt po szerokości

        pPolygons = xPolygon.Split_PolygonByWidth(xMullionPosition_X);

        pPolygon_A = pPolygons[1];
        pPolygon_B = pPolygons[2];
        
        //kolekcja C Polygon_A
        pC_Cln_A[1] = xPolygon.Assembly.AssemblyItems[1].C;
        pC_Cln_A[2] = pC;
        pC_Cln_A[3] = xPolygon.Assembly.AssemblyItems[3].C;
        pC_Cln_A[4] = xPolygon.Assembly.AssemblyItems[4].C;

        //kolekcja C Polygon_B
        pC_Cln_B[1] = xPolygon.Assembly.AssemblyItems[1].C;
        pC_Cln_B[2] = xPolygon.Assembly.AssemblyItems[2].C;
        pC_Cln_B[3] = xPolygon.Assembly.AssemblyItems[3].C;
        pC_Cln_B[4] = pC;

        //usuwamy startowy wielokąt, dodajemy dwa nowe, wykorzystując numer starego
        pIdx = xPolygon.Index;
        mPolygons.Remove(xPolygon.Index);

        AddPolygon(pIdx, pPolygon_A);
        AddPolygon(pPolygon_B);

        //dla obu nowych wielokątów tworzymy Assembly z kolekcją C
        pPolygon_A.CreateAssembly(pWidht_Profile, pPolygon_A, pC_Cln_A);
        pPolygon_A.Assembly.AssemblyItems[2].Polygon.CntPF = PolygonFunctionalityEnum.Mullion;
        pPolygon_A.Assembly.AssemblyItems[3].Polygon.CntPF = PolygonFunctionalityEnum.Mullion;

        pPolygon_B.CreateAssembly(pWidht_Profile, pPolygon_B, pC_Cln_B);
        pPolygon_B.Assembly.AssemblyItems[1].Polygon.CntPF = PolygonFunctionalityEnum.Mullion;
        pPolygon_B.Assembly.AssemblyItems[4].Polygon.CntPF = PolygonFunctionalityEnum.Mullion;

      } else if (xMullionPosition_X == 0) {           // jeśli słupek jest poziomy
        //szukamy C słupka po wszystkich Mullion i jego osi
        foreach (cPolygon pPoly in mPolygons.Values) {
          if (pPoly.CntPF != PolygonFunctionalityEnum.Mullion) continue;
          if (pPoly.AssemblyItem.Axis_Symmetry == xMullionPosition_Y) {
            pC = pPoly.AssemblyItem.C;
            break;
          }
        } //dzielimy wielokąt po wysokości
        pPolygons = xPolygon.Split_PolygonByHeight(xMullionPosition_Y);

        pPolygon_A = pPolygons[1];
        pPolygon_B = pPolygons[2];

        //kolekcja C Polygon_A
        pC_Cln_A[1] = xPolygon.Assembly.AssemblyItems[1].C;
        pC_Cln_A[2] = xPolygon.Assembly.AssemblyItems[2].C;
        pC_Cln_A[3] = pC;
        pC_Cln_A[4] = xPolygon.Assembly.AssemblyItems[4].C;

        //kolekcja C Polygon_B
        pC_Cln_B[1] = pC;
        pC_Cln_B[2] = xPolygon.Assembly.AssemblyItems[2].C;
        pC_Cln_B[3] = xPolygon.Assembly.AssemblyItems[3].C;
        pC_Cln_B[4] = xPolygon.Assembly.AssemblyItems[4].C;

        //usuwamy startowy wielokąt, dodajemy dwa nowe, wykorzystując numer starego
        pIdx = xPolygon.Index;
        mPolygons.Remove(xPolygon.Index);

        AddPolygon(pIdx, pPolygon_A);
        AddPolygon(pPolygon_B);

        //dla obu nowych wielokątów tworzymy Assembly z kolekcją C
        pPolygon_A.CreateAssembly(pWidht_Profile, pPolygon_A, pC_Cln_A);
        pPolygon_A.Assembly.AssemblyItems[3].Polygon.CntPF = PolygonFunctionalityEnum.Mullion;
        pPolygon_A.Assembly.AssemblyItems[4].Polygon.CntPF = PolygonFunctionalityEnum.Mullion;

        pPolygon_B.CreateAssembly(pWidht_Profile, pPolygon_B, pC_Cln_B);
        pPolygon_B.Assembly.AssemblyItems[1].Polygon.CntPF = PolygonFunctionalityEnum.Mullion;
        pPolygon_B.Assembly.AssemblyItems[2].Polygon.CntPF = PolygonFunctionalityEnum.Mullion;

      } else { }

    }

    internal void SplitPolygonVirtual_ByLine(cPolygon xPolygon, int xMullionPosition_X, int xMullionPosition_Y) {
      // UWAGA FUNKCJA NIE SKOŃCZONA!!! - MO

      Dictionary<int, cPolygon> pPolygons;
      cPolygon pPolygon_A, pPolygon_B;
      Dictionary<int, int> pC_Cln_A, pC_Cln_B;
      int pWidht_Profile;
      int pIdx;
      cVector xVector_Mullion;
      cPoint pPt_Vector;
      cStraightLine pStraightLine;

      //szerokość profilu (dla wszystkich obiektów taka sama)
      pWidht_Profile = xPolygon.Assembly.AssemblyItems[1].Width_Profile;

      pPt_Vector = new cPoint(xMullionPosition_X, xMullionPosition_Y);

      xVector_Mullion = new cVector(1, 0);

      pStraightLine = new cStraightLine(0, 1, -700);

      pPolygons = xPolygon.Split_PolygonByStreightLine(pStraightLine);
      pPolygon_A = pPolygons[1];
      pPolygon_B = pPolygons[2];

      pC_Cln_A = Prepare_Cln_C(pPolygon_A, xVector_Mullion);
      pC_Cln_B = Prepare_Cln_C(pPolygon_B, xVector_Mullion);

      pIdx = xPolygon.Index;
      mPolygons.Remove(xPolygon.Index);

      AddPolygon(pIdx, pPolygon_B);
      AddPolygon(pPolygon_A);

      pPolygon_A.CreateAssembly(pWidht_Profile, pPolygon_A, pC_Cln_A);
      pPolygon_B.CreateAssembly(pWidht_Profile, pPolygon_B, pC_Cln_B);

      foreach (cPolygon pPolygon_Child in pPolygons.Values) {
        foreach (cAssemblyItem pAssemblyItem in pPolygon_Child.Assembly.AssemblyItems.Values) {

          foreach (cPolygon pPolygon in mPolygons.Values) {
            if (pPolygon.CntPF == PolygonFunctionalityEnum.Mullion) {
              if (pPolygon.AssemblyItem.Axis_Symmetry == xVector_Mullion.X ||
                  pPolygon.AssemblyItem.Axis_Symmetry == xVector_Mullion.Y) {

                pAssemblyItem.Polygon.CntPF = PolygonFunctionalityEnum.Mullion;
                pAssemblyItem.AssemblyItem_Next.Polygon.CntPF = PolygonFunctionalityEnum.Mullion;

              }
            }
          }
        }
      }
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

    internal Dictionary<int, cPolygon> GetPolygonsVirtual_By_MullionPositon(int xMullionPosition_X, int xMullionPosition_Y) {
      //funkcja zwracająca pierwszy wirtualny Polygon

      Dictionary<int, cPolygon> pCln;
      int pIdx;

      pCln = new Dictionary<int, cPolygon>();
      pIdx = 1;
      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
        if (pPolygon.Segments[1].Point.X <= xMullionPosition_X && pPolygon.Segments[3].Point.X >= xMullionPosition_X &&
            pPolygon.Segments[1].Point.Y <= xMullionPosition_Y && pPolygon.Segments[3].Point.Y >= xMullionPosition_Y) {
          pCln.Add(pIdx, pPolygon);
          pIdx++;
        }
      }

      return pCln;

    }

    internal cPolygon GetPolygonMullion() {
      //funkcja zwracająca Polygon pierwszego słupeka

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.Mullion) continue;
        return pPolygon;
      }

      return null;

    }

    internal cPolygon GetPolygonOutline() {
      //funkcja zwracająca pierwszy Polygon_Outline

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameOutline) continue;
        return pPolygon;
      }

      return null;

    }

    internal cPolygon GetPolygonVirtual_WithoutChild() {
      //znajduję pierwszy virtual bez dziecka

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
          if (pPolygon.Child == null)
            return pPolygon;
      }
      return null;

    }

    internal cPolygon GetPolygonVirtual_WithChild() {
      //znajduję pierwszy virtual bez dziecka

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

    internal void CreatePolygon_Mullion(Dictionary<int, cPolygon> xCln_Polygons, int xMullionPosition_X, int xMullionPosition_Y, int xMullionWidth, float xWidth_Profile, int xC) {
      //funkcja tworząca  wielokąt
      //xPolygon - wielokąt, z którego ma zostać utworzony Polygon_Mullion
      //xMullionPosition_X - pozycja X słupka
      //xMullionWidth - szerokość słupka
      //xWidth_Profile - szerokość profilu
      //xC - odległość od osi elementu

      cPolygon pPolygon;

      pPolygon = new cPolygon();

      pPolygon.CntPF = PolygonFunctionalityEnum.Mullion;

      pPolygon.SetPolygonToMullion(xCln_Polygons, xMullionPosition_X, xMullionPosition_Y, xMullionWidth, xC);

      pPolygon.Organize_Segments(pPolygon);

      AddPolygon(pPolygon);

      pPolygon.Parent = xCln_Polygons[1].Parent.Parent;


    }

    internal void CreatePolygon_Sash(cPolygon xPolygon, int xWidth_Profile) {
      //funkcja tworząca wielokąt skrzydła wraz z assembly
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


    private Dictionary<int, int> Prepare_Cln_C(cPolygon xPolygon_Child, cVector xVector_Mullion) {


      Dictionary<int, int> pC_Cln;
      int pIdx;

      pC_Cln = new Dictionary<int, int>();

      pIdx = 1;
      foreach (cSegment pSegment in xPolygon_Child.Segments.Values) {

        foreach (cPolygon pPolygon in mPolygons.Values) {
          if (pPolygon.CntPF == PolygonFunctionalityEnum.Mullion) {
            if (pPolygon.AssemblyItem.Axis_Symmetry == xVector_Mullion.X ||
                pPolygon.AssemblyItem.Axis_Symmetry == xVector_Mullion.Y) {
              pC_Cln[pIdx] = pPolygon.AssemblyItem.C;
              break;
            }
          } else
            pC_Cln[pIdx] = xPolygon_Child.Parent.Assembly.AssemblyItems[pIdx].C;
        }
        pIdx++;
         
      }

      return pC_Cln;

      }

  }
}
