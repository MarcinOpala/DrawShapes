using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {
  public class cStraightLine {
    //prosta w postaci ogólnej: Ax + By + C

    private double mA;                                         //parametr A prostej
    private double mB;                                         //parametr B prostej
    private double mC;                                         //parametr C prostej

    internal double A { get { return mA; } set { mA = value; } }
    internal double B { get { return mB; } set { mB = value; } }
    internal double C { get { return mC; } set { mC = value; } }

    public cStraightLine() {

    }

    public cStraightLine(cSegment xSegment) {
      //rownanie prostej pokrywające się z bokiem

      float pX_1, pY_1;
      float pX_2, pY_2;

      pX_1 = xSegment.Point.X;
      pY_1 = xSegment.Point.Y;

      pX_2 = xSegment.Segment_Next.Point.X;
      pY_2 = xSegment.Segment_Next.Point.Y;

      mA = pY_2 - pY_1;
      mB = -(pX_2 - pX_1);
      mC = mA * (-pX_1) + mB * (-pY_1);

    }

    public cStraightLine(cPoint xPoint_1, cPoint xPoint_2) {
      //rownanie prostej pokrywające się z bokiem

      float pX_1, pY_1;
      float pX_2, pY_2;

      pX_1 = xPoint_1.X;
      pY_1 = xPoint_1.Y;

      pX_2 = xPoint_2.X;
      pY_2 = xPoint_2.Y;

      mA = pY_2 - pY_1;
      mB = -(pX_2 - pX_1);
      mC = mA * (-pX_1) + mB * (-pY_1);

    }

    public cStraightLine(Dictionary<int, cPoint> xPoints) {
      //rownanie prostej pokrywające się z bokiem

      float pX_1, pY_1;
      float pX_2, pY_2;

      pX_1 = xPoints[1].X;
      pY_1 = xPoints[1].Y;

      pX_2 = xPoints[2].X;
      pY_2 = xPoints[2].Y;

      mA = pY_2 - pY_1;
      mB = -(pX_2 - pX_1);
      mC = mA * (-pX_1) + mB * (-pY_1);

    }

    public cStraightLine(double xA, double xB, double xC) {
      mA = xA;
      mB = xB;
      mC = xC;

    }

    internal double Get_DistanceToLine(cPoint xPoint) {
      //

      double mDistanceToLine;

      mDistanceToLine = ((mA * xPoint.X) + (mB * xPoint.Y) + mC); // Ax+Bx+C

      return mDistanceToLine;

    }

    internal cPoint Get_PointFromCrossLines(cStraightLine xLine_2) {
      //

      cPoint pPoint;
      double pX, pY;
      double pW, pWx, pWy;

      if (Math.Abs(mA) == Math.Abs(xLine_2.A) && Math.Abs(mB) == Math.Abs(xLine_2.B)) return null; //proste są równoległe

      pW = (mA * xLine_2.B) - (xLine_2.A * mB);            //obliczenie wyznaczników
      pWx = ((-mC) * xLine_2.B) - ((-xLine_2.C) * mB);
      pWy = (mA * (-xLine_2.C)) - (xLine_2.A * (-mC));

      if (pW == 0) {        //prosta jest prostopadła
        pX = pWx;
        pY = pWy;
      } else {
        pX = pWx / pW;
        pY = pWy / pW;
      }

      pPoint = new cPoint((float)pX, (float)pY);

      return pPoint;

    }

    internal cStraightLine Get_StraightLine_Parallel(cPoint xPoint) {
      //prosta równoległa przechodząca przez punkt

      cStraightLine pStraightLine_Parallel;
      double pC;

      pC = -(mA * xPoint.X + mB * xPoint.Y);

      pStraightLine_Parallel = new cStraightLine(mA, mB, pC);

      return pStraightLine_Parallel;
    }

    internal cStraightLine Get_Parallel(double xDistance) {
      //prosta równoległa przesunięta o Y 

      cStraightLine pStraightLine_Parallel, pStraightLine_X;

      double pC;
      double pAngle;

      pStraightLine_X = new cStraightLine(0,1,0);   //prosta pokrywająca się z osią Ox
      
      pAngle = Get_Angle(pStraightLine_X);

      pC = mC - (xDistance / Math.Cos(pAngle));

      pStraightLine_Parallel = new cStraightLine(mA, mB, pC);

      return pStraightLine_Parallel;
    }


    internal cStraightLine Get_StraightLine_Normal(cStraightLine xStraightLine, cPoint xPoint ) {
      //prosta prostopadła w punkcie

      cStraightLine pStraightLine_Normal;
      double pA, pB, pC;
      
      pA = -(xStraightLine.B);
      pB = xStraightLine.A;
      pC = -(pA * xPoint.X + pB * xPoint.Y); 
      pStraightLine_Normal = new cStraightLine(pA, pB, pC);

      return pStraightLine_Normal;
    }

    internal double Get_Angle(cStraightLine xLine_2) {

      double pAlfa;
      double pCos;

      pCos = ((mA * xLine_2.A) + (mB * xLine_2.B)) /
            (Math.Sqrt((mA * mA) + (mB * mB)) * Math.Sqrt((xLine_2.A * xLine_2.A) + (xLine_2.B * xLine_2.B)));

      pAlfa = (Math.Acos(pCos)) * 180 / Math.PI;

      return pAlfa;
    }
  }
}
