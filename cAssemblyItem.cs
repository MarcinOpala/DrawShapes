using System;

namespace DrawShape {

  public class cAssemblyItem {

    private cPolygon mPolygon;

    internal cPolygon Polygon { get { return mPolygon; } set { mPolygon = value; } }

    public cAssemblyItem() {

      mPolygon = new cPolygon();

    }

    public void CreateAssemblyItem_Profile(cSegment xSegment, int xWidth)  {
      //funkcja tworząca AssemblyItem na podstawie boku wielokąta
      //xSegment - oryginalny bok wielokąta
      //xWidth - szerokość profilu
      
      cSegment pSegment;
      float pOffset_X, pOffset_Y;

      //Bok 1 punkt z oryginału
      pSegment = new cSegment(xSegment.Point, 1, mPolygon);
      mPolygon.AddSegment(pSegment);



      //Bok 2 punkt z oryginał_next
      pSegment = new cSegment(xSegment.Segment_Next.Point, 2, mPolygon);
      mPolygon.AddSegment(pSegment);



      //Bok 3 przypisujemy wartość segmentu, z którego robimy przesunięcie
      pSegment = new cSegment(xSegment.Segment_Next.Point, 3, mPolygon);

      //obliczenie przesunięcia potrzebnego do wygenerowania pozycji punktu dla boku numer 3
      CalculatePointOffset(xSegment.Segment_Next, xWidth, out pOffset_X, out pOffset_Y);

      pSegment.Point.X += pOffset_X;
      pSegment.Point.Y += pOffset_Y;

      mPolygon.AddSegment(pSegment);



      //Bok 4 przypisujemy wartość segmentu, z którego robimy przesunięcie
      pSegment = new cSegment(xSegment.Point, 4, mPolygon);

      //obliczenie przesunięcia potrzebnego do wygenerowania pozycji punktu dla boku numer 4
      CalculatePointOffset(xSegment, xWidth, out pOffset_X, out pOffset_Y);
     
      pSegment.Point.X += pOffset_X;
      pSegment.Point.Y += pOffset_Y;

      mPolygon.AddSegment(pSegment);
     
    }

    private static void CalculatePointOffset(cSegment xSegment, int xWidth, out float xPtX_Offset, out float xPtY_Offset) {
      //funkcja zwracająca wartość przesunięcia punktów. 
      //działanie: pokrywający się z segment_next vektor (o długości przekątnej profilu) obracamy o kąt przy podstawie profilu
      //xSegment - bazowy segment
      //xWidth - szerokość profilu
      //xPtX_Offset - końcowe przesunięcie X
      //xPtY_Offset - końcowe przesunięcie Y

      double pAlfa, pCosAlfa, pSinAlfa;                              //kąt pomiędzy Vector_Helper, a Vektor_Segments
      double pBeta, pSinBeta, pCosBeta, pBetaInRadius;               //kąt przy podstawie profilu == połowa kąta wieloboku foremnego
      double pDiagonalSize;                                          //długość przekątnej przy łączeniu profili
      double pVectorX_SegNext, pVectorY_SegNext;                     //vektor pokrywający się z Segment_Next
      double pVectorX_Helper, pVectorY_Helper;                       //vektor pomocniczy równoległy do osi OX zaczepiony w punkcie segment.point
      double pLenghtVector_Segments, pLenghtVector_Helper;           //długości wektorów
      double pPt_X, pPt_Y;                                           //pośrednie wartości przesunięcia

      xSegment.Segment_Next.SetPolygon_Parent(xSegment.Polygon_Parent);

      pVectorX_SegNext = xSegment.Segment_Next.Point.X - xSegment.Point.X;
      pVectorY_SegNext = xSegment.Segment_Next.Point.Y - xSegment.Point.Y;

      pVectorX_Helper = 100;
      pVectorY_Helper = 0;

      pLenghtVector_Segments = Math.Sqrt(pVectorX_SegNext * pVectorX_SegNext + pVectorY_SegNext * pVectorY_SegNext);
      pLenghtVector_Helper = Math.Sqrt(pVectorX_Helper * pVectorX_Helper + pVectorY_Helper * pVectorY_Helper);

      pCosAlfa = (pVectorX_Helper * pVectorX_SegNext + pVectorY_Helper * pVectorY_SegNext) /
                 (pLenghtVector_Segments * pLenghtVector_Helper);

      //ustawienie kąta Alfa
      if (pCosAlfa == 0 && pVectorY_SegNext > 0) {
        pAlfa = 90;

      } else if (pCosAlfa == 0 && pVectorY_SegNext < 0) {
        pAlfa = 270;

      } else 
        pAlfa = (Math.Acos(pCosAlfa));

      pSinAlfa = Math.Sin((pAlfa * (Math.PI)) / 180);
      
      pBeta = 360 / xSegment.Polygon_Parent.Segments.Count / 2;
      pBetaInRadius = (pBeta * (Math.PI)) / 180;

      pSinBeta = Math.Sin(pBetaInRadius);
      pCosBeta = Math.Cos(pBetaInRadius);

      pDiagonalSize = xWidth / Math.Sin(pBetaInRadius);

      pPt_X = (pDiagonalSize * pCosAlfa);
      pPt_Y = (pDiagonalSize * pSinAlfa);

      xPtX_Offset = (float)(pPt_X * pCosBeta - pPt_Y * pSinBeta);
      xPtY_Offset = (float)(pPt_X * pSinBeta + pPt_Y * pCosBeta);

    }

  }

}
