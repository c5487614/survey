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

public partial class MediRecord_deptView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        //if (Session["power"] == null) Response.Redirect("../Default.aspx");
        //if (!srrshPage.Level2(Session["power"].ToString())) Response.Redirect("../Err/powerNeed.aspx");//没权限

        string date = DateTime.Now.ToString("yyyy-MM-dd");
        tboxdate.Text = date;
        Button1_Click(sender,e);

    }
    private void getBrifeBind(string dept,string date)
    {
        try
        {
            DateTime date1, date2;
            string sdept = "1";
            int year, month;
            year = Convert.ToInt16(date.Split('-')[0]);
            month = Convert.ToInt16(date.Split('-')[1]);
            date1 = new DateTime(year, month, 1);
            date2 = new DateTime(year, month, 30);
            MediTable mt = new MediTable();
            mt.getBrifeInfo(sdept, date1.ToString("yyyy-MM-dd"), date2.ToString("yyyy-MM-dd"), GridView1);
            mt.getDetailInfo(sdept, date1.ToString("yyyy-MM-dd"), date2.ToString("yyyy-MM-dd"), GridView2);
        }
        catch (Exception ex)
        {
			Response.Write(ex.ToString());
            //Response.Redirect("../Err/nonePage.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string date = tboxdate.Text;
        getBrifeBind("测试", date);
    }
}
