using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape {

  public class cDrawingAdapter {

    private static cPoint mCircleCenter;                    //środek figury opisanej na okręgu

    internal static cPoint CircleCenter { get { return mCircleCenter; } set { mCircleCenter = value; } }

    public void DrawPolygon(cPolygon xPolygon, double xScale, double xBasePtX, double xBasePtY, PaintEventArgs e) {
      //funkcja rysująca wielobok
      //xPolygon - poligon bazowy
      //xScale - skala rysunku
      //xBasePtX - współrzędne X punktu bazowego
      //xBasePtY - współrzędne Y punktu bazowego

      int pCount;                                           //liczba boków figury
      int pIndex;                                           //indeks iteracji
      cSegment pSegment;
      cSegment pSegment_Next;

      cDrawing pDrawing;
      cDrawingItem pDrawingItem;

      pCount = xPolygon.Segments.Count;

      pDrawing = new cDrawing();

      for (pIndex = 0; pIndex <= (pCount - 1); pIndex++) {  //pętla dodająca DrawingItemy

        pSegment = xPolygon.GetSegmentByNumer(pIndex);
        pSegment_Next = xPolygon.GetSegmentByNumer(pIndex + 1);
        pDrawingItem = new cDrawingItem();

        pDrawingItem = GetItem(pIndex, pSegment, pSegment_Next);

        pDrawing.AddItem(pDrawingItem, pIndex);

      }

      for (pIndex = 0; pIndex <= (pCount - 1); pIndex++) {  //pętla wywołująca rysowanie DrawingItemów

        pDrawing.DrawItem(pIndex, xScale, xBasePtX, xBasePtY, e);

      }
    }

    public static cDrawingItem GetItem(int xNumber, cSegment xSegment, cSegment xSegment_Next) {
      //funkcja 

      int pNumber;
      cDrawingItem pDrawingItem;
      cSegment pSegment;

      pDrawingItem = new cDrawingItem();
      pNumber = xNumber + 1;

      pSegment = xSegment;
      pDrawingItem.AddSegment(new cDrawingSegment(pSegment), xNumber);

      pSegment = xSegment_Next;
      pDrawingItem.AddSegment(new cDrawingSegment(pSegment), xNumber + 1);


      //  AddProfileShape(pPolygon, xSegment, xSegment_Next);

      return pDrawingItem;

    }

    public static void AddProfileShape(cPolygon pPolygon, cSegment xSegment, cSegment xSegment_Next) {
      // funkcja dodająca profil okna

      int pWindowFrameSize;
      int pDirection;
      double pAngle;
      cSegment pSegment;
      float pOffsetX;
      float pOffsetY;
      double pCosinusAngle;                          //cosunus kąta pAngleTemp
      double pSinusAngle;                            //sinus kąta pAngleTemp

      pSegment = new cSegment(new cPoint(new PointF()), new int());
      pWindowFrameSize = 2;

      pAngle = (45 * (Math.PI)) / 180;
      pCosinusAngle = Math.Cos(pAngle);
      pSinusAngle = Math.Sin(pAngle);


      pOffsetX = pWindowFrameSize * (int)(pWindowFrameSize * pCosinusAngle);
      pOffsetY = pWindowFrameSize * (int)(pWindowFrameSize * pSinusAngle);

      pSegment.Point.X = xSegment_Next.Point.X - pOffsetX;
      pSegment.Point.Y = xSegment_Next.Point.Y + pOffsetY;
      pPolygon.AddSegment(pSegment, 2);

      pSegment.Point.X = xSegment.Point.X + pOffsetX;
      pSegment.Point.Y = xSegment.Point.Y + pOffsetY;
      pPolygon.AddSegment(pSegment, 3);
    }


  }

}


