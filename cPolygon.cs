using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrawShape {

  public enum PolygonFunctionalityEnum {                    //numerator funkcjonalności wielokąta
    Undefined = 0,                                          //nieokreślony
    FrameOutline = 1,                                       //poligon posiada assembly
    FrameVirtual = 2,                                       //wirualny kształt ramy
    Mullion = 3,                                            //słupek
  }

  public class cPolygon {

    private cAssembly mAssembly;                            //Assembly wielokąta
    private cAssemblyItem mAssemblyItem;                    //AssemblyItem wielokąta
    private cPolygon mChild;                                //dziecko wielokąta
    private PolygonFunctionalityEnum mCntPF;                //typ funkcji
    private int mIndex;                                     //numer wielokąta
    private cPolygon mParent;                               //rodzic wielokąta
    private Dictionary<int, cSegment> mSegments;            //lista boków

    internal cAssembly Assembly { get { return mAssembly; } }
    internal cAssemblyItem AssemblyItem { get { return mAssemblyItem; } }
    internal cPolygon Child { get { return mChild; } set { mChild = value; } }
    internal PolygonFunctionalityEnum CntPF { get { return mCntPF; } set { mCntPF = value; } } 
    internal int Index { get { return mIndex; } set { mIndex = value; } }
    internal cPolygon Parent { get { return mParent; } set { mParent = value; } }
    internal Dictionary<int, cSegment> Segments { get { return mSegments; } set { mSegments = value; } }
    internal string SegmentsList { get { return GetSegmentsList(); } }

    public cPolygon() {

      mSegments = new Dictionary<int, cSegment>();
      mCntPF = PolygonFunctionalityEnum.Undefined;     
      
    }

    public cPolygon(int xIndex) {

      mSegments = new Dictionary<int, cSegment>();
      mIndex = xIndex;
      mCntPF = PolygonFunctionalityEnum.Undefined;

    }

    internal void AddSegment(cSegment xSegment) {
      //funkcja dodająca nowy segment do listy
      //xSegment - wybrany segment

      mSegments.Add(xSegment.Index, xSegment);

    }
       
    internal void CreateAssembly(int xWidth, cPolygon xPolygon) {
      //funkcja tworząca Assembly dla poligonu
      //xWidth - szerokość profilu
      //xPolygon - poligon do przeprowadzenia assembly

      mAssembly = new cAssembly();

      mAssembly.CreateMe(xWidth, xPolygon);

      xPolygon.mCntPF = PolygonFunctionalityEnum.FrameOutline;

    }

    internal void SetSegmentToCurve(int xIndex) {
      //funkcja zamieniająca bok w łuk
      //xIndex - numer obsługiwanego boku

      mSegments[xIndex].IsCurve = true;

    }

    public string GetSegmentsList() {
      //funkcja zwracająca listę segmentów

      string pStr = string.Empty;

      foreach (var i in mSegments) {
        pStr += $"cSegment: {i} Punkt: ( {i.Value.Point.X} ; {i.Value.Point.Y} Numer:  {i.Value.Index} Type: {i.Value.IsCurve} \n";
      }

      Console.WriteLine(pStr);

      return pStr;

    }

    public void ShowSegmentsList() {
      //funkcja wyświetlająca listę segmentów

      Console.WriteLine(GetSegmentsList());

    }

    internal cSegment GetSegmentByIndex(int xIndex) {
      //funkcja zwracająca segment
      //xIndex - numer segmentu

      int pSegmentIndex;
      int pCountMax;

      pCountMax = mSegments.Count;

      pSegmentIndex = xIndex;

      if (xIndex > pCountMax)
        pSegmentIndex = 1;

      return mSegments[pSegmentIndex];

    }

    internal void FillMeByObject(cPolygon xPolygon) {
      //funkcja wypełniająca podstawowe dane według wybranego boku
      //xPolygon - wielokąt bazowy

      cSegment pSegment;

      //składniki, których nie ustawiamy, ani nie kopiujemy - powinny być ustawione w innym miejscu - na zewnątrz
      //mIndex;
      //mAssembly = null;                                           //do rozbudowy
      //mParent;                                                    //rodzic wielokąta

      mCntPF = xPolygon.CntPF; 

      mSegments = new Dictionary<int, cSegment>();

      foreach (cSegment pSegment_Oryginal in xPolygon.Segments.Values) {

        pSegment = pSegment_Oryginal.Clone();
        pSegment.SetPolygon_Parent(this);
        
        mSegments.Add(pSegment.Index, pSegment);

      }

    }

    internal cPolygon Clone() {
      //funkcja zwracająca kopie boku (wypełnionionia tylko podstawowe pola)

      cPolygon pPolygon;

      pPolygon = new cPolygon();

      pPolygon.FillMeByObject(this);

      return pPolygon;

    }

    public Dictionary <int, cPolygon> Split_PolygonByWidth(int xWidth) {
      //funkcja zwracająca kolekcję podzielonych prostokątów
      //xWidth - szerokości względem której dzielimy prostokąt

      Dictionary<int, cPolygon> pCln;
      cPolygon pPolygon;

      pCln = new Dictionary<int, cPolygon> ();

      pPolygon = Clone();

      pPolygon.Index = mIndex;
      pPolygon.Segments[2].Point.X = xWidth;
      pPolygon.Segments[3].Point.X = xWidth;

      pCln.Add(1, pPolygon);


      pPolygon = Clone();

      pPolygon.Index = mIndex + 1;
      pPolygon.Segments[1].Point.X = xWidth;
      pPolygon.Segments[4].Point.X = xWidth;

      pCln.Add(2, pPolygon);

      return pCln;

    }

    internal void SetPolygonToMullion(cPolygon xPolygon, int xMullionPosition_X, int xMullionWidth, float xWidth_Profile) {
      //funkcja ustawiająca poszczególne parametry na parametry typowe dla Polygon_Mullion
      //xPolygon - Polygon bazowy
      //xMullionPosition_X - współrzędna X osi słupka
      //xMullionWidth - szerokość słupka
      //xWidth_Profile - szerokość profilu

      cAssemblyItem pAssemblyItem;

      mCntPF = PolygonFunctionalityEnum.Mullion;
      mParent = xPolygon;

      //ustawiamy boki względem pozycji słupka + przesunięte o połowę jego szerokości
      mSegments[1].Point.X = xMullionPosition_X - xMullionWidth / 2;
      mSegments[2].Point.X = xMullionPosition_X + xMullionWidth / 2;
      mSegments[3].Point.X = xMullionPosition_X + xMullionWidth / 2;
      mSegments[4].Point.X = xMullionPosition_X - xMullionWidth / 2;

      pAssemblyItem = new cAssemblyItem(mIndex);
      pAssemblyItem.CreateAssemblyItem_Mullion(this, xWidth_Profile);

      mAssemblyItem = pAssemblyItem;
    
    }

  }

}
