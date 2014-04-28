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

public partial class Acc_Manage_accDetailInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        if (Request.QueryString["id"] == null) Response.Redirect("../../Err/nonePage.aspx");
        string tid = Request.QueryString["id"].ToString();
       


        accTable ac = new accTable();
        DataSet ds = ac.sDetailView(tid);
        init(ds.Tables[0]);
        ac.reGenerator(ds.Tables[1], GridView1);
    }
    private void init(DataTable dt)
    {
        DataRow dr=dt.Rows[0];
        string occurdate = dr["reportDate"].ToString();
        occurdate=occurdate.Replace('/', '-');
        reportTitle.Text =dr["tableName"].ToString();
        pName.Text = dr["pName"].ToString();
        if (dr["pSex"].ToString()=="男") cbSex1.Checked = true;
        else cbSex2.Checked = true;

        pID.Text = dr["pmrn"].ToString();
        ppage.Text = dr["page"].ToString();
        pOper.Text = dr["pOper"].ToString();
        hEmp.Text = dr["reportPerson"].ToString();
        hDept.Text = dr["reportDept"].ToString();
        hPhone.Text = dr["personPhone"].ToString();
        hoccurDate.Text = occurdate.Split(' ')[0];
        
        hoccurTime.Text = dr["occurTime"].ToString();

        if (dr["pType"].ToString().Equals("1")) hcb1.Checked = true;
        else hcb2.Checked = true;

        addContent.Text = dr["addContent"].ToString();
        hDescribe.Text = dr["goThrough"].ToString();
        hMeasure.Text = dr["measure"].ToString();
    }
    protected void btsubmit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || null == Session["power"]) Response.Write("你还没有登录！"); 

        string[] power = Session["power"].ToString().Split(',');
        accTable ac = new accTable();
        string person = power[2];
        string acDate = DateTime.Now.ToShortDateString();
        string reportId = Request.QueryString["id"].ToString();
        if (ac.accept(reportId, person, acDate))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('审核成功！');window.location='http://localhost/jci/tableList.aspx';</script>");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null || null == Session["power"]) Response.Write("你还没有登录！"); 

        //修改加审核代码  实现：先删除  再插入
        string[] power = Session["power"].ToString().Split(',');
        string reportId = Request.QueryString["id"].ToString();
        accTable acc = new accTable();
        string person = power[2];
        acc.AccDelete(reportId, person);

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
        if ("ok" == ac.saveBrifeInfo(stablerdn, stablename, sreportdept, sreportperson, spersonphone, sreportdate, soccurtime, soweekday, spname, spage, spmrn, spssex, spoper, sptype, sgothrough, smeasure, saddcontent, GridView1))
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('上报成功！');window.location='../tableList.aspx';</script>");
        }
    }
}
