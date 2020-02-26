using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DrawShape
{
  
  public class cPolygonFactory {
    //klasa tworząca Listy (punktów początkowych oraz ich numery), do rysowania figur geometrycznych.
    // 1.Prostokąt - CreateRectangle
    // 2.Wielokąt Foremny - CreateRegularPolygon

    public static List<cSegment> pRectangleSegmentList = new List<cSegment>();                             //inicjacja listy cSegmentu - figura prostokąt
    public static List<cSegment> pRegularPolygonCSegmentList = new List<cSegment>();                        //inicjacja listy cSegmentu - figura wielokąt formeny
    public static double pTotalSegment;                      
  


    public void CreateRectangle(cPoint xPoint, int xWidth, int xHeight) {
      //funkcja dodająca 4 cSegmenty do listy, z której powstaje prostokąt
      //xPoint - współrzędne punktu bazowego
      //xWidth - szerokość prostokąta
      //xHeight - wysokość prostokąta
      
      if (pRectangleSegmentList.Capacity == 0) {
        AddSegment(0, 0, xPoint, 1);                        // (xOffsetX, xOffsetY, xPoint, xNumberOfSegment)
        AddSegment(xWidth, 0, xPoint, 2);
        AddSegment(xWidth, -xHeight, xPoint, 3);
        AddSegment(0, -xHeight, xPoint, 4);
        GetNewSegmentList(pRectangleSegmentList);            //pobieramy liste cSegment - rectanglePointList
        ShowNewSegmentList(pRectangleSegmentList);           //wyświetlamy liste cSegment - rectanglePointList
      }
      else {          // to edit - do same as ^^
        pRectangleSegmentList[0].pPointOfSegment.x = xPoint.x;
        pRectangleSegmentList[0].pPointOfSegment.y = xPoint.y;

        pRectangleSegmentList[1].pPointOfSegment.x = xPoint.x + xWidth;
        pRectangleSegmentList[1].pPointOfSegment.y = xPoint.y;

        pRectangleSegmentList[2].pPointOfSegment.x = xPoint.x + xWidth;
        pRectangleSegmentList[2].pPointOfSegment.y = xPoint.y - xHeight;

        pRectangleSegmentList[3].pPointOfSegment.x = xPoint.x;
        pRectangleSegmentList[3].pPointOfSegment.y = xPoint.y - xHeight;
      }
    }

    public void AddSegment(int xOffsetX, int xOffsetY, cPoint xPoint, int xNumberOfSegment) {
      //funkcja dodająca cSegment, ustawia współrzędne punktu oraz numer
      //xOffsetX - przesunięcie względem osi X
      //xOffsetY - przesunięcie względem osi Y
      //xPoint - współrzędne punktu bazowego
      //xNumberOfSegment - numer cSegmentu

      PointF point = new PointF(xPoint.x + xOffsetX, xPoint.y + xOffsetY);
      cPoint pPointOfSegment = new cPoint(point);     
      cSegment pNewSegment = new cSegment(pPointOfSegment, xNumberOfSegment);                 //inicjacja cSegmentu
      pRectangleSegmentList.Insert(xNumberOfSegment - 1, pNewSegment);                             //dodanie nowego segmentu do listy
    }

    public void RemoveSegment(List<cSegment> xSegmentList, int xNumberOfSegment) {
      //funkcja usuwająca cSegment
      //xNumberOfSegment - numer cSegmentu

      xSegmentList.RemoveAt(xNumberOfSegment);                             //usunięcie segmentu z listy
      Console.WriteLine("Segment usunięty");
    }

    public void CreateRegularPolygon(cPoint xCircleCenter, int xRadius, double xAngle) {
      //funkcja dodająca poszczególne cSegmenty do listy, z której powstaje wielokąt foremny
      //xCircleCenter - Punkt - środek koła, na którym opisana jest figura
      //xRadius - promień koła w który jest wpisana figura
      //xAngle - kąt pomiędzy [punktem pPoint - środkiem koła xCircleCenter - punktem pPoint(z kolejnego segmentu)]

      int pNumberOfSegment = 0;                             //inicjacja numer segmentu
      pTotalSegment = 360 / Math.Abs(xAngle);
      Console.WriteLine(pTotalSegment + "   total!!!");

      if (pRegularPolygonCSegmentList.Capacity == 0) {   // EDIT!!!! błąd przy zmianie liczby boków

        //pętla tworząca kolejne cSegmenty, kończy się gdy zrobi 360 stopni, inkrementuje po kącie bazowym xAngle
        for (double pAngleIncremental = 0; pAngleIncremental <= 360 - Math.Abs(xAngle); pAngleIncremental += Math.Abs(xAngle))
        {

          double pAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * pNumberOfSegment;                     //zwiekszenie kąta przy każdym kolejnym cSegmencie
          double pAngleTotal = (pAngleTemp * (Math.PI)) / 180;                                        //kąta przedstawiony w radianach
          double pCosinusOfAngleTotal = Math.Cos(pAngleTotal);
          double pSinusOfAngleTotal = Math.Sin(pAngleTotal);
          pNumberOfSegment++;

          PointF point = new PointF(xCircleCenter.x + (int)(xRadius * pSinusOfAngleTotal),
                                   xCircleCenter.y + (int)(xRadius * pCosinusOfAngleTotal));
          cPoint pPoint = new cPoint(point);  //(xCircleCenter.x + (int)(xRadius * pSinusOfAngleTotal),
                                     //xCircleCenter.y + (int)(xRadius * pCosinusOfAngleTotal));        //inicjacja współrzędnych punktu cSegmentu
          cSegment pNewSegment = new cSegment(pPoint, pNumberOfSegment);
          pRegularPolygonCSegmentList.Insert(pNumberOfSegment - 1, pNewSegment);                        //dodawanie segmentu do listy


          Console.WriteLine(pAngleTemp + " ?????? " + xAngle);
          Console.WriteLine();

        }
      }
      else {
        pNumberOfSegment = 0;
        for (double pAngleIncremental = 0; pAngleIncremental <= 360 - Math.Abs(xAngle); pAngleIncremental += Math.Abs(xAngle))
        {
          Console.WriteLine(" 2222222222222 " );

          double pAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * pNumberOfSegment;                     //zwiekszenie kąta przy każdym kolejnym cSegmencie
          double pAngleTotal = (pAngleTemp * (Math.PI)) / 180;                                        //kąta przedstawiony w radianach
          double pCosinusOfAngleTotal = Math.Cos(pAngleTotal);
          double pSinusOfAngleTotal = Math.Sin(pAngleTotal);
         

          pRegularPolygonCSegmentList[pNumberOfSegment].pPointOfSegment.x = xCircleCenter.x + (int)(xRadius * pSinusOfAngleTotal);
          pRegularPolygonCSegmentList[pNumberOfSegment].pPointOfSegment.y = xCircleCenter.y + (int)(xRadius * pCosinusOfAngleTotal);
          pNumberOfSegment++;
        }
      }
      GetNewSegmentList(pRegularPolygonCSegmentList);       //pobieramy liste cSegment - pRegularPolygonPointList
      ShowNewSegmentList(pRegularPolygonCSegmentList);      //wyświetlamy liste cSegment - pRegularPolygonPointList
    }



    //
    // funkcje wykonujące operacje na listach
    //
    public static List<cSegment> GetNewSegmentList(List<cSegment> list) {
      //funkcja pobierająca listę wszystkich cSegmentów z wybranej listy

      return list;
    }

    public static List<cSegment> ShowNewSegmentList(List<cSegment> list) {
      //funkcja wyświetlająca wszystkie cSegmenty z wybranej listy

      foreach (var i in list) {
        Console.WriteLine("cSegment: " + (list.IndexOf(i) + 1) + "\n" +
          "( " + i.pPointOfSegment.x + " ; " + i.pPointOfSegment.y + " )\n" +
          "Numer: " + i.pNumberOfSegment + "\n");
      }
      return null;
    }
  }
}
