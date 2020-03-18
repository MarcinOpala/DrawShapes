using System;
using System.Drawing;

namespace DrawShape {

  public class cPoint {
    //

    private float mX;
    private float mY;

    internal float X { get { return mX; } set { mX = value; } }
    internal float Y { get { return mY; } set { mY = value; } }

    public cPoint(float xPtX, float xPtY) {

      PointF pPt;

      pPt = new PointF(xPtX, xPtY);

      this.mX = pPt.X;
      this.mY = pPt.Y;

    }

  }
  
}
