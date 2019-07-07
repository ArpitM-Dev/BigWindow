<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="map.aspx.cs" Inherits="storeSocial.map" %>

<!DOCTYPE html>
<html>
<head>
  <title>gmaps.js &mdash; the easiest way to use Google Maps</title>
  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
  <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>
  <script type="text/javascript" src="http://hpneo.github.io/gmaps/gmaps.js"></script>
  <script type="text/javascript" src="http://hpneo.github.io/gmaps/prettify/prettify.js"></script>
  <link href='http://hpneo.github.io/gmaps/styles.css' rel='stylesheet' type='text/css' />
  <link href='http://hpneo.github.io/gmaps/prettify/prettify.css' rel='stylesheet' type='text/css' />
  <script type="text/javascript">
      var map;
      $(document).ready(function () {
          prettyPrint();
          map = new GMaps({
              div: '#map',
              lat: -12.043333,
              lng: -77.028333
          });
      });
  </script>
</head>
<body>
  <div style="width:400px; height:150px; border:15px solid #fff;">
    <div id="map" style="height:150px"></div>
  </div>
</body>
</html>