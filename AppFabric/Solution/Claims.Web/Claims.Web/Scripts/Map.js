var map = null;
var points = [];
var shapes = [];
var center = null;

function LoadMap(latitude, longitude, onMapLoaded) 
{
    map = new VEMap('theMap');
    options = new VEMapOptions();
    options.EnableBirdseye = false;
    // Makes the control bar less obtrusize.
    map.SetDashboardSize(VEDashboardSize.Small);
    if (onMapLoaded != null)
        map.onLoadMap = onMapLoaded;
    if (latitude != null && longitude != null) {
        center = new VELatLong(latitude, longitude);
    }
    map.LoadMap(center, null, null, null, null, null, null, options);
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

function FindAddressOnMap(where) 
{
    var numberOfResults = 20;
    var setBestMapView = true;
    var showResults = true;
    map.Find("", where, null, null, null,
    numberOfResults, showResults, true, true,
    setBestMapView, callbackForLocation);
}

function callbackForLocation(layer, resultsArray, places,
hasMore, VEErrorMessage) 
{
    clearMap();
    if (places == null)
        return;
    //Make a pushpin for each place we find
    $.each(places, function (i, item) 
    {
        var description = "";
        if (item.Description !== undefined) 
        {
            description = item.Description;
        }
        var LL = new VELatLong(item.LatLong.Latitude, item.LatLong.Longitude);
        LoadPin(LL, item.Name, description);
    });

    //Make sure all pushpins are visible
    if (points.length > 1) {
        map.SetMapView(points);
    }

    //If we've found exactly one place, that's our address.
    if (points.length === 1) {
        $("#Latitude").val(points[0].Latitude);
        $("#Longitude").val(points[0].Longitude);
    }
}


function clearMap() 
{
    map.Clear();
    points = [];
    shapes = [];
}



function FindClaimsGivenLocation(where) {
    map.Find("", where, null, null, null, null, null, false,
       null, null, callbackUpdateMapClaims);
}

function callbackUpdateMapClaims(layer, resultsArray, places, hasMore, VEErrorMessage) {
    $("#claimList").empty();
    clearMap();
    var center = map.GetCenter();

    $.post("/Search/SearchByLocation", { latitude: center.Latitude,
        longitude: center.Longitude
    }, function (claims) {
        $.each(claims, function (i, claim) {

            var LL = new VELatLong(claim.Latitude, claim.Longitude, 0, null);


            // Add Pin to Map
            LoadPin(LL, '<a href="/Claims/Details/' + claim.ClaimId + '">'
                        + claim.Title + '</a>',
                        "<p>" + claim.Description + "</p>");

            //Add a claim to the <ul> claimList on the right
            $('#claimList').append($('<li/>')
                            .attr("class", "claimItem")
                            .append($('<a/>').attr("href",
                                      "/Claims/Details/" + claim.ClaimId)
                            .html(claim.Description)));
        });

        // Adjust zoom to display all the pins we just added.
        if (points.length > 1) {
            map.SetMapView(points);
        }

        // Display the event's pin-bubble on hover.
        $(".claimItem").each(function (i, claim) {
            $(claim).hover(
                function () { map.ShowInfoBox(shapes[i]); },
                function () { map.HideInfoBox(shapes[i]); }
            );
        });
    }, "json");
}
