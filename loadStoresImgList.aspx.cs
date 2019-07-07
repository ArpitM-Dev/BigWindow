using megaSoftwaresUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace storeSocial
{
    public partial class loadStoresImgList : System.Web.UI.Page
    {
        string loc;
        protected void Page_Load(object sender, EventArgs e)
        {
            loc = postUtilities.getStringFrmQString("loc");
            if (!Page.IsPostBack) getStores();
        }

        private void getStores()
        {
            string megaSQL = "select TOP 10 storeImg = (storeLogo), storeID, storeName from bw_store S LEFT OUTER JOIN bw_store_loc L ON L.locID = S.storeLocation LEFT OUTER JOIN bw_user U ON U.uID=S.uID WHERE uAllowed=1 AND uVarified=1 ORDER BY newID();";
            int i = -1; int arrayLength = 1; string[] outputString; string[,] inputSQL = new string[arrayLength, 4]; string[,] inputParams = new string[0, 3];
            i++; inputSQL[i, 0] = i.ToString(); inputSQL[i, 1] = megaSQL; inputSQL[i, 2] = "SQL"; inputSQL[i, 3] = "tblJobList";
            sqlData objDAL = new sqlData(); DataSet DSdataSet = objDAL.getDataset(inputSQL, inputParams, out outputString);

            if (outputString[0].ToString() == "Success")
            {
                DataTable DT = DSdataSet.Tables[0]; DataRow DR;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (DT.Rows.Count > 0)
                {
                    string storeName = "";
                    for (int k = 0; k < DT.Rows.Count; k++)
                    {
                        storeName = DT.Rows[k]["storeName"].ToString(); if (storeName == "") storeName = "No name added";
                        sb.Append("<div class=\"col-xs-6 lessPadding\"><div class=\"col-xs-12 noPadding rightStoreImg\" style=\"border-top:5px solid #c9c9c9; background:#c9c9c9 url(/getThumb.aspx?width=120&img=img/cImg/" + DT.Rows[k]["storeImg"].ToString() + ") top center no-repeat; background-size:auto 75%\">")
                            .Append("<div class=\"rightStoreName\"><a style=\"padding:5px; display:block;\" href=\"/stores/" + DT.Rows[k]["storeID"].ToString() + "/" + urlUtilities.codeStrURL(DT.Rows[k]["storeName"].ToString()) + "/\">" + storeName + "</a></div></div></div>");
                    }
                }
                else sb.Append("No stores available");
                ltStoreList.Text = sb.ToString();
            }
            else { ltStoreList.Text = "No stores available"; }
        }
    }
}