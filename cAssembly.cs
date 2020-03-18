using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cAssembly {

    private Dictionary<int, cAssemblyItem> mAssemblyItems;  //lista AssemblyItemów
    private cPolygon mPolygon_Parent;                       //poligon bazowy

    internal Dictionary<int, cAssemblyItem> AssemblyItems { get { return mAssemblyItems; } }

    public cAssembly( ) {

      mAssemblyItems = new Dictionary<int, cAssemblyItem>();

    }

    internal void CreateMe(int xWidth, cPolygon xPolygon_Parent) {
      //funkcja tworząca konstrukcję dla wybranego poligonu
      //xWidth - szerokość profilu
      //xPolygon_Parent - poligon bazowy

      cAssemblyItem pAssemblyItem;                          

      mPolygon_Parent = xPolygon_Parent;
 
      //tworzenie AssemblyItemu dla każdego segmentu
      foreach (cSegment pSegment in xPolygon_Parent.Segments.Values) {

        pAssemblyItem = new cAssemblyItem();
        pAssemblyItem.CreateAssemblyItem_Profile(pSegment, xWidth);

        AddAssemblyItem(pSegment.Number, pAssemblyItem);

      }
      
    }
    
    internal void AddAssemblyItem(int xNumber, cAssemblyItem xAssemblyItem) {
      //funkcja dodająca AssemblyItem do listy
      //xNumber - numer na liście
      //xAssemblyItem - AssemblyItem

      mAssemblyItems.Add(xNumber, xAssemblyItem);

    }

    internal cAssemblyItem GetAssemblyItemByNumer(int xNumber) {
      //funkcja zwracająca AssemblyItem po numerze
      //xNumber - numer AssemblyItemu

      int pAssemblyItemNumber;
      int pCountMax;

      pCountMax = mAssemblyItems.Count;

      pAssemblyItemNumber = xNumber;

      if (xNumber > pCountMax)
        pAssemblyItemNumber = 1;
      
      return mAssemblyItems[pAssemblyItemNumber];

    }

  }

}
