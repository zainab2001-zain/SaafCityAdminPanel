using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication5
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //String activepage = Request.RawUrl; if (activepage.Contains("Home.aspx"))
            //{
            //    string thisURL = this.Page.GetType().Name.ToString();
            //    switch (thisURL)
            //    {
            //        case "Home.aspx":
            //            home.Attributes.Add("class", "nav-link active");
            //            break;
            //        case "Vendor.aspx":
            //            vendor.Attributes.Add("class", "nav-link");
            //            break;
            //    }
            //}
        }
        public Label labusername
        {
            get
            {
                return this.UsernameDisplayLabel;
            }
        }
    }
}