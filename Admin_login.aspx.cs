using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Admin_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
    }
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
}
