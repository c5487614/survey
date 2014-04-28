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

public partial class AJAX_getDicForQA : System.Web.UI.Page
{
    private SqlConnect conn = new SqlConnect();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Request.QueryString["keys"] == null) return;

        string keys =Request.QueryString["keys"].ToString();
        getDicContent(keys);
    }

    private void getDicContent(string keys)
    {
        QATable qa = new QATable();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(@"<?xml version='1.0' encoding='UTF-8' ?>");
        sb.AppendLine(@"<contents>");
        string[] name = qa.getDicContent(keys);

        if (name == null)
        {
            return;//没有数据返回
        }

        foreach (string n in name)
        {
            if (n != "")
                sb.AppendLine(@"<content>" + n + "</content>");
        }
        sb.AppendLine(@"</contents>");
        Response.Charset = "utf-8";
        Response.ContentType = "text/xml";
        Response.Write(sb.ToString());
    }
}
