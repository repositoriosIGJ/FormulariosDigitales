using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MsgBox
/// </summary>
public class MsgBox
{
    public MsgBox()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    static public void NoEntidad(Control page, string msj)
    {
        //string message;
        //if (HttpContext.Current.Profile.GetPropertyValue("LanguagePreference").ToString() == "en-US")
        //{ message = "The operation was realized correctly"; }
        //else { message = "La operaci" + '\u00f3' + "n se realiz" + '\u00f3' + " correctamente."; }
        string myScript = String.Format("alert('{0}');", msj);
        ScriptManager.RegisterStartupScript(page, page.GetType(),
          "MyScript", myScript, true);
    }
}
