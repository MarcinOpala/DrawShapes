using System;

namespace DrawShape {

  public class cDrawingAdapter {

    public cDrawingAdapter() { }

    public cDrawing GetDrawing(cProject xProject, cDrawingFilter xDrawingFilter) {
      //funkcja zwracająca Drawing_Project
      //xProject - projekt wielokąta oryginalny
      //xDrawingFilter - filtr elementów wyświetlanych

      cDrawing pDrawing;

      pDrawing = new cDrawing();
      //wybranie wielokątów do przetwarzanie według filtra i ich typów
      foreach (cPolygon pPolygon in xProject.PolygonsEnv.Polygons.Values) {
        switch (pPolygon.CntPF) {
          case PolygonFunctionalityEnum.FrameVirtual:       //wielokąt wirtualny
            continue;
          case PolygonFunctionalityEnum.Undefined:          //obrys
            ProcessPolygon(pDrawing, pPolygon);
            break;
          case PolygonFunctionalityEnum.FrameOutline:       //konstrukcja
            ProcessPolygon(pDrawing, pPolygon);
            if (pPolygon.Assembly == null) continue;
            if (!xDrawingFilter.DisplayAssembly) continue;  //sprawdzenie warunków filtra rysowania
            ProcessAssembly(pDrawing, pPolygon.Assembly);
            break;
          case PolygonFunctionalityEnum.Mullion:            //słupek
            if (!xDrawingFilter.DisplayMullion) continue;   //sprawdzenie warunków filtra rysowania
            ProcessPolygon(pDrawing, pPolygon.AssemblyItem.Polygon);
            break;

        }
      }
      
      //skrzydła sprawdzamy oddcielnie, aby były na wierzchu rysunku
      foreach (cPolygon pPolygon in xProject.PolygonsEnv.Polygons.Values) {   
        switch (pPolygon.CntPF) {
          case PolygonFunctionalityEnum.FrameVirtual:
            if (!xDrawingFilter.DisplaySash) continue;      //sprawdzenie warunków filtra rysowania
            if (pPolygon.Child == null) continue;
            ProcessPolygon(pDrawing, pPolygon.Child);       //obrys skrzydła
            if (pPolygon.Child.Assembly != null)
              ProcessAssembly(pDrawing, pPolygon.Child.Assembly); //elementy konstrukcji skrzydła
            break;

        }
      }
      return pDrawing;

    }

    private void ProcessPolygon(cDrawing xDrawing, cPolygon xPolygon) {
      //funkcja przetwarzająca wielokąt
      //xDrawing - rysownik oryginalny
      //xPolygon - wielokąt do przetworzenia

      cDrawingItem pDrawingItem;
      cDrawingSegment pDrawingSegment;

      pDrawingItem = new cDrawingItem();

      if (xPolygon.CntPF == PolygonFunctionalityEnum.Profile ||     //jeżeli wielokąt jest profilem/słupkiem to wypełniamy wnętrze
          xPolygon.CntPF == PolygonFunctionalityEnum.Mullion)
        pDrawingItem.CntDIF = DrawingItemFillingEnum.IsFilled;

      foreach (cSegment pSegment in xPolygon.Segments.Values) {
        pDrawingSegment = new cDrawingSegment(pSegment);
        pDrawingItem.AddSegment(pDrawingSegment);
       
      }

      xDrawing.AddItem(pDrawingItem);

    }

    private void ProcessAssembly(cDrawing xDrawing, cAssembly xAssembly) {
      //funkcja przetwarzająca konstrukcję
      //xDrawing - rysownik oryginalny
      //xAssembly - konstrukcja do przetworzenia

      cPolygon pPolygon;

      foreach (cAssemblyItem pAssemblyItem in xAssembly.AssemblyItems.Values) {
        pPolygon = pAssemblyItem.Polygon;

        ProcessPolygon(xDrawing, pPolygon); 
        
      }

    }

  }

}


