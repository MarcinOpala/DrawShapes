using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape {


  public class cDrawing {

    private Dictionary<int, cDrawingItem> mDrawingItems;

    internal Dictionary<int, cDrawingItem> DrawingItem { get { return mDrawingItems; } set { mDrawingItems = value; } }

    public cDrawing() {

      mDrawingItems = new Dictionary<int, cDrawingItem>();

    }

    internal void AddItem(cDrawingItem xDrawingItem, int xNumber) {
      //funkcja dodająca nowy item do listy
      //xDrawingItem - wybrany item
      //xNumber - numer itemu

      mDrawingItems.Add(xNumber, xDrawingItem);

    }

    public void Draw(cPolygon xPolygon, double xScale, cPoint xPt_Base, PaintEventArgs e) {
      //funkcja rysująca wielobok
      //xPolygon - poligon bazowy
      //xScale - skala rysunku
      //xBasePtX - współrzędne X punktu bazowego
      //xBasePtY - współrzędne Y punktu bazowego

      int pCount;
      int pIndex;
      pCount = xPolygon.Segments.Count;

      //pętla wywołująca rysowanie DrawingItemów
      for (pIndex = 1; pIndex <= (pCount); pIndex++) {  

        DrawItem(pIndex, xScale, xPt_Base, e);

      }

    }

    public void DrawItem(int xIndex, double xScale, cPoint xPt_Base, PaintEventArgs e) {
      //funkcja rysująca Itemy

      Pen pBluePen;                                         //kolor długopisu
      PointF[] pLinePoints;                                 //punkty tworzące linie
      cSegment pSegment;
      cSegment pSegment_Next;
      int pIndex;
      cDrawingItem pDrawingItem;

      pBluePen = new Pen(Color.Blue, 3);
      pLinePoints = new PointF[2];

      pDrawingItem = mDrawingItems[xIndex];

      Console.WriteLine("Punkty wielokąta");

      for (pIndex = 1; pIndex <= pDrawingItem.DrawingSegments.Count; pIndex++) {

        
        pSegment = pDrawingItem.GetSegmentByNumer(pIndex).Segment;
        pSegment_Next = pDrawingItem.GetSegmentByNumer(pIndex + 1).Segment;

        //przekształcenie punktu poligonu na współrzędne do wyświetlenia na Canvas
        var pPoints = TransformPoints(pSegment, pSegment_Next, xScale, xPt_Base);



        //szukanie błędu przy ośmiokącie bok 6
/*        Console.WriteLine(">>> <<<<  " + xScale);
        Console.WriteLine($"PtA:{pPoints.Pt_A.X - pPoints.Pt_B.X}");  //, {pPoints.Pt_A.Y}    PtB:{pPoints.Pt_B.X}, {pPoints.Pt_B.Y}");
*/



        if (!pSegment.IsCurve) {                            //jeśli segment jest prosty
          pLinePoints[0] = new PointF(pPoints.Pt_A.X, pPoints.Pt_A.Y);
          pLinePoints[1] = new PointF(pPoints.Pt_B.X, pPoints.Pt_B.Y);

          e.Graphics.DrawPolygon(pBluePen, pLinePoints);

        } else {                                            //jeśli segment jest krzywywą
          DrawBezierCurve(pSegment, pPoints.Pt_A, pPoints.Pt_B, xIndex, e);

        }
      }
    }

    private (PointF Pt_A, PointF Pt_B) TransformPoints(cSegment xSegment, cSegment xSegment_Next,
                                    double xScale, cPoint xPt_Base) {
      //funkcja przekształcenie punktu poligonu na współrzędne do wyświetlenia na Canvas
      //xSegment - segment figury
      //xSegment_Next - następny segment figury
      //xScale - skala figury
      //xPt_Base - przesuniecie punktu bazowego
      //xPt_A - współrzędne punktów przeskalowanych
      //xPt_B - współrzędne punktów przeskalowanych

      PointF pPt_A_;
      PointF pPt_B_;
 
      pPt_A_ = new PointF();
      pPt_A_.X = (xPt_Base.X + (float)(xSegment.Point.X * xScale));
      pPt_A_.Y = (xPt_Base.Y - (float)(xSegment.Point.Y * xScale));

      pPt_B_ = new PointF();
      pPt_B_.X = (xPt_Base.X + (float)(xSegment_Next.Point.X * xScale));
      pPt_B_.Y = (xPt_Base.Y - (float)(xSegment_Next.Point.Y * xScale));

      return (Pt_A: pPt_A_, Pt_B: pPt_B_);

    }

    internal void DrawBezierCurve(cSegment xSegment, PointF xPt_A, PointF xPt_B, int xIndex, PaintEventArgs e) {
      //funkcja rysująca łuk po wybraniu boku
      //xSegment - segmentu pierwszy
      //xPt_A - współrzędne punktów przeskalowanych
      //xPt_B - współrzędne punktów przeskalowanych

      Pen pBluePen;                                         //kolor długopisu
      float pCenterX, pCenterY;                             //punkt środka segmentu
      float pControl_2X;                                    //punkt X punktu kotrolnego 1 do rysowania łuku 1:4 odcinka segmentu
      float pControl_2Y;                                    //punkt Y punktu kotrolnego 1 do rysowania łuku
      float pControl_3X;                                    //punkt X punktu kotrolnego 2 do rysowania łuku 
      float pControl_3Y;                                    //punkt Y punktu kotrolnego 2 do rysowania łuku
      PointF pControlPt1_Bezier;                            //punkt pomocniczy do rysowania krzywej
      PointF pControlPt2_Bezier;                            //punkt pomocniczy do rysowania krzywej
      PointF pControlPt3_Bezier;                            //punkt pomocniczy do rysowania krzywej
      PointF pControlPt4_Bezier;                            //punkt pomocniczy do rysowania krzywej
      int pIncreaseVector;                                  //sztuczne zwiększenie wartości vektora
      double pVectNormalX;                                  //X wartość wektora prostopadłego do segmentu
      double pVectNormalY;                                  //Y wartość wektora prostopadłego do segmentu
      int pQuarterNumber;

      pCenterX = (xPt_A.X + xPt_B.X) / 2;
      pCenterY = (xPt_A.Y + xPt_B.Y) / 2;

      pIncreaseVector = 50;
      pBluePen = new Pen(Color.Blue, 3);

      //przeliczanie wektorów w zależności od położenia segmentu wzgledem środka koła
      if (((int)(xPt_A.X) - (int)(xPt_B.X)) == 0) {                       // f(x)=0
        pVectNormalX = Math.Abs((xPt_B.Y - xPt_A.Y) / 4);
        pVectNormalY = 0;

      } else if ((xPt_A.Y - xPt_B.Y == 0)) {                // f(y)=0
        pVectNormalX = 0;
        pVectNormalY = Math.Abs((xPt_B.X - xPt_A.X) / 4);

      } else {                                              // f(y)=ax+b
        pVectNormalX = Math.Abs((xPt_A.Y - xPt_B.Y) / (xPt_A.X - xPt_B.X)) * pIncreaseVector;
        pVectNormalY = pIncreaseVector;

      }

      //ustawienie nr cwiartki w której jest segment
      SetQuarterNumber(xSegment, xIndex, out pQuarterNumber);

      //przesunięcie środka segmentu o wektor 
      //w zależności od ćwiartki w której się znajduję środek koła,
      //na którym opisana jest figura
      if (pQuarterNumber == 1) {
        pCenterX = pCenterX + (float)pVectNormalX;
        pCenterY = pCenterY + (float)pVectNormalY;

      } else if (pQuarterNumber == 2) {
        pCenterX = pCenterX - (float)pVectNormalX;
        pCenterY = pCenterY + (float)pVectNormalY;

      } else if (pQuarterNumber == 3) {
        pCenterX = pCenterX - (float)pVectNormalX;
        pCenterY = pCenterY - (float)pVectNormalY;

      } else {
        pCenterX = pCenterX + (float)pVectNormalX;
        pCenterY = pCenterY - (float)pVectNormalY;

      }

      pControl_2X = (xPt_A.X + pCenterX) / 2;
      pControl_2Y = (xPt_A.Y + pCenterY) / 2;
      pControl_3X = (pCenterX + xPt_B.X) / 2;
      pControl_3Y = (pCenterY + xPt_B.Y) / 2;

      //punkty po przesunięciu o vektor
      pControlPt1_Bezier = new PointF(xPt_A.X, xPt_A.Y);
      pControlPt2_Bezier = new PointF(pControl_2X, pControl_2Y);
      pControlPt3_Bezier = new PointF(pControl_3X, pControl_3Y);
      pControlPt4_Bezier = new PointF(xPt_B.X, xPt_B.Y);

      e.Graphics.DrawBezier(pBluePen, pControlPt1_Bezier, pControlPt2_Bezier, pControlPt3_Bezier, pControlPt4_Bezier);

    }

    internal void SetQuarterNumber(cSegment xSegment, int xIndex, out int xQuarterNumber) {
      //fukcja ustawiająca wartość xQuarterNumber w zależności od tego w której ćwiartce znajduje się środek segmentu
      //xSegment - segment
      //xQuarterNumber - nr ćwiartki liczona według schematu:      3|4 
      //                                                           2|1
      
      cPoint pPt_Base_ItemNext;
      cPoint pPt_Base_Item;

      //punkt bazowy DrawingItemu
      pPt_Base_Item = new cPoint();
      pPt_Base_Item.X = GetDrawingItemByNumber(xIndex).DrawingSegments[1].Segment.Point.X;
      pPt_Base_Item.Y = GetDrawingItemByNumber(xIndex).DrawingSegments[1].Segment.Point.Y;

      //punkt bazowy nastepnego DrawingItemu
      pPt_Base_ItemNext = new cPoint();
      pPt_Base_ItemNext.X = GetDrawingItemByNumber(xIndex + 1).DrawingSegments[1].Segment.Point.X;
      pPt_Base_ItemNext.Y = GetDrawingItemByNumber(xIndex + 1).DrawingSegments[1].Segment.Point.Y;


      xQuarterNumber = new int();

      if (pPt_Base_ItemNext.X <= pPt_Base_Item.X && pPt_Base_ItemNext.Y < pPt_Base_Item.Y) {
        xQuarterNumber = 3;

      } else if (pPt_Base_ItemNext.X > pPt_Base_Item.X && pPt_Base_ItemNext.Y >= pPt_Base_Item.Y) {
        xQuarterNumber = 1;      

      } else if (pPt_Base_ItemNext.X > pPt_Base_Item.X && pPt_Base_ItemNext.Y < pPt_Base_Item.Y) {
        xQuarterNumber = 2;

      } else if (pPt_Base_ItemNext.X <= pPt_Base_Item.X && pPt_Base_ItemNext.Y >= pPt_Base_Item.Y) {
        xQuarterNumber = 4;

      } else {
        Console.WriteLine("Błąd liczenia ćwiartki");
      }

    }

    public cDrawingItem GetDrawingItemByNumber(int xNumber) {
      //funkcja zwracająca segment
      //xSegmentNumber - numer segmentu

      int pItemNumber;
      int pCountMax;

      pCountMax = mDrawingItems.Count;

      pItemNumber = xNumber;

      if (xNumber > pCountMax)
        pItemNumber = 1;

      return mDrawingItems[pItemNumber];

    }

  }

}
