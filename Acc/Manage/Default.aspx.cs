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

public partial class Acc_Manage_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        if (null == Session["power"]) Response.Redirect("../../Err/Err.aspx");
        if (!srrshPage.Level2(Session["power"].ToString())) Response.Redirect("../../Err/powerNeed.aspx");
        
        //0：未审核  1：审核  2：全部  type属性说明
        string type = "2";
        if (null == Request.QueryString["type"]) type = "2";
        else type = Request.QueryString["type"].ToString();
        
        string[] power = Session["power"].ToString().Split(',');
        string dept = power[1];
        accTable ac = new accTable();
        DataTable dt = ac.sDeptView(dept, type, "", Session["power"].ToString().Split(',')[3].Split('.')[1]);
        dt=alertTable(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        u_init();
    }

    private void u_init()
    {
        string sql = "select rdn,tableName1 from jci_tableList order by sortid asc";
        SqlConnect conn = new SqlConnect();
        //DataTable dt = conn.ExcuteSelect(sql);
        conn.DropDownListBind(sql, typeList);
        //typeList.SelectedIndex = typeList.Items.Count;

    }
    private DataTable alertTable(DataTable dt)
    {
        DataTable rdt = new DataTable();

        rdt.Columns.Add("类别");
        rdt.Columns.Add("编号");
        rdt.Columns.Add("上报员工/电话");
        rdt.Columns.Add("日期/星期");
        rdt.Columns.Add("姓名/病历号");
        rdt.Columns.Add("住院/门诊");
        rdt.Columns.Add("审核");
        foreach (DataRow dr in dt.Rows)
        {
            rdt.Rows.Add(dr["tableName"],
                dr["reportId"]
                , dr["reportPerson"].ToString() + "/" + dr["personPhone"].ToString()
                , Convert.ToDateTime(dr["reportDate"].ToString()).ToString("yyyy-MM-dd")+"/" + dr["oweekday"].ToString()
                , dr["pName"].ToString() +"/" +dr["pmrn"].ToString()
                , dr["pType"].ToString()
                , dr["isPassed"].ToString() == "" ? "未审核" : "已审核");
        }
        return rdt;
    }

    protected void deleteBtn(object sender, CommandEventArgs e)
    {
        try 
	    {	        
		    accTable acc = new accTable();
            string person = Session["power"].ToString().Split(',')[2];
            acc.AccDelete(e.CommandArgument.ToString(),person);

            string type = "2";
            if (null == Request.QueryString["type"]) type = "2";
            else type = Request.QueryString["type"].ToString();

            string[] power = Session["power"].ToString().Split(',');
            string dept = power[1];
            accTable ac = new accTable();
            DataTable dt = ac.sDeptView(dept, type, "", Session["power"].ToString().Split(',')[3].Split('.')[1]);
            dt = alertTable(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
	    }
	    catch (Exception ex)
	    {
    		
            Response.Write(ex.ToString());
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string type = "2";
            string strWhere="";
            if (null == Request.QueryString["type"]) type = "2";
            else type = Request.QueryString["type"].ToString();

            string[] power = Session["power"].ToString().Split(',');
            string dept = power[1];
            string pow = Session["power"].ToString().Split(',')[3].Split('.')[1];
            if (!("".Equals(tbDate1.Text.Trim()) || "".Equals(tbDate2.Text.Trim()))) 
             strWhere= " and reportdate>='" + tbDate1.Text + "' and reportdate<='" + tbDate2.Text + "' ";
            if (!typeList.SelectedValue.Equals("-99"))
                strWhere += " and tableRdn=" + typeList.SelectedValue;
            accTable ac = new accTable();
            DataTable dt = ac.sDeptView(dept, type, strWhere, pow);
            dt = alertTable(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {

            Response.Write(ex.ToString());
        }
        
    }
}
