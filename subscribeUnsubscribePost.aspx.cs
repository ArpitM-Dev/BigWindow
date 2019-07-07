using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaSoftwaresUtilities;

namespace storeSocial
{
    public partial class subscribeUnsubscribePost : System.Web.UI.Page
    {
        int storeID, isSub, uID;
        protected void Page_Load(object sender, EventArgs e)
        {
            storeID = postUtilities.getIntFrmQString("storeID");
            isSub = postUtilities.getIntFrmQString("issub");
            uID = postUtilities.getIntFrmSession("userID");
            if (storeID > 0 && uID > 0) doLikeUnlike();
        }

        private void doLikeUnlike()
        {
            string megaSQL = "DELETE FROM bw_post_subscribe WHERE uID = " + uID.ToString() + " AND storeID =" + storeID.ToString();
            if (isSub == 0) megaSQL += "INSERT INTO bw_post_subscribe (uID, storeID) VALUES (" + uID.ToString() + ", " + storeID.ToString() + ")";
            string sqlMessage = genFunctions.runSQLCommand(megaSQL);            
        }
    }
}