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

    internal void CreateMe(int xWidth, int xHeight, string xNe) {
      //funkcja tworząca projekt
      //xWidth - szerokość prostokąta
      //xHeight - wysokość prostokąta
      //xNe - nazwa projektu

      int pIndex;
      cPolygon pPolygon;

      mNe = xNe;

      pIndex = 1;
      
      pPolygon = cPolygonFactory.GetPolygon_Rect(xWidth, xHeight, pIndex);

      //24.03.2020 MO tymczasowo wyłączone - rysowanie wielokąta foremnego
      //pPolygon = cPolygonFactory.GetPolygon_Regular(xDiameter, xSegementsQuantity, pIndex);

      mPolygonsEnv.AddPolygon(pPolygon);
      
    }

    



  }

}
