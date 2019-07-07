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
    public partial class loadStores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) getStores();
        }

        private void getStores()
        {
            string megaSQL = "SELECT cName, cID FROM bw_store_cat WHERE cName like '" + postUtilities.getStringFrmQString("storeLetter") + "%' ORDER BY cName";
            int i = -1; int arrayLength = 1; string[] outputString; string[,] inputSQL = new string[arrayLength, 4]; string[,] inputParams = new string[0, 3];
            i++; inputSQL[i, 0] = i.ToString(); inputSQL[i, 1] = megaSQL; inputSQL[i, 2] = "SQL"; inputSQL[i, 3] = "tblJobList";
            sqlData objDAL = new sqlData(); DataSet DSdataSet = objDAL.getDataset(inputSQL, inputParams, out outputString);

            if (outputString[0].ToString() == "Success")
            {
                DataTable DT = DSdataSet.Tables[0]; DataRow DR;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (DT.Rows.Count > 0)
                {
                    for (int k = 0; k < DT.Rows.Count; k++) sb.Append("<a href=\"javascript:void(0);\" onclick=\"applyCatValue('" + DT.Rows[k]["cID"].ToString() + "-" + urlUtilities.codeStrURL(DT.Rows[k]["cName"].ToString()) + "');\">" + DT.Rows[k]["cName"].ToString() + "</a>");
                }
                else sb.Append("No store categories available");
                ltStoreList.Text = sb.ToString();
            }
            else { ltStoreList.Text = "No store categories available"; }
        }
    }
}