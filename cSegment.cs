using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape{
  public class cSegment {

    private cPoint mPoint;                                  //punkt początkowy danego cSegmentu
    private int mNumber;                                    //numer danego cSegmentu
//  private Enum mType = Enum.GetValues(typeof(EnumSegmentType.mType.pType1));                        //rodzaj segmentu
    private bool mIsCurve;                                 //czy jest łuk

    internal cPoint Point { get { return mPoint; } set { mPoint = value; } }
    internal int Number { get { return mNumber; } set { mNumber = value; } }
    internal bool IsCurve { get { return mIsCurve; } set { mIsCurve = value; } }

    //internal int Type { get { return mType; } set { mType = value; } }

        

    public cSegment(cPoint xPointOfSegment, int xNumberOfSegment) {
      //xPointOfSegment - pierwszy punkt danego cSegmentu
      //xNumberOfSegment - numer danego cSegmentu

      this.mPoint = xPointOfSegment;
      this.mNumber = xNumberOfSegment;

    }

  }
}
