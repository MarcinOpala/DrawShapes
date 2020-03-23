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

  }

}
