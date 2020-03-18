using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShape {

  public class cPolygon {

    private Dictionary<int, cSegment> mSegments;
    private cAssembly mAssembly;

    internal Dictionary<int, cSegment> Segments { get { return mSegments; } set { mSegments = value; } }
    internal cAssembly Assembly { get { return mAssembly; } }

    public cPolygon() {

      mSegments = new Dictionary<int, cSegment>();
      mAssembly = new cAssembly();

    }

    internal void AddSegment(cSegment xSegment) {
      //funkcja dodająca nowy segment do listy
      //xSegment - wybrany segment

      mSegments.Add(xSegment.Number, xSegment);

    }
       
    internal void CreateAssembly(int xWidth, cPolygon xPolygon) {
      //funkcja tworząca Assembly dla poligonu
      //xWidth - szerokość profilu
      //xPolygon - poligon do przeprowadzenia assembly

      mAssembly = new cAssembly();

      mAssembly.CreateMe(xWidth, xPolygon);
            
    }

    internal void SetSegmentToCurve(int xNumber) {
      //funkcja zamieniająca bok w łuk
      //xSegmentNumber - numer obsługiwanego boku

      mSegments[xNumber].IsCurve = true;

    }

    public void ShowSegmentsList() {
      //funkcja wyświetlająca listę segmentów

      foreach (var i in mSegments) {
        Console.WriteLine($"cSegment: {i} Punkt: ( {i.Value.Point.X} ; {i.Value.Point.Y} Numer:  {i.Value.Number} Type: {i.Value.IsCurve} \n");

      }

    }

    internal cSegment GetSegmentByNumber(int xNumber) {
      //funkcja zwracająca segment
      //xSegmentNumber - numer segmentu

      int pSegmentNumber;
      int pCountMax;

      pCountMax = mSegments.Count;

      pSegmentNumber = xNumber;

      if (xNumber > pCountMax)
        pSegmentNumber = 1;

      return mSegments[pSegmentNumber];

    }
    
  }

}
