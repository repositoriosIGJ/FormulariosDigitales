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
using System.Collections.Generic;
using System.Drawing;

using FD.BusinessLayer;
using FD.Entities;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PadronAfip padron = BLPadronAfip.Cargar(336474759);

        PostBackInitialization();

        //TODO_DESCOMENTAR: Comentar esto en produccion
        //TODO_COMENTAR: Solo para desarrollo, comentar en produccion
        #region "Saltear Pagina Default";

        //Boton Iniciar
        //btnInfoInicialOk_Click(null, null);

        //Datos Mockup
        //TODO_ucDOC: Cambio el Control CtrlTxtDNI1 x ucDOC        
        //ucDOC.Text = "24444444";
        //txtNombre.Text = "NomPrueba";
        //txtApellido.Text = "ApePrueba";
        //txtMail.Text = "emailfd@pruebafd.com";
        //txtTelefono.Text = "53004000";

        //Boton Confirmar
        //btnConfirmar_Click(null, null);

        #endregion "Saltear Pagina Default";

        if (!IsPostBack)
        {
            NonPostBackInitialization();
        }
    }

    //---------------- PRIVATE METHODS ----------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Realiza todas las tareas que se realizan en cada PostBack.
    /// </summary>
    private void PostBackInitialization()
    {
        Page.MaintainScrollPositionOnPostBack = true;
    }

    /// <summary>
    /// Realiza todas las operaciones que se hacen una sola vez y se evitan en cada postback.
    /// </summary>
    private void NonPostBackInitialization()
    {
        Session.Abandon();
        Session.Add("verConfirEntidad", false);

        //this.CtrlTxtDNI1.Attributes.Add("onkeydown", Util.ScriptEnter(imOK));
        this.txtMail.Attributes.Add("onkeydown", Util.ScriptEnter(btnConfirmarFirmante));
    }

    /// <summary>
    /// Valida que JavaScript este habilitado en el explorador del cliente
    /// </summary>
    private void ValidateJavaScript()
    {
        //----------------------------- Validar JavaScript -------------------------------
        HtmlInputHidden hiddenField = (HtmlInputHidden)Master.FindControl("testBox");
        Panel pnlContentPlaceHolder = (Panel)Master.FindControl("pnlContentPlaceHolder");
        Label labelJavaError = (Label)Master.FindControl("lblJavaError");

        if (hiddenField.Value != "yes")
        {
            labelJavaError.Visible = true;
            labelJavaError.Text = "<br/><br/>Esta aplicación requiere que su explorador tenga habilitado JavaScript <br/> si desea continuar por favor habilite esta opción y vuelva a cargar la página.";
            this.pnlInfoInicial.Visible = false;
            pnlContentPlaceHolder.Visible = false;
        }
        else
        {
            labelJavaError.Visible = false;
            this.pnlInfoInicial.Visible = true;
            pnlContentPlaceHolder.Visible = true;
        }

        hiddenField = null;
        pnlContentPlaceHolder = null;
        labelJavaError = null;
    }

    private void ocultarValidatores()
    {
        //this.rfvCaracter.Visible 
        this.rfvAutorizado.Visible = false;
        this.rfvMail.Visible = false;
        this.revMail.Visible = false;
        this.rfvApellido.Visible = false;
        this.rfvNombre.Visible = false;
    }

    //---------------- BUTTONS ---------------------------------------------------------------------------------------------------------------------

    protected void imCancel_Click(object sender, ImageClickEventArgs e)
    {
        this.pnlDatosFirmante.Visible = false;
        //TODO_ucDOC: Cambio el Control CtrlTxtDNI1 x ucDOC
        //this.CtrlTxtDNI1.Text = string.Empty;
        this.ucDOC.Text = string.Empty;
        this.txtNombre.Text = string.Empty;
        this.txtApellido.Text = string.Empty;
        this.txtMail.Text = string.Empty;
        this.imOK.Visible = true;
        this.imCancel.Visible = false;
        this.lblDescTilde.Visible = true;
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //TODO_ucDOC: Cambio el Control CtrlTxtDNI1 x ucDOC
            /*Firmante oFirmante = new Firmante(Convert.ToInt64(this.CtrlTxtDNI1.Text),
                                              this.txtNombre.Text.Trim(),
                                              this.txtApellido.Text.Trim(),
                                              this.txtMail.Text.Trim(),
                                              this.txtTelefono.Text.Trim());*/
            //TODO_Firmante_v18: Nuevo objeto con 3 campos mas
            Firmante oFirmante = new Firmante(Convert.ToInt64(this.ucDOC.Text),
                                              this.txtNombre.Text.Trim(),
                                              this.txtApellido.Text.Trim(),
                                              this.txtMail.Text.Trim(),
                                              this.txtTelefono.Text.Trim(),
                                              this.ucDOC.propddlTipoDoc,
                                              this.rblCaracter.SelectedValue,
                                              this.txtAutorizado.Text.Trim());
            Session.Add("DatosFirmante", oFirmante);

            Response.Redirect("Tramite.aspx");
        }
    }

    protected void btnInfoInicialOk_Click(object sender, EventArgs e)
    {
        //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
        {
            ValidateJavaScript();
        }

        this.pnlInfoInicial.Visible = false;
        this.PnlInfoFirmante.Visible = true;
    }

    //---------------- VALIDATORS ---------------------------------------------------------------------------------------------------------------------

    protected void EmptyAndStringFieldValidator(object source, ServerValidateEventArgs args)
    {
        //Valido que los campos no sean null
        Validation.NullValueValidation(args);
        //Valido que el campo sea todo string
        Validation.StringValueValidation(args);
        ocultarValidatores();
    }

    protected void EMailValidator(object source, ServerValidateEventArgs args)
    {
        string msj;
        Validation.NullValueValidation(args, out msj);
        if (args.IsValid)
        {
            Validation.EMailValueValidator(args, out msj);
        }
        this.CustomValidator3.ErrorMessage = msj;
        ocultarValidatores();
    }

    protected void cvNombre_ServerValidate(object source, ServerValidateEventArgs args)
    {
        foreach (char ch in args.Value)
        {
            if (char.IsSymbol(ch))
            {
                args.IsValid = false;
                return;
            }
        }
    }
    protected void rblCaracter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Cuando elije "Autorizado a tramitar" muestro el textbox
        if (this.rblCaracter.SelectedIndex == 2)
        {
            this.trAutorizado.Visible = true;
        }
        else
        {
            this.trAutorizado.Visible = false;
        }

    }
}