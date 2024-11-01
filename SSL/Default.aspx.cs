using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SSL_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void fn_Check_Expiration(string strURL)
    {
        //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
        X509Certificate2 cert2 = null;
        HttpWebResponse response = null;

        try
        {
            response = (HttpWebResponse)request.GetResponse();
            X509Certificate cert = request.ServicePoint.Certificate;
            cert2 = new X509Certificate2(cert);
        }
        catch
        {
            X509Certificate X509Cert = request.ServicePoint.Certificate;
            cert2 = new X509Certificate2(X509Cert);
        }
        finally
        {
            if (response != null)
            {
                response.Close();
            }
        }

        if (cert2 != null)
        {
            //string cn = cert2.GetIssuerName();
            //string cn2 = cert2.GetName();
            string cedate = cert2.GetExpirationDateString();
            string cpub = cert2.GetPublicKeyString();

            Console.WriteLine(cedate);

            Literal1.Text += cedate + "<br />";
        }
    }

    protected void btnGoogle_Click(object sender, EventArgs e)
    {
        Literal1.Text = "";
        //for (int ii = 1; ii <= 10; ii++)
        fn_Check_Expiration("https://google.com");
    }

    protected void btnArtisan_Click(object sender, EventArgs e)
    {
        Literal1.Text = "";
        //for (int ii = 1; ii <= 100; ii++)
        fn_Check_Expiration("https://www.artisan.com.tw");
    }

    //TcpClient tcpWhois;
    //NetworkStream nsWhois;
    //BufferedStream bfWhois;
    //StreamWriter strmSend;
    //StreamReader strmRecive;
    protected void btnDomainName_Click(object sender, EventArgs e)
    {
        try
        {
            TcpClient objTCPC = new TcpClient(Request.Form["WhoisServer"], 43);
            string strDomain = Request.Form["DomainName"] + "\r\n";
            byte[] arrDomain = Encoding.ASCII.GetBytes(strDomain);

            Stream objStream = objTCPC.GetStream();
            objStream.Write(arrDomain, 0, strDomain.Length);
            StreamReader objSR = new StreamReader(objTCPC.GetStream(), Encoding.ASCII);
            lblResponse.Text = "<b>" + Request.Form["DomainName"] + "</b><br><br>" + Regex.Replace(objSR.ReadToEnd(), "\n", "<br>");

            objTCPC.Close();
        }
        catch (Exception ex)
        {
            lblResponse.Text = ex.ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string whois = Whois("artisan.com");
        //Debug.WriteLine(whois);
        Literal1.Text = whois;
        //Console.ReadKey();
    }

    public static string Whois(string domain)
    {
        if (domain == null)
            throw new ArgumentNullException();
        int ccStart = domain.LastIndexOf(".");
        if (ccStart < 0 || ccStart == domain.Length)
            throw new ArgumentException();
        string ret = "";
        Socket s = null;
        try
        {
            string cc = domain.Substring(ccStart + 1);
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(new IPEndPoint(Dns.GetHostEntry(cc + ".whois-servers.net").AddressList[0], 43));
            s.Send(Encoding.ASCII.GetBytes(domain + "\r\n"));
            byte[] buffer = new byte[1024];
            int recv = s.Receive(buffer);
            while (recv > 0)
            {
                ret += Encoding.ASCII.GetString(buffer, 0, recv);
                recv = s.Receive(buffer);
            }
            s.Shutdown(SocketShutdown.Both);
        }
        catch
        {
            throw new SocketException();
        }
        finally
        {
            if (s != null)
                s.Close();
        }
        return ret;
    }
}

