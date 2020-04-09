using System;

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

    internal void CreateAssemblyItem_Mullion(cPolygon xPolygon, float xMullionWidth, int xC, int xMullionPosition_X, int xMullionPosition_Y) {
      // !!! TO EDIT !!! - obecnie nie działa
      //funkcja tworząca AssemblyItem - kształt słupka (xPolygon skrócony o szerokość profilu / słupka)
      //xPolygon - Polygon bazowy słupka
      //xMullionWidth - szerokość słupka
      //xC - odległość od osi słupka do skrzydła, które dodamy
      //xMullionPosition_X - pozycja słupka X
      //xMullionPosition_Y - pozycja słupka Y

      cAssembly pAssembly_Virtual;
      cPoint pPoint;
      cPolygon pPolygon;
      cSegment pSegment_Base;
      cSegment pSegment;
      int pIdx;
      float pWidth, pHeight;

      mPolygon.Parent = xPolygon;
      mC = xC;
      mPolygon.CntPF = PolygonFunctionalityEnum.Mullion;

      pAssembly_Virtual = xPolygon.Parent.Assembly;

      if (xMullionPosition_Y == 0) {    // jeśli słupek jest pionowy

        //obliczamy dla każdego boku wysykość o którą będziemy skracać
        foreach (cAssemblyItem pAssemblyItem in pAssembly_Virtual.AssemblyItems.Values) {
          pIdx = pAssemblyItem.Index;
          pPolygon = pAssemblyItem.Polygon;

          if (pPolygon.CntPF == PolygonFunctionalityEnum.Mullion) { //jeśli przecinamy inny słupek
            if (pIdx == 1 || pIdx == 2)
              pHeight = (xMullionWidth / 2);
            else
              pHeight = -(xMullionWidth / 2);
          } else                                                    //jeśli przecinamy profil
            pHeight = (pPolygon.Segments[4].Point.Y - pPolygon.Segments[1].Point.Y);

          //inicjujemy nowy, skrócony bok
          pSegment_Base = xPolygon.Segments[pIdx];
          pPoint = new cPoint(pSegment_Base.Point.X, pSegment_Base.Point.Y + pHeight);
          pSegment = new cSegment(pPoint, pIdx, false, xPolygon);

          mPolygon.AddSegment(pSegment);
        }
      } else if (xMullionPosition_X == 0) {   //jeśli słupek jest poziomy
        //obliczamy dla każdego boku szerokość o którą będziemy skracać
        foreach (cAssemblyItem pAssemblyItem in pAssembly_Virtual.AssemblyItems.Values) {
          pIdx = pAssemblyItem.Index;
          pPolygon = pAssemblyItem.Polygon;

          if (pPolygon.CntPF == PolygonFunctionalityEnum.Mullion) {   //jeśli przecinamy inny słupek
            if (pIdx == 2 || pIdx == 3)
              pWidth = -(xMullionWidth / 2);
            else
              pWidth = (xMullionWidth / 2);
          } else                                                      //jeśli przecinamy profil
            pWidth = (pPolygon.Segments[4].Point.X - pPolygon.Segments[1].Point.X);

          //inicjujemy nowy, skrócony bok
          pSegment_Base = xPolygon.Segments[pIdx];
          pPoint = new cPoint(pSegment_Base.Point.X + pWidth, pSegment_Base.Point.Y);
          pSegment = new cSegment(pPoint, pIdx, false, xPolygon);

          mPolygon.AddSegment(pSegment);
        }
      } else { }

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
