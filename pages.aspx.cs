using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaSoftwaresUtilities;
using megaAdmin;

namespace storeSocial
{
    public partial class pages : System.Web.UI.Page
    {
        int catID, pageID, uID;
        protected void Page_Load(object sender, EventArgs e)
        {
            uID = postUtilities.getIntFrmSession("userID");
            catID = postUtilities.getIntFrmRoute("catID", Page);
            pageID = postUtilities.getIntFrmRoute("pageID", Page);
            if (!Page.IsPostBack) getPageData();
        }

        private void getPageData()
        {
            if (uID > 0) { if (postUtilities.getIntFrmSession("uType") == 4) mySub.HRef = "/account/user/"; else { mySub.InnerHtml = "My Store Settings"; mySub.HRef = "/account/store-owner/"; } } else { mySub.HRef = "javascript:void(0);"; mySub.Attributes.Add("class", "jazTipGeneral"); }
            ltFeedSearchHF.Text = "<input type=\"hidden\" id=\"hfPrdSearch\" value=\"\" /><input type=\"hidden\" id=\"hfLocSearch\" value=\"\" /><input type=\"hidden\" id=\"hfCatSearch\" value=\"\" />";

            string pageContent, pageSEO, headingFormat, contentFormat, abstractFormat;
            headingFormat = "<h1 class=\"noPadding noMargin blueFont\">megaPageHeading</h1><h2 class=\"noPadding noMargin pt10\" style=\"font-size:18px;\">megaPageSubHeading</h2>";
            contentFormat = "<div class=\"col-xs-12 noPadding tm10\">megaPageContent</div>";
            abstractFormat = "<h2 class=\"noMargin\" style=\"font-size:18px; padding-bottom:3px;\">megaPageAbstractHeading</h2><div class=\"col-xs-12 noPadding\">megaPageAbstractContent<br /><a href=\"megaPageAbstractReadMoreLink\" style=\"margin-top:3px; display:block;\">Read More</a></div><div class=\"col-xs-12 clearfix tm10\"></div>";

            adminFunctions.getPageContent(headingFormat, contentFormat, abstractFormat, catID, pageID, 250, out pageSEO, out pageContent);
            //ltPageSEO.Text = pageSEO; 
            ltPageContent.Text = pageContent;
        }
    }
}