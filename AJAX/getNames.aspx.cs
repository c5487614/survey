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

public partial class MediRecord_getNames : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("dd");
        if (Request.QueryString["py"] == null) Response.Redirect("../Err/Err.aspx");
        getDocName(Request.QueryString["py"].ToString());
        //testNames();
    }
    private void getDocName(string py)
    {
        SqlConnect conn = new SqlConnect();
        string sql = "select top 8 empName from JCI_empTable where empName like '%" + py + "%'";
        DataTable dt = conn.ExcuteSelect(sql);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(@"<?xml version='1.0' encoding='UTF-8' ?>");
        sb.AppendLine(@"<names>");
        foreach (DataRow dr in dt.Rows)
        {
            if (!"".Equals(dr["empName"]))
                sb.AppendLine(@"<name>" + dr["empName"].ToString() + "</name>");
        }
        sb.AppendLine(@"</names>");
        Response.Charset = "utf-8";
        Response.ContentType = "text/xml";
        Response.Write(sb.ToString());
    }
    private void testNames()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(@"<?xml version='1.0' encoding='UTF-8' ?>");
        sb.AppendLine(@"<names>");
        for (int i = 0; i < 10; i++)
        {
            sb.AppendLine(@"<name>" + "c陈w" + "</name>");
        }
        sb.AppendLine(@"</names>");
        Response.Charset = "utf-8";
        Response.ContentType = "text/xml";
        Response.Write(sb.ToString());
    }
}
