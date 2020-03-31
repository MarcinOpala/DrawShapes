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

      mPolygons.Add(xPolygon.Index, xPolygon);
     

    }

    internal void Split_Polygon(int xMullionPosition_X, int xMullionPosition_Y, cPolygon xPolygon) {
      //fukncja  dzieląca Virtual Polygon na 2 w miejscu słupka 
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



      //tworzymy dwa wielokąty
      if (xMullionPosition_Y == 0) {


        foreach (cPolygon pPoly in mPolygons.Values) {
          if (pPoly.CntPF != PolygonFunctionalityEnum.Mullion) continue;
          if (pPoly.AssemblyItem.Axis_Y == xMullionPosition_X) {
            pC = pPoly.AssemblyItem.C;
            break;
          }
        }

        pPolygons = xPolygon.Split_PolygonByWidth(xMullionPosition_X);

        pPolygon_A = pPolygons[1];
        pPolygon_B = pPolygons[2];

        
        pC_Cln_A[1] = xPolygon.Assembly.AssemblyItems[1].C;
        pC_Cln_A[2] = pC;
        pC_Cln_A[3] = xPolygon.Assembly.AssemblyItems[3].C;
        pC_Cln_A[4] = xPolygon.Assembly.AssemblyItems[4].C;

        
        pC_Cln_B[1] = xPolygon.Assembly.AssemblyItems[1].C;
        pC_Cln_B[2] = xPolygon.Assembly.AssemblyItems[2].C;
        pC_Cln_B[3] = xPolygon.Assembly.AssemblyItems[3].C;
        pC_Cln_B[4] = pC;


        pIdx = xPolygon.Index;
        //usuwamy startowy wielokąt
        mPolygons.Remove(xPolygon.Index);

        mPolygons.Add(pIdx, pPolygon_A);
        mPolygons.Add(GetEmptyIndex(), pPolygon_B);

      }

      else if (xMullionPosition_X == 0) {

        foreach (cPolygon pPoly in mPolygons.Values) {
          if (pPoly.CntPF != PolygonFunctionalityEnum.Mullion) continue;
          if (pPoly.AssemblyItem.Axis_X == xMullionPosition_Y) {
            pC = pPoly.AssemblyItem.C;
            break;
          }
        }

        pPolygons = xPolygon.Split_PolygonByHeight(xMullionPosition_Y);

        pPolygon_A = pPolygons[1];
        pPolygon_B = pPolygons[2];

        mPolygons.Add(GetEmptyIndex(), pPolygon_A);
        pC_Cln_A[1] = xPolygon.Assembly.AssemblyItems[1].C;
        pC_Cln_A[2] = xPolygon.Assembly.AssemblyItems[2].C;
        pC_Cln_A[3] = pC;
        pC_Cln_A[4] = xPolygon.Assembly.AssemblyItems[4].C;

        mPolygons.Add(GetEmptyIndex(), pPolygon_B);
        pC_Cln_B[1] = pC;
        pC_Cln_B[2] = xPolygon.Assembly.AssemblyItems[2].C;
        pC_Cln_B[3] = xPolygon.Assembly.AssemblyItems[3].C;
        pC_Cln_B[4] = xPolygon.Assembly.AssemblyItems[4].C;

        pIdx = xPolygon.Index;
        //usuwamy startowy wielokąt
        mPolygons.Remove(xPolygon.Index);
      }
      else {
        pPolygon_A = null;
        pPolygon_B = null;

      }




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

    internal cPolygon GetPolygonVirtual_By_MullionPositon(int xMullionPosition_X) {
      //funkcja zwracająca pierwszy wirtualny Polygon

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
        if (pPolygon.Segments[1].Point.X <= xMullionPosition_X && pPolygon.Segments[2].Point.X >= xMullionPosition_X)
        return pPolygon;
      }

      return null;

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

      pPolygon_Virtual.Index = GetEmptyIndex();

      pPolygon_Virtual.Parent = xPolygon;                   //rodzicem staje się wielokąt wzorcowy


      //szerokość profilu (dla wszystkich obiektów taka sama)
      pWidth_Profile = pPolygon_Virtual.Parent.Assembly.AssemblyItems[1].Width_Profile;


      pC = pPolygon_Virtual.Parent.Assembly.AssemblyItems[1].C;

      //utworzenie konstrukcji w wielokącie wirtualnym
      pPolygon_Virtual.CreateAssembly(pWidth_Profile, pPolygon_Virtual, pC);

      pPolygon_Virtual.CntPF = PolygonFunctionalityEnum.FrameVirtual;

      AddPolygon(pPolygon_Virtual);

    }

    internal void CreatePolygon_Mullion(cPolygon xPolygon, int xMullionPosition_X, int xMullionPosition_Y, int xMullionWidth, float xWidth_Profile, int xC) {
      //funkcja tworząca wirtualny wielokąt
      //xPolygon - wielokąt, z którego ma zostać utworzony Polygon_Mullion
      //xMullionPosition_X - pozycja X słupka
      //xMullionWidth - szerokość słupka
      //xWidth_Profile - szerokość profilu
      //xC - odległość od osi elementu

      cPolygon pPolygon;

      pPolygon = xPolygon.Clone();

      //ag - przenieść nadawanie indeksu do Addpolygon
      pPolygon.Index = GetEmptyIndex();
      pPolygon.CntPF = PolygonFunctionalityEnum.Mullion;

      //zmieniam wielokąt w kształt słupka z jego właściwościami
      pPolygon.SetPolygonToMullion_Vertical(xPolygon, xMullionPosition_X, xMullionWidth, xWidth_Profile, xC);

     // pPolygon.SetPolygonToMullion_Horizontal(xPolygon, xMullionPosition_Y, xMullionWidth, xWidth_Profile, xC);

      AddPolygon(pPolygon);

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

    private int Find_C_NearestItem(cPolygon xPolygon, int xIndex) {
      //funkcja zwracająca stałą C najbliższego sąsiada
      //xPolygon - wielokąt dla którego szukamy sąsiadów
      //xIndex - indeks boku, który jest brany pod uwagę

      Dictionary<int, int> pC_Cln;
      Dictionary<int, int> pDistance_Cln;
      int pC;
      int pDistance;
      int pIndex;
      int pDistanceKey;

      pC_Cln = new Dictionary<int, int>();
      pDistance_Cln = new Dictionary<int, int>();
      pIndex = 1;

      //mierzymy odległość do każdego możliwego sąsiada
      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF == PolygonFunctionalityEnum.FrameVirtual) continue;

        if (pPolygon.Assembly != null)    //jeśli ma Assembly
          foreach (cAssemblyItem pAssemblyItem in pPolygon.Assembly.AssemblyItems.Values) {
            if (pAssemblyItem.Index == xIndex || pAssemblyItem.Index == xIndex + 2) {     //sprawdzamy tylko odpowiednie indeksy boków
              pDistance = Measure_Distance(xIndex, xPolygon, pAssemblyItem.Polygon);      
              if (pDistance <= 0) continue;
              pC_Cln.Add(pIndex, pAssemblyItem.C);          //dodajemy równolegle C i Distance do dwóch list
              pDistance_Cln.Add(pIndex, pDistance);
              pIndex++;
            }
          }
        else {                            //jeśli nie ma Assembly
          pDistance = Measure_Distance(xIndex, xPolygon, pPolygon.AssemblyItem.Polygon);
          if (pDistance <= 0) continue;
          pC_Cln.Add(pIndex, pPolygon.AssemblyItem.C);      //dodajemy równolegle C i Distance do dwóch list
          pDistance_Cln.Add(pIndex, pDistance);
          pIndex++;
        }
      }
      
      pDistance = pDistance_Cln.Values.Min();                                      //wybieramy najmniejszą odległość
      pDistanceKey = pDistance_Cln.FirstOrDefault(x => x.Value == pDistance).Key;  //numer Key na lista odległości == lista C
      pC = pC_Cln[pDistanceKey];                                                   //wartość C elementu najbliższego

      return pC;

    }

    private int Measure_Distance(int xIndex, cPolygon xPolygon_A, cPolygon xPolygon_B) {
      //funkcja zwracająca odległość między środkami wielokątów
      //xIndex - indeks boku który sprawdzamy
      //xPolygon_A - wielokąt dla którego szukamy sąsiadów
      //xPolygon_B - kolejny wielokąt z foreach

      int pDistance;

      if (xIndex == 1) 
        pDistance = (int)(xPolygon_A.GetCenterPoint().Y - xPolygon_B.GetCenterPoint().Y);
      else if ( xIndex == 3) 
        pDistance = (int)(xPolygon_B.GetCenterPoint().Y - xPolygon_A.GetCenterPoint().Y);
      else if (xIndex == 2)
        pDistance = (int)(xPolygon_B.GetCenterPoint().X - xPolygon_A.GetCenterPoint().X);
      else if (xIndex == 4)
        pDistance = (int)(xPolygon_A.GetCenterPoint().X - xPolygon_B.GetCenterPoint().X);
      else {
          pDistance = 0;
          Console.WriteLine("Measure Distance Error");
      }
      
      return pDistance;

    }

  }

}
