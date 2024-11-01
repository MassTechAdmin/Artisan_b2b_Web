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
using System.Drawing;
using System.Web.SessionState;
using System.ComponentModel;

public partial class Trip_gif : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string randomcode = this.CreateRandomCode(4);

        Session["v$code"] = randomcode;

        this.CreateImage(randomcode);
    }
    ///  <summary>
    ///  生成隨機碼
    ///  </summary>
    ///  <param  name="length">隨機碼個數</param>
    ///  <returns></returns>
    private string CreateRandomCode(int length)
    {
        int rand;
        char code;

        System.Random random = new Random();
        string randomcode = String.Empty;

        //生成一定長度的隨機碼    
        for (int i = 0; i < length; i++)
        {
            rand = random.Next();

            if (rand % 2 == 0)//大寫英文
            {
                code = (char)randA();
            }
            else//數字
            {
                code = (char)('2' + (char)(rand % 8));
            }

            randomcode += code.ToString();
        }

        return randomcode;
    }

    //英文大寫
    private int randA()
    {

        System.Random random = new Random();
        int rand;

        rand = random.Next() % 26 + 65;

        //如果是大寫i,o,u,v,z,j就重新取得亂碼
        if (rand == 73 || rand == 79 || rand == 83 || rand == 85 || rand == 86 || rand == 87 || rand == 90 || rand == 74)
        {
            rand = random.Next() % 26 + 65;
        }

        //如果是大寫i,o,u,v,z,j就重新取得亂碼
        if (rand == 73 || rand == 79 || rand == 83 || rand == 85 || rand == 86 || rand == 87 || rand == 90 || rand == 74)
        {
            rand = random.Next() % 26 + 65;
        }

        //如果是大寫i,o,u,v,z,j就重新取得亂碼
        if (rand == 73 || rand == 79 || rand == 83 || rand == 85 || rand == 86 || rand == 87 || rand == 90 || rand == 74)
        {
            rand = random.Next() % 26 + 65;
        }

        //如果是大寫i,o,u,v,z,j就重新取得亂碼
        if (rand == 73 || rand == 79 || rand == 83 || rand == 85 || rand == 86 || rand == 87 || rand == 90 || rand == 74)
        {
            rand = random.Next() % 26 + 65;
        }

        //如果是大寫i,o,u,v,z,j就重新取得亂碼
        if (rand == 73 || rand == 79 || rand == 83 || rand == 85 || rand == 86 || rand == 87 || rand == 90 || rand == 74)
        {
            rand = random.Next() % 26 + 65;
        }

        return rand;

    }



    ///  <summary>
    ///  創建隨機碼圖片
    ///  </summary>
    ///  <param  name="randomcode">隨機碼</param>
    private void CreateImage(string randomcode)
    {
        int randAngle = 25; //隨機轉動角度
        int mapwidth = (int)(randomcode.Length * 20);
        Bitmap map = new Bitmap(mapwidth, 28);//創建圖片背景

        Graphics graph = Graphics.FromImage(map);
        graph.Clear(Color.AliceBlue);//清除畫面，填充背景
        graph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1);//畫一個邊框
        graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//模式


        Random rand = new Random();

        //背景干擾點生成
        Pen blackPen;
        for (int i = 0; i < 100; i++)
        {
            int x = rand.Next(0, map.Width);
            int y = rand.Next(0, map.Height);

            if (x % 3 == 0)
            {
                blackPen = new System.Drawing.Pen(Color.LightGray, 0);
            }
            else
            {
                blackPen = new System.Drawing.Pen(Color.DarkSeaGreen, 0);
            }

            graph.DrawRectangle(blackPen, x, y, 1, 1);
        }


        //驗證碼旋轉，防止機器識別
        char[] chars = randomcode.ToCharArray();//拆散字符串成單字符數組

        //文字距中
        StringFormat format = new StringFormat(StringFormatFlags.NoClip);
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;

        //定義顏色
        Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Brown, Color.Purple, Color.DarkViolet, Color.DeepPink };

        //定義字體
        string[] font = { "Verdana", "Microsoft Sans Serif", "Arial", "細明體", };
        int fontsize = 15;


        for (int i = 0; i < chars.Length; i++)
        {
            int cindex = rand.Next(7);
            int findex = rand.Next(4);

            Font f = new System.Drawing.Font(font[findex], fontsize, System.Drawing.FontStyle.Bold);//字體樣式(參數2是字體大小)
            Brush b = new System.Drawing.SolidBrush(c[cindex]);

            Point dot = new Point(16, 16);
            float angle = rand.Next(-randAngle, randAngle);//轉動度數

            graph.TranslateTransform(dot.X, dot.Y);//移動到指定位置
            graph.RotateTransform(angle);
            graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);

            graph.RotateTransform(-angle);//轉回去
            graph.TranslateTransform(2, -dot.Y);//移動到指定位置
        }


        //生成圖片
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        map.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        Response.ClearContent();
        Response.ContentType = "image/gif";
        Response.BinaryWrite(ms.ToArray());
        graph.Dispose();
        map.Dispose();
    }


    protected void Page_UnLoad(object sender, EventArgs e)
    {
        base.Dispose();
    }



    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        this.Load += new System.EventHandler(this.Page_Load);
    }
    #endregion
}
