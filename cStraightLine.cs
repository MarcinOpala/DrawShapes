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
      //równanie prostej pokrywające się z bokiem

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
      //równanie prostej przechodzącej przez dwa punkty

      float pX_1, pY_1;
      float pX_2, pY_2;

      pX_1 = xPoint_1.X;
      pY_1 = xPoint_1.Y;

      pX_2 = xPoint_2.X;
      pY_2 = xPoint_2.Y;

      mA = pY_2 - pY_1;
      mB = -(pX_2 - pX_1);
      mC = mA * (-pX_1) + mB * (-pY_1);

      Simplify(this);

    }

    public cStraightLine(Dictionary<int, cPoint> xPoints) {
      //rownanie prostej utworzonej z kolekcji dwóch punktów

      float pX_1, pY_1;
      float pX_2, pY_2;

      pX_1 = xPoints[1].X;
      pY_1 = xPoints[1].Y;

      pX_2 = xPoints[2].X;
      pY_2 = xPoints[2].Y;

      mA = pY_2 - pY_1;
      mB = -(pX_2 - pX_1);
      mC = mA * (-pX_1) + mB * (-pY_1);

      //Simplify(this);

    }

    public cStraightLine(double xA, double xB, double xC) {
      //podstawowe równanie prostej

      mA = xA;
      mB = xB;
      mC = xC;

      Simplify(this);

    }

    internal double Get_DistanceToLine(cPoint xPoint) {
      //funkcja zwracajaca odległość punktu od prostej typu Ax + By + C

      double mDistanceToLine;

      mDistanceToLine = ((mA * xPoint.X) + (mB * xPoint.Y) + mC); 

      return mDistanceToLine;

    }

    internal cPoint Get_PointFromCrossLines(cStraightLine xLine_2) {
      //funkcja zwracajaca punkt przecięcia się dwóch prostych
      //xLine_2 - druga prosta

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
      //funkcja zwracająca prostą równoległą przechodząca przez punkt

      cStraightLine pStraightLine_Parallel;
      double pC;

      pC = -(mA * xPoint.X + mB * xPoint.Y);

      pStraightLine_Parallel = new cStraightLine(mA, mB, pC);

      Simplify(pStraightLine_Parallel);

      return pStraightLine_Parallel;
    }

    internal cStraightLine Get_Parallel(double xDistance) {
      //funkcja zwracająca prostą równoległą przesuniętą wzdłóż osi Y 

      cStraightLine pStraightLine_Parallel;

      double pC;

      pC = mC - xDistance;

      pStraightLine_Parallel = new cStraightLine(mA, mB, pC);

      Simplify(pStraightLine_Parallel);

      return pStraightLine_Parallel;

    }


    internal cStraightLine Get_StraightLine_Normal(cPoint xPoint) {
      //funkcja zwracająca prostą prostopadłą do danej
      //xPoint - punkt przecięcia prostych

      cStraightLine pStraightLine_Normal;
      double pA, pB, pC;
      
      pA = -(mB);
      pB = mA;
      pC = -(pA * xPoint.X + pB * xPoint.Y); 
      pStraightLine_Normal = new cStraightLine(pA, pB, pC);

      Simplify(pStraightLine_Normal);

      return pStraightLine_Normal;
    }

    internal double Get_Angle(cStraightLine xLine_2) {
      //funkcja zwracająca kąt pomiędzy dwoma prostymi
      //xLine_2 - druga prosta

      double pAlfa;
      double pCos;

      pCos = ((mA * xLine_2.A) + (mB * xLine_2.B)) /
            (Math.Sqrt((mA * mA) + (mB * mB)) * Math.Sqrt((xLine_2.A * xLine_2.A) + (xLine_2.B * xLine_2.B)));

      pAlfa = (Math.Acos(pCos)) * 180 / Math.PI;

      return pAlfa;
    }

    internal bool IsCover(cStraightLine xStraightLine) {
      //funkca sprawdzająca czy dwie proste pokrywają się
      //xStraightLine - druga prosta

      bool pCheck;

      if (mA != 0 && xStraightLine.A != 0) {
        mB /= mA;
        mC /= mA;
        xStraightLine.B /= xStraightLine.A;
        xStraightLine.C /= xStraightLine.A;

        if (mB == xStraightLine.B && mC == xStraightLine.C) pCheck = true;
        else pCheck = false;

      } else if (mB != 0 && xStraightLine.B != 0) {
        mA /= mB;
        mC /= mB;
        xStraightLine.A /= xStraightLine.B;
        xStraightLine.C /= xStraightLine.B;

        if (mA == xStraightLine.A && mC == xStraightLine.C) pCheck = true;
        else pCheck = false;

      } else pCheck = false;

        return pCheck;

    }
    internal void Simplify(cStraightLine xStraightLine) {
      //funkcja skracająca równanie prostej do podstawowego (ciągle w postaci ogólnej)

      double pMax;

      if (Math.Abs(xStraightLine.A) >= Math.Abs(xStraightLine.B) && xStraightLine.A != 0) {
        pMax = xStraightLine.A;

        mA /= pMax;
        mB /= pMax;
        mC /= pMax;

      } else if (Math.Abs(xStraightLine.B) > Math.Abs(xStraightLine.A) && xStraightLine.B != 0) {
        pMax = xStraightLine.B;

        mA /= pMax;
        mB /= pMax;
        mC /= pMax;

      } 

    }
  }
}
