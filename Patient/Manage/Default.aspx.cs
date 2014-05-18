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
		//user = GetDummyUser();
        if (user == null)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('没有登录或会话失效，请登录！')</script>");
            Response.Redirect("../../Admin_login.aspx");
            return;
        }
		string sqlWhere = GetPowerSqlWhere(user);
		u_init(sqlWhere);
		PowerManage(user);
    }
    private UserPower GetDummyUser()
    {
        UserPower user = new UserPower();
        user.Power = "admin";
		Session["user"] = user;
        return user;
    }
	private string GetPowerSqlWhere(UserPower user)
	{
		string retValue = "";
		if (user.IsAdmin())
        {
            //do nothing
        }
        else if (user.IsSuperuser())
        {
        }
        else if (user.IsUser())
        {
			retValue = " and dept='" + user.UserDeptName + "' ";
        }
        else
        {
            //default
        }
		return retValue;
	}
	
    private void PowerManage(UserPower user)
    {
        if (user.IsAdmin())
        {
            //do nothing
        }
        else if (user.IsSuperuser())
        {
			//ddl_dept.Visible = false;
			//lbl_dept.Text = user.UserDeptName;
			//p_floor.Visible = false;
        }
        else if (user.IsUser())
        {
			ddl_dept.Visible = false;
			lbl_dept.Text = user.UserDeptName;
			p_floor.Visible = false;
			ddl_floor.Visible = false;
			p_catigory2.Visible = false;
			ddl_category2.Visible = false;
			btn_export_floor.Visible = false;
			div_operation.Visible = false;
        }
        else
        {
            //default
        }
    }
	private void u_init(string sqlWhere)
	{
		string sql = "select top 1000 * from dbo.JCI_patient_brifeInfo where 1=1 " + sqlWhere + " order by rdn DESC";
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
		//added by Chunhui Chen 2014-05-18
		DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
		tbox_beginDate.Text = now.ToString("yyyy-MM-dd");
		tbox_endDate.Text = now.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
	}
	protected void btn_export_Click(object sender, EventArgs e)
	{
		PatientTable pt = new PatientTable();
		DateTime beginDate, endDate;
		string filePath = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + "医疗工作.xls";
		beginDate = DateTime.Now.AddMonths(-1);
		endDate = DateTime.Now.AddMonths(1);
		beginDate = Convert.ToDateTime(tbox_beginDate.Text);
		beginDate = beginDate.AddDays(-1);
		endDate = Convert.ToDateTime(tbox_endDate.Text);
		endDate = endDate.AddDays(1);
		string result = pt.Exportyl(filePath, beginDate, endDate, null);
		DownloadFile(result, Response, filePath, "医疗工作.xls");
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
		string result = pt.Exporthl(filePath, beginDate, endDate,null);

		DownloadFile(result, Response, filePath, "护理工作.xls");
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
		string result = pt.Exportyj(fileName, beginDate, endDate,null);
		DownloadFile(result,Response, fileName, "医技工作.xls");
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
		string result = pt.Exporthq(fileName, beginDate, endDate,null);
		DownloadFile(result, Response, fileName, "后勤工作.xls");
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
		string result = pt.Exportzy(fileName, beginDate, endDate, null);
		DownloadFile(result, Response, fileName, "行政收费.xls");
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
		string result = pt.ExportAll(fileName, beginDate, endDate);
		DownloadFile(result, Response, fileName, "所有数据.xls");
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
		string result = "";
		switch (category)
		{
			case "医疗":
				result = pt.Exportyl(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "医疗工作_" + category + "_" + floorName + ".xls");
				break;
			case "护理":
				result = pt.Exporthl(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "护理工作_" + category + "_" + floorName + ".xls");
				break;
			case "医技":
				result = pt.Exportyj(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "医技工作_" + category + "_" + floorName + ".xls");
				break;
			case "后勤":
				result = pt.Exporthq(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "后勤工作_" + category + "_" + floorName + ".xls");
				break;
			case "行政-收费":
				result = pt.Exportzy(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "行政-收费_" + category + "_" + floorName + ".xls");
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
		string deptName = "";// ddl_dept.SelectedValue;
		UserPower user = Session["user"] as UserPower;
		if (user == null)
		{
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('没有登录或会话失效，请登录！')</script>");
			Response.Redirect("../../Admin_login.aspx");
		}
		else
		{
			deptName =user.GetPowerDept();
			if (deptName.Equals(""))
			{
				//use selected dept
				deptName = ddl_dept.SelectedValue;
			}
		}
		//deptName = GetPowerDept()
		string category = ddl_category1.SelectedValue;
		string fileName = System.Configuration.ConfigurationManager.AppSettings["ExportPath"] + category + "_" + deptName + DateTime.Now.ToString("yyyyMMdd") + ".xls";
		string sqlWhere = " and brifeInfo.dept ='" + deptName + "'";
		string result = "";
		switch (category)
		{
			case "医疗":
				result = pt.Exportyl(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "医疗_" + category + "_" + deptName + ".xls");
				break;
			case "护理":
				result = pt.Exporthl(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "护理_" + category + "_" + deptName + ".xls");
				break;
			case "医技":
				result = pt.Exportyj(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "医技_" + category + "_" + deptName + ".xls");
				break;
			case "后勤":
				result = pt.Exporthq(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "后勤_" + category + "_" + deptName + ".xls");
				break;
			case "行政-收费":
				result = pt.Exportzy(fileName, beginDate, endDate, sqlWhere);
				DownloadFile(result, Response, fileName, "行政-收费_" + category + "_" + deptName + ".xls");
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
		u_init("");
	}
	//btn_logout_Click
	protected void btn_logout_Click(object sender, EventArgs e)
	{
		Session["user"] = null;
		Response.Redirect("../../Admin_login.aspx");
	}
	private void DownloadFile(string result,HttpResponse response, string fileName, string saveFileName)
	{
		if (!"ok".Equals(result))
		{
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('查询记录为空。')</script>");
			return;
		}
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
	protected void rpt_patient_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		//e.Item.DataItem
		UserPower user = Session["user"] as UserPower;
		//user = GetDummyUser();
		if (user == null)
		{
			Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>没有登录或会话失效，请登录！</script>");
			Response.Redirect("../../Admin_login.aspx");
		}
		object o = e.Item.DataItem;
		Control Btndelete = e.Item.FindControl("btn_delete");
		if (Btndelete != null)
		{
			if (!user.IsAdmin())
			{
				Btndelete.Visible = false;
			}
		}
		
	}
}
