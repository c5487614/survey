using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Patient_Manage_PreView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (IsPostBack) return;

		u_init();
    }
	private void u_init()
	{
		string rdn = Request.QueryString["rdn"];
		if (rdn != null)
		{
			u_init_brifeInfo(rdn);
			u_init_grid(rdn);
		}
	}
	private void u_init_brifeInfo(string rdn)
	{
		SqlConnect conn = new SqlConnect();
		string sql = "select * from dbo.JCI_patient_brifeInfo where rdn=" + rdn;
		DataTable dt = conn.ExcuteSelect(sql);
		DataRow dr = dt.Rows[0];
		tbox_reportDate.Text = dr["regDept"].ToString();
		tbox_fillPerson.Text = dr["fillPerson"].ToString();
		tbox_once.Text = dr["isFirst"].ToString();
		tbox_other1.Text = dr["other1"].ToString();
		tbox_personSex.Text = dr["personSex"].ToString();
		tbox_regDate.Text = Convert.ToDateTime(dr["regDate"]).ToString("yyy-MM-dd");
		tbox_ddl_dept.Text = dr["dept"].ToString();
		tbox_ddl_floor.Text = dr["floorName"].ToString();
		tbox_job.Text = dr["job"].ToString();
		tbox_pay.Text = dr["payType"].ToString();

		string promote = dr["promote"].ToString();
		string needImprove = dr["needImprove"].ToString();
		string[] promotes = promote.Split(';');
		string[] needImproves = needImprove.Split(';');
		tbox_promote_yl.Text = promotes[0].Split(':')[1];
		tbox_promote_hl.Text = promotes[1].Split(':')[1];
		tbox_promote_yj.Text = promotes[2].Split(':')[1];
		tbox_promote_hq.Text = promotes[3].Split(':')[1];
		tbox_promote_zy.Text = promotes[4].Split(':')[1];
		tbox_needImprove_yl.Text = needImproves[0].Split(':')[1];
		tbox_needImprove_hl.Text = needImproves[1].Split(':')[1];
		tbox_needImprove_yj.Text = needImproves[2].Split(':')[1];
		tbox_needImprove_hq.Text = needImproves[3].Split(':')[1];
		tbox_needImprove_zy.Text = needImproves[4].Split(':')[1];

	}
	private void u_init_grid(string rdn)
	{
		string sql = "select * from dbo.JCI_detailInfo where applyRdn = " + rdn + " and typefrom='Patient'";
		SqlConnect conn = new SqlConnect();
		DataTable dt = conn.ExcuteSelect(sql);
		GridView1.DataSource = dt;
		GridView1.DataBind();
	}
	protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
	{
		Label lblResult = e.Row.FindControl("Label3") as Label;
		if (lblResult != null)
		{
			DataRowView drv = e.Row.DataItem as DataRowView;
			string result = drv.Row["result"].ToString();
			switch (result)
			{
				case "10":
					lblResult.Text = "满意";
					break;
				case "7":
					lblResult.Text = "比较满意";
					break;
				case "5":
					lblResult.Text = "一般";
					break;
				case "0":
					lblResult.Text = "不满意";
					break;
				case "-1":
					lblResult.Text = "无此需要";
					break;
				default:
					break;
			}
		}
	}
}
