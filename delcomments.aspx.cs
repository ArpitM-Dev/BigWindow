using megaUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace storeSocial
{
    public partial class delcomments : System.Web.UI.Page
    {
        int cID;
        protected void Page_Load(object sender, EventArgs e)
        {
            cID = postUtilities.getIntFrmQString("cID");
            delcomm();
        }

        private void delcomm()
        {
            string megaSQL = "DELETE bw_post_comment WHERE commentID=" + cID.ToString();
            string sqlMessage = sqlFunctions.runSQLCommand(megaSQL);
        }
    }
}