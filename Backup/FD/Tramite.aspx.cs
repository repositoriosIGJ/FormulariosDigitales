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

using FD.Entities;
using FD.BusinessLayer;

public partial class Tramite : System.Web.UI.Page
{
    bool tramEspecial = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        PostBackInitialization();

        if (!Page.IsPostBack)
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
        mapeoBarraInfo();
        this.btnConfirmarTramite.Visible = false;
        //this.PanelPedirAnexos.Visible = false;
        getTiposEntidad();
        this.ddlTipoEntidad.Focus();
    }

    private void mapeoBarraInfo()
    {
        #region DATOS DEL FIRMANTE
        Firmante oFirmante = SessionData.getInfoFirmante();

        Panel pnlInfoFirmante = (Panel)Page.Master.FindControl("pnlInfoFirmante");
        pnlInfoFirmante.Visible = true;

        Label lblDniFirmante = (Label)pnlInfoFirmante.FindControl("lblDniFirmante");
        lblDniFirmante.Text += Server.HtmlEncode(oFirmante.DNI.ToString());
        Label lblNomFirmante = (Label)pnlInfoFirmante.FindControl("lblNomFirmante");
        lblNomFirmante.Text += Server.HtmlEncode(oFirmante.Nombre.ToString());
        Label lblApeFirmante = (Label)pnlInfoFirmante.FindControl("lblApeFirmante");
        lblApeFirmante.Text += Server.HtmlEncode(oFirmante.Apellido.ToString());
        Label lblMailFirmante = (Label)pnlInfoFirmante.FindControl("lblMailFirmante");
        lblMailFirmante.Text += Server.HtmlEncode(oFirmante.Mail.ToString());
        #endregion
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

    private void getTiposEntidad()
    {
        try
        {
            BLTipoEntidad oBLTipoEntidad = new BLTipoEntidad();
            List<TipoEntidad> lTipoEntidad = new List<TipoEntidad>();
            oBLTipoEntidad.RetornarTiposEntidad(lTipoEntidad);

            this.ddlTipoEntidad.DataSource = lTipoEntidad;
            this.ddlTipoEntidad.DataTextField = "descripcion";
            this.ddlTipoEntidad.DataValueField = "id";
            this.ddlTipoEntidad.DataBind();

            oBLTipoEntidad = null;
            lTipoEntidad = null;
            this.ddlTipoEntidad.Items.Insert(0, "Seleccione el tipo de entidad.");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //---------------- BUTTONS -------------------------------------------------------------------------------------------------------------------------

    protected void btnBuscarTramite_Click(object sender, EventArgs e)
    {
        try
        {
            //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
            {
                ValidateJavaScript();
            }

            if (this.ddlTipoEntidad.SelectedIndex != 0)
            {
                this.btnBuscarTramite.Visible = false;
                this.imgHabilitarBusqueda.Visible = true;
                this.ddlTipoEntidad.Enabled = false;

                this.gvTramites.DataSource = ODSTramite;
                this.gvTramites.DataBind();
                this.gvTramites.Visible = true;
                this.lblComentarioTram.Visible = true;
                this.btnConfirmarTramite.Enabled = false;

                this.pnlFiltrarTramites.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgHabilitarBusqueda_Click(object sender, ImageClickEventArgs e)
    {
        this.btnBuscarTramite.Visible = true;
        this.imgHabilitarBusqueda.Visible = false;
        this.ddlTipoEntidad.Enabled = true;
        this.PnlFormulario.Visible = false;
        this.btnConfirmarTramite.Visible = false;
        this.gvTramites.Visible = false;
        this.lblComentarioTram.Visible = false;

        //Deshabilita el Panel Pedir Anexo -- No se usa mas
        //this.PanelPedirAnexos.Visible = false;

        this.pnlFiltrarTramites.Visible = false;
    }

    protected void btnFiltrarTramitexNombre_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.cvTramite.IsValid)
            {
                if (this.txtNombreTramite.Text != string.Empty)
                {
                    FD.BusinessLayer.BLTramite oBLTramite = new BLTramite();
                    List<FD.Entities.Tramite> oListOfTramites = new List<FD.Entities.Tramite>();
                    List<FD.Entities.Tramite> oListOfTramitesIndex = new List<FD.Entities.Tramite>();
                    oListOfTramites = oBLTramite.RetornarTiposTramites(this.ddlTipoEntidad.Text.ToString());
                    List<int> oIndexList = new List<int>();

                    for (int i = 0; i < oListOfTramites.Count; i++)
                    {
                        if (oListOfTramites[i].Descripcion.IndexOf(this.txtNombreTramite.Text.ToString().ToUpper()) == -1)
                            oListOfTramitesIndex.Add(oListOfTramites[i]);
                    }

                    if (oListOfTramites.Count != oListOfTramitesIndex.Count)
                    {
                        for (int i = 0; i < oListOfTramitesIndex.Count; i++)
                        {
                            oListOfTramites.Remove(oListOfTramitesIndex[i]);
                        }
                        this.lblMsgFiltro.Text = string.Empty;
                    }
                    else
                    {
                        this.lblMsgFiltro.Text = "No se ha encontrado ningún nombre de trámite con la palabra ingresada.";
                    }

                    this.lblFiltrado.Visible = true;
                    this.bntMostrarTodosTramites.Visible = true;

                    this.gvTramites.PageSize = oListOfTramites.Count;
                    this.gvTramites.DataSource = oListOfTramites;
                    this.gvTramites.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void bntMostrarTodosTramites_Click(object sender, EventArgs e)
    {
        this.lblFiltrado.Visible = false;
        this.gvTramites.PageSize = 10;
        this.gvTramites.DataSource = ODSTramite;
        this.gvTramites.DataBind();
        this.bntMostrarTodosTramites.Visible = false;
    }

    protected void imgCheck_Click(object sender, EventArgs e)
    {
        this.chkMarcarTramUrg.Checked = false;
        this.lblUrgente.Visible = false;

        List<FD.Entities.Formulario> lFormulario = new List<FD.Entities.Formulario>();
        BLFormulario oBLFormulario = new BLFormulario();

        try
        {
            //Llena el Flag para Mostrar la opcion de Pedir Anexos dependiendo del Tipo Societario y Numero de Tramite
            //TODO_ANEXOS: Quitar Harcodeo y Traer el Flag perdir Anexo de la Base de Datos
            //NULL ó 0 = Sin Anexo de Rubrica
            //1 = Anexo I
            //2 = Anexo II
            //3 = Anexo III
            int FlagAnexo = 0;

            int idTramite = Convert.ToInt32(this.gvTramites.SelectedDataKey["Id"].ToString());
            int idTipoEntidad = Convert.ToInt32(this.ddlTipoEntidad.SelectedValue.ToString());

            oBLFormulario.RetornarTiposFormulario(lFormulario, this.gvTramites.SelectedDataKey["Id"].ToString());

            //Pingo el ID(oculto) y la Descripcion del tramite en el label, para despues
            //poderlo levantar mas facil para generar el constructor para el reporte
            this.lblIdTramite.Text = Server.HtmlDecode(this.gvTramites.SelectedDataKey["Id"].ToString());
            this.lblDescTramite.Text = Server.HtmlDecode(this.gvTramites.Rows[this.gvTramites.SelectedRow.RowIndex].Cells[2].Text);

            CheckBox chkFlagUrg = (CheckBox)this.gvTramites.Rows[this.gvTramites.SelectedRow.RowIndex].FindControl("chkFlagUrgente");
            CheckBox chkFlagNumCorr = (CheckBox)this.gvTramites.Rows[this.gvTramites.SelectedRow.RowIndex].FindControl("chkFlagNumCorr");

            //Habilito dependiendo del ItemTemplate de la Grilla
            this.chkMarcarTramUrg.Visible = chkFlagUrg.Checked;

            this.lblNumFormulario.Text = Server.HtmlDecode(lFormulario[0].Descripcion.ToString());
            Session.Add("PrecioForm", lFormulario[0].Precio);
            this.lblMontoFormulario.Text = Server.HtmlDecode(lFormulario[0].Precio.ToString());
            this.PnlFormulario.Visible = true;

            FlagAnexo = oBLFormulario.RetornarFlagAnexo(idTramite, idTipoEntidad);
            Session.Add("FlagAnexo", FlagAnexo);

            this.btnConfirmarTramite.Enabled = true;
            this.btnConfirmarTramite.Visible = true;

            //Pregunta si para este tramite se pide completar Anexos
            //if (FlagAnexo > 0)
            //{
            //    //Habilita el Panel Pedir Anexo -- No se usa mas
            //    //this.PanelPedirAnexos.Visible = true;
            //    //Deshabilita el boton ConfirmarTramite
            //    this.btnConfirmarTramite.Enabled = false;
            //    this.btnConfirmarTramite.Visible = false;
            //}
            //else
            //{
            //    //Deshabilita el Panel Pedir Anexo -- No se usa mas
            //    //this.PanelPedirAnexos.Visible = false;
            //    //Habilita el boton ConfirmarTramite
            //    this.btnConfirmarTramite.Enabled = true;
            //    this.btnConfirmarTramite.Visible = true;
            //}

            //si el tramite tiene chkFlagNumCorr = False, entonces no tengo uqe habilitar
            //la busqueda, solo dejar que complete el campo de Nombre de entidad.
            if (chkFlagNumCorr.Checked)
            {
                Session.Add("FlagNumCorr", "1");
            }
            else
            {
                Session.Add("FlagNumCorr", "0");
            }

            this.pnlFiltrarTramites.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.gvTramites.DataSource = null;
            this.gvTramites.Visible = false;
            this.lblComentarioTram.Visible = false;
        }
    }

    //---------------- COMMON CONTROLS -------------------------------------------------------------------------------------------------------------------------

    protected void chkMarcarTramUrg_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chkMarcarTramUrg.Checked)
        {
            try
            {
                BLTramite oBLTramite = new BLTramite();
                this.lblMontoFormulario.Text = Convert.ToString(Convert.ToInt32(this.lblMontoFormulario.Text) * oBLTramite.RetornarFactor(Convert.ToInt32(this.gvTramites.SelectedDataKey["Id"].ToString())));
                this.lblUrgente.Visible = this.chkMarcarTramUrg.Checked;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            this.lblMontoFormulario.Text = Session["PrecioForm"].ToString();
            this.lblUrgente.Visible = false;
        }
    }

    protected void gvTramites_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gvTramites.PageIndex = e.NewPageIndex;
            this.gvTramites.DataSource = ODSTramite;
            this.gvTramites.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlTipoEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtNombreTramite.Text = string.Empty;
    }

    protected void cvTramite_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Validation.StringValueValidation(args);
    }


    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
        {
            ValidateJavaScript();
        }

        FD.Entities.Tramite oTramite = new FD.Entities.Tramite(Convert.ToInt32(this.lblIdTramite.Text.Trim()),
                                           this.lblDescTramite.Text.Trim().ToString(),
                                           this.chkMarcarTramUrg.Checked);

        //Entidad.aspx?id=6&cor=1
        Session.Add("DatosTramite", oTramite);

        //el formulario no estaria de mas??? si siempre es uno dependiendo del tramite.
        //en caso afirmativo, borrar y borrar constructor.
        FD.Entities.Formulario oFormulario = new FD.Entities.Formulario(this.lblNumFormulario.Text.ToString(), Convert.ToInt32(this.lblMontoFormulario.Text));
        Session.Add("DatosFormulario", oFormulario);

        #region Datos del Tramite para la seccion de Entidad

        switch (this.lblIdTramite.Text)
        {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "35":
            case "36":
            case "45":
            case "46":
            case "47":
            case "48":
                tramEspecial = true;
                break;
        }

        //Mapeo los datos del tramite para utiliarlo en decisiones necesarias para la entidad
        object[] vInfoTram = new object[6];
        vInfoTram.SetValue(this.lblIdTramite.Text.Trim(), 0); //Posicion 0 = Id Tramite
        vInfoTram.SetValue(Session["FlagNumCorr"], 1); //Posicion 1 = Flag Numero Correlativo
        vInfoTram.SetValue(this.ddlTipoEntidad.SelectedValue, 2); //Posicion 2 = Id Tipo de entidad
        vInfoTram.SetValue(this.ddlTipoEntidad.SelectedItem.Text, 3); //posicion 3 = Descripcion Tramite 
        vInfoTram.SetValue(tramEspecial, 4); //posicion 4 = campo bool, que dice si cambia el label del datos adjuntos por observaciones
        vInfoTram.SetValue(Session["FlagAnexo"], 5); //posicion 5 = FlagAnexo: 1 = Anexo I - 2 = Anexo II - 3 = Anexo III
        Session.Add("vInfoTram", vInfoTram);

        if (Convert.ToInt32(Session["FlagAnexo"]) > 0)
        {
            Response.Redirect("Anexo.aspx");
        }
        else
        {
            Response.Redirect("Entidad.aspx");
        }        

        #endregion
    }

    /*
    protected void btnConfirmarAnexo_Click(object sender, EventArgs e)
    {
        //TODO_INTRANET: ELIMINO Validacion de Javascript para que no moleste en los quioscos
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["ValidarJavascript"]))
        {
            ValidateJavaScript();
        }

        FD.Entities.Tramite oTramite = new FD.Entities.Tramite(Convert.ToInt32(this.lblIdTramite.Text.Trim()),
                                           this.lblDescTramite.Text.Trim().ToString(),
                                           this.chkMarcarTramUrg.Checked);

        //Entidad.aspx?id=6&cor=1
        Session.Add("DatosTramite", oTramite);

        //el formulario no estaria de mas??? si siempre es uno dependiendo del tramite.
        //en caso afirmativo, borrar y borrar constructor.
        FD.Entities.Formulario oFormulario = new FD.Entities.Formulario(this.lblNumFormulario.Text.ToString(), Convert.ToInt32(this.lblMontoFormulario.Text));
        Session.Add("DatosFormulario", oFormulario);

        #region Datos del Tramite para la seccion de Entidad

        switch (this.lblIdTramite.Text)
        {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "35":
            case "36":
            case "45":
            case "46":
            case "47":
            case "48":
                tramEspecial = true;
                break;
        }

        //Mapeo los datos del tramite para utiliarlo en decisiones necesarias para la entidad
        object[] vInfoTram = new object[6];
        vInfoTram.SetValue(this.lblIdTramite.Text.Trim(), 0); //Posicion 0 = Id Tramite
        vInfoTram.SetValue(Session["FlagNumCorr"], 1); //Posicion 1 = Flag Numero Correlativo
        vInfoTram.SetValue(this.ddlTipoEntidad.SelectedValue, 2); //Posicion 2 = Id Tipo de entidad
        vInfoTram.SetValue(this.ddlTipoEntidad.SelectedItem.Text, 3); //posicion 3 = Descripcion Tramite
        vInfoTram.SetValue(tramEspecial, 4); //posicion 4 = campo bool, que dice si cambia el label del datos adjuntos por observaciones
        vInfoTram.SetValue(Session["FlagAnexo"], 5); //posicion 5 = FlagAnexo: 1 = Anexo I - 2 = Anexo II - 3 = Anexo III

        Session.Add("vInfoTram", vInfoTram);

        #endregion

        Response.Redirect("Anexo.aspx");
    }
    */
}