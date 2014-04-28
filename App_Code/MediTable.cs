using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;

/// <summary>
///MediTable 的摘要说明
/// </summary>
public class MediTable: Ftable
{
	public MediTable()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        this.tableType = "MediRecord";
	}
    /// <summary>
    /// 生成页面
    /// </summary>
    /// <param name="dt">数据源</param>
    /// <param name="gv">目标实体</param>
    public void generator(DataTable dt,GridView gv)
    {
        int i;
        DataTable tmpdt=new DataTable();
        tmpdt.Columns.Add("c1");
        tmpdt.Columns.Add("c2");
        for (i = 0; i < dt.Rows.Count; i++)
        {
            tmpdt.Rows.Add("", "");
        }
        gv.DataSource = tmpdt;
        gv.DataBind();
        for (i = 0; i < dt.Rows.Count; i++)
        {
            Label ll1 = new Label();
            Label ll2 = new Label();
            CheckBox cb = new CheckBox();
            HiddenField hf1 = new HiddenField();
            ll1.Text = "[" + dt.Rows[i]["largeItemName"].ToString() + "]";
            ll2.Text = dt.Rows[i]["smallItemName"].ToString();
            ll1.ForeColor = System.Drawing.Color.Red;
            hf1.Value = dt.Rows[i]["largeItemName"].ToString() + ":" + dt.Rows[i]["smallItemName"].ToString();
            gv.Rows[i].Cells[0].Controls.Add((Control)ll1);
            gv.Rows[i].Cells[0].Controls.Add((Control)ll2);
            gv.Rows[i].Cells[0].Controls.Add((Control)hf1);
            //------------------------------------------------------上面是cell[0]
            cb.ID = dt.Rows[i]["largeItemRdn"].ToString() + "_" + dt.Rows[i]["smallItemRdn"].ToString();
            cb.Text = dt.Rows[i]["sItemValue"].ToString();
            cb.Attributes.Add("onClick", "setColor(this)");
            cb.InputAttributes.Add("value", dt.Rows[i]["sItemValue"].ToString());//给checkbox赋值
            gv.Rows[i].Cells[1].Controls.Add((Control)cb);
        }
    }
    public void saveMediTable(GridView gv,string index)
    {
        try
        {
            int i;
            SqlConnect conn = new SqlConnect();
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            #region 遍历
            for (i = 0; i < gv.Rows.Count; i++)
            {
                sql.Remove(0, sql.Length);
                sql.Append("insert into JCI_detailInfo([applyRdn],[largeItemRdn],[smallItemRdn],[result],[largeItemName],[smallItemName],[typefrom])");
                sql.Append("values(" + index);

                CheckBox cb = null;//控件类型
                Label lab = null;
                ControlCollection ccs = gv.Rows[i].Cells[1].Controls;//控件位置
                foreach (Control c in ccs)//遍历控件
                {
                    cb = c as CheckBox;
                    if (cb == null) continue;//出错
                    string[] rdns = cb.ID.Split('_');
                    //string[] rdns=new string[2];
                    //rdns[0] = "1"; rdns[0] = "1";
                    sql.Append("," + rdns[0] + "," + rdns[1]);
                    if (cb.Checked)
                    {
                        sql.Append(",'" + cb.Text + "'");
                    }
                    else
                    {
                        sql.Append(",'0'");
                    }
                }
                ccs = gv.Rows[i].Cells[0].Controls;
                foreach (Control c in ccs)//遍历控件 cell[0]
                {
                    lab = c as Label;
                    if (lab != null)
                    {
                        sql.Append(",'" + lab.Text + "'");
                    }
                }
                sql.Append(",'" + this.tableType + "'");
                sql.Append(")");
                conn.ExcuteCmd(sql.ToString());
            }
            #endregion
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 保存信息到数据库
    /// </summary>
    /// <param name="filldate">填写时间</param>
    /// <param name="filldept">填写人部门</param>
    /// <param name="docdept">医生部门</param>
    /// <param name="docall">atending医生</param>
    /// <param name="fellows">fellow医生</param>
    /// <param name="tablerdn">对应的rdn</param>
    /// <param name="tablename">table名字</param>
    /// <param name="tabletype">table类别</param>
    /// <param name="tablescore">table分数</param>
    /// <param name="gv">源</param>
    /// <returns></returns>
    public string saveBrifeInfo(string filldate, string filldept, string docdept, string docall, string fellows, string tablerdn, string tablename, string tabletype, string tablescore,string mrn,string pname,GridView gv)
    {
        string[] paramNames = new string[15];
        string[] paramValues = new string[15];
        string[] docs = docall.Split(':');
        paramNames[0] = "@fillInDate";
        paramNames[1] = "@fillDept";
        paramNames[2] = "@docDept";
        paramNames[3] = "@doc1";
        paramNames[4] = "@doc2";
        paramNames[5] = "@doc3";
        paramNames[6] = "@doc4";
        paramNames[7] = "@doc5";
        paramNames[8] = "@fellows";
        paramNames[9] = "@tableRdn";
        paramNames[10] = "@tableName";
        paramNames[11] = "@tableType";
        paramNames[12] = "@tableScore";
        paramNames[13] = "@mrn";
        paramNames[14] = "@pName";


        paramValues[0] = filldate;
        paramValues[1] = filldept;
        paramValues[2] = docdept;
        paramValues[3] = docs[0];
        paramValues[4] = docs[1];
        paramValues[5] = docs[2];
        paramValues[6] = docs[3];
        paramValues[7] = docs[4];
        paramValues[8] = fellows;
        paramValues[9] = tablerdn;
        paramValues[10] = tablename;
        paramValues[11] = tabletype;
        paramValues[12] = tablescore;
        paramValues[13] = mrn;
        paramValues[14] = pname;
        try
        {
            SqlConnect conn = new SqlConnect();
            string rr = conn.ExcuteUpdateCmd("[dbo].[insert_brifeInfo]", paramNames, paramValues).ToString();
            saveMediTable(gv, rr);
            return rr;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
        
    }

    public void getDetailInfo(string dept, string date1,string date2,GridView gv)
    {
        DataTable returndt, sourcedt;
        returndt = new DataTable();
        sourcedt = new DataTable();
        SqlConnect conn = new SqlConnect();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select JCI_detailInfo.*,dbo.JCI_brifeInfo.mrn,dbo.JCI_brifeInfo.pName from JCI_detailInfo,JCI_brifeInfo where applyRdn in( ");
        sb.Append("select rdn from JCI_brifeInfo where docdept='" + dept + "' and convert(nvarchar(10),fillindate,120)>='" + date1 + "' and convert(nvarchar(10),fillindate,120)<='" + date2 + "'");
        sb.Append(") and applyRdn=JCI_brifeInfo.Rdn and result<>'0' order by smallitemrdn asc");
        sourcedt = conn.ExcuteSelect(sb.ToString());
        int j = 0;
        returndt.Columns.Add("类别");
        returndt.Columns.Add("项目");
        returndt.Columns.Add("病历号");
        for (int i = 0; i < sourcedt.Rows.Count; i++)
        {
            DataRow drNow = sourcedt.Rows[i];
            if (i != 0)
            {
                DataRow drBefore = sourcedt.Rows[i - 1];
                if (drNow["smallItemRdn"].ToString() != drBefore["smallItemRdn"].ToString())//新行
                {
                    returndt.Rows.Add(drNow["largeItemName"], drNow["smallItemName"], drNow["mrn"]);
                    j++;
                }
                else
                {
                    returndt.Rows[j - 1][2] += "，"+drNow["mrn"].ToString();
                }
            }
            else//第一行
            {
                returndt.Rows.Add(drNow["largeItemName"], drNow["smallItemName"], drNow["mrn"]);
                j++;
            }
        }
        gv.DataSource = returndt;
        gv.DataBind();
    }
    public void getBrifeInfo(string dept, string date1,string date2,GridView gv)
    {
        date1 = "2010-04-01"; date2 = "2010-04-30";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select * from JCI_brifeInfo where docdept='" + dept + "'");
        sb.Append(" and convert(nvarchar(10),fillindate,120)>='" + date1 + "' and convert(nvarchar(10),fillindate,120)<='" + date2 + "'");
        SqlConnect conn = new SqlConnect();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        dt2.Columns.Add("编号");
        dt2.Columns.Add("attending/fellow");
        dt2.Columns.Add("病历号/姓名");
        dt2.Columns.Add("分数");
        dt1=conn.ExcuteSelect(sb.ToString());
        for (int i = 0; i < dt1.Rows.Count;i++ )
        {
            string[] o = new string[4];
            o[0] = (i+1).ToString();
            for (int j = 4; j < 9; j++)
            {
                if (dt1.Rows[i][j].ToString() != "") o[1] += dt1.Rows[i][j].ToString() + ",";
            }
            o[1] = o[1].Substring(0, o[1].Length - 1);
            o[1] += "/" + dt1.Rows[i]["fellows"].ToString();//加上fellow
            o[2] = dt1.Rows[i]["mrn"].ToString() +"/"+ dt1.Rows[i]["pName"].ToString();
            o[3] = dt1.Rows[i]["tableScore"].ToString();
            dt2.Rows.Add(o);
        }
        gv.DataSource = dt2;
        gv.DataBind();
    }



    public DataTable getShowData(string rdn)
    {
        try
        {
            string sql = "select * from dbo.JCI_brifeInfo where rdn=" + rdn;
            SqlConnect conn = new SqlConnect();
            return conn.ExcuteSelect(sql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
