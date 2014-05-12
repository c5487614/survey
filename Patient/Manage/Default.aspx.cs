using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Patient_Manage_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (IsPostBack) return;
        UserPower user = Session["user"] as UserPower;
        if (user == null)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>没有登录或会话失效，请登录！</script>");
            Response.Redirect("../../Admin_login.aspx");
            return;
        }
        PowerManage(user);
		u_init();
    }

    private void PowerManage(UserPower user)
    { 

    }
	private void u_init()
	{
		string sql = "select top 1000 * from dbo.JCI_patient_brifeInfo order by rdn DESC";
		SqlConnect conn = new SqlConnect();
		DataTable dt = conn.ExcuteSelect(sql);
		rpt_patient.DataSource = dt;
		rpt_patient.DataBind();

		sql = "select code,name from JCI_patient_dic where dicType='科室' order by orderNum ASC ";
		conn.DropDownListBind(sql, ddl_dept);

		sql = "select code,name from JCI_patient_dic where dicType='楼层' order by orderNum ASC ";
		conn.DropDownListBind(sql, ddl_floor);

		DataTable dt1 = new DataTable();
		dt1.Columns.Add("code");
		dt1.Columns.Add("name");
		dt1.Rows.Add("医疗", "医疗");
		dt1.Rows.Add("护理", "护理");
		dt1.Rows.Add("医技", "医技");
		dt1.Rows.Add("后勤", "后勤");
		dt1.Rows.Add("行政-收费", "行政-收费");

		ddl_category1.DataSource = dt1;
		ddl_category1.DataValueField = "code";
		ddl_category1.DataTextField = "name";
		ddl_category1.DataBind();

		ddl_category2.DataSource = dt1;
		ddl_category2.DataValueField = "code";
		ddl_category2.DataTextField = "name";
		ddl_category2.DataBind();

	}
	protected void btn_export_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		string fileName = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + "医疗工作.xls";
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		pt.Exportyl(fileName, beginDate, endDate,null);
	}
	protected void btn_export_hl_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		string filePath = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + "护理工作.xls";
		//string fileName = @"E:\testsrrsh" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
		pt.Exporthl(filePath, beginDate, endDate,null);
		DownloadFile(Response, filePath, "护理工作.xls");
	}
	protected void btn_export_yj_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		string fileName = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + "医技工作.xls";
		pt.Exportyj(fileName, beginDate, endDate,null);
		//DownloadFile(Response, fileName, "医技工作.xls");
	}
	protected void btn_export_hq_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		string fileName = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + "后勤工作.xls";
		pt.Exporthq(fileName, beginDate, endDate,null);
		//DownloadFile(Response, fileName, "医技工作.xls");
	}
	protected void btn_export_zy_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		string fileName = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + "行政收费.xls";
		pt.Exportzy(fileName, beginDate, endDate, null);
		//DownloadFile(Response, fileName, "医技工作.xls");
	}
	//btn_export_all_Click
	protected void btn_export_all_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		string fileName = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + "所有数据"+DateTime.Now.ToString("yyyyMMdd")+".xls";
		pt.ExportAll(fileName, beginDate, endDate);
		//DownloadFile(Response, fileName, "医技工作.xls");
	}
	protected void btn_export_floor_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		string floorName = ddl_floor.SelectedValue;
		string category = ddl_category2.SelectedValue;
		string fileName = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + category + "_" + floorName + DateTime.Now.ToString("yyyyMMdd") + ".xls";
		string sqlWhere = " and brifeInfo.floorName ='" + floorName + "'";
		switch (category)
		{
			case "医疗":
				pt.Exportyl(fileName, beginDate, endDate, sqlWhere);
				break;
			case "护理":
				pt.Exporthl(fileName, beginDate, endDate, sqlWhere);
				break;
			case "医技":
				pt.Exportyj(fileName, beginDate, endDate, sqlWhere);
				break;
			case "后勤":
				pt.Exporthq(fileName, beginDate, endDate, sqlWhere);
				break;
			case "行政-收费":
				pt.Exportzy(fileName, beginDate, endDate, sqlWhere);
				break;
			default:
				break;
		}

	}
	protected void btn_export_dept_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		string deptName = ddl_dept.SelectedValue;
		string category = ddl_category1.SelectedValue;
		string fileName = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + category + "_" + deptName + DateTime.Now.ToString("yyyyMMdd") + ".xls";
		string sqlWhere = " and brifeInfo.dept ='" + deptName + "'";
		switch (category)
		{
			case "医疗":
				pt.Exportyl(fileName, beginDate, endDate, sqlWhere);
				break;
			case "护理":
				pt.Exporthl(fileName, beginDate, endDate, sqlWhere);
				break;
			case "医技":
				pt.Exportyj(fileName, beginDate, endDate, sqlWhere);
				break;
			case "后勤":
				pt.Exporthq(fileName, beginDate, endDate, sqlWhere);
				break;
			case "行政-收费":
				pt.Exportzy(fileName, beginDate, endDate, sqlWhere);
				break;
			default:
				break;
		}

	}
	protected void btn_delete_Click(object sender, CommandEventArgs e)
	{
		PatientTable pt = new PatientTable();
		string rdn = e.CommandArgument.ToString();
		pt.Delete(rdn);
		u_init();
	}
	private void DownloadFile(HttpResponse response, string fileName, string saveFileName)
	{
		if (Request.UserAgent.Contains("MSIE") || Request.UserAgent.Contains("msie"))
		{
			saveFileName = HttpUtility.UrlEncode(saveFileName);
		}
		response.Clear();
		Response.Charset = "UTF-8";
		Response.ContentEncoding = System.Text.Encoding.UTF8;
		Response.Buffer = true;
		response.AppendHeader("Content-Disposition", "attachment;filename=" + saveFileName);
		response.WriteFile(fileName);
		response.Flush();
		response.Close();
	}
}
