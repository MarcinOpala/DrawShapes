using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cProjectRegions {

    private Dictionary<int, cProjectRegion> mRegions;            //lista regionów

    internal Dictionary<int, cProjectRegion> Regions { get { return mRegions; } set { mRegions = value; } }

    internal void CreateProjectRegions(cPolygonsEnv xPolygonsEnv) {
      //funkcja tworząca regiony projektu
      //xPolygonsEnv - lista wielokątów w projekcie

      cProjectRegion pProjectRegion;

      mRegions = new Dictionary<int, cProjectRegion>();

      //dodajemy każdy wielokąt oraz element konstrukcji
      foreach (cPolygon pPolygon in xPolygonsEnv.Polygons.Values) {

        if (pPolygon.CntPF == PolygonFunctionalityEnum.FrameOutline && pPolygon.Assembly == null) {
          pProjectRegion = new cProjectRegion(pPolygon);
          AddRegion(pProjectRegion);

        } if (pPolygon.Assembly != null) {
          pProjectRegion = new cProjectRegion(pPolygon);
          AddRegion(pProjectRegion);

          foreach (cAssemblyItem pAssemblyItem in pPolygon.Assembly.AssemblyItems.Values) {
            pProjectRegion = new cProjectRegion(pAssemblyItem);
            AddRegion(pProjectRegion);

          }

        } if (pPolygon.AssemblyItem != null) {
            pProjectRegion = new cProjectRegion(pPolygon);
            AddRegion(pProjectRegion);

            pProjectRegion = new cProjectRegion(pPolygon.AssemblyItem);
            AddRegion(pProjectRegion);

        } if (pPolygon.Child != null) {
    
            pProjectRegion = new cProjectRegion(pPolygon.Child);
            AddRegion(pProjectRegion);

          foreach (cAssemblyItem pAssemblyItem in pPolygon.Child.Assembly.AssemblyItems.Values) {
            pProjectRegion = new cProjectRegion(pAssemblyItem);
            AddRegion(pProjectRegion);

          }
        } 
      }

    }

    internal void AddRegion(cProjectRegion xProjectRegion) {
      //funkcja dodająca region do listy
      //xProjectRegion - region do dodania

      bool pExist;
      pExist = false;

      foreach (cProjectRegion pProjectRegion in mRegions.Values) {
        if (pProjectRegion != xProjectRegion) continue;
        
        pExist = true;
        break;

      }
      if (pExist == false)
        mRegions.Add(GetEmptyIndex(), xProjectRegion);

    }

    internal int GetEmptyIndex() {
      //funkcja zwracająca pierwszy wolny Index z listy mRegions

      int pIndex;

      pIndex = mRegions.Count + 1;

      return pIndex;

    }
  }
}
