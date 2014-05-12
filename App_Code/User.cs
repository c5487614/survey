using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///User 的摘要说明
/// </summary>
public class UserPower
{

    private string userName;

    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }
    private string userId;

    public string UserId
    {
        get { return userId; }
        set { userId = value; }
    }
    private string userDept;

    public string UserDept
    {
        get { return userDept; }
        set { userDept = value; }
    }
    private string userFloor;

    public string UserFloor
    {
        get { return userFloor; }
        set { userFloor = value; }
    }
    /// <summary>
    /// admin	all functions
    /// user	see dept & floor result 
    /// </summary>
    private string power;

    public string Power
    {
        get { return power; }
        set { power = value; }
    }

    private string psw;

    public string Psw
    {
        get { return psw; }
        set { psw = value; }
    }
    public UserPower()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    private string GetPower(string power)
    {
        string retPower = string.Empty;
        switch (power)
        {
            case "":
                break;
            default:
                break;
        }
        return retPower;
    }
    public UserPower Login(UserPower u)
    {
        SqlConnect conn = new SqlConnect();
        string sql = "select * from dbo.JCI_newEmp emp where 1=1 where emp.empId='" + u.UserId + "' and emp.deptNum='" + u.UserDept + "' and emp.psw='" + u.Psw + "'";
        DataTable dt = conn.ExcuteSelect(sql);
        if (dt.Rows.Count >= 0) 
        {
            u.Power = GetPower(dt.Rows[0]["power"].ToString());
            return u;
        }
        return null;
       
    }
}
