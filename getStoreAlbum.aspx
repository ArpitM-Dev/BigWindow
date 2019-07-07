<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getStoreAlbum.aspx.cs" Inherits="storeSocial.getStoreAlbum" %>
<div class="row" runat="server" id="albumTemplate" style="border-bottom:2px solid #ddd; padding-bottom:20px;">
    <div class="col-xs-12"><h3 class="noPadding noMargin blueFont">[albumName] <small><a href="javascript:void(0);" onclick="getAlbum(0);">close</a></small></h3></div>
    [megaItem]
    <div class="col-xs-2 pt10" style="height:150px; overflow:hidden;"><a href="/getThumb.aspx?width=800&amp;img=img/cImg/[imgName]" class="fancyPrdImg"><img src="/getThumb.aspx?width=225&amp;img=img/cImg/[imgName]" alt="" class="darkBorderAll"></a></div>
    [megaItem]
</div>