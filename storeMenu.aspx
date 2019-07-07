<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="storeMenu.aspx.cs" Inherits="storeSocial.storeMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="col-xs-12 " style="padding:15px;">
        <h3 class="noPadding noMargin">Store Menu</h3>
        <asp:Literal runat="server" ID="ltMenuImage"></asp:Literal>
        <div class="col-xs-12 pb20"></div>
        <h3 class="noPadding noMargin"><asp:Literal runat="server" ID="ltMenuLink"></asp:Literal></h3>
        
    </div>
    </form>
</body>
</html>
