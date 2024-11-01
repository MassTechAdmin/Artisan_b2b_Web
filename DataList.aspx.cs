using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Net;
using System.Data.SqlClient;

public partial class DataList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clsFunction.Check.CheckSession();
        if (this.IsPostBack == false)
        {
            fn_Show_Data();
            fn_GridView1();

            fn_JavaScript();
        }
    }
    #region " --- 顯示資料 --- "
	/// <summary>
	/// 顯示資料
	/// </summary>
	/// <remarks></remarks>
	private void fn_Show_Data()
	{
		string tour_numb = "";
		//取得報名單資訊
		if (!string.IsNullOrEmpty(Request["Nid"])) {
			string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
			SqlConnection connect = new SqlConnection(constring);
			connect.Open();
			string strSql = "";
			strSql += " select number, Cust_Numb, netcustnumb, CONVERT(varchar(100), EnliI_Date, 111) as EnliI_Date, Tour_Numb";
			strSql += " , BookPax, TourFee, Remark, AGT_TR10_Check";
			strSql += " from tr10";
            strSql += " where number = '" + Request["Nid"] + "'";
			SqlCommand command = new SqlCommand(strSql, connect);
			command.Parameters.Add(new SqlParameter("@number", Request["Nid"].ToString()));
			SqlDataReader reader = command.ExecuteReader();
			if (reader.Read()) {
				Label1.Text = reader["number"].ToString();
				Label2.Text = Session["PerName"].ToString();
				//現在使用者
				Label3.Text = System.DateTime.Today.ToString("yyyy/MM/dd");
				Label4.Text = Session["PerName"].ToString();
				//建檔人員
				Label6.Text = reader["netcustnumb"].ToString();
				Label14.Text = reader["EnliI_Date"].ToString();
				TextBox11.Text = reader["BookPax"].ToString();
				TextBox16.Text = reader["Remark"].ToString();
				tour_numb = reader["Tour_Numb"].ToString();
				Hidden1.Value = reader["Tour_Numb"].ToString();

				switch (reader["AGT_TR10_Check"].ToString().ToUpper()) {
					case "V":
						txbState.Text = "已審核";
						break;
					case "Y":
						txbState.Text = "審核中";
						break;
					default:
						txbState.Text = "處理中";
						break;
				}
			}
			command.Dispose();
			reader.Close();
			connect.Close();
		}


		//取得團體資訊
		if (!string.IsNullOrEmpty(tour_numb)) {
			string constring2 = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
			SqlConnection connect2 = new SqlConnection(constring2);
			connect2.Open();
			string strSql = "";
			strSql = "SELECT [Grop_Numb],[Grop_Name],[Grop_Tour],CONVERT(varchar(12) , [Grop_Depa], 111 ) as Grop_Depa FROM [grop] WHERE ([Grop_Numb] ='" + tour_numb + "')";
			SqlCommand command = new SqlCommand(strSql, connect2);
			SqlDataReader reader = command.ExecuteReader();
			string tournum = "";
			if (reader.HasRows == true) {
				reader.Read();
				Label15.Text = reader["Grop_Name"].ToString();
				TextBox4.Text = reader["Grop_Depa"].ToString();
				TextBox12.Text = reader["Grop_Tour"].ToString();
			}
			command.Dispose();
			reader.Close();
			connect2.Close();
		}


		//取得同行資訊
		if (!string.IsNullOrEmpty(Session["compno"].ToString()) && !string.IsNullOrEmpty(Session["PerName"].ToString())) {
			string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
			SqlConnection connect = new SqlConnection(constring);
			connect.Open();
			string strSql = "";
			strSql = "";
			strSql += " select AGENT_M.AGT_NAME1, TEL1_ZONE, TEL1, FAX_ZONE, FAX, AGT_CONT, CONT_CELL, CONT_MAIL from AGENT_M ";
			strSql += " left join AGENT_L on AGENT_M.AGT_NAME1 = AGENT_L.AGT_NAME1 ";
			strSql += " where AGENT_M.Comp_No = '" + Session["compno"] + "' ";
			strSql += " and AGT_CONT = '" + Session["PerName"] + "' ";
			strSql += "  ";
			SqlCommand command = new SqlCommand(strSql, connect);
			SqlDataReader reader = command.ExecuteReader();
			string tournum = "";
			if (reader.Read()) {
				TextBox6.Text = reader["AGT_NAME1"].ToString();
				if (string.IsNullOrEmpty(reader["TEL1_ZONE"].ToString())) {
					TextBox8.Text = reader["TEL1"].ToString();
				} else {
					TextBox8.Text = reader["TEL1_ZONE"].ToString() + "-" + reader["TEL1"].ToString();
				}
				if (string.IsNullOrEmpty(reader["FAX_ZONE"].ToString())) {
					TextBox9.Text = reader["FAX"].ToString();
				} else {
					TextBox9.Text = reader["FAX_ZONE"].ToString() + "-" + reader["FAX"].ToString();
				}
				TextBox7.Text = reader["AGT_CONT"].ToString();
				TextBox19.Text = reader["CONT_CELL"].ToString();
				TextBox20.Text = reader["CONT_MAIL"].ToString();
			}
			command.Dispose();
			reader.Close();
			connect.Close();
		}
	}
	/// <summary>
	/// 顯示 GridView1 的資料
	/// </summary>
	/// <remarks></remarks>
	protected void fn_GridView1()
	{
		string strSql = "";
		strSql += " SELECT Tr10Number, Cust_Numb, netcustnumb, GROP_NUMB, CONVERT(varchar(100), Enli_Date, 111) as Enli_Date";
		strSql += " , Sequ_No, Cust_Idno, MRSS_Name, Cust_Name, Eng_Name";
        strSql += " , CONVERT(varchar(100), BIRT_DATE, 111) as BIRT_DATE, PASS_NO, CONVERT(varchar(100), PASS_ISSU, 111) as PASS_ISSU, CONVERT(varchar(100), PASS_VALI, 111) as PASS_VALI, Cell_Tel";
		strSql += " , E_Mail, Bed_Type, EAT, Tick_Type, Remark";
		strSql += " , Tour_Mony";
		strSql += " FROM TR20";
		strSql += " WHERE tr10number = @tr10number";
		strSql += " ORDER BY tr20.SEQU_NO";
		string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
		SqlConnection connect = new SqlConnection(constring);
		connect.Open();
		SqlCommand cmd = new SqlCommand(strSql, connect);
		cmd.Parameters.Add(new SqlParameter("@tr10number", Request["Nid"]));
		SqlDataAdapter da = new SqlDataAdapter();
		da.SelectCommand = cmd;
		DataTable dt = new DataTable();
		da.Fill(dt);
		cmd.Dispose();
		connect.Close();

        if (clsFunction.Check.IsNumeric(TextBox11.Text.Trim()))
        {
			for (int ii = dt.Rows.Count + 1; ii <= Convert.ToInt32(TextBox11.Text.Trim()); ii++) {
				DataRow dr = dt.NewRow();
				dr["SEQU_NO"] = ii.ToString("D4");
                dr["BIRT_DATE"] = "1900-1-1";
                dr["PASS_ISSU"] = "1900-1-1";
                dr["PASS_VALI"] = "1900-1-1";
				dt.Rows.Add(dr);
			}
		}


		GridView1.DataSource = dt;
		//GridView1.DataKeyNames = New String() {"apar_numb", "cust_numb", "sequ_no"}
		GridView1.DataBind();
	}

	protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow) {
			// 稱呼
			HiddenField hidMRSS_Name = (HiddenField)e.Row.FindControl("hidMRSS_Name");
			DropDownList ddlMRSS_Name = (DropDownList)e.Row.FindControl("ddlMRSS_Name");
			if ((ddlMRSS_Name != null)) {
				string strSql = "";
				strSql = "SELECT DISTINCT glb_id,[descrip] FROM [GLB_CODE] WHERE ([glb_code] = 'mrss') ORDER BY glb_id,descrip";
				string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
				SqlConnection connect = new SqlConnection(constring);
				connect.Open();
				SqlCommand cmd = new SqlCommand(strSql, connect);
				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = cmd;
				DataTable dt = new DataTable();
				da.Fill(dt);
				cmd.Dispose();
				connect.Close();

				ddlMRSS_Name.DataSource = dt;
				ddlMRSS_Name.DataTextField = "descrip";
				ddlMRSS_Name.DataValueField = "descrip";
				ddlMRSS_Name.DataBind();

				ddlMRSS_Name.SelectedIndex = ddlMRSS_Name.Items.IndexOf(ddlMRSS_Name.Items.FindByValue(hidMRSS_Name.Value));
			}
            
			// 佔床
			HiddenField hidBED_TYPE = (HiddenField)e.Row.FindControl("hidBED_TYPE");
			DropDownList ddlBED_TYPE = (DropDownList)e.Row.FindControl("ddlBED_TYPE");
			if ((ddlBED_TYPE != null)) {
				ddlBED_TYPE.SelectedIndex = ddlBED_TYPE.Items.IndexOf(ddlBED_TYPE.Items.FindByValue(hidBED_TYPE.Value));
			}

			// 飲食
			HiddenField hidEAT = (HiddenField)e.Row.FindControl("hidEAT");
			DropDownList ddlEAT = (DropDownList)e.Row.FindControl("ddlEAT");
			if ((ddlEAT != null)) {
				ddlEAT.SelectedIndex = ddlEAT.Items.IndexOf(ddlEAT.Items.FindByValue(hidEAT.Value));
			}

			// 票別
			HiddenField hidTICK_TYPE = (HiddenField)e.Row.FindControl("hidTICK_TYPE");
			DropDownList ddlTICK_TYPE = (DropDownList)e.Row.FindControl("ddlTICK_TYPE");
			if ((ddlTICK_TYPE != null)) {
				ddlTICK_TYPE.SelectedIndex = ddlTICK_TYPE.Items.IndexOf(ddlTICK_TYPE.Items.FindByValue(hidTICK_TYPE.Value));
			}

			//出生日期
			TextBox lblBIRT_DATE = (TextBox)e.Row.FindControl("lblBIRT_DATE");
			lblBIRT_DATE.Attributes.Add("onchange", "CheckBirthDate('" + lblBIRT_DATE.ClientID + "');");

			TextBox lblPASS_ISSU = (TextBox)e.Row.FindControl("lblPASS_ISSU");
			//護照發照日
			TextBox lblPASS_VALI = (TextBox)e.Row.FindControl("lblPASS_VALI");
			//護照效期
			lblPASS_ISSU.Attributes.Add("onchange", "checkPass_ISSU('" + lblPASS_ISSU.ClientID + "','" + lblPASS_VALI.ClientID + "');");


			//Dim lblBIRT_DATE As TextBox = CType(e.Row.FindControl("lblBIRT_DATE"), TextBox)
			//If (lblBIRT_DATE.Text = "1900-1-1 0:00:00") Then
			//    lblBIRT_DATE.Text = ""
			//End If
		}
	}

	protected void fn_JavaScript()
	{
		// **********************************************************************************************************************
		//取得團號
		string script = "";
		//生日，自動加 /，並判斷格式正不正確
		script = "<script>";
		script += "function CheckBirthDate(objConrtol)";
		script += "{";
		script += "var c = document.getElementById(objConrtol).value;";
		script += "if(document.getElementById(objConrtol).value.length == 8)";
		script += "{";
		script += "document.getElementById(objConrtol).value=c.substr(0,4) + '/' + c.substr(4,2) + '/' + c.substr(6,2);";
		script += "}";
		script += "var a = new Date(document.getElementById(objConrtol).value);";
		script += "var y = a.getFullYear();";
		script += "var m = a.getMonth() + 1;";
		script += "var d = a.getDate();";
		//月份，自動加0
		script += "if(m.toString().length == 1)";
		script += "{";
		script += "m = '0' + m.toString();";
		script += "}";
		//日，自動加0
		script += "if(d.toString().length == 1)";
		script += "{";
		script += "d = '0' + d.toString();";
		script += "}";
		script += "var myday = y + '/' + m + '/' + d;";
		script += "if(myday != document.getElementById(objConrtol).value && document.getElementById(objConrtol).value != '')";
		script += "{";
		script += "alert('生日，日期格式錯誤或輸入錯誤');";
		script += "document.getElementById(objConrtol).focus();";
		script += "return false;";
		script += "}";
		script += "}";
		script += "</script>";
		this.ClientScript.RegisterClientScriptBlock(typeof(string), "CheckBirthDate", script);


		// **********************************************************************************************************************
		//護照發照日
		script = "<script>";
		script += "function checkPass_ISSU(objConrtol,objPASS_VALI)";
		script += "{";
		script += "var c = document.getElementById(objConrtol).value;";
		script += "if(document.getElementById(objConrtol).value.length == 8)";
		script += "{";
		script += "document.getElementById(objConrtol).value=c.substr(0,4) + '/' + c.substr(4,2) + '/' + c.substr(6,2);";
		script += "}";
		script += "var a = new Date(document.getElementById(objConrtol).value);";
		script += "var y = a.getFullYear();";
		script += "var m = a.getMonth() + 1;";
		script += "var d = a.getDate();";
		//月份，自動加0
		script += "if(m.toString().length == 1)";
		script += "{";
		script += "m = '0' + m.toString();";
		script += "}";
		//日，自動加0
		script += "if(d.toString().length == 1)";
		script += "{";
		script += "d = '0' + d.toString();";
		script += "}";
		script += "var myday = y + '/' + m + '/' + d;";
		script += "if(myday != document.getElementById(objConrtol).value && document.getElementById(objConrtol).value != '')";
		script += "{";
		script += "alert('護照發照日，日期格式錯誤或輸入錯誤');";
		script += "document.getElementById(objConrtol).focus();";
		script += "return false;";
		script += "}";
		//護照效期自動加十年
		script += "else";
		script += "{";
		script += "if(document.getElementById(objPASS_VALI).value == '' && myday == document.getElementById(objConrtol).value)";
		script += "{";
		script += "document.getElementById(objPASS_VALI).value = (y + 10) + '/' + m + '/' + d;";
		script += "}";
		script += "}";

		script += "}";
		script += "</script>";
		this.ClientScript.RegisterClientScriptBlock(typeof(string), "checkPass_ISSU", script);
	}
	#endregion

	#region " --- Agent 資料 --- "
	// 回傳 AGENT_M 是否有資料
	private bool fn_RtnAGENT_M_IsData()
	{
		bool blnIsData = false;

		string strSql = "";
		strSql = " SELECT AGT_NAME1 FROM AGENT_M";
		strSql += " WHERE AGT_NAME1 = @AGT_NAME1";

		string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString"].ToString();
		SqlConnection connect = new SqlConnection(constring);
		connect.Open();
		SqlCommand cmd = new SqlCommand(strSql, connect);
		cmd.Parameters.Add(new SqlParameter("@AGT_NAME1", TextBox6.Text.Trim()));
		SqlDataReader reader = cmd.ExecuteReader();
		if (reader.HasRows) {
			blnIsData = true;
		}
		reader.Close();
		cmd.Dispose();
		connect.Close();

		return blnIsData;
	}
	// 更新/新增 AGENT_M 資料
	private void fn_Update_AGENT_M()
	{
		if (!fn_RtnAGENT_M_IsData()) {
			string strSql = "";
			strSql = " INSERT INTO AGENT_M(";
			strSql += " AGT_NAME1";
			strSql += " ) VALUES (";
			strSql += " @AGT_NAME1";
			strSql += " )";
			string conString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString"].ToString();
			SqlConnection conn = new SqlConnection(conString);
			conn.Open();
			SqlCommand cmd = new SqlCommand(strSql, conn);
			cmd.Parameters.Add(new SqlParameter("@AGT_NAME1", TextBox6.Text.Trim()));
			cmd.ExecuteNonQuery();
			cmd.Dispose();
			conn.Close();
		}
	}
	// 回傳 AGENT_L 是否有資料
	private bool fn_RtnAGENT_L_IsData()
	{
		bool blnIsData = false;

		string strSql = "";
		strSql = " SELECT AGT_NAME1 FROM AGENT_L";
		strSql += " WHERE AGT_NAME1 = @AGT_NAME1";
		strSql += " AND AGT_CONT = @AGT_CONT";

		string constring = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString"].ToString();
		SqlConnection connect = new SqlConnection(constring);
		connect.Open();
		SqlCommand cmd = new SqlCommand(strSql, connect);
		cmd.Parameters.Add(new SqlParameter("@AGT_NAME1", TextBox6.Text.Trim()));
		cmd.Parameters.Add(new SqlParameter("@AGT_CONT", TextBox7.Text.Trim()));
		SqlDataReader reader = cmd.ExecuteReader();
		if (reader.HasRows) {
			blnIsData = true;
		}
		reader.Close();
		cmd.Dispose();
		connect.Close();

		return blnIsData;
	}
	// 更新/新增 AGENT_L 資料
	private void fn_Update_AGENT_L()
	{
		if (fn_RtnAGENT_L_IsData()) {
			string strSql = "";
			strSql = " UPDATE AGENT_L SET";
			strSql += " CONT_TEL = @CONT_TEL";
			strSql += " ,CONT_FAX = @CONT_FAX";
			strSql += " ,CONT_CELL = @CONT_CELL";
			strSql += " ,CONT_MAIL = @CONT_MAIL";
			strSql += " WHERE AGT_NAME1 = @AGT_NAME1";
			strSql += " AND AGT_CONT = @AGT_CONT";
			string conString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString"].ToString();
			SqlConnection conn = new SqlConnection(conString);
			conn.Open();
			SqlCommand cmd = new SqlCommand(strSql, conn);
			cmd.Parameters.Add(new SqlParameter("@CONT_TEL", TextBox8.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@CONT_FAX", TextBox9.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@CONT_CELL", TextBox19.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@CONT_MAIL", TextBox20.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@AGT_NAME1", TextBox6.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@AGT_CONT", TextBox7.Text.Trim()));
			cmd.ExecuteNonQuery();
			cmd.Dispose();
			conn.Close();
		} else {
			string strSql = "";
			strSql = " INSERT INTO AGENT_L(";
			strSql += " AGT_NAME1,AGT_CONT,CONT_TEL,CONT_FAX,CONT_CELL,CONT_MAIL";
			strSql += " ) VALUES (";
			strSql += " @AGT_NAME1,@AGT_CONT,@CONT_TEL,@CONT_FAX,@CONT_CELL,@CONT_MAIL";
			strSql += " )";
			string conString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GLBConnectionString"].ToString();
			SqlConnection conn = new SqlConnection(conString);
			conn.Open();
			SqlCommand cmd = new SqlCommand(strSql, conn);
			cmd.Parameters.Add(new SqlParameter("@AGT_NAME1", TextBox6.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@AGT_CONT", TextBox7.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@CONT_TEL", TextBox8.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@CONT_FAX", TextBox9.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@CONT_CELL", TextBox19.Text.Trim()));
			cmd.Parameters.Add(new SqlParameter("@CONT_MAIL", TextBox20.Text.Trim()));
			cmd.ExecuteNonQuery();
			cmd.Dispose();
			conn.Close();
		}
	}
	#endregion

	protected void Page_Unload(object sender, System.EventArgs e)
	{
		this.Dispose();
		base.Dispose();
	}

	private string GetClientIP()
	{
        string strIPAddr = "";

        if (string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) || (Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf("unknown")) > 0)
        {
            strIPAddr = Request.ServerVariables["REMOTE_ADDR"];
        }
        else if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",") > 0)
        {
            strIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(1, Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",") - 1);
        }
        else if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";") > 0)
        {
            strIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(1, Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";") - 1);
        }
        else
        {
            strIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        if (strIPAddr.Length > 30)
        {
            return strIPAddr.Substring(1, 30).Trim();
        }
        return strIPAddr;
	}

	public string fn_Rtn_DATE(System.DateTime dateDATE)
	{
        
        if (dateDATE > Convert.ToDateTime("2000,1,1"))
        {
			return dateDATE.ToString("yyyy/MM/dd");
		}

		return "";
	}
	public void Index_DataList()
	{
		Unload += Page_Unload;
		Load += Page_Load;
	}
}
