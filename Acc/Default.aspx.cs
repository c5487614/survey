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

public partial class Acc_Default : System.Web.UI.Page
{
    public bool IsDynamicLoadControl
    {
        set
        {
            ViewState["IsDynamicLoadControl"] = value;
        }
        get
        {
            object Dynamic = ViewState["IsDynamicLoadControl"];
            return Dynamic == null ? false : true;
        }
    }
    public static DataTable _dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        SqlConnect conn = new SqlConnect();
        DataTable dt = new DataTable();
        accTable at = new accTable();
        string tablerdn = "1";
        Inital(ref tablerdn);

        dt = conn.ExcuteSelect("select * from dbo.JCI_report_mainTable where tableRdn=" + tablerdn);
        _dt = dt;

        at.generator(dt, GridView1);
        IsDynamicLoadControl = true;
    }
    protected override void LoadViewState(object savedState)
    {
        base.LoadViewState(savedState);
        if (IsDynamicLoadControl)
        {
            accTable ac = new accTable();
            ac.generator(_dt, GridView1);
        }
    }
    protected void btsubmit_Click(object sender, EventArgs e)
    {
        string stablerdn = "1";
        string stablename = "1";
        string sreportdept = "1";
        string sreportperson = "1";
        string spersonphone = "1";
        string sreportdate = "2010-1-1";
        string soccurtime = "1";
        string soweekday = "1";
        string spname = "1";
        string spage = "1";
        string spmrn = "1";
        string spssex = "1";
        string spoper = "1";
        string sptype = "1";//住院1  门诊2
        string sgothrough = "1";//经过
        string smeasure = "1";//措施
        string saddcontent = "1";

        string[] info = Request.QueryString["id"].ToString().Split(',');

        stablerdn = info[0];
        stablename = info[1];
        sreportdept = hDept.Text;
        sreportperson = hEmp.Text;
        spersonphone = hPhone.Text;
        sreportdate = hoccurDate.Text;
        soccurtime = hoccurTime.Text;
        spname = pName.Text;
        spage = ppage.Text;
        spmrn = pID.Text;
        spssex = cbSex1.Checked ? "男" : "女";
        spoper = pOper.Text;
        sptype = hcb1.Checked ? "住院病人" : "门诊病人";
        sgothrough = hDescribe.Text;
        smeasure = hMeasure.Text;
        saddcontent = addContent.Text;

        DateTime date = Convert.ToDateTime(sreportdate);
        accTable ac = new accTable();
        soweekday = ac.setWeekday(date);//星期
        if ("ok"==ac.saveBrifeInfo(stablerdn, stablename, sreportdept, sreportperson, spersonphone, sreportdate, soccurtime, soweekday, spname, spage, spmrn, spssex, spoper, sptype, sgothrough, smeasure, saddcontent, GridView1))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('上报成功！');window.location='../tableList.aspx';</script>");
        }
    }
    private void Inital(ref string tablerdn)
    {
        if (Request.QueryString["id"] == null) Response.Redirect("../tableList.aspx");
        if (Session["power"] == null) Response.Redirect("../Default.aspx");
        string[] power = Session["power"].ToString().Split(',');
        string[] info = Request.QueryString["id"].ToString().Split(',');
        Title = info[1];
        reportTitle.Text = Title;
        hEmp.Text = power[2];
        hPhone.Text = power[3].Split('.')[0];
        hDept.Text = power[1];
        tablerdn = info[0];
    }
}
