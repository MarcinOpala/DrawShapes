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

    public cTechElement(string xString) {

      mElement = new cElement();
      mNe = xString;


    }
  }
}
