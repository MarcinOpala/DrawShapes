using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cPolygonsEnv {

    private Dictionary<int, cPolygon> mPolygons;

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

  }

}
