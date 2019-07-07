<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="storeprofile.aspx.cs" Inherits="storeSocial.storeprofile" MasterPageFile="~/main.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="cpMainBody" ID="cpMainBody">

<%--                    <div class="col-xs-12 noPadding" style="height:30px; margin-top:3px;">
                    [megaRating]
                </div>--%>

<div class="col-xs-10 innerLargeBodyHolder">    
    <div class="col-xs-12 innerLargeBody noPadding" runat="server" id="storeDetails">
        <div class="col-xs-12 pageDiv">
            <table cellpadding="20" cellspacing="10" style="width:100%;" class="tm10">
                <tr>
                    <td class="col-xs-4 pt10 pb10"><img src="/getThumb.aspx?width=225&img=img/cImg/[storeLogo]" style="max-height:100px;" alt="" class="borderAll shadow" /><br />[storeTagLine]</td>
                    <td class="col-xs-8"><h4>[storeSmDesc]</h4></td>
                </tr>
            </table>
        </div>
        <div class="col-xs-12 pageDiv" style="padding:0 0 5px 0;">
            <div class="col-xs-7 tm10">
                <h1 class="blueFont noMargin noPadding">[storeName]</h1>
                <div class="col-xs-12 tm10 pt10 pb10 commentBox img-rounded">
                    <div class="row">
                        <div class="col-xs-12 leftdiv"><span class="glyphicon glyphicon-tags blackColor"></span> &nbsp;[storeCategoryList]</div>
                    </div>
                    
                    <div class="row tm5">
                        <div class="col-xs-12 leftdiv"><i class="mega-icon fa fa-map-marker blackColor"></i> &nbsp;[storeAddress]</div>
                    </div>
                    
                    <div class="row tm5">
                        <div class="col-xs-6"><span class="mega-icon glyphicon glyphicon-earphone blackColor"></span> &nbsp;[storePhone1][storePhone2]</div>
                        <div class="col-xs-6"><i class="mega-icon fa fa-reply-all blackColor"></i> &nbsp;<a href="[storeWebsite]" target="_blank">[storeWebsite]</a></div>                        
                    </div>                    
                    
                    <div class="row tm5">                        
                        <div class="col-xs-6"><i class="mega-icon fa fa-credit-card blackColor"></i> &nbsp;Payment Method: [paymentMethodList]</div>
                        <div class="col-xs-6"><span class="mega-icon glyphicon glyphicon-road blackColor"></span> &nbsp;Parking Available : [parkingFacilityAvailable] <small>([parkingInfo])</small></div>
                    </div>
                </div>

                <div class="col-xs-12 tm5 pt10 pb10 commentBox img-rounded">
                    <div class="pull-left"><i class="mega-icon fa fa-clock-o blackColor"></i></div><div class="pull-right" style="width:97%;">[storeOpenDays]</div>
                </div>                
                <div class="col-xs-12 pt10"></div>
            </div>
            
            <div class="col-xs-5 tm20">
                <div class="col-xs-12 noPadding text-center">
                    <div class="col-xs-4 noPadding"><a href="/storeMenu.aspx?storeid=[storeID]" class="btn btn-primary btn-subscribe medFancyBox1">Our Catalog &nbsp; <i class="fa fa-list xlFont"></i></a></div>
                    <div class="col-xs-4 noPadding">[subscribeLink]</div>
                    <div class="col-xs-4 noPadding">[storeMsgBtn]</div>
                </div>
                [storeAlbum]
                <div class="col-xs-12 text-right blueFont lessPadding" style="margin-top:65px; font-weight:bold;">Last updated : [lastUpdated]</div>
            </div>
        </div>
        <div class="col-xs-12 noPadding tm10">
            <div class="col-xs-12" id="albumHolder"></div>
                <div class="col-xs-12"><h2 class="blueFont">Special Offers by [storeName]</h2></div>
            <div class="col-xs-8">
                <div class="col-xs-12 clearfix pt10 noPadding infinite-container"><div class="infinite-item"></div></div>
                <a href="/feed.aspx?storeid=[storeID]&rid=" id="infiniteSearch" class="infinite-more-link">More</a>
            </div>
            
            <div class="col-xs-4">
                <div style="font-size:16px;" class="blueFont"><i class="fa fa-map-marker" style="font-size:24px;"></i> [storeCity], [storeState] - <a href="https://www.google.com/maps/embed/v1/place?key=AIzaSyADSBETYmF_BixvQ1ycyMwiosbya8dGeBk&q=[storeAddress]" class="medFancyBox1 small">View larger map</a></div>
                <div style="width:300px; height:320px; border:10px solid #fff; margin-top:10px;" class="shadow">
                    <iframe frameborder="0" style="width:100%; height:300px;"  src="https://www.google.com/maps/embed/v1/place?key=AIzaSyADSBETYmF_BixvQ1ycyMwiosbya8dGeBk&q=[storeAddress]"></iframe>
                </div>
            </div>
        </div>
    </div>
</div>
    <div style="display:none;" runat="server" id="divSysErr"></div>
</asp:Content>

<asp:Content ContentPlaceHolderID="cpExtraBotScripts" runat="server">
  <link href='/css/styles.css' rel='stylesheet' type='text/css' />
</asp:Content>