<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="storeSocial._default" MasterPageFile="~/main.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="cpMainBody" ID="cpMainbody">
        <div class="col-xs-7 homeBody">
            <asp:Literal runat="server" ID="ltFeedSearchHF"></asp:Literal>            
            <div class="homeSearchProduct leftDiv">
                <div class="homeSearchField"><input type="text" class="form-control form-control-home-topNav" id="searchByPrd" placeholder="Business Name or Product Deals" /></div>
                <div class="homeSearchBtn"><a href="javascript:void(0);" onclick="applyPrdValue();" class="searchBtn"></a></div>
            </div>
            <div class="homeSearchProduct leftDiv">
                <div class="homeSearchField"><input type="text" class="form-control form-control-home-topNav" id="searchByLocation" placeholder="Town, Suburb or City" /></div>
                <div class="homeSearchBtn"><a href="javascript:void(0);" onclick="applyPrdLocationValue();" class="searchBtn"></a></div>
            </div>
            <div class="col-xs-12 homeTopLinks lessPadding">
                <a href="/">News Feed</a> | <a runat="server" id="mySub">My Subscriptions</a> 
            </div>
            <div class="row"><div class="homeData">
                <asp:Literal runat="server" ID="ltHomeFilters"></asp:Literal>
                <div class="col-xs-12 tm10"></div>
                <div class="col-xs-12 clearfix pt10 noPadding infinite-container"><div class="infinite-item"></div></div>
                <a id="infiniteSearch" class="infinite-more-link" runat="server">More</a>
            </div></div>
        </div>
        <div class="col-xs-3 rightCol">
            <div class="rightHead lessPadding">
                <h2>Store Index</h2>
                <div class="homeSearchProduct">
                    <div class="homeSearchField"><input type="text" class="form-control form-control-home-topNav" id="searchByStore" placeholder="Filter by store category" /></div>
                    <div class="homeSearchBtn"><a href="javascript:void(0);" onclick="loadStoresScroll($('#searchByStore').val());" class="searchBtn"></a></div>
                </div>
            </div>

            <div style="height:40px; padding:0 37px;">          
             <div id="myCarousel" class="carousel slide">
                <div class="carousel-inner">
                    <div class="item active">
                        <div class="row">
                              <ul class="pagination noMargin noPadding" style="margin-left:16px !important;">
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('');">ALL</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('a');">A</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('b');">B</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('c');">C</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('d');">D</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="item">
                        <div class="row">
                              <ul class="pagination noMargin noPadding" style="margin-left:17px !important;">
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('e');">E</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('f');">F</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('g');">G</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('h');">H</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('i');">I</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('j');">J</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="item">
                        <div class="row">
                              <ul class="pagination noMargin noPadding" style="margin-left:12px !important;">
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('k');">K</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('l');">L</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('m');">M</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('n');">N</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('o');">O</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('p');">P</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="item">
                        <div class="row">
                              <ul class="pagination noMargin noPadding" style="margin-left:13px !important;">
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('q');">Q</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('r');">R</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('s');">S</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('t');">T</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('u');">U</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('v');">V</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="item">
                        <div class="row">
                              <ul class="pagination noMargin noPadding" style="margin-left:15px !important;">
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('w');">W</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('x');">X</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('y');">Y</a></li>
                                  <li><a href="javascript:void(0);" onclick="loadStoresScroll('z');">Z</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <a class="left carousel-control  darkBorderAll img-rounded" href="#myCarousel" data-slide="prev">&laquo;</a>
                <a class="right carousel-control darkBorderAll img-rounded" href="#myCarousel" data-slide="next">&raquo;</a>                
            </div><!-- End Carousel --> 
            </div>

            <div class="col-xs-12 rightStoreList img-rounded pt10 pb10">
               <div class="col-xs-12 noPadding" style="max-height:200px; overflow-y:scroll;" id="storeList"></div>
            </div>

            <div class="rightHead lessPadding tm20 col-xs-12">
                <h2><a href="/storeNearBy.aspx">All Stores</a></h2>
                <div class="homeSearchProduct">
                    <div class="homeSearchField"><input type="text" class="form-control form-control-home-topNav" id="filterByLocation" placeholder="Search by store name" /></div>
                    <div class="homeSearchBtn"><a href="javascript:void(0);" onclick="goToStoreNearBy($('#filterByLocation').val());" class="searchBtn"></a></div>
                </div>
            </div>
            <div class="col-xs-12 rightStoreList lessPadding img-rounded">
                <div class="col-xs-12 noPadding" style="max-height:390px; overflow-y:scroll;" id="storeImgList"></div>
            </div>
        </div>
</asp:Content>
