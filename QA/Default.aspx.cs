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

public partial class QA_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        if (Request.QueryString["id"] == null) Response.Redirect("../Err/nonePage.aspx");
        //if (Session["power"] == null) Response.Redirect("../Default.aspx");
        //if(!srrshPage.Level2(Session["power"].ToString())) Response.Redirect("../Err/powerNeed.aspx");//没权限
        Title = Request.QueryString["id"].Split(',')[1].ToString();
        lbltitle.Text = "<h2>" + Title + "</h2>";
        SqlConnect conn = new SqlConnect();
        string sql = "select * from JCI_generateTable where tableRdn=" + Request.QueryString["id"].Split(',')[0].ToString() + Ftable.orderstring();
        conn.gv_bind(sql, GridView1);
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null) Response.Redirect("../Err/Err.aspx");
        QATable qat = new QATable();
        string scheckstair = checkStair.Text;
        string scheckperson = doc.Text;
        string scheckdate = checkdate.Text;
        string score = tableScore.Text;
        if (score == "") Response.Redirect("../Err/Err.aspx");
        
        string[] tableInfo = Request.QueryString["id"].ToString().Split(',');
        try
        {
            if("ok".Equals(qat.saveBrifeInfo(scheckperson, scheckdate, scheckstair, tableInfo[0], tableInfo[1], tableInfo[2], score,GridView1)))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('添加成功');window.location='../tableList.aspx'</script>");
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
}
