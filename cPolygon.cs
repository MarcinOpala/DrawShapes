using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShape {

  public class cPolygon {

    private List<cSegment> mSegments = new List<cSegment>();                       //lista segmentów

    internal List<cSegment> Segments { get { return mSegments; } set { mSegments = value; } }
     
    internal void AddSegment(cSegment xSegment, int xNumber) {
      //funkcja dodająca nowy segment do listy
      //xSegment - wybrany segment
      //xNumber - numer segmentu

      mSegments.Insert(xNumber, xSegment);

    }

    internal void SetSegmentToCurve(int xSegmentNumber) {
      //funkcja zamieniająca bok w łuk
      //xSegmentNumber - numer obsługiwanego boku

      mSegments[xSegmentNumber - 1].IsCurve = true;

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

    internal cSegment GetSegmentByNumer(int xSegmentNumber) {
      //funkcja zwracająca segment po numerze
      //xSegmentNumber - numer segmentu

      return mSegments[xSegmentNumber];

    }
  }
}
