using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

/// <summary>
///tableBind 的摘要说明
/// </summary>
public class accTable:Ftable
{
	public accTable()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        //tableName = "意外事件";
        //tableType = "1";
    }
    #region
    public bool u_intial(DataTable dt, GridView gv,string allInOne)
    {
        bool Succ = false;
        try
        {
            string[] rows = allInOne.Split(';');
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("c1");
            dt1.Columns.Add("c2");
            for (int i = 0; i < dt.Rows.Count; i++)
                dt1.Rows.Add("", "");
            gv.DataSource = dt1;
            gv.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                gv.Rows[i].Cells[0].Text = dt.Rows[i]["itemName"].ToString();
                string itemsStr = dt.Rows[i]["sItemNames"].ToString();
                u_setCheck(itemsStr, i,gv,rows[i]);
            }
            Succ = true;
        }
        catch (Exception ex)
        {

            Succ = false;
        }
        return Succ;
    }
    public bool u_setCheck(string itemsStr, int index,GridView gv,string row)
    {
        bool Succ = false;
        try
        {
            string[] item = itemsStr.Split('；');
            string[] itemValue=row.Split(',');//这个是结果中的数据  格式 string:0
            for (int i = 0; i < item.Length; i++)
            {
                string[] info=itemValue[i].Split(':');
                CheckBox cb = new CheckBox();
                if (i % 5 == 0 && i != 0)
                {
                    Label ll = new Label();
                    ll.Text = "<br/><br/>";
                    gv.Rows[index].Cells[1].Controls.Add(ll);
                }
                cb.Text = item[i];
                if (cb.Text != info[0])//不相等说明有问题
                {
                }
                else
                {
                    if (info[1] == "1")
                    {
                        cb.ForeColor = System.Drawing.Color.Red;
                        cb.Checked = true;
                    }
                }
                cb.ID = "Dcb" + i.ToString();
                gv.Rows[index].Cells[1].Controls.Add((Control)cb);
            }
            Succ = true;
        }
        catch (Exception ex)
        {

            Succ = false;
        }
        return Succ;
    }
    public bool u_getValue(GridView gv,string allInOne)
    {
        bool Succ = false;
        try
        {
            string[] rows = allInOne.Split(';');
            int LineCount = gv.Rows.Count;
            for (int i = 0; i < LineCount; i++)
            {
                string[] items = rows[i].Split(',');
                ControlCollection ccs = gv.Rows[i].Cells[1].Controls;
                if (items.Length < ccs.Count) break;//如果对应不上就跳出
                for(int j=0;j<ccs.Count;j++)
                {
                    CheckBox cb = ccs[j] as CheckBox;
                    string[] info = items[j].Split(':');
                    if (cb != null)
                    {
                        if (info[0] == cb.Text)
                        {
                            if (info[1] == "1") cb.Checked = true;
                            else cb.Checked = false;
                        }
                    }
                    else
                    {
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Succ = false;        
        }
        return Succ;
    }

    //从页面获取gridview的数据
    public string submit1(ref System.Text.StringBuilder xmldata,GridView gv)
    {
        int LineCount = gv.Rows.Count;
        string title = null;
        System.Text.StringBuilder resultStr = new System.Text.StringBuilder();

        // xmldata.Append("<result>");
        for (int i = 0; i < LineCount; i++)
        {
            bool hasFound = false;
            ControlCollection ccs = gv.Rows[i].Cells[1].Controls;
            foreach (Control c in ccs)
            {
                CheckBox cb = c as CheckBox;
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        resultStr.Append(cb.Text.Trim() + ":" + "1");
                        #region xmldata数据操作
                        if (!hasFound)//xml数据生成
                        {
                            title = gv.Rows[i].Cells[0].Text;
                            hasFound = true;
                            xmldata.Append("<" + title + ">");// xml头
                        }
                        xmldata.Append(cb.Text + ";");//有选中的加入
                        #endregion
                    }
                    else//没有选中 string  resutlt
                    {
                        resultStr.Append(cb.Text.Trim() + ":" + "0");
                    }
                    resultStr.Append(",");
                }
                //cch20100324 备注栏修改
                //else
                //{
                //    TextBox ttb = c as TextBox;
                //    if (ttb != null)
                //    {
                //        resultStr.Append("备注"+ ":" + ttb.Text.Trim());
                //    }
                //}
            }
            resultStr.Remove(resultStr.Length - 1, 1);
            resultStr.Append(";");
            if (hasFound)//xml数据操作
            {
                xmldata.Remove(xmldata.Length - 1, 1);
                xmldata.Append("</" + title + ">");
            }
        }
        // xmldata.Append("</result>");
        resultStr.Remove(resultStr.Length - 1, 1);
        return resultStr.ToString();
    }
    public string setWeekday(DateTime date)
    {
        switch (date.DayOfWeek)
        {
            case DayOfWeek.Monday:
                return "星期一";
            case DayOfWeek.Tuesday:
                return "星期二";
            case DayOfWeek.Wednesday:
                return "星期三";
            case DayOfWeek.Thursday:
                return "星期四";
            case DayOfWeek.Friday:
                return "星期五";
            case DayOfWeek.Saturday:
                return "星期六";
            case DayOfWeek.Sunday:
                return "星期天";
            default:
                throw new Exception("日期出错！");
        }
    }
    #endregion

    #region 页面处理
    public bool generator(DataTable dt, GridView gv)
    {
        int i,count;
        DataTable tmpdt = new DataTable();
        tmpdt.Columns.Add("c1");
        tmpdt.Columns.Add("c2");
        for (i = 0; i < dt.Rows.Count; i++)
        {
            tmpdt.Rows.Add("", "");
        }
        gv.DataSource = tmpdt;
        gv.DataBind();
        //------------------------------------------------产生列
        string Item = null;
        for (i = 0; i < dt.Rows.Count; i++)
        {
            Item = dt.Rows[i]["sItemNames"].ToString();
            Label ll = new Label();
            HiddenField hf1 = new HiddenField();
            hf1.Value = dt.Rows[i]["itemIndex"].ToString();
            ll.Text = dt.Rows[i]["itemName"].ToString();
            gv.Rows[i].Cells[0].Controls.Add(ll);
            gv.Rows[i].Cells[0].Controls.Add(hf1);
            //---------------------------------------------添加第一列数据
            count = 0;
            string[] Items = Item.Split('；');
            foreach (string s in Items)
            {
                if (s.Equals("")) continue;

                CheckBox cb = new CheckBox();
                cb.Text = s;
                gv.Rows[i].Cells[1].Controls.Add(cb);
                count++;
                if (count % 5 == 0)
                {
                    Label lbr = new Label();
                    lbr.Text = "<br /><br />";
                    gv.Rows[i].Cells[1].Controls.Add(lbr);
                }
            }
            //---------------------------------------------添加第二列checkbox
        }
        return true;
    }

    //----生成已存入的数据
    public bool reGenerator(DataTable dt, GridView gv)
    {
        int i, count;
        DataTable tmpdt = new DataTable();
        tmpdt.Columns.Add("c1");
        tmpdt.Columns.Add("c2");
        for (i = 0; i < dt.Rows.Count; i++)
        {
            tmpdt.Rows.Add("", "");
        }
        gv.DataSource = tmpdt;
        gv.DataBind();
        //------------------------------------------------产生列
        string Item = null;
        for (i = 0; i < dt.Rows.Count; i++)
        {
            Item = dt.Rows[i]["result"].ToString();
            Label ll = new Label();
            HiddenField hf1 = new HiddenField();
            hf1.Value = dt.Rows[i]["largeItemRdn"].ToString();
            ll.Text = dt.Rows[i]["largeItemName"].ToString();
            gv.Rows[i].Cells[0].Controls.Add(ll);
            gv.Rows[i].Cells[0].Controls.Add(hf1);
            //---------------------------------------------添加第一列数据
            count = 0;
            string[] Items = Item.Split(',');
            foreach (string s in Items)
            {
                if (s.Equals("")) continue;

                CheckBox cb = new CheckBox();
                cb.Text = s;
                if (s.Split(':')[1].Equals("1")) { cb.Checked = true; cb.ForeColor = System.Drawing.Color.Red; }
                gv.Rows[i].Cells[1].Controls.Add(cb);
                count++;
                if (count % 5 == 0)
                {
                    Label lbr = new Label();
                    lbr.Text = "<br /><br />";
                    gv.Rows[i].Cells[1].Controls.Add(lbr);
                }
            }
            //---------------------------------------------添加第二列checkbox
        }
        return true;
    }

    #endregion

    #region 保存数据
    public string saveBrifeInfo(
        string stablerdn, 
        string stablename, 
        string sreportdept,
        string sreportperson,
        string spersonphone,
        string sreportdate,
        string soccurtime,
        string soweekday,
        string spname,
        string page,
        string spmrn,
        string spssex,
        string spoper,
        string sptype,
        string sgothrough,
        string smeasure,
        string addcontent,
        GridView gv)
    {
        string[] paramNames = new string[17];
        string[] paramValues = new string[17];
        paramNames[0] = "@tableRdn";
        paramNames[1] = "@tableName";
        paramNames[2] = "@reportDept";
        paramNames[3] = "@reportPerson";
        paramNames[4] = "@personPhone";
        paramNames[5] = "@reportDate";
        paramNames[6] = "@occurTime";
        paramNames[7] = "@oweekday";
        paramNames[8] = "@pName";
        paramNames[9] = "@pMrn";
        paramNames[10] = "@pAge";
        paramNames[11] = "@pSex";
        paramNames[12] = "@pOper";
        paramNames[13] = "@pType";
        paramNames[14] = "@goThrough";
        paramNames[15] = "@measure";
        paramNames[16] = "@addcontent";

        paramValues[0] = stablerdn;
        paramValues[1] = stablename;
        paramValues[2] = sreportdept;
        paramValues[3] = sreportperson;
        paramValues[4] = spersonphone;
        paramValues[5] = sreportdate;
        paramValues[6] = soccurtime;
        paramValues[7] = soweekday;
        paramValues[8] = spname;
        paramValues[9] = page;
        paramValues[10] = spmrn;
        paramValues[11] = spssex;
        paramValues[12] = spoper;
        paramValues[13] = sptype;
        paramValues[14] = sgothrough;
        paramValues[15] = smeasure;
        paramValues[16] = addcontent;
        try
        {
            SqlConnect conn = new SqlConnect();
            string rr = conn.ExcuteUpdateCmd("dbo.acc_insert_brifeInfo", paramNames, paramValues).ToString();
            saveDetailInfo(rr, gv);
            return "ok";
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    private string saveDetailInfo(string rdn, GridView gv)
    {
        int LineCount = gv.Rows.Count;
        int i;
        DataTable dt = new DataTable();
        //--------------------------------------------------
        string tablerdn = null;
        string largeItemRdn = null;
        string largeItemName = null;
        System.Text.StringBuilder result = null;
        System.Text.StringBuilder selected = null;
        //--------------------------------------------------
        dt.Columns.Add("tablerdn");
        dt.Columns.Add("largeItemRdn");
        dt.Columns.Add("largeItemName");
        dt.Columns.Add("result");
        dt.Columns.Add("selected");
        for (i = 0; i < LineCount; i++)
        {
            ControlCollection ccs = gv.Rows[i].Cells[1].Controls;
            result = new System.Text.StringBuilder();
            selected = new System.Text.StringBuilder();
            foreach (Control c in ccs)
            {
                CheckBox cb = c as CheckBox;
                if (cb != null)
                {
                    result.Append(cb.Text+":");
                    if (cb.Checked) 
                    { 
                        result.Append("1");
                        selected.Append(cb.Text + ","); //加入selected列
                    }
                    else result.Append("0");
                }
                result.Append(",");
            }
            result.Length = result.Length - 1;//去掉最后一个 ‘,’  结果
            if (selected.Length != 0) selected.Length = selected.Length - 1;//去掉最后一个 ‘,’  结果
            Label ll = gv.Rows[i].Cells[0].Controls[0] as Label;
            HiddenField hf1 = gv.Rows[i].Cells[0].Controls[1] as HiddenField;

            if (ll != null) largeItemName = ll.Text;//项目名
            if (hf1 != null) largeItemRdn = hf1.Value;//项目编号

            dt.Rows.Add(rdn, largeItemRdn, largeItemName, result.ToString(),selected.ToString());
        }
        SqlConnect conn = new SqlConnect();
        conn.tableMerge(dt, getCmd());
        return "ok";
    }
    private SqlCommand getCmd()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[dbo].[acc_insert_DetailInfo]";
        SqlParameter p1 = new SqlParameter("@tableRdn", SqlDbType.Int, 16, "tablerdn");
        SqlParameter p2 = new SqlParameter("@largeItemRdn", SqlDbType.Int, 16, "largeItemRdn");
        SqlParameter p3 = new SqlParameter("@largeItemName", SqlDbType.VarChar, 50, "largeItemName");
        SqlParameter p4 = new SqlParameter("@result", SqlDbType.VarChar, 500, "result");
        SqlParameter p5 = new SqlParameter("@selected", SqlDbType.VarChar, 500, "selected");
        cmd.Parameters.Add(p1);
        cmd.Parameters.Add(p2);
        cmd.Parameters.Add(p3);
        cmd.Parameters.Add(p4);
        cmd.Parameters.Add(p5);
        return cmd;
    }

    public string AccDelete(string rdn, string deletePerson)
    {
        string result = "";
        try
        {
            SqlConnect conn = new SqlConnect();

            SqlParameter p1 = new SqlParameter("@rdn", rdn);
            SqlParameter p2 = new SqlParameter("@deletePerson", deletePerson);
            string[] paramNames = new string[2];
            string[] paramValues = new string[2];
            paramNames[0] = "@rdn";
            paramNames[1] = "@deletePerson";

            paramValues[0]=rdn;
            paramValues[1]=deletePerson;
            int n =conn.ExcuteUpdateCmd("acc_delete", paramNames, paramValues);
            if (n >= 0) result = "ok";
            

        }
        catch (Exception ex)
        {
            
            throw ex;
        }
        return result;
    }
    #endregion

    #region 数据查询分析

    
    public int getIsPassedCount(string type,string dept,string power)
    {
        //0：未审核 1：审核
        int result=0;
        string sql = "";
        switch (type)
        {
            case "0":
                if ("管理员".Equals(power)) sql = "select count(*) from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and (ispassed='0' or ispassed is null)";
                else sql = "select count(*) from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and (ispassed='0' or ispassed is null) and reportDept='" + dept + "'";
                break;
            case "1":
                if ("管理员".Equals(power)) sql = "select count(*) from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and  ispassed='1'";
                else sql = "select count(*) from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and  ispassed='1' and reportDept='" + dept + "'";
                break;
            default:
                break;
        }
        try
        {
            SqlConnect conn = new SqlConnect();
            DataTable dt = conn.ExcuteSelect(sql);
            result = Convert.ToInt16(dt.Rows[0][0]);
        }
        catch (Exception ex )
        {
            
            throw ex;
        }
        
        return result;
        
    }
    public DataTable sDeptView(string deptName)
    {
        string sql = "select * from dbo.JCI_acc_brifeInfo where reportDept='" + deptName+"'";
        try
        {
            SqlConnect conn = new SqlConnect();
            return conn.ExcuteSelect(sql);

        }
        catch (Exception ex)
        {
            
            throw;
        }
       
    }

    public DataTable sDeptView(string deptName,string type)
    {
        //1：审核  0：未审核 2：全部
        string sql = "";
        if ("1".Equals(type)) sql = "select * from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and  isPassed='1' and reportDept='" + deptName + "'";
        else if ("0".Equals(type)) sql = "select * from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and (isPassed='0' or isPassed is null) and reportDept='" + deptName + "'";
        else if ("2".Equals(type)) sql = "select * from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and  reportDept='" + deptName + "'"; 
        try
        {
            SqlConnect conn = new SqlConnect();
            return conn.ExcuteSelect(sql);

        }
        catch (Exception ex)
        {

            throw;
        }

    }
    public DataTable sDeptView(string deptName, string type ,string strWhere,string power)
    {
        //1：审核  0：未审核 2：全部
        string sql = "";
        if ("1".Equals(type)) sql = "select * from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and  isPassed='1' ";
        else if ("0".Equals(type)) sql = "select * from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) and (isPassed='0' or isPassed is null) ";
        else if ("2".Equals(type)) sql = "select * from dbo.JCI_acc_brifeInfo where (deleteFlag <>'1' or deleteFlag is null) ";

        if ("管理员".Equals(power))
            ;
        else if ("部门管理员".Equals(power))
            sql += " and reportDept='" + deptName + "' ";
        sql += " " + strWhere;
        try
        {
            SqlConnect conn = new SqlConnect();
            return conn.ExcuteSelect(sql);

        }
        catch (Exception ex)
        {

            throw;
        }

    }
    
    public DataSet sDetailView(string id)
    {
        string sql = "select * from dbo.JCI_acc_brifeInfo where reportId=" + id;
        string sql1 = "select * from dbo.JCI_acc_detailInfo where tablerdn=" + id;
        try
        {
            DataSet ds = new DataSet();
            SqlConnect conn = new SqlConnect();
            ds.Tables.Add(conn.ExcuteSelect(sql));
            ds.Tables.Add(conn.ExcuteSelect(sql1));
            return ds;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    #endregion

    #region 事件审核
    public bool accept(string reportId,string passPerson,string passDate)
    {
        bool Succ = false;
        try
        {
            string sql = "update dbo.JCI_acc_brifeInfo set passPerson='" + passPerson + "',passDate='" + passDate + "',isPassed='1' where reportId=" + reportId;
            SqlConnect conn = new SqlConnect();
            if (conn.ExcuteCmd(sql) > 0)
                Succ = true;
        }
        catch (Exception ex)
        {
            
            throw;
        }
        return Succ;
    }
    #endregion
}
