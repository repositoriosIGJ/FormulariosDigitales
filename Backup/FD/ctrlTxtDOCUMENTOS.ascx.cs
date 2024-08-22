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


public partial class ctrlTxtDOCUMENTOS : System.Web.UI.UserControl
{
    public string Text
    {
        get { return txtDOCUMENTOS.Text; }
        set { txtDOCUMENTOS.Text = value; }
    }

    public bool Enable
    {
        get { return txtDOCUMENTOS.Enabled; }
        set { txtDOCUMENTOS.Enabled = value; }
    }

    
    //public bool ValidarDOCUMENTOS
    //{
    //    get { return valDOCUMENTOS.Enabled; }
    //    set { valDOCUMENTOS.Enabled = value; }
    //}
    

    public bool ValidarObligatorio
    {
        get { return RFVDocumentos.Enabled; }
        set { RFVDocumentos.Enabled = value; }
    }
}
