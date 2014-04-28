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
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

/// <summary>
///PatientTable 的摘要说明
/// </summary>
public class PatientTable :Ftable
{
	public PatientTable()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        this.tableType = "Patient";
        this.tableRdn = "12";
        this.tableName = "邵逸夫医院住院病人满意度调查";
	}
    public string saveBrifeInfo(string fillperson, string isfirst, string personSex, string regdate, string regdept, string job, string other1,string other2,string tablescore,string payType,string dept,string floorName,string promote,string needImprove, GridView gv)
    {
        string[] paramNames = new string[17];
        string[] paramValues = new string[17];

        paramNames[0] = "@fillPerson";
        paramNames[1] = "@isFirst";
        paramNames[2] = "@personSex";
        paramNames[3] = "@regDate";
        paramNames[4] = "@regDept";
        paramNames[5] = "@job";
        paramNames[6] = "@other1";
        paramNames[7] = "@other2";
        paramNames[8] = "@tableScore";
        paramNames[9] = "@tableRdn";
        paramNames[10] = "@tableName";
        paramNames[11] = "@tableType";
		paramNames[12] = "@payType";
		paramNames[13] = "@dept";
		paramNames[14] = "@floorName";
		paramNames[15] = "@promote";
		paramNames[16] = "@needImprove";



        paramValues[0] = fillperson;
        paramValues[1] = isfirst;
        paramValues[2] = personSex;
        paramValues[3] = regdate;
        paramValues[4] = regdept;
        paramValues[5] = job;
        paramValues[6] = other1;
        paramValues[7] = other2;
        paramValues[8] = tablescore;
        paramValues[9] = this.tableRdn;
        paramValues[10] = this.tableName;
        paramValues[11] = this.tableType;
		paramValues[12] = payType;
		paramValues[13] = dept;
		paramValues[14] = floorName;
		paramValues[15] = promote;
		paramValues[16] = needImprove;


        try
        {
            SqlConnect conn = new SqlConnect();
            string rdn = conn.ExcuteUpdateCmd("[dbo].[Patient_insert_brifeInfo]", paramNames, paramValues).ToString();
            saveDetailInfo(rdn, gv);
            return "ok";
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void saveDetailInfo(string rdn, GridView gv)
    {
		System.Text.StringBuilder sql = new System.Text.StringBuilder();
        try
        {
            int i;
            SqlConnect conn = new SqlConnect();
            
            #region 遍历
            for (i = 0; i < gv.Rows.Count; i++)
            {
                sql.Remove(0, sql.Length);
                sql.Append("insert into JCI_detailInfo([applyRdn],[largeItemRdn],[smallItemRdn],[result],[largeItemName],[smallItemName],[typefrom],[sortId])");
                sql.Append("values(" + rdn + ",");

                ControlCollection ccs = gv.Rows[i].Cells[0].Controls;//控件位置
                Label ll1 = ccs[1] as Label;
                Label ll2 = ccs[5] as Label;
                RadioButtonList rbl = ccs[7] as RadioButtonList;
                HiddenField hf1 = ccs[9] as HiddenField;
                HiddenField hf2 = ccs[11] as HiddenField;
				HiddenField hf3 = ccs[13] as HiddenField;
                if (ll1 != null && ll2 != null && rbl != null)
                {
                    sql.Append(hf1.Value + "," + hf2.Value);
                    sql.Append(",'" + rbl.SelectedValue + "','" + ll1.Text + "','" + ll2.Text + "'");
                }
                sql.Append(",'" + this.tableType + "'");
				sql.Append("," + hf3.Value);
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

	public bool Delete(string rdn)
	{
		try
		{
			string sql = "delete from dbo.JCI_patient_brifeInfo where rdn = " + rdn;
			SqlConnect conn = new SqlConnect();
			conn.ExcuteCmd(sql);
			sql = "delete from dbo.JCI_detailInfo where typefrom='" + this.tableType + "' and applyRdn = " + rdn;
			conn.ExcuteCmd(sql);
		}
		catch (Exception ex)
		{
			throw;
		}
		return true;
	}
	#region Export All Data
	public string ExportAll(string fileNamePath,DateTime beginDate,DateTime endDate)
	{
		HSSFWorkbook hssfworkbook = new HSSFWorkbook();
		hssfworkbook.CreateSheet("Data");
		ExportAllHead(hssfworkbook);
		ExportAllData(hssfworkbook, beginDate, endDate);
		FileStream file = new FileStream(fileNamePath, FileMode.Create);
		hssfworkbook.Write(file);
		file.Close();

		return "ok";
	}
	private void ExportAllHead(HSSFWorkbook hssfworkbook)
	{
		ISheet sheet = hssfworkbook.GetSheetAt(0);
		IRow row;
		ICell cell;
		row = sheet.CreateRow(0);
		int iCell = 0;
		#region
		
		//cell = row.CreateCell(iCell++, CellType.String);
		//cell.SetCellValue("一般信息");
		CellRangeAddress cellrange = new CellRangeAddress(0, 0, 0, 8);
		MergeAreaBorder(hssfworkbook, sheet, cellrange, "一般信息");
		sheet.AddMergedRegion(cellrange);
		//cell = row.CreateCell(iCell++, CellType.String);
		//cell.SetCellValue("医疗工作");
		cellrange = new CellRangeAddress(0, 0, 9, 18);
		MergeAreaBorder(hssfworkbook, sheet, cellrange, "医疗工作");
		sheet.AddMergedRegion(cellrange);


		//cell = row.CreateCell(iCell++, CellType.String);
		//cell.SetCellValue("护理工作");
		//cellrange = new CellRangeAddress(0, 0, 19, 31);
		//sheet.AddMergedRegion(cellrange);

		cellrange = new CellRangeAddress(0, 0, 19, 31);
		MergeAreaBorder(hssfworkbook, sheet, cellrange, "护理工作");
		sheet.AddMergedRegion(cellrange);

		//cell = row.CreateCell(iCell++, CellType.String);
		//cell.SetCellValue("医技工作");
		//cellrange = new CellRangeAddress(0, 0, 32, 37);
		//sheet.AddMergedRegion(cellrange);
		cellrange = new CellRangeAddress(0, 0, 32, 37);
		MergeAreaBorder(hssfworkbook, sheet, cellrange, "医技工作");
		sheet.AddMergedRegion(cellrange);

		//cell = row.CreateCell(iCell++, CellType.String);
		//cell.SetCellValue("后勤工作");
		//cellrange = new CellRangeAddress(0, 0, 38, 44);
		//sheet.AddMergedRegion(cellrange);
		cellrange = new CellRangeAddress(0, 0, 38, 44);
		MergeAreaBorder(hssfworkbook, sheet, cellrange, "后勤工作");
		sheet.AddMergedRegion(cellrange);

		//cell = row.CreateCell(iCell++, CellType.String);
		//cell.SetCellValue("住院费用相关内容");
		//cellrange = new CellRangeAddress(0, 0, 45, 46);
		//sheet.AddMergedRegion(cellrange);
		cellrange = new CellRangeAddress(0, 0, 45, 46);
		MergeAreaBorder(hssfworkbook, sheet, cellrange, "住院费用相关内容");
		sheet.AddMergedRegion(cellrange);
		//cell = row.CreateCell(iCell++, CellType.String);
		//cell.SetCellValue("满意以及意见或建议");
		//cellrange = new CellRangeAddress(0, 0, 47, 55);
		//sheet.AddMergedRegion(cellrange);
		cellrange = new CellRangeAddress(0, 0, 47, 55);
		MergeAreaBorder(hssfworkbook, sheet, cellrange, "满意以及意见或建议");
		sheet.AddMergedRegion(cellrange);
		#endregion
		#region normal head
		row = sheet.CreateRow(1);
		iCell = 0;
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("编号");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("录入时间");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("填表人");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("是否第一次住院");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("住院情况");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("性别");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("入院时间");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("职业");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("付费方式");
		#endregion
		#region DataBase Head
		SqlConnect conn = new SqlConnect();
		string sql = "select smallItemName from dbo.JCI_generateTable where tablerdn=12 order by largeItemRdn ASC, sortID ASC ";
		DataTable dt = conn.ExcuteSelect(sql);

		foreach (DataRow dr in dt.Rows)
		{
			cell = row.CreateCell(iCell++, CellType.String);
			cell.SetCellValue(dr["smallItemName"].ToString());
		}
		#endregion
		#region promote & needImprove
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("住院期间您最满意的人和事（医疗工作）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("本院需要改进的方面（医疗工作）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("住院期间您最满意的人和事（护理工作）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("本院需要改进的方面（护理工作）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("住院期间您最满意的人和事（医技工作）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("本院需要改进的方面（医技工作）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("住院期间您最满意的人和事（后勤工作）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("本院需要改进的方面（后勤工作）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("住院期间您最满意的人和事（住院费用相关）");
		cell = row.CreateCell(iCell++);
		cell.SetCellValue("本院需要改进的方面（住院费用相关）");
		#endregion


	}
	private void ExportAllData(HSSFWorkbook hssfworkbook, DateTime beginDate, DateTime endDate)
	{
		SqlConnect conn = new SqlConnect();
		string sql = "select * from dbo.JCI_patient_brifeInfo brifeInfo " +
			"where brifeInfo.regDept>'" + beginDate.ToString("yyyy-MM-dd") + "' and brifeInfo.regDept<'" + endDate.ToString("yyyy-MM-dd") + "' " +
			"order by rdn asc";
		DataTable dt = conn.ExcuteSelect(sql);
		ISheet sheet = hssfworkbook.GetSheetAt(0);
		IRow row;
		ICell cell;
		int iRow = 2;
		int iCell = 0;
		foreach (DataRow dr in dt.Rows)
		{
			iCell = 0;
			#region normal information
			row = sheet.CreateRow(iRow++);
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(dr["rdn"].ToString());
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(Convert.ToDateTime(dr["other2"]).ToString("yyyyMMdd"));
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(dr["fillPerson"].ToString());
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(dr["isFirst"].ToString());
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(dr["other1"].ToString());
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(dr["personSex"].ToString());
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(dr["regDate"].ToString());
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(dr["job"].ToString());
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(dr["payType"].ToString());
			#endregion
			#region detail information
			sql = "select * from dbo.JCI_detailInfo where typefrom='Patient' and  applyRdn=" + dr["rdn"].ToString();
			DataTable dt1 = conn.ExcuteSelect(sql);
			foreach (DataRow dr1 in dt1.Rows)
			{
				cell = row.CreateCell(iCell++);
				string result = dr1["result"].ToString();
				string cellValue = "";
				switch (result)
				{
					case "-1":
						cellValue = "无此需求";
						break;
					case "-2":
						cellValue = "缺项";
						break;
					case "10":
						cellValue = "满意";
						break;
					case "7":
						cellValue = "比较满意";
						break;
					case "5":
						cellValue = "一般";
						break;
					case "0":
						cellValue = "不满意";
						break;
					default:
						cellValue = "缺项";
						break;
				}
				cell.SetCellValue(cellValue);
				//cell.SetCellValue(dr["payType"].ToString());
			}
			#endregion
			#region promote & improve
			string promote = dr["promote"].ToString();
			string needImprove = dr["needImprove"].ToString();

			string[] promotes = promote.Split(';');
			string[] needImproves = needImprove.Split(';');
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(promotes[0].Split(':')[1]);
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(needImproves[0].Split(':')[1]);

			cell = row.CreateCell(iCell++);
			cell.SetCellValue(promotes[1].Split(':')[1]);
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(needImproves[1].Split(':')[1]);

			cell = row.CreateCell(iCell++);
			cell.SetCellValue(promotes[2].Split(':')[1]);
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(needImproves[2].Split(':')[1]);

			cell = row.CreateCell(iCell++);
			cell.SetCellValue(promotes[3].Split(':')[1]);
			cell = row.CreateCell(iCell++);
			cell.SetCellValue(needImproves[3].Split(':')[1]);
			#endregion
		}
	}
	#endregion
	#region Export Sumup
	public string Exportyl(string fileNamePath,DateTime beginDate,DateTime endDate,string sqlWhere)
	{
		string largeItemRdn = "1";
		DateTime date = DateTime.Now;
		HSSFWorkbook hssfworkbook = new HSSFWorkbook();
		hssfworkbook.CreateSheet("医疗");
		PatientReportHead reportHead = new PatientReportHead();
		reportHead.headName="住院病人满意度调查";
		reportHead.reportDate = DateTime.Now.ToString("yyyy.MM");
		reportHead.reportType = "医疗工作";
		HSSFHead(reportHead, hssfworkbook);

		int iCurrentRow = 0;
		#region sumup of the answer
		DataTable dt = new DataTable();
		string result = ReportHandler(largeItemRdn, beginDate, endDate, ref dt, sqlWhere);
		if (!result.Equals("ok")) return result;
		HSSFAnswer(hssfworkbook, dt, ref iCurrentRow);
		#endregion
		#region sumup of the advice
		DataTable dtAdvice = ExportAdviceTable(PatientAdviceDept.YL, beginDate, endDate, sqlWhere);
		HSSFPromote(hssfworkbook, dtAdvice,ref iCurrentRow);
		#endregion

		FileStream file = new FileStream(fileNamePath, FileMode.Create);
		hssfworkbook.Write(file);
		file.Close();

		return "ok";
	}
	public string Exporthl(string fileNamePath, DateTime beginDate, DateTime endDate, string sqlWhere)
	{
		string largeItemRdn = "2";
		DateTime date = DateTime.Now;
		HSSFWorkbook hssfworkbook = new HSSFWorkbook();
		hssfworkbook.CreateSheet("护理");
		PatientReportHead reportHead = new PatientReportHead();
		reportHead.headName = "住院病人满意度调查";
		reportHead.reportDate = DateTime.Now.ToString("yyyy.MM");
		reportHead.reportType = "护理工作";
		HSSFHead(reportHead, hssfworkbook);

		int iCurrentRow = 0;
		#region sumup of the answer
		DataTable dt = new DataTable();
		string result = ReportHandler(largeItemRdn, beginDate, endDate, ref dt, sqlWhere);
		if (!result.Equals("ok")) return result;
		HSSFAnswer(hssfworkbook, dt, ref iCurrentRow);
		#endregion
		#region sumup of the advice
		DataTable dtAdvice = ExportAdviceTable(PatientAdviceDept.HL, beginDate, endDate,sqlWhere);
		HSSFPromote(hssfworkbook, dtAdvice, ref iCurrentRow);
		#endregion

		FileStream file = new FileStream(fileNamePath, FileMode.Create);
		hssfworkbook.Write(file);
		file.Close();
		return "ok";
	}
	public string Exportyj(string fileNamePath, DateTime beginDate, DateTime endDate, string sqlWhere)
	{
		string largeItemRdn = "3";
		DateTime date = DateTime.Now;
		HSSFWorkbook hssfworkbook = new HSSFWorkbook();
		hssfworkbook.CreateSheet("医技");
		PatientReportHead reportHead = new PatientReportHead();
		reportHead.headName = "住院病人满意度调查";
		reportHead.reportDate = DateTime.Now.ToString("yyyy.MM");
		reportHead.reportType = "医技工作";
		HSSFHead(reportHead, hssfworkbook);

		int iCurrentRow = 0;
		#region sumup of the answer
		DataTable dt = new DataTable();
		string result = ReportHandler(largeItemRdn, beginDate, endDate, ref dt,sqlWhere);
		if (!result.Equals("ok")) return result;
		HSSFAnswer(hssfworkbook, dt, ref iCurrentRow);
		#endregion
		#region sumup of the advice
		DataTable dtAdvice = ExportAdviceTable(PatientAdviceDept.YJ, beginDate, endDate,sqlWhere);
		HSSFPromote(hssfworkbook, dtAdvice, ref iCurrentRow);
		#endregion

		FileStream file = new FileStream(fileNamePath, FileMode.Create);
		hssfworkbook.Write(file);
		file.Close();
		return "ok";
	}
	public string Exporthq(string fileNamePath, DateTime beginDate, DateTime endDate, string sqlWhere)
	{
		string largeItemRdn = "4";
		DateTime date = DateTime.Now;
		HSSFWorkbook hssfworkbook = new HSSFWorkbook();
		hssfworkbook.CreateSheet("后勤");
		PatientReportHead reportHead = new PatientReportHead();
		reportHead.headName = "住院病人满意度调查";
		reportHead.reportDate = DateTime.Now.ToString("yyyy.MM");
		reportHead.reportType = "后勤工作";
		HSSFHead(reportHead, hssfworkbook);

		int iCurrentRow = 0;
		#region sumup of the answer
		DataTable dt = new DataTable();
		string result = ReportHandler(largeItemRdn, beginDate, endDate, ref dt,sqlWhere);
		if (!result.Equals("ok")) return result;
		HSSFAnswer(hssfworkbook, dt, ref iCurrentRow);
		#endregion
		#region sumup of the advice
		DataTable dtAdvice = ExportAdviceTable(PatientAdviceDept.HQ, beginDate, endDate, sqlWhere);
		HSSFPromote(hssfworkbook, dtAdvice, ref iCurrentRow);
		#endregion

		FileStream file = new FileStream(fileNamePath, FileMode.Create);
		hssfworkbook.Write(file);
		file.Close();
		return "ok";
	}
	public string Exportzy(string fileNamePath, DateTime beginDate, DateTime endDate, string sqlWhere)
	{
		string largeItemRdn = "5";
		DateTime date = DateTime.Now;
		HSSFWorkbook hssfworkbook = new HSSFWorkbook();
		hssfworkbook.CreateSheet("行政");
		PatientReportHead reportHead = new PatientReportHead();
		reportHead.headName = "住院病人满意度调查";
		reportHead.reportDate = DateTime.Now.ToString("yyyy.MM");
		reportHead.reportType = "行政工作";
		HSSFHead(reportHead, hssfworkbook);

		int iCurrentRow = 0;
		#region sumup of the answer
		DataTable dt = new DataTable();
		string result = ReportHandler(largeItemRdn, beginDate, endDate, ref dt, sqlWhere);
		if (!result.Equals("ok")) return result;
		HSSFAnswer(hssfworkbook, dt, ref iCurrentRow);
		#endregion
		#region sumup of the advice
		DataTable dtAdvice = ExportAdviceTable(PatientAdviceDept.ZY, beginDate, endDate,sqlWhere);
		HSSFPromote(hssfworkbook, dtAdvice, ref iCurrentRow);
		#endregion

		FileStream file = new FileStream(fileNamePath, FileMode.Create);
		hssfworkbook.Write(file);
		file.Close();
		return "ok";
	}
	#endregion
	private void HSSFGraph(HSSFWorkbook hssfworkbook)
	{
 
	}
	private void HSSFPromote(HSSFWorkbook hssfworkbook, DataTable dt,ref int iCurrentRow)
	{
		ISheet sheet = hssfworkbook.GetSheetAt(0);
		IRow row = sheet.CreateRow(iCurrentRow++);
		#region prmote
		ICellStyle style = GetNormalStyle(hssfworkbook);
		style.Alignment = HorizontalAlignment.General;
		style.VerticalAlignment = VerticalAlignment.Center;
		style.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
		style.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
		style.GetFont(hssfworkbook).Boldweight = (short)FontBoldWeight.Bold;
		ICell cell = row.CreateCell(0);
		cell.CellStyle = style;
		cell.SetCellValue("满意：");
		//line number
		int iCount = 1;
		//dr["rdn"], dr["dept"], dr["floorName"], adviceDept, "adviceType", adviceContent
		
		foreach (DataRow dr in dt.Rows)
		{
			if (dr["adviceType"].ToString().Equals("promote"))
			{
				row = sheet.CreateRow(iCurrentRow++);
				style = GetNormalStyle(hssfworkbook);
				style.Alignment = HorizontalAlignment.Center;
				style.VerticalAlignment = VerticalAlignment.Justify;
				style.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
				style.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
				cell = row.CreateCell(0, CellType.Numeric);
				cell.CellStyle = style;
				cell.SetCellValue(iCount);
				iCount++;
				style = GetNormalStyle(hssfworkbook);
				style.Alignment = HorizontalAlignment.General;
				style.VerticalAlignment = VerticalAlignment.Center;
				style.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
				style.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
				cell = row.CreateCell(1, CellType.String);
				cell.CellStyle = style;
				string promote = dr["floorName"].ToString() + "：" + dr["adviceContent"].ToString();
				if (promote.Length > 45)
				{
					cell.SetCellValue(promote.Substring(0, 45));
					row = sheet.CreateRow(iCurrentRow++);
					cell = row.CreateCell(1, CellType.String);
					cell.CellStyle = style;
					cell.SetCellValue(promote.Substring(45, promote.Length-45));
				}
				else
				{
					cell.SetCellValue(promote);
				}
				
			}
		}
		#endregion
		#region need improve
		row = sheet.CreateRow(iCurrentRow++);
		style = GetNormalStyle(hssfworkbook);
		style.Alignment = HorizontalAlignment.General;
		style.VerticalAlignment = VerticalAlignment.Center;
		style.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
		style.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
		style.GetFont(hssfworkbook).Boldweight = (short)FontBoldWeight.Bold;
		cell = row.CreateCell(0);
		cell.CellStyle = style;
		cell.SetCellValue("意见或建议：");
		iCount = 1;
		foreach (DataRow dr in dt.Rows)
		{
			if (dr["adviceType"].ToString().Equals("needImprove"))
			{
				row = sheet.CreateRow(iCurrentRow++);
				style = GetNormalStyle(hssfworkbook);
				style.Alignment = HorizontalAlignment.Center;
				style.VerticalAlignment = VerticalAlignment.Justify;
				style.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
				style.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
				cell = row.CreateCell(0, CellType.Numeric);
				cell.CellStyle = style;
				cell.SetCellValue(iCount);
				iCount++;
				style = GetNormalStyle(hssfworkbook);
				style.Alignment = HorizontalAlignment.General;
				style.VerticalAlignment = VerticalAlignment.Center;
				style.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
				style.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
				cell = row.CreateCell(1, CellType.String);
				cell.CellStyle = style;
				//cell.SetCellValue(dr["floorName"].ToString() + "：" + dr["adviceContent"].ToString());
				string needImprove = dr["floorName"].ToString() + "：" + dr["adviceContent"].ToString();
				if (needImprove.Length > 45)
				{
					cell.SetCellValue(needImprove.Substring(0, 45));
					row = sheet.CreateRow(iCurrentRow++);
					cell = row.CreateCell(1, CellType.String);
					cell.CellStyle = style;
					cell.SetCellValue(needImprove.Substring(45, needImprove.Length - 45));
				}
				else
				{
					cell.SetCellValue(needImprove);
				}
			}
		}
		#endregion

	}
	private void HSSFAnswer(HSSFWorkbook hssfworkbook, DataTable dt,ref int iCurrentRow)
	{
		ISheet sheet = hssfworkbook.GetSheetAt(0);
		int iRow = 8,iColumn = 0;
		IRow row;
		ICell cell;
		ICellStyle style = GetNormalStyle(hssfworkbook);
		IFont font = style.GetFont(hssfworkbook);
		font.FontHeightInPoints = 11;
		font.FontName = "Times New Roman";
		//style.VerticalAlignment = VerticalAlignment.Bottom;
		style.Alignment = HorizontalAlignment.General;
		style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
		style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
		foreach (DataRow dr in dt.Rows)
		{
			ICellStyle styleCell1 = GetNormalStyle(hssfworkbook);
			font = styleCell1.GetFont(hssfworkbook);
			font.FontHeightInPoints = 11;
			font.FontName = "Times New Roman";
			//style.VerticalAlignment = VerticalAlignment.Bottom;
			styleCell1.Alignment = HorizontalAlignment.General;
			styleCell1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
			styleCell1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

			row = sheet.CreateRow(iRow++);
			iColumn = 0;
			cell = row.CreateCell(iColumn++, CellType.Numeric);
			cell.CellStyle = styleCell1;
			cell.CellStyle.Alignment = HorizontalAlignment.Center;
			cell.CellStyle.VerticalAlignment = VerticalAlignment.Justify;
			cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Double;
			ICellStyle styleTxt = GetNormalStyle(hssfworkbook);
			if (Convert.ToInt32(dr[0]) == 1001 || Convert.ToInt32(dr[0]) == 1002)
			{
				//cell.SetCellValue(" ");
				//string value = dr[0].ToString();// +".";
				//cell.SetCellValue(Convert.ToInt32(dr[0]));
				CellRangeAddress cellrange = new CellRangeAddress(iRow - 1, iRow - 1, 0, 1);
				MergeAreaBorder(hssfworkbook, sheet, cellrange, dr[1].ToString());
				sheet.AddMergedRegion(cellrange);
				iColumn++;
			}
			else
			{
				cell.SetCellValue(Convert.ToInt32(dr[0]));
				cell = row.CreateCell(iColumn++, CellType.String);
				styleTxt.Alignment = HorizontalAlignment.General;
				styleTxt.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
				styleTxt.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
				cell.CellStyle = styleTxt;
				cell.SetCellValue(dr[1].ToString());
			}
			for (; iColumn < dt.Columns.Count - 1; iColumn++)
			{
				cell = row.CreateCell(iColumn, CellType.Numeric);
				style.Alignment = HorizontalAlignment.Center;
				style.VerticalAlignment = VerticalAlignment.Justify;
				cell.CellStyle = style;
				if (Convert.ToInt32(dr[0]) == 1001 || Convert.ToInt32(dr[0]) == 1002)
				{
					styleTxt = GetHeaderStyle(hssfworkbook);
					styleTxt.BorderBottom = NPOI.SS.UserModel.BorderStyle.Double;
					styleTxt.BorderTop = NPOI.SS.UserModel.BorderStyle.Double;
					styleTxt.GetFont(hssfworkbook).Boldweight = (short)FontBoldWeight.Normal;
					//styleTxt.DataFormat = 0xa;
					cell.CellStyle = styleTxt;
					if (Convert.ToInt32(dr[0]) == 1002)
					{
						HSSFDataFormat formatPercent = (NPOI.HSSF.UserModel.HSSFDataFormat)hssfworkbook.CreateDataFormat();
						cell.CellStyle.DataFormat = formatPercent.GetFormat("0.0%");
					}
				}
				cell.SetCellValue(Convert.ToDouble(dr[iColumn]));
			}
			cell = row.CreateCell(iColumn);
			styleTxt = GetHeaderStyle(hssfworkbook);
			styleTxt.GetFont(hssfworkbook).Boldweight = (short)FontBoldWeight.Normal;
			styleTxt.BorderBottom = NPOI.SS.UserModel.BorderStyle.Double;
			styleTxt.BorderTop = NPOI.SS.UserModel.BorderStyle.Double;
			styleTxt.BorderLeft = NPOI.SS.UserModel.BorderStyle.Double;
			styleTxt.BorderRight = NPOI.SS.UserModel.BorderStyle.Double;
			cell.CellStyle = styleTxt;
			if (Convert.ToInt32(dr[0]) == 1002)
			{
				HSSFDataFormat formatPercent = (NPOI.HSSF.UserModel.HSSFDataFormat)hssfworkbook.CreateDataFormat();
				cell.CellStyle.DataFormat = formatPercent.GetFormat("0.0%");
				cell.SetCellValue(Convert.ToDouble(dr[iColumn]));
			}
			else
			{
				cell.SetCellValue(Convert.ToDouble(dr[iColumn]) * 100);
			}
			
		}
		iCurrentRow = iRow;
	}
	private void HSSFHead(PatientReportHead reportHead,HSSFWorkbook hssfworkbook)
	{
		ISheet sheet = hssfworkbook.GetSheetAt(0);
		sheet.SetColumnWidth(0, 5 * 256);
		sheet.SetColumnWidth(1, 50*256);
		sheet.SetColumnWidth(2, 8 * 256);
		sheet.SetColumnWidth(3, 8 * 256);
		sheet.SetColumnWidth(4, 8 * 256);
		sheet.SetColumnWidth(5, 8 * 256);
		sheet.SetColumnWidth(6, 8 * 256);
		sheet.SetColumnWidth(7, 8 * 256);
		IRow row = sheet.CreateRow(0);
		row.HeightInPoints = 50;
		sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));

		#region MergeHeader
		ICell cell = row.CreateCell(0, CellType.String);
		IFont font = hssfworkbook.CreateFont();
		font.FontName = "宋体";
		font.FontHeightInPoints = 16;
		font.Boldweight = (short)FontBoldWeight.Bold;
		
		ICellStyle style = hssfworkbook.CreateCellStyle();
		style.SetFont(font);
		style.Alignment = HorizontalAlignment.Center;
		style.VerticalAlignment = VerticalAlignment.Justify;

		cell.SetCellValue(reportHead.headName);
		cell.CellStyle = style;
		
		font = hssfworkbook.CreateFont();
		font.FontName = "宋体";
		font.FontHeightInPoints = 12;
		font.Boldweight = (short)FontBoldWeight.Bold;
		style = hssfworkbook.CreateCellStyle();
		style.SetFont(font);
		style.Alignment = HorizontalAlignment.Center;
		style.VerticalAlignment = VerticalAlignment.Center;

		row = sheet.CreateRow(1);
		cell = row.CreateCell(0, CellType.String);
		//cell.SetCellValue(reportHead.reportDate);
		cell.CellStyle = style;
		sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, 7));
		MergeAreaBorder(hssfworkbook, sheet, new CellRangeAddress(1, 1, 0, 7), reportHead.reportDate);
		
		font = hssfworkbook.CreateFont();
		font.FontName = "宋体";
		font.FontHeightInPoints = 14;
		font.Boldweight = (short)FontBoldWeight.Bold;
		style = hssfworkbook.CreateCellStyle();
		style.SetFont(font);
		
		style.Alignment = HorizontalAlignment.Center;
		style.VerticalAlignment = VerticalAlignment.Justify;

		row = sheet.CreateRow(2);
		cell = row.CreateCell(0, CellType.String);
		//cell.SetCellValue(reportHead.reportType);
		cell.CellStyle = style;
		sheet.AddMergedRegion(new CellRangeAddress(2, 7, 0, 1));
		MergeAreaBorder(hssfworkbook, sheet, new CellRangeAddress(2, 7, 0, 1), reportHead.reportType);
		#endregion

		#region Header
		ICellStyle normalStyle = GetHeaderStyle(hssfworkbook);
		WriteHeadTxt("无", normalStyle, sheet, 2, 2);
		WriteHeadTxt("满", normalStyle, sheet, 2, 3);
		WriteHeadTxt("比", normalStyle, sheet, 2, 4);
		WriteHeadTxt("一", normalStyle, sheet, 2, 5);
		WriteHeadTxt("不", normalStyle, sheet, 2, 6);
		WriteHeadTxt("此", normalStyle, sheet, 3, 2);
		WriteHeadTxt("意", normalStyle, sheet, 3, 3);
		WriteHeadTxt("较", normalStyle, sheet, 3, 4);
		WriteHeadTxt("般", normalStyle, sheet, 3, 5);
		WriteHeadTxt("满", normalStyle, sheet, 3, 6);
		WriteHeadTxt("需", normalStyle, sheet, 4, 2);
		WriteHeadTxt("  ", normalStyle, sheet, 4, 3);
		WriteHeadTxt("满", normalStyle, sheet, 4, 4);
		WriteHeadTxt("  ", normalStyle, sheet, 4, 5);
		WriteHeadTxt("意", normalStyle, sheet, 4, 6);
		WriteHeadTxt("要", normalStyle, sheet, 5, 2);
		WriteHeadTxt("  ", normalStyle, sheet, 5, 3);
		WriteHeadTxt("意", normalStyle, sheet, 5, 4);
		WriteHeadTxt("  ", normalStyle, sheet, 5, 5);
		WriteHeadTxt("  ", normalStyle, sheet, 5, 6);
		WriteHeadTxt("/", normalStyle, sheet, 6, 2);
		WriteHeadTxt("10分", normalStyle, sheet, 6, 3);
		WriteHeadTxt("7分", normalStyle, sheet, 6, 4);
		WriteHeadTxt("5分", normalStyle, sheet, 6, 5);
		WriteHeadTxt("0分", normalStyle, sheet, 6, 6);
		WriteHeadTxt("人次", normalStyle, sheet, 7, 2);
		WriteHeadTxt("人次", normalStyle, sheet, 7, 3);
		WriteHeadTxt("人次", normalStyle, sheet, 7, 4);
		WriteHeadTxt("人次", normalStyle, sheet, 7, 5);
		WriteHeadTxt("人次", normalStyle, sheet, 7, 6);
		#endregion
		CellRangeAddress cellrange = new CellRangeAddress(2, 7, 7, 7);
		sheet.AddMergedRegion(cellrange);
		MergeAreaBorder(hssfworkbook, sheet, cellrange, "得分率");
		

	}
	
	private void MergeAreaBorder(HSSFWorkbook hssfworkbook,ISheet sheet, CellRangeAddress cellrange, string value)
	{
		IFont font = hssfworkbook.CreateFont();
		font.FontName = "宋体";
		font.FontHeightInPoints = 12;
		font.Boldweight = (short)FontBoldWeight.Bold;
		ICellStyle style = hssfworkbook.CreateCellStyle();
		style.SetFont(font);
		style.Alignment = HorizontalAlignment.Center;
		style.VerticalAlignment = VerticalAlignment.Center;
		style.WrapText = false;
		style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Double;
		style.BorderTop = NPOI.SS.UserModel.BorderStyle.Double;
		style.BorderRight = NPOI.SS.UserModel.BorderStyle.Double;
		style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Double;
		IRow row;
		ICell cell;
		for (int i = cellrange.FirstRow; i <= cellrange.LastRow; i++)
		{
			row = sheet.GetRow(i);
			if (row == null)
			{
				row = sheet.CreateRow(i);
			}
			for (int j = cellrange.FirstColumn; j <= cellrange.LastColumn; j++)
			{
				cell = row.CreateCell(j);
				cell.CellStyle = style;
				cell.SetCellValue(value);
			}
		}
	}

	private void WriteHeadTxt(string cellTxt, ICellStyle style, ISheet sheet, int  iRow, int iColumn)
	{
		IRow row = sheet.GetRow(iRow);
		if (row == null)
		{
			row = sheet.CreateRow(iRow);
		}
		ICell cell = row.CreateCell(iColumn, CellType.String);
		cell.CellStyle = style;
		cell.SetCellValue(cellTxt);
	}
	private ICellStyle GetNormalStyle(HSSFWorkbook hssfworkbook)
	{
		IFont font = hssfworkbook.CreateFont();
		font.FontName = "宋体";
		font.FontHeightInPoints = 11;
		//font.Boldweight = (short)FontBoldWeight.Bold;
		ICellStyle style = hssfworkbook.CreateCellStyle();
		style.SetFont(font);
		style.Alignment = HorizontalAlignment.Center;
		style.VerticalAlignment = VerticalAlignment.Justify;
		style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
		style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
		return style;
	}
	private ICellStyle GetHeaderStyle(HSSFWorkbook hssfworkbook)
	{
		IFont font = hssfworkbook.CreateFont();
		font.FontName = "宋体";
		font.FontHeightInPoints = 11;
		font.Boldweight = (short)FontBoldWeight.Bold;
		ICellStyle style = hssfworkbook.CreateCellStyle();
		style.SetFont(font);
		style.Alignment = HorizontalAlignment.Center;
		style.VerticalAlignment = VerticalAlignment.Justify;
		style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
		style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
		return style;
	}

	
	private string ReportHandler(string largeItemRdn, DateTime beginDate,DateTime endDate,ref DataTable dt1,string sqlWhere)
	{
		string sql = "select Count(*) from dbo.JCI_patient_brifeInfo brifeInfo " +
			"where brifeInfo.regDept>'" + beginDate.ToString("yyyy-MM-dd") + "' and brifeInfo.regDept<'" + endDate.ToString("yyyy-MM-dd") + "' ";
		if (sqlWhere != null &&""!=sqlWhere)
		{
			sql = sql + sqlWhere;
		}
		SqlConnect conn = new SqlConnect();
		DataTable dt = conn.ExcuteSelect(sql);
		int iCount = 0;
		if (dt.Rows.Count == 0) return "there is no record to report. - 1";
		iCount = Convert.ToInt32(dt.Rows[0][0]);
		if (iCount == 0) return "there is no record to report. - 2";

		dt = ExportDataTable(largeItemRdn, beginDate, endDate, sqlWhere);
		if (dt.Rows.Count == 0) return "there is no record to report. - 3";
		dt.Columns.Add("rate", typeof(double));
		int case0Sum = 0, case1Sum = 0, case2Sum = 0, case3Sum = 0, case4Sum = 0;
		foreach (DataRow dr in dt.Rows)
		{
			//case0Sum = case1Sum = case2Sum = case3Sum = case4Sum = 0;
			double sum = CalculateSatisfyRate(dr);
			case0Sum = case0Sum + Convert.ToInt32(dr["case0"]);
			case1Sum = case1Sum + Convert.ToInt32(dr["case1"]);
			case2Sum = case2Sum + Convert.ToInt32(dr["case2"]);
			case3Sum = case3Sum + Convert.ToInt32(dr["case3"]);
			case4Sum = case4Sum + Convert.ToInt32(dr["case4"]);
			double rate = sum / (Convert.ToInt32(dr["case1"]) + Convert.ToInt32(dr["case2"]) + Convert.ToInt32(dr["case3"]) + Convert.ToInt32(dr["case4"])) / 10;
			dr["rate"] = rate;
		}
		double rateSum = 0d;
		if (!(case3Sum == 0 && case2Sum == 0 && case3Sum == 0))
		{
			rateSum = (case1Sum * 10 + case2Sum * 7 + case3Sum * 5) * 1.0 / (case1Sum * 10 + case2Sum * 10 + case3Sum * 10 + case4Sum * 10);
		}
		//人次合计
		dt.Rows.Add(1001, "人次合计", case0Sum, case1Sum, case2Sum, case3Sum, case4Sum, rateSum);
		
		int iPeopleSum = case0Sum + case1Sum + case2Sum + case3Sum + case4Sum;
		if (iPeopleSum == 0)
		{
			dt.Rows.Add(1002, "平均人次率", 0, 0, 0, 0, 0, 0);
		}
		else
		{ //平均人次率
			//double test = case2Sum * 1.0 / iPeopleSum;
			dt.Rows.Add(1002, "平均人次率", case0Sum * 1.0 / iPeopleSum, case1Sum * 1.0 / iPeopleSum, case2Sum * 1.0 / iPeopleSum, case3Sum * 1.0 / iPeopleSum, case4Sum * 1.0 / iPeopleSum, 1);
			//dt.Rows.Add(1002, "平均人次率", 0.1, 0.1, 0.1, 0.1, 0.1, 1);
		}
		dt1 = dt;
		return "ok";
	}
	private double CalculateSatisfyRate(DataRow dr)
	{
		int case0, case1, case2, case3, case4;
		case0 = Convert.ToInt32(dr["case0"]) * 0;
		case1 = Convert.ToInt32(dr["case1"]) * 10;
		case2 = Convert.ToInt32(dr["case2"]) * 7;
		case3 = Convert.ToInt32(dr["case3"]) * 5;
		case4 = Convert.ToInt32(dr["case4"]) * 0;
		return case1 + case2 + case3;
	}
	private DataTable ExportDataTable(string largeItemRdn,DateTime beginDate,DateTime endDate,string sqlWhere)
	{
		string sql = "select genTable.sortID,t2.* from  " +
		"(select t1.smallItemName,SUM(case0)*1.0 'case0',SUM(case1)*1.0 'case1',SUM(case2)*1.0 'case2',SUM(case3)*1.0 'case3',SUM(case4)*1.0 'case4' from " +
		"(select smallItemName, " +
		"case when result='-1' then count(*) else 0 end 'case0', " +
		"case when result='10' then count(*) else 0 end 'case1', " +
		"case when result='7' then count(*) else 0 end 'case2', " +
		"case when result='5' then count(*) else 0 end 'case3', " +
		"case when result='0' then count(*) else 0 end 'case4', " +
		"result " +
		"from dbo.JCI_detailInfo detailInfo,dbo.JCI_patient_brifeInfo brifeInfo " +
		"where typefrom='patient' and largeItemRdn='" + largeItemRdn + "' and brifeInfo.rdn = detailInfo.applyRdn ";
		if (sqlWhere != null && sqlWhere != "")
		{
			sql = sql + sqlWhere + " ";
		}
		sql = sql +
		"and brifeInfo.regDept>'" + beginDate.ToString("yyyy-MM-dd") + "' and brifeInfo.regDept<'" + endDate.ToString("yyyy-MM-dd") + "' " +
		"group by smallItemName,result) t1  " +
		"group by smallItemName ) t2 " +
		"left join dbo.JCI_generateTable genTable on genTable.smallItemName=t2.smallItemName " +
		"order by genTable.sortID ASC ";
		SqlConnect conn = new SqlConnect();
		DataTable dt = conn.ExcuteSelect(sql);
		return dt;
		//return null;
	}
	private DataTable ExportAdviceTable(PatientAdviceDept adviceDept, DateTime beginDate,DateTime endDate,string sqlWhere)
	{
		string sql = "select rdn,dept,floorName,promote,needImprove  from dbo.JCI_patient_brifeInfo brifeInfo " +
		"where brifeInfo.regDept>'" + beginDate.ToString("yyyy-MM-dd") + "' and brifeInfo.regDept<'" + endDate.ToString("yyyy-MM-dd") + "' ";
		if (sqlWhere != null && "" != sqlWhere)
		{
			sql = sql + sqlWhere;
		}
		SqlConnect conn = new SqlConnect();
		DataTable dt = conn.ExcuteSelect(sql);
		DataTable dt1 = new DataTable();
		dt1.Columns.Add("rdn", typeof(int));
		dt1.Columns.Add("dept", typeof(string));
		dt1.Columns.Add("floorName", typeof(string));
		dt1.Columns.Add("adviceDept", typeof(string));
		dt1.Columns.Add("adviceType", typeof(string));
		dt1.Columns.Add("adviceContent", typeof(string));

		foreach (DataRow dr in dt.Rows)
		{
			string advicePromote = dr["promote"].ToString();
			string adviceNeedImprove = dr["needImprove"].ToString();
			string[] advicePromotes = advicePromote.Split(';');
			string[] adviceNeedImproves = adviceNeedImprove.Split(';');
			if (advicePromotes.Length <= 0 || adviceNeedImproves.Length <= 0)
			{
				continue;
			}
			string tmpPromote = "";
			string tmpNeedImprove = "";
			#region handler
			switch (adviceDept)
			{
				case PatientAdviceDept.YL:
					tmpPromote = advicePromotes[0].Split(':')[1];
					tmpNeedImprove = adviceNeedImproves[0].Split(':')[1];
					break;
				case PatientAdviceDept.HL:
					tmpPromote = advicePromotes[1].Split(':')[1];
					tmpNeedImprove = adviceNeedImproves[1].Split(':')[1];
					break;
				case PatientAdviceDept.YJ:
					tmpPromote = advicePromotes[2].Split(':')[1];
					tmpNeedImprove = adviceNeedImproves[2].Split(':')[1];
					break;
				case PatientAdviceDept.HQ:
					tmpPromote = advicePromotes[3].Split(':')[1];
					tmpNeedImprove = adviceNeedImproves[3].Split(':')[1];
					break;
				case PatientAdviceDept.ZY:
					tmpPromote = advicePromotes[4].Split(':')[1];
					tmpNeedImprove = adviceNeedImproves[4].Split(':')[1];
					break;
				default:
					break;
			}
			if (!tmpPromote.Equals(""))
			{
				dt1.Rows.Add(dr["rdn"], dr["dept"], dr["floorName"], adviceDept, "promote", tmpPromote);
			}
			if (!tmpNeedImprove.Equals(""))
			{
				dt1.Rows.Add(dr["rdn"], dr["dept"], dr["floorName"], adviceDept, "needImprove", tmpNeedImprove);
			}
			#endregion
		}
		return dt1;
	}
}
public enum PatientAdviceDept
{
	YL,
	YJ,
	HL,
	HQ,
	ZY
}
public class PatientReportHead
{
	public string headName;
	public string reportDate;
	public string reportType;
}
