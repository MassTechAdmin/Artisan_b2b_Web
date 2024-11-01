using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// Format 的摘要描述
/// </summary>
public class Format
{
	public Format()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public static string Replace(object input, string uppath)
    {

        if (File.Exists(uppath) == false)//找不到檔案就新增一個Format text
        {
            File.WriteAllText(uppath, CreaFormat.ForStr(), System.Text.Encoding.UTF8);
        }

        StreamReader aa = new StreamReader(uppath, System.Text.Encoding.UTF8);


        string data = Convert.ToString(input);
        while (!aa.EndOfStream)
        {
            data = data.Replace(aa.ReadLine(), "");
        }


        data = data.Replace("'","&qapos;");


        aa.Dispose();
        aa.Close();


        return data;
    }

    /// <summary>
    /// 檢查是否為數字
    /// </summary>
    /// <param name="Expression"></param>
    /// <returns></returns>
    public bool IsNumeric(object Expression)
    {
        bool isNum;
        double retNum;
        isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    } 

    /// <summary>
    /// 將英數等半形符號，每兩個字元視為一個全形字/長度
    /// </summary>
    /// <param name="TheMixedString">混合字串(來源)</param>
    /// <param name="TheLength">欲取得之字串長度</param>
    /// <returns>String</returns>
    public string HalfWay(string TheMixedString, int TheLength)
    {
        try
        {
            if (string.IsNullOrEmpty(TheMixedString) | TheLength <= 0)
            {
                return "&nbsp;";
            }
            else
            {
                //char c = '\0';
                double sngTotalLength = 0;
                int intPosition = 0;
                foreach (char c in TheMixedString.ToCharArray())
                {
                    if (Convert.ToChar(Convert.ToInt32(c)).ToString().Length == 2)
                    {
                        sngTotalLength += 0.5;
                    }
                    else
                    {
                        sngTotalLength += 1;
                    }

                    //'這段的用意是用來避免讓回傳字串長度超出範圍 
                    if (sngTotalLength + 0.5 <= TheLength)
                    {
                        intPosition += 1;
                    }
                    else
                    {
                        break; // TODO: might not be correct. Was : Exit For 
                    }
                }

                if (TheMixedString.Length > TheLength)
                {
                    //return Strings.Left(TheMixedString, intPosition + 1) + "...";
                    return TheMixedString.Substring(0, intPosition + 1) + "...";
                }
                else
                {
                    //return Strings.Left(TheMixedString, intPosition + 1);
                    //return TheMixedString.Substring(0, intPosition + 1);
                    return TheMixedString;
                }

            }
        }
        catch (Exception ex)
        {
        }
        //Throw New Exception("HalfWay, " & ex.Message) 
        return "&nbsp;";
    }
}
