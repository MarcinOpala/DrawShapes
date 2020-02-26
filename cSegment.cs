using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape
{
  public class cSegment {

    public cPoint pPointOfSegment { get; set; }                          //punkt początkowy danego cSegmentu
    public int pNumberOfSegment { get; set; }                            //numer danego cSegmentu

    public cSegment(cPoint xPointOfSegment, int xNumberOfSegment) {
      //xPointOfSegment - pierwszy punkt danego cSegmentu
      //xNumberOfSegment - numer danego cSegmentu

      this.pPointOfSegment = xPointOfSegment;
      this.pNumberOfSegment = xNumberOfSegment;
    }

  }
}
