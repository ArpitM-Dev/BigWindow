using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaUtilities;
using System.Data;

namespace storeSocial
{
    public partial class storeNearBy : System.Web.UI.Page
    {
        string storeName, locID, catID; int uID;
        protected void Page_Load(object sender, EventArgs e)
        {
            uID = postUtilities.getIntFrmSession("userID");
            storeName = postUtilities.getStringFrmQString("storename"); locID = postUtilities.getStringFrmQString("locid"); catID = postUtilities.getStringFrmQString("catid").Replace(",0","");
            if (!Page.IsPostBack) getStores();
        }

        private void getStores()
        {
            int i = -1, j = -1, arrayLength = 4, paramLength = 0; sqlConnectivity sql = new sqlConnectivity(); sql.inputSQL = new string[arrayLength, 4]; sql.inputParams = new string[paramLength, 3];
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblStores"; sql.inputSQL[i, 1] = getStoreListSQL();
            //i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblLocations"; sql.inputSQL[i, 1] = "SELECT * FROM bw_store_loc";
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblCat"; sql.inputSQL[i, 1] = "SELECT * FROM bw_store_cat";
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblStoreCount"; sql.inputSQL[i, 1] = "SELECT count(*) FROM bw_store S LEFT OUTER JOIN bw_user U ON U.uID=S.uID WHERE uAllowed=1 AND uVarified=1";
            //i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblSelectedLocations"; sql.inputSQL[i, 1] = "SELECT * FROM bw_store_loc WHERE locID IN (0" + locID + ")";
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblSelectedCats"; sql.inputSQL[i, 1] = "SELECT * FROM bw_store_cat WHERE cID IN (0" + catID + ")";
         
            sql.getData();
            if (sql.sysErr == "")
            {
                //genFunctions.createDrop("Please select location", "", "locName", "locID", ddLocation, sql.megaDataSet.Tables["tblLocations"]);
                genFunctions.createDrop("Please select category", "", "cName", "cID", ddCategory, sql.megaDataSet.Tables["tblCat"]);
                totalStores.Text = sql.megaDataSet.Tables["tblStoreCount"].Rows[0][0].ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptUtilities.getChosen(), true);
                //DataTable tblLoc = sql.megaDataSet.Tables["tblSelectedLocations"];
                
                //for (int a = 0; a < tblLoc.Rows.Count; a++)
                //{
                //    storeFilters.InnerHtml += "<a href=\"javascript:void(0);\" onclick=\"removeFilter('loc_" + tblLoc.Rows[a][0].ToString() + "')\" class=\"btn btn-default btn-sm lessPadding\">" + tblLoc.Rows[a][1].ToString() + " <i class=\"glyphicon glyphicon-remove\"></i></a> ";
                //    if (a > 0) hfLoc.Value += ",";
                //    hfLoc.Value += tblLoc.Rows[a][0].ToString();
                //}

                DataTable tblCats = sql.megaDataSet.Tables["tblSelectedCats"];
                for (int b = 0; b < tblCats.Rows.Count; b++)
                {
                    storeFilters.InnerHtml += "<a href=\"javascript:void(0);\" onclick=\"removeFilter(1, '" + tblCats.Rows[b][0].ToString() + "')\" class=\"btn btn-default btn-sm lessPadding\">" + tblCats.Rows[b][1].ToString() + " <i class=\"glyphicon glyphicon-remove\"></i></a> ";
                    if (b > 0) hfCat.Value += ",";
                    hfCat.Value += tblCats.Rows[b][0].ToString();
                }

                hfStoreName.Value = storeName; if (storeName != "") storeFilters.InnerHtml += "<a href=\"javascript:void(0);\" onclick=\"removeFilter(2, '')\" class=\"btn btn-default btn-sm lessPadding\">" + storeName + " <i class=\"glyphicon glyphicon-remove\"></i></a> ";
                hfLoc.Value = locID; if (locID != "") storeFilters.InnerHtml += "<a href=\"javascript:void(0);\" onclick=\"removeFilter(3, '')\" class=\"btn btn-default btn-sm lessPadding\">" + locID + " <i class=\"glyphicon glyphicon-remove\"></i></a> ";

                getStoreList(sql.megaDataSet.Tables["tblStores"]);
            }
            else sysErr.InnerHtml = sql.sysErr;
        }

        private void getStoreList(DataTable DT)
        {
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                sb.Append("<div class=\"col-xs-4 mediumPadding\">");
                sb.Append("<div class=\"col-xs-12 img-rounded storeSearch lightBorderAll tm10\">");
                sb.Append("<table>");
                sb.Append("<tr> ");
                sb.Append("<td style=\"width:120px;\">");
                sb.Append("<div class=\"col-xs-12 noPadding storeSearchImg\"><img src=\"/getThumb.aspx?width=120&img=img/cImg/" + DT.Rows[i]["storeLogo"].ToString() + "\" alt=\"\" /></div>");
                string subscribeOnClick = " onclick=\"subScribe('sub" + DT.Rows[i]["storeID"].ToString() + "'," + DT.Rows[i]["storeID"].ToString() + ");\" ";
                sb.Append("<div class=\"col-xs-12\" style=\"height:10px;\"></div>" + genFunc.getLoginMsg("subscribe", "jazTipSubscribe", subscribeOnClick, DT.Rows[i]["storeID"].ToString(), DT.Rows[i]["storeID"].ToString(), Convert.ToInt32(DT.Rows[i]["userSub"]))); 
                sb.Append("</td>");
                sb.Append("<td style=\"padding-left:10px; text-align:left;\"><h3 class=\"blueFont\"><a href=\"/stores/" + DT.Rows[i]["storeID"].ToString() + "/" + urlUtilities.codeStrURL(DT.Rows[i]["storeName"].ToString()) + "/\">" + DT.Rows[i]["storeName"].ToString() + "</a></h3>");
                sb.Append("<span>" + DT.Rows[i]["locName"].ToString() + "</span>");
                sb.Append("<span>Categories : " + DT.Rows[i]["categories"].ToString() + "</span>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            ltStores.Text = "<div class=\"col-xs-12\"><div class=\"col-xs-12 img-rounded\" style=\"background:#f1f1f1; padding:10px; margin-top:10px;\">" + DT.Rows.Count.ToString() + " Stores Found </div></div>" + sb.ToString();

        }

        private string getStoreListSQL()
        {
            bool isCondAdded = true;
            string rtnStr = "";
            rtnStr += "SELECT DISTINCT S.storeID, S.storeLogo, S.storeName, locName, S.storeCity, userSub = (SELECT count(*) from bw_post_subscribe WHERE storeID=S.storeID AND uID=" + uID.ToString() + "), storeImg = (SELECT TOP 1 imgName from bw_user_img WHERE uID=S.uID), S.storeState, categories=STUFF((SELECT ', ' + cName FROM bw_store_cat WHERE cID IN (SELECT cID FROM bw_store_cat_rel WHERE storeID=S.storeID) FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 1, '') FROM bw_store S";
            rtnStr += " LEFT OUTER JOIN bw_store_cat_rel CR ON CR.storeID = S.storeID";
            rtnStr += " LEFT OUTER JOIN bw_store_loc L ON L.locID = S.storeLocation";
            rtnStr += " LEFT OUTER JOIN bw_user U ON U.uID=S.uID";
            rtnStr += " WHERE uAllowed=1 AND uVarified=1";
            if (catID != "") rtnStr += " AND cID IN (0" + catID + ")"; 
            if (storeName != "") { rtnStr += (isCondAdded) ? " AND" : " WHERE"; rtnStr += " S.storeName LIKE '%" + storeName + "%'"; isCondAdded = true; }
            if (locID != "") { rtnStr += (isCondAdded) ? " AND" : " WHERE"; rtnStr += " (locName LIKE '%" + locID + "%' OR storeCity LIKE '%" + locID + "%' OR storeState LIKE '%" + locID + "%')"; }
            //Response.Write(rtnStr);
            return rtnStr;
        }
    }
}