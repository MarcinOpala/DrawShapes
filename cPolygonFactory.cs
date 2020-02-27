using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DrawShape {
  public class cPolygonFactory {
    //klasa tworząca Listy (punktów początkowych oraz ich numery), do rysowania figur geometrycznych.
    // 1.Prostokąt - CreateRectangle
    // 2.Wielokąt Foremny - CreateRegularPolygon

    public static List<cSegment> pRectangleSegmentList = new List<cSegment>();            //inicjacja listy cSegmentu - figura prostokąt
    public static List<cSegment> pRegularPolygonCSegmentList = new List<cSegment>();      //inicjacja listy cSegmentu - figura wielokąt formeny
    public static double pTotalSegment;                     //liczba wszystkich boków figury
    public double pAngleTemp = new double();                //wartość kąta pomiędzy punktem pierwszego segmentu - środkiem koła - punktem kolejnego segmentu
    public double pAngleTotal = new double();               //kąt pAngleTemp przedstawiony w radianach
    public double pCosinusOfAngleTotal = new double();      //cosunus kąta pAngleTemp
    public double pSinusOfAngleTotal = new double();        //sinus kąta pAngleTemp
    public int pNumberOfSegment = new int();                //numer danego segmentu

    public void CreateRectangle(cPoint xPoint, int xWidth, int xHeight) {
      //funkcja dodająca 4 cSegmenty do listy, z której powstaje prostokąt
      //xPoint - współrzędne punktu bazowego
      //xWidth - szerokość prostokąta
      //xHeight - wysokość prostokąta
      
      if (pRectangleSegmentList.Capacity == 0) {
        AddSegment(0, 0, xPoint, 1 , pRectangleSegmentList); 
        AddSegment(xWidth, 0, xPoint, 2, pRectangleSegmentList);
        AddSegment(xWidth, -xHeight, xPoint, 3, pRectangleSegmentList);
        AddSegment(0, -xHeight, xPoint, 4, pRectangleSegmentList);
        GetNewSegmentList(pRectangleSegmentList);            
        ShowNewSegmentList(pRectangleSegmentList);           
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
    
    public void CreateRegularPolygon(cPoint xCircleCenter, int xRadius, double xAngle) {
      //funkcja dodająca poszczególne segmenty do listy, z której powstaje wielokąt foremny
      //xCircleCenter - środek koła, na którym wpisana jest figura
      //xRadius - promień koła w który jest wpisana figura
      //xAngle - kąt pomiędzy [punktem pPoint - środkiem koła xCircleCenter - punktem pPoint(z kolejnego segmentu)]

      pNumberOfSegment = 0;                             //inicjacja numer segmentu
      pTotalSegment = 360 / Math.Abs(xAngle);
            
      //CREATE - liczymy kąt do punktu początkowego, dodajemy kąt przy każdym kolejnym segmencie
      if (pRegularPolygonCSegmentList.Capacity == 0) {   
        for (double pAngleIncremental = 0; pAngleIncremental <= 360 - Math.Abs(xAngle); pAngleIncremental += Math.Abs(xAngle)) {
          pAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * pNumberOfSegment;                     
          pAngleTotal = (pAngleTemp * (Math.PI)) / 180;                                        
          pCosinusOfAngleTotal = Math.Cos(pAngleTotal);
          pSinusOfAngleTotal = Math.Sin(pAngleTotal);
          pNumberOfSegment++;
          AddSegment((int)(xRadius * pSinusOfAngleTotal), (int)(xRadius * pCosinusOfAngleTotal),
                     xCircleCenter, pNumberOfSegment, pRegularPolygonCSegmentList);
        }
      }
      
      else {
        pNumberOfSegment = 0;
        //DELETE - zmniejszamy liczbę boków
        if (pRegularPolygonCSegmentList.Count > (int)pTotalSegment) {
          for (int i = 0; i <= pRegularPolygonCSegmentList.Count - pTotalSegment; i++) {
            RemoveSegment(pRegularPolygonCSegmentList, pRegularPolygonCSegmentList.Count-1);
            Console.WriteLine("Segment: " + (pRegularPolygonCSegmentList.Count - 1) + " removed sucessfull ");
          }
        }
        //ADD - zwiększamy liczbę boków
        else if (pRegularPolygonCSegmentList.Count < (int)pTotalSegment) {
          for (int i = 0; i <= pTotalSegment - pRegularPolygonCSegmentList.Count; i++) {
            pAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * pNumberOfSegment;                     
            pAngleTotal = (pAngleTemp * (Math.PI)) / 180;                                       
            pCosinusOfAngleTotal = Math.Cos(pAngleTotal);
            pSinusOfAngleTotal = Math.Sin(pAngleTotal);
            pNumberOfSegment++;
            AddSegment((int)(xRadius * pSinusOfAngleTotal), (int)(xRadius * pCosinusOfAngleTotal),
                       xCircleCenter, pNumberOfSegment, pRegularPolygonCSegmentList);
            Console.WriteLine("New segment added");
          }
          ShowNewSegmentList(pRegularPolygonCSegmentList);
        }
        //SET - liczba boków zostaje, robimy tylko RESIZE
        else {
          for (double pAngleIncremental = 0; pAngleIncremental <= 360 - Math.Abs(xAngle); pAngleIncremental += Math.Abs(xAngle))  {
            pAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * pNumberOfSegment;                     
            pAngleTotal = (pAngleTemp * (Math.PI)) / 180;                                       
            pCosinusOfAngleTotal = Math.Cos(pAngleTotal);
            pSinusOfAngleTotal = Math.Sin(pAngleTotal);

            pRegularPolygonCSegmentList[pNumberOfSegment].pPointOfSegment.x = xCircleCenter.x + (int)(xRadius * pSinusOfAngleTotal);
            pRegularPolygonCSegmentList[pNumberOfSegment].pPointOfSegment.y = xCircleCenter.y + (int)(xRadius * pCosinusOfAngleTotal);
            pNumberOfSegment++;
          }
        }
      }
      GetNewSegmentList(pRegularPolygonCSegmentList);       
      ShowNewSegmentList(pRegularPolygonCSegmentList);      
    }

    public void AddSegment(int xOffsetX, int xOffsetY, cPoint xPoint, int xNumberOfSegment, List<cSegment> xSegmentList) {
      //funkcja dodająca segment, ustawia współrzędne punktu oraz nadaje numer
      //xOffsetX - przesunięcie względem osi X
      //xOffsetY - przesunięcie względem osi Y
      //xPoint - współrzędne punktu bazowego
      //xNumberOfSegment - numer segmentu
      //xSegmentList - wybranie listy z segmentami 

      PointF point = new PointF(xPoint.x + xOffsetX, xPoint.y + xOffsetY);
      cPoint pPointOfSegment = new cPoint(point);
      cSegment pNewSegment = new cSegment(pPointOfSegment, xNumberOfSegment);                
      xSegmentList.Insert(xNumberOfSegment - 1, pNewSegment);                             
    }

    public void RemoveSegment(List<cSegment> xSegmentList, int xNumberOfSegment) {
      //funkcja usuwająca segment
      //xSegmentList - wybranie listy z segmentami
      //xNumberOfSegment - numer segmentu

      xSegmentList.RemoveAt(xNumberOfSegment);                             //usunięcie segmentu z listy
      Console.WriteLine("Segment usunięty");
    }

    //
    // funkcje pomocnicze wykonujące operacje na listach
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
