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

    public int GetEmptyIndex() {
      //funkcja zwracająca pierwszy wolny Index z listy mPolygons

      int pIndex;

      pIndex = mPolygons.Count + 1;

      return pIndex;

    }

    internal void CreatePolygon_Virtual(cPolygon xPolygon) {
      //funkcja tworząca wirtualny wielokąt
      //xPolygon - wielokąt, z którego ma zostać utworzony wirtualny

      cPolygon pPolygon_Virtual;

      pPolygon_Virtual = xPolygon.Clone();

      pPolygon_Virtual.Index = GetEmptyIndex();

      pPolygon_Virtual.CntPF = PolygonFunctionalityEnum.FrameVirtual;

      pPolygon_Virtual.Parent = xPolygon;                             //rodzicem staje się wielokąt wzorcowy

      AddPolygon(pPolygon_Virtual);

    }

    internal void CreatePolygon_Mullion(cPolygon xPolygon, int xMullionPosition_X, int xMullionWidth, float xWidth_Profile) {
      //funkcja tworząca wirtualny wielokąt
      //xPolygon - wielokąt, z którego ma zostać utworzony Polygon_Mullion
      //xMullionPosition_X - pozycja X słupka
      //xMullionWidth - szerokość słupka
      //xWidth_Profile - szerokość profilu

      cPolygon pPolygon;

      pPolygon = xPolygon.Clone();

      pPolygon.Index = GetEmptyIndex();

      pPolygon.SetPolygonToMullion(xPolygon, xMullionPosition_X, xMullionWidth, xWidth_Profile);

      AddPolygon(pPolygon);

    }


    internal cPolygon GetPolygonVirtual() {
      //funkcja zwracająca pierwszy wirtualny Polygon

      foreach (cPolygon pPolygon in mPolygons.Values) {
        if (pPolygon.CntPF != PolygonFunctionalityEnum.FrameVirtual) continue;
        return pPolygon;
      }

      return null;

    }

    internal void Split_Polygon(int xMullionPosition_X, cPolygon xPolygon) {
      //fukncja  dzieląca Virtual Polygon na 2 w miejscu słupka 
      //xMullionPosition_X - pozycja X słupka
      //xPolygon - bazowy wielokąt

      Dictionary <int, cPolygon> pPolygons;

      pPolygons = xPolygon.Split_PolygonByWidth(xMullionPosition_X);
      
      mPolygons.Remove(xPolygon.Index);

      mPolygons.Add(pPolygons[1].Index, pPolygons[1]);

      mPolygons.Add(pPolygons[2].Index, pPolygons[2]);

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

    internal void CreatePolygon_Sash(cPolygon xPolygon, float xWidth_Profile, double xC_Frame, double xC_Mullion) {
      //funkcja tworząca wielokąt skrzydła

      cPolygon pPolygon_A;
      cVector pCFrame_CFrame;
      cVector pCMullion_CFrame;
      
      pPolygon_A = xPolygon.Clone();

      xPolygon.Child = pPolygon_A;

      pPolygon_A.Parent = xPolygon;
      pPolygon_A.CntPF = PolygonFunctionalityEnum.FrameOutline;
      pPolygon_A.Index = GetEmptyIndex();

      //ustawiamy SegmentIsIncludedEnum
      if (GetPolygonMullion() == null)
        foreach (cSegment pSegment in pPolygon_A.Segments.Values) {
          pSegment.CntSInclude = SegmentIsIncludedEnum.FrameOutline;
        }
      else {
        foreach (cPolygon pPolygon_B in mPolygons.Values) {
          if (pPolygon_B.CntPF != PolygonFunctionalityEnum.Mullion) continue;

          foreach (cSegment pSegment in pPolygon_A.Segments.Values) {
            if (pSegment.CntSInclude == SegmentIsIncludedEnum.Mullion) continue;

            if (pPolygon_B.Segments[1].Point.X <= pSegment.Point.X && pPolygon_B.Segments[1].Point.Y <= pSegment.Point.Y
             && pPolygon_B.Segments[3].Point.X >= pSegment.Point.X && pPolygon_B.Segments[3].Point.Y >= pSegment.Point.Y)

              pSegment.CntSInclude = SegmentIsIncludedEnum.Mullion;

            else pSegment.CntSInclude = SegmentIsIncludedEnum.FrameOutline;

          }
        }
      }

      //setujemy boki
      pCFrame_CFrame = new cVector(xC_Frame, xC_Frame);
      pCMullion_CFrame = new cVector(xC_Mullion, xC_Frame);

      foreach (cSegment pSegment in pPolygon_A.Segments.Values) {
        if (pSegment.CntSInclude == SegmentIsIncludedEnum.FrameOutline)
          pSegment.SetPointByVector(pCFrame_CFrame);

        else if (pSegment.CntSInclude == SegmentIsIncludedEnum.Mullion)
          pSegment.SetPointByVector(pCMullion_CFrame);

        else
          Console.WriteLine("Error with SegmentIsIncludedEnum in CreatePolygon_Sash");
      }

      //TODO Assembly
      

      AddPolygon(pPolygon_A);

    }

  }

}
