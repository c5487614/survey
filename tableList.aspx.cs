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

public partial class tableList : System.Web.UI.Page
{
    SqlConnect conn = new SqlConnect();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
	Session["power"]="666716,其它,何街浪,71031.管理员";
        if (Session["power"] == null) Response.Redirect("Default.aspx");
        
        conn.gv_bind("select * from JCI_tableList order by sortid", GridView1);

        u_init();
    }
    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {

        string tableType = e.CommandArgument.ToString().Split(',')[2];
        if (tableType == null) Response.Redirect("Err\nonpage.aspx");

        Response.Redirect(tableType + "\\Default.aspx?id=" + e.CommandArgument.ToString());
    }

    private void u_init()
    {
        string[] power = Session["power"].ToString().Split(',');
        string dept=power[1];
        string pow = power[3].Split('.')[1];
        try
        {
            accTable acc = new accTable();
            //0：未审核   1：已审核
            int notPass = acc.getIsPassedCount("0", dept,pow);
            int isPass = acc.getIsPassedCount("1", dept,pow);

            LblisPass.Text = "("+isPass.ToString()+")";
            LblnotPass.Text = "(" + notPass.ToString() + ")";
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        
    }
}
