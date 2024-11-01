using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class OLApply_Check : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Load_Data();
    }

    protected void Load_Data()
    {
        string strSql = "";
        strSql += " SELECT AGT_NAME2,AGENT_M.COMP_NO,AGENT_L.AGT_IDNo,AGENT_L.AGT_PW,AGENT_L.AGT_CONT";
        strSql += " ,AGENT_L.CONT_MAIL, AGENT_L.CONT_ZONE,AGENT_L.CONT_TEL,AGENT_L.CONT_TEL_Ext,AGENT_L.CONT_CELL";
        strSql += " FROM AGENT_L";
        strSql += " LEFT JOIN AGENT_M ON AGENT_L.AGT_NAME1 = AGENT_M.AGT_NAME1";
        strSql += " WHERE AGENT_L.AGT_IDNo = @AGT_IDNo";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@AGT_IDNo", Session["PERNO"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                //任職旅行社
                Lbl_Comp.Text = reader["AGT_NAME2"].ToString();
                //統一編號
                Lbl_Comp_No.Text = reader["COMP_NO"].ToString();
                //會員帳號
                Lbl_Account.Text = reader["AGT_IDNo"].ToString();
                //密碼
                Lbl_PassWord.Text = reader["AGT_PW"].ToString();
                //中文姓名
                Lbl_Name.Text = reader["AGT_CONT"].ToString();
                //電子郵件
                Lbl_EMail.Text = reader["CONT_MAIL"].ToString();
                //公司電話
                Lbl_Tel1.Text = reader["CONT_ZONE"].ToString();
                Lbl_Tel2.Text = reader["CONT_TEL"].ToString();
                Lbl_Tel3.Text = reader["CONT_TEL_Ext"].ToString();
                //行動電話
                Lbl_Phone.Text = reader["CONT_CELL"].ToString();

                if (Lbl_Account.Text.Length > 2)
                {
                    if (Lbl_Account.Text.Substring(1, 1) == "1")
                    { DropDownList1.SelectedValue = "M"; }
                    else
                    { DropDownList1.SelectedValue = "F"; }
                }
            }
            reader.Close();
            comm.Dispose();
        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true);
            return;
        }
        finally
        {
            connect.Close();
        }
    }

    protected void button_Click(object sender, EventArgs e)
    {
        //帳號
        if (string.IsNullOrEmpty(Lbl_Account.Text) == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('帳號為必填資料。');", true); return;
        }
        else
        {
            if (!checkId(Lbl_Account.Text)) { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('身分證字號錯誤。');", true); return; }
        }
        //密碼
        if (string.IsNullOrEmpty(Lbl_PassWord.Text) == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('密碼為必填資料。');", true); return;
        }
        //中文姓名
        if (string.IsNullOrEmpty(Lbl_Name.Text) == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('中文姓名為必填資料。');", true); return;
        }
        //性別
        if (DropDownList1.SelectedValue == "0")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請選擇您的性別。');", true); return;
        }
        //出生日期
        string strBirthday = Lbl_Birthday1.Text + "/" + Lbl_Birthday2.Text.PadLeft(2, '0') + "/" + Lbl_Birthday3.Text.PadLeft(2, '0');
        if (strBirthday == "/00/00") { strBirthday = ""; }
        if (!string.IsNullOrEmpty(strBirthday))
        {
            DateTime dt;
            if (!DateTime.TryParse(strBirthday, out dt))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出生日期資料不正確。');", true); return;
            }
        }
        //電子郵件
        if (string.IsNullOrEmpty(Lbl_EMail.Text) == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('電子郵件為必填資料。');", true); return;
        }
        //公司電話
        if (string.IsNullOrEmpty(Lbl_Tel1.Text) == true || string.IsNullOrEmpty(Lbl_Tel2.Text) == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('公司電話資料不正確。');", true); return;
        }
        //行動電話
        if (string.IsNullOrEmpty(Lbl_Phone.Text) == true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('行動電話為必填資料。');", true); return;
        }

        if (CheckBox1.Checked == true && CheckBox2.Checked == true)
        { fn_Save(); }  
        else
        {
            if (CheckBox1.Checked == false) { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請閱讀審核資料相關事項，並打勾同意其內容。');", true); return; }
            if (CheckBox2.Checked == false) { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('請確認您的B2B帳號資料，並打勾確認資料正確。');", true); return; }
        }
    }

    protected void fn_Save()
    {
        string strSql = "";
        strSql = " insert into Confirm ( ";
        strSql += " Comp ,CompNO ,OLD_ID ,ID ,PW ,Name ,SEX ,birthday ,Email ,TEL ,TEL1 ,TEL2 ,Phone ,crea_date ";
        strSql += " ) VALUES ( ";
        strSql += " @Comp ,@CompNO ,@OLD_ID ,@ID ,@PW ,@Name ,@SEX ,@birthday ,@Email ,@TEL ,@TEL1 ,@TEL2 ,@Phone ,@crea_date ";
        strSql += " ) ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Comp", Lbl_Comp.Text));
            comm.Parameters.Add(new SqlParameter("@CompNO", Lbl_Comp_No.Text));
            comm.Parameters.Add(new SqlParameter("@OLD_ID", Session["PERNO"]));
            comm.Parameters.Add(new SqlParameter("@ID", Lbl_Account.Text));
            comm.Parameters.Add(new SqlParameter("@PW", Lbl_PassWord.Text));
            comm.Parameters.Add(new SqlParameter("@Name", Lbl_Name.Text));
            comm.Parameters.Add(new SqlParameter("@SEX", DropDownList1.SelectedValue));
            string strBirthday = Lbl_Birthday1.Text + "/" + Lbl_Birthday2.Text.PadLeft(2, '0') + "/" + Lbl_Birthday3.Text.PadLeft(2, '0');
            //if (strBirthday == "/00/00") { strBirthday = DBNull.Value; }
            comm.Parameters.Add(new SqlParameter("@birthday", (strBirthday == "/00/00" ? DBNull.Value : (object)strBirthday)));
            comm.Parameters.Add(new SqlParameter("@Email", Lbl_EMail.Text));
            comm.Parameters.Add(new SqlParameter("@TEL", Lbl_Tel1.Text));
            comm.Parameters.Add(new SqlParameter("@TEL1", Lbl_Tel2.Text));
            comm.Parameters.Add(new SqlParameter("@TEL2", Lbl_Tel3.Text));
            comm.Parameters.Add(new SqlParameter("@Phone", Lbl_Phone.Text));
            comm.Parameters.Add(new SqlParameter("@crea_date", Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")));

            comm.ExecuteNonQuery();
            comm.Dispose();
            try
            {
        }
        catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true); return; }
        finally { connect.Close(); }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('感謝您耐心完成資料審查，本公司將有專人再次與您核對資料。'); location='/index.aspx';", true); return;
    }

    private bool checkId(string user_id) //檢查身分證字號
    {
        //回傳：true正確  false錯誤 
        int[] uid = new int[10]; //數字陣列存放身分證字號用
        int chkTotal; //計算總和用

        if (user_id.Length == 10) //檢查長度
        {
            user_id = user_id.ToUpper(); //將身分證字號英文改為大寫

            //將輸入的值存入陣列中
            for (int i = 1; i < user_id.Length; i++)
            {
                uid[i] = Convert.ToInt32(user_id.Substring(i, 1));
            }
            //將開頭字母轉換為對應的數值
            switch (user_id.Substring(0, 1).ToUpper())
            {
                case "A": uid[0] = 10; break;
                case "B": uid[0] = 11; break;
                case "C": uid[0] = 12; break;
                case "D": uid[0] = 13; break;
                case "E": uid[0] = 14; break;
                case "F": uid[0] = 15; break;
                case "G": uid[0] = 16; break;
                case "H": uid[0] = 17; break;
                case "I": uid[0] = 34; break;
                case "J": uid[0] = 18; break;
                case "K": uid[0] = 19; break;
                case "L": uid[0] = 20; break;
                case "M": uid[0] = 21; break;
                case "N": uid[0] = 22; break;
                case "O": uid[0] = 35; break;
                case "P": uid[0] = 23; break;
                case "Q": uid[0] = 24; break;
                case "R": uid[0] = 25; break;
                case "S": uid[0] = 26; break;
                case "T": uid[0] = 27; break;
                case "U": uid[0] = 28; break;
                case "V": uid[0] = 29; break;
                case "W": uid[0] = 32; break;
                case "X": uid[0] = 30; break;
                case "Y": uid[0] = 31; break;
                case "Z": uid[0] = 33; break;
            }
            //檢查第一個數值是否為1.2(判斷性別)
            if (uid[1] == 1 || uid[1] == 2)
            {
                chkTotal = (uid[0] / 10 * 1) + (uid[0] % 10 * 9);

                int k = 8;
                for (int j = 1; j < 9; j++)
                {
                    chkTotal += uid[j] * k;
                    k--;
                }

                chkTotal += uid[9];

                if (chkTotal % 10 != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }
}