using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShape {

  public enum PolygonFunctionalityEnum {                                       //numerator funkcjonalności wielokąta
    Undefined = 0,
    FrameOutline = 1,  
  }

  public class cPolygon {

    private cAssembly mAssembly;
    private PolygonFunctionalityEnum mCntPF;
    private int mIndex;
    private Dictionary<int, cSegment> mSegments;

    internal cAssembly Assembly { get { return mAssembly; } }
    internal PolygonFunctionalityEnum CntPF { get { return mCntPF; } set { mCntPF = value; } } 
    internal int Index { get { return mIndex; } set { mIndex = value; } }
    internal Dictionary<int, cSegment> Segments { get { return mSegments; } set { mSegments = value; } }
    internal string SegmentsList { get { return GetSegmentsList(); } }


    public cPolygon() {

      mSegments = new Dictionary<int, cSegment>();
      //mAssembly = new cAssembly();
      mCntPF = PolygonFunctionalityEnum.Undefined;     
      
    }

    public cPolygon(int xIndex) {

      mSegments = new Dictionary<int, cSegment>();
     // mAssembly = new cAssembly();
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

  }

}
