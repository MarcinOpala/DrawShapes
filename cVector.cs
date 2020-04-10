using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace DrawShape {

  public class cVector {

    private cPoint mPt_First;                               //punkt pierwszy
    private cPoint mPt_Next;                                //punkt drugi
    private Vector mVector;                                 //vektor
    private double mX;                                      //współrzędne wektora X
    private double mY;                                      //współrzędne wektora Y
    private static double mCosinusAlfa;                     //cosinus pomiędzy wektorami

    internal cPoint Pt_First { get { return mPt_First; } set { mPt_First = value; } }
    internal cPoint Pt_Next { get { return mPt_Next; } set { mPt_Next = value; } }
    internal Vector Vector { get { return mVector; } set { mVector = value; } }
    internal double X { get { return mX; } set { mX = value; } }
    internal double Y { get { return mY; } set { mY = value; } }
    internal static double CosinusAlfa { get { return mCosinusAlfa; } set { mCosinusAlfa = value; } }
       
    public cVector(cPoint xPt_First, cPoint xPt_Next) {
      //konstruktor tworzący vektor z dwóch punktów
      //xPt_First - pierwszy punkt
      //xPt_Next - drugi punkt

      Vector pVector;
      double pVector_X;
      double pVector_Y;

      pVector_X = xPt_Next.X - xPt_First.X;
      pVector_Y = xPt_Next.Y - xPt_First.Y;

      pVector = new Vector(pVector_X, pVector_Y);

      mVector =  pVector;

    }

    public cVector(double xVector_X, double xVector_Y) {
      //konstruktor tworzący vektor z dwóch punktów
      //xVector_X - współrzędne x
      //xVector_Y - współrzędne y

      Vector pVector;
      
      mX = xVector_X;
      mY = xVector_Y;

      pVector = new Vector(xVector_X, xVector_Y);

      mVector = pVector;

    }
    
    public cVector(cSegment xSeg_First, cSegment xSeg_Next) {
      //konstruktor tworzący wektor z dwóch boków
      //xSeg_First - pierwszy bok
      //xSeg_Next - drugi bok

      Vector pVector;
      double pVector_X;
      double pVector_Y;

      pVector_X = xSeg_Next.Point.X - xSeg_First.Point.X;
      pVector_Y = xSeg_Next.Point.Y - xSeg_First.Point.Y;

      pVector = new Vector(pVector_X, pVector_Y);

      mVector = pVector;

    }

    internal cVector AddVectors(cVector xVector_First,  cVector xVector_Secound) {
      //funkcja dodająca wektory
      //xVector_First - pierwszy wektor
      //xVector_Secound - drugi wektor

      cVector pVector;

      pVector = new cVector(xVector_First.X + xVector_Secound.X, xVector_First.Y + xVector_Secound.Y);

      return pVector;

    }

    internal cVector SubtractVectors(cVector xVector_First, cVector xVector_Secound) {
      //funkjca odejmująca wektory
      //xVector_First - pierwszy wektor
      //xVector_Secound - drugi wektor

      cVector pVector;

      pVector = new cVector(xVector_First.X - xVector_Secound.X, xVector_First.Y - xVector_Secound.Y);

      return pVector;

    }

    internal cVector SetDirection(cVector xVector) {
      //funkjca zmieniająca kierunek wektora
      //xVector - wektor do zmiany

      xVector.X = - xVector.X;
      xVector.Y = - xVector.Y;

      return xVector;

    }

    internal cVector Multiplication(cVector xVector, double xNumber) {
      //funkcja mnożąca wektor przez liczbę
      //xVector - wektor podstawowy
      //xNumber - wartość skakali


      xVector.X = xVector.X * xNumber;
      xVector.Y = xVector.Y * xNumber;

      return xVector;

    }

    internal static double Scalar(cVector xVector_First, cVector xVector_Secound) {
      //funkcja zwracająca wektor będący wynikiem iloczynu skalarnego
      //xVector_First - pierwszy wektor
      //xVector_Secound - drugi wektor

      double pScalar;
      
      pScalar = (xVector_First.mVector.X * xVector_Secound.mVector.X + xVector_First.mVector.Y * xVector_Secound.mVector.Y);

      return pScalar;

    }

    internal double CosAlfa(cVector xVector_First, cVector xVector_Secound) {
      //funkcja zwracająca wektor będący wynikiem iloczynu skalarnego
      //UWAGA: przy obliczaniu kąta Alfa pamiętać o cykliczności Cosinusa
      //xVector_First - pierwszy wektor
      //xVector_Secound - drugi wektor

      double pCos;
      double pFirst_Lenght;
      double pSecound_Lenght;

      pFirst_Lenght = Math.Sqrt(xVector_First.mVector.X * xVector_First.mVector.X + xVector_First.mVector.Y * xVector_First.mVector.Y);
      pSecound_Lenght = Math.Sqrt(xVector_Secound.mVector.X * xVector_Secound.mVector.X + xVector_Secound.mVector.Y * xVector_Secound.mVector.Y);

      pCos = Scalar(xVector_First, xVector_Secound) / 
                  (pFirst_Lenght * pSecound_Lenght);
       
      mCosinusAlfa = pCos;
                  
      return pCos;

    }

    internal double Get_VectorLength() {
      //funkcja zwracająca długość wektora

      double pLength;

      pLength = Math.Sqrt((mX * mX) + (mY * mY));

      return pLength;

    }




  }
  
}
