using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaSoftwaresUtilities;

namespace storeSocial
{
    public partial class _default : System.Web.UI.Page
    {
        string prd, loc, cid; int uID;
        protected void Page_Load(object sender, EventArgs e)
        {
            uID = postUtilities.getIntFrmSession("userID");
            prd = postUtilities.getStringFrmQString("prd"); loc = postUtilities.getStringFrmQString("loc"); cid = postUtilities.getStringFrmQString("cid"); 
            if (!Page.IsPostBack) getPage();
        }

        private void getPage()
        {
            if (uID > 0) { if (postUtilities.getIntFrmSession("uType") == 4) mySub.HRef = "/account/user/"; else { mySub.InnerHtml = "My Store Settings"; mySub.HRef = "/account/store-owner/"; } } else { mySub.HRef = "javascript:void(0);"; mySub.Attributes.Add("class", "jazTipGeneral"); }
            ltFeedSearchHF.Text = "<input type=\"hidden\" id=\"hfPrdSearch\" value=\"" + prd + "\" /><input type=\"hidden\" id=\"hfLocSearch\" value=\"" + loc + "\" /><input type=\"hidden\" id=\"hfCatSearch\" value=\"" + cid + "\" />";
            infiniteSearch.HRef = "feed.aspx?prd=" + prd + "&loc=" + loc + "&cid=" + cid + "&rid=" + genFunctions.getRandNum();

            if (prd != "" || loc != "" || cid !="")
            {
                ltHomeFilters.Text = "<div class=\"activeFilters img-rounded col-xs-12\"><div class=\"col-xs-2 text-right\" style=\"padding:7px 3px;\"><h4 class=\"noMargin blueFont\">Active Filters :</h4></div><div class=\"col-xs-10\">";
                ltHomeFilters.Text += getFiltersList();
                ltHomeFilters.Text += "</div></div>";
            }
        }

        private string getFiltersList()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string[] APrd = prd.Split(',');
            for (int i = 0; i < APrd.Length; i++)
            {
                if (APrd[i] != "")
                    sb.Append(" <a href=\"javascript:void(0);\" onclick=\"removePrd('" + APrd[i] + "');\" class=\"btn btn-default btn-sm lessPadding\">" + APrd[i] + " <i class=\"glyphicon glyphicon-remove\"></i></a>");
            }


            string[] ALoc = loc.Split(',');
            for (int i = 0; i < ALoc.Length; i++)
            {
                if (ALoc[i] != "")
                    sb.Append(" <a href=\"javascript:void(0);\" onclick=\"removeLoc('" + ALoc[i] + "');\" class=\"btn btn-default btn-sm lessPadding\">" + ALoc[i] + " <i class=\"glyphicon glyphicon-remove\"></i></a>");
            }
            if (cid != "") sb.Append(" <a href=\"javascript:void(0);\" onclick=\"removeCat('" + cid + "');\" class=\"btn btn-default btn-sm lessPadding\">" + megaUtilities.genFunctions.removeFirstAndLastCharacter(cid.Replace("-", " ") + " ").Trim() + " <i class=\"glyphicon glyphicon-remove\"></i></a>");
            return sb.ToString();
        }
    }
}