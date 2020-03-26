using System;

namespace DrawShape {

  public class cDrawingAdapter {

    public cDrawingAdapter() { }


    public cDrawing GetDrawing(cProject xProject) {
      //funkcja zwracająca Drawing_Project
      //xProject - projekt wielokąta oryginalny

      cDrawing pDrawing;

      pDrawing = new cDrawing();

      foreach (cPolygon pPolygon in xProject.PolygonsEnv.Polygons.Values) {

        if (pPolygon.CntPF == PolygonFunctionalityEnum.Undefined)
          ProcessPolygon(pDrawing, pPolygon);

        else if (pPolygon.CntPF == PolygonFunctionalityEnum.FrameOutline) {
          ProcessPolygon(pDrawing, pPolygon);

          if (pPolygon.Assembly != null)
            ProcessAssembly(pDrawing, pPolygon.Assembly);

          if (pPolygon.AssemblyItem != null)
            ProcessPolygon(pDrawing, pPolygon.AssemblyItem.Polygon);

        } else if (pPolygon.CntPF == PolygonFunctionalityEnum.Mullion) {
          if (pPolygon.Assembly != null)
            ProcessAssembly(pDrawing, pPolygon.Assembly);

          if (pPolygon.AssemblyItem != null)
            ProcessPolygon(pDrawing, pPolygon.AssemblyItem.Polygon);

        } else if (pPolygon.CntPF == PolygonFunctionalityEnum.FrameVirtual) {
          if (pPolygon.Child != null)
            ProcessPolygon(pDrawing, pPolygon.Child);

          if (pPolygon.Assembly != null)
            ProcessAssembly(pDrawing, pPolygon.Assembly);

          if (pPolygon.AssemblyItem != null)
            ProcessPolygon(pDrawing, pPolygon.AssemblyItem.Polygon);
        }
      }
      
      return pDrawing;

    }

    private void ProcessPolygon(cDrawing xDrawing, cPolygon xPolygon) {
      //funkcja przetwarzająca wielokąt obrysu ramy
      //xDrawing - rysownik oryginalny
      //xPolygon - wielokąt do przetworzenia

      cDrawingItem pDrawingItem;
      cDrawingSegment pDrawingSegment;

      pDrawingItem = new cDrawingItem();

      foreach (cSegment pSegment in xPolygon.Segments.Values) {

        pDrawingSegment = new cDrawingSegment(pSegment);

        pDrawingItem.AddSegment(pDrawingSegment);
       
      }

      xDrawing.AddItem(pDrawingItem);

    }

    private void ProcessAssembly(cDrawing xDrawing, cAssembly xAssembly) {
      //funkcja przetwarzająca Assembly
      //xDrawing - rysownik oryginalny
      //xAssembly - Assembly do przetworzenia

      cPolygon pPolygon;

      foreach (cAssemblyItem pAssemblyItem in xAssembly.AssemblyItems.Values) {

        pPolygon = pAssemblyItem.Polygon;

        ProcessPolygon(xDrawing, pPolygon); 
        
      }

    }

  }

}


