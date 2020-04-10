using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cLine {
    //prosta w postaci ogólnej: Ax + By + C

    private double mA;                                         //parametr A prostej
    private double mB;                                         //parametr B prostej
    private double mC;                                         //parametr C prostej

    internal double A { get { return mA; } set { mA = value; } }
    internal double B { get { return mB; } set { mB = value; } }
    internal double C { get { return mC; } set { mC = value; } }

    public cLine() {

    }

    public cLine(cSegment xSegment) {
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

    public cLine(cPoint xPoint_1, cPoint xPoint_2) {
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

    public cLine(Dictionary<int, cPoint> xPoints) {
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

    public cLine(double xA, double xB, double xC) {
      //podstawowe równanie prostej

      mA = xA;
      mB = xB;
      mC = xC;

     // Simplify(this);

    }

    internal double Get_DistanceToLine(cPoint xPoint) {
      //funkcja zwracajaca odległość punktu od prostej typu Ax + By + C

      double mDistanceToLine;

      mDistanceToLine = ((mA * xPoint.X) + (mB * xPoint.Y) + mC); 

      return mDistanceToLine;

    }

    internal cPoint Get_PointFromCrossLines(cLine xLine_2) {
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

    internal cLine Get_Line_Parallel(cPoint xPoint) {
      //funkcja zwracająca prostą równoległą przechodząca przez punkt

      cLine pLine_Parallel;
      double pC;

      pC = -(mA * xPoint.X + mB * xPoint.Y);

      pLine_Parallel = new cLine(mA, mB, pC);

      Simplify(pLine_Parallel);

      return pLine_Parallel;
    }

    internal cLine Get_Parallel(double xDistance) {
      //funkcja zwracająca prostą równoległą przesuniętą wzdłóż osi Y 

      cLine pLine_Parallel;

      double pC;

      pC = mC - xDistance;

      pLine_Parallel = new cLine(mA, mB, pC);

      Simplify(pLine_Parallel);

      return pLine_Parallel;

    }


    internal cLine Get_Line_Normal(cPoint xPoint) {
      //funkcja zwracająca prostą prostopadłą do danej
      //xPoint - punkt przecięcia prostych

      cLine pLine_Normal;
      double pA, pB, pC;
      
      pA = -(mB);
      pB = mA;
      pC = -(pA * xPoint.X + pB * xPoint.Y); 
      pLine_Normal = new cLine(pA, pB, pC);

      Simplify(pLine_Normal);

      return pLine_Normal;
    }

    internal double Get_Angle(cLine xLine_2) {
      //funkcja zwracająca kąt pomiędzy dwoma prostymi
      //xLine_2 - druga prosta

      double pAlfa;
      double pCos;

      pCos = ((mA * xLine_2.A) + (mB * xLine_2.B)) /
            (Math.Sqrt((mA * mA) + (mB * mB)) * Math.Sqrt((xLine_2.A * xLine_2.A) + (xLine_2.B * xLine_2.B)));

      pAlfa = (Math.Acos(pCos)) * 180 / Math.PI;

      return pAlfa;
    }

    internal bool IsCover(cLine xLine) {
      //funkca sprawdzająca czy dwie proste pokrywają się
      //xLine - druga prosta

      bool pCheck;

      
     // Simplify(xLine);

        if (mA == xLine.A && mB == xLine.B && mC == xLine.C) pCheck = true;
        else pCheck = false;


        return pCheck;

    }
    internal void Simplify(cLine xLine) {
      //funkcja skracająca równanie prostej do podstawowego (ciągle w postaci ogólnej)

      double pMax;

      if (Math.Abs(xLine.A) >= Math.Abs(xLine.B) && xLine.A != 0) {
        pMax = xLine.A;

        mA = Math.Round((mA / pMax), 3);
        mB = Math.Round((mB / pMax), 3);
        mC = Math.Round((mC / pMax), 3);

      } else if (Math.Abs(xLine.B) > Math.Abs(xLine.A) && xLine.B != 0) {
        pMax = xLine.B;

        mA = Math.Round((mA / pMax), 3);
        mB = Math.Round((mB / pMax), 3);
        mC = Math.Round((mC / pMax), 3);

      } 
    }

    internal bool IsInclude(cPoint xPoint) {
      //

      double pDistanceToLine;
      bool pCheck;

      pDistanceToLine = ((mA * xPoint.X) + (mB * xPoint.Y) + mC);

      if (pDistanceToLine >= -0.1 && pDistanceToLine <= 0.1) 
        pCheck = true;

      else pCheck = false;

      return pCheck;

    }

    internal int Get_IndexOf_PointNearest(cPoint xPoint, Dictionary<int, cPoint> xCln) {

      Dictionary<int, double> pCln_Distance;
      cVector pVector;
      double pDistance;
      int pIdx;

      pIdx = 1;
      pCln_Distance = new Dictionary<int, double>();

      foreach (cPoint pPoint in xCln.Values) {
        pVector = new cVector(xPoint, pPoint);
        pDistance = pVector.Get_VectorLength();
        pCln_Distance.Add(pIdx, pDistance);
        pIdx++;

      }

      var pKeyR = pCln_Distance.OrderBy(kvp => kvp.Value).First();

      return pKeyR.Key;

    }



  }
}
