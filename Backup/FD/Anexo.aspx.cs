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

public partial class Anexo : System.Web.UI.Page
{
    //public object[] vInfoTram
    //{
    //    get { return ((object[])Session["vInfoTram"]); }
    //    set { Session.Add("vInfoTram", value); }
    //}

    //TODO_STATIC: Revisar si hay problemas con estas variables static
    public static int FlagAnexo = 0;
    public static List<TipoLibro> lstTipoLibro = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //TODO_DESCOMENTAR: Descomentar esto en produccion
            Page.MaintainScrollPositionOnPostBack = true;

            FD.Entities.Tramite oTramite = (FD.Entities.Tramite)Session["DatosTramite"];
            BLLibro oBLLibro = new BLLibro();

            if (!Page.IsPostBack)
            {
                object[] vInfoTram = (object[])Session["vInfoTram"];
                FlagAnexo = Convert.ToInt32(vInfoTram[5].ToString().Trim());

                //Muestra los datos referenciales
                mapeoBarraInfo();

                //Varia el Anexo de Rubrica segun el numero de tramite y el tipo societario
                if (FlagAnexo > 0)
                {
                    lstTipoLibro = oBLLibro.RetornarTiposLibros();
                    CambiarAnexo();
                }
                else
                {
                    throw new Exception("No se encontro el anexo");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
            Log.GrabarAdvertencia("ERROR en la pagina de Anexos" + ex.Message, "Page_Load", "LOCAL");
        }
    }

    //Retorna tipo de Libro por el IdTipoLibro
    private TipoLibro GetLibro(int id)
    {
        TipoLibro oTipoLibro = new TipoLibro();

        foreach (TipoLibro tipolib in lstTipoLibro)
        {
            if (tipolib.IdTipoLibro == id)
            {
                oTipoLibro = tipolib;
            }
        }

        return oTipoLibro;
    }

    private void CambiarAnexo()
    {
        switch (FlagAnexo)
        {
            //ANEXO I //FORMULARIO DE SOLICITUD RUBRICA SOCIEDADES NO ACCIONARIAS (SRL)
            case 1: //ANEXO I
            case 4: //ANEXO I (SIN LIBROS OPCIONALES, SOLO OBLIGATORIOS)
                #region "ANEXO I"

                lblTituloAnexo.Text = "Anexo I";
                lblTituloFormulario.Text = "Formulario de Solicitud Rúbrica Sociedades No Accionarias (SRL)";

                //Check Separa Libros Anexo I - Oculta Check Anexo 2
                chkAnexo1.Visible = true;
                chkAnexo1.Enabled = true;
                chkAnexo2.Visible = false;
                chkAnexo2.Enabled = false;
                tablachkAnexo2.Attributes.Add("style", "display: none;");

                //Libros Obligatorios
                pnlLibOb.Visible = true;
                pnlLibOb.Enabled = true;
                //Titulos de Libros
                UcLIBOB1.prophfIdTipoLibro = 1;
                UcLIBOB1.Visible = GetLibro(1).Habilitado;
                UcLIBOB1.proplblLibro = GetLibro(1).TipodeLibro; //"Libro Diario"
                UcLIBOB2.prophfIdTipoLibro = 2;
                UcLIBOB2.Visible = GetLibro(2).Habilitado;
                UcLIBOB2.proplblLibro = GetLibro(2).TipodeLibro; //"Libro Inventario y Balances"

                //Depende del Check "chkAnexo1", muestra juntos o por separado
                if (!chkAnexo1.Checked)
                {
                    UcLIBOB3.prophfIdTipoLibro = 3;
                    UcLIBOB3.Visible = false;
                    UcLIBOB3.propObligatorio = false;
                    UcLIBOB3.proplblLibro = GetLibro(3).TipodeLibro; //"Libro Actas de Reuniones de Gerentes"
                    UcLIBOB4.prophfIdTipoLibro = 4;
                    UcLIBOB4.Visible = false;
                    UcLIBOB4.propObligatorio = false;
                    UcLIBOB4.proplblLibro = GetLibro(4).TipodeLibro; //"Libro Actas de Reuniones de Socios"
                    UcLIBOB5.prophfIdTipoLibro = 5;
                    UcLIBOB5.Visible = GetLibro(5).Habilitado;
                    UcLIBOB5.propObligatorio = true;
                    UcLIBOB5.proplblLibro = GetLibro(5).TipodeLibro; //"Libro Actas de Reuniones de Gerentes y Socios"
                }
                else
                {
                    UcLIBOB3.prophfIdTipoLibro = 3;
                    UcLIBOB3.Visible = GetLibro(3).Habilitado;
                    UcLIBOB3.propObligatorio = true;
                    UcLIBOB3.proplblLibro = GetLibro(3).TipodeLibro; //"Libro Actas de Reuniones de Gerentes"
                    UcLIBOB4.prophfIdTipoLibro = 4;
                    UcLIBOB4.Visible = GetLibro(4).Habilitado;
                    UcLIBOB4.propObligatorio = true;
                    UcLIBOB4.proplblLibro = GetLibro(4).TipodeLibro; //"Libro Actas de Reuniones de Socios"
                    UcLIBOB5.prophfIdTipoLibro = 5;
                    UcLIBOB5.Visible = false;
                    UcLIBOB5.propObligatorio = false;
                    UcLIBOB5.proplblLibro = GetLibro(5).TipodeLibro; //"Libro Actas de Reuniones de Gerentes y Socios"
                }

                //Libros Opcionales FlagAnexo = 1 (Se muestran) y FlagAnexo = 4 (Quedan ocultos para que no esten disponibles)
                if (FlagAnexo == 1)
                {
                    //Libros Opcionales
                    pnlLibOp.Visible = true;
                    pnlLibOp.Enabled = true;
                    //Titulos de Libros
                    UcLIBOP1.prophfIdTipoLibro = 13;
                    UcLIBOP1.Visible = GetLibro(13).Habilitado; //True
                    UcLIBOP1.propObligatorio = GetLibro(13).Obligatorio; //False             
                    UcLIBOP1.proplblLibro = GetLibro(13).TipodeLibro; //"Libro IVA Ventas"
                    UcLIBOP2.prophfIdTipoLibro = 14;
                    UcLIBOP2.Visible = GetLibro(14).Habilitado; //True
                    UcLIBOP2.propObligatorio = GetLibro(14).Obligatorio; //False                 
                    UcLIBOP2.proplblLibro = GetLibro(14).TipodeLibro; //"Libro IVA Compras"
                }

                #endregion "ANEXO I"
                break;
            //ANEXO II //FORMULARIO DE SOLICITUD RUBRICA SOCIEDADES ACCIONARIAS (SA y SAU)
            case 2: //ANEXO II
            case 5: //ANEXO II (SIN LIBROS OPCIONALES, SOLO OBLIGATORIOS)
                #region "ANEXO II"

                lblTituloAnexo.Text = "Anexo II";
                lblTituloFormulario.Text = "Formulario de Solicitud Rúbrica Sociedades Accionarias (SA Y SAU)";

                //Check Junta Libros Anexo II - Oculta Check Anexo I
                chkAnexo1.Visible = false;
                chkAnexo1.Enabled = false;
                chkAnexo2.Visible = true;
                chkAnexo2.Enabled = true;
                tablachkAnexo1.Attributes.Add("style", "display: none;");

                //Libros Obligatorios
                pnlLibOb.Visible = true;
                pnlLibOb.Enabled = true;
                //Titulos de Libros
                UcLIBOB1.prophfIdTipoLibro = 1;
                UcLIBOB1.Visible = GetLibro(1).Habilitado;
                UcLIBOB1.proplblLibro = GetLibro(1).TipodeLibro; //"Libro Diario"
                UcLIBOB2.prophfIdTipoLibro = 2;
                UcLIBOB2.Visible = GetLibro(2).Habilitado;
                UcLIBOB2.proplblLibro = GetLibro(2).TipodeLibro; //"Libro Inventario y Balances"
                UcLIBOB3.prophfIdTipoLibro = 6;
                UcLIBOB3.Visible = GetLibro(6).Habilitado;
                UcLIBOB3.proplblLibro = GetLibro(6).TipodeLibro; //"Libro Registro de Acciones / Accionistas"
                UcLIBOB4.prophfIdTipoLibro = 7;
                UcLIBOB4.Visible = GetLibro(7).Habilitado;
                UcLIBOB4.proplblLibro = GetLibro(7).TipodeLibro; //"Libro Depósito de Acciones y Asistencia a Asambleas"

                //Depende del Check "chkAnexo2", muestra juntos o por separado
                if (!chkAnexo2.Checked)
                {
                    UcLIBOB5.prophfIdTipoLibro = 10;
                    UcLIBOB5.Visible = true;
                    UcLIBOB5.propObligatorio = true;
                    UcLIBOB5.proplblLibro = GetLibro(10).TipodeLibro; //"Libro Actas de Reuniones de Asambleas"
                    UcLIBOB6.prophfIdTipoLibro = 11;
                    UcLIBOB6.Visible = true;
                    UcLIBOB6.propObligatorio = true;
                    UcLIBOB6.proplblLibro = GetLibro(11).TipodeLibro; //"Libro Actas de Reuniones de Directorio"
                    UcLIBOB7.prophfIdTipoLibro = 12;
                    UcLIBOB7.Visible = false;
                    UcLIBOB7.propObligatorio = false;
                    UcLIBOB7.proplblLibro = GetLibro(12).TipodeLibro; //"Libro Actas de Reuniones de Asambleas y Directorio"
                }
                else
                {
                    UcLIBOB5.prophfIdTipoLibro = 10;
                    UcLIBOB5.Visible = false;
                    UcLIBOB5.propObligatorio = false;
                    UcLIBOB5.proplblLibro = GetLibro(10).TipodeLibro; //"Libro Actas de Reuniones de Asambleas"
                    UcLIBOB6.prophfIdTipoLibro = 11;
                    UcLIBOB6.Visible = false;
                    UcLIBOB6.propObligatorio = false;
                    UcLIBOB6.proplblLibro = GetLibro(11).TipodeLibro; //"Libro Actas de Reuniones de Directorio"
                    UcLIBOB7.prophfIdTipoLibro = 12;
                    UcLIBOB7.Visible = true;
                    UcLIBOB7.propObligatorio = true;
                    UcLIBOB7.proplblLibro = GetLibro(12).TipodeLibro; //"Libro Actas de Reuniones de Asambleas y Directorio"
                }

                //Libros Opcionales FlagAnexo = 2 (Se muestran) y FlagAnexo = 5 (Quedan ocultos para que no esten disponibles)
                if (FlagAnexo == 2)
                {
                    //Libros Opcionales
                    pnlLibOp.Visible = true;
                    pnlLibOp.Enabled = true;
                    //Titulos de Libros
                    UcLIBOP1.prophfIdTipoLibro = 13;
                    UcLIBOP1.Visible = GetLibro(13).Habilitado; //True
                    UcLIBOP1.propObligatorio = GetLibro(13).Obligatorio; //False             
                    UcLIBOP1.proplblLibro = GetLibro(13).TipodeLibro; //"Libro IVA Ventas"
                    UcLIBOP2.prophfIdTipoLibro = 14;
                    UcLIBOP2.Visible = GetLibro(14).Habilitado; //True
                    UcLIBOP2.propObligatorio = GetLibro(14).Obligatorio; //False                 
                    UcLIBOP2.proplblLibro = GetLibro(14).TipodeLibro; //"Libro IVA Compras"
                }

                #endregion "ANEXO II"
                break;
            case 3: //ANEXO III //FORMULARIO DE SOLICITUD RUBRICA ASOCIACION CIVIL
                #region "ANEXO III"

                lblTituloAnexo.Text = "Anexo III";
                lblTituloFormulario.Text = "Formulario de Solicitud Rúbrica Asociación Civil";
                lblTituloFormulario.Attributes.Add("style", "font-size: 17px;");

                //Checks Anexo I y II - Oculta Check Anexo I y II
                chkAnexo1.Visible = false;
                chkAnexo1.Enabled = false;
                chkAnexo2.Visible = false;
                chkAnexo2.Enabled = false;
                tablachkAnexo1.Attributes.Add("style", "display: none;");
                tablachkAnexo2.Attributes.Add("style", "display: none;");

                //Libros Obligatorios
                pnlLibOb.Visible = true;
                pnlLibOb.Enabled = true;
                //Titulos de Libros
                UcLIBOB1.prophfIdTipoLibro = 1;
                UcLIBOB1.Visible = GetLibro(1).Habilitado;
                UcLIBOB1.proplblLibro = GetLibro(1).TipodeLibro; //"Libro Diario"
                UcLIBOB2.prophfIdTipoLibro = 2;
                UcLIBOB2.Visible = GetLibro(2).Habilitado;
                UcLIBOB2.proplblLibro = GetLibro(2).TipodeLibro; //"Libro Inventario y Balances"
                UcLIBOB3.prophfIdTipoLibro = 8;
                UcLIBOB3.Visible = GetLibro(8).Habilitado;
                UcLIBOB3.proplblLibro = GetLibro(8).TipodeLibro; //"Libro Actas"
                UcLIBOB4.prophfIdTipoLibro = 9;
                UcLIBOB4.Visible = GetLibro(9).Habilitado;
                UcLIBOB4.proplblLibro = GetLibro(9).TipodeLibro; //"Libro de Asociados"

                #endregion "ANEXO III"
                break;
        }
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

    private void VerificarNroCorrelativoSociedad(Int64 nroCorrelativo)
    {
        //bool bloquear = false; //Si no cumple con las condiciones entonces se bloquea.

        //FD.Entities.Tramite tramite = (FD.Entities.Tramite)Session["DatosTramite"];

        //if (tramite != null)
        //{
        //    if ((tramite.Id == 321) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 254) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 253) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 251) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 259) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 260) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 261) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 352) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 354) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 350) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 3) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 267) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 268) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 351) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 353) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 349) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //    else if ((tramite.Id == 262) && (nroCorrelativo < 1000000))
        //        bloquear = true;
        //}
        //else
        //    this.lblNroCorrelativoError.Visible = false;

        //if (bloquear)
        //{
        //    this.lblNroCorrelativoError.Visible = true;
        //    this.lblNroCorrelativoError.Text = "Usted seleccionó el trámite " + tramite.Descripcion + " en carácter de urgente, sin embargo, solo podrá tramitarlo por procedimiento normal debido a que el número correlativo de la entidad es menor a 1.000.000. De continuar generando el formulario este se confeccionará como <b>trámite normal.</b>";
        //    ((FD.Entities.Tramite)Session["DatosTramite"]).FlagUrgente = false;
        //}
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
        {
            ValidateJavaScript();
        }

        //Pregunto si los validadores de la pagina encontraron error
        if (Page.IsValid)
        {
            List<Libro> lstLibros = CargarLibros();
            Session.Add("ListaLibros", lstLibros);
            lstLibros = null;

            Response.Redirect("Entidad.aspx");
        }
    }

    private List<Libro> CargarLibros()
    {
        List<Libro> lstLibros = new List<Libro>();

        Libro oLibro_OB1 = null;
        Libro oLibro_OB2 = null;
        Libro oLibro_OB3 = null;
        Libro oLibro_OB4 = null;
        Libro oLibro_OB5 = null;
        Libro oLibro_OB6 = null;
        Libro oLibro_OB7 = null;
        Libro oLibro_OP1 = null;
        Libro oLibro_OP2 = null;

        switch (FlagAnexo)
        {
            case 1:
            case 4:
                //CARGAR Lista de Libros del ANEXO I

                //LIBROS OBLIGATORIOS
                #region "Libros Obligatorios"
                //Carga en un Objeto el Libro Obligatorio 1
                oLibro_OB1 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB1.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB1.proplblLibro, //TipoLibro
                UcLIBOB1.proprblCopiador, //Copiador //"SI" o "NO"
                UcLIBOB1.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB1.proptxtPaginas),//Paginas
                UcLIBOB1.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB1.proptxtFojas)//Fojas
                );

                //Carga en un Objeto el Libro Obligatorio 2
                oLibro_OB2 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB2.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB2.proplblLibro, //TipoLibro
                UcLIBOB2.proprblCopiador, //Copiador
                UcLIBOB2.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB2.proptxtPaginas),//Paginas
                UcLIBOB2.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB2.proptxtFojas)//Fojas
                );

                //Depende del Check "chkAnexo1", van juntos o por separado
                if (chkAnexo1.Checked)
                {//Si se marco el Check "chkAnexo1" "Gerentes y Socios" van Separados

                    //Libro Actas de Reuniones de Gerentes
                    //Carga en un Objeto el Libro Obligatorio 3
                    oLibro_OB3 = new Libro(
                    0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOB3.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOB3.proplblLibro, //TipoLibro
                    UcLIBOB3.proprblCopiador, //Copiador
                    UcLIBOB3.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB3.proptxtPaginas),//Paginas
                    UcLIBOB3.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB3.proptxtFojas)//Fojas
                    );

                    //Libro Actas de Reuniones de Socios
                    //Carga en un Objeto el Libro Obligatorio 4
                    oLibro_OB4 = new Libro(
                    0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOB4.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOB4.proplblLibro, //TipoLibro
                    UcLIBOB4.proprblCopiador, //Copiador
                    UcLIBOB4.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB4.proptxtPaginas),//Paginas
                    UcLIBOB4.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB4.proptxtFojas)//Fojas
                    );
                }
                else
                {//Si no se marco el Check "chkAnexo1" "Gerentes y Socios" van Juntos

                    //Libro Actas de Reuniones de Gerentes y Socios
                    //Carga en un Objeto el Libro Obligatorio 5
                    oLibro_OB5 = new Libro(
                    0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOB5.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOB5.proplblLibro, //TipoLibro
                    UcLIBOB5.proprblCopiador, //Copiador
                    UcLIBOB5.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB5.proptxtPaginas),//Paginas
                    UcLIBOB5.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB5.proptxtFojas)//Fojas
                    );
                }

                #endregion "Libros Obligatorios"

                //LIBROS OPCIONALES
                #region "Libros Opcionales"
                //Si no se cargaron paginas ni fojas no se agrega porque es opcional
                if (UcLIBOP1.proptxtPaginas != "" || UcLIBOP1.proptxtFojas != "")
                {
                    //Carga en un Objeto el Libro Opcional 1
                    oLibro_OP1 = new Libro(
                    1, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOP1.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOP1.proplblLibro, //TipoLibro
                    UcLIBOP1.proprblCopiador, //Copiador
                    UcLIBOP1.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOP1.proptxtPaginas),//Paginas
                    UcLIBOP1.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOP1.proptxtFojas)//Fojas
                    );
                }

                //Si no se cargaron paginas ni fojas no se agrega porque es opcional
                if (UcLIBOP2.proptxtPaginas != "" || UcLIBOP2.proptxtFojas != "")
                {
                    //Carga en un Objeto el Libro Opcional 2
                    oLibro_OP2 = new Libro(
                    1, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOP2.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOP2.proplblLibro, //TipoLibro
                    UcLIBOP2.proprblCopiador, //Copiador
                    UcLIBOP2.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOP2.proptxtPaginas),//Paginas
                    UcLIBOP2.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOP2.proptxtFojas)//Fojas
                    );
                }
                #endregion "Libros Opcionales"

                break;
            case 2:
            case 5:
                //CARGAR Lista de Libros del ANEXO II

                //LIBROS OBLIGATORIOS
                #region "Libros Obligatorios"
                //Carga en un Objeto el Libro Obligatorio 1
                oLibro_OB1 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB1.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB1.proplblLibro, //TipoLibro
                UcLIBOB1.proprblCopiador, //Copiador
                UcLIBOB1.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB1.proptxtPaginas),//Paginas
                UcLIBOB1.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB1.proptxtFojas)//Fojas
                );

                //Carga en un Objeto el Libro Obligatorio 2
                oLibro_OB2 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB2.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB2.proplblLibro, //TipoLibro
                UcLIBOB2.proprblCopiador, //Copiador
                UcLIBOB2.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB2.proptxtPaginas),//Paginas
                UcLIBOB2.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB2.proptxtFojas)//Fojas
                );

                //Carga en un Objeto el Libro Obligatorio 3
                oLibro_OB3 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB3.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB3.proplblLibro, //TipoLibro
                UcLIBOB3.proprblCopiador, //Copiador
                UcLIBOB3.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB3.proptxtPaginas),//Paginas
                UcLIBOB3.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB3.proptxtFojas)//Fojas
                );

                //Carga en un Objeto el Libro Obligatorio 4
                oLibro_OB4 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB4.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB4.proplblLibro, //TipoLibro
                UcLIBOB4.proprblCopiador, //Copiador
                UcLIBOB4.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB4.proptxtPaginas),//Paginas
                UcLIBOB4.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB4.proptxtFojas)//Fojas
                );

                //Depende del Check "chkAnexo2", muestra juntos o por separado
                if (!chkAnexo2.Checked)
                {//Si no se marco el Check "chkAnexo2" "Asambleas y Directorio" van Separados

                    //Libro Actas de Reuniones de Asambleas
                    //Carga en un Objeto el Libro Obligatorio 5
                    oLibro_OB5 = new Libro(
                    0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOB5.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOB5.proplblLibro, //TipoLibro
                    UcLIBOB5.proprblCopiador, //Copiador
                    UcLIBOB5.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB5.proptxtPaginas),//Paginas
                    UcLIBOB5.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB5.proptxtFojas)//Fojas
                    );

                    //Libro Actas de Reuniones de Directorio
                    //Carga en un Objeto el Libro Obligatorio 6
                    oLibro_OB6 = new Libro(
                    0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOB6.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOB6.proplblLibro, //TipoLibro
                    UcLIBOB6.proprblCopiador, //Copiador
                    UcLIBOB6.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB6.proptxtPaginas),//Paginas
                    UcLIBOB6.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB6.proptxtFojas)//Fojas
                    );

                }
                else
                {//Si se marco el Check "chkAnexo2" "Asambleas y Directorio" van Juntos

                    //Libro Actas de Reuniones de Asambleas y Directorio
                    //Carga en un Objeto el Libro Obligatorio 7
                    oLibro_OB7 = new Libro(
                    0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOB7.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOB7.proplblLibro, //TipoLibro
                    UcLIBOB7.proprblCopiador, //Copiador
                    UcLIBOB7.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB7.proptxtPaginas),//Paginas
                    UcLIBOB7.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB7.proptxtFojas)//Fojas
                    );
                }

                #endregion "Libros Obligatorios"

                //LIBROS OPCIONALES
                #region "Libros Opcionales"
                //Si no se cargaron paginas ni fojas no se agrega porque es opcional
                if (UcLIBOP1.proptxtPaginas != "" || UcLIBOP1.proptxtFojas != "")
                {

                    //Carga en un Objeto el Libro Opcional 1
                    oLibro_OP1 = new Libro(
                    1, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOP1.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOP1.proplblLibro, //TipoLibro
                    UcLIBOP1.proprblCopiador, //Copiador
                    UcLIBOP1.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOP1.proptxtPaginas),//Paginas
                    UcLIBOP1.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOP1.proptxtFojas)//Fojas
                    );
                }

                //Si no se cargaron paginas ni fojas no se agrega porque es opcional
                if (UcLIBOP2.proptxtPaginas != "" || UcLIBOP2.proptxtFojas != "")
                {
                    //Carga en un Objeto el Libro Opcional 2
                    oLibro_OP2 = new Libro(
                    1, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                    Convert.ToInt32(UcLIBOP2.prophfIdTipoLibro), //IdTipoLibro
                    UcLIBOP2.proplblLibro, //TipoLibro
                    UcLIBOP2.proprblCopiador, //Copiador
                    UcLIBOP2.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOP2.proptxtPaginas),//Paginas
                    UcLIBOP2.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOP2.proptxtFojas)//Fojas
                    );
                }
                #endregion "Libros Opcionales"

                break;
            case 3:
                //CARGAR Lista de Libros del ANEXO III

                //LIBROS OBLIGATORIOS
                #region "Libros Obligatorios"
                //Carga en un Objeto el Libro Obligatorio 1
                oLibro_OB1 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB1.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB1.proplblLibro, //TipoLibro
                UcLIBOB1.proprblCopiador, //Copiador
                UcLIBOB1.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB1.proptxtPaginas),//Paginas
                UcLIBOB1.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB1.proptxtFojas)//Fojas
                );

                //Carga en un Objeto el Libro Obligatorio 2
                oLibro_OB2 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB2.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB2.proplblLibro, //TipoLibro
                UcLIBOB2.proprblCopiador, //Copiador
                UcLIBOB2.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB2.proptxtPaginas),//Paginas
                UcLIBOB2.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB2.proptxtFojas)//Fojas
                );

                //Carga en un Objeto el Libro Obligatorio 3
                oLibro_OB3 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB3.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB3.proplblLibro, //TipoLibro
                UcLIBOB3.proprblCopiador, //Copiador
                UcLIBOB3.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB3.proptxtPaginas),//Paginas
                UcLIBOB3.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB3.proptxtFojas)//Fojas
                );

                //Carga en un Objeto el Libro Obligatorio 4
                oLibro_OB4 = new Libro(
                0, //LibroObOp//2 estados: 0 = Obligatorio / 1 = Opcional
                Convert.ToInt32(UcLIBOB4.prophfIdTipoLibro), //IdTipoLibro
                UcLIBOB4.proplblLibro, //TipoLibro
                UcLIBOB4.proprblCopiador, //Copiador
                UcLIBOB4.proptxtPaginas == "" ? (long?)null : Convert.ToInt64(UcLIBOB4.proptxtPaginas),//Paginas
                UcLIBOB4.proptxtFojas == "" ? (long?)null : Convert.ToInt64(UcLIBOB4.proptxtFojas)//Fojas
                );
                #endregion "Libros Obligatorios"

                break;
        }

        //Carga el Listado de Libros (Solamente los cargados)
        //LIBROS OBLIGATORIOS
        if (oLibro_OB1 != null) { lstLibros.Add(oLibro_OB1); };
        if (oLibro_OB2 != null) { lstLibros.Add(oLibro_OB2); };
        if (oLibro_OB3 != null) { lstLibros.Add(oLibro_OB3); };
        if (oLibro_OB4 != null) { lstLibros.Add(oLibro_OB4); };
        if (oLibro_OB5 != null) { lstLibros.Add(oLibro_OB5); };
        if (oLibro_OB6 != null) { lstLibros.Add(oLibro_OB6); };
        if (oLibro_OB7 != null) { lstLibros.Add(oLibro_OB7); };

        //LIBROS OPCIONALES
        if (oLibro_OP1 != null) { lstLibros.Add(oLibro_OP1); };
        if (oLibro_OP2 != null) { lstLibros.Add(oLibro_OP2); };

        oLibro_OB1 = null;
        oLibro_OB2 = null;
        oLibro_OB3 = null;
        oLibro_OB4 = null;
        oLibro_OB5 = null;
        oLibro_OB6 = null;
        oLibro_OB7 = null;
        oLibro_OP1 = null;
        oLibro_OP2 = null;

        return lstLibros;
    }

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

    protected void chkAnexo1_CheckedChanged(object sender, EventArgs e)
    {
        CambiarAnexo();
    }

    protected void chkAnexo2_CheckedChanged(object sender, EventArgs e)
    {
        CambiarAnexo();
    }

    protected void btnPrincipal_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    /*--------VALIDACIONES--------*/

    protected void ValidateTextbox(object source, ServerValidateEventArgs args)
    {
        //OcultarValidators();
        string msj = "";
        //Que no sea NULL
        Validation.NullValueValidation(args, out msj);
        //Que solo sean numeros y letras.

        if (args.IsValid)
            Validation.StringNumericValidation(args, out msj);
        CustomValidator cv = (CustomValidator)source;
        cv.ErrorMessage = msj;
    }

    protected void ValidarDatosAdjuntos(object source, ServerValidateEventArgs args)
    {
        Validation.MaxCharactersValidation(args, 300);
    }


}