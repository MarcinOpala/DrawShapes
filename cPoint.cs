using System;
using System.Drawing;

namespace DrawShape {
  public class cPoint {
    private float mX;
    private float mY;

    internal float X { get { return mX; } set { mX = value; } }
    internal float Y { get { return mY; } set { mY = value; } }

    public cPoint(PointF xPt) {
      this.mX = xPt.X;
      this.mY = xPt.Y;
    }

    public void PrintPoint(int x1, int y1) {
      this.mX = x1;
      this.mY = y1;

      Console.WriteLine("X: " + mX);
      Console.WriteLine("Y: " + mY);

    }

    public String ToString() {
      return "X: " + mX + " Y: " + mY;
    }
  }




}
