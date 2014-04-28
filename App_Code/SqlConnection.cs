using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Sql数据库连接
/// </summary>
public class SqlConnect
{
    #region Fields/Attributes/Properties

    SqlConnection sqlConn = null;
    SqlCommand sqlCmd = null;
    //string sql_constr="Data Source=.;database=Club;uid=sa;password=cch;";
    string sql_constr = ConfigurationSettings.AppSettings["connStr"].ToString();
    #endregion

    #region Constructor

    public SqlConnect(string connectionString)
    {
        sqlConn = new SqlConnection(connectionString);
    }

    public SqlConnect()
    {
        sqlConn = new SqlConnection(sql_constr);
    }

    #endregion

    #region Sql Database Open/Close

    /// <summary>
    /// 打开Sql数据库
    /// </summary>
    /// <returns>成功返回true；未初始化返回false</returns>
    private bool DBOpen()
    {
        try
        {
            if (sqlConn == null)
            {
                return false;
            }
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 关闭Sql数据库
    /// </summary>
    /// <returns>成功返回true；未初始化返回false</returns>
    private bool DBClose()
    {
        try
        {
            if (sqlConn == null)
            {
                return false;
            }
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Sql Databse Select
    /// <summary>
    /// Sql数据库查询 Date Select
    /// </summary>
    /// <param name="selectProcedureName">查询存储过程名</param>
    /// <returns>成功返回DataTable类型数据；否则返回null</returns>
    public DataTable ExcuteSelectCmd(string selectProcedureName)
    {
        DataTable tbl = new DataTable();
        sqlCmd = new SqlCommand(selectProcedureName, sqlConn);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        try
        {
            if (!DBOpen())
                return null;
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(tbl);
            if (!DBClose())
                return null;
            return tbl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    ///  Sql数据库查询 Date Select
    /// </summary>
    /// <param name="sql">Select SQL语句</param>
    /// <returns>成功返回DataTable类型数据;否则返回null</returns>
    public DataTable ExcuteSelect(string sql)
    {
        DataTable dt = new DataTable();
        sqlCmd = new SqlCommand(sql, sqlConn);
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            if (!DBOpen())
                return null;
            SqlDataAdapter da = new SqlDataAdapter(sql, sqlConn);
            da.Fill(dt);
            da.Update(dt);
            if (!DBClose())
                return null;
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Sql Database Insert/Modify/Delete

    /// <summary>
    /// Sql数据库插入、修改、删除   Data Upadte
    /// </summary>
    /// <param name="updateProcedureName">更新存储过程名</param>
    /// <param name="parameterName">参数名</param>
    /// <param name="paramterValue">参数值</param>
    /// <returns>返回影响行数。</returns>
    public int ExcuteUpdateCmd(string updateProcedureName, string[] parameterName, string[] paramterValue)
    {
        int effectLines = -1;
        sqlCmd = new SqlCommand(updateProcedureName, sqlConn);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        try
        {
            if (!DBOpen())
                return -1;
            for (int i = 0; i < parameterName.Length; i++)
            {
                sqlCmd.Parameters.Add(parameterName[i], paramterValue[i]);
            }
            sqlCmd.Parameters.Add("@RETURN_VALUE",SqlDbType.Int);
            sqlCmd.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
            sqlCmd.ExecuteNonQuery();

            effectLines =(int)sqlCmd.Parameters["@RETURN_VALUE"].Value;
            DBClose();
            return effectLines;
        }
        catch (Exception ex)
        {
            DBClose();
            throw ex;
        }
    }

    /// <summary>
    /// Sql数据库插入、修改、删除   Data Update
    /// </summary>
    /// <param name="cmd">插入、修改、删除 语句</param>
    /// <returns>返回影响行数</returns>
    public int ExcuteCmd(string cmd)
    {
        int nRowChanged = 0;
        try
        {
            sqlConn.Open();
            sqlCmd = new SqlCommand(cmd, sqlConn);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = cmd;
            nRowChanged = sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
        }
        catch(Exception ex)
        {
			throw;
        }
        return nRowChanged;
    }
    #endregion


    #region cch
    public bool gv_bind(string sql, GridView gv)
    {
        gv.DataSource = ExcuteSelect(sql);
        gv.DataBind();
        return true;
    }
    public bool DropDownListBind(string sql, DropDownList ddl)
    {
        DataTable dt=new DataTable();
        dt=ExcuteSelect(sql);
        //dt.Rows.Add("999", "------请选择------");
        ddl.DataSource = dt;
        ddl.DataValueField = dt.Columns[0].ToString();
        ddl.DataTextField = dt.Columns[1].ToString();
        ddl.DataBind();
        dt.Dispose();
        return true;
    }
    //datatable写入数据库
    public bool tableMerge(DataTable dt, SqlCommand ccmd)
    {
        bool bResutl = false;
        try
        {
            ccmd.Connection = this.sqlConn;
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = ccmd;
            da.Update(dt);
            bResutl = true;
        }
        catch (Exception ex)
        {
            
            throw;
        }
        return bResutl;
        
    }
    #endregion
}

