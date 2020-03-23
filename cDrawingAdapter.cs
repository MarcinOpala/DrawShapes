using System;

namespace DrawShape {

  public class cDrawingAdapter {

    public cDrawingAdapter() { }


    public cDrawing GetDrawing(cProject xProject) {
      //funkcja zwracająca Drawing_Project
      //xProject - projekt wielokąta oryginalny

      cDrawing pDrawing;
      cPolygon pPolygon;

      pPolygon = xProject.PolygonsEnv.Polygons[1];          //wielokąt obrysu ramy
      
      pDrawing = new cDrawing();

      /*      if (pPolygon.Assembly == null) {

              ProcessPolygon(pDrawing, pPolygon);

            } else {

              ProcessAssmbly(pDrawing, pPolygon.Assembly);

            }*/

      ProcessPolygon(pDrawing, pPolygon);

      if (pPolygon.Assembly == null)
        return pDrawing;




      ProcessAssembly(pDrawing, pPolygon.Assembly);

    


      return pDrawing;

    }

    private void ProcessPolygon(cDrawing xDrawing, cPolygon xPolygon) {
      //funkcja przetwarzająca wielokąt obrysu ramy
      //xDrawing - 
      //xPolygon - 

      cDrawingItem pDrawingItem;
      cDrawingSegment pDrawingSegment;

      pDrawingItem = new cDrawingItem();

      foreach (cSegment pSegment in xPolygon.Segments.Values) {

        pDrawingSegment = new cDrawingSegment(pSegment);

        pDrawingItem.AddSegment(pDrawingSegment);
       
      }

      xDrawing.AddItem(pDrawingItem, xPolygon.Index);

    }

    private void ProcessAssembly(cDrawing xDrawing, cAssembly xAssembly) {
      //funkcja przetwarzająca Assembly
      //xDrawing - 
      //xAssembly - 

      cPolygon pPolygon;

      foreach (cAssemblyItem pAssemblyItem in xAssembly.AssemblyItems.Values) {

        pPolygon = pAssemblyItem.Polygon;

        ProcessPolygon(xDrawing, pPolygon); 
        
      }

    }

  }

}


