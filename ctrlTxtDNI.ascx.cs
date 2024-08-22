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


public partial class ctrlTxtDNI : System.Web.UI.UserControl
{
    public string Text
    {
        get { return txtDNI.Text; }
        set { txtDNI.Text = value; }
    }

    public bool Enable
    {
        get { return txtDNI.Enabled; }
        set { txtDNI.Enabled = value; }
    }

    public bool ValidarDNI
    {
        get { return valDNI.Enabled;}
        set { valDNI.Enabled = value; }
    }

    public bool ValidarObligatorio
    {
        get { return RequiredFieldValidator1.Enabled; }
        set { RequiredFieldValidator1.Enabled = value; }
    }
}