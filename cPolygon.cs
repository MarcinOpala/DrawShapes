using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShape {

  public class cPolygon {

    private List<cSegment> mSegments;

    internal List<cSegment> Segments { get { return mSegments; } set { mSegments = value; } }


    public cPolygon() {

      mSegments = new List<cSegment>();

    }

    internal void AddSegment(cSegment xSegment, int xNumber) {
      //funkcja dodająca nowy segment do listy
      //xSegment - wybrany segment
      //xNumber - numer segmentu

      mSegments.Insert(xNumber, xSegment);

    }

    internal void SetSegmentToCurve(int xNumber) {
      //funkcja zamieniająca bok w łuk
      //xSegmentNumber - numer obsługiwanego boku

      mSegments[xNumber - 1].IsCurve = true;

    }

    public void ShowSegmentsList() {
      //funkcja wyświetlająca listę segmentów

      foreach (var i in mSegments) {
        Console.WriteLine("cSegment: " + (mSegments.IndexOf(i) + 1) + "\n" +
          "Punkt: (" + i.Point.X + " ; " + i.Point.Y + ")\n" +
          "Numer: " + i.Number + "\n" +
          "Type: " + i.IsCurve + "\n");

      }
      Console.WriteLine(" ^____________^ " + "\n");

    }

    internal cSegment GetSegmentByNumer(int xNumber) {
      //funkcja zwracająca segment
      //xSegmentNumber - numer segmentu

      int pSegmentNumber;
      int pCountMax;

      pCountMax = mSegments.Count - 1;

      pSegmentNumber = xNumber;

      if (xNumber > pCountMax)
        pSegmentNumber = 0;


      return mSegments[pSegmentNumber];

    }
  }
}
