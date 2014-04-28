using System;

/// <summary>
///srrshPage 的摘要说明
/// </summary>
public class srrshPage
{
	public srrshPage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
   
   public static bool Level1(string session)
   {
       string power=session.Split(',')[3].Split('.')[1];
       if(power=="管理员") return true;

       return false;
   }
   public static bool Level2(string session)
    {  
       string power=session.Split(',')[3].Split('.')[1];
       if (power == "部门管理员" || power == "管理员") return true;

       return false;
    }

   public static bool Level3(string session)
    {  
       string power=session.Split(',')[3].Split('.')[1];
       if(power=="一般员工") return true;

       return false;
    }
}
