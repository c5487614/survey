using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///User 的摘要说明
/// </summary>
public class User
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
	public User()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
	public User Login(User u)
	{
		SqlConnect conn = new SqlConnect();
		string sql = "select * from JCI_empTable where 1=1 ";
		return null;
	}
}
