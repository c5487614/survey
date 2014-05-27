using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Manage_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	protected void btn_changePsw_Click(object sender, EventArgs e)
	{
		try
		{
			UserPower user = Session["user"] as UserPower;
			if (user == null)
			{
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('没有登录或会话过期！请重新登录');window.location.href='../../Admin_login.aspx';</script>");
				return;
			}
			if (!user.Psw.Equals(tbox_oldPassword.Value))
			{
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('原始密码不正确！');</script>");
				return;
			}
			bool bRet = false;
			string newPsw = tbox_newPassword1.Value;
			bRet = user.ChangePsw(user, newPsw);
			if (bRet)
			{
				user.Psw = newPsw;
				Session["user"] = user;
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "javascript", "<script>alert('密码修改成功！');window.location.href='Default.aspx';</script>");
				return;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.ToString());
		}
	}
}
