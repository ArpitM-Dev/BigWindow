using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaSoftwaresUtilities;
using System.Data;

namespace storeSocial
{
    public partial class feed : System.Web.UI.Page
    {
        int curPage, uID, storeID, cid; string userMsg, sysMsg, cPreFix, postIDs, prd, loc;
        protected void Page_Load(object sender, EventArgs e)
        {
            prd = postUtilities.getStringFrmQString("prd"); loc = postUtilities.getStringFrmQString("loc"); cid = megaUtilities.genFunctions.getIDfromString(postUtilities.getStringFrmQString("cid")); 
            curPage = postUtilities.getIntFrmQString("pid"); 
            uID = postUtilities.getIntFrmSession("userID");
            storeID = postUtilities.getIntFrmQString("storeID");
            cPreFix = System.Configuration.ConfigurationManager.AppSettings["cPreFix"];
            if (!Page.IsPostBack) getPage();
        }

        private void getPage()
        {
            DataSet DSdataSet = getDSdataSet(); //Get data from Database
            if (userMsg == "Success")
            {
                DataTable tblFeed = DSdataSet.Tables["tblFeed"];
                if (tblFeed.Rows.Count > 0)
                {
                    ltFeed.Text = "";
                    //Get Post IDs
                    for (int k = 0; k < tblFeed.Rows.Count; k++) { postIDs += tblFeed.Rows[k]["postID"].ToString(); if (k < tblFeed.Rows.Count - 1) postIDs += ","; }
                    DataTable tblComments = getCommentsFromDB();
                    for (int i = 0; i < tblFeed.Rows.Count; i++) ltFeed.Text += getFeedRow(tblFeed.Rows[i], tblComments);
                    lbFeedLink.HRef = "/feed.aspx?pid=" + (curPage + 1).ToString() + "&prd=" + prd + "&loc=" + loc + "&cid=" + cid.ToString() + "&storeid=" + storeID.ToString() + "&rid=" + genFunctions.getRandNum(); 
                    ltFeed.Text += "<input type=\"hidden\" id=\"pageID" + (curPage + 1).ToString() + "\" value=\"" + postIDs + "\" />";
                }
                else
                {
                    lbFeedLink.HRef = "javascript:void(0);";
                    lbFeedLink.InnerHtml = "No More Feeds Available";
                }
            }
            else genFunctions.printError(ltUserErr, ltSysErr, userMsg, sysMsg);
        }

        private string getFeedRow(DataRow DR, DataTable tblComments)
        {
            string postImg = DR["postImage"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string postID = DR["postID"].ToString();
            int totalComments = Convert.ToInt32(DR["totalComments"].ToString());
            string storeName = DR["storeName"].ToString(); if (storeName == "") storeName = "No name provided";
            sb.Append("<div class=\"col-xs-2 noPadding feedLogo\"><a href = \"/stores/" + DR["storeID"].ToString() + "/" + urlUtilities.codeStrURL(storeName) + "/\"><img src=\"/getThumb.aspx?width=120&img=img/cImg/" + DR["storeLogo"].ToString() + "\" alt=\"\" /></a></div>");
            sb.Append("<div class=\"col-xs-10 feedData\">");
            sb.Append("<h2><a href = \"/stores/" + DR["storeID"].ToString() + "/" + urlUtilities.codeStrURL(storeName) + "/\">" + storeName + "</a></h2>");
            sb.Append(DR["storeTagLine"].ToString());
            sb.Append("<div class=\"col-xs-12 img-rounded feedDataProduct lightBorderAll tm10 pt10\">");
            sb.Append(" <table cellpadding=\"0\" cellspacing=\"0\">");
            sb.Append("<tr>");
            if (postImg!="") sb.Append("<td rowspan=\"2\" class=\"col-xs-5 feedImg\"><a href = \"/stores/" + DR["storeID"].ToString() + "/" + urlUtilities.codeStrURL(storeName) + "\"><img class=\"lightBorderAll\" src=\"/getThumb.aspx?width=202&img=img/cImg/" + DR["postImage"].ToString() + "\" alt=\"\" /></a></td>");
            sb.Append("<td class=\"col-xs-7 noPadding\">");
            sb.Append("<h3>" + DR["postName"].ToString() + "</h3>");
            sb.Append("<div class=\"col-xs-12 noPadding article\">" + DR["postDetail"].ToString() + "</div>");
            sb.Append(" </td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            string subscribeOnClick = " onclick=\"subScribe('sub" + postID + "'," + DR["storeID"].ToString() + ");\" ";
            sb.Append("<td class=\"col-xs-7 homeFeedSubscribe noPadding\">" + genFunc.getLoginMsg("subscribe", "jazTipSubscribe", subscribeOnClick, postID, DR["storeID"].ToString(), Convert.ToInt32(DR["userSub"])) + "</td>");
            sb.Append("</tr>");
            sb.Append(" </table>");
            sb.Append("</div>");

            string likeOnClick = " onclick=\"LUpost('u" + DR["postID"].ToString() + "', 'ua" + DR["postID"].ToString() + "'," + DR["totalLikes"].ToString() + "," + postID + ");\" ";

            sb.Append("<div class=\"col-xs-12 tm5 homeLCBox\"><i class=\"fa fa-thumbs-o-up\"></i> " + genFunc.getLoginMsg("like", "jazTipLike", likeOnClick, postID,  DR["storeID"].ToString(), Convert.ToInt32(DR["userLike"])));
            sb.Append(" | <i class=\"fa fa-comments-o\"></i> " + genFunc.getLoginMsg("comment", "jazTipComment", "javascript:void(0);", postID, DR["storeID"].ToString(), 0) + "</div>");
            sb.Append("<div class=\"col-xs-12 tm5 pt10 pb10 commentBox img-rounded\">");
            sb.Append("<div class=\"col-xs-12 lessPadding darkBorderBot\"><i class=\"fa fa-thumbs-o-up\" style=\"float:left; padding:3px 3px 0 0;\"></i><div style=\"float:left;\" id=\"ua" + DR["postID"].ToString() + "\">"+getLikeText(DR)+"</div></div>");

            if (totalComments>0) sb.Append("<div class=\"col-xs-12 lessPadding\">").Append(getCommentsForPost(postID, tblComments, totalComments)).Append(" </div>");
            sb.Append("<div class=\"col-xs-12 lessPadding\" id=\"comDiv" + postID + "\"></div>");
            if (uID > 0) sb.Append("<div class=\"col-xs-12\" style=\"padding:10px 0;\"><input type=\"text\" id=\"comm" + postID + "\" class=\"form-control txtComment\" placeholder=\"Write a comment\" /></div>");

            sb.Append("</div>");
            sb.Append("</div>");           

            return sb.ToString();
        }

        private string getCommentsForPost(string postID, DataTable tblComments, int totalComments)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (totalComments > 3)
            {
                sb.Append("<div class=\"col-xs-12\" style=\"padding:5px 0;\"><i class=\"fa fa-comments-o\"></i> <a class=\"loadMore\" id=\"showMore" + postID + "\" href=\"javascript:void(0);\">View " + (totalComments - 3).ToString() + " more comments</a>");
                sb.Append(" <a class=\"loadLess\" id=\"showLess" + postID + "\" href=\"javascript:void(0);\" style=\"display:none;\">Show less comments</a></div>");
            }
            sb.Append("<ul id=\"myList" + postID + "\" class=\"myList\"> ");
            for (int i = 0; i < tblComments.Rows.Count; i++) { if (postID == tblComments.Rows[i]["postID"].ToString()) sb.Append(getCommentRow(tblComments.Rows[i])); }
            sb.Append("</u>");
            return sb.ToString();
        }

        private string getCommentRow(DataRow DR)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<li>");
            //sb.Append("<table><tr><td class=\"commentImg\"><div style=\"width:32px; height:32px; overflow:hidden\"><img src=\"/getThumb.aspx?width=32&img=img/commentimg.png\" alt=\"\" /></div></td>");
            sb.Append("<table><tr>");
            sb.Append("<td id=\"comm_" + DR["commentID"].ToString() + "\"><b>" + DR["userName"].ToString() + "</b> " + DR["commentText"].ToString() + (((uID.ToString() == DR["uID"].ToString()) || postUtilities.getIntFrmSession("adminID")>0) ? " <a href=\"javascript:void(0);\" style=\"float:right; color:Red;\" onclick=\"delComment('" + DR["commentID"].ToString() + "')\"> - Delete</a>" : "") + "<br /><abbr class=\"timeago\" title=\"" + Convert.ToDateTime(DR["commentDate"]).ToString("yyyy-MM-ddTHH:mm:ss") + "\">" + Convert.ToDateTime(DR["commentDate"]).ToString("MMM dd, yyyy HH:mm:ss") + "</abbr></td>");
            sb.Append("</tr></table></li>");
            return sb.ToString();
        }

        private string getLikeText(DataRow DR)
        {
            string returnText = "";

            int totalLikes = Convert.ToInt32(DR["totalLikes"]);
            int userLike = Convert.ToInt32(DR["userLike"]);
            if (userLike > 0) totalLikes++;

            string likeUserName = DR["likeUserName"].ToString();
            switch (totalLikes)
            {
                case 0: returnText = "Be the first one to like it"; break;
                case 1:
                    returnText = "<b>" + likeUserName + "</b> like it";
                    if (userLike > 0) returnText = "You like this"; break;
                case 2:
                    returnText = "<b>" + likeUserName + "</b>";
                    if (userLike > 0) returnText = "You and " + returnText;
                    else returnText += " and 1 other ";
                    returnText += " like it"; 
                    break;
                default:
                    returnText = "<b>" + likeUserName + "</b> and <strong>totalLikes other</strong> like this";
                    if (userLike > 0) returnText = "You, " + returnText.Replace("totalLikes", (totalLikes - 2).ToString());
                    else returnText = returnText.Replace("totalLikes", (totalLikes - 1).ToString());
                    break;
            }

            return returnText;
        }

        private DataTable getCommentsFromDB()
        {
            DataTable DT = new DataTable();

            if (postIDs != "")
            {
                string commentSQL = "SELECT C.commentID, C.postID, C.commentDate, C.commentText, (U.uFirstName + ' ' + U.uLastName) as userName, U.uID FROM bw_post_comment C LEFT OUTER JOIN bw_user U ON U.uID = C.uID WHERE postID in (" + postIDs + ")";
                int i = -1; int arrayLength = 1; string[] outputString; string[,] inputSQL = new string[arrayLength, 4]; string[,] inputParams = new string[0, 3];
                i++; inputSQL[i, 0] = i.ToString(); inputSQL[i, 1] = commentSQL; inputSQL[i, 2] = "SQL"; inputSQL[i, 3] = "DT";
                sqlData objDAL = new sqlData(); DataSet DSdataSet = objDAL.getDataset(inputSQL, inputParams, out outputString);
                if (outputString[0].ToString() == "Success") DT = DSdataSet.Tables[0];
            }
            return DT;
        }

        private DataSet getDSdataSet()
        {
            int i = -1;
            int arrayLength = 1;

            string[,] inputSQL = new string[arrayLength, 4]; //inputSQL (sqlID, sqlText, commandtype, tableName)
            string[,] inputParams = new string[0, 3]; //inputParams (sqlID, paramName, paramValue) 
            string[] outputString; //outputString (outputMessages)
            DataSet DSdataSet; //multi table dataset 

            i++; inputSQL[i, 0] = i.ToString();
            inputSQL[i, 1] = getSQL();
            inputSQL[i, 2] = "SQL";
            inputSQL[i, 3] = "tblFeed";

            sqlData objDAL = new sqlData();
            DSdataSet = objDAL.getDataset(inputSQL, inputParams, out outputString);

            userMsg = outputString[0].ToString();
            sysMsg = outputString[1].ToString();
            return DSdataSet;
        }

        private string getSQL()
        {
            string megaSQL = "";
            int pageSize = 5;

            megaSQL += "DECLARE @first_id int;";

            if (curPage==0) megaSQL += "SET rowcount " + ((pageSize * curPage) + 1).ToString() + ";"; 
            else megaSQL += "SET rowcount " + (pageSize * curPage).ToString() + ";";

            megaSQL += "SELECT @first_id = postID from bw_store_post ORDER BY postDate DESC;";
            megaSQL += "SET ROWCOUNT " + pageSize.ToString() + ";";

            megaSQL += "select DISTINCT P.*";
            megaSQL += ", storeLogo, storeName, storeTagLine, totalLikes=(SELECT count(*) from bw_post_like WHERE postID=P.postID AND uid<>" + uID.ToString() + "),";
            megaSQL += " likeUserName = (SELECT TOP 1 uFirstName + ' ' + uLastName from bw_User WHERE uID=(SELECT TOP 1 uID from bw_post_like WHERE postID=P.postID AND uid<>" + uID.ToString() + ")),";
            megaSQL += " userLike=(SELECT count(*) from bw_post_like WHERE postID=P.postID AND uid=" + uID.ToString() + "),";
            megaSQL += " userSub=(SELECT count(*) from bw_post_subscribe WHERE storeID=(select TOP 1 storeID from bw_store where uID=P.uID) AND uid=" + uID.ToString() + "),";
            megaSQL += " storeID = (select TOP 1 storeID from bw_store where uID=P.uID),";
            megaSQL += " totalComments = (select Count(*) from bw_post_comment where postID=P.postID)";
            megaSQL += " from bw_store_post P";
            megaSQL += " LEFT OUTER JOIN bw_user U ON U.uID=P.uID";
            megaSQL += " LEFT OUTER JOIN bw_store S ON S.uID=U.uID";
            megaSQL += " LEFT OUTER JOIN bw_user_country C ON S.storeCountry=C.countryID";
            megaSQL += " LEFT OUTER JOIN bw_store_cat_rel CR ON CR.storeID=S.storeID";
            megaSQL += " LEFT OUTER JOIN bw_store_loc L ON L.locID=S.storeLocation";
            if (curPage == 0) megaSQL += " WHERE postID<=@first_id"; else megaSQL += " WHERE postID<@first_id";

            megaSQL += " AND uAllowed=1 AND uVarified=1";
            if (prd != "")
            {
                string[] APrd = prd.Split(','); bool isAndAdded = false; bool isPrdIni = false;
                for (int i = 0; i < APrd.Length; i++)
                {
                    if (APrd[i] != "")
                    {
                        if (isAndAdded) megaSQL += " OR"; else { megaSQL += " AND ("; isAndAdded = true; }
                        megaSQL += " (postName like '%" + APrd[i].Replace(" ", "%") + "%' OR postTitle like '%" + APrd[i].Replace(" ", "%") + "%'  OR storeName like '%" + APrd[i].Replace(" ", "%") + "%' )";
                        isPrdIni = true;
                    }
                }
                if (isPrdIni) megaSQL += " )";
            }

            if (loc != "")
            {
                string[] ALoc = loc.Split(','); bool isAndAdded = false; bool isPrdIni = false;
                for (int i = 0; i < ALoc.Length; i++)
                {
                    if (ALoc[i] != "")
                    {
                        if (isAndAdded) megaSQL += " OR"; else { megaSQL += " AND ("; isAndAdded = true; }
                        megaSQL += " (locName like '%" + ALoc[i].Replace(" ", "%") + "%' OR storeAddress like '%" + ALoc[i].Replace(" ", "%") + "%'  OR storeCity like '%" + ALoc[i].Replace(" ", "%") + "%'  OR storeState like '%" + ALoc[i].Replace(" ", "%") + "%'  OR country_Name like '%" + ALoc[i].Replace(" ", "%") + "%' )";
                        isPrdIni = true;
                    }
                }
                if (isPrdIni) megaSQL += " )";
            }

            if (storeID > 0) megaSQL += " AND S.storeID=" + storeID.ToString();
            if (cid > 0) megaSQL += " AND cID=" + cid.ToString();
            megaSQL += " ORDER BY postDate DESC;";
            megaSQL += "SET ROWCOUNT 0;";

            //Response.Write(megaSQL); Response.End();
            return megaSQL;
        }
    }
}
