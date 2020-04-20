using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape {

  public class cDrawingFilter {

    private bool mDisplayAssembly;                          //informacja o wyświetlaniu konstrukcji
    private bool mDisplayMullion;                           //informacja o wyświetlaniu słupków
    private bool mDisplaySash;                              //informacja o wyświetlaniu skrzydeł


    internal bool DisplayAssembly { get { return mDisplayAssembly; } set { mDisplayAssembly = value; } }
    internal bool DisplayMullion { get { return mDisplayMullion; } set { mDisplayMullion = value; } }
    internal bool DisplaySash { get { return mDisplaySash; } set { mDisplaySash = value; } }

    public cDrawingFilter() {

      mDisplayAssembly = true;
      mDisplayMullion = true;
      mDisplaySash = true;

    }

    internal void SetFilter(bool xDisplayAssembly, bool xDisplaySash, bool xDisplayMullion) {
      //funkcja zmieniająca informację o wyświetlanych elementach projektu 
      //xDisplayAssembly - wyświetlanie konstrukcji
      //xDisplayMullion - wyświetlanie słupków
      //xDisplaySash - wyświetlanie skrzydeł

      string pStr;

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
