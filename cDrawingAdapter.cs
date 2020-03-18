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
      cDrawing pDrawing;
      cDrawingItem pDrawingItem;

      pCount = xPolygon.Segments.Count;

      pDrawing = new cDrawing();

      for (pIndex = 1; pIndex <= pCount; pIndex++) {  //pętla tworząca i dodająca DrawingItemy

        pSegment = xPolygon.Assembly.AssemblyItems[pIndex].Polygon.Segments[1];

        pDrawingItem = CreateDrawingItem(pSegment);

        pDrawing.AddItem(pDrawingItem, pIndex);

      }

      for (pIndex = 1; pIndex <= (pCount); pIndex++) {  //pętla wywołująca rysowanie DrawingItemów

        pDrawing.DrawItem(pIndex, xScale, xBasePtX, xBasePtY, e);

      }
    }

    public static cDrawingItem CreateDrawingItem(cSegment xSegment) {
      //funkcja 

      cDrawingItem pDrawingItem;
      cSegment pSegment;

      pDrawingItem = new cDrawingItem();

      for (int i = 1; i <= xSegment.Polygon_Parent.Segments.Count ; i++) {

        pSegment = xSegment.Polygon_Parent.Segments[i];

        pDrawingItem.AddSegment(new cDrawingSegment(pSegment, i));

      }
     
      return pDrawingItem;

    }
    
  }

}


