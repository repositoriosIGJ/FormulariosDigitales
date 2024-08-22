using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;

public partial class imgCaptcha : System.Web.UI.Page
{ 
    // ----- PROPERTIES ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private string _CaptchaURL = ConfigurationSettings.AppSettings["CAPTCHA"]; //"http://intranetdesa.jus.gov.ar/SeguridadWeb/imgCaptcha.aspx?Captcha=" '"http://localhost:1990/Captcha/imgCaptcha.aspx?Captcha=";
    public string CaptchaURL
    {
        get { return _CaptchaURL; }
        set { _CaptchaURL = value; }
    }

    // ----- METHODS ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateCaptcha();
    }
    
    private void CreateCaptcha()
    {
        //Direccion del CAPTCHA, se le pasa el valor (METODO GET) recibe la imagen
        
        // Prepara la cabecera del archivo
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AppendHeader("content-disposition", "attachment; filename=captcha.jpg");
        Response.ContentType = "image/jpeg";

        // Regenera el Captcha
        Session.Add("captchaValue", GenerateRandomCode());
        Session.Add("captchaUrl", _CaptchaURL + Session["captchaValue"].ToString());

        // Obtiene la imagen
        WebClient client = new WebClient();
        Byte[] buffer = client.DownloadData(Session["captchaUrl"].ToString());
        
        // Pone la imagen en la salida
        Response.BinaryWrite(buffer);

        // Cierra la transmision del archivo
        Response.End();
    }

    private string GenerateRandomCode()
    {
        // Caracteres posibles
        string randomValue = "ABCDEFGHIJKLMNOPQRSTUVWXYZ23456789";
        
        // Codigo resultante
        string s = string.Empty;
        Random rand = new Random();

        for (int i = 0; i < 5; i++) //6 caracteres...
        {
            s = s + randomValue.Substring(rand.Next(0, randomValue.Length - 1), 1);
        }

        return s; // retorna el valor/codigo;
    }
}
