// ****************************************************************************************************
// 20160315 若agent有輸入個人的電話就帶個人的電話，要不然就是帶公司的話 by roger
// ****************************************************************************************************
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Web;

public partial class OLApply_Apply3 : System.Web.UI.Page
{
    int intTax = 0;    //稅險
    int intVisa = 0;   //簽證
    int intDiscount = 0;   //折扣
    string ResponseXML = ""; //XML回傳訊息

    TextBox txtUsePoint = new TextBox();
    TextBox txtMustPoint = new TextBox();
    TextBox txtRemainPoint = new TextBox();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Load_Data();
        TextBox txt_Remark1 = (TextBox)this.PreviousPage.FindControl("txt_Remark1");
        hid_Remark1.Value = txt_Remark1.Text;
        TextBox kt1 = (TextBox)this.PreviousPage.FindControl("Tai_Kao1");
        if (!string.IsNullOrEmpty(kt1.Text)) hid_kt1.Value = kt1.Text;
        TextBox kt2 = (TextBox)this.PreviousPage.FindControl("Tai_Kao2");
        if (!string.IsNullOrEmpty(kt2.Text)) hid_kt2.Value = kt2.Text;

        

        

    }

    protected void Load_Data()
    {
        txtUsePoint = (TextBox)this.PreviousPage.FindControl("txtConfirmUsePoint");
        txtMustPoint = (TextBox)this.PreviousPage.FindControl("txtMustPoint");
        txtRemainPoint = (TextBox)this.PreviousPage.FindControl("txtRemainPoint");
        ppUsePoint.Text = txtUsePoint.Text;
        ppMustPoint.Text = txtMustPoint.Text;
        ppRemainPoint.Text = txtRemainPoint.Text;

        string strSql = "";
        strSql += " SELECT top 1 Van_Number ,Grop_Name ,Grop_Numb ,Grop_Visa ,Grop_Tax, Group_Name.Group_Name ";
        strSql += " FROM grop ";
        strSql += " left join Group_Name on Group_Name.Group_Name_No = grop.Group_Name_No";
        strSql += " where Van_Number = @Van_Number ";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Van_Number", Request["n"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                //團名
                Lbl_Grop_Name.Text = reader["Group_Name"].ToString();
                //團號
                Lbl_Grop_Numb.Text = reader["Grop_Numb"].ToString();
                strGroupNumb.Value = reader["Grop_Numb"].ToString();
                //稅險
                intTax = Convert.ToInt32(reader["Grop_Tax"].ToString());
                //簽證
                intVisa = Convert.ToInt32(reader["Grop_Visa"].ToString());
                //所有報價
                fn_Show_Tour_Price(reader["Van_Number"].ToString());
            }
            reader.Close();
            comm.Dispose();

            TextBox Remark2 = (TextBox)this.PreviousPage.FindControl("txt_Remark2");
            txt_Remark2.Text = Remark2.Text;
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

        //團體售價
        int value1 = int.Parse(Price_Show_0.Text, NumberStyles.AllowThousands);
        value1 += int.Parse(Price_Show_1.Text, NumberStyles.AllowThousands);
        value1 += int.Parse(Price_Show_2.Text, NumberStyles.AllowThousands);
        value1 += int.Parse(Price_Show_3.Text, NumberStyles.AllowThousands);
        PT1.Text = value1.ToString("#,0");
        //金額合計
        PT2.Text = value1.ToString("#,0");

        
    }

    protected void fn_Show_Tour_Price(string strNumber)
    {
        int UsePointTemp = 0;
        int MustPointTemp = 0;
        int RemainPointTemp = 0;

        if (ppUsePoint.Text != "")
        {
            UsePointTemp = Convert.ToInt32(ppUsePoint.Text);
            MustPointTemp = Convert.ToInt32(ppMustPoint.Text);
            RemainPointTemp = Convert.ToInt32(ppRemainPoint.Text);
        }

        
        string strSql = "";
        strSql = " SELECT [Number],[Sequ_No],[Tick_Type],[Tour_Type],[Bed_Type]";
        strSql += " ,[Cruises_Type],IsNull([SalePrice],0) AS SalePrice,IsNull([AgentPrice],0) AS AgentPrice";
        strSql += " FROM [Tour_Price]";
        strSql += " WHERE Number = @Number ";
        strSql += " ORDER BY [Sequ_No]";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TRIPConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@Number", strNumber));
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                int intAgentPrice = Convert.ToInt32(reader["AgentPrice"].ToString());
                if (reader["Tick_Type"].ToString() == "A")
                {
                    //沒價格不鎖
                    //Price_Show_0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                    //Price_Single_0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                    //Price_Discount_0.Text = intDiscount.ToString();
                    //TextBox TextBox0 = (TextBox)this.PreviousPage.FindControl("TextBox0");
                    //TextBox0.Text = string.IsNullOrEmpty(TextBox0.Text) ? "0" : TextBox0.Text;
                    //Price_People_0.Text = TextBox0.Text;
                    //Price_Total_0.Text = ((intAgentPrice - intDiscount) * Convert.ToInt32(TextBox0.Text)).ToString("#,0");
                    //沒價格要鎖
                    if (intAgentPrice > 9000)
                    {
                        
                        Price_Single_0.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                        

                        TextBox TextBox0 = (TextBox)this.PreviousPage.FindControl("TextBox0");
                        TextBox0.Text = string.IsNullOrEmpty(TextBox0.Text) ? "0" : TextBox0.Text;
                        Price_People_0.Text = TextBox0.Text;
                        if(UsePointTemp==1)
                        {
                            if(RemainPointTemp>=MustPointTemp*Convert.ToInt32(Price_People_0.Text))
                            {
                                Price_Discount_0.Text = Convert.ToString(MustPointTemp*Convert.ToInt32(Price_People_0.Text));
                                RemainPointTemp=RemainPointTemp-Convert.ToInt32(Price_Discount_0.Text);
                            }
                            else
                            {
                                Price_Discount_0.Text = Convert.ToString(RemainPointTemp);
                                RemainPointTemp = RemainPointTemp - Convert.ToInt32(Price_Discount_0.Text);
                            }
                        }
                        else
                        {
                            Price_Discount_0.Text = intDiscount.ToString();
                        }

                        Price_Total_0.Text = ((intAgentPrice - intDiscount) * Convert.ToInt32(TextBox0.Text)).ToString("#,0");
                        Price_Show_0.Text = Convert.ToString(Convert.ToInt32(Price_Total_0.Text.Replace(",", "")) - Convert.ToInt32(Price_Discount_0.Text));
                    }
                    else
                    {
                        Price_Show_0.Text = "0"; Price_Single_0.Text = "0"; Price_Discount_0.Text = "0"; Price_People_0.Text = "0"; Price_Total_0.Text = "0";
                    }
                }
                else
                {
                    switch (reader["Bed_Type"].ToString().ToUpper())
                    {
                        case "1": // 小孩 佔床
                            //沒價格不鎖
                            //Price_Show_1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //Price_Single_1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //Price_Discount_1.Text = intDiscount.ToString();
                            //TextBox TextBox1 = (TextBox)this.PreviousPage.FindControl("TextBox1");
                            //TextBox1.Text = string.IsNullOrEmpty(TextBox1.Text) ? "0" : TextBox1.Text;
                            //Price_People_1.Text = TextBox1.Text;
                            //Price_Total_1.Text = ((intAgentPrice - intDiscount) * Convert.ToInt32(TextBox1.Text)).ToString("#,0");
                            //沒價格要鎖
                            if (intAgentPrice > 9000)
                            {
                                
                                Price_Single_1.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                               
                                TextBox TextBox1 = (TextBox)this.PreviousPage.FindControl("TextBox1");
                                TextBox1.Text = string.IsNullOrEmpty(TextBox1.Text) ? "0" : TextBox1.Text;
                                Price_People_1.Text = TextBox1.Text;
                                if(UsePointTemp==1)
                                {
                                    if(RemainPointTemp>=MustPointTemp*Convert.ToInt32(Price_People_1.Text))
                                    {
                                        Price_Discount_1.Text = Convert.ToString(MustPointTemp*Convert.ToInt32(Price_People_1.Text));
                                        RemainPointTemp=RemainPointTemp-Convert.ToInt32(Price_Discount_1.Text);
                                    }
                                    else
                                    {
                                        Price_Discount_1.Text = Convert.ToString(RemainPointTemp);
                                        RemainPointTemp = RemainPointTemp - Convert.ToInt32(Price_Discount_1.Text);
                                    }
                                }
                                else
                                {
                                    Price_Discount_1.Text = intDiscount.ToString();
                                }

                                Price_Total_1.Text = ((intAgentPrice - intDiscount) * Convert.ToInt32(TextBox1.Text)).ToString("#,0");
                                Price_Show_1.Text = Convert.ToString(Convert.ToInt32(Price_Total_1.Text.Replace(",", "")) - Convert.ToInt32(Price_Discount_1.Text));
                            }
                            else
                            {
                                Price_Show_1.Text = "0"; Price_Single_1.Text = "0"; Price_Discount_1.Text = "0"; Price_People_1.Text = "0"; Price_Total_1.Text = "0";
                            }
                            break;
                        case "2": // 小孩 不佔床
                            //沒價格不鎖
                            //Price_Show_3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //Price_Single_3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //Price_Discount_3.Text = intDiscount.ToString();
                            //TextBox TextBox3 = (TextBox)this.PreviousPage.FindControl("TextBox3");
                            //TextBox3.Text = string.IsNullOrEmpty(TextBox3.Text) ? "0" : TextBox3.Text;
                            //Price_People_3.Text = TextBox3.Text;
                            //Price_Total_3.Text = ((intAgentPrice - intDiscount) * Convert.ToInt32(TextBox3.Text)).ToString("#,0");
                            //沒價格要鎖
                            if (intAgentPrice > 9000)
                            {
                                
                                Price_Single_3.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                
                                TextBox TextBox3 = (TextBox)this.PreviousPage.FindControl("TextBox3");
                                TextBox3.Text = string.IsNullOrEmpty(TextBox3.Text) ? "0" : TextBox3.Text;
                                Price_People_3.Text = TextBox3.Text;

                                if(UsePointTemp==1)
                                {
                                    if(RemainPointTemp>=MustPointTemp*Convert.ToInt32(Price_People_3.Text))
                                    {
                                        Price_Discount_3.Text = Convert.ToString(MustPointTemp*Convert.ToInt32(Price_People_3.Text));
                                        RemainPointTemp=RemainPointTemp-Convert.ToInt32(Price_Discount_3.Text);
                                    }
                                    else
                                    {
                                        Price_Discount_3.Text = Convert.ToString(RemainPointTemp);
                                        RemainPointTemp = RemainPointTemp - Convert.ToInt32(Price_Discount_3.Text);
                                    }
                                }
                                else
                                {
                                    Price_Discount_3.Text = intDiscount.ToString();
                                }

                                Price_Total_3.Text = ((intAgentPrice - intDiscount) * Convert.ToInt32(TextBox3.Text)).ToString("#,0");
                                Price_Show_3.Text = Convert.ToString(Convert.ToInt32(Price_Total_3.Text.Replace(",", "")) - Convert.ToInt32(Price_Discount_3.Text));
                            }
                            else
                            {
                                Price_Show_3.Text = "0"; Price_Single_3.Text = "0"; Price_Discount_3.Text = "0"; Price_People_3.Text = "0"; Price_Total_3.Text = "0";
                            }
                            break;
                        case "3": // 小孩 加床
                            //沒價格不鎖
                            //Price_Show_2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //Price_Single_2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                            //Price_Discount_2.Text = intDiscount.ToString();
                            //TextBox TextBox2 = (TextBox)this.PreviousPage.FindControl("TextBox2");
                            //TextBox2.Text = string.IsNullOrEmpty(TextBox2.Text) ? "0" : TextBox2.Text;
                            //Price_People_2.Text = TextBox2.Text;
                            //Price_Total_2.Text = ((intAgentPrice - intDiscount) * Convert.ToInt32(TextBox2.Text)).ToString("#,0");
                            //沒價格要鎖
                            if (intAgentPrice > 9000)
                            {
                                
                                Price_Single_2.Text = Convert.ToInt32(intAgentPrice.ToString()).ToString("#,0");
                                
                                TextBox TextBox2 = (TextBox)this.PreviousPage.FindControl("TextBox2");
                                TextBox2.Text = string.IsNullOrEmpty(TextBox2.Text) ? "0" : TextBox2.Text;
                                Price_People_2.Text = TextBox2.Text;

                                if(UsePointTemp==1)
                                {
                                    if(RemainPointTemp>=MustPointTemp*Convert.ToInt32(Price_People_2.Text))
                                    {
                                        Price_Discount_2.Text = Convert.ToString(MustPointTemp*Convert.ToInt32(Price_People_2.Text));
                                        RemainPointTemp=RemainPointTemp-Convert.ToInt32(Price_Discount_2.Text);
                                    }
                                    else
                                    {
                                        Price_Discount_2.Text = Convert.ToString(RemainPointTemp);
                                        RemainPointTemp = RemainPointTemp - Convert.ToInt32(Price_Discount_2.Text);
                                    }
                                }
                                else
                                {
                                    Price_Discount_2.Text = intDiscount.ToString();
                                }

                                Price_Total_2.Text = ((intAgentPrice - intDiscount) * Convert.ToInt32(TextBox2.Text)).ToString("#,0");
                                Price_Show_2.Text = Convert.ToString(Convert.ToInt32(Price_Total_2.Text.Replace(",", "")) - Convert.ToInt32(Price_Discount_2.Text));
                            }
                            else
                            {
                                Price_Show_2.Text = "0"; Price_Single_2.Text = "0"; Price_Discount_2.Text = "0"; Price_People_2.Text = "0"; Price_Total_2.Text = "0";
                            }
                            break;
                    }
                }
            }
            reader.Close();
            comm.Dispose();

            int PeopleNumber=Convert.ToInt32(Price_People_0.Text)+Convert.ToInt32(Price_People_1.Text)+Convert.ToInt32(Price_People_3.Text)+Convert.ToInt32(Price_People_2.Text);
            if (UsePointTemp == 1)
            {
                if (Convert.ToInt32(ppRemainPoint.Text) >= (Convert.ToInt32(ppMustPoint.Text) * PeopleNumber))
                {

                    UsePoint.Text = Convert.ToString(Convert.ToUInt32(ppMustPoint.Text) * PeopleNumber);
                    RemainPoint.Text = Convert.ToString(Convert.ToInt32(ppRemainPoint.Text) - Convert.ToUInt32(UsePoint.Text));
                }
                else
                {
                    UsePoint.Text = ppRemainPoint.Text;
                    RemainPoint.Text = "0";
                }
            }
            else
            {
                UsePoint.Text = "0";
                RemainPoint.Text = Convert.ToString(Convert.ToInt32(ppRemainPoint.Text));
            }
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

    protected void btn_previous_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "history.go(-2);", true); return;
    }

    protected void btn_next_Click(object sender, EventArgs e)
    {
        btn_next.Enabled = false;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", someScript, true); return;
        //Response.Write(" <script type=\"text/javascript\">document.forms.reg.submit();</script>");
        //Response.Write(" <script type=\"text/javascript\">send();</script>");
        //ClientScript.RegisterClientScriptBlock(Page.GetType(), "t", "<script>aaa();</script>");
        //Response.Write(MyFunction.XmlPost.fn_Show_data("法克魷"));

        fn_Save();
        //fn_Send_XML();
        //Response.Write(ResponseXML);
    }

    protected void fn_Save()
    {
        string strSql = "";
        string strConnTel = "";
        string strConnFax = "";
        string strComp_Code = "";
        string strCONT_CELL = "";
        string strCONT_MAIL = "";
        // ****************************************************************************************************
        // 抓取聯絡人相關資料
        strSql += " SELECT Agent_M.AGT_NAME1,Agent_M.TEL1_ZONE,Agent_M.TEL1,Agent_M.FAX_ZONE,Agent_M.FAX";
        strSql += " ,Agent_M.Sales,AGENT_L.CONT_ZONE,AGENT_L.CONT_TEL,AGENT_L.CFAX_ZONE,AGENT_L.CONT_FAX";
        strSql += " ,AGENT_L.CONT_CELL,AGENT_L.CONT_MAIL";
        strSql += " FROM Agent_M";
        strSql += " LEFT JOIN AGENT_L ON AGENT_L.AGT_NAME1 = Agent_M.AGT_NAME1";
        strSql += " WHERE Agent_M.COMP_NO = @COMP_NO";
        strSql += " AND AGENT_L.AGT_IDNo = @AGT_IDNo";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        try
        {
            connect.Open();
            SqlCommand comm = new SqlCommand(strSql, connect);
            comm.Parameters.Add(new SqlParameter("@COMP_NO", Session["Compno"]));
            comm.Parameters.Add(new SqlParameter("@AGT_IDNo", Session["PerIDNo"]));
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                // 同業聯絡人(若同業有輸入資料就帶資料，若沒有的話帶公司資料)
                if (!string.IsNullOrEmpty(reader["CONT_TEL"].ToString()))
                {
                    if (!string.IsNullOrEmpty(reader["CONT_ZONE"].ToString()))
                    { strConnTel += reader["CONT_ZONE"].ToString(); }
                    if (!string.IsNullOrEmpty(reader["CONT_TEL"].ToString()))
                    { strConnTel += (string.IsNullOrEmpty(strConnTel) ? "" : "-") + reader["CONT_TEL"].ToString(); }
                }
                else
                {
                    if (!string.IsNullOrEmpty(reader["TEL1_ZONE"].ToString()))
                    { strConnTel += reader["TEL1_ZONE"].ToString(); }
                    if (!string.IsNullOrEmpty(reader["TEL1"].ToString()))
                    { strConnTel += (string.IsNullOrEmpty(strConnTel) ? "" : "-") + reader["TEL1"].ToString(); }
                }


                if (!string.IsNullOrEmpty(reader["CONT_FAX"].ToString()))
                {
                    if (!string.IsNullOrEmpty(reader["CFAX_ZONE"].ToString()))
                    { strConnFax += reader["CFAX_ZONE"].ToString(); }
                    if (!string.IsNullOrEmpty(reader["CONT_FAX"].ToString()))
                    { strConnFax += (string.IsNullOrEmpty(strConnFax) ? "" : "-") + reader["CONT_FAX"].ToString(); }
                }
                else
                {
                    if (!string.IsNullOrEmpty(reader["FAX_ZONE"].ToString()))
                    { strConnFax += reader["FAX_ZONE"].ToString(); }
                    if (!string.IsNullOrEmpty(reader["FAX"].ToString()))
                    { strConnFax += (string.IsNullOrEmpty(strConnFax) ? "" : "-") + reader["FAX"].ToString(); }
                }

                strComp_Code = reader["AGT_NAME1"].ToString();
                strCONT_CELL = reader["CONT_CELL"].ToString();
                strCONT_MAIL = reader["CONT_MAIL"].ToString();
            }
            reader.Close();
            comm.Dispose();
        }
        catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true); return; }
        finally { connect.Close(); }


        // ****************************************************************************************************
        // 若取不到資料，就抓取 agent_m 的資料
        if (string.IsNullOrEmpty(strConnTel) && string.IsNullOrEmpty(strConnFax))
        {
            strSql = " SELECT AGT_NAME1,(TEL1_ZONE + TEL1) as TEL ,(FAX_ZONE + FAX) as FAX ,Sales ";
            strSql += " FROM Agent_M";
            strSql += " where COMP_NO = @COMP_NO ";
            try
            {
                connect.Open();
                SqlCommand comm = new SqlCommand(strSql, connect);
                comm.Parameters.Add(new SqlParameter("@COMP_NO", Session["Compno"]));
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    strConnTel = reader["TEL"].ToString();
                    strConnFax = reader["FAX"].ToString();
                    strComp_Code = reader["AGT_NAME1"].ToString();
                }
                reader.Close();
                comm.Dispose();
            }
            catch { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('出錯了。');", true); return; }
            finally { connect.Close(); }
        }

        // ****************************************************************************************************
        // 檢查字元長度
        if (strConnTel.Length > 16)
        { strConnTel = strConnTel.Substring(0, 16); }
        if (strConnFax.Length > 16)
        { strConnFax = strConnFax.Substring(0, 16); }
        if (strCONT_CELL.Length > 10)
        { strCONT_CELL = strCONT_CELL.Substring(0, 10); }


        // ****************************************************************************************************
        strSql = "DECLARE @numberdate nvarchar(6);";
        strSql += " DECLARE @Number nvarchar(12);";
        strSql += " select @numberdate =";
        strSql += " cast(year(getdate()) as nvarchar) + (case when len(cast(month(getdate()) as nvarchar)) = 1 then '0'+ cast(month(getdate()) as nvarchar) else cast(month(getdate()) as nvarchar) end)";
        strSql += " SELECT @Number =";
        strSql += " 'N'+ @numberdate";
        strSql += " + REPLICATE('0', 5 - len(CAST(isnull(max(CAST(right(NetCustNumb,5) AS float)),0)+ 1 as nvarchar)))";
        strSql += " + CAST(isnull(max(cast(right(NetCustNumb,5) as float)),0)+1 as nvarchar)";
        strSql += " FROM Tr10_OL";
        strSql += " where left(NetCustNumb,7) = 'N' + @numberdate";

        strSql += " insert into Tr10_OL ( ";
        strSql += " Cust_Numb ,netcustnumb ,Enli_Code ,EnliI_Date ,Tour_Numb ";
        strSql += " ,Comp_Code ,Comp_Conn ,ConnTel ,ConnFax ,BookPax ";
        strSql += " ,TourFee ,Remark ,Remark2 ,crea_date ,crea_user ";
        strSql += " ,loginIP ,K_T1 ,K_T2 ,CONT_CELL ,CONT_MAIL";
        strSql += " ) VALUES ( ";
        strSql += " @Cust_Numb ,@Number ,@Enli_Code ,@EnliI_Date ,@Tour_Numb ";
        strSql += " ,@Comp_Code ,@Comp_Conn ,@ConnTel ,@ConnFax ,@BookPax ";
        strSql += " ,@TourFee ,@Remark ,@Remark2 ,@crea_date ,@crea_user ";
        strSql += " ,@loginIP ,@K_T1 ,@K_T2 ,@CONT_CELL ,@CONT_MAIL";
        strSql += " ) ";
        strSql += " select @Number as Number,scope_identity() AS Tr10Number";

        connect.Open();
        SqlCommand cmd = new SqlCommand(strSql, connect);
        cmd.Parameters.Add(new SqlParameter("@Cust_Numb", ""));
        cmd.Parameters.Add(new SqlParameter("@Enli_Code", "2"));
        cmd.Parameters.Add(new SqlParameter("@EnliI_Date", Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")));
        cmd.Parameters.Add(new SqlParameter("@Tour_Numb", Lbl_Grop_Numb.Text));

        cmd.Parameters.Add(new SqlParameter("@Comp_Code", strComp_Code));
        cmd.Parameters.Add(new SqlParameter("@Comp_Conn", Session["PerName"]));
        cmd.Parameters.Add(new SqlParameter("@ConnTel", strConnTel));
        cmd.Parameters.Add(new SqlParameter("@ConnFax", strConnFax));
        cmd.Parameters.Add(new SqlParameter("@BookPax", Convert.ToInt32(Price_People_0.Text) + Convert.ToInt32(Price_People_1.Text) + Convert.ToInt32(Price_People_2.Text) + Convert.ToInt32(Price_People_3.Text)));

        cmd.Parameters.Add(new SqlParameter("@TourFee", Convert.ToInt32(int.Parse(Price_Single_0.Text, NumberStyles.AllowThousands))));
        cmd.Parameters.Add(new SqlParameter("@Remark", hid_Remark1.Value));
        cmd.Parameters.Add(new SqlParameter("@Remark2", txt_Remark2.Text));
        cmd.Parameters.Add(new SqlParameter("@crea_date", Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")));
        cmd.Parameters.Add(new SqlParameter("@crea_user", Session["PERNO"]));

        cmd.Parameters.Add(new SqlParameter("@loginIP", IPAddress));
        cmd.Parameters.Add(new SqlParameter("@K_T1", Convert.ToInt32(hid_kt1.Value)));
        cmd.Parameters.Add(new SqlParameter("@K_T2", Convert.ToInt32(hid_kt2.Value)));
        cmd.Parameters.Add(new SqlParameter("@CONT_CELL", strCONT_CELL));
        cmd.Parameters.Add(new SqlParameter("@CONT_MAIL", strCONT_MAIL));

        SqlDataReader reader1 = cmd.ExecuteReader();
        if (reader1.Read())
        {
            strCustNumb.Value = reader1["Number"].ToString();
            //Response.Write(strCustNumb.Value + strGroupNumb.Value);
            //Response.End();
            fn_Save_TR20(reader1["Tr10Number"].ToString());
            fn_Send_XML(reader1["Tr10Number"].ToString());
        }
        reader1.Close();
        cmd.Dispose();

        try
        {
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

    protected void fn_Save_TR20(string strTr10Number)
    {
        string strSql = "";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);

        ///2017.11.24新增紅利寫回TR20_OL
        int MustPointTemp = 0, RemainPointTemp = 0,BeforeRemainPoint=0;
        string UsePointTemp = ppUsePoint.Text;
        MustPointTemp = Convert.ToInt32(ppMustPoint.Text);
        RemainPointTemp = Convert.ToInt32(ppRemainPoint.Text);
        BeforeRemainPoint=Convert.ToInt32(ppRemainPoint.Text);
        ///

        for (int i = 0; i <= 3; i++)
        {
            strSql = " insert into Tr20_OL ( ";
            strSql += " Tr10Number ,Enli_Date ,GROP_NUMB ,BookPax ,Tour_Mony ,Tick_Type ,Bed_Type,Tr20_Money_Back";
            strSql += " ) VALUES ( ";
            strSql += " @Tr10Number ,@Enli_Date ,@GROP_NUMB ,@BookPax ,@Tour_Mony ,@Tick_Type ,@Bed_Type,@Tr20_Money_Back ";
            strSql += " ) ";

            connect.Open();
            SqlCommand cmd = new SqlCommand(strSql, connect);
            cmd.Parameters.Add(new SqlParameter("@Tr10Number", strTr10Number));
            cmd.Parameters.Add(new SqlParameter("@Enli_Date", Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss")));
            cmd.Parameters.Add(new SqlParameter("@GROP_NUMB", Lbl_Grop_Numb.Text));


            
            switch (i.ToString())
            {
                case "0":
                    cmd.Parameters.Add(new SqlParameter("@BookPax", Convert.ToInt32(Price_People_0.Text)));
                    //if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_0.Text) != "" && RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_0.Text) && RemainPointTemp > 0)
                    //{
                    //    txtSingle0.Text = Convert.ToString(Convert.ToInt32(Price_Single_0.Text.Replace(",", "")) - MustPointTemp);
                    //    cmd.Parameters.Add(new SqlParameter("@Tour_Mony",txtSingle0.Text));
                    //}
                    //else
                    //{
                        cmd.Parameters.Add(new SqlParameter("@Tour_Mony", Convert.ToInt32(int.Parse(Price_Single_0.Text, NumberStyles.AllowThousands))));
                    //}
                    cmd.Parameters.Add(new SqlParameter("@Tick_Type", "A"));
                    cmd.Parameters.Add(new SqlParameter("@Bed_Type", ""));
                    //if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_0.Text) != "" && RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_0.Text) && RemainPointTemp > 0)
                    if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_0.Text) != "" && RemainPointTemp > 0 && RemainPointTemp >= MustPointTemp)
                    {
                        if (RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_0.Text))
                        {

                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", Convert.ToString(MustPointTemp * Convert.ToInt32(Price_People_0.Text))));
                            RemainPointTemp = RemainPointTemp - MustPointTemp * Convert.ToInt32(Price_People_0.Text);
                        }
                        else
                        {

                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", RemainPointTemp));
                            RemainPointTemp = 0;
                            
                            //for (int j = 1; j <= Convert.ToInt32(Price_People_0.Text); j++)
                            //{
                            //    if (RemainPointTemp >= MustPointTemp * j)
                            //    {

                            //    }
                            //    else
                            //    {
                            //        cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", MustPointTemp * (j - 1)));
                            //        RemainPointTemp = RemainPointTemp - (MustPointTemp * (j - 1));
                            //        break;
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        if (ppUsePoint.Text == "1" && RemainPointTemp > 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", RemainPointTemp));
                            RemainPointTemp = 0;
                        }
                        else
                        {
                            //txtPriceTotal0.Text = Price_Total_0.Text;
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", ""));
                        }
                    }
                    if (Price_People_0.Text != "0") { cmd.ExecuteNonQuery(); }
                    break;
                case "1":
                    cmd.Parameters.Add(new SqlParameter("@BookPax", Convert.ToInt32(Price_People_1.Text)));
                    //if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_1.Text) != "" && RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_1.Text) && RemainPointTemp > 0)
                    //{


                    //    txtSingle1.Text = Convert.ToString(Convert.ToInt32(Price_Single_1.Text.Replace(",", "")) - MustPointTemp);
                    //    cmd.Parameters.Add(new SqlParameter("@Tour_Mony", txtSingle1.Text));
                    //}
                    //else
                    //{
                        cmd.Parameters.Add(new SqlParameter("@Tour_Mony", Convert.ToInt32(int.Parse(Price_Single_1.Text, NumberStyles.AllowThousands))));
                    //}
                    cmd.Parameters.Add(new SqlParameter("@Tick_Type", "C"));
                    cmd.Parameters.Add(new SqlParameter("@Bed_Type", "1"));
                    //if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_1.Text) != "" && RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_1.Text) && RemainPointTemp > 0)
                    if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_1.Text) != "" && RemainPointTemp > 0 && RemainPointTemp >= MustPointTemp)
                    {
                        if (RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_1.Text))
                        {

                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", Convert.ToString(MustPointTemp * Convert.ToInt32(Price_People_1.Text))));
                            RemainPointTemp = RemainPointTemp - MustPointTemp * Convert.ToInt32(Price_People_1.Text);
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", RemainPointTemp));
                            RemainPointTemp = 0;
                            //for (int j = 1; j <= Convert.ToInt32(Price_People_1.Text); j++)
                            //{
                            //    if (RemainPointTemp >= MustPointTemp * j)
                            //    {

                            //    }
                            //    else
                            //    {
                            //        cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", MustPointTemp * (j - 1)));
                            //        RemainPointTemp = RemainPointTemp - (MustPointTemp * (j - 1));
                            //        break;
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        if (ppUsePoint.Text == "1" && RemainPointTemp > 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", RemainPointTemp));
                            RemainPointTemp = 0;
                        }
                        else
                        {
                            //txtPriceTotal1.Text = Price_Total_1.Text;
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", ""));
                        }
                    }
                    if (Price_People_1.Text != "0") { cmd.ExecuteNonQuery(); }
                    break;
                case "2":
                    cmd.Parameters.Add(new SqlParameter("@BookPax", Convert.ToInt32(Price_People_2.Text)));
                    //if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_2.Text) != "" && RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_2.Text) && RemainPointTemp > 0)
                    //{


                    //    txtSingle2.Text = Convert.ToString(Convert.ToInt32(Price_Single_2.Text.Replace(",", "")) - MustPointTemp);
                    //    cmd.Parameters.Add(new SqlParameter("@Tour_Mony", txtSingle2.Text));
                    //}
                    //else
                    //{
                        cmd.Parameters.Add(new SqlParameter("@Tour_Mony", Convert.ToInt32(int.Parse(Price_Single_2.Text, NumberStyles.AllowThousands))));
                    //}
                    cmd.Parameters.Add(new SqlParameter("@Tick_Type", "C"));
                    cmd.Parameters.Add(new SqlParameter("@Bed_Type", "3"));
                    //if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_2.Text) != "" && RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_2.Text) && RemainPointTemp > 0)
                    if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_2.Text) != "" && RemainPointTemp > 0 && RemainPointTemp >= MustPointTemp)
                    {
                        if (RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_2.Text))
                        {

                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", Convert.ToString(MustPointTemp * Convert.ToInt32(Price_People_2.Text))));
                            RemainPointTemp = RemainPointTemp - MustPointTemp * Convert.ToInt32(Price_People_2.Text);
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", RemainPointTemp));
                            RemainPointTemp = 0;
                            //for (int j = 1; j <= Convert.ToInt32(Price_People_2.Text); j++)
                            //{
                            //    if (RemainPointTemp >= MustPointTemp * j)
                            //    {

                            //    }
                            //    else
                            //    {
                            //        cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", MustPointTemp * (j - 1)));
                            //        RemainPointTemp = RemainPointTemp - (MustPointTemp * (j - 1));
                            //        break;
                            //    }
                            //}
                        }
                    }
                    else
                    {
                        if (ppUsePoint.Text == "1" && RemainPointTemp > 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", RemainPointTemp));
                            RemainPointTemp = 0;
                        }
                        else
                        {
                            //txtPriceTotal2.Text = Price_Total_2.Text;
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", ""));
                        }
                    }
                    if (Price_People_2.Text != "0") { cmd.ExecuteNonQuery(); }
                    break;
                case "3":
                    cmd.Parameters.Add(new SqlParameter("@BookPax", Convert.ToInt32(Price_People_3.Text)));
                    //if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_3.Text) != "" && RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_3.Text) && RemainPointTemp > 0)
                    //{

                        
                    //    txtSingle3.Text = Convert.ToString(Convert.ToInt32(Price_Single_3.Text.Replace(",", "")) - MustPointTemp);
                    //    cmd.Parameters.Add(new SqlParameter("@Tour_Mony", txtSingle3.Text));

                    //}
                    //else
                    //{
                        cmd.Parameters.Add(new SqlParameter("@Tour_Mony", Convert.ToInt32(int.Parse(Price_Single_3.Text, NumberStyles.AllowThousands))));
                    //}
                    cmd.Parameters.Add(new SqlParameter("@Tick_Type", "C"));
                    cmd.Parameters.Add(new SqlParameter("@Bed_Type", "2"));
                    //if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_3.Text) != "" && RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_3.Text) && RemainPointTemp > 0)
                    if (ppUsePoint.Text == "1" && Convert.ToString(Price_People_3.Text) != "" && RemainPointTemp > 0 && RemainPointTemp >= MustPointTemp)
                    {
                        if (RemainPointTemp >= MustPointTemp * Convert.ToInt32(Price_People_3.Text))
                        {
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", Convert.ToString(MustPointTemp * Convert.ToInt32(Price_People_3.Text))));
                            RemainPointTemp = RemainPointTemp - MustPointTemp * Convert.ToInt32(Price_People_3.Text);
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", RemainPointTemp));
                            RemainPointTemp = 0;
                            //for(int j = 1;j <= Convert.ToInt32(Price_People_3.Text);j++)
                            //{
                            //    if (RemainPointTemp >= MustPointTemp * j)
                            //    {

                            //    }
                            //    else
                            //    {
                            //        cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", MustPointTemp*(j-1)));
                            //        RemainPointTemp = RemainPointTemp-(MustPointTemp * (j - 1));
                            //        break;
                            //    }
                            //}
                        }
                       
                    }
                    else
                    {
                        if (ppUsePoint.Text == "1" && RemainPointTemp > 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", RemainPointTemp));
                            RemainPointTemp = 0;
                        }
                        else
                        {
                            //txtPriceTotal3.Text = Price_Total_3.Text;
                            cmd.Parameters.Add(new SqlParameter("@Tr20_Money_Back", ""));
                        }
                    }
                    if (Price_People_3.Text != "0") { cmd.ExecuteNonQuery(); }
                    break;
            }
            cmd.Dispose();
            connect.Close();
        }
        if (UsePointTemp == "1")
        {
            PointRecode(RemainPointTemp, BeforeRemainPoint);
        }
    }

    protected void fn_Send_XML(string strTr10Number)
    {
        //要傳送的資料
        string strXML = "", strURL="";
        strXML += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        strXML += "<Message>";
        strXML += "<MA>";
        strXML += "<A><![CDATA[artisan988]]></A>";  //驗證碼
        strXML += "<B><![CDATA[" + strCustNumb.Value + "]]></B>";  //網路報名單號
        strXML += "<C><![CDATA[" + strGroupNumb.Value + "]]></C>";  //團號
        strXML += "</MA>";
        strXML += "</Message>";
        //strURL = "http://localhost:59397/xml/GetApplyChecked.aspx";
        strURL = "http://210.71.206.199:502/xmltest/GetApplyChecked.aspx";
        string strComNum = "";
        fn_Show_XML(SendRequest(strURL, strXML, strComNum));

    }

    #region " --- 傳送XML資料 --- "
    /// <summary>
    /// 傳送的function
    /// </summary>
    /// <param name="uri">網址</param>
    /// <param name="poscontent">傳送的資料</param>
    /// <returns></returns>
    public string SendRequest(string uri, string poscontent, string strComNum)
    {
        string strMessage = "";
        string responseText = "";

        //設置編碼
        byte[] postBody = System.Text.Encoding.UTF8.GetBytes(poscontent);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.Method = "POST";
        //request.Timeout = 60000;  //設置超時屬性。默認為100000毫秒（100秒）。
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = postBody.Length;
        request.AllowWriteStreamBuffering = true;
        HttpWebResponse response = null;
        Stream dataStream = null;
        StreamReader reader = null;
        try
        {
            dataStream = request.GetRequestStream();
            dataStream.Write(postBody, 0, postBody.Length);
            dataStream.Close();
            response = (HttpWebResponse)request.GetResponse();
            dataStream = response.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
            reader = new StreamReader(dataStream, encode);
            responseText = reader.ReadToEnd(); //回傳結果
        }
        catch (WebException ex1)
        {
            //HttpWebResponse exResponse = (HttpWebResponse)ex1.Response;
            //MessageBox.Show(ex1.Message);
            strMessage = ex1.ToString();
        }
        catch (NotSupportedException ex2)
        {
            //MessageBox.Show(ex2.Message);
            strMessage = ex2.ToString();
        }
        catch (ProtocolViolationException ex3)
        {
            //MessageBox.Show(ex3.Message);
            strMessage = ex3.ToString();
        }
        catch (InvalidOperationException ex4)
        {
            //MessageBox.Show(ex4.Message);
            strMessage = ex4.ToString();
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
            strMessage = ex.ToString();
        }
        finally
        {
            if (response != null) response.Close();
            if (dataStream != null) dataStream.Close();
            if (reader != null) reader.Close();
        }

        if (string.IsNullOrEmpty(responseText))
        {
            string strPrint = "";
            strPrint = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

            strPrint += "<SHOWMSG>";
            strPrint += "<SHOWDATA>";
            strPrint += "<SHOPID>" + strComNum + "</SHOPID>";
            strPrint += "<DETAIL_NUM></DETAIL_NUM >";
            strPrint += "<DETAIL_ITEM></DETAIL_ITEM >";
            strPrint += "<STATUS_CODE><![CDATA[1003]]></STATUS_CODE>";
            strPrint += "<STATUS_DESC><![CDATA[系統維護中]]></STATUS_DESC>";
            strPrint += "<SYS_DESC><![CDATA[" + strMessage + "]]></SYS_DESC>";
            strPrint += "<CONFIRM>FAIL</CONFIRM>";
            strPrint += "</SHOWDATA>";
            strPrint += "</SHOWMSG>";


            responseText = strPrint;
        }

        return responseText;
    }
    #endregion

    #region " --- 回傳XML資料 --- "
    /// <summary>
    /// 顯示回傳的xml資料
    /// </summary>
    /// <param name="xmlData"></param>
    protected void fn_Show_XML(string xmlData)
    {

        Session["ppUsePoint"] = ppUsePoint.Text;
        Session["ppMustPoint"] = ppMustPoint.Text;
        Session["ppRemainPoint"] = ppRemainPoint.Text;

        //if (System.Web.HttpContext.Current.Request.RequestType == "GET")
        {
            //接收並讀取POST過來的XML資料
            StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Request.InputStream);
            try
            {
                XmlDocument dom = new XmlDocument();
                dom.LoadXml(xmlData);

                ResponseXML = dom.SelectSingleNode("SHOWMSG").ChildNodes.Item(0).ChildNodes.Item(6).InnerText;
                if (ResponseXML == "OK") { Response.Redirect("Apply4.aspx"); }
                else { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('報名失敗')", true); return; }
                //顯示回傳訊息結果
                //string strPrint = "";
                //strPrint += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                //strPrint += "<SHOWMSG>";
                //strPrint += Show_XML(dom.SelectSingleNode("SHOWMSG").ChildNodes);
                //strPrint += "</SHOWMSG>";

                //System.Web.HttpContext.Current.Response.Write(strPrint);
            }
            catch (Exception ex)
            {
                string strPrint = "";
                strPrint += "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                strPrint += "<SHOWMSG>";
                strPrint += ex.ToString();
                strPrint += "</SHOWMSG>";

                //System.Web.HttpContext.Current.Response.ContentType = "text/xml";
                System.Web.HttpContext.Current.Response.Write(strPrint);
            }
        }
    }
    /// <summary>
    /// 顯示XML訊息
    /// </summary>
    /// <param name="strXmlMA"></param>
    /// <returns></returns>
    protected string Show_XML(XmlNodeList xnlXmlMA)
    {
        string strPrint = "";
        for (int ii = 0; ii <= xnlXmlMA.Count - 1; ii++)
        {
            System.Xml.XmlNodeList xnlXml = xnlXmlMA.Item(ii).ChildNodes;

            strPrint += xnlXmlMA.Item(ii).Name;
            strPrint += xnlXmlMA.Item(ii).InnerXml;
            //if (xnlXml.Count == 1)
            //{
            //    strPrint += "<" + xnlXmlMA.Item(ii).Name + ">" + xnlXmlMA.Item(ii).InnerXml + "</" + xnlXmlMA.Item(ii).Name + ">";
            //}
            //else
            //{
            //    strPrint += "<" + xnlXmlMA.Item(ii).Name + ">";
            //    strPrint += Show_XML(xnlXml);
            //    strPrint += "</" + xnlXmlMA.Item(ii).Name + ">";
            //}
        }

        return strPrint;
    }
    #endregion

    #region " --- IP --- "
    public static string IPAddress
    {
        get
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理    
                if (result.IndexOf(".") == -1)    //沒有，肯定是非IPv4格式  
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有，估計有多個代理，取第一個不是內網的ip。    
                        result = result.Replace(" ", "").Replace("\"", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (isIPAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                //&& temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i];    //找到不是內網的地址   
                            }
                        }
                    }
                    else if (isIPAddress(result)) //代理即是IP格式    
                        return result;
                    else
                        result = null;    //代理中的内容 非IP，取IP    
                }
            }
            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (result == null || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;
            return result;
        }
    }

    private static bool isIPAddress(string strAddress)
    {
        bool bResult = true;

        foreach (char ch in strAddress)
        {
            if ((false == Char.IsDigit(ch)) && (ch != '.'))
            {
                bResult = false;
                break;
            }
        }

        return bResult;
    }
    #endregion

    protected void PointRecode(int AfterRemainPoint, int BeforeRemainPoint)
    {
        try
        {
            string strconn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["B2BConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string strsql = "";
            strsql += " INSERT INTO Point_Recode(";
            strsql += " Cust_Idno,reduce,point_balance,crea_user,crea_date)";
            strsql += " VALUES(@CustIdno,@reduce,@point_balance,@crea_user,@crea_date)";
            SqlCommand comm = new SqlCommand(strsql,conn);
            comm.Parameters.Add(new SqlParameter("@CustIdno", Convert.ToString(Session["PerIDNo"])));
            comm.Parameters.Add(new SqlParameter("@reduce", BeforeRemainPoint - AfterRemainPoint));
            comm.Parameters.Add(new SqlParameter("@point_balance", AfterRemainPoint));
            comm.Parameters.Add(new SqlParameter("@crea_user", Convert.ToString(Session["PerIDNo"])));
            comm.Parameters.Add(new SqlParameter("@crea_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            comm.ExecuteNonQuery();
            comm.Dispose();
            conn.Close();
        }
        catch (Exception ex)
        { 
        }
    }


}