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
using System.IO;
using System.Collections.Generic;

using FD.Entities;
using FD.BusinessLayer;

public partial class Entidad : System.Web.UI.Page
{
    public object[] vInfoTram
    {
        get { return ((object[])Session["vInfoTram"]); }
        set { Session.Add("vInfoTram", value); }
    }

    private static bool _FlagCUIT;

    public static bool FlagCUIT
    {
        get { return _FlagCUIT; }
        set { _FlagCUIT = value; }
    }

    public static int FlagAnexo = 0;

    //TODO_STATIC: Revisar si hay problemas con estas variables static
    private static bool _errorWS;

    public static bool errorWS
    {
        get { return _errorWS; }
        set { _errorWS = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        //Evita el error <pages enableEventValidation="true"/>


        FD.Entities.Tramite oTramite = (FD.Entities.Tramite)Session["DatosTramite"];

        //Pongo el Check de cambio de Cuit en falso para que oculte el panel de cambio de cuit
        //if (this.chkCambioCuit.Visible == true && TIngresoCuit.Visible == false)
        //{            
        //    this.chkCambioCuit.Checked = false;
        //    chkCambioCuit_CheckedChanged(null, null);
        //}

        if (!Page.IsPostBack)
        {
            //Variable que Habilita (Internet) o Deshabilita (Intranet) el captcha            
            bool CaptchaHabilitado = true;

            CaptchaHabilitado = Convert.ToBoolean(ConfigurationManager.AppSettings["CaptchaVisible"]);

            Session.Add("CaptchaHabilitado", CaptchaHabilitado);

            if (CaptchaHabilitado == false)
            {
                lbltitCaptcha.Visible = false;
                divimgCaptcha.Visible = false;
                divimgCaptcha.Disabled = true;
            }

            this.txtCaptcha.Attributes.Add("onkeydown", Util.ScriptEnter(this.btnCaptcha));
            //Mapeo los datos del tramite para utiliarlo en decisiones necesarias para la entidad
            //Posicion 0 = Id Tramite - (this.lblIdTramite.Text.Trim())
            //Posicion 1 = Flago Numero COrrelativo - ((string)Session["FlagNumCorr"])
            //Posicion 2 = Id Tipo de entidad - (this.ddlTipoEntidad.SelectedValue)
            //posicion 3 = Descripcion Tramite - (this.ddlTipoEntidad.SelectedText)
            //posicion 4 = campo bool, que dice si cambia el label del datos adjuntos por observaciones
            //posicion 5 = FlagAnexo: 1 = Anexo I - 2 = Anexo II - 3 = Anexo III

            vInfoTram = (object[])Session["vInfoTram"];
            HiddenField1.Value = vInfoTram.GetValue(2).ToString();

            if (vInfoTram.GetValue(4) != null)
            {
                if ((bool)vInfoTram.GetValue(4))
                {
                    this.lblMotivo.Text = "Observaciones: ";
                    //this.lblMotivo.Text = "Documentos Adjuntos:";
                }
            }

            // Cambio del label Observaciones - PRESENTACION DE OFICIOS REITERATORIOS
            if ((oTramite.Id == 543) || (oTramite.Id == 579))
            {
                this.lblMotivo.Text = "Nro. de Tr&aacute;mite del oficio original y fecha de presentaci&oacute;n";
            }

            //agrego las propiedades de JS para la cantidad maxima de caracteres del Textbox Multiline
            this.txtMotivo.Attributes.Add("onkeypress", " ValidarCaracteres(this, 300);");
            this.txtMotivo.Attributes.Add("onkeyup", " ValidarCaracteres(this, 300);");

            mapeoBarraInfo();

            //TODO_GATEWAY: AGREGO IDS DE TRAMITES HARCODEADOS ADEMAS DEL 6
            //evaluo Id Tramite            
            //if (vInfoTram[0].ToString().Trim().Equals("6") || vInfoTram[0].ToString().Trim().Equals("372") || vInfoTram[0].ToString().Trim().Equals("373"))


            //TODO_RESERVA: Retorno si para este id de tramite es del tipo de reserva
            //1 = Reserva -- 0 = No Reserva
            BLTramite oBLTramite = new BLTramite();
            bool FlagReserva = oBLTramite.RetornarTramiteReserva(Convert.ToInt32(vInfoTram.GetValue(0))); //vInfoTram.GetValue(0) ==> Id del Tramite

            //this.btnConfirmarEntidad.Visible = true;

            if (FlagReserva == true)
            {
                //muestro el panel con las opciones de nombres
                this.pnlEspecial.Visible = true;
                //Hago que se muestre siempre las opciones de persona fisica
                rblSeleccionOpcion_SelectedIndexChanged(null, null);
                //oculto el boton confirmar que no se utiliza en Reserva de Denominacion (btnConfirmarEntidad)
                this.btnConfirmarEntidad.Visible = false;
                this.PanelCuerpoEntidad.Visible = false;
            }

            ocultarControles(FlagReserva);
            modificarLabelsTramites();

            /****************************************/
            /*-- EMPIEZA NUEVA MODIF BANELCO 2014 --*/
            /****************************************/

            getBancos();

            decimal MontoFormulario = Convert.ToDecimal(HttpContext.Current.Session["PrecioForm"]);

            //Deshabilito el Pago por Banelco si el precio es $0
            if (MontoFormulario == 0)
                chkFormaPago.Enabled = false;

            //Deshabilito el formulario de pago por banelco
            txtDNIpago1.Text = "";
            txtDNIpago1.Enable = false;
            txtDNIpago1.ValidarDNI = false;
            txtDNIpago1.ValidarObligatorio = false;
            txtDNIpago2.Text = "";
            txtDNIpago2.Enable = false;
            txtDNIpago2.ValidarDNI = false;
            txtDNIpago2.ValidarObligatorio = false;
            ddlBANCOpago.SelectedValue = "Seleccione un Banco";
            ddlBANCOpago.Enabled = false;
            divErrorBanelco.Visible = false;
            lblErrorBanelco.Text = "";

            /****************************************/
            /*-- TERMINA NUEVA MODIF BANELCO 2014 --*/
            /****************************************/
        }
    }

    private void modificarLabelsTramites()
    {
        Label label = new Label();
        label = (Label)Master.FindControl("lblDescTramite");

        if (label.Text == "RESERVA DE DENOMINACION")
            this.lblEntidad.Text = "<b>&nbsp;&nbsp;&nbsp;En el siguiente campo debe ingresar la denominación de la entidad</b>";
        else
            this.lblEntidad.Text = "<b>&nbsp;&nbsp;&nbsp;Para identificar a la entidad, seleccione el método de búsqueda.</b>";

        if (label.Text == "DESARCHIVO (TRAMITES FINALIZADOS)")
            this.lblMotivo.Text = "Nro. de expediente/trámite o descripción del mismo:";
        if (label.Text == "DESARCHIVO DE EXPEDIENTE DE ENTIDAD CANCELADA")
            this.lblMotivo.Text = "Nro. de expediente/trámite o descripción del mismo:";
        if (label.Text == "DESARCHIVO PARA CONSULTA Y/O CONTESTACION DE VISTA (TRAMITES NO FINALIZADOS)")
            this.lblMotivo.Text = "Nro. de expediente/trámite o descripción del mismo:";
        if (label.Text == "CERTIFICACION DE FIRMAS")
            this.lblMotivo.Text = "Ingrese nombre completo y nro. de documento de las personas que ratifican firma:";

        //------ FOTOCOPIAS --------------

        if (label.Text == "FOTOCOPIAS CERTIFICADAS DE ESTATUTO CONSTITUTIVO RELATIVO A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVAS MENOR A 1.000.000 Y A ENTIDADES CIVILES (HASTA 100 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS CERTIFICADAS DE ESTATUTO CONSTITUTIVO RELATIVO A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000 (HASTA 100 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS CERTIFICADAS DE ESTATUTO Y MODIFICACIONES RELATIVAS A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000 Y A ENTIDADES CIVILES (HASTA 100 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS CERTIFICADAS DE ESTATUTO Y MODIFICACIONES RELATIVO A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000 (HASTA 100 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS CERTIFICADAS DE TRAMITES DETERMINADOS RELATIVAS A MATRICULAS Y SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000 (HASTA 100 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS CERTIFICADAS DE TRAMITES DETERMINADOS RELATIVAS A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000 Y A ENTIDADES CIVILES (HASTA 100 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS DE BALANCES PRESENTADOS")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS SIMPLES DE ESTATUTO CONSTITUTIVO RELATIVAS A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000 Y A ENTIDADES CIVILES (HASTA 200 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS SIMPLES DE ESTATUTO CONSTITUTIVO RELATIVAS SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000 (HASTA 200 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS SIMPLES DE ESTATUTO Y MODIFICACIONES RELATIVAS A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000 (HASTA 200 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS SIMPLES DE ESTATUTO Y MODIFICACIONES RELATIVAS A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000 Y A ENTIDADES CIVILES (HASTA 200 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS SIMPLES DE TRAMITES DETERMINADOS RELATIVAS A MATRICULAS Y SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000 (HASTA 200 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "FOTOCOPIAS SIMPLES DE ESTATUTO CONSTITUTIVO  RELATIVAS A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000 Y A ENTIDADES CIVILES (HASTA 200 COPIAS)")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";

        //------- CERTIFICADOS -----------

        if (label.Text == "CERTIFICADO DE CANCELACION REGISTRAL RELATIVO A MATRICULAS Y SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE CANCELACION REGISTRAL RELATIVO A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE DOMICILIO RELATIVO A MATRICULAS Y SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE DOMICILIO RELATIVO A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000 Y A ENTIDADES CIVILES")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE MEDIDAS CAUTELARES RELATIVO A MATRICULAS Y SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE MEDIDAS CAUTELARES RELATIVO A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000 Y A ENTIDADES CIVILES")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE VIGENCIA POR CAMBIO DE JURISDICCION")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE VIGENCIA RELATIVO A MATRICULAS Y SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO A PARTIR DE 1.000.000")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE VIGENCIA RELATIVO A SOCIEDADES COMERCIALES CON NUMERO CORRELATIVO MENOR A 1.000.000 Y A ENTIDADES CIVILES")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
        if (label.Text == "CERTIFICADO DE VIGENCIA POR CAMBIO DE JURISDICCION")
            this.lblMotivo.Text = "Observaciones / Autorización para retiro:";
    }

    private void mapeoBarraInfo()
    {
        //MUESTRO LA BARRA DE FIRMANTE TAMBIEN
        Panel pnlInfoFirmante = (Panel)Page.Master.FindControl("pnlInfoFirmante");
        pnlInfoFirmante.Visible = true;

        #region DATOS DEL FIRMANTE
        Firmante oFirmante = SessionData.getInfoFirmante();

        Label lblDniFirmante = (Label)pnlInfoFirmante.FindControl("lblDniFirmante");
        lblDniFirmante.Text += Server.HtmlDecode(oFirmante.DNI.ToString());
        Label lblNomFirmante = (Label)pnlInfoFirmante.FindControl("lblNomFirmante");
        lblNomFirmante.Text += Server.HtmlDecode(oFirmante.Nombre.ToString());
        Label lblApeFirmante = (Label)pnlInfoFirmante.FindControl("lblApeFirmante");
        lblApeFirmante.Text += Server.HtmlDecode(oFirmante.Apellido.ToString());
        Label lblMailFirmante = (Label)pnlInfoFirmante.FindControl("lblMailFirmante");
        lblMailFirmante.Text += Server.HtmlDecode(oFirmante.Mail.ToString());
        #endregion

        #region DATOS DEL TRAMITE
        FD.Entities.Tramite oTramite = SessionData.getInfoTramite();

        Panel pnlInfoTramite = (Panel)Page.Master.FindControl("pnlInfoTramite");
        pnlInfoTramite.Visible = true;

        Label lblDescTramite = (Label)pnlInfoTramite.FindControl("lblDescTramite");
        lblDescTramite.Text += Server.HtmlDecode(oTramite.Descripcion.ToString());
        Label lblDescUrgente = (Label)pnlInfoTramite.FindControl("lblDescUrgente");

        if (oTramite.FlagUrgente)
        {
            lblDescUrgente.Text += "SI";
        }
        else
        {
            lblDescUrgente.Text += "NO";
        }
        #endregion

        //Los DATOS DE ENTIDAD se muestran en el metodo "btnConfirmar_Click"
    }

    private void ocultarControles(bool FlagReserva)
    {
        //si el tramite tiene chkFlagNumCorr = False, entonces no tengo uqe habilitar
        //la busqueda, solo dejar que complete el campo de Nombre de entidad.
        //Flag Numero Correlativo

        if ((string)vInfoTram.GetValue(1) == "1")
        {
            //oculto el panel y muestyro las opciones de busqueda
            this.pnlNumCorrNomSoc.Visible = false;

            this.gvSociedades.Visible = false;
            this.gvSociedades.DataSource = null;
            this.txtNomSociedad.Style.Add("background-color", "#FFFFFF");
            this.txtNroCorrelativo.Style.Add("background-color", "#FFFFFF");
            this.rblBuscarEntidad.Visible = true;
            this.imBuscarSocXNum.Visible = true;
            this.txtNroCorrelativo.Visible = true;
            this.lblNroCorrelativo.Visible = true;
            this.txtNroCorrelativo.Text = "";
            this.txtNomSociedad.Text = "";
        }
        else
        {
            this.rblBuscarEntidad.Visible = false;
            this.txtNroCorrelativo.Visible = false;
            this.lblNroCorrelativo.Visible = false;
            this.imBuscarSocXNum.Visible = false;
            this.txtNomSociedad.Style.Add("background-color", "#FAFAD2");

            //if ((string)vInfoTram.GetValue(0) != "6") this.btnConfirmarEntidad.Visible = true;

            //TODO_GATEWAY: AGREGO IDS DE TRAMITES HARCODEADOS ADEMAS DEL 6
            //if ((string)vInfoTram.GetValue(0) != "6" && (string)vInfoTram.GetValue(0) != "372" && (string)vInfoTram.GetValue(0) != "373") this.btnConfirmarEntidad.Visible = true;
            if (FlagReserva == true)
            {
                //Muestro el panel NumeroCorrNomSoc
                this.pnlNumCorrNomSoc.Visible = true;
            }
            else
            {
                this.btnConfirmarEntidad.Visible = true;
            }
        }
    }

    protected void rblBuscarEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TODO_NOCUIT: No se usa el pedido de CUIT
        /*
        //Oculto el div de Validez del CUIT
        this.divValidezCuit.Visible = false;
        this.lblValidezCuit.Text = "";
        //Siempre Oculto Panel de CUIT
        this.updpnlCUIT.Visible = false;
        //Reinicio los datos del ucCUIT
        this.ucCUIT1.CuitText = "";
        this.ucCUIT1.NomEntAfipText = "";
        this.ucCUIT1.NoEncontrado = false;
        */
        this.btnConfirmarEntidad.Visible = false;

        if (this.rblBuscarEntidad.SelectedIndex == 0)
        {   //Buscar por numero de entidad
            //muestro el panel con los campos de busqueda
            this.pnlNumCorrNomSoc.Visible = true;

            this.gvSociedades.Visible = false;
            this.gvSociedades.DataSource = null;
            //limpio las cajas de texto
            this.txtNroCorrelativo.Text = "";
            this.txtNomSociedad.Text = "";
            ////Limpio los labels
            this.lblNomEntidadSelecc.Text = "";
            this.lblNumCorrEnt.Text = "";
            this.lblTipoEntidad.Text = "";
            //TODO_NOCUIT: No se usa el pedido de CUIT
            //this.lblCuitRef.Text = "";

            this.txtNroCorrelativo.Visible = true;
            this.lblNroCorrelativo.Visible = true;
            this.txtNomSociedad.Visible = false;
            this.lblNomSociedad.Visible = false;
            this.txtNroCorrelativo.Style.Add("background-color", "#FAFAD2");
            this.txtNomSociedad.Style.Add("background-color", "#FFFFFF");
            this.imBuscarSocXNum.Enabled = true;
            this.txtNroCorrelativo.Focus();

            this.txtNroCorrelativo.Enabled = true;
            //Oculto los datos referenciales de la entidad de IGJ
            this.TDatosRefEntidad.Visible = false;
            //Limpio los labels de los datos referenciales de la entidad
            this.lblComentarioEnt.Visible = false;
            this.lblNumCorrEnt.Text = "";
            this.lblNomEntidadSelecc.Text = "";
            this.lblTipoEntidad.Text = "";

            //TODO_NOCUIT: No se usa el pedido de CUIT
            /*
            this.lblCuitRef.Text = "";
            //Oculto los datos referenciales de la entidad de AFIP
            this.TDatosRefAfip.Visible = false;
            //Limpio los labels de los datos referenciales de la entidad de AFIP
            this.lblNomEntidadAfip.Text = "";
            //this.lblCuitRefAfip.Text = "";
            */

            this.lblLeyendaBusquedaNumCorrelativo.Visible = true;
            this.lblLeyendaBusquedaNonSociedad.Visible = false;
        }
        else
        {//nombre de entidad
            //muestro el panel con los campos de busqueda
            this.pnlNumCorrNomSoc.Visible = true;

            this.gvSociedades.Visible = false;
            this.gvSociedades.DataSource = null;
            //limpio las cajas de texto
            this.txtNroCorrelativo.Text = "";
            this.txtNomSociedad.Text = "";

            //Oculto los datos referenciales de la entidad
            this.TDatosRefEntidad.Visible = false;
            ////Limpio los labels de los datos referenciales de la entidad
            this.lblNomEntidadSelecc.Text = "";
            this.lblNumCorrEnt.Text = "";
            this.lblTipoEntidad.Text = "";

            //TODO_NOCUIT: No se usa el pedido de CUIT
            /*
            this.lblCuitRef.Text = "";
            //Oculto los datos referenciales de la entidad de AFIP
            this.TDatosRefAfip.Visible = false;
            //Limpio los labels de los datos referenciales de la entidad de AFIP
            this.lblNomEntidadAfip.Text = "";
            //this.lblCuitRefAfip.Text = "";
            */

            this.txtNomSociedad.Visible = true;
            this.lblNomSociedad.Visible = true;
            this.txtNroCorrelativo.Visible = false;
            this.lblNroCorrelativo.Visible = false;
            this.txtNomSociedad.Style.Add("background-color", "#FAFAD2");
            this.txtNroCorrelativo.Style.Add("background-color", "#FFFFFF");
            this.imBuscarSocXNum.Enabled = true;
            this.txtNomSociedad.Focus();

            this.txtNomSociedad.Enabled = true;

            this.lblComentarioEnt.Visible = false;

            this.lblLeyendaBusquedaNonSociedad.Visible = true;
            this.lblLeyendaBusquedaNumCorrelativo.Visible = false;
        }
        this.imBuscarSocXNum.ImageUrl = "~/imagenes/Buscar.png";
        this.imBuscarSocXNum.ToolTip = "Buscar entidad";
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
            pnlContentPlaceHolder.Visible = false;
        }
        else
        {
            labelJavaError.Visible = false;
            pnlContentPlaceHolder.Visible = true;
        }

        hiddenField = null;
        pnlContentPlaceHolder = null;
        labelJavaError = null;
    }

    protected void imBuscarSocXNum_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Se Borra el Objeto oEntidadSel (Entidad Seleccionada) Previo a buscar las entidades
            //oEntidadSel = null;

            //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
            {
                ValidateJavaScript();
            }

            if (imBuscarSocXNum.ImageUrl == "~/imagenes/Buscar.png")
            {
                //this.lblNomEntidadSelecc.Text = string.Empty;

                if (this.txtNroCorrelativo.Visible && this.rblBuscarEntidad.SelectedIndex == 0 && this.txtNroCorrelativo.Text != "")
                {
                    this.gvSociedades.DataSource = ODSEntidad;
                    this.gvSociedades.DataBind();

                    this.RequiredFieldValidator1.Visible = false;

                    if (this.gvSociedades.Rows.Count != 0)
                    {
                        this.gvSociedades.Visible = true;
                        this.lblComentarioEnt.Visible = true;
                    }
                    else
                    {
                        MsgBox.NoEntidad(this, "No se encontró entidad del tipo: " + (string)vInfoTram.GetValue(3).ToString() + " con dicho número.");
                    }
                }
                else if (this.txtNomSociedad.Visible && this.txtNomSociedad.Text != "" && this.rblBuscarEntidad.SelectedIndex == 1)
                {
                    this.RequiredFieldValidator1.Visible = true;

                    this.gvSociedades.DataSource = ODSRazonEntidad;
                    this.gvSociedades.DataBind();

                    if (this.gvSociedades.Rows.Count != 0)
                    {
                        this.gvSociedades.Visible = true;
                        this.lblComentarioEnt.Visible = true;
                    }
                    else
                    {
                        MsgBox.NoEntidad(this, "No se encontró entidad con dicho nombre.");
                    }
                }
            }
            else
            {
                //ESTE ES EL CAMINO DE LA FLECHA PARA VOLVER A BUSCAR
                this.txtNroCorrelativo.Enabled = true;
                this.txtNroCorrelativo.Text = "";

                this.txtNomSociedad.Enabled = true;
                this.txtNomSociedad.Text = "";

                //TODO_NOCUIT: No se usa el pedido de CUIT
                /*
                //Oculto el div de Validez del CUIT
                this.divValidezCuit.Visible = false;
                this.lblValidezCuit.Text = "";

                //Siempre Oculto Panel de CUIT
                this.updpnlCUIT.Visible = false;
                //Reinicio los datos del ucCUIT
                this.ucCUIT1.CuitText = "";
                this.ucCUIT1.NomEntAfipText = "";
                this.ucCUIT1.NoEncontrado = false;
                */

                this.btnConfirmarEntidad.Visible = false;

                //Oculto los datos referenciales de la entidad
                this.TDatosRefEntidad.Visible = false;
                //Limpio los labels de los datos referenciales de la entidad
                this.lblNomEntidadSelecc.Text = "";
                this.lblNumCorrEnt.Text = "";
                this.lblTipoEntidad.Text = "";

                //TODO_NOCUIT: No se usa el pedido de CUIT
                /*
                this.lblCuitRef.Text = "";
                //Oculto los datos referenciales de la entidad de AFIP
                this.TDatosRefAfip.Visible = false;
                //Limpio los labels de los datos referenciales de la entidad de AFIP
                this.lblNomEntidadAfip.Text = "";
                this.lblCuitRefAfip.Text = "";
                */

                this.imBuscarSocXNum.ImageUrl = "~/imagenes/Buscar.png";
                this.imBuscarSocXNum.ToolTip = "Buscar entidad";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvSociedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gvSociedades.PageIndex = e.NewPageIndex;
            this.gvSociedades.DataSource = ODSRazonEntidad;
            this.gvSociedades.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Selecciona una Entidad en la grilla "gvSociedades" luego de buscarla por Numero Correlativo o Nombre de Entidad
    #region "Selecciona Entidad en la Grilla"

    protected void gvSociedades_SelectedIndexChanged(object sender, EventArgs e)
    {
        FD.BusinessLayer.BLEntidad oBLEntidad = new FD.BusinessLayer.BLEntidad();
        FD.Entities.Entidad oEntidadSel = new FD.Entities.Entidad();

        try
        {
            this.imBuscarSocXNum.ImageUrl = "~/imagenes/clear_left.png";
            this.imBuscarSocXNum.ToolTip = "Buscar nueva entidad";


            //TODO: Probando traer el tipo societario desde vInfoTram
            object[] vInfoTram = (object[])Session["vInfoTram"];

            string g_tiposoc = vInfoTram[3].ToString().Trim();

            //Llena el Objeto de Entidad Seleccionada para no tener que usar los datos desde la Grilla o desde Controles
            int g_nrocorr = Int32.Parse(Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[0].Text));
            int g_idtiposoc = Int32.Parse(((Label)this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].FindControl("IdTipoSociedad")).Text);

            TipoEntidad tipoent = new TipoEntidad(g_idtiposoc, g_tiposoc);

            oEntidadSel = oBLEntidad.GetSociedadSel(g_nrocorr, tipoent);

            //si txtNomSociedad esta visible le pongo el numero correlativo ademas del nombre (en caso de buscar por Nombre de entidad)
            //if (txtNomSociedad.Visible)

            if (oEntidadSel != null)
            {
                //Guarda la entidad Seleccionada para luego compararla con el tramite
                Session.Add("EntidadSel", oEntidadSel);

                //Pregunta si esta seleccionado en el radio list button "Buscar por nombre de entidad"
                if (rblBuscarEntidad.SelectedValue == "2")
                {
                    //DataKeyNames="NroCorrelativo ,Nombre, TipoSociedad"
                    //this.lblNumCorrEnt.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[0].Text);// +" - ";
                    //this.txtNomSociedad.Enabled = false;
                    //this.lblNomEntidadSelecc.Text += Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[1].Text);
                    //this.lblTipoEntidad.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[2].Text);

                    //Muestra los campos de datos refereciales de la entidad
                    this.TDatosRefEntidad.Visible = true;
                    this.lblNumCorrEnt.Text = oEntidadSel.NroCorrelativo.ToString();
                    this.txtNomSociedad.Enabled = false;
                    this.lblNomEntidadSelecc.Text = oEntidadSel.Nombre;
                    this.lblTipoEntidad.Text = oEntidadSel.TipoSociedad.Descripcion;

                    //Verifica el correlativo de la sociedad
                    VerificarNroCorrelativoSociedad(oEntidadSel.NroCorrelativo);
                }
                else
                {//Entra si esta seleccionado en el radio list button "Buscar por número correlativo"

                    //    this.txtNroCorrelativo.Text = "";
                    //    this.txtNroCorrelativo.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[0].Text);
                    //    this.txtNroCorrelativo.Enabled = false;

                    //    //Muestro los campos de datos refereciales de la entidad
                    //    this.TDatosRefEntidad.Visible = true;
                    //    this.lblNumCorrEnt.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[0].Text);
                    //    this.lblNomEntidadSelecc.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[1].Text);
                    //    this.lblTipoEntidad.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[2].Text);

                    //Muestra los campos de datos refereciales de la entidad
                    this.txtNroCorrelativo.Text = "";
                    this.txtNroCorrelativo.Text = oEntidadSel.NroCorrelativo.ToString();
                    this.txtNroCorrelativo.Enabled = false;

                    this.TDatosRefEntidad.Visible = true;
                    this.lblNumCorrEnt.Text = oEntidadSel.NroCorrelativo.ToString();
                    this.lblNomEntidadSelecc.Text = oEntidadSel.Nombre;
                    this.lblTipoEntidad.Text = oEntidadSel.TipoSociedad.Descripcion;
                }


                //TODO_NOCUIT: No se usa el pedido de CUIT
                //TODO_CUIT: Retorno si para este id de tramite pide cuit
                //1 = Pido CUIT -- 0 = No Pido CUIT
                FlagCUIT = false;
                Session.Add("FlagCUIT", FlagCUIT);

                //Muestro el boton de Confirmar
                this.btnConfirmarEntidad.Visible = true;

                //Oculto y Borro la grilla de seleccion de entidades
                this.gvSociedades.Visible = false;
                this.gvSociedades.DataSource = null;
                //Oculto el ingreso de Comentario
                this.lblComentarioEnt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            oBLEntidad = null;
            oEntidadSel = null;
        }
    }

    #endregion "Selecciona Entidad en la Grilla"

    //TODO_NOCUIT: No se usa el pedido de CUIT
    #region "NOCUIT";
    //////////////////////////////////////
    // EMPIEZA EL INGRESO O NO DEL CUIT //
    //////////////////////////////////////

    //TODO_NOCUIT: No se usa el pedido de CUIT
    /*
    public void ucCUIT_StatusUpdated(object sender, EventArgs e)
    {
        //Habilito el boton Confirmar para continuar
        this.btnConfirmarEntidad.Visible = true;
        //this.btnConfirmarEntidad.CssClass = "bluebutton";
    }
    */

    //TODO_NOCUIT: No se usa el pedido de CUIT    
    //protected void gvSociedades_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    this.imBuscarSocXNum.ImageUrl = "~/imagenes/clear_left.png";
    //    this.imBuscarSocXNum.ToolTip = "Buscar nueva entidad";
    //    //si txtNomSociedad esta visible le pongo el numero correlativo ademas del nombre (en caso de buscar por Nombre de entidad)
    //    if (txtNomSociedad.Visible)
    //    {
    //        //Muestro los campos de datos refereciales de la entidad
    //        this.TDatosRefEntidad.Visible = true;
    //        //DataKeyNames="NroCorrelativo ,Nombre, TipoSociedad, Cuit"
    //        this.lblNumCorrEnt.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[0].Text);// +" - ";
    //        this.txtNomSociedad.Enabled = false;
    //        this.lblNomEntidadSelecc.Text += Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[1].Text);
    //        this.lblTipoEntidad.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[2].Text);
    //        //TODO_NOCUIT: No se usa el pedido de CUIT
    //        /*
    //        //TODO_CUIT: Traigo el cuit de la base para ver si existe 06/10/16
    //        this.lblCuitRef.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[3].Text);
    //        if (this.lblCuitRef.Text.Trim() == "0")
    //            this.lblCuitRef.Text = "No Registrado";
    //        */

    //        //Verifico el correlativo de la sociedad
    //        VerificarNroCorrelativoSociedad(Convert.ToInt64(Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[0].Text)));
    //    }
    //    else
    //    {
    //        this.txtNroCorrelativo.Text = "";
    //        this.txtNroCorrelativo.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[0].Text);
    //        this.txtNroCorrelativo.Enabled = false;

    //        //Muestro los campos de datos refereciales de la entidad
    //        this.TDatosRefEntidad.Visible = true;
    //        this.lblNumCorrEnt.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[0].Text);
    //        this.lblNomEntidadSelecc.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[1].Text);
    //        this.lblTipoEntidad.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[2].Text);
    //        //TODO_NOCUIT: No se usa el pedido de CUIT
    //        //TODO_CUIT: Traigo el cuit de la base para ver si existe 06/10/16
    //        /*
    //        this.lblCuitRef.Text = Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[3].Text);
    //        if (this.lblCuitRef.Text.Trim() == "0")
    //            this.lblCuitRef.Text = "No Registrado";
    //        */
    //    }


    //    //TODO_CUIT: Retorno si para este id de tramite pide cuit
    //    //FD.Entities.Tramite oTramite = (FD.Entities.Tramite)Session["DatosTramite"];
    //    //BLTramite oBLTramite = new BLTramite();
    //    //1 = Pido CUIT -- 0 = No Pido CUIT
    //    //TODO_NOCUIT: No se usa el pedido de CUIT
    //    //FlagCUIT = oBLTramite.RetornarTramiteCUIT(oTramite.Id);
    //    FlagCUIT = false;
    //    Session.Add("FlagCUIT", FlagCUIT);

    //    //TODO_NOCUIT: No se usa el pedido de CUIT
    //    #region "Pedido de Cuit"

    //    //Pregunto si es un tipo de tramite al cual se le va a pedir el CUIT (Registrales, etc)
    //    //if (FlagCUIT == true)
    //    //{
    //    //    //Muestro el UpdatePanel del CUIT
    //    //    updpnlCUIT.Visible = true;
    //    //    //Muestro el boton de Confirmar
    //    //    this.btnConfirmarEntidad.Visible = true;

    //    //    bool errorWSaux = false;
    //    //    long CUITaux = (this.lblCuitRef.Text.Trim() == "No Registrado") ? 0 : Convert.ToInt64(this.lblCuitRef.Text.Trim());


    //    //    //Traigo los datos de la entidad provenientes de AFIP mediante el CUIT y Cargo el objeto PadronAfip
    //    //    PadronAfip padron = BLPadronAfip.Cargar(CUITaux, out errorWSaux);

    //    //    errorWS = errorWSaux;

    //    //    //Pregunto si no fallo el ws de afip
    //    //    if (!errorWS)
    //    //    {
    //    //        //Pregunto si trajo el cuit de nuestra BD
    //    //        if (this.lblCuitRef.Text.Trim() == "No Registrado")
    //    //        {//CASO 1: El CUIT no esta en nuestra BD
    //    //            //Obligo a Ingresar uno Nuevo
    //    //            //Entonces muestro el DIV que incluye el control de Ingreso de CUIT sin el Check de Cambio de CUIT

    //    //            //Muestro Mensaje de Error de validez del CUIT
    //    //            this.divValidezCuit.Visible = true;
    //    //            this.lblValidezCuit.Text = "EL CUIT DE LA ENTIDAD NO SE ENCUENTRA REGISTRADO EN NUESTRA BASE DE DATOS";
    //    //            this.lblValidezCuit.CssClass = "validezcuiterror";

    //    //            this.TIngresoCuit.Visible = true;
    //    //            this.ucCUIT1.TituloCuit = "INGRESE EL NRO DE CUIT DE LA ENTIDAD:";

    //    //            //Oculto el check de cambio de cuit
    //    //            this.chkCambioCuit.Checked = false;
    //    //            this.divCambioCuit.Visible = false;
    //    //            this.chkCambioCuit.Visible = false;
    //    //            this.chkCambioCuit.Enabled = false;

    //    //        }
    //    //        else
    //    //        {//CASO 2: El CUIT se encuentra en nuestra BD
    //    //            //bool errorWSaux = false;

    //    //            ////Traigo los datos de la entidad provenientes de AFIP mediante el CUIT y Cargo el objeto PadronAfip
    //    //            //PadronAfip padron = BLPadronAfip.Cargar(Convert.ToInt64(this.lblCuitRef.Text.Trim()), out errorWSaux);

    //    //            //errorWS = errorWSaux;

    //    //            ////Pregunto si no fallo el ws de afip
    //    //            //if (!errorWS)
    //    //            //{
    //    //            //Pregunto si los datos de la entidad retornaron correctamente desde AFIP
    //    //            if (padron.success == true)
    //    //            {//CASO 2A:Si el CUIT es valido y se encuentra en la AFIP
    //    //                //Muestro datos traidos del AFIP
    //    //                if (padron.data.tipoClave == "CUIT")
    //    //                {
    //    //                    //Muestro el div con los datos referenciales del afip
    //    //                    this.TDatosRefAfip.Visible = true;
    //    //                    //Cargo los datos referenciales de la entidad que provienen de AFIP en los labels
    //    //                    this.lblNomEntidadAfip.Text = padron.data.nombre.ToString(); //Razón Social
    //    //                    //this.lblCuitRefAfip.Text = padron.data.idPersona.ToString(); //Numero de CUIT
    //    //                }

    //    //                //Muestro Mensaje de Error de validez del CUIT
    //    //                this.divValidezCuit.Visible = true;
    //    //                this.lblValidezCuit.Text = "VERIFIQUE LA RAZON SOCIAL INFORMADA POR AFIP PARA DETERMINAR SI EL CUIT ES CORRECTO"; //"VERIFIQUE EL NRO DE CUIT REGISTRADO EN IGJ";
    //    //                this.lblValidezCuit.CssClass = "validezcuitadvertencia";

    //    //                //Pregunto al usuario si desea actualizar el CUIT de la ENTIDAD
    //    //                //Muestro el check de cambio de cuit
    //    //                divCambioCuit.Visible = true;
    //    //                chkCambioCuit.Visible = true;
    //    //                chkCambioCuit.Enabled = true;
    //    //                //Oculto el ingreso de Cuit la primera vez
    //    //                TIngresoCuit.Visible = false;
    //    //                this.ucCUIT1.TituloCuit = "INGRESE EL NRO DE CUIT DE LA ENTIDAD:";
    //    //                this.chkCambioCuit.Checked = false;
    //    //            }
    //    //            else
    //    //            {//CASO 2b:Si el CUIT es invalido y no se encuentra en la AFIP 
    //    //                //Obligo a Ingresar uno Nuevo
    //    //                //Entonces muestro el DIV que incluye el control de Ingreso de CUIT sin el Check de Cambio de CUIT

    //    //                //Muestro Mensaje de Error de validez del CUIT
    //    //                this.divValidezCuit.Visible = true;
    //    //                this.lblValidezCuit.Text = "EL CUIT DE LA ENTIDAD REGISTRADO EN IGJ ES INVALIDO"; //"VERIFIQUE EL NRO DE CUIT REGISTRADO EN IGJ"
    //    //                this.lblValidezCuit.CssClass = "validezcuiterror";

    //    //                this.TIngresoCuit.Visible = true;
    //    //                this.ucCUIT1.TituloCuit = "INGRESE EL NRO DE CUIT DE LA ENTIDAD:";

    //    //                //Oculto el check de cambio de cuit
    //    //                this.chkCambioCuit.Checked = false;
    //    //                this.divCambioCuit.Visible = false;
    //    //                this.chkCambioCuit.Visible = false;
    //    //                this.chkCambioCuit.Enabled = false;
    //    //            }
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        //TODO: Revisar el Manejo del fallo del ws del afip
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    //Oculto el UpdatePanel del CUIT
    //    //    updpnlCUIT.Visible = false;
    //    //    //Muestro el boton de Confirmar
    //    //    this.btnConfirmarEntidad.Visible = true;
    //    //}

    //    //TODO_NOCUIT: No se usa el pedido de CUIT
    //    //Oculto el UpdatePanel del CUIT
    //    //updpnlCUIT.Visible = false;
    //    #endregion "Pedido de Cuit"

    //    //Muestro el boton de Confirmar
    //    this.btnConfirmarEntidad.Visible = true;

    //    //Oculto y Borro la grilla de seleccion de entidades
    //    this.gvSociedades.Visible = false;
    //    this.gvSociedades.DataSource = null;
    //    //Oculto el ingreso de Comentario
    //    this.lblComentarioEnt.Visible = false;
    //}
    //TODO_NOCUIT: No se usa el pedido de CUIT
    #region "Check para Rectificar el numero de CUIT"
    /*    
    protected void chkCambioCuit_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chkCambioCuit.Checked)
        {
            //this.btnConfirmarEntidad.CssClass = "disablebluebutton";
            TIngresoCuit.Visible = true;
            //Oculte el btnConfirmar
            //this.btnConfirmarEntidad.Visible = false;
            this.ucCUIT1.CuitText = "";
            this.ucCUIT1.NomEntAfipText = "";
            //hago un click en buscar y duelvo todo vacio para que no tire
            //el error de <pages enableEventValidation="true"/>
            //ucCUIT1.btnBuscarNomEntAfip_Click(null, null);
        }
        else
        {
            //Habilito el boton Confirmar para continuar
            this.btnConfirmarEntidad.Visible = true;
            TIngresoCuit.Visible = false;
            //Mostrar el btnConfirmar
            this.btnConfirmarEntidad.Visible = true;
            this.ucCUIT1.CuitText = "";
            this.ucCUIT1.NomEntAfipText = "";
        }
    }
    */
    #endregion "Check para Rectificar el numero de CUIT"

    //////////////////////////////////////
    // TERMINA EL INGRESO O NO DEL CUIT //
    //////////////////////////////////////
    #endregion "NOCUIT";

    private void VerificarNroCorrelativoSociedad(Int64 nroCorrelativo)
    {
        bool bloquear = false; //Si no cumple con las condiciones entonces se bloquea.

        FD.Entities.Tramite tramite = (FD.Entities.Tramite)Session["DatosTramite"];

        if (tramite != null)
        {
            if ((tramite.Id == 321) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 254) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 253) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 251) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 259) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 260) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 261) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 352) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 354) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 350) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 3) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 267) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 268) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 351) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 353) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 349) && (nroCorrelativo < 1000000))
                bloquear = true;
            else if ((tramite.Id == 262) && (nroCorrelativo < 1000000))
                bloquear = true;
        }
        else
            this.lblNroCorrelativoError.Visible = false;

        if (bloquear)
        {
            this.lblNroCorrelativoError.Visible = true;
            this.lblNroCorrelativoError.Text = "Usted seleccionó el trámite " + tramite.Descripcion + " en carácter de urgente, sin embargo, solo podrá tramitarlo por procedimiento normal debido a que el número correlativo de la entidad es menor a 1.000.000. De continuar generando el formulario este se confeccionará como <b>trámite normal.</b>";
            ((FD.Entities.Tramite)Session["DatosTramite"]).FlagUrgente = false;
        }
    }

    //TODO_NOCUIT: No se usa el pedido de CUIT
    #region "NOCUIT";
    //protected void btnConfirmar_Click(object sender, EventArgs e)
    //{
    //    //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
    //    if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
    //    {
    //        ValidateJavaScript();
    //    }


    //    //Pregunto si los validadores de la pagina encontraron error
    //    if (Page.IsValid)
    //    {
    //        //TODO_CUIT: Pregunto e inserto de que manera se realizo o no el cambio o ingreso de nuevo cuit                
    //        int TipoOpeCuit = 0; //0 = Sin Cambios / 1 = Alta / 2 = Actualizacion
    //        long NuevoCuit = 0;

    //        //TODO_NOCUIT: No se usa el pedido de CUIT
    //        #region "Pedido de CUIT"
    //        //if (HttpContext.Current.Session["FlagCUIT"] != null)
    //        //{
    //        //    FlagCUIT = (bool)HttpContext.Current.Session["FlagCUIT"];
    //        //}
    //        //else
    //        //{
    //        //    FlagCUIT = false;
    //        //}

    //        ////Pregunto si no se pidio el CUIT
    //        //if (FlagCUIT == false)
    //        //{
    //        //    TipoOpeCuit = 0; //Sin Cambios
    //        //    //Pregunto si el label es no registrado para guardar un 0
    //        //    if (this.lblCuitRef.Text.Trim() == "No Registrado" || this.lblCuitRef.Text.Trim() == "")
    //        //    {
    //        //        NuevoCuit = 0;
    //        //    }
    //        //    else
    //        //    {
    //        //        NuevoCuit = Convert.ToInt64(this.lblCuitRef.Text.Trim());
    //        //    }
    //        //}
    //        //else
    //        //{ //Si se pide CUIT
    //        //    //Pregunto si el Check de Cambio de Cuit esta Habilitado
    //        //    if (this.chkCambioCuit.Enabled == true)
    //        //    {
    //        //        //Pregunto si el Check de Cambio de Cuit esta tildado
    //        //        if (this.chkCambioCuit.Checked == true)
    //        //        {
    //        //            TipoOpeCuit = 2; //Actualizacion
    //        //            NuevoCuit = Convert.ToInt64(ucCUIT1.CuitText);
    //        //        }
    //        //        else
    //        //        {
    //        //            TipoOpeCuit = 0; //Sin Cambios
    //        //            NuevoCuit = Convert.ToInt64(this.lblCuitRef.Text.Trim());
    //        //        }
    //        //    }
    //        //    else
    //        //    {
    //        //        //Pregunto si fallo el ws del afip
    //        //        if (errorWS)
    //        //        {
    //        //            TipoOpeCuit = 0; //Sin Cambios por fallo del ws
    //        //            NuevoCuit = 0; //0 por fallo del ws
    //        //        }
    //        //        else
    //        //        {
    //        //            TipoOpeCuit = 1; //Alta
    //        //            NuevoCuit = Convert.ToInt64(ucCUIT1.CuitText);
    //        //        }
    //        //    }
    //        //}
    //        #endregion "Pedido de CUIT"

    //        //1- Completar el objeto entidad con los datos del usuario
    //        if (this.txtNroCorrelativo.Visible)//.Text != ""
    //        {
    //            //CON Numero Correlativo
    //            //FD.Entities.Entidad oEntidad = new FD.Entities.Entidad(Convert.ToInt32(Server.HtmlDecode(this.txtNroCorrelativo.Text.Trim())),
    //            //                                                       this.lblNomEntidadSelecc.Text.ToString(),
    //            //                                                       new TipoEntidad(Convert.ToInt32(vInfoTram.GetValue(2)), this.lblTipoEntidad.Text));




    //            //TODO_CUIT: Lleno objeto de Entidad con el constructor para el CUIT 06/10/16
    //            FD.Entities.Entidad oEntidad = new FD.Entities.Entidad(Convert.ToInt32(Server.HtmlDecode(this.txtNroCorrelativo.Text.Trim())),
    //                                                                   this.lblNomEntidadSelecc.Text.ToString(),
    //                                                                   new TipoEntidad(Convert.ToInt32(vInfoTram.GetValue(2)), this.lblTipoEntidad.Text),
    //                                                                   NuevoCuit,
    //                                                                   TipoOpeCuit,
    //                                                                   Convert.ToInt64(Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[3].Text))); //CuitViejo
    //            Session.Add("DatosEntidad", oEntidad);
    //            oEntidad = null;
    //        }
    //        else
    //        {
    //            switch ((string)vInfoTram.GetValue(1))
    //            {
    //                case "0":
    //                    //SIN Numero correlativo
    //                    FD.Entities.Entidad oEntidad = new FD.Entities.Entidad(Server.HtmlDecode(this.txtNomSociedad.Text.Trim().ToString()),//.lblNomEntidadSelecc.Text.ToString(),
    //                                                                           new TipoEntidad((Convert.ToInt32(vInfoTram.GetValue(2))),
    //                                                                           getTipoEntidadXIdTramite(Convert.ToInt32(vInfoTram.GetValue(2)))));//this.lblTipoEntidad.Text
    //                    Session.Add("DatosEntidad", oEntidad);
    //                    oEntidad = null;
    //                    break;
    //                case "1":
    //                    FD.Entities.Entidad oEntidadA = new FD.Entities.Entidad(Convert.ToInt32(this.lblNumCorrEnt.Text),
    //                                                                            this.lblNomEntidadSelecc.Text.ToString(),
    //                                                                            new TipoEntidad(Convert.ToInt32(vInfoTram.GetValue(2)), this.lblTipoEntidad.Text),
    //                                                                            NuevoCuit,
    //                                                                            TipoOpeCuit,
    //                                                                            Convert.ToInt64(Server.HtmlDecode(this.gvSociedades.Rows[this.gvSociedades.SelectedRow.RowIndex].Cells[3].Text))); //CuitViejo);
    //                    Session.Add("DatosEntidad", oEntidadA);
    //                    oEntidadA = null;
    //                    break;
    //            }
    //        }

    //        //2- Oculto el panel de Entidad
    //        this.PanelTituloEntidad.Visible = false;
    //        this.PanelCuerpoEntidad.Visible = false;
    //        this.btnConfirmarEntidad.Visible = false;

    //        //3- Muestro info de los datos ingresados
    //        Panel pnlInfoEntidad = (Panel)Page.Master.FindControl("pnlInfoEntidad");
    //        pnlInfoEntidad.Visible = true;

    //        #region DATOS DE LA ENTIDAD
    //        FD.Entities.Entidad oEntidad1 = SessionData.getInfoEntidad();

    //        Label lblDescNroCorrelativo = (Label)pnlInfoEntidad.FindControl("lblDescNroCorrelativo");
    //        if (oEntidad1.NroCorrelativo == 0) { lblDescNroCorrelativo.Text += ""; } else { lblDescNroCorrelativo.Text += Server.HtmlEncode(oEntidad1.NroCorrelativo.ToString()); }
    //        Label lblDescNombreEnt = (Label)pnlInfoEntidad.FindControl("lblDescNombreEnt");
    //        lblDescNombreEnt.Text += Server.HtmlEncode(oEntidad1.Nombre.ToString());
    //        Label lblDescTipoEnt = (Label)pnlInfoEntidad.FindControl("lblDescTipoEnt");
    //        lblDescTipoEnt.Text += Server.HtmlDecode(oEntidad1.TipoSociedad.Descripcion);//(string)vInfoTram.GetValue(3)
    //        //oEntidad1.TipoSociedad.Descripcion.ToString();
    //        //TODO_CUIT: Agrego el Cuit a los datos de la entidad referenciales para mostrar
    //        Label lblDescCuitEnt = (Label)pnlInfoEntidad.FindControl("lblDescCuitEnt");
    //        if (oEntidad1.Cuit == 0) { lblDescCuitEnt.Text += ""; } else { lblDescCuitEnt.Text += Server.HtmlEncode(oEntidad1.Cuit.ToString()); }
    //        #endregion

    //        //4- Habilito el Panel Captcha
    //        this.PanelCaptcha.Visible = true;
    //        this.txtCaptcha.Focus();
    //    }
    //}
    #endregion "NOCUIT";

    #region "Boton Confirmar Entidad Seleccionada Nuevo"
    //Boton Confirmar Entidad Seleccionada Nuevo
    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        FD.Entities.Entidad oEntidad = new FD.Entities.Entidad();
        FD.Entities.Entidad oEntidadSel = new FD.Entities.Entidad();
        oEntidadSel = (FD.Entities.Entidad)Session["EntidadSel"];

        try
        {
            //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
            {
                ValidateJavaScript();
            }

            //vInfoTram[0] = Id del Tramite (int)
            //vInfoTram[1] = Flag que pide Numero Correlativo (bool)
            //vInfoTram[2] = Id del Tipo de Entidad //.SetValue(this.ddlTipoEntidad.SelectedValue, 2);
            //vInfoTram[3] = Nombre del Tipo de Entidad //.SetValue(this.ddlTipoEntidad.SelectedItem.Text, 3);
            //vInfoTram[4] = Que es un Tramite Especial? (bool) //SetValue(tramEspecial, 4);
            //vInfoTram[5] = FlagAnexo;        

            //Pregunto si los validadores de la pagina encontraron error
            if (Page.IsValid)
            {
                //1- Completar el objeto entidad con los datos del usuario
                //No se entiende porque pregunta si el textbox esta visible
                //if (this.txtNroCorrelativo.Visible)//.Text != ""
                //Pregunta si esta seleccionado en el radio list button "Buscar por Numero Correlativo"
                //if (rblBuscarEntidad.SelectedValue == "1")

                //Pregunta si el Tramite Pide Numero Correlativo (FlagNumCorr)
                //PORQUE NO ES UN TRAMITE DE CONSTITUCION
                if (vInfoTram[1].ToString() == "1")
                {//CON Numero Correlativo
                    //Pregunta si hay una Entidad Seleccionada guardada en el Objeto Estatico de la Clase
                    if (oEntidadSel != null)
                    {

                        oEntidad = new FD.Entities.Entidad(oEntidadSel.NroCorrelativo,
                                                           oEntidadSel.Nombre,
                                                           new TipoEntidad(oEntidadSel.TipoSociedad.Id, oEntidadSel.TipoSociedad.Descripcion),
                                                           oEntidadSel.Cuit);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {//SIN Numero correlativo                    
                    //oEntidad = new FD.Entities.Entidad(oEntidadSel.Nombre,
                    //                                   new TipoEntidad(oEntidadSel.TipoSociedad.Id, oEntidadSel.TipoSociedad.Descripcion));
                    oEntidad = new FD.Entities.Entidad(Server.HtmlDecode(this.txtNomSociedad.Text.Trim().ToString()),//.lblNomEntidadSelecc.Text.ToString(),
                                                                          new TipoEntidad((Convert.ToInt32(vInfoTram.GetValue(2))),
                                                                          getTipoEntidadXIdTramite(Convert.ToInt32(vInfoTram.GetValue(2)))));
                }

                Session.Add("DatosEntidad", oEntidad);

                //2- Oculta el panel de Entidad
                this.PanelTituloEntidad.Visible = false;
                this.PanelCuerpoEntidad.Visible = false;
                this.btnConfirmarEntidad.Visible = false;

                //3- Muestra la info de los datos de la Entidad (en la master page)
                #region "Datos de la Entidad de Referencia"

                //Crea un control auxiliar del Panel de Info de Entidad en la Master Page
                Panel pnlInfoEntidad = (Panel)Page.Master.FindControl("pnlInfoEntidad");
                pnlInfoEntidad.Visible = true;

                //FD.Entities.Entidad oEntidad1 = SessionData.getInfoEntidad();

                //Info del Numero Correlativo de la Entidad
                Label lblDescNroCorrelativo = (Label)pnlInfoEntidad.FindControl("lblDescNroCorrelativo");
                if (oEntidad.NroCorrelativo == 0)
                {
                    lblDescNroCorrelativo.Text += "";
                }
                else
                {
                    lblDescNroCorrelativo.Text += Server.HtmlEncode(oEntidad.NroCorrelativo.ToString());
                }

                //Info del Nombre de la Entidad
                Label lblDescNombreEnt = (Label)pnlInfoEntidad.FindControl("lblDescNombreEnt");
                lblDescNombreEnt.Text += Server.HtmlEncode(oEntidad.Nombre.ToString());

                //Info de la Descripcion del Tipo Societario de la Entidad
                Label lblDescTipoEnt = (Label)pnlInfoEntidad.FindControl("lblDescTipoEnt");
                lblDescTipoEnt.Text += Server.HtmlDecode(oEntidad.TipoSociedad.Descripcion); //(string)vInfoTram.GetValue(3)

                //Info del Cuit de la Entidad
                Label lblDescCuitEnt = (Label)pnlInfoEntidad.FindControl("lblDescCuitEnt");
                if (oEntidad.Cuit == 0)
                {
                    lblDescCuitEnt.Text += "NO REGISTRADO";
                }
                else
                {
                    lblDescCuitEnt.Text += Server.HtmlEncode(oEntidad.Cuit.ToString());
                }

                #endregion "Datos de la Entidad de Referencia"


                //4- Habilita el Panel Captcha
                this.PanelCaptcha.Visible = true;
                this.txtCaptcha.Focus();
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            //Borra los objetos de entidad
            oEntidad = null;
            oEntidadSel = null;
        }

    }
    #endregion "Boton Confirmar Entidad Seleccionada Nuevo"

    //Boton Confirmar Entidad Seleccionada Viejo
    #region "Boton Confirmar Entidad Seleccionada Viejo"
    //protected void btnConfirmar_Click(object sender, EventArgs e)
    //{
    //    //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
    //    if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
    //    {
    //        ValidateJavaScript();
    //    }

    //    //Pregunto si los validadores de la pagina encontraron error
    //    if (Page.IsValid)
    //    {
    //        //1- Completar el objeto entidad con los datos del usuario
    //        //if (this.txtNroCorrelativo.Visible)//.Text != ""
    //        //Pregunta si esta seleccionado en el radio list button "Buscar por Numero Correlativo"
    //        //if (rblBuscarEntidad.SelectedValue == "1")
    //        {
    //            //CON Numero Correlativo
    //            //TODO: Traer Cuit y demas datos desde la grilla o un objeto
    //            FD.Entities.Entidad oEntidad = new FD.Entities.Entidad(Convert.ToInt32(Server.HtmlDecode(this.txtNroCorrelativo.Text.Trim())),
    //                                                                   this.lblNomEntidadSelecc.Text.ToString(),
    //                                                                   new TipoEntidad(Convert.ToInt32(vInfoTram.GetValue(2)), this.lblTipoEntidad.Text),
    //                                                                   0);

    //            Session.Add("DatosEntidad", oEntidad);
    //            oEntidad = null;
    //        }
    //        else
    //        {
    //            //SIN Numero correlativo
    //            FD.Entities.Entidad oEntidad = new FD.Entities.Entidad(Server.HtmlDecode(this.txtNomSociedad.Text.Trim().ToString()),//.lblNomEntidadSelecc.Text.ToString(),
    //                                                                   new TipoEntidad((Convert.ToInt32(vInfoTram.GetValue(2))),
    //                                                                   getTipoEntidadXIdTramite(Convert.ToInt32(vInfoTram.GetValue(2)))));//this.lblTipoEntidad.Text
    //            Session.Add("DatosEntidad", oEntidad);
    //            oEntidad = null;
    //        }

    //        //2- Oculto el panel de Entidad
    //        this.PanelTituloEntidad.Visible = false;
    //        this.PanelCuerpoEntidad.Visible = false;
    //        this.btnConfirmarEntidad.Visible = false;

    //        //3- Muestro info de los datos ingresados
    //        Panel pnlInfoEntidad = (Panel)Page.Master.FindControl("pnlInfoEntidad");
    //        pnlInfoEntidad.Visible = true;

    //        #region DATOS DE LA ENTIDAD
    //        FD.Entities.Entidad oEntidad1 = SessionData.getInfoEntidad();

    //        Label lblDescNroCorrelativo = (Label)pnlInfoEntidad.FindControl("lblDescNroCorrelativo");
    //        if (oEntidad1.NroCorrelativo == 0) { lblDescNroCorrelativo.Text += ""; } else { lblDescNroCorrelativo.Text += Server.HtmlEncode(oEntidad1.NroCorrelativo.ToString()); }
    //        Label lblDescNombreEnt = (Label)pnlInfoEntidad.FindControl("lblDescNombreEnt");
    //        lblDescNombreEnt.Text += Server.HtmlEncode(oEntidad1.Nombre.ToString());
    //        Label lblDescTipoEnt = (Label)pnlInfoEntidad.FindControl("lblDescTipoEnt");
    //        lblDescTipoEnt.Text += Server.HtmlDecode(oEntidad1.TipoSociedad.Descripcion);//(string)vInfoTram.GetValue(3)
    //        #endregion

    //        //4- Habilito el Panel Captcha
    //        this.PanelCaptcha.Visible = true;
    //        this.txtCaptcha.Focus();
    //    }
    //}
    #endregion "Boton Confirmar Entidad Seleccionada Viejo"

    private string getTipoEntidadXIdTramite(int IdTipoEntidad)
    {
        BLTipoEntidad oBLTipoEntidad;
        try
        {
            oBLTipoEntidad = new BLTipoEntidad();
            return oBLTipoEntidad.getTipoEntidadXIdTramite(IdTipoEntidad);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            oBLTipoEntidad = null;
        }
    }

    protected void btnCaptcha_Click(object sender, EventArgs e)
    {
        #region "Validar Entidad"

        //Valida:
        //ERR01: Si "El Nro Correlativo no se corresponde al de la Entidad Seleccionada"
        //ERR02: Si "El Tipo Societario no se corresponde al de la Entidad Seleccionada"
        //ERR03: Si "El Tipo Societario de la Entidad Seleccionada no está asociado al Tipo de Trámite"
        //ERR04: Si "El Nro Correlativo de la Entidad Seleccionada no se corresponde al de nuestra Base de Datos"
        //ERR05: Si "El Tipo Societario de la Entidad Seleccionada no se corresponde al de nuestra Base de Datos"

        //bool EntidadOk = false;
        //string msg = "OK";

        //BLEntidad oBLEntidad = new BLEntidad();
        //FD.Entities.Tramite oTramiteAux = (FD.Entities.Tramite)Session["DatosTramite"];
        //FD.Entities.Entidad oEntidadAux = (FD.Entities.Entidad)Session["DatosEntidad"];
        //FD.Entities.Entidad oEntidadSelAux = (FD.Entities.Entidad)Session["EntidadSel"];
        //FD.Entities.TipoEntidad oTipoEntidadAux = new TipoEntidad(Convert.ToInt32(vInfoTram.GetValue(2)));

        //try
        //{
        //    EntidadOk = oBLEntidad.ValidarEntidadconTramite(oTramiteAux, oEntidadAux, oEntidadSelAux, oTipoEntidadAux, out msg);
        //}
        //catch (Exception ex)
        //{
        //    msg = "ERROR al Validar la Entidad con el Tramite";
        //}
        //finally
        //{
        //    oTramiteAux = null;
        //    oEntidadAux = null;
        //    oEntidadSelAux = null;
        //    oTipoEntidadAux = null;
        //    oBLEntidad = null;
        //}

        //if (!EntidadOk)
        //{
        //    Session.Add("MsgErrorValEntidad", msg);
        //    Response.Redirect("Error.aspx");
        //}

        #endregion "Validar Entidad"

        int a = 0;
        bool FlagFormaPago = false;
        if (this.chkFormaPago.Enabled != false)
        {
            FlagFormaPago = this.chkFormaPago.Checked;
        }
        else
        {
            FlagFormaPago = false;
        }

        FD.Entities.Depositante oDepositante;

        try
        {

            //Pregunto si paga por banelco
            if (FlagFormaPago)
            {
                /****************************************/
                /*-- EMPIEZA NUEVA MODIF BANELCO 2014 --*/
                /****************************************/

                //Pregunto si salto algun validador
                if (Page.IsValid)
                {
                    string msgErrorBanelco = "";

                    //Valido el cuadro de pago por banelco
                    if (!ValidarBanelco(out msgErrorBanelco))
                    {
                        //Como Fallo Muestro Mensaje de Error Banelco y Salgo
                        divErrorBanelco.Visible = true;
                        lblErrorBanelco.Text = msgErrorBanelco;
                        return;
                    }
                    else
                    {
                        divErrorBanelco.Visible = false;
                        lblErrorBanelco.Text = "";

                        //Cargo el Objeto depositante
                        oDepositante = new FD.Entities.Depositante(Convert.ToInt64(this.txtDNIpago1.Text.Trim()),
                                       this.ddlBANCOpago.SelectedValue,
                                       this.ddlBANCOpago.SelectedItem.Text.Trim().ToString());
                        Session.Add("DatosDepositante", oDepositante);
                    }
                }
                else
                {
                    return;
                }

                /****************************************/
                /*-- TERMINA NUEVA MODIF BANELCO 2014 --*/
                /****************************************/
            }


            //Si el captcha esta habilitado => Para version Online
            if ((bool)HttpContext.Current.Session["CaptchaHabilitado"] == true)
            {
                a = 1;

                if (this.txtCaptcha.Text.ToUpper() != (string)Session["captchaValue"])
                {
                    this.lblErrorCaptcha.Text = "";
                    this.lblErrorCaptcha.Text = "Lo ingresado no concuerda con la imagen. Intentelo de nuevo.";
                    this.lblErrorCaptcha.Visible = true;
                    a = 1;
                    return;
                }
                else
                {
                    //this.lblErrorCaptcha.Visible = false;
                    //this.btnCaptcha.Enabled = false;
                    //TODO_GATEWAY: AGREGO IDS DE TRAMITES HARCODEADOS ADEMAS DEL 6
                    //evaluo Id Tramite
                    //if (!vInfoTram[0].ToString().Trim().Equals("6"))
                    //if (vInfoTram[0].ToString().Trim().Equals("6") || vInfoTram[0].ToString().Trim().Equals("372") || vInfoTram[0].ToString().Trim().Equals("373"))

                    BLTramite oBLTramite = new BLTramite();
                    bool FlagReserva = oBLTramite.RetornarTramiteReserva(Convert.ToInt32(vInfoTram.GetValue(0)));  //vInfoTram.GetValue(0) ==> Id del Tramite
                    //TODO_RESERVA: Retorno si para este id de tramite es del tipo de reserva
                    //1 = Reserva <--> 0 = No Reserva                    

                    //TODO: Devolver un Error si fallo la persistencia de los datos
                    guardarFormulario(FlagReserva, FlagFormaPago);

                    //Pregunto si el codigo de referencia de pago banelco fallo en el gateway
                    if ((string)Session["codrefpago"] != "-1" && (string)Session["codrefpago"] != "-2" && (string)Session["codrefpago"] != null)
                    {    //Deshabilito el boton del captcha
                        this.lblErrorCaptcha.Visible = false;
                        this.btnCaptcha.Enabled = false;

                        Response.Redirect("DownLoadForm.aspx", false);
                    }
                    //TODO: Crear un Else con respuesta de error en codrefpago
                }
            }
            //Si el captcha esta deshabilitado => Para version Intranet
            else
            {
                a = 1;

                this.lblErrorCaptcha.Visible = false;
                this.btnCaptcha.Enabled = false;
                BLTramite oBLTramite = new BLTramite();
                bool FlagReserva = oBLTramite.RetornarTramiteReserva(Convert.ToInt32(vInfoTram.GetValue(0)));  //vInfoTram.GetValue(0) ==> Id del Tramite
                //TODO_RESERVA: Retorno si para este id de tramite es del tipo de reserva
                //1 = Reserva <--> 0 = No Reserva

                //TODO: Devolver un Error si fallo la persistencia de los datos
                guardarFormulario(FlagReserva, FlagFormaPago);

                //Pregunto si el codigo de referencia de pago banelco fallo en el gateway
                if ((string)Session["codrefpago"] != "-1" && (string)Session["codrefpago"] != "-2")
                    Response.Redirect("DownLoadForm.aspx", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (a != 1)
            {
                this.lblErrorCaptcha.Text = string.Empty;
            }
        }
    }

    ///////////////////////////////////
    // GENERACION DEL CODIGO BANELCO //
    ///////////////////////////////////
    public void ObtenerCodRefPago(Transaccion oTransaccion, Operador oDatosOperador, bool reserva, bool formapago)
    {
        #region "Obtener Codigo de Referencia de Pago mandiante GateWay"

        //TODO_GATEWAY: DESCOMENTAR - Quito GateWay de Pagos por ahora
        //Se obtiene el codigo de referencia de pago
        string codrefpago = Util.GenerarCodigoReferenciaPagoNuevo(reserva, formapago);

        //Persisto el codrefpago en la propiedad del objeto
        HttpContext.Current.Session.Add("codrefpago", codrefpago);
        oTransaccion.CodRefPago = codrefpago;

        if (codrefpago != null)
        {
            //codrefpag "" => Sin Banelco / codrefpago = "-1" => Error en gateway / codrefpago = "-2" => Error en banelco
            switch (codrefpago)
            {
                case "-1":
                    //ERROR EN EL GATEWAY
                    //codigoBanelco = "-1";
                    this.divErrorBanelco.Visible = true;
                    //this.lblErrorBanelco.Text = "ERROR EN EL GATEWAY (codrefpago = -1): No se pudo informar el pago a Banelco en este momento, intente mas tarde";
                    this.lblErrorBanelco.Text = "No se pudo informar el pago a Banelco en este momento, intente mas tarde";
                    break;
                case "-2":
                    //ERROR EN BANELCO
                    //codigoBanelco = "-2";
                    this.divErrorBanelco.Visible = true;
                    //this.lblErrorBanelco.Text = "ERROR EN BANELCO (codrefpago = -2): No se pudo informar el pago a Banelco en este momento, intente mas tarde";
                    this.lblErrorBanelco.Text = "No se pudo informar el pago a Banelco en este momento, intente mas tarde";

                    switch ((string)HttpContext.Current.Session["codigorespuesta"])
                    {
                        //Código de Respuesta = 33, Descripcion: Factura ya pagada
                        case "33":
                            this.lblErrorBanelco.Text = "No se pudo realizar el pago a Banelco porque la Factura ya ha sido pagada";
                            break;
                        //Código de respuesta ISO = 62, Descripcion: Usuario inexistente
                        case "62":
                            this.lblErrorBanelco.Text = "No se pudo realizar el pago a Banelco porque el dni y banco ingresados no corresponden a un usuario existente";
                            break;
                        //Código de respuesta ISO = 69, Descripcion: Empresa Inhabilitada
                        case "69":
                            this.lblErrorBanelco.Text = "No se pudo realizar el pago a Banelco porque la Empresa se encuentra Inhabilitada";
                            break;
                        default:
                            this.lblErrorBanelco.Text = "No se pudo informar el pago a Banelco en este momento, intente mas tarde";
                            break;
                    }
                    break;
                case "":
                    //CODIGO DE REFERENCIA DE PAGO BANELCO VACIO - PAGA POR CAJA
                    //codigoBanelco = wsAF.IdentificacionFactura; // ejm: "0000000000001384235"
                    this.divErrorBanelco.Visible = false;
                    this.lblErrorBanelco.Text = "";
                    break;
                default:
                    //CODIGO DE REFERENCIA DE PAGO BANELCO LLENO
                    //codigoBanelco = wsAF.IdentificacionFactura; // ejm: "0000000000001384235"
                    this.divErrorBanelco.Visible = false;
                    this.lblErrorBanelco.Text = "";

                    //Ya no actualizo porque lo inserto todo junto
                    //Actualizacion del Codigo de Referencia de Pago en la tabla Transaccion
                    //oBLTransaccion.UpdateTransaccion(Convert.ToInt32(oDatosOperador.IdTrans), codrefpago);
                    break;
            }
        }
        else
        {
            //ERROR EN EL GATEWAY 
            //codrefpago == null
            this.divErrorBanelco.Visible = true;
            //this.lblErrorBanelco.Text = "ERROR EN EL GATEWAY (codrefpago = null): No se pudo informar el pago a Banelco en este momento, intente mas tarde";
            this.lblErrorBanelco.Text = "No se pudo informar el pago a Banelco en este momento, intente mas tarde";
        }

        #endregion
    }

    /////////////////////////////////////////////////////
    // PERSISTENCIA DEL FORMULARIO EN LA BASE DE DATOS //
    /////////////////////////////////////////////////////
    private void guardarFormulario(bool reserva, bool formapago)
    {
        //INSTANCIA DE OBJETOS
        #region "Instancia de Objetos"

        FD.Entities.Entidad oEntidad;
        FD.Entities.Transaccion oTransaccion;
        FD.Entities.Firmante oFirmante;
        FD.Entities.Formulario oFormulario;
        FD.Entities.Tramite oTramite;
        FD.Entities.Denominacion oDenominacion;
        FD.Entities.Depositante oDepositante;
        BLTransaccion oBLTransaccion;
        TipoEntidad oTipoEntidad;

        /*-- NUEVA MODIF ANEXOS 2021 --*/
        //Nueva lista de Objetos de Libros de Rubrica
        List<Libro> lstLibros;
        //Cargar el Flag desde la base como el flagReserva
        //[Retorna si el tramite es reserva o no a partir del flag en la tabla NomecladorTramite - public bool RetornarTramiteReserva(int id)]

        //posicion 5 = FlagAnexo: 1 = Anexo I - 2 = Anexo II - 3 = Anexo III
        object[] vInfoTram = (object[])Session["vInfoTram"];

        if (vInfoTram[5] != null)
        {
            FlagAnexo = Convert.ToInt32(vInfoTram[5].ToString().Trim());
        }

        #endregion "Instancia de Objetos"

        try
        {
            //CARGA DE OBJETOS DESDE SESSION
            #region "Carga los Objetos desde la Session"
            oFirmante = (FD.Entities.Firmante)Session["DatosFirmante"];
            oFormulario = (FD.Entities.Formulario)Session["DatosFormulario"];
            oTramite = (FD.Entities.Tramite)Session["DatosTramite"];
            /*-- NUEVA MODIF BANELCO 2014 --*/
            oDepositante = (FD.Entities.Depositante)Session["DatosDepositante"];
            /*-- NUEVA MODIF ANEXOS 2021 --*/
            lstLibros = (List<Libro>)Session["ListaLibros"];
            #endregion "Carga los Objetos desde la Session"

            //GUARDA UN FORMULARIO QUE NO ES DE RESERVA DE NOMBRE
            #region "Guarda Formulario (no de reserva)"
            if (!reserva)
            {
                //Se obtiene el motivo del control txtMotivo removiendo los enters
                string motivo = Util.RemoveEnter(this.txtMotivo.Text.Trim().ToString());
                oEntidad = (FD.Entities.Entidad)Session["DatosEntidad"];

                oTransaccion = new FD.Entities.Transaccion(new Estado(0), oFormulario,
                                                      oFirmante,
                                                      oEntidad,
                                                      oTramite,
                                                      Server.HtmlEncode(motivo));

                oBLTransaccion = new BLTransaccion();
                Operador oDatosOperador = new Operador();
                oDatosOperador.Reserva = false;
                Session.Add("DatosOperador", oDatosOperador);
                BLLibro oBLLibro = new BLLibro();

                #region "Obtener Codigo de Referencia de Pago mandiante GateWay"
                ////////////////////////////////////////////////////
                // EMPIEZA LA GENERACION DEL CODIGO BANELCO NUEVO //
                ////////////////////////////////////////////////////

                //TODO_GATEWAY: 
                ObtenerCodRefPago(oTransaccion, oDatosOperador, reserva, formapago);

                ////////////////////////////////////////////////////
                // TERMINA LA GENERACION DEL CODIGO BANELCO NUEVO //
                ////////////////////////////////////////////////////
                #endregion "Obtener Codigo de Referencia de Pago mandiante GateWay"

                #region "Guarda la transaccion en la base"

                if (oTransaccion.CodRefPago != "-1" && oTransaccion.CodRefPago != "-2" && oTransaccion.CodRefPago != null)
                {
                    //TODO: Retornar Id de la Transaccion, no se usa.
                    long IdTransaccion = 0;

                    //GUARDA UN FORMULARIO QUE NO ES DE RESERVA DE NOMBRE CON LIBROS DE RUBRICA
                    //Pregunta si el Formulario partenece o no al pedido de Anexo de Rubrica
                    if (FlagAnexo > 0)
                    {
                        if (lstLibros != null)
                        {
                            //Alta de la Transaccion con Alta de Libros de Rubrica
                            oBLTransaccion.AltaTransaccionLibros(oTransaccion, oDatosOperador, oDepositante, lstLibros, out IdTransaccion);

                        }
                        else
                        {
                            throw new Exception("No se pudo dar de alta la transacción porque no se cargaron los libros de rubrica");
                        }
                    }
                    else
                    {
                        //Alta de la Transaccion Comun
                        oBLTransaccion.AltaTransaccion(oTransaccion, oDatosOperador, oDepositante, out IdTransaccion);
                    }
                }
                else
                {
                    //TODO: Muestro Error en el cuadro de banelco
                    return;
                }
                #endregion
            }
            #endregion "Guarda Formulario (no de reserva)"

            //GUARDA UN FORMULARIO QUE ES DE RESERVA DE NOMBRE
            #region "Guarda Formulario de Reserva"
            else
            {
                oTipoEntidad = new TipoEntidad(Convert.ToInt32(vInfoTram.GetValue(2)));
                oEntidad = new FD.Entities.Entidad(oTipoEntidad);

                oDenominacion = (FD.Entities.Denominacion)Session["Denominacion"];

                oTransaccion = new FD.Entities.Transaccion(new Estado(0), oFormulario,
                                                                          oFirmante,
                                                                          oTramite,
                                                                          oEntidad,
                                                                          oDenominacion.Denominacion1.ToString());
                //codrefpago);
                oBLTransaccion = new BLTransaccion();
                Operador oDatosOperador = new Operador();
                //Alta de la Transaccion
                //oBLTransaccion.AltaTransaccionReserva(oTransaccion, oDatosOperador);
                oDatosOperador.Reserva = true;
                Session.Add("DatosOperador", oDatosOperador);

                #region "Obtener Codigo de Referencia de Pago mandiante GateWay"
                ////////////////////////////////////////////////////
                // EMPIEZA LA GENERACION DEL CODIGO BANELCO NUEVO //
                ////////////////////////////////////////////////////

                //TODO_GATEWAY: 
                ObtenerCodRefPago(oTransaccion, oDatosOperador, reserva, formapago);

                ////////////////////////////////////////////////////
                // TERMINA LA GENERACION DEL CODIGO BANELCO NUEVO //
                ////////////////////////////////////////////////////
                #endregion

                #region "Guarda la transaccion en la base"

                if (oTransaccion.CodRefPago != "-1" && oTransaccion.CodRefPago != "-2" && oTransaccion.CodRefPago != null)
                {
                    //Alta de la Transaccion
                    oBLTransaccion.AltaTransaccionReserva(oTransaccion, oDatosOperador, oDenominacion, oDepositante);
                }
                else
                {
                    //Muestro Error en el cuadro de banelco
                    return;
                }

                #endregion
            }
            #endregion "Guarda Formulario de Reserva"
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //VACIA LOS OBJETOS YA UTILIZADOS
            #region "Vacia los Objetos"
            //Elimino los objetos
            oFirmante = null;
            oTramite = null;
            oFormulario = null;
            oEntidad = null;
            oTransaccion = null;
            oBLTransaccion = null;
            oDenominacion = null;
            oTipoEntidad = null;
            oDepositante = null;
            #endregion "Vacia los Objetos"
        }
    }


    /**************************************************/
    /*-- PANEL DE CONSTITUCION Y RESERVA DE NOMBRES --*/
    /**************************************************/

    #region "Metodos del Panel de Constitucion y Reserva de Nombres"

    protected void btnAceptarDemonSoc_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            List<Constituyente> lConsituyente = new List<Constituyente>(2);
            Denominacion oDenominacion;

            //MUESTRO LA BARRA DE DENOMINACIONES
            Panel pnlInfoDenomSoc = (Panel)Page.Master.FindControl("pnlInfoDenomSoc");
            pnlInfoDenomSoc.Visible = true;

            Label lblDescDenomOp1 = (Label)pnlInfoDenomSoc.FindControl("lblDescDenomOp1");
            lblDescDenomOp1.Text = Server.HtmlEncode(this.txtOp1.Text.ToString().Trim());
            Label lblDescDenomOp2 = (Label)pnlInfoDenomSoc.FindControl("lblDescDenomOp2");
            lblDescDenomOp2.Text = Server.HtmlEncode(this.txtOp2.Text.ToString().Trim());
            Label lblDescDenomOp3 = (Label)pnlInfoDenomSoc.FindControl("lblDescDenomOp3");
            lblDescDenomOp3.Text = Server.HtmlEncode(this.txtOp3.Text.ToString().Trim());

            Panel pnlConstituyente = (Panel)Page.Master.FindControl("pnlConstituyente");
            pnlConstituyente.Visible = true;


            #region Mapeo de Labels al Master y a objetos

            switch (this.rblSeleccionOpcion.SelectedValue)
            {
                case "0":
                    Panel pnlPersonaConstituyente = (Panel)Page.Master.FindControl("pnlPersonaConstituyente");
                    pnlPersonaConstituyente.Visible = true;

                    Label lblDescNumDoc1 = (Label)pnlPersonaConstituyente.FindControl("lblDescNumDoc1");
                    lblDescNumDoc1.Text = Server.HtmlEncode(this.txtDNIConsti1.Text.Trim().ToString());
                    Label lblDescNumDoc2 = (Label)pnlPersonaConstituyente.FindControl("lblDescNumDoc2");
                    lblDescNumDoc2.Text = Server.HtmlEncode(this.txtDNIConsti2.Text.Trim().ToString());
                    Label lblDescNomYApeConsti1 = (Label)pnlPersonaConstituyente.FindControl("lblDescNomYApeConsti1");
                    lblDescNomYApeConsti1.Text = Server.HtmlEncode(this.txtNyAConsti1.Text.ToString().Trim());
                    Label lblDescNomYApeConsti2 = (Label)pnlPersonaConstituyente.FindControl("lblDescNomYApeConsti2");
                    lblDescNomYApeConsti2.Text = Server.HtmlEncode(this.txtNyAConsti2.Text.ToString().Trim());

                    //Completo las propiedades del objeto CONSTITUYENTE
                    lConsituyente.Add(new Constituyente(Server.HtmlDecode(this.txtDNIConsti1.Text.ToString().Trim()),
                                                        Server.HtmlDecode(this.txtNyAConsti1.Text.ToString().Trim()), 1));
                    lConsituyente.Add(new Constituyente(Server.HtmlDecode(this.txtDNIConsti2.Text.ToString().Trim()),
                                                        Server.HtmlDecode(this.txtNyAConsti2.Text.ToString().Trim()), 2));
                    break;
                case "1":
                    Panel pnlDenomConstituyente = (Panel)Page.Master.FindControl("pnlDenomConstituyente");
                    pnlDenomConstituyente.Visible = true;

                    Label lblDescDenom1Consti = (Label)pnlDenomConstituyente.FindControl("lblDescDenom1Consti");
                    lblDescDenom1Consti.Text = Server.HtmlEncode(this.txtDenom1.Text.Trim().ToString());
                    Label lblDescDenom2Consti = (Label)pnlDenomConstituyente.FindControl("lblDescDenom2Consti");
                    lblDescDenom2Consti.Text = Server.HtmlEncode(this.txtDenom2.Text.Trim().ToString());

                    //Completo las propiedades del objeto CONSTITUYENTE
                    lConsituyente.Add(new Constituyente(Server.HtmlDecode(this.txtDenom1.Text.Trim().ToString()),
                                                        Server.HtmlDecode(this.txtDenom2.Text.Trim().ToString())));
                    break;
                case "2":
                    Panel pnlMixtoMaster = (Panel)Page.Master.FindControl("pnlMixtoMaster");
                    pnlMixtoMaster.Visible = true;

                    Label lblDescNumDocMixto = (Label)pnlMixtoMaster.FindControl("lblDescNumDocMixto");
                    lblDescNumDocMixto.Text = Server.HtmlEncode(this.txtDNIMixto.Text.ToString().Trim());
                    Label lblDescNomYApeConstiMixto = (Label)pnlMixtoMaster.FindControl("lblDescNomYApeConstiMixto");
                    lblDescNomYApeConstiMixto.Text = Server.HtmlEncode(this.txtNyAMixto.Text.ToString().Trim());
                    Label lblDescDenomConstiMixto = (Label)pnlMixtoMaster.FindControl("lblDescDenomConstiMixto");
                    lblDescDenomConstiMixto.Text = Server.HtmlEncode(this.txtDenomMixto.Text.ToString().Trim());

                    //Completo las propiedades del objeto CONSTITUYENTE
                    lConsituyente.Add(new Constituyente(Server.HtmlDecode(this.txtDNIMixto.Text.ToString().Trim()),
                                                        Server.HtmlDecode(this.txtNyAMixto.Text.ToString().Trim()),
                                                        Server.HtmlDecode(this.txtDenomMixto.Text.ToString().Trim())));
                    break;
            }
            oDenominacion = new Denominacion(Server.HtmlDecode(this.txtOp1.Text.ToString().Trim()),
                                             Server.HtmlDecode(this.txtOp2.Text.ToString().Trim()),
                                             Server.HtmlDecode(this.txtOp3.Text.ToString().Trim()),
                                             lConsituyente,
                                             this.rblSeleccionOpcion.SelectedIndex);//lleno el tipo asi puedo saber qye estampar en el pdf dependiente esta opcion
            Session.Add("Denominacion", oDenominacion);

            oDenominacion = null;

            #endregion

            this.btnConfirmarEntidad.Visible = false;
            //oculto el panel de denominaciones propuestas-
            this.PanelTituloEntidad.Visible = false;
            this.PanelCuerpoEntidad.Visible = false;
            this.PanelCaptcha.Visible = true;
        }
    }

    protected void rblSeleccionOpcion_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (this.rblSeleccionOpcion.SelectedValue)
        {
            case "0":
                this.pnlDenom.Visible = false;
                this.pnlMixto.Visible = false;
                //this.txtConstDoc.Text = string.Empty;
                this.pnlNomApe.Visible = true;
                break;
            case "1":
                this.pnlNomApe.Visible = false;
                this.pnlMixto.Visible = false;
                this.txtDenom1.Text = string.Empty;
                this.txtDenom2.Text = string.Empty;
                this.pnlDenom.Visible = true;
                break;
            case "2":
                this.pnlNomApe.Visible = false;
                this.pnlDenom.Visible = false;
                this.pnlMixto.Visible = true;
                break;
        }
    }

    #endregion "Metodos del Panel de Constitucion y Reserva de Nombres"

    /************************/
    /*-- DESCARGA DEL PDF --*/
    /************************/

    #region "Descarga de PDF del Formulario"
    protected void ImgBtnDescForm_Click(object sender, ImageClickEventArgs e)
    {
        Util.DownloadForm();
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    #endregion "Descarga de PDF del Formulario"

    /********************/
    /*-- VALIDACIONES --*/
    /********************/

    #region "Validaciones"

    protected void ValidateTextbox(object source, ServerValidateEventArgs args)
    {
        OcultarValidators();
        string msj = "";
        //Que no sea NULL
        Validation.NullValueValidation(args, out msj);
        //Que solo sean numeros y letras.

        if (args.IsValid)
            Validation.StringNumericValidation(args, out msj);
        CustomValidator cv = (CustomValidator)source;
        cv.ErrorMessage = msj;
    }

    private void OcultarValidators()
    {
        this.RequiredFieldValidator1.Visible = false;
        this.RequiredFieldValidator2.Visible = false;
        this.RequiredFieldValidator3.Visible = false;
        this.RequiredFieldValidator4.Visible = false;
        this.RequiredFieldValidator6.Visible = false;
        this.RequiredFieldValidator7.Visible = false;
        this.RequiredFieldValidator8.Visible = false;
        this.RequiredFieldValidator9.Visible = false;
        this.RequiredFieldValidator5.Visible = false;
        this.RequiredFieldValidator10.Visible = false;
    }

    protected void ValidarDatosAdjuntos(object source, ServerValidateEventArgs args)
    {
        Validation.MaxCharactersValidation(args, 300);
    }

    protected void imgRefreshCaptcha_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnEfectuarPago_Click(object sender, EventArgs e)
    {

    }

    #endregion "Validaciones"

    /****************************************/
    /*-- EMPIEZA NUEVA MODIF BANELCO 2014 --*/
    /****************************************/

    #region "Metodos para el Pago mandiante GateWay de Pagos Banelco"
    protected void ddlBANCOpago_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void chkFormaPago_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFormaPago.Checked)
        {
            txtDNIpago1.Text = "";
            txtDNIpago1.Enable = true;
            txtDNIpago1.ValidarDNI = true;
            txtDNIpago1.ValidarObligatorio = true;
            txtDNIpago2.Text = "";
            txtDNIpago2.Enable = true;
            txtDNIpago2.ValidarDNI = true;
            txtDNIpago2.ValidarObligatorio = true;
            ddlBANCOpago.Enabled = true;
        }
        else
        {
            txtDNIpago1.Text = "";
            txtDNIpago1.Enable = false;
            txtDNIpago1.ValidarDNI = false;
            txtDNIpago1.ValidarObligatorio = false;
            txtDNIpago2.Text = "";
            txtDNIpago2.Enable = false;
            txtDNIpago2.ValidarDNI = false;
            txtDNIpago2.ValidarObligatorio = false;
            ddlBANCOpago.SelectedValue = "Seleccione un Banco";
            ddlBANCOpago.Enabled = false;
            divErrorBanelco.Visible = false;
            lblErrorBanelco.Text = "";
        }
    }

    private bool ValidarBanelco(out string msgError)
    {
        bool DNIOK = false;
        bool BANCOOK = false;
        string msgErrorBanelco = "";

        //Valido si ambos DNI son iguales
        if (txtDNIpago1.Text.Trim() == txtDNIpago2.Text.Trim())
        {
            DNIOK = true;
        }
        else
        {
            DNIOK = false;
            msgErrorBanelco = "Los numeros de DNI ingresados no coinciden.";
        }

        //Valido si selecciono algun banco del combo
        if (ddlBANCOpago.SelectedValue != "Seleccione un Banco")
        {
            BANCOOK = true;
        }
        else
        {
            BANCOOK = false;
            if (msgErrorBanelco != "")
            {
                msgErrorBanelco = msgErrorBanelco + "<br />" + "Debe seleccionar su entidad bancaria.";
            }
            else
            {
                msgErrorBanelco = "Debe seleccionar su entidad bancaria";
            }
        }

        if (chkFormaPago.Checked)
        {
            if (DNIOK && BANCOOK)
            {
                msgError = msgErrorBanelco;
                return true;
            }
            else
            {
                msgError = msgErrorBanelco;
                return false;
            }
        }
        else
        {
            msgError = msgErrorBanelco;
            return false;
        }
    }

    private void getBancos()
    {
        try
        {
            BLBanco oBLBanco = new BLBanco();
            List<Banco> lBanco = new List<Banco>();
            oBLBanco.RetornarBancos(lBanco);

            this.ddlBANCOpago.DataSource = lBanco;
            this.ddlBANCOpago.DataTextField = "descripcion";
            this.ddlBANCOpago.DataValueField = "id";
            this.ddlBANCOpago.DataBind();

            oBLBanco = null;
            lBanco = null;
            this.ddlBANCOpago.Items.Insert(0, "Seleccione un Banco");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion "Metodos para el Pago mandiante GateWay de Pagos Banelco"

    /****************************************/
    /*-- TERMINA NUEVA MODIF BANELCO 2014 --*/
    /****************************************/
}