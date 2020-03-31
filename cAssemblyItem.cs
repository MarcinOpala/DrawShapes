using System;

namespace DrawShape {
  
  public class cAssemblyItem {

    private int mAxis_X;                                    //oś X AssemblyItemu
    private int mAxis_Y;                                    //oś Y AssemblyItemu
    private int mIndex;                                     //numer AssemblyItemu
    private cPolygon mPolygon;                              //Polygon AssemblyItemu
    private cPolygon mParent;                               //rodzic wielokąta
    private int mWidth_Profile;                             //szerokość profilu
    private int mC;                                         //odległość od krawędzi do następnego elementu

    internal int Axis_X { get { return mAxis_X; } set { mAxis_X = value; } }
    internal int Axis_Y { get { return mAxis_Y; } set { mAxis_Y = value; } }
    internal int Index { get { return mIndex; } set { mIndex = value; } }
    internal cPolygon Polygon { get { return mPolygon; } set { mPolygon = value; } }
    internal cPolygon Parent { get { return mParent; } set { mParent = value; } }
    internal int C { get { return mC; } }
    internal int Width_Profile { get { return mWidth_Profile; } }

    public cAssemblyItem(int xIndex) {
      //xIndex - numer AssemblyItemu

      //ag - usunąć indeks
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

      mWidth_Profile = xWidth_Profile;
      mC = xC;

      //Bok 1 punkt z oryginału
      pSegment = new cSegment(xSegment.Point, 1, xSegment.IsCurve, mPolygon);
      mPolygon.AddSegment(pSegment);

      //Bok 2 punkt z oryginał_next
      pSegment = new cSegment(xSegment.Segment_Next.Point, 2, false, mPolygon);
      mPolygon.AddSegment(pSegment);

      //Bok 3 przypisujemy wartość segmentu, z którego robimy przesunięcie
      pSegment = new cSegment(xSegment.Segment_Next.Point, 3, xSegment.Segment_Next.IsCurve, mPolygon);

      //obliczenie przesunięcia potrzebnego do wygenerowania pozycji punktu dla boku numer 3
      CalculatePointOffset(xSegment.Segment_Next, xWidth_Profile, out pOffset_X, out pOffset_Y);

      pSegment.Point.X += pOffset_X;
      pSegment.Point.Y += pOffset_Y;

      mPolygon.AddSegment(pSegment);

      //Bok 4 przypisujemy wartość segmentu, z którego robimy przesunięcie
      pSegment = new cSegment(xSegment.Point, 4, false, mPolygon);

      //obliczenie przesunięcia potrzebnego do wygenerowania pozycji punktu dla boku numer 4
      CalculatePointOffset(xSegment, xWidth_Profile, out pOffset_X, out pOffset_Y);

      pSegment.Point.X += pOffset_X;
      pSegment.Point.Y += pOffset_Y;

      mPolygon.AddSegment(pSegment);

      mPolygon.CntPF = PolygonFunctionalityEnum.Profile;

    }

    private static void CalculatePointOffset(cSegment xSegment, int xWidth_Profile, out float xPtX_Offset, out float xPtY_Offset) {
      //funkcja zwracająca wartość przesunięcia punktów. 
      //działanie: pokrywający się z segment_next vektor (o długości przekątnej profilu) obracamy o kąt przy podstawie profilu
      //xSegment - bazowy segment
      //xWidth_Profile - szerokość profilu
      //xPtX_Offset - końcowe przesunięcie X
      //xPtY_Offset - końcowe przesunięcie Y

      double pAlfa, pCosAlfa, pSinAlfa, pAlfaInRadius;
      double pBeta, pSinBeta, pCosBeta, pBetaInRadius;
      double pDiagonalSize;
      double pPt_X, pPt_Y;
      cVector pVector_SegNext, pVector_OX;

      //nowy wektor pokrywający się z następnym bokiem
      pVector_SegNext = new cVector(xSegment, xSegment.Segment_Next);

      //nowy wektor pomocniczy pokrywający się z osią OX
      pVector_OX = new cVector(100, 0);

      //kąt pomiędzy wektorami
      pCosAlfa = cVector.CosAlfa(pVector_OX, pVector_SegNext);

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

      //obliczanie sin, cos 
      pAlfaInRadius = (pAlfa * (Math.PI)) / 180;
      pSinAlfa = Math.Sin(pAlfaInRadius);
      pBeta = (180 - (360 / xSegment.Polygon_Parent.Segments.Count)) / 2;
      pBetaInRadius = (pBeta * (Math.PI)) / 180;
      pSinBeta = Math.Sin(pBetaInRadius);
      pCosBeta = Math.Cos(pBetaInRadius);

      pDiagonalSize = xWidth_Profile / Math.Sin(pBetaInRadius);    //długość przekątnej profilu

      pPt_X = (pDiagonalSize * pCosAlfa);                          //pośrednie wartości przesunięcia
      pPt_Y = (pDiagonalSize * pSinAlfa);

      xPtX_Offset = (float)(pPt_X * pCosBeta - pPt_Y * pSinBeta);  //obliczenie przesunięcia potrzebnego do wygenerowania pozycji punktu
      xPtY_Offset = (float)(pPt_X * pSinBeta + pPt_Y * pCosBeta);

    }

    internal void CreateAssemblyItem_Mullion(cPolygon xPolygon, float xWidth_Profile, int xC) {
      //funkcja tworząca AssemblyItem - kształt słupka (xPolygon skrócony o szerokość profilu)
      //xPolygon - Polygon bazowy
      //xWidth_Profile - szerokość profilu
      //xC - odległość od osi słupka do skrzydła, które dodamy

      cPoint pPoint;
      cSegment pSegment_Base;
      cSegment pSegment;

      mC = xC;

      pSegment_Base = xPolygon.Segments[1];
      pPoint = new cPoint(pSegment_Base.Point.X, pSegment_Base.Point.Y + xWidth_Profile);
      pSegment = new cSegment(pPoint, 1, false, xPolygon);

      mPolygon.AddSegment(pSegment);

      pSegment_Base = xPolygon.Segments[2];
      pPoint = new cPoint(pSegment_Base.Point.X, pSegment_Base.Point.Y + xWidth_Profile);
      pSegment = new cSegment(pPoint, 2, false, xPolygon);

      mPolygon.AddSegment(pSegment);

      pSegment_Base = xPolygon.Segments[3];
      pPoint = new cPoint(pSegment_Base.Point.X, pSegment_Base.Point.Y - xWidth_Profile);
      pSegment = new cSegment(pPoint, 3, false, xPolygon);

      mPolygon.AddSegment(pSegment);

      pSegment_Base = xPolygon.Segments[4];
      pPoint = new cPoint(pSegment_Base.Point.X, pSegment_Base.Point.Y - xWidth_Profile);
      pSegment = new cSegment(pPoint, 4, false, xPolygon);

      mPolygon.AddSegment(pSegment);

      mPolygon.Parent = xPolygon;

    }

  }

}
