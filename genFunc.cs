using megaUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace storeSocial
{
    public class genFunc
    {
        internal static string getLoginMsg(string actionType, string returnClass, string onClick, string postID, string storeID, int isOn)
        {
            string returnString = "";
            int uID = postUtilities.getIntFrmSession("userID");
            if (actionType == "like")
            {
                if (uID == 0) returnString = "<a href=\"javascript:void(0);\" class=\"" + returnClass + "\">Like the deal</a>";
                else
                {
                    returnString = "<a href=\"javascript:void(0);\" " + onClick + " id=\"u" + postID + "\">";
                    if (isOn == 0) returnString += "Like the deal"; else returnString += "Unlike the deal";
                    returnString += "</a>";
                }
            }

            if (actionType == "comment")
            {
                if (uID == 0) returnString = "<a href=\"javascript:void(0);\" class=\"" + returnClass + "\">Comment</a>";
                else returnString = "<a href=\"javascript:void(0);\" id=\"comment" + postID + "\">Comment</a>";
            }

            if (actionType == "subscribe")
            {
                int userType = postUtilities.getIntFrmSession("uType");
                if (uID == 0 || userType == 5) returnString = "<a href=\"javascript:void(0);\" class=\"btn btn-primary btn-subscribe " + returnClass + "\">SUBSCRIBE &nbsp; <i class=\"fa fa-rss xlFont\"></i></a>";
                else
                {
                    returnString = "<a class=\"btn btn-primary btn-subscribe\" href=\"javascript:void(0);\" " + onClick + " id=\"sub" + postID + "\">";
                    if (isOn == 0) returnString += "SUBSCRIBE &nbsp; <i class=\"fa fa-rss xlFont\"></i>"; else returnString += "UN-SUBSCRIBE &nbsp; <i class=\"fa fa-rss xlFont\"></i>";
                    returnString += "</a>";
                }
            }

            if (actionType == "storeMsg")
            {
                int userType = postUtilities.getIntFrmSession("uType");
                if (uID == 0 || userType == 5) returnString = "<a href=\"javascript:void(0);\" class=\"btn btn-primary btn-subscribe jazTipMessage\">MESSAGE &nbsp; <i class=\"fa fa-comments-o xlFont\"></i></a>";
                else if (onClick=="") returnString = "<a class=\"btn btn-primary btn-subscribe\" href=\"javascript:alert('No email provided by store');\">MESSAGE &nbsp; <i class=\"fa fa-comments-o xlFont\"></i></a>";
                else returnString = "<a class=\"btn btn-primary btn-subscribe\" href=\"mailto:" + onClick + "\">MESSAGE &nbsp; <i class=\"fa fa-comments-o xlFont\"></i></a>";
            }

            return returnString;
        }


    }
}