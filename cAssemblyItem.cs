using System;
using System.Collections.Generic;

namespace DrawShape {
  
  public class cAssemblyItem {

    private cLine mAxis_Symmetry;                   //równanie prostej pokrywające oś symetrii
    private int mIndex;                                     //numer AssemblyItemu
    private cPolygon mPolygon;                              //Polygon AssemblyItemu
    private cPolygon mParent;                               //rodzic wielokąta
    private int mWidth_Profile;                             //szerokość profilu
    private int mC;                                         //odległość od krawędzi do następnego elementu
    private cAssembly mAssemblyParent;

    internal cLine Axis_Symmetry { get { return mAxis_Symmetry; } set { mAxis_Symmetry = value; } }
    internal int Index { get { return mIndex; } set { mIndex = value; } }
    internal cPolygon Polygon { get { return mPolygon; } set { mPolygon = value; } }
    internal cPolygon Parent { get { return mParent; } set { mParent = value; } }
    internal int C { get { return mC; } }
    internal int Width_Profile { get { return mWidth_Profile; } }

    internal cAssembly AssemblyParent { get { return mAssemblyParent; } }
    internal cAssemblyItem AssemblyItem_Next { get { return GetAssemblyItem_Next(); } }


    public cAssemblyItem() {
      //xIndex - numer AssemblyItemu

      mPolygon = new cPolygon();
      
    }

    public cAssemblyItem(int xIndex) {
      //xIndex - numer AssemblyItemu

      mPolygon = new cPolygon(xIndex);
      mIndex = xIndex;

    }

    public void CreateAssemblyItem_Profile(cSegment xSegment, int xWidth_Profile, int xC) {
      //funkcja tworząca AssemblyItem - kształt profilu na podstawie boku wielokąta
      //xSegment - oryginalny bok wielokąta
      //xWidth_Profile - szerokość profilu
      //xC - stała C dla każdego profilu

      cSegment pSegment;
      float pOffset_X, pOffset_Y;
      cLine pLine;

      mPolygon.CntPF = PolygonFunctionalityEnum.Profile;
      mWidth_Profile = xWidth_Profile;
      mC = xC;

      //Bok 1 punkt z oryginału
      pSegment = new cSegment(xSegment.Point, 1, xSegment.IsCurve, mPolygon);
      mPolygon.AddSegment(pSegment);

      //Bok 2 punkt z oryginał_next
      pSegment = new cSegment(xSegment.Segment_Next.Point, 2, false, mPolygon);
      mPolygon.AddSegment(pSegment);

      //Bok 3 przypisujemy wartość boku, z którego później robimy przesunięcie
      pSegment = new cSegment(xSegment.Segment_Next.Point, 3, xSegment.Segment_Next.IsCurve, mPolygon);
      pLine = new cLine(xSegment); //prosta pokrywająca się z bokiem oryginału

      //obliczenie przesunięcia potrzebnego do wygenerowania pozycji punktu dla boku numer 3
      CalculatePointOffset(xSegment.Segment_Next, xWidth_Profile, pLine, out pOffset_X, out pOffset_Y);

      pSegment.Point.X += pOffset_X;
      pSegment.Point.Y += pOffset_Y;
      mPolygon.AddSegment(pSegment);

      //Bok 4 przypisujemy wartość boku, z którego później robimy przesunięcie
      pSegment = new cSegment(xSegment.Point, 4, false, mPolygon);
      pLine = new cLine(xSegment.Segment_Before); //prosta pokrywająca się z bokiem poprzedzającym oryginał

      //obliczenie przesunięcia potrzebnego do wygenerowania pozycji punktu dla boku numer 4
      CalculatePointOffset(xSegment, xWidth_Profile, pLine, out pOffset_X, out pOffset_Y);

      pSegment.Point.X += pOffset_X;
      pSegment.Point.Y += pOffset_Y;
      mPolygon.AddSegment(pSegment);

    }

    private void CalculatePointOffset(cSegment xSegment, int xWidth_Profile, cLine xLine,
                                             out float xPtX_Offset, out float xPtY_Offset) {
      //funkcja zwracająca wartość przesunięcia punktów. 
      //działanie: pokrywający się z segment_next vektor (o długości przekątnej profilu) obracamy o kąt przy podstawie profilu
      //xSegment - bazowy segment
      //xWidth_Profile - szerokość profilu
      //xLine - prosta względem której liczymy kąt
      //xPtX_Offset - końcowe przesunięcie X
      //xPtY_Offset - końcowe przesunięcie Y

      double pAlfa, pCosAlfa, pSinAlfa, pAlfaInRadian;
      double pBeta, pSinBeta, pCosBeta, pBetaInRadius;
      double pDiagonalSize;
      double pPt_X, pPt_Y;
      cVector pVector_SegNext, pVector_OX;
      cLine pLine_1;

      pVector_SegNext = new cVector(xSegment, xSegment.Segment_Next);  //wektor pokrywający się z następnym bokiem

      pVector_OX = new cVector(100, 0);                                //wektor pomocniczy pokrywający się z osią OX

      pCosAlfa = pVector_OX.CosAlfa(pVector_OX, pVector_SegNext);         //cosinus kąta pomiędzy wektorami

      //ustawienie kąta Alfa
      if (pCosAlfa >= 0 && pVector_SegNext.Vector.Y >= 0) {
        pAlfa = (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      } else if (pCosAlfa < 0 && pVector_SegNext.Vector.Y > 0) {
        pAlfa = (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      } else if (pCosAlfa <= 0 && pVector_SegNext.Vector.Y <= 0) {
        pAlfa = 180 + (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      } else if (pCosAlfa >= 0 && pVector_SegNext.Vector.Y < 0) {
        pAlfa = 180 + (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      } else {
        pAlfa = (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      }
      pAlfaInRadian = (pAlfa * (Math.PI)) / 180;
      pSinAlfa = Math.Sin(pAlfaInRadian);

      pLine_1 = new cLine(xSegment);   //prosta pokrywająca sie z bokiem

      pBeta = (180 - pLine_1.Get_Angle(xLine)) / 2; //kąt pomiędzy dwoma kolejnymi bokami

      pBetaInRadius = (pBeta * (Math.PI)) / 180;
      pSinBeta = Math.Sin(pBetaInRadius);
      pCosBeta = Math.Cos(pBetaInRadius);

      pDiagonalSize = xWidth_Profile / Math.Sin(pBetaInRadius);    //długość przekątnej profilu

      //https://www.obliczeniowo.com.pl/65 - obrót wektora o sumę kątów
      pPt_X = (pDiagonalSize * pCosAlfa);                          //punkty po obruceniu o kąt alfa (do wielokąta)
      pPt_Y = (pDiagonalSize * pSinAlfa);

      xPtX_Offset = (float)(pPt_X * pCosBeta - pPt_Y * pSinBeta);  //punkty po obruceniu o kąt beta (połowa kąta pomiędzy bokami)
      xPtY_Offset = (float)(pPt_X * pSinBeta + pPt_Y * pCosBeta);

    }

    internal void CreateAssemblyItem_Mullion(cPolygon xPolygon, Dictionary<int, cPolygon> xPolygonsVirtual,
                                             Dictionary<int, cPolygon> xPolygonsMullion, Dictionary<int, cLine> xCln_Line, int xC) {
      // !!! TO EDIT !!! - obecnie nie działa
      //funkcja tworząca AssemblyItem - kształt słupka (xPolygon skrócony o szerokość profilu / słupka)
      //xPolygon - wielokąt bazowy słupka
      //xMullionWidth - szerokość słupka
      //xC - stała C (odległość od osi słupka do skrzydła)
      //xMullionPosition_X - pozycja słupka X
      //xMullionPosition_Y - pozycja słupka Y

      cLine pLine_Mullion, pLine_AssemblySegment_1, pLine_AssemblySegment_3;
      cPoint pPointAI, pPointMullion, pPoint_New;
      cSegment pSegment_New, pSegment_AI_1, pSegment_AI_3;
      int pIdx;
      bool pCheck_Mullion, pCheck_AI, pCheck_Virtual;

      mPolygon.Parent = xPolygon;
      mC = xC;
      mPolygon.CntPF = PolygonFunctionalityEnum.Mullion;

      pIdx = 20;

      foreach (cLine pLine in xCln_Line.Values) {
        foreach (cPolygon pPolygon_Virtual in xPolygonsVirtual.Values) {
          foreach (cAssemblyItem pAssemblyItem in pPolygon_Virtual.Assembly.AssemblyItems.Values) {
          

            pSegment_AI_3 = pAssemblyItem.Polygon.Segments[3];
            pLine_AssemblySegment_3 = new cLine(pSegment_AI_3);
            pLine_AssemblySegment_3.Simplify(pLine_AssemblySegment_3);

            pSegment_AI_1 = pAssemblyItem.Polygon.Segments[1];
            pLine_AssemblySegment_1 = new cLine(pSegment_AI_1);
            pLine_AssemblySegment_1.Simplify(pLine_AssemblySegment_1);

            pPointAI = pLine.Get_PointFromCrossLines(pLine_AssemblySegment_3);

            if (xPolygonsMullion.Count != 0) {
              if (pAssemblyItem.Polygon.CntPF == PolygonFunctionalityEnum.Mullion) {
                foreach (cPolygon pPolygon_Mullion in xPolygonsMullion.Values) {
                  if (pLine_AssemblySegment_1.IsCover(pPolygon_Mullion.AssemblyItem.Axis_Symmetry)) {
                    foreach (cSegment pSegment in pPolygon_Mullion.AssemblyItem.Polygon.Segments.Values) {
                      pLine_Mullion = new cLine(pSegment);
                      pPointMullion = pLine.Get_PointFromCrossLines(pLine_Mullion);

                      pCheck_Virtual = pPolygon_Virtual.IsInclude(pPointMullion);
                      if (!pCheck_Virtual) continue;

                      pPoint_New = new cPoint(pPointMullion.X, pPointMullion.Y);
                      pSegment_New = new cSegment(pPoint_New, pIdx, mPolygon);
                      mPolygon.AddSegment(pSegment_New);
                      pIdx++;
                      break;

                    }
                  }
                }
              } else {
                if (pPointAI == null) continue;
                pCheck_Virtual = pPolygon_Virtual.IsInclude(pPointAI);
                if (!pCheck_Virtual) continue;

                pPoint_New = new cPoint(pPointAI.X, pPointAI.Y);
                pSegment_New = new cSegment(pPoint_New, pIdx, mPolygon);
                mPolygon.AddSegment(pSegment_New);
                pIdx++;

              }
            } else {                  //punkt należy tylko do AI
              if (pPointAI == null) continue;
              pCheck_Virtual = pPolygon_Virtual.IsInclude(pPointAI);
              if (!pCheck_Virtual) continue;

              pPoint_New = new cPoint(pPointAI.X, pPointAI.Y);
              pSegment_New = new cSegment(pPoint_New, pIdx, mPolygon);
              mPolygon.AddSegment(pSegment_New);
              pIdx++;

            }
          }
        }
      }
      mPolygon.Organize_Segments(mPolygon);

    }

    private cAssemblyItem GetAssemblyItem_Next() {
      //funkja zwracjąca następny AssemblyItem

      int pIndex_Next;
      int pCountMax;

      pCountMax = mParent.Assembly.AssemblyItems.Count;

      pIndex_Next = mIndex + 1;

      if (mIndex > pCountMax)
        pIndex_Next = 1;

      return mParent.Assembly.GetAssemblyItemByIndex(pIndex_Next);

    }

  }

}
