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

        int pIndex;
        pIndex = pSegment.Index + xPolygon_Parent.Index;

        pAssemblyItem = new cAssemblyItem(pIndex);
        pAssemblyItem.CreateAssemblyItem_Profile(pSegment, xWidth);

        AddAssemblyItem(pIndex, pAssemblyItem);

      }
      
    }
    
    internal void AddAssemblyItem(int xIndex, cAssemblyItem xAssemblyItem) {
      //funkcja dodająca AssemblyItem do listy
      //xIndex - numer na liście
      //xAssemblyItem - AssemblyItem

      mAssemblyItems.Add(xIndex, xAssemblyItem);

    }

    internal cAssemblyItem GetAssemblyItemByIndex(int xIndex) {
      //funkcja zwracająca AssemblyItem po numerze
      //xIndex - numer AssemblyItemu

      int pAssemblyItemIndex;
      int pCountMax;

      pCountMax = mAssemblyItems.Count;

      pAssemblyItemIndex = xIndex;

      if (xIndex > pCountMax)
        pAssemblyItemIndex = 1;
      
      return mAssemblyItems[pAssemblyItemIndex];

    }

  }

}
