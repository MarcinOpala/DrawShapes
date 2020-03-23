using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cProject {

    private cPolygonsEnv mPolygonsEnv;                      //lista poligonów w projekcie
    private string mNe;                                     //nazwa projektu

    internal cPolygonsEnv PolygonsEnv { get { return mPolygonsEnv; } set { mPolygonsEnv = value; } }
    internal string Ne { get { return mNe; } set { mNe = value; } }

    public cProject() {

      mPolygonsEnv = new cPolygonsEnv();

    }

    internal void CreateMe(int xDiameter, int xSegementsQuantity, string xNe) {
      //funkcja tworząca projekt
      //xWidth_Profile - szerokość profilu
      //xDiametr - średnica okręgu, w który wpisany jest wielobok
      //xSegementsQuantity - liczba boków wielokąta
      //xNe - nazwa projektu

      int pIndex;
      cPolygon pPolygon;

      mNe = xNe;

      pIndex = 1;
      
      pPolygon = cPolygonFactory.GetPolygon_Regular(xDiameter, xSegementsQuantity, pIndex);
     // pPolygon.CreateAssembly(xWidth_Profile, pPolygon);

      mPolygonsEnv.AddPolygon(pPolygon);
      
    }



  }

}
