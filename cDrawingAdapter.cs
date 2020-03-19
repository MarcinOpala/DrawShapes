using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape {

  public class cDrawingAdapter {

    private cDrawing mDrawing;
    private static cPoint mCircleCenter;                    //środek figury opisanej na okręgu

    internal static cPoint CircleCenter { get { return mCircleCenter; } set { mCircleCenter = value; } }
    internal cDrawing Drawing { get { return mDrawing; } set { mDrawing = value; } }

    public cDrawingAdapter(){

      cDrawing pDrawing;

      pDrawing = new cDrawing();

      mDrawing = pDrawing;

    }

    public cDrawing GetDrawing(cPolygon xPolygon) {
      //funkcja 
      //xPolygon - poligon bazowy

      int pCount;                                           //liczba boków figury
      int pIndex;                                           //indeks iteracji
      cSegment pSegment;
      cDrawingItem pDrawingItem;
      cDrawing pDrawing;

      pDrawing = new cDrawing();

      pCount = xPolygon.Segments.Count;
     
      for (pIndex = 1; pIndex <= pCount; pIndex++) {        //pętla tworząca i dodająca DrawingItemy

        pSegment = xPolygon.Assembly.AssemblyItems[pIndex].Polygon.Segments[1];

        pDrawingItem = CreateDrawingItem(pSegment);

        pDrawing.AddItem(pDrawingItem, pIndex);

      }

      return pDrawing;

    }

    public cDrawingItem CreateDrawingItem(cSegment xSegment) {
      //funkcja tworząca DrawingItemy
      //xSegment - segment oryginalny

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


