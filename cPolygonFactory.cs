using System;
using System.Collections.Generic;
using System.Drawing;

namespace DrawShape
{
  public class cPolygonFactory {
    //klasa tworząca Listy (punktów początkowych oraz ich numery), do rysowania figur geometrycznych.
    // 1.Prostokąt - CreateRectangle
    // 2.Wielokąt Foremny - CreateRegularPolygon

    private double mAngleTemp;                //wartość kąta pomiędzy punktem pierwszego segmentu - środkiem koła - punktem kolejnego segmentu
    private double mAngleTotal;               //kąt pAngleTemp przedstawiony w radianach
    private double mCosinusOfAngleTotal;       //cosunus kąta pAngleTemp
    private static List<cSegment> mRectangleSegmentList = new List<cSegment>();            //inicjacja listy cSegmentu - figura prostokąt
    private static List<cSegment> mRegularPolygonCSegmentList = new List<cSegment>();     //inicjacja listy cSegmentu - figura wielokąt formeny
    private int mNumberOfSegment;                 //numer danego segmentu
    private double mSinusOfAngleTotal;       //sinus kąta pAngleTemp
    private static double mTotalSegment;                     //liczba wszystkich boków figury

    internal static List<cSegment> RectangleSegmentList { get { return mRectangleSegmentList; } set { mRectangleSegmentList = value; } }
    internal static List<cSegment> RegularPolygonCSegmentList { get { return mRegularPolygonCSegmentList; } set { mRegularPolygonCSegmentList = value; } }




    public void CreateRectangle(cPoint xPoint, int xWidth, int xHeight) {
      //funkcja dodająca 4 cSegmenty do listy, z której powstaje prostokąt
      //xPoint - współrzędne punktu bazowego
      //xWidth - szerokość prostokąta
      //xHeight - wysokość prostokąta
      
      if (mRectangleSegmentList.Capacity == 0) {
        AddSegment(0, 0, xPoint, 1 , mRectangleSegmentList); 
        AddSegment(xWidth, 0, xPoint, 2, mRectangleSegmentList);
        AddSegment(xWidth, -xHeight, xPoint, 3, mRectangleSegmentList);
        AddSegment(0, -xHeight, xPoint, 4, mRectangleSegmentList);
        GetSegmentsList(mRectangleSegmentList);            
        ShowSegmentsList(mRectangleSegmentList);           
      }
      else {          // to edit - do same as ^^
        mRectangleSegmentList[0].PointOfSegment.X = xPoint.X;
        mRectangleSegmentList[0].PointOfSegment.Y = xPoint.Y;

        mRectangleSegmentList[1].PointOfSegment.X = xPoint.X + xWidth;
        mRectangleSegmentList[1].PointOfSegment.Y = xPoint.Y;

        mRectangleSegmentList[2].PointOfSegment.X = xPoint.X + xWidth;
        mRectangleSegmentList[2].PointOfSegment.Y = xPoint.Y - xHeight;

        mRectangleSegmentList[3].PointOfSegment.X = xPoint.X;
        mRectangleSegmentList[3].PointOfSegment.Y = xPoint.Y - xHeight;
      }
    }
    
    public void CreateRegularPolygon(cPoint xCircleCenter, int xRadius, double xAngle) {
      //funkcja dodająca poszczególne segmenty do listy, z której powstaje wielokąt foremny
      //xCircleCenter - środek koła, na którym wpisana jest figura
      //xRadius - promień koła w który jest wpisana figura
      //xAngle - kąt pomiędzy [punktem pPoint - środkiem koła xCircleCenter - punktem pPoint(z kolejnego segmentu)]

      mNumberOfSegment = 0;                             //inicjacja numer segmentu
      mTotalSegment = 360 / Math.Abs(xAngle);
            
      //CREATE - liczymy kąt do punktu początkowego, dodajemy kąt przy każdym kolejnym segmencie
      if (mRegularPolygonCSegmentList.Capacity == 0) {   
        for (double pAngleIncremental = 0; pAngleIncremental <= 360 - Math.Abs(xAngle); pAngleIncremental += Math.Abs(xAngle)) {
          mAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * mNumberOfSegment;                     
          mAngleTotal = (mAngleTemp * (Math.PI)) / 180;                                        
          mCosinusOfAngleTotal = Math.Cos(mAngleTotal);
          mSinusOfAngleTotal = Math.Sin(mAngleTotal);
          mNumberOfSegment++;
          AddSegment((int)(xRadius * mSinusOfAngleTotal), (int)(xRadius * mCosinusOfAngleTotal),
                     xCircleCenter, mNumberOfSegment, mRegularPolygonCSegmentList);
        }
      }
      else {
        mNumberOfSegment = 0;
        //DELETE - zmniejszamy liczbę boków
        if (mRegularPolygonCSegmentList.Count > (int)mTotalSegment) {
          for (int i = 0; i <= mRegularPolygonCSegmentList.Count - mTotalSegment; i++) {
            RemoveSegment(mRegularPolygonCSegmentList, mRegularPolygonCSegmentList.Count-1);
            Console.WriteLine("Segment: " + (mRegularPolygonCSegmentList.Count - 1) + " removed sucessfull ");
          }
        }
        //ADD - zwiększamy liczbę boków
        else if (mRegularPolygonCSegmentList.Count < (int)mTotalSegment) {
          for (int i = 0; i <= mTotalSegment - mRegularPolygonCSegmentList.Count; i++) {
            mAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * mNumberOfSegment;                     
            mAngleTotal = (mAngleTemp * (Math.PI)) / 180;                                       
            mCosinusOfAngleTotal = Math.Cos(mAngleTotal);
            mSinusOfAngleTotal = Math.Sin(mAngleTotal);
            mNumberOfSegment++;
            AddSegment((int)(xRadius * mSinusOfAngleTotal), (int)(xRadius * mCosinusOfAngleTotal),
                       xCircleCenter, mNumberOfSegment, mRegularPolygonCSegmentList);
            Console.WriteLine("New segment added");
          }
          ShowSegmentsList(mRegularPolygonCSegmentList);
        }
        //SET - liczba boków zostaje, robimy tylko REBUILD
        else {
          for (double pAngleIncremental = 0; pAngleIncremental <= 360 - Math.Abs(xAngle); pAngleIncremental += Math.Abs(xAngle))  {
            mAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * mNumberOfSegment;                     
            mAngleTotal = (mAngleTemp * (Math.PI)) / 180;                                       
            mCosinusOfAngleTotal = Math.Cos(mAngleTotal);
            mSinusOfAngleTotal = Math.Sin(mAngleTotal);

            mRegularPolygonCSegmentList[mNumberOfSegment].PointOfSegment.X = xCircleCenter.X + (int)(xRadius * mSinusOfAngleTotal);
            mRegularPolygonCSegmentList[mNumberOfSegment].PointOfSegment.Y = xCircleCenter.Y + (int)(xRadius * mCosinusOfAngleTotal);
            mNumberOfSegment++;
          }
        }
      }
      GetSegmentsList(mRegularPolygonCSegmentList);       
      ShowSegmentsList(mRegularPolygonCSegmentList);      
    }

    public void AddSegment(int xOffsetX, int xOffsetY, cPoint xPoint, int xNumberOfSegment, List<cSegment> xSegmentList) {
      //funkcja dodająca segment, ustawia współrzędne punktu oraz nadaje numer
      //xOffsetX - przesunięcie względem osi X
      //xOffsetY - przesunięcie względem osi Y
      //xPoint - współrzędne punktu bazowego
      //xNumberOfSegment - numer segmentu
      //xSegmentList - wybranie listy z segmentami 

      PointF pPoint = new PointF(xPoint.X + xOffsetX, xPoint.Y + xOffsetY);
      cPoint pPointOfSegment = new cPoint(pPoint);
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
    public static List<cSegment> GetSegmentsList(List<cSegment> xList) {
      //funkcja pobierająca listę wszystkich cSegmentów z wybranej listy
      //xList - nazwa pobranej listy

      return xList;
    }

    public static List<cSegment> ShowSegmentsList(List<cSegment> xList) {
      //funkcja wyświetlająca wszystkie cSegmenty z wybranej listy
      //xList - nazwa listy do wyświetlenia

      foreach (var i in xList) {
        Console.WriteLine("cSegment: " + (xList.IndexOf(i) + 1) + "\n" +
          "( " + i.PointOfSegment.X + " ; " + i.PointOfSegment.Y + " )\n" +
          "Numer: " + i.NumberOfSegment + "\n");
      }
      return null;
    }
  }
}
