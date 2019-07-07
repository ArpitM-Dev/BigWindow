using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace storeSocial
{
    public partial class getStoreAlbum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            store obj = new store(); obj.resetValues(obj);
            obj.varItems = new string[,] { { "", "[megaItem]" } }; //[0, 0] [0, 1]
            obj.template = albumTemplate.InnerHtml;
            obj.getImagesFromAlbum(obj);
            albumTemplate.InnerHtml = obj.outPut;
        }
    }
}