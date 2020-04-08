using System;
using System.Drawing;

namespace DrawShape {

  public class cPoint {
    //

    private float mX;
    private float mY;

    internal float X { get { return mX; } set { mX = value; } }
    internal float Y { get { return mY; } set { mY = value; } }

    public cPoint() {

    }

    public cPoint(float xPtX, float xPtY) {

      PointF pPt;

      pPt = new PointF(xPtX, xPtY);

      mX = pPt.X;
      mY = pPt.Y;

    }

    public string GetString() {

      return $"X:{mX}, Y:{mY}";

    }

  }
  
}
