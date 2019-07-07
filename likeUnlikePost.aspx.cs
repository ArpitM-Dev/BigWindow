using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaSoftwaresUtilities;

namespace storeSocial
{
    public partial class likeUnlikePost : System.Web.UI.Page
    {
        int postID, uLike, uID;
        protected void Page_Load(object sender, EventArgs e)
        {
            postID = postUtilities.getIntFrmQString("postID");
            uLike = postUtilities.getIntFrmQString("uLike");
            uID = postUtilities.getIntFrmSession("userID");
            if (postID > 0 && uID > 0) doLikeUnlike();
        }

        private void doLikeUnlike()
        {
            string megaSQL = "DELETE FROM bw_post_like WHERE uID = " + uID.ToString() + " AND postID =" + postID.ToString();
            if (uLike == 0) megaSQL = "INSERT INTO bw_post_like (uID, postID) VALUES (" + uID.ToString() + ", " + postID.ToString() + ")";
            string sqlMessage = genFunctions.runSQLCommand(megaSQL);            
        }
    }
}