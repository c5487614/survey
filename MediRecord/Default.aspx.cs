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

public partial class MediRecord_Default : System.Web.UI.Page
{
    SqlConnect conn = new SqlConnect();
    public static DataTable _ddt;
    public bool IsDynamicLoadControl
    {
        get
        {
            object Dynamic = ViewState["IsDynamicLoadControl"];
            return Dynamic == null ? false : true;
        }
        set { ViewState["IsDynamicLoadControl"] = value; }
    }
    protected override void LoadViewState(object savedState)
    {
        base.LoadViewState(savedState);
        if (IsDynamicLoadControl)
        {
            MediTable mt = new MediTable();
            mt.generator(_ddt, GridView1);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        //if (Request.QueryString["id"] == null) Response.Redirect("../Err/nonePage.aspx");

        //if (Session["power"] == null) Response.Redirect("../Default.aspx");
        //if (!srrshPage.Level2(Session["power"].ToString())) Response.Redirect("../Err/powerNeed.aspx");//没权限

        //setAutoComplete();
        //commonHis ch = new commonHis();
        //ddldept.DataSource = ch.getDeptList("*");
        //ddldept.DataTextField="deptName";
        //ddldept.DataValueField = "deptName";
        //ddldept.DataBind();

        string sql = "select largeItemRdn,largeItemName,smallItemRdn,smallItemName,sItemValue,sItemType from JCI_generateTable where tableRdn=" + Request.QueryString["id"].Split(',')[0].ToString();
        DataTable dt = conn.ExcuteSelect(sql);
        MediTable mt = new MediTable();
        _ddt = dt;
        mt.generator(dt, GridView1);
        IsDynamicLoadControl = true;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MediTable mt = new MediTable();
        string sdocDept = ddldept.SelectedValue ;
        string scheckDpt = checkDept.Text.Trim();
        string scheckDate = checkTime.Text.Trim();
        string stablescore = sumup.Text;
        string smrn = mrn.Text.Trim();
        string spname = pName.Text;
        if (sumup.Text == "") { Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('请先计算得分');</script>"); return; }
        try
        {
            string[] tableinfo = Request.QueryString["id"].ToString().Split(',');
            string sdocs = attend1.Text + ":" + attend2.Text + ":" + attend3.Text + ":" + attend4.Text + ":" + attend5.Text;
            string sfellows = getfellows();
            string index = mt.saveBrifeInfo(scheckDate, scheckDpt, sdocDept, sdocs, sfellows, tableinfo[0], tableinfo[1], mt.tableType,stablescore,smrn,spname, GridView1);
            if (index != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('添加成功');window.location='../tableList.aspx'</script>");
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("../Err/nonePage.aspx");
        }
        
    }

    private string getfellows()
    {
        System.Text.StringBuilder sb=new System.Text.StringBuilder();
        if (fellow1.Text != "") sb.Append(fellow1.Text);
        if (fellow2.Text != "") sb.Append(","+fellow2.Text);
        if (fellow3.Text != "") sb.Append("," + fellow3.Text);
        if (fellow4.Text != "") sb.Append("," + fellow4.Text);
        if (fellow5.Text != "") sb.Append("," + fellow5.Text);
        return sb.ToString();
    }

    //private void setAutoComplete()
    //{
    //    attend1.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend1'");
    //    attend2.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend2'");
    //    attend3.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend3'");
    //    attend4.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend4'");
    //    attend5.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend5'");

    //    fellow1.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend1'");
    //    fellow2.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend1'");
    //    fellow3.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend1'");
    //    fellow4.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend1'");
    //    fellow5.Attributes.Add("onKeyup", "findNames('AJAX/AutoComplete.asxp?names=','attend1'");
    //}
}
