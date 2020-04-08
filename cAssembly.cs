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
      //utworzenie pustej konstrukji wielokąta

      mAssemblyItems = new Dictionary<int, cAssemblyItem>();

    }

    internal void CreateMe(int xWidth, cPolygon xPolygon_Parent, int xC) {
      //funkcja tworząca konstrukcję dla wybranego wielokąta
      //xWidth - szerokość profilu
      //xPolygon_Parent - poligon bazowy
      //xC - stała C 

      cAssemblyItem pAssemblyItem;

      mPolygon_Parent = xPolygon_Parent;

      //utworzenie AssemblyItemu dla każdego boku
      foreach (cSegment pSegment in xPolygon_Parent.Segments.Values) {

        pAssemblyItem = new cAssemblyItem(pSegment.Index);
        pAssemblyItem.CreateAssemblyItem_Profile(pSegment, xWidth, xC);

        AddAssemblyItem(pSegment.Index, pAssemblyItem);

      }

    }

    internal void CreateMe(int xWidth, cPolygon xPolygon_Parent, Dictionary<int, int> xC_Cln) {
      //funkcja tworząca konstrukcję dla wybranego wielokąta
      //xWidth - szerokość profilu
      //xPolygon_Parent - poligon bazowy
      //xC_Cln - kolekcja stałej C 

      cAssemblyItem pAssemblyItem;
      int pIdx;

      pIdx = 1;
      mPolygon_Parent = xPolygon_Parent;
 
      //utworzenie AssemblyItemu dla każdego boku
      foreach (cSegment pSegment in xPolygon_Parent.Segments.Values) {

        pAssemblyItem = new cAssemblyItem(pSegment.Index);
        pAssemblyItem.CreateAssemblyItem_Profile(pSegment, xWidth, xC_Cln[pIdx]);

        AddAssemblyItem(pSegment.Index, pAssemblyItem);

        pIdx++;
      }
      
    }
    
    internal void AddAssemblyItem(int xIndex, cAssemblyItem xAssemblyItem) {
      //funkcja dodająca AssemblyItem do listy
      //xIndex - numer na liście
      //xAssemblyItem - element do dodania

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

    internal void CreateCross(cPoint xPoint, int xSize) {
      //funkcja tworząca kształt specjalny - krzyżyk
      //xPoint - miejsce położenia krzyżyka
      //xSize - rozmar krzyżyka

      cAssemblyItem pAssemblyItem;
      cPolygon pPolygon;
      cSegment pSegment;

      //część pozioma
      pPolygon = new cPolygon();
      pSegment = new cSegment();
      pSegment.Point.X = xPoint.X - xSize;
      pSegment.Point.Y = xPoint.Y;
      pSegment.Index = 1;
      pPolygon.AddSegment(pSegment);

      pSegment = new cSegment();
      pSegment.Point.X = xPoint.X + xSize;
      pSegment.Point.Y = xPoint.Y;
      pSegment.Index = 2;

      pPolygon.AddSegment(pSegment);
      pAssemblyItem = new cAssemblyItem();
      pAssemblyItem.Polygon = pPolygon;

      AddAssemblyItem(1, pAssemblyItem);

      //część pionowa
      pPolygon = new cPolygon();
      pSegment = new cSegment();
      pSegment.Point.X = xPoint.X;
      pSegment.Point.Y = xPoint.Y - xSize;
      pSegment.Index = 1;
      pPolygon.AddSegment(pSegment);

      pSegment = new cSegment();
      pSegment.Point.X = xPoint.X;
      pSegment.Point.Y = xPoint.Y + xSize;
      pSegment.Index = 2;
      pPolygon.AddSegment(pSegment);
      pAssemblyItem = new cAssemblyItem();
      pAssemblyItem.Polygon = pPolygon;

      AddAssemblyItem(2, pAssemblyItem);

    }
  }


}
