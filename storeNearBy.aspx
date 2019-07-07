<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="storeNearBy.aspx.cs" Inherits="storeSocial.storeNearBy" MasterPageFile="~/main.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="cpMainBody" ID="cpMainBody">
<div class="col-xs-10 innerLargeBodyHolder">    
    <div class="col-xs-12 innerLargeBody noPadding">
        <div class="col-xs-12 pageDiv noPadding">
            <div class="col-xs-6"><h2 class="innerHead"><a href="/storeNearBy.aspx" class="bigLink">View all Stores (<asp:Literal runat="server" ID="totalStores"></asp:Literal>)</a></h2></div>
            <div class="col-xs-3 storeTopDrops" style="margin-top:3px;">                   
                <table style="width:100%;">
                    <tr>
                        <td><input type="text" class="form-control form-control-home-topNav" id="filterByStLocation" placeholder="Search by store location" /></td>
                        <td style="width:30px;"><a href="javascript:void(0);" onclick="applyFilters();" class="searchBtn"></a></td>
                    </tr>
                </table> 
            </div>
            <div class="col-xs-3 storeTopDrops"><asp:DropDownList runat="server" ID="ddCategory" CssClass="form-control chosen-select"></asp:DropDownList></div>
        </div>

        <div class="col-xs-12 leftDiv tm10">

            <div class="col-xs-8 activeFilters img-rounded">
                <div class="col-xs-2 text-right" style="padding:7px 0px;"><h4 class="noMargin blueFont">Active Filters :</h4></div>
                <div class="col-xs-10" runat="server" id="storeFilters"></div>                
            </div>
            <div class="col-xs-4" style="padding-right:0;">
            <div class="col-xs-12 img-rounded" style="background:#f1f1f1; padding:10px;">
                    <div class=" pull-left"><input type="text" class="form-control form-control-home-topNav" style="width:265px;" id="filterByLocation" placeholder="Search by store name" /></div>
                    <div class=" pull-left"><a href="javascript:void(0);" onclick="applyFilters();" class="searchBtn"></a></div>
                    
                <%--<iframe src="/exForm.aspx" marginheight="0" marginwidth="0" frameborder="0" scrolling="no" style="width:320px; height:45px; margin-top:5px; "></iframe>--%>
            </div>           
                </div>

        </div>

        

        <div class="col-xs-12 lessPadding">            
            <asp:Literal runat="server" ID="ltStores"></asp:Literal>
            <div style="display:none" runat="server" id="sysErr"></div>
        </div>
    </div>
</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="cpExtraBotScripts" runat="server">
    <input type="hidden" id="hfLoc" runat="server" />
    <input type="hidden" id="hfCat" runat="server" />
    <input type="hidden" id="hfStoreName" runat="server" />
    <script>
        $("#cpMainBody_ddCategory").change(function () { applyFilters(); });

        function applyFilters()
        {
            var storeName = ($("#filterByLocation").val() == "") ? $("#cpExtraBotScripts_hfStoreName").val() : $("#filterByLocation").val();
            var storeLocations = ($("#filterByStLocation").val() == "") ? $("#cpExtraBotScripts_hfLoc").val() : $("#filterByStLocation").val();
            var storeCategories = ($("#cpMainBody_ddCategory").val() == "") ? $("#cpExtraBotScripts_hfCat").val() : $("#cpExtraBotScripts_hfCat").val() + "," + $("#cpMainBody_ddCategory").val();
            var URL = "/storeNearBy.aspx?storename=" + storeName + "&locid=" + storeLocations + "&catid=" + storeCategories;
            window.location = URL;
        }

        function removeFilter(fType, fID) { switch (fType) { case 1: $("#cpExtraBotScripts_hfCat").val(("," + $("#cpExtraBotScripts_hfCat").val()).replace("," + fID, "")); break; case 2: $("#cpExtraBotScripts_hfStoreName").val(''); break; case 3: $("#cpExtraBotScripts_hfLoc").val(''); break; } applyFilters(); }
        </script>
</asp:Content>