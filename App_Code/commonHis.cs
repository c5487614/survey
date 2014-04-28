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
///commonHis 的摘要说明
/// </summary>
public class commonHis
{
	//private string programName;
	//private WebSc.Service_CommonClient sc = null;
	//public commonHis()
	//{
	//    sc = new WebSc.Service_CommonClient();
	//    sc.ClientCredentials.UserName.UserName = "srrshweb";
	//    sc.ClientCredentials.UserName.Password = "654dsdsf87031a4c4335d563ewqr055246d";
	//    Ehis.SOA.Client.MyUtil.SetCertificatePolicy();

	//    programName = "医院意外事件上报网";
	//}
	//public string Login(string uid,string psw)
	//{
	//    string loginReturn;
	//    try
	//    {
            
	//        loginReturn = sc.LogInWeb(uid, psw, this.programName);
	//    }
	//    catch (Exception ex)
	//    {
	//        throw ex;
	//    }
	//    return loginReturn;
	//}
	//public string searchNameBypy(string py)//拼音查询
	//{
	//    string name = null;
	//    try
	//    {
	//        return name = sc.GetUserList("%", py);
	//    }
	//    catch (Exception ex)
	//    {
            
	//        throw;
	//    }
	//    return null;
	//}
	//public DataTable getDeptList(string str)
	//{
	//    DataTable dt = new DataTable();
	//    dt.Columns.Add("id");
	//    dt.Columns.Add("deptName");
	//    try
	//    {
	//        string dept=sc.GetDeptList("%");
	//        string[] names = dept.Split(';');
	//        foreach (string n in names)
	//        {
	//            if(n!="")
	//                dt.Rows.Add(n.Split(',')[0].Trim(), n.Split(',')[1].Trim());
	//        }
	//    }
	//    catch (Exception ex)
	//    {
            
	//        throw;
	//    }
	//    return dt;
	//}
}
