using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaUtilities;

namespace storeSocial
{
    public partial class storeprofile : System.Web.UI.Page
    {
        int storeID;
        protected void Page_Load(object sender, EventArgs e)
        {
            storeID = postUtilities.getIntFromRoute("storeID");
            if (!Page.IsPostBack) getPage();
        }

        private void getPage()
        {
            if (storeID > 0)
            {
                store obj = new store(); obj.storeID = storeID; obj.template = storeDetails.InnerHtml; obj.getData(obj);
                if (obj.megaDataSet.Tables["tblStore"].Rows.Count > 0) storeDetails.InnerHtml = obj.outPut; else storeDetails.InnerHtml = "<div style=\"padding:300px 0 400px 0; text-align:center; font-size:18px;\">No Store Information Found</div>"; divSysErr.InnerHtml = obj.sysErr;                
                //storeMsgBtn.InnerHtml = 
            }
            else Response.Redirect("/");
        }
    }
}