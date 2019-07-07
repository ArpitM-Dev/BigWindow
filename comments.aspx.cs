using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using megaSoftwaresUtilities;

namespace storeSocial
{
    public partial class comments : System.Web.UI.Page
    {
        int postID, uID; string comment;
        protected void Page_Load(object sender, EventArgs e)
        {
            postID = postUtilities.getIntFrmQString("postID");
            comment = postUtilities.getStringFrmQString("comment");
            uID = postUtilities.getIntFrmSession("userID");
            if (postID > 0 && uID > 0 && comment != "") insertComment();
        }

        private void insertComment()
        {
            sqlData objDAL = new sqlData();

            int i = -1; int arrayLength = 1;
            string[,] inputParams = new string[arrayLength, 2];
            string[] outputString; //outputString (outputMessages)

            string megaSQL = "INSERT INTO bw_post_comment (uID, postID, commentText) VALUES (" + uID.ToString() + ", " + postID.ToString() + ",'" + comment + "'); SELECT @@IDENTITY;";

            i++; inputParams[i, 0] = megaSQL;
            inputParams[i, 1] = "";

            string strID = objDAL.ExecuteScalar(inputParams, out outputString);
            Response.ContentType = "text/plain";
            Response.Clear();
            Response.Write(strID);
        }
    }
}