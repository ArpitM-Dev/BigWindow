using megaUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace storeSocial
{
    public class store
    {
        public int storeID { get; set; }
        public int uID { get; set; }
        public string storeLogo { get; set; }
        public string storeTagLine { get; set; }
        public string storeSmDesc { get; set; }
        public string storeMenu { get; set; }
        public string storeMenuDetail { get; set; }
        public string storeAddress { get; set; }
        public int storeLocation { get; set; }
        public string storeCity { get; set; }
        public string storeState { get; set; }
        public int storeCountry { get; set; }
        public string storeEmail { get; set; }
        public string storeWebsite { get; set; }
        public string storePhone1 { get; set; }
        public string storePhone2 { get; set; }
        public bool isLoyaltyCard { get; set; }
        public string listOfLoyaltyCards { get; set; }
        public int parkingFacilityAvailable { get; set; }
        public string parkingInfo { get; set; }
        public bool mondayOpen { get; set; }
        public DateTime mondayOpeningTime { get; set; }
        public DateTime mondayClosingTime { get; set; }
        public bool tuesdayOpen { get; set; }
        public DateTime tuesdayOpenTime { get; set; }
        public DateTime tuesdayClosingTime { get; set; }
        public bool wednesdayOpen { get; set; }
        public DateTime wednesdayOpenTime { get; set; }
        public DateTime wednesdayClosingTime { get; set; }
        public bool thursdayOpen { get; set; }
        public DateTime thursdayOpenTime { get; set; }
        public DateTime thursdayClosingTime { get; set; }
        public bool fridayOpen { get; set; }
        public DateTime fridayOpenTime { get; set; }
        public DateTime fridayClosingTime { get; set; }
        public bool saturdayOpen { get; set; }
        public DateTime saturdayOpenTime { get; set; }
        public DateTime saturdayClosingTime { get; set; }
        public bool sundayOpen { get; set; }
        public DateTime sundayOpenTime { get; set; }
        public DateTime sundayClosingTime { get; set; }
        public string storeName { get; set; }
        
        public string storeCategoryList { get; set; }
        public string paymentMethodList { get; set; }

        public int sqlID { get; set; } public int maxSqlFields { get; set; } public string[,] fieldsArray { get; set; } public int varTopIndex { get; set; }
        public string template { get; set; } public string seo { get; set; } public string outPut { get; set; } public string sysErr { get; set; } public int currPage { get; set; } public int pageSize { get; set; } public int totalNum { get; set; } public int topResults { get; set; } public string[,] varItems { get; set; } public string cPreFix { get; set; } public DataSet megaDataSet { get; set; }

        public store() { assignVarValues(); }

        public void getData(store obj) { getDS(obj); if (obj.sysErr == "") printData(obj); }

        public void printData(store obj)
        {
            //if (obj.template == "") obj.template = obj.megaDataSet.Tables["tblTemplate"].Rows[0][0].ToString(); 
            getFormat(obj);

            if (obj.megaDataSet.Tables["tblStore"].Rows.Count>0)
            {
                DataRow DR = obj.megaDataSet.Tables["tblStore"].Rows[0]; 
                string[,] objVars = new string[,] { 
                    { "[storeID]", DR["storeID"].ToString() },
                    { "[storeName]", DR["storeName"].ToString() },
                    { "[storeLogo]", DR["storeLogo"].ToString() },
                    { "[storeTagLine]", DR["storeTagLine"].ToString() },
                    { "[storeSmDesc]", DR["storeSmDesc"].ToString() },
                    { "[megaRating]", getRating() },
                    { "[storeCategoryList]", DR["storeCategoryList"].ToString() },
                    { "[storeAddress]", getStoreAddress(DR) },
                    { "[storePhone1]", DR["storePhone1"].ToString() },
                    { "[storePhone2]", ((DR["storePhone2"].ToString()=="")?"":", " +DR["storePhone2"].ToString()) },
                    { "[paymentMethodList]", getPayMethodList(obj) },
                    { "[storeWebsite]", DR["storeWebsite"].ToString() },
                    { "[parkingFacilityAvailable]", (DR["parkingFacilityAvailable"].ToString().ToLower() == "true") ? "Yes" : "No" },
                    { "[parkingInfo]", DR["parkingInfo"].ToString() },
                    { "[storeMenu]", DR["storeMenu"].ToString() },
                    { "[lastUpdated]", convertUtilities.stringToDate(DR["lastUpdated"].ToString()).ToString("dd/MM/yyyy") },
                    { "[storeAlbum]", getStoreAlbum(obj) },
                    { "[storeOpenDays]", getStoreOpenDays(DR) },
                    { "[storeCity]", DR["storeCity"].ToString() },
                    { "[storeState]", DR["storeState"].ToString() },
                    { "[storeTagLine]", DR["storeTagLine"].ToString() },
                    { "[subscribeLink]", getSubscribeLink(storeID, Convert.ToInt32(DR["userSub"])) },
                    { "[storeMsgBtn]", genFunc.getLoginMsg("storeMsg", "jazTipSubscribe", DR["storeEmail"].ToString(), "0", "0", 0) },
                    { "[storeTagLine]", DR["storeTagLine"].ToString() }
                };
                obj.outPut = genFunctions.replaceVars(objVars, obj.template);
            }            
        }

        private string getPayMethodList(store obj)
        {
            string rtnStr = "";
            DataTable DT = obj.megaDataSet.Tables["tblPayMethod"];
            for (int i = 0; i < DT.Rows.Count; i++ )
            {
                if (i > 0) rtnStr += ", ";
                rtnStr += DT.Rows[i]["paymentMethName"].ToString();
            }
            return rtnStr;
        }

        private string getSubscribeLink(int storeID, int userSub)
        {
            string subscribeOnClick = " onclick=\"subScribe('substrPr'," + storeID.ToString() + ");\" ";
            string rtnStr = genFunc.getLoginMsg("subscribe", "jazTipSubscribe", subscribeOnClick, "strPr", storeID.ToString(), userSub);
            return rtnStr;
        }

        private string getStoreOpenDays(DataRow DR)
        {
            string rtnStr = "";
            rtnStr += "<div class=\"col-xs-4\" style=\"padding-left:7px;\">";
            rtnStr += (DR["mondayOpen"].ToString().ToLower() == "true") ? "Mon : " + convertUtilities.stringToDate(DR["mondayOpeningTime"].ToString()).ToString("hh:mm tt").ToString() + "-" + convertUtilities.stringToDate(DR["mondayClosingTime"].ToString()).ToString("hh:mm tt").ToString() : "Mon : Closed";
            rtnStr += "</div>";

            rtnStr += "<div class=\"col-xs-4\">";
            rtnStr += (DR["thursdayOpen"].ToString().ToLower() == "true") ? "Thu : " + convertUtilities.stringToDate(DR["thursdayOpenTime"].ToString()).ToString("hh:mm tt").ToString() + "-" + convertUtilities.stringToDate(DR["thursdayClosingTime"].ToString()).ToString("hh:mm tt").ToString() : "Thu : Closed";
            rtnStr += "</div>";

            rtnStr += "<div class=\"col-xs-4\">";
            rtnStr += (DR["sundayOpen"].ToString().ToLower() == "true") ? "Sun : " + convertUtilities.stringToDate(DR["sundayOpenTime"].ToString()).ToString("hh:mm tt").ToString() + "-" + convertUtilities.stringToDate(DR["sundayClosingTime"].ToString()).ToString("hh:mm tt").ToString() : "Sun : Closed";
            rtnStr += "</div>";

            rtnStr += "<div class=\"col-xs-4\"  style=\"padding-left:7px;\">";
            rtnStr += (DR["tuesdayOpen"].ToString().ToLower() == "true") ? "Tue : " + convertUtilities.stringToDate(DR["tuesdayOpenTime"].ToString()).ToString("hh:mm tt").ToString() + "-" + convertUtilities.stringToDate(DR["tuesdayClosingTime"].ToString()).ToString("hh:mm tt").ToString() : "Tue : Closed";
            rtnStr += "</div>";

            rtnStr += "<div class=\"col-xs-4\">";
            rtnStr += (DR["fridayOpen"].ToString().ToLower() == "true") ? "Fri : " + convertUtilities.stringToDate(DR["fridayOpenTime"].ToString()).ToString("hh:mm tt").ToString() + "-" + convertUtilities.stringToDate(DR["fridayClosingTime"].ToString()).ToString("hh:mm tt").ToString() : "Fri : Closed";
            rtnStr += "</div>";

            rtnStr += "<div class=\"col-xs-4\"> &nbsp; ";
            rtnStr += "</div>";

            rtnStr += "<div class=\"col-xs-4\" style=\"padding-left:7px;\">";
            rtnStr += (DR["wednesdayOpen"].ToString().ToLower() == "true") ? "Wed : " + convertUtilities.stringToDate(DR["wednesdayOpenTime"].ToString()).ToString("hh:mm tt").ToString() + "-" + convertUtilities.stringToDate(DR["wednesdayClosingTime"].ToString()).ToString("hh:mm tt").ToString() : "Wed : Closed";
            rtnStr += "</div>";

            rtnStr += "<div class=\"col-xs-4\">";
            rtnStr += (DR["saturdayOpen"].ToString().ToLower() == "true") ? "Sat : " + convertUtilities.stringToDate(DR["saturdayOpenTime"].ToString()).ToString("hh:mm tt").ToString() + "-" + convertUtilities.stringToDate(DR["saturdayClosingTime"].ToString()).ToString("hh:mm tt").ToString() : "Sat : Closed";
            rtnStr += "</div>";
            
            return rtnStr;

        }

        private string getStoreAlbum(store obj)
        {
            string rtnStr = "";
            rtnStr += "<div class=\"col-xs-12 noPadding tm20\">";
            DataTable tblAlbum = obj.megaDataSet.Tables["tblAlbum"];
            if (tblAlbum.Rows.Count > 0)
            {
                DataRow DR;
                string allAlbums = "";
                for (int i = 0; i < tblAlbum.Rows.Count; i++)
                {
                    DR = tblAlbum.Rows[i];
                    allAlbums += DR["albumID"].ToString() + ",";
                    if ((i == 0 || i == 1) || (i == 2 && tblAlbum.Rows.Count == 3))
                        rtnStr += "<div class=\"col-xs-4 noPadding\"><div class=\"col-xs-12 albumImg\"><img src=\"/getThumb.aspx?width=225&img=img/cImg/" + DR["thumb"].ToString() + "\" alt=\"\" class=\"darkBorderAll\" /></div><div class=\"col-xs-12 text-center\" style=\"border:1px solid #f1f1f1;\"><a href=\"javascript:void(0);\" onclick = \"getAlbum(" + DR["albumID"].ToString() + ")\">" + DR["albumName"].ToString() + "</a></div></div>";

                    if (tblAlbum.Rows.Count > 3 && i == 2)
                        rtnStr += "<div class=\"col-xs-4 albumImg\"><div class=\"col-xs-12 moreAlbum\"><a href=\"javascript:void(0);\" onclick = \"getAlbums('[allAlbums]')\">View All<br />Albums</a></div></div>";

                }
                rtnStr = rtnStr.Replace("[allAlbums]", allAlbums);
            }
            else rtnStr += "<div class=\"col-xs-4 albumImg\"><div class=\"col-xs-12 moreAlbum\">No albums added by store</div></div>";
            rtnStr += "</div>";
            return rtnStr;
        }

        private string getStoreAddress(DataRow DR)
        {
            string rtnStr = DR["storeAddress"].ToString();
            if (DR["storeCity"].ToString() != "") rtnStr += ", " + DR["storeCity"].ToString();
            if (DR["storeState"].ToString() != "") rtnStr += ", " + DR["storeState"].ToString();
            if (DR["country_Name"].ToString() != "") rtnStr += ", " + DR["country_Name"].ToString();
            return rtnStr;
        }

        private string getRating()
        {
            return "<div style=\"float:left; \"><img src=\"/img/star.png\" alt=\"\" /></div><div style=\"float:left; padding:3px 0 0 6px;\">320 Ratings, <a href=\"[addRatingLink]\">Add your Ratings</a></div>";
        }

        internal void getDS(store obj)
        {
            int i = -1, j = -1, arrayLength = 3, paramLength = 0; sqlConnectivity sql = new sqlConnectivity(); sql.inputSQL = new string[arrayLength, 4]; sql.inputParams = new string[paramLength, 3];
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblStore"; sql.inputSQL[i, 1] = getStoreSQL(obj);
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblAlbum"; sql.inputSQL[i, 1] = "SELECT TOP 4 *, thumb =(SELECT TOP 1 imgName from bw_user_img WHERE albumID=A.albumID) FROM bw_user_img_album A WHERE uID=(SELECT TOP 1 uID FROM bw_store WHERE storeID=" + storeID.ToString() + ")";
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblPayMethod"; sql.inputSQL[i, 1] = "SELECT * FROM bw_storePayMeth A LEFT OUTER JOIN bw_paymentMethod B ON A.paymentMethID=B.paymentMethID WHERE storeID=" + storeID.ToString();            
            sql.getData();
            //HttpContext.Current.Response.Write(sql.inputSQL[i, 1]); HttpContext.Current.Response.End();
            if (sql.sysErr == "") obj.megaDataSet = sql.megaDataSet; else obj.sysErr = sql.sysErr;
        }

        private string getStoreSQL(store obj)
        {
            string rtnStr = "";
            rtnStr = "SELECT TOP 1 *";
            rtnStr += " , userSub=(SELECT count(*) from bw_post_subscribe WHERE storeID=" + obj.storeID.ToString() + " AND uID=" + postUtilities.getIntFrmSession("userID").ToString() + ")";
            rtnStr += " , storeCategoryList = STUFF((SELECT ', ' + cName FROM bw_store_cat WHERE cID IN (SELECT cID FROM bw_store_cat_rel WHERE storeID=S.storeID) FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(max)'), 1, 1, '')";
            rtnStr += " FROM " + cPreFix + "store S";
            rtnStr += " LEFT OUTER JOIN " + cPreFix + "user_country C ON C.countryID = S.storeCountry";
            rtnStr += " WHERE storeID=" + obj.storeID.ToString();
            //HttpContext.Current.Response.Write(rtnStr);
            return rtnStr;
        }

        internal void getImagesFromAlbum(store obj)
        {
            getFormat(obj);
            int i = -1, j = -1, arrayLength = 1, paramLength = 0; sqlConnectivity sql = new sqlConnectivity(); sql.inputSQL = new string[arrayLength, 4]; sql.inputParams = new string[paramLength, 3];
            i++; sql.inputSQL[i, 0] = i.ToString(); sql.inputSQL[i, 2] = "SQL"; sql.inputSQL[i, 3] = "tblImages"; sql.inputSQL[i, 1] = "select * from bw_user_img I LEFT OUTER JOIN bw_user_img_album a on A.albumID=I.albumID WHERE I.albumID=" + postUtilities.getIntFrmQString("albumid");
            //HttpContext.Current.Response.Write(sql.inputSQL[i, 1]);
            sql.getData(); if (sql.sysErr == "")
            {
                obj.megaDataSet = sql.megaDataSet; obj.varTopIndex = (obj.varItems.Length / 2) - 1;
                if (obj.megaDataSet.Tables["tblImages"].Rows.Count > 0) obj.outPut = obj.template.Replace("[albumName]", obj.megaDataSet.Tables["tblImages"].Rows[0]["albumName"].ToString()).Replace(obj.varItems[obj.varTopIndex, 1], getAlbumImageList(obj)); 
                else obj.outPut = "";
            }
            else obj.sysErr = sql.sysErr;

        }

        private string getAlbumImageList(store obj)
        {
            string rtnStr = "", itemStr = ""; DataRow DR; string[,] objVars;
            DataTable DT = obj.megaDataSet.Tables["tblImages"];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DR = DT.Rows[i];
                objVars = new string[,] { 
                    { "[imgName]", DR["imgName"].ToString() }
                };
                itemStr = genFunctions.replaceVars(objVars, obj.varItems[obj.varTopIndex, 0]);
                rtnStr += itemStr;
            }
            return rtnStr;
        }

        private void assignVarValues() { maxSqlFields = 42; cPreFix = System.Configuration.ConfigurationManager.AppSettings["cPreFix"]; currPage = postUtilities.getIntFrmQString("p"); if (currPage == 0) currPage = 1; pageSize = 30; topResults = 0; megaDataSet = new DataSet(); sysErr = outPut = ""; varItems = new string[0, 0]; }
        public void resetValues(store obj) { int i = 0; foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj)) { if (i >= 0 && i < obj.maxSqlFields) { if (prop.PropertyType.ToString().ToLower().Contains("int")) prop.SetValue(obj, 0); if (prop.PropertyType.ToString().ToLower().Contains("string")) prop.SetValue(obj, ""); if (prop.PropertyType.ToString().ToLower().Contains("decimal")) prop.SetValue(obj, Convert.ToDecimal(0)); if (prop.PropertyType.ToString().ToLower().Contains("date")) prop.SetValue(obj, DateTime.Today); if (prop.PropertyType.ToString().ToLower().Contains("bool")) prop.SetValue(obj, true); } i++; } }
        public void assignArray(store obj) { obj.fieldsArray = new string[obj.maxSqlFields, 3]; int i = 0; foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj)) { if (i >= 0 && i < obj.maxSqlFields) { obj.fieldsArray[i, 0] = obj.sqlID.ToString(); obj.fieldsArray[i, 1] = prop.Name; if (prop.PropertyType.ToString().ToLower().Contains("int") || prop.PropertyType.ToString().ToLower().Contains("string") || prop.PropertyType.ToString().ToLower().Contains("date") || prop.PropertyType.ToString().ToLower().Contains("bool")) obj.fieldsArray[i, 2] = prop.GetValue(obj).ToString(); if (prop.PropertyType.ToString().ToLower().Contains("decimal")) obj.fieldsArray[i, 2] = ((Decimal)prop.GetValue(obj)).ToString("0.00"); } i++; } }
        public void fillDataFromDataRow(store obj, DataRow DR) { int i = 0; foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj)) { if (i >= 0 && i < obj.maxSqlFields) { if (prop.PropertyType.ToString().ToLower().Contains("int")) prop.SetValue(obj, convertUtilities.stringToInt(DR[prop.Name].ToString())); if (prop.PropertyType.ToString().ToLower().Contains("string")) prop.SetValue(obj, DR[prop.Name].ToString()); if (prop.PropertyType.ToString().ToLower().Contains("decimal")) prop.SetValue(obj, convertUtilities.stringToDecimal(DR[prop.Name].ToString())); if (prop.PropertyType.ToString().ToLower().Contains("date")) prop.SetValue(obj, convertUtilities.stringToDate(DR[prop.Name].ToString())); if (prop.PropertyType.ToString().ToLower().Contains("bool")) prop.SetValue(obj, convertUtilities.stringToBool(DR[prop.Name].ToString())); } i++; } }
        private void getFormat(store obj) { string rtnTemplate = ""; for (int i = 0; i < obj.varItems.Length / 2; i++) { obj.varItems[i, 0] = genFunctions.getItemFormat(obj.varItems[i, 1], obj.template, out rtnTemplate); obj.template = rtnTemplate; } }

    }
}