using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Admin_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
    }
    protected void btn_Login_Click(object sender, EventArgs e)
    {
        try
        {
            //Alter table dbo.JCI_newEmp add psw varchar(16)
            UserPower user = new UserPower();
            user.UserId = tb_UserId.Text;
            user.UserDept = tb_userDept.Text;
            user.Psw = tb_UserPsw.Text;
            user = user.Login(user);
            if (user != null)
            {
                Session["user"] = user;
                Response.Redirect("Patient/Manage/Default.aspx");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>用户名或密码错误！</script>");
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
}
