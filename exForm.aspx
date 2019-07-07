<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exForm.aspx.cs" Inherits="storeSocial.test" %>

<!DOCTYPE html>
<html lang="en" class="no-js">
	<head>
		<meta charset="UTF-8" />
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"> 
		<meta name="viewport" content="width=device-width, initial-scale=1.0"> 
		<title>Expanding Search Bar Deconstructed</title>
		<link rel="stylesheet" type="text/css" href="css/style.css" />
		<link rel="stylesheet" type="text/css" href="css/default.css" />
		<link rel="stylesheet" type="text/css" href="css/component.css" />
        <style>
        *, *:after, *:before { -webkit-box-sizing: border-box; -moz-box-sizing: border-box; box-sizing: border-box; }
        </style>
	</head>
	<body style="padding:0; margin:0;">

    <div style="width:320px; float:left;"><div style="position:absolute; width:260px; padding:9px 10px 0 0; font-size:16px; text-align:right;" class="blueFont">Search by Store Name</div>
					<div id="sb-search" class="sb-search" >
						<form method="post" onsubmit="return false;">
							<input class="sb-search-input" type="text" value="" name="search" id="search">
							<input class="sb-search-submit" type="submit" value="">
							<span class="sb-icon-search"></span>
						</form>
					</div>
					</div>
		<script src="js/modernizr.custom.js"></script>			
		<script src="js/classie.js"></script>
		<script src="js/uisearch.js"></script>
		<script>
		    new UISearch(document.getElementById('sb-search'));
		</script>

</body>
</html>
