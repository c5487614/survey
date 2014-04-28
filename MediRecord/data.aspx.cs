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

public partial class MediRecord_data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Request.QueryString["dept"] == null) return;
        //if (Request.QueryString["date"] == null) return;

        //if (Session["power"] == null) Response.Redirect("../Default.aspx");
        //if (!srrshPage.Level2(Session["power"].ToString())) Response.Redirect("../Err/powerNeed.aspx");//没权限

        //string dept = Request.QueryString["dept"].ToString();
        //string date = Request.QueryString["date"].ToString();
       string dept = "1";
        string date = "2010-06-14";
        int year, month, days;
        year = Convert.ToInt16(date.Split('-')[0]);
        month = Convert.ToInt16(date.Split('-')[1]);
        days = Convert.ToInt16(date.Split('-')[2]);
        DateTime date1 = new DateTime(year, month,1);
        DateTime date2 = new DateTime(year, month, 30);
        date1.ToString("yyyy-MM-dd");
        SqlConnect conn = new SqlConnect();
        //string sql = "select doc1,doc2,doc3,doc4,doc5,fellows,tableScore from brifeInfo where docDept='" + dept + "'";
        string sql = "select doc1,doc2,doc3,doc4,doc5,fellows,tableScore from JCI_brifeInfo where docDept='"+dept+"' and convert(varchar(10),fillindate,120)>='" + date1.ToString("yyyy-MM-dd") + "' and convert(nvarchar(10),fillindate,120)<='" + date2.ToString("yyyy-MM-dd") + "'";
        DataTable dt = conn.ExcuteSelect(sql);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.Text.StringBuilder docs = new System.Text.StringBuilder();
        System.Text.StringBuilder score = new System.Text.StringBuilder();
        docs.Append("<docs>");
        score.Append("<scores>");
        sb.Append(@"<?xml version='1.0' encoding='utf-8' ?>");
        sb.Append("<datas>");
        foreach (DataRow dr in dt.Rows)
        {
            if (dr[0].ToString() != "") docs.Append(dr[0].ToString());
            if (dr[1].ToString() != "") docs.Append("," + dr[1].ToString());
            if (dr[2].ToString() != "") docs.Append("," + dr[2].ToString());
            if (dr[3].ToString() != "") docs.Append("," + dr[3].ToString());
            if (dr[4].ToString() != "") docs.Append("," + dr[4].ToString());
            if (dr[5].ToString() != "") docs.Append("/" + dr[5].ToString());
            docs.Append(";");
            if (dr[6].ToString() != "") score.Append(dr[6].ToString());
            score.Append(";");
        }
        string getdoc = docs.ToString().Substring(0, docs.Length - 1) + "</docs>";
        string getscore = score.ToString().Substring(0, score.Length - 1) + "</scores>";
        sb.Append(getdoc + getscore);
        sb.Append("</datas>");
        Response.Charset = "utf-8";
        Response.ContentType = "text/xml";
        Response.Write(sb.ToString());

    }
}
