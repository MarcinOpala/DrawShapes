using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cTechElement {

    private cElement mElement;
    private string mNe;

    internal string Ne { get { return mNe; } set { mNe = value; } }
    internal cElement Element { get { return mElement; } set { mElement = value; } }


    public cTechElement() {

    }

    public cTechElement(string xString) {

      mElement = new cElement(xString);
      mNe = "+ " + xString;

    }

    internal cTechElement (cPolygon xPolygon_Parent) {

      string pStr;

      pStr = "";

      switch (xPolygon_Parent.CntPF) {
        case PolygonFunctionalityEnum.Sash:
          pStr = "Skrzydło 404";
          break;
        case PolygonFunctionalityEnum.FrameVirtual:
          if (xPolygon_Parent.Child == null) break;
          pStr = "Skrzydło 404";
          break;
        case PolygonFunctionalityEnum.FrameOutline:
          pStr = "Konstrukcja 505";
          break;
        case PolygonFunctionalityEnum.Mullion:
          pStr = "Słupek 303";
          break;

      }

      mElement = new cElement(pStr);
      mNe = "+ " + pStr;
     

      

    }

  }
}
