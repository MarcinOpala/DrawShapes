using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShape {

  public class cPolygon  {

    public void DrawRectangle(PaintEventArgs e) {
    //funkcja rysująca prostokąt z listy punktów w klasnie cSegment
      
    cPolygonFactory.GetSegmentsList(cPolygonFactory.RectangleSegmentList);
    cDrawingAdapter.DrawWithOutSelectedSide( cPolygonFactory.RectangleSegmentList, cDrawingAdapter.SegmentPoints, 3, e);
    cDrawingAdapter.DrawBezierCurve(cDrawingAdapter.SegmentPoints, e);
    }

    public void DrawRegularPolygon(PaintEventArgs e) {
      //funkcja rysująca wielokąt foremny z listy punktów w klasnie cSegment
      
      cPolygonFactory.GetSegmentsList(cPolygonFactory.RegularPolygonCSegmentList);
      cDrawingAdapter.DrawWithOutSelectedSide( cPolygonFactory.RegularPolygonCSegmentList, cDrawingAdapter.SegmentPoints, cDrawingAdapter.IsSelected, e);
      cDrawingAdapter.DrawBezierCurve(cDrawingAdapter.SegmentPoints, e);
    }
  }
}
