using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaSoftwaresUtilities;
using System.Data;
using megaAdmin;

namespace storeSocial
{
    public partial class main : System.Web.UI.MasterPage
    {
        int userID; string userType, userMsg, sysMsg, cPreFix;
        protected void Page_Load(object sender, EventArgs e)
        {            
            userID = postUtilities.getIntFrmSession("userID");
            userType = postUtilities.getStringFrmSession("uType");
            cPreFix = System.Configuration.ConfigurationManager.AppSettings["cPreFix"];
            HFuserName.Value = postUtilities.getStringFrmSession("userName");
            if (!Page.IsPostBack) getPage();
        }

        protected void logout(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/");
        }

        private void getPage()
        {
            if (postUtilities.getIntFrmQString("reg") > 0)
            {
                registerLink.HRef = "/register/?t=" + postUtilities.getIntFrmQString("reg").ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$(document).ready(function () { $('#registerLink').trigger('click');});", true);
            }
            
            DataSet DSdataSet = getDSdataSet(); //Get data from Database
            if (userMsg == "Success")
            {
                DataTable tblSideNav = DSdataSet.Tables["tblSideNav"];
                getLeftNav(DSdataSet.Tables["tblStore"]);
                ltLeftNav.Text += getLeftNavFromDB(tblSideNav);
            }
            else genFunctions.printError(ltUserErr, ltSysErr, userMsg, sysMsg);            
        }

        private string getLeftNavFromDB(DataTable tblSideNav)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < tblSideNav.Rows.Count; i++)
                sb.Append("<a href=\"" + adminFunctions.getCatLink(tblSideNav.Rows[i]) + "\">" + tblSideNav.Rows[i]["catName"].ToString() + "</a>");

            return sb.ToString();
        }

        private void getLeftNav(DataTable tblStore)
        {
            string storeOwnerLink = "<a class=\"loginBox\" href=\"/login/?t=5\">Store Owner</a>";
            string customerLink = "<a class=\"loginBox\" href=\"/login/?t=4\">Customers</a>";

            if (userID > 0)
            {
                leftSetting.Visible = true;
                if (userType == "5")
                {
                    ltLeftSetting.Text = "<a href=\"/account/store-owner/?t=4\">Post New Feed</a>";
                    ltLeftSetting.Text += "<a href=\"/account/store-owner/managefeed.aspx\">Manage Feeds</a>";
                    ltLeftSetting.Text += "<a href=\"/account/store-owner/\">Settings</a>";

                    if (tblStore.Rows.Count > 0) storeOwnerLink = "<a href=\"/stores/" + tblStore.Rows[0]["storeID"].ToString() + "/" + urlUtilities.codeStrURL(tblStore.Rows[0]["storeName"].ToString()) + "/\">Store Owner</a>";
                    else storeOwnerLink = "<a href=\"/account/store-owner/\">Store Owner</a>";
                    customerLink = "";
                }
                else if (userType == "4")
                {
                    ltLeftSetting.Text = "<a href=\"/account/user/\">Settings</a>";
                    customerLink = "<a href=\"/account/user/\">Customers</a>";
                    storeOwnerLink = "";
                }
            }
            ltLeftNav.Text = storeOwnerLink + customerLink;            
        }       

        private DataSet getDSdataSet()
        {
            int i = -1;
            int arrayLength = 2;

            string[,] inputSQL = new string[arrayLength, 4]; //inputSQL (sqlID, sqlText, commandtype, tableName)
            string[,] inputParams = new string[0, 3]; //inputParams (sqlID, paramName, paramValue) 
            string[] outputString; //outputString (outputMessages)
            DataSet DSdataSet; //multi table dataset 

            i++; inputSQL[i, 0] = i.ToString();
            inputSQL[i, 1] = "SELECT * From " + cPreFix + "cat WHERE isLive=1 AND isLeftNav=1";
            inputSQL[i, 2] = "SQL";
            inputSQL[i, 3] = "tblSideNav";

            i++; inputSQL[i, 0] = i.ToString();
            inputSQL[i, 1] = "SELECT TOP 1 * From " + cPreFix + "store WHERE uID=" + userID.ToString();
            inputSQL[i, 2] = "SQL";
            inputSQL[i, 3] = "tblStore";

            sqlData objDAL = new sqlData();
            DSdataSet = objDAL.getDataset(inputSQL, inputParams, out outputString);

            userMsg = outputString[0].ToString();
            sysMsg = outputString[1].ToString();
            return DSdataSet;
        }
    }
}