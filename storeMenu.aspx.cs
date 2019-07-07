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
    public partial class storeMenu : System.Web.UI.Page
    {
        int storeID;
        protected void Page_Load(object sender, EventArgs e)
        {
            storeID = postUtilities.getIntFrmQString("storeid");
            if (storeID == 0) closeWindow(); else getMenu();
        }

        private void closeWindow()
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "parent.$.fancybox.close();", true);             
        }

        private void getMenu()
        {
            string cPreFix = System.Configuration.ConfigurationManager.AppSettings["cPreFix"]; 
            int i = -1; int arrayLength = 1; sqlConnectivity sql = new sqlConnectivity(); sql.inputSQL = new string[arrayLength, 4]; sql.inputParams = new string[0, 3];
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblStore"; sql.inputSQL[i, 1] = "SELECT * FROM " + cPreFix + "store WHERE storeID=" + storeID.ToString();
            sql.getData(); if (sql.sysErr == "")
            {
                DataTable tblStore = sql.megaDataSet.Tables["tblStore"];
                if (tblStore.Rows.Count > 0) 
                {
                    DataRow DR = tblStore.Rows[0];
                    ltMenuImage.Text = DR["storeMenuDetail"].ToString();
                    if (DR["storeMenu"].ToString() != "") ltMenuLink.Text = "<a href=\"/img/cImg/" + DR["storeMenu"].ToString() + "\">Download Catlog</a>";
                }
            }
            else ltMenuImage.Text = "Error Occured, please contact administrator";

        }
    }
}