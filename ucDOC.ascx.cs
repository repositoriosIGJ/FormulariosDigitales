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


public partial class ucDOC : System.Web.UI.UserControl
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
        get { return rfvDNI.Enabled; }
        set { rfvDNI.Enabled = value; }
    }

    public string propddlTipoDoc
    {
        get { return Convert.ToString(ddlTipoDoc.SelectedValue); }
        set
        {
            foreach (ListItem l in ddlTipoDoc.Items)
            {
                if (Convert.ToString(l.Value) == value)
                {
                    l.Selected = true;
                    return;
                }
            }
        }
    }
}
