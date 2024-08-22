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
using System.Text;
using System.Security.Cryptography;

public partial class DigiForm : System.Web.UI.MasterPage
{
    //---------------- COMMON ----------------------------------------------------------------------------------------------------------------

    protected void Page_Load(object sender, EventArgs e)
    {

        //TODO_DESCOMENTAR: Descomentar esto en produccion
        PostBackInitialization();

        if (!Page.IsPostBack)
        {
            NonPostBackInitialization();
        }
    }

    //---------------- BUTTONS ----------------------------------------------------------------------------------------------------------------

    //protected void imgBtnRecomendaciones_Click(object sender, ImageClickEventArgs e)
    //{
    //    //TODO: Revisar porque no abre bien en otra ventana
    //    //Response.Write("<script>window.open('DatosUtiles/Manual_de_usuario.pdf','_blank');</script>");
    //    //Response.Redirect("DatosUtiles/Recomendaciones.pdf");
    //}

    //protected void imgBtnManualdeUsuario_Click(object sender, ImageClickEventArgs e)
    //{
    //    //TODO: Revisar porque no abre bien en otra ventana
    //    //Response.Write("<script>window.open('DatosUtiles/Manual_de_usuario.pdf','_blank');</script>");
    //    //Response.Redirect("DatosUtiles/Manual_de_usuario.pdf");
    //}

    //protected void imgBtnDescargueAcrobat_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("http://get.adobe.com/es/reader/");
    //}

    //protected void imbBtnFromReimputacion_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("Pdf/Formulario de Reimputación.pdf");
    //}

    //---------------- PRIVATE METHODS ----------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Realiza todas las operaciones que se hacen una sola vez y se evitan en cada postback.
    /// </summary>
    private void PostBackInitialization()
    {
        //Objects Instantiation.
        SecurityLayer.SessionSecurity slSessionSecurity = new SecurityLayer.SessionSecurity();
        if (slSessionSecurity.CheckSessionFixation())
            Response.Redirect("Default.aspx");

        slSessionSecurity = null;      

        Page.MaintainScrollPositionOnPostBack = true;
    }

    /// <summary>
    /// Realiza todas las operaciones que se hacen una sola vez y se evitan en cada postback.
    /// </summary>
    private void NonPostBackInitialization()
    {
        //Variable harcodea el numero de version
        string versionfd = "2.1"; //Version - ValidarEntidadconTramite
        //string versionfd = "2.0"; //Version - Modificacion Anexos de Rubrica
        //string versionfd = "1.9"; //Version para Online
        //string versionfd = "1.8 (sjs)"; //Version Intranet

        //Texto debajo del Header
        this.lblBarraFD.Text = "SISTEMA DE FORMULARIOS DIGITALES " + "<ver> v" + versionfd + "</ver>";

        //this.lblDescripcionPie.Text = "Si tiene dificultades para completar el formulario, " +
        //       "o desea enviarnos su comentario o sugerencia, comuníquese con la Inspección General " +
        //       "de Justicia vía mail a " + "<a href='mailto:infoigj@jus.gov.ar?subject=Formulario'> infoigj@jus.gov.ar</a>";
               //Quitado por Telefono Viejo       
               //+ "o telefónicamente al 0800-333-3445 opción 3.";

        //this.lblFecha.Text = DateTime.Today.ToLongDateString();
    }

    protected void BtnRubrica_Click(object sender, EventArgs e)
    {
        Response.Redirect("Documentos.aspx");
    }
}
