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

    internal void CreateMe(int xWidth, cPolygon xPolygon_Parent, int xC) {
      //funkcja tworząca konstrukcję dla wybranego poligonu
      //xWidth - szerokość profilu
      //xPolygon_Parent - poligon bazowy
           //xC_Cln - kolekcja stałej C 

      cAssemblyItem pAssemblyItem;

      mPolygon_Parent = xPolygon_Parent;

      //tworzenie AssemblyItemu dla każdego segmentu
      foreach (cSegment pSegment in xPolygon_Parent.Segments.Values) {

        pAssemblyItem = new cAssemblyItem(pSegment.Index);
        pAssemblyItem.CreateAssemblyItem_Profile(pSegment, xWidth, xC);

        AddAssemblyItem(pSegment.Index, pAssemblyItem);

      }

    }

    internal void CreateMe(int xWidth, cPolygon xPolygon_Parent, Dictionary<int, int> xC_Cln) {
      //funkcja tworząca konstrukcję dla wybranego poligonu
      //xWidth - szerokość profilu
      //xPolygon_Parent - poligon bazowy
      //xC_Cln - kolekcja stałej C 

      cAssemblyItem pAssemblyItem;                          

      mPolygon_Parent = xPolygon_Parent;
 
      //tworzenie AssemblyItemu dla każdego segmentu
      foreach (cSegment pSegment in xPolygon_Parent.Segments.Values) {

        pAssemblyItem = new cAssemblyItem(pSegment.Index);
        pAssemblyItem.CreateAssemblyItem_Profile(pSegment, xWidth, xC_Cln[pSegment.Index]);

        AddAssemblyItem(pSegment.Index, pAssemblyItem);

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

    internal void CreateCross(int xMullion_X, int xMullion_Y, int xSize) {

      cAssemblyItem pAssemblyItem;
      cPolygon pPolygon;
      cSegment pSegment;

      pPolygon = new cPolygon();
      pSegment = new cSegment();
      pSegment.Point.X = xMullion_X - xSize;
      pSegment.Point.Y = xMullion_Y;
      pSegment.Index = 1;
      pPolygon.AddSegment(pSegment);

      pSegment = new cSegment();
      pSegment.Point.X = xMullion_X + xSize;
      pSegment.Point.Y = xMullion_Y;
      pSegment.Index = 2;

      pPolygon.AddSegment(pSegment);
      pAssemblyItem = new cAssemblyItem();
      pAssemblyItem.Polygon = pPolygon;

      AddAssemblyItem(1, pAssemblyItem);

      ///////
      pPolygon = new cPolygon();
      pSegment = new cSegment();
      pSegment.Point.X = xMullion_X;
      pSegment.Point.Y = xMullion_Y - xSize;
      pSegment.Index = 1;
      pPolygon.AddSegment(pSegment);

      pSegment = new cSegment();
      pSegment.Point.X = xMullion_X;
      pSegment.Point.Y = xMullion_Y + xSize;
      pSegment.Index = 2;
      pPolygon.AddSegment(pSegment);
      pAssemblyItem = new cAssemblyItem();
      pAssemblyItem.Polygon = pPolygon;

      AddAssemblyItem(2, pAssemblyItem);
    }

  }

}
