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

public partial class Default3 : System.Web.UI.Page
{
    public object[] vInfoTram
    {
        get { return ((object[])Session["vInfoTram"]); }
        set { Session.Add("vInfoTram", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ImgBtnDescForm_Click(object sender, ImageClickEventArgs e)
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

        //FD.BusinessLayer.BLEntidad oBLEntidad = new FD.BusinessLayer.BLEntidad();
        //FD.Entities.Tramite oTramiteAux = (FD.Entities.Tramite)Session["DatosTramite"];
        //FD.Entities.Entidad oEntidadAux = (FD.Entities.Entidad)Session["DatosEntidad"];
        //FD.Entities.Entidad oEntidadSelAux = (FD.Entities.Entidad)Session["EntidadSel"];
        //FD.Entities.TipoEntidad oTipoEntidadAux = new FD.Entities.TipoEntidad(Convert.ToInt32(vInfoTram.GetValue(2)));

        //try
        //{
        //    EntidadOk = oBLEntidad.ValidarEntidadconTramite(oTramiteAux, oEntidadAux, oEntidadSelAux, oTipoEntidadAux, out msg);
        //}
        //catch (Exception)
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

        Util.DownloadForm();
        Response.End();
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
