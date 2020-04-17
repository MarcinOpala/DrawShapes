using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cObjectHitController {

    private object mObjectHitController;

    internal object ObjectHitController { get { return mObjectHitController; } set { mObjectHitController = value; } }


    public cObjectHitController() {

      mObjectHitController = new object();

    }
    internal string HitTest(cPoint xPoint, cProjectRegion xProjectRegion) {

      string pStr;

      pStr = "";
      if (xProjectRegion.Region.IsVisible(xPoint.X, xPoint.Y)) { 

      pStr += $"Region: {xProjectRegion.CntProjectRegion} {xProjectRegion.Polygon.CntPF}" + Environment.NewLine +
              $"Index: {xProjectRegion.Polygon.Index}" + Environment.NewLine +
              $"________________________" + Environment.NewLine;
      }

      return pStr;

  }

}
}
