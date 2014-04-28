using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///QATable 的摘要说明
/// </summary>
public class QATable : Ftable
{
	public QATable()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        this.tableType = "QA";
	}
    public void saveDetailInfo(string rdn,GridView gv)
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
                sql.Append("insert into JCI_detailInfo([applyRdn],[largeItemRdn],[smallItemRdn],[result],[largeItemName],[smallItemName],[typefrom],[addContent])");
                sql.Append("values(" + rdn + ",");

                ControlCollection ccs = gv.Rows[i].Cells[0].Controls;//控件位置
                Label ll1 = ccs[1] as Label;
                Label ll2 = ccs[3] as Label;
                RadioButtonList rbl = ccs[5] as RadioButtonList;
                HiddenField hf1 = ccs[7] as HiddenField;
                HiddenField hf2 = ccs[9] as HiddenField;
                ccs = gv.Rows[i].Cells[1].Controls;
                TextBox addcontent = ccs[1] as TextBox;
                if (ll1 != null && ll2 != null && rbl != null && addcontent != null)
                {
                    sql.Append(hf1.Value + "," + hf2.Value);
                    sql.Append(",'" + rbl.SelectedValue + "','" + ll1.Text + "','" + ll2.Text + "'");
                }
                sql.Append(",'" + this.tableType + "'");
                sql.Append(",'" + addcontent.Text + "'");
                sql.Append(")");
                conn.ExcuteCmd(sql.ToString());
            }
            #endregion
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    public string saveBrifeInfo(string checkperson,string checkdate,string checkstair,string tablerdn,string tablename,string tabletype,string tablescore,GridView gv)
    {
        string[] paramNames = new string[7];
        string[] paramValues = new string[7];

        paramNames[0] = "@checkPerson";
        paramNames[1] = "@checkDate";
        paramNames[2] = "@checkStair";
        paramNames[3] = "@tableRdn";
        paramNames[4] = "@tableName";
        paramNames[5] = "@tableType";
        paramNames[6] = "@tableScore";


        paramValues[0] = checkperson;
        paramValues[1] = checkdate;
        paramValues[2] = checkstair;
        paramValues[3] = tablerdn;
        paramValues[4] = tablename;
        paramValues[5] = tabletype;
        paramValues[6] = tablescore;
        try
        {
            SqlConnect conn = new SqlConnect();
            string rdn = conn.ExcuteUpdateCmd("[dbo].[QA_insert_brifeInfo]", paramNames, paramValues).ToString();
            saveDetailInfo(rdn, gv);
            return "ok";
        }
        catch (Exception ex)
        {
            
            throw;
        }
        

    }


    //输入提示
    public string[] getDicContent(string keys)
    {

        string sql = "select top 20 dicItemContent from JCI_Dic where dicItemContent like '%" + keys + "%' or dicItemNickName like '%" + keys + "%'";
        SqlConnect conn = new SqlConnect();
        
        DataTable dt = conn.ExcuteSelect(sql);
        if (dt.Rows.Count == 0) return null;
        string[] content = new string[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            content[i] = dt.Rows[i][0].ToString();
        }
        return content;
    }
}
