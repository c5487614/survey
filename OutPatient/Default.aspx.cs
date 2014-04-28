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

public partial class OutPatient_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        if (Session["power"] == null) Response.Redirect("../Default.aspx");
        if (!srrshPage.Level2(Session["power"].ToString())) Response.Redirect("../Err/powerNeed.aspx");//没权限

        string sql = "select * from JCI_generateTable where tablerdn=13";
        SqlConnect conn = new SqlConnect();
        conn.gv_bind(sql, GridView1);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
