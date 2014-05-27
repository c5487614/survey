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

public partial class Patient_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        //if (Session["power"] == null) Response.Redirect("../Default.aspx");
        //if (!srrshPage.Level2(Session["power"].ToString())) Response.Redirect("../Err/powerNeed.aspx");//没权限
		//Added by Chunhui Chen 2014-04-29
		initUser();

        SqlConnect conn = new SqlConnect();
        string sql = "select * from JCI_generateTable where tablerdn=12 order by largeItemRdn ASC, SortId ASC";
        conn.gv_bind(sql, GridView1);

		sql = "select code,name from JCI_patient_dic where dicType='科室' order by orderNum ASC ";
		conn.DropDownListBind(sql, ddl_dept);

		sql = "select code,name from JCI_patient_dic where dicType='楼层' order by orderNum ASC ";
		conn.DropDownListBind(sql, ddl_floor);
		tbox_reportDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }
	private void initUser()
	{
		if (Session["user"] == null)
		{
			Response.Redirect("../Admin_login.aspx");
			return;
		}
		UserPower u = Session["user"] as UserPower;
		if (u == null)
		{
			Response.Redirect("../Admin_login.aspx");
			return;
		}
		if (u.IsAdmin())
		{
			hf_hidden_test.Value = "true";
			btn_manage.Visible = true;
		}
		else
		{
			hf_hidden_test.Value = "false";
			btn_manage.Visible = false;
		}
	}
    private string getInfo(CheckBox[] cbs)
    {
        foreach (CheckBox cb in cbs)
        {
            if (cb.Checked) return cb.Text;
        }
        return "";
    }
	protected void btn_manage_Click(object sender, EventArgs e)
	{
		Response.Redirect("Manage/Default.aspx");
	}
    protected void Button1_Click1(object sender, EventArgs e)
    {
		if (hf_hidden_test.Value.Equals("false"))
		{
			//没有权限
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('没有权限');</script>");
			return;
		}
        try
        {
            CheckBox[] cbs = new CheckBox[4];
            cbs[0] = cbself;
            cbs[1] = cbfriend;
            cbs[2] = cbfamily;
			cbs[3] = cb_fillperson_absent;
            string sfillperson = getInfo(cbs);
			cbs = new CheckBox[3];
            cbs[0] = cbmale;
            cbs[1] = cbfemale;
			cbs[2] = cbsex_absent;
            string sPsex = getInfo(cbs);
            cbs[0] = cbonce;
            cbs[1] = cbnotonce;
			cbs[2] = cbonce_absent;
            string sIsFirst = getInfo(cbs);
            string sregDate = regDate.Text.Trim();
            string sregDept = ddl_dept.SelectedValue;//dept.SelectedValue;
			string sfloor = ddl_floor.SelectedValue;
			CheckBox[] cbjobs = new CheckBox[7];
			cbjobs[0] = cbox_job1;
			cbjobs[1] = cbox_job2;
			cbjobs[2] = cbox_job3;
			cbjobs[3] = cbox_job4;
			cbjobs[4] = cbox_job5;
			cbjobs[5] = cbox_job6;
			cbjobs[6] = cbox_job_absent;
			string sjob = getInfo(cbjobs);//jobs.SelectedValue;
			CheckBox[] cbpays = new CheckBox[6];
			cbpays[0] = cbox_pay1;
			cbpays[1] = cbox_pay2;
			cbpays[2] = cbox_pay3;
			cbpays[3] = cbox_pay4;
			cbpays[4] = cbox_pay_absent;
			string spayType = getInfo(cbpays);
			CheckBox[] cbother1 = new CheckBox[5];
			cbother1[0] = cbox_mzh;
			cbother1[1] = cbox_jz;
			cbother1[2] = cbox_qtyyzr;
			cbother1[3] = cbox_qt;
			cbother1[4] = cbox_zyqk_absent;
			string sother1 = getInfo(cbother1);
			string sother2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			//sother2 is for report
			//sreportDate stored in regDept field to clarify the report date
			string sreportDate = tbox_reportDate.Text;
			System.Text.StringBuilder sbpromote = new System.Text.StringBuilder();
			System.Text.StringBuilder sbneedimprove = new System.Text.StringBuilder();
			sbpromote.Append("医疗:").Append(tbox_promote_yl.Text).Append(";");
			sbpromote.Append("护理:").Append(tbox_promote_hl.Text).Append(";");
			sbpromote.Append("医技:").Append(tbox_promote_yj.Text).Append(";");
			sbpromote.Append("后勤:").Append(tbox_promote_hq.Text).Append(";");
			sbpromote.Append("住院:").Append(tbox_promote_zy.Text).Append(";");

			sbneedimprove.Append("医疗:").Append(tbox_needImprove_yl.Text).Append(";");
			sbneedimprove.Append("护理:").Append(tbox_needImprove_hl.Text).Append(";");
			sbneedimprove.Append("医技:").Append(tbox_needImprove_yj.Text).Append(";");
			sbneedimprove.Append("后勤:").Append(tbox_needImprove_hq.Text).Append(";");
			sbneedimprove.Append("住院:").Append(tbox_needImprove_zy.Text).Append(";");
            PatientTable pt = new PatientTable();
			string result = pt.saveBrifeInfo(sfillperson, sIsFirst, sPsex, sregDate, sreportDate, sjob, sother1, sother2, HiddenField1.Value, spayType, sregDept, sfloor, sbpromote.ToString(), sbneedimprove.ToString(), GridView1);
            if (result == "ok")
            {
				Response.Redirect("Default.aspx", false);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('添加成功');window.location='../tableList.aspx'</script>");
            }
        }
        catch (Exception ex)
        {
			Response.Write(ex.ToString());
            //Response.Redirect("../Err/nonePage.aspx");
        }
    }
	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{

		//HtmlControl c = e.Row.FindControl("row_div") as HtmlControl;
		//if (c == null)
		//{
		//    return;
		//}
		////c = e.Row.TemplateControl.FindControl("row_div") as HtmlControl;
		//DataRowView drv = e.Row.DataItem as DataRowView;
		//if (drv.Row["largeItemRdn"].ToString().Equals("1"))
		//{
		//    c.Style.Add("background-color", "#eee");
		//}
		
	}
}
