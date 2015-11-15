<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BusinessEntities.Accident>" %>

<script src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>

<div id="myMap" style="position:relative; width:520px; height:400px;">
</div>

<script type="text/javascript">

    //var latitude = <%=Model.Latitude %>;
    //var longitude = <%=Model.Longitude %>;
    var center = null;
    var points = [];
    var shapes = [];

    try {
        var map = new VEMap('myMap');

        //map.AttachEvent("oncredentialserror", HandleCredentialsError);
        //map.AttachEvent("oncredentialsvalid", LoadMyMap); 

        //map.SetCredentials("AhumQNw9jvEi_Tpd3FXUYt6Pl7rJONTKENGkRDVcIuD-BHtxz0CAzr_MTFqb145O");
        map.LoadMap(new VELatLong(0, 0), 14, 'h', false);
        LoadMyMap();
    } catch (e) {

    }
    

       
    function FindAddressOnMap(where) {
        var numberOfResults = 20;
        var setBestMapView = true;
        var showResults = true;
        map.Find("", where, null, null, null,
        numberOfResults, showResults, true, true,
        setBestMapView, callbackForLocation);
    }       
    
    function LoadPin(LL, name, description) 
    {
        var shape = new VEShape(VEShapeType.Pushpin, LL);
        //Make a nice Pushpin shape with a title and description
        shape.SetTitle("<span class=\"pinTitle\"> " + escape(name) + "</span>");

        if (description !== undefined) {
            shape.SetDescription("<p class=\"pinDetails\">" +
            escape(description) + "</p>");
        }
        map.AddShape(shape);
        points.push(LL);
        shapes.push(shape);
    }    
    
    
    function callbackForLocation(layer, resultsArray, places, hasMore, VEErrorMessage) 
    {
        clearMap();
        if (places == null)
            return;
            
        //Make a pushpin for each place we find
        var description = "";
        if (places[0].Description !== undefined) 
        {
            description = places[0].Description;
        }
        var LL = new VELatLong(places[0].LatLong.Latitude, places[0].LatLong.Longitude);
        LoadPin(LL, places[0].Name, description);

        //Make sure all pushpins are visible
        if (points.length > 1) {
            map.SetMapView(points);
        }
        
    }


    function clearMap() 
    {
        map.Clear();
        points = [];
        shapes = [];
    }
    
    function HandleCredentialsError()
    {
        alert("The Bing Maps credentials are invalid.");
    }

    function LoadMyMap()
    {
        // Makes the control bar less obtrusize.
        map.SetDashboardSize(VEDashboardSize.Small);
//        if ((latitude == 0) || (longitude == 0))
//            map.LoadMap();
//        else
//        {
//            center = new VELatLong(latitude, longitude);
//            map.LoadMap(new VELatLong(latitude, longitude), 14, 'h', false);
//        }

        center = new VELatLong(0, 0);
        var title = "Map of the car accident";   
        var address = "<%= Html.Encode(Model.Address) %>,  " + 
        "<%= Html.Encode(Model.City) %>,  " +
        "<%= Html.Encode(Model.State.ToUpper()) %>, " +
        "<%= Html.Encode(Model.Zip) %> ";

        FindAddressOnMap(address);
        LoadPin(center, title, address);
    }

    
    
</script>

