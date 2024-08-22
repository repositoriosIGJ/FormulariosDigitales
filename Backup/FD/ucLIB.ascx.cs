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


public partial class ucLIB : System.Web.UI.UserControl
{
    public int prophfIdTipoLibro
    {
        get { return Convert.ToInt32(hfIdTipoLibro.Value); }
        set { hfIdTipoLibro.Value = value.ToString(); }
    }

    public string proplblLibro
    {
        get { return lblLibro.Text; }
        set { lblLibro.Text = value; }
    }

    public bool proplblLibroEnable
    {
        get { return lblLibro.Enabled; }
        set { lblLibro.Enabled = value; }
    }

    public string proptxtPaginas
    {
        get { return txtPaginas.Text; }
        set { txtPaginas.Text = value; }
    }

    public string proptxtFojas
    {
        get { return txtFojas.Text; }
        set { txtFojas.Text = value; }
    }

    //Habilita y Deshabilita el Validador de Campo Obligatorio para Cantidad de Paginas y Fojas
    public bool propObligatorio
    {
        set
        {
            rfvPAG.Enabled = value;
            rfvFOJ.Enabled = value;
        }
    }

    //public bool ValidarPAG
    //{
    //    get { return valPAG.Enabled; }
    //    set { valPAG.Enabled = value; }
    //}

    //public bool ValidarObligatorio
    //{
    //    get { return rfvPAG.Enabled; }
    //    set { rfvPAG.Enabled = value; }
    //}

    public string proprblCopiador
    {
        get { return rblCopiador.SelectedValue; }
        //set { rblCopiador.SelectedValue = value.ToString(); }
    }

    //public string propddlCopiador
    //{
    //    get { return Convert.ToString(ddlCopiador.SelectedValue); }
    //    set
    //    {
    //        foreach (ListItem l in ddlCopiador.Items)
    //        {
    //            if (Convert.ToString(l.Value) == value)
    //            {
    //                l.Selected = true;
    //                return;
    //            }
    //        }
    //    }
    //}

    protected void rblCopiador_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rblPagFoj_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblPagFoj.SelectedIndex == 0)
        {
            //Habilito y Muestro Paginas
            trPaginas.Visible = true;
            //Borro Campo de Texto y Habilito Validadores
            txtPaginas.Text = "";
            ValidaPAG.Enabled = true;
            valPAG.Enabled = true;
            msgvalpag.Enabled = true;
            rfvPAG.Enabled = true;
            msgrfvpag.Enabled = true;

            //Deshabilito y Oculto Fojas
            trFojas.Visible = false;
            //Borro Campo de Texto y Deshabilito Validadores
            txtFojas.Text = "";
            ValidaPAG.Enabled = false;
            valPAG.Enabled = false;
            msgvalpag.Enabled = false;
            rfvPAG.Enabled = false;
            msgrfvpag.Enabled = false;
        }
        else
        {
            //Habilito y Muestro Fojas
            trFojas.Visible = true;
            //Borro Campo de Texto y Habilito Validadores
            txtFojas.Text = "";
            ValidaPAG.Enabled = true;
            valPAG.Enabled = true;
            msgvalpag.Enabled = true;
            rfvPAG.Enabled = true;
            msgrfvpag.Enabled = true;

            //Deshabilito y Oculto Paginas
            trPaginas.Visible = false;
            //Borro Campo de Texto y Deshabilito Validadores
            txtPaginas.Text = "";
            ValidaPAG.Enabled = false;
            valPAG.Enabled = false;
            msgvalpag.Enabled = false;
            rfvPAG.Enabled = false;
            msgrfvpag.Enabled = false;
        }
    }
}
