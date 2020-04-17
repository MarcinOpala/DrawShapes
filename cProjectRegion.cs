using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  internal enum ProjectRegionEnum {                         //numerator typu regionu projektu
    Polygon,                                                //wielokąt
    AssemblyItem,                                           //element konstrukcji
  }

  public class cProjectRegion {

    private ProjectRegionEnum mCntProjectRegion;            //typ regionu
    private cPolygon mPolygon;                              //powiązany wielokąt
    private Region mRegion;                                 //region gdi

    internal cPolygon Polygon { get { return mPolygon; } set { mPolygon = value; } }
    internal Region Region { get { return mRegion; } set { mRegion = value; } }
    internal ProjectRegionEnum CntProjectRegion { get { return mCntProjectRegion; } set { mCntProjectRegion = value; } }

    public cProjectRegion(cPolygon xPolygon) {
      //konstruktor tworzący pojedyńczy region
      //xPolygon - wielokąt dla którego ma być utworzony region

      GraphicsPath xPath;
      PointF[] pPoints;

      pPoints = new PointF[xPolygon.Segments.Count];

      foreach (cSegment pSegment in xPolygon.Segments.Values) {
        pPoints[pSegment.Index-1] = new PointF(pSegment.Point.X, pSegment.Point.Y);
 
      }

      xPath = new GraphicsPath();
      xPath.AddPolygon(pPoints);

      mPolygon = xPolygon;
      mRegion = new Region(xPath);

      mCntProjectRegion = ProjectRegionEnum.Polygon;

    }

    public cProjectRegion(cAssemblyItem xAssemblyItem) {
      //konstruktor tworzący pojedyńczy region
      //xAssemblyItem - element dla którego ma być utworzony region

      GraphicsPath xPath;
      PointF[] pPoints;

      pPoints = new PointF[xAssemblyItem.Polygon.Segments.Count];

      foreach (cSegment pSegment in xAssemblyItem.Polygon.Segments.Values) {
        pPoints[pSegment.Index - 1] = new PointF(pSegment.Point.X, pSegment.Point.Y);

      }

      xPath = new GraphicsPath();
      xPath.AddPolygon(pPoints);

      mPolygon = xAssemblyItem.Polygon;
      mRegion = new Region(xPath);
      mCntProjectRegion = ProjectRegionEnum.AssemblyItem;

    }

  }

}
