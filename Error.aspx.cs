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

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       //aca borrar la imagen generada
        //string error = Request.QueryString["error"].ToString();
        //Response.Write("NO SE ENCUENTRA LA SOCIEDAD CON DICHO NUMERO CORRELATIVO");

        string msg = (string)Session["MsgErrorValEntidad"];

        if (msg != "")
        {
            lblErrorMsg.Text = msg;
            lblErrorMsg.Visible = true;
        }
    }
}
