<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AndriodDeviceLocation.aspx.cs" Inherits="DeviceTracker.AndriodDeviceLocation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
<script src="https://kit.fontawesome.com/a076d05399.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    
    <script type="text/javascript">
    var markers = [
    <asp:Repeater ID="rptMarkers" runat="server">
    <ItemTemplate>
             {
                "lat": '<%# Eval("LATITUDE") %>',
                "lng": '<%# Eval("LONGITUDE") %>',
                "description": '<%# Eval("CREATED_AT") %>'
            }
    </ItemTemplate>
    <SeparatorTemplate>
        ,
    </SeparatorTemplate>
    </asp:Repeater>
    ];
    </script>
      
    <script type="text/javascript">
        window.onload = function () {        
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var infoWindow = new google.maps.InfoWindow();
            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            markers = markers.reverse();
            for (i = 0; i < markers.length; i++) {
                var data = markers[i]
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    icon: i == markers.length-1 ? {url: "http://maps.google.com/mapfiles/ms/icons/red-dot.png"} : {url: "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png"}
                });
                (function (marker, data) {
                    google.maps.event.addListener(marker, "click", function (e) {
                        infoWindow.setContent(data.description);
                        infoWindow.open(map, marker);
                    });
                })(marker, data);
            }
        }
    </script>
    <div id="dvMap" style="width: auto; height: 640px">
    </div>

     <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAk5vbxf6_zKW7ZqYmKPCueUF8YWKUOXLU&callback=initMap"
    async defer></script>

    </form>
     
</body>
</html>