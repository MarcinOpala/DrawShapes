using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape
{
  
  public class cPolygon

  {
    public PointF[] pSegmentPoints = new PointF[2];

   public int pIsSelected = new int ();        //1, 2, 3, 4
   public int pNumberOfSide = new int();
    public int pCheckCirclePoint = new int();   // nr ćwiartki S point

    public  double pVectorXNormalToStart_End = new double();
  public  double pVectorYNormalToStart_End = new double();

  public  float Sx = new float();
    public float Sy = new float();
    public Pen pBluePen = new Pen(Color.Blue, 3);


    public void DrawRectangle(PaintEventArgs e) {
      //funkcja rysująca prostokąt z listy punktów w klasnie cSegment
      //

      pIsSelected = 3;        //1, 2, 3, 4
      pNumberOfSide = 4;
      pCheckCirclePoint = 3;      // nr ćwiartki S point

      cPolygonFactory.GetNewSegmentList(cPolygonFactory.pRectangleSegmentList);
      cPolygonFactory.ShowNewSegmentList(cPolygonFactory.pRectangleSegmentList);
      DrawWithOutSelected(e, pSegmentPoints, cPolygonFactory.pRectangleSegmentList);
      DrawBezierPointF(e, pSegmentPoints);



      pIsSelected = 10;        //1, 2, 3, 4
      pNumberOfSide = 10;
      pCheckCirclePoint = 3;      // nr ćwiartki S point
      cPolygonFactory.GetNewSegmentList(cPolygonFactory.pRegularPolygonCSegmentList);
      cPolygonFactory.ShowNewSegmentList(cPolygonFactory.pRegularPolygonCSegmentList);
      DrawWithOutSelected(e, pSegmentPoints, cPolygonFactory.pRegularPolygonCSegmentList);
      DrawBezierPointF(e, pSegmentPoints);

      /*      for (int i = 0; i <= 10- 1; i++) {
              if (i == 9)
              {
                pSegmentPoints[0] = new PointF(cPolygonFactory.pRegularPolygonCSegmentList[i].pPointOfSegment.x,
                                                cPolygonFactory.pRegularPolygonCSegmentList[i].pPointOfSegment.y);
                pSegmentPoints[1] = new PointF(cPolygonFactory.pRegularPolygonCSegmentList[0].pPointOfSegment.x,
                                                cPolygonFactory.pRegularPolygonCSegmentList[0].pPointOfSegment.y);
                pCheckCirclePoint = 3;
                DrawBezierPointF(e, pSegmentPoints);
              }
              else
              {
                pSegmentPoints[0] = new PointF(cPolygonFactory.pRegularPolygonCSegmentList[i].pPointOfSegment.x,
                                                cPolygonFactory.pRegularPolygonCSegmentList[i].pPointOfSegment.y);
                pSegmentPoints[1] = new PointF(cPolygonFactory.pRegularPolygonCSegmentList[i + 1].pPointOfSegment.x,
                                                cPolygonFactory.pRegularPolygonCSegmentList[i + 1].pPointOfSegment.y);
                e.Graphics.DrawPolygon(pBluePen, pSegmentPoints);
              }
            }*/



    }

    private void DrawBezierPointF(PaintEventArgs e, PointF[] pSegmentPoints) //Rebuild!!!!
    {

      if ((pSegmentPoints[0].X - pSegmentPoints[1].X) == 0)
      {
        pVectorXNormalToStart_End = Math.Abs((pSegmentPoints[1].Y - pSegmentPoints[0].Y) / 4);
        pVectorYNormalToStart_End = 0;
        Console.WriteLine("|||||||");
      }
      else if ((pSegmentPoints[0].Y - pSegmentPoints[1].Y) == 0)
      {
        pVectorXNormalToStart_End = 0;
        pVectorYNormalToStart_End = Math.Abs((pSegmentPoints[1].X - pSegmentPoints[0].X) / 4);
        Console.WriteLine("_______");
      }
      else
      {
        pVectorXNormalToStart_End = Math.Abs((pSegmentPoints[0].Y - pSegmentPoints[1].Y) / (pSegmentPoints[0].X - pSegmentPoints[1].X))*100;
        pVectorYNormalToStart_End = 1;

        Console.WriteLine(pVectorXNormalToStart_End + " ///// ///// \\\\\\\\ " + (pSegmentPoints[0].Y - pSegmentPoints[1].Y) + " "
          + (pSegmentPoints[0].X - pSegmentPoints[1].X));
      }



      Sx = (pSegmentPoints[0].X + pSegmentPoints[1].X) / 2;
      Sy = (pSegmentPoints[0].Y + pSegmentPoints[1].Y) / 2;

      if (pCheckCirclePoint == 1)
      {
        Sx = Sx + (float)pVectorXNormalToStart_End;
        Sy = Sy + (float)pVectorYNormalToStart_End;
      }
      else if (pCheckCirclePoint == 2)
      {
        Sx = Sx - (float)pVectorXNormalToStart_End;
        Sy = Sy + (float)pVectorYNormalToStart_End;
      }
      else if (pCheckCirclePoint == 3)
      {
        Sx = Sx - (float)pVectorXNormalToStart_End;
        Sy = Sy - (float)pVectorYNormalToStart_End;
      }
      else
      {
        Sx = Sx + (float)pVectorXNormalToStart_End;
        Sy = Sy - (float)pVectorYNormalToStart_End;
      }

      float S1x = (pSegmentPoints[0].X + Sx) / 2;
      float S1y = (pSegmentPoints[0].Y + Sy) / 2;

      float S2x = (Sx + pSegmentPoints[1].X) / 2;
      float S2y = (Sy + pSegmentPoints[1].Y) / 2;

      Console.WriteLine(" S: (" + Sx + " ; " + Sy + ") " +
                       " S1: (" + S1x + " ; " + S1y + ") " +
                       " S2: (" + S2x + " ; " + S2y + ") ");
      Console.WriteLine();
      Console.WriteLine(pVectorXNormalToStart_End + "  <<vektor x \n" +
                        pVectorYNormalToStart_End + "  <<vektor y \n" +
                       "P1(" + pSegmentPoints[0].X + " ; " + pSegmentPoints[0].Y + ") \n" +
                       "P2(" + pSegmentPoints[1].X + " ; " + pSegmentPoints[1].Y + ") \n");

      PointF start = new PointF(pSegmentPoints[0].X, pSegmentPoints[0].Y);
      PointF control1 = new PointF(S1x, S1y);
      PointF control2 = new PointF(S2x, S2y);
      PointF end = new PointF(pSegmentPoints[1].X, pSegmentPoints[1].Y);
      e.Graphics.DrawBezier(pBluePen, start, control1, control2, end);
    }

    public void DrawWithOutSelected(PaintEventArgs e, PointF[] xSegmentPoints, List<cSegment> xSegmentList)
    {


      //opis to add !!!!
      for (int i = 0; i <= pIsSelected - 2; i++)
      {
        if (i == (pNumberOfSide - 1))
        {
          xSegmentPoints[0] = new PointF(xSegmentList[i].pPointOfSegment.x,
                                         xSegmentList[i].pPointOfSegment.y);
          xSegmentPoints[1] = new PointF(xSegmentList[0].pPointOfSegment.x,
                                         xSegmentList[0].pPointOfSegment.y);
        }
        else
        {
          xSegmentPoints[0] = new PointF(xSegmentList[i].pPointOfSegment.x,
                                         xSegmentList[i].pPointOfSegment.y);
          xSegmentPoints[1] = new PointF(xSegmentList[i + 1].pPointOfSegment.x,
                                         xSegmentList[i + 1].pPointOfSegment.y);
        }
        e.Graphics.DrawPolygon(pBluePen, xSegmentPoints);
      }

      //opis to add!!!
      for (int i = pIsSelected; i <= pNumberOfSide - 1; i++)
      {
        if (i == (pNumberOfSide - 1))
        {
          xSegmentPoints[0] = new PointF(xSegmentList[i].pPointOfSegment.x,
                                         xSegmentList[i].pPointOfSegment.y);
          xSegmentPoints[1] = new PointF(xSegmentList[0].pPointOfSegment.x,
                                         xSegmentList[0].pPointOfSegment.y);
        }
        else
        {
          xSegmentPoints[0] = new PointF(xSegmentList[i].pPointOfSegment.x,
                                         xSegmentList[i].pPointOfSegment.y);
          xSegmentPoints[1] = new PointF(xSegmentList[i + 1].pPointOfSegment.x,
                                         xSegmentList[i + 1].pPointOfSegment.y);
        }
        e.Graphics.DrawPolygon(pBluePen, xSegmentPoints);
      }

      xSegmentPoints[0] = new PointF(xSegmentList[pIsSelected - 1].pPointOfSegment.x,
                                     xSegmentList[pIsSelected - 1].pPointOfSegment.y);

      if (pIsSelected == pNumberOfSide) pIsSelected = 0;
      xSegmentPoints[1] = new PointF(xSegmentList[pIsSelected].pPointOfSegment.x,
                                     xSegmentList[pIsSelected].pPointOfSegment.y);




    }


    public void DrawRegularPolygon() {
      //funkcja rysująca wielokąt foremny z listy punktów w klasnie cSegment
      //


    }
  }
}
