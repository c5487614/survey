﻿using System;
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
///Ftable 的摘要说明
/// </summary>
public class Ftable
{
    public string tableName;
    public string tableType;
    public string tableRdn;
	public Ftable()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string orderstring()
    {
        return " order by sortid";
    }
}
