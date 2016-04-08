## Sample Table Of Contents
## Maps


- **MapView**

    * [Show magnifier](Xamarin.iOS/Samples/MapView/ShowMagnifier)

    This sample demonstrates how you can tap and hold on a map to get the magnifier. You can also pan while tapping and holding to move the magnifier across the map.

    * [Change viewpoint](Xamarin.iOS/Samples/MapView/ChangeViewpoint)

    This sample demonstrates different ways in which you can change the viewpoint or visible area of the map.


- **Map**

    * [Display a map](Xamarin.iOS/Samples/Map/DisplayMap)

    This samples demonstrates how to display a map with a basemap

    * [Open an existing map](Xamarin.iOS/Samples/Map/OpenExistingMap)

    This sample demonstrates how to open an existing map from a portal. The sample opens with a map displayed by default. You can change the shown map by selecting a new one from the populated list.

    * [Display drawing status](Xamarin.iOS/Samples/Map/DisplayDrawingStatus)

    This sample demonstrates how to use the DrawStatus value of the MapView to notify user that the MapView is drawing.

    * [Map rotation](Xamarin.iOS/Samples/Map/MapRotation)

    This sample illustrates how to rotate a map.

    * [Change basemap](Xamarin.iOS/Samples/Map/ChangeBasemap)

    This sample demonstrates how to dynamically change the basemap displayed in a Map.

    * [Set Min & Max Scale](Xamarin.iOS/Samples/Map/SetMinMaxScale)

    This sample demonstrates how to set the minimum and maximum scale of a Map. Setting the minimum and maximum scale for the Map can be useful in keeping the user focused at a certain level of detail.

    * [Set initial map location](Xamarin.iOS/Samples/Map/SetInitialMapLocation)

    This sample demonstrates how to create a map with a standard ESRI Imagery with Labels basemap that is centered on a latitude and longitude location and zoomed into a specific level of detail.

    * [Set initial map area](Xamarin.iOS/Samples/Map/SetInitialMapArea)

    This sample demonstrates how to set the initial viewpoint from envelope defined by minimum (x,y) and maximum (x,y) values. The map's InitialViewpoint is set to this viewpoint before the map is loaded into the MapView. Upon loading the map zoom to this initial area.

    * [Set map spatial reference](Xamarin.iOS/Samples/Map/SetMapSpatialReference)

    This sample demonstrates how you can set the spatial reference on a Map and all the operational layers would project accordingly.

    * [Access load status](Xamarin.iOS/Samples/Map/AccessLoadStatus)

    This sample demonstrates how to access the Maps' LoadStatus. The LoadStatus will be considered loaded when the following are true: The Map has a valid SpatialReference and the Map has an been set to the MapView.

## Layers


- **Tiled Layers**

    * [ArcGIS tiled layer (URL)](Xamarin.iOS/Samples/Layers/ArcGISTiledLayerUrl)

    This sample demonstrates how to add an ArcGISTiledLayer as a base layer in a map. The ArcGISTiledLayer comes from an ArcGIS Server sample web service.

    * [ArcGIS tiled layer (local TPK)](Xamarin.iOS/Samples/Layers/ArcGISTiledLayerLocal)

    Demonstrates loading an ArcGISTiledLayer from a local data source (on disk) using a .TPK file.


- **Map Image Layers**

    * [ArcGIS map image layer (URL)](Xamarin.iOS/Samples/Layers/ArcGISMapImageLayerUrl)

    This sample demonstrates how to add an ArcGISMapImageLayer as a base layer in a map. The ArcGISMapImageLayer comes from an ArcGIS Server sample web service.

    * [Change sublayer visibility](Xamarin.iOS/Samples/Layers/ChangeSublayerVisibility)

    This sample demonstrates how to show or hide sublayers of a map image layer.

## Features


- **Feature Layers**

    * [Feature layer (feature service)](Xamarin.iOS/Samples/Layers/FeatureLayerUrl)

    This sample demonstrates how to show a feature layer on a map using the URL to the service.


- **Feature Tables**

    * [Service feature table (cache)](Xamarin.iOS/Samples/Data/ServiceFeatureTableCache)

    This sample demonstrates how to use a feature service in on interaction cache mode.

    * [Service feature table (no cache)](Xamarin.iOS/Samples/Data/ServiceFeatureTableNoCache)

    This sample demonstrates how to use a feature service in on interaction no cache mode.

    * [Service feature table (manual cache)](Xamarin.iOS/Samples/Data/ServiceFeatureTableManualCache)

    This sample demonstrates how to use a feature service in manual cache mode.

## Display Information


- **Graphics Overlay**

    * [Add graphics (Renderer)](Xamarin.iOS/Samples/GraphicsOverlay/AddGraphicsRenderer)

    This sample demonstrates how you add graphics and set a renderer on a graphic overlays.


- **Symbology**

    * [Render simple markers](Xamarin.iOS/Samples/Symbology/RenderSimpleMarkers)

    This sample adds a point graphic to a graphics overlay symbolized with a red circle specified via a SimpleMarkerSymbol.

    * [Render picture markers](Xamarin.iOS/Samples/Symbology/RenderPictureMarkers)

    This sample demonstrates how to create picture marker symbols from a URL and embedded resources.

    * [Unique value renderer](Xamarin.iOS/Samples/Symbology/RenderUniqueValues)

    This sample demonstrate how to use a unique value renderer to style different features in a feature layer with different symbols. Features do not have a symbol property for you to set, renderers should be used to define the symbol for features in feature layers. The unique value renderer allows for separate symbols to be used for features that have specific attribute values in a defined field.



[](Esri Tags: ArcGIS Runtime SDK .NET WinRT WinStore WPF WinPhone C# C-Sharp DotNet XAML MVVM)
[](Esri Language: DotNet)
