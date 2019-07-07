<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feed.aspx.cs" Inherits="storeSocial.feed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal runat="server" ID="ltUserErr"></asp:Literal>
        <div class="col-xs-12 clearfix pt10 noPadding infinite-container"><div class="infinite-item">
            <asp:Literal runat="server" ID="ltFeed"></asp:Literal>            
        </div></div>
        <a id="lbFeedLink" runat="server" class="infinite-more-link">Load More</a>
        <asp:Literal runat="server" ID="ltSysErr"></asp:Literal>
        <script>$(document).ready(function () {$.ajaxSetup({ cache: false });});</script>
    </form>
</body>
</html>
