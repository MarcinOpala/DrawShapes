using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape
{
  public class cSegment {

    private cPoint mPointOfSegment;                          //punkt początkowy danego cSegmentu
    private int mNumberOfSegment;                        //numer danego cSegmentu

    internal cPoint PointOfSegment { get { return mPointOfSegment; } set { mPointOfSegment = value; } }
    internal int NumberOfSegment { get { return mNumberOfSegment; } set { mNumberOfSegment = value; } }

    public cSegment(cPoint xPointOfSegment, int xNumberOfSegment) {
      //xPointOfSegment - pierwszy punkt danego cSegmentu
      //xNumberOfSegment - numer danego cSegmentu

      this.mPointOfSegment = xPointOfSegment;
      this.mNumberOfSegment = xNumberOfSegment;
    }

  }
}
