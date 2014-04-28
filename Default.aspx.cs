using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string loginResult = "";
        try
        {
            // string suid = id.Text.Trim();
            string spsw = psw.Text.Trim();
            // if (suid == "" || spsw == "") return;
            // commonHis ch = new commonHis();
            // loginResult = ch.Login(suid, spsw);
            // Session["power"] = loginResult;
			if (spsw == "123456")
			{
				Response.Redirect("tableList.aspx", false);
			}

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
       
        
    }
}
