using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using FD.Entities;
/// <summary>
/// Retorna los datos que el usuario va ingresando,
/// tomados de variables de session.
/// </summary>
public class SessionData
{
    public SessionData()
    {
    }

    static public Firmante getInfoFirmante()
    {
        Firmante oFirmante;
        return oFirmante = (Firmante)HttpContext.Current.Session["DatosFirmante"];
    }

    static public Tramite getInfoTramite()
    {
        Tramite oTramite;
        return oTramite = (Tramite)HttpContext.Current.Session["DatosTramite"];
    }

    static public Entidad getInfoEntidad()
    {
        Entidad oEntidad;
        return oEntidad = (Entidad)HttpContext.Current.Session["DatosEntidad"];
    }

    static public Denominacion getDenominacion()
    {
        Denominacion oDenominacion;
        return oDenominacion = (Denominacion)HttpContext.Current.Session["Denominacion"];
    }

    static public Operador getOperador()
    {
        Operador oDatosOperador;
        return oDatosOperador = (Operador)HttpContext.Current.Session["DatosOperador"];

    }
  
}
