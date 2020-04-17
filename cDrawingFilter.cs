using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cDrawingFilter {

    private bool mDisplayAssembly;
    private bool mDisplaySash;
    private bool mDisplayMullion;

    internal bool ChBoxAssembly { get { return mDisplayAssembly; } set { mDisplayAssembly = value; } }
    internal bool ChBoxSash { get { return mDisplaySash; } set { mDisplaySash = value; } }
    internal bool ChBoxMullion { get { return mDisplayMullion; } set { mDisplayMullion = value; } }

    public cDrawingFilter() {

      mDisplayAssembly = true;
      mDisplaySash = true;
      mDisplayMullion = true;

  }

    internal void SetFilter( bool xDisplayAssembly, bool xDisplaySash, bool xDisplayMullion) {

      string pStr;
 /*     MainForm main;

      main = new MainForm();*/

      mDisplayAssembly = xDisplayAssembly;
      mDisplaySash = xDisplaySash;
      mDisplayMullion = xDisplayMullion;

      pStr = $" mDisplayAssembly: {mDisplayAssembly},\n" +
             $" mDisplaySash: {mDisplaySash},\n" +
             $" mDisplayMullion: {mDisplayMullion},\n";

      Console.WriteLine(pStr);

    }




  }
}
