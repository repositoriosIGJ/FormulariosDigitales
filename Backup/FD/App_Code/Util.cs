using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using iTextSharp.text.pdf;
using FD.BusinessLayer;
using FD.Entities;
using System.Threading;
using System.Collections.Generic;
//using WSPagos;
//using FD.BusinessLayer.WsPagosBanelco;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    public Util()
    {

    }
    // TODO_BORRAR: EMPIEZA Codigo para probar sin SSL
    private static bool MyHandler(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    // TODO_BORRAR: TERMINA Codigo para probar sin SSL

    public static string GenerarCodigoReferenciaPagoViejo(Transaccion oTransaccion)
    {
        #region "Sin Gateway de Pagos"
        //try
        //{
        //    //TODO_GATEWAY: COMENTAR HASTA AQUI - Sin GateWay de Pagos

        //    ////////////////////////////////////////////////////
        //    // EMPIEZA LA GENERACION DEL VIEJO CODIGO BANELCO //
        //    ////////////////////////////////////////////////////

        //    #region "Generacion del Codigo Banelco Viejo sin GateWay de Pagos"

        //    string nroFormulario = ((Operador)HttpContext.Current.Session["DatosOperador"]).IdTrans.ToString();
        //    string letraFormulario;
        //    string añoFechaImpresion = oTransaccion.FechaAlta.Year.ToString().Substring(2, 2);

        //    string mesFechaImpresion = string.Empty;
        //    if (oTransaccion.FechaAlta.Month < 10)

        //        mesFechaImpresion = "0" + oTransaccion.FechaAlta.Month.ToString();

        //    else
        //        mesFechaImpresion = oTransaccion.FechaAlta.Month.ToString();

        //    string urgente = string.Empty;

        //    if (oTransaccion.Tramite.FlagUrgente)
        //        urgente = "01";
        //    else
        //        urgente = "00";

        //    #region "Seleccion Formulario"

        //    switch (oTransaccion.Formulario.Descripcion)
        //    {
        //        case "A":
        //            {
        //                letraFormulario = "01";
        //                break;
        //            }
        //        case "B":
        //            {
        //                letraFormulario = "02";
        //                break;
        //            }
        //        case "C":
        //            {
        //                letraFormulario = "03";
        //                break;
        //            }
        //        case "D":
        //            {
        //                letraFormulario = "04";
        //                break;
        //            }
        //        case "E":
        //            {
        //                letraFormulario = "05";
        //                break;
        //            }
        //        case "F":
        //            {
        //                letraFormulario = "06";
        //                break;
        //            }
        //        case "G":
        //            {
        //                letraFormulario = "07";
        //                break;
        //            }
        //        case "H":
        //            {
        //                letraFormulario = "08";
        //                break;
        //            }
        //        case "I":
        //            {
        //                letraFormulario = "09";
        //                break;
        //            }
        //        case "J":
        //            {
        //                letraFormulario = "10";
        //                break;
        //            }
        //        case "K":
        //            {
        //                letraFormulario = "11";
        //                break;
        //            }
        //        case "L":
        //            {
        //                letraFormulario = "12";
        //                break;
        //            }
        //        case "M":
        //            {
        //                letraFormulario = "13";
        //                break;
        //            }
        //        case "N":
        //            {
        //                letraFormulario = "14";
        //                break;
        //            }
        //        case "O":
        //            {
        //                letraFormulario = "15";
        //                break;
        //            }
        //        case "P":
        //            {
        //                letraFormulario = "16";
        //                break;
        //            }
        //        case "Q":
        //            {
        //                letraFormulario = "17";
        //                break;
        //            }
        //        case "R":
        //            {
        //                letraFormulario = "18";
        //                break;
        //            }
        //        case "S":
        //            {
        //                letraFormulario = "19";
        //                break;
        //            }
        //        case "T":
        //            {
        //                letraFormulario = "20";
        //                break;
        //            }
        //        case "U":
        //            {
        //                letraFormulario = "21";
        //                break;
        //            }
        //        case "V":
        //            {
        //                letraFormulario = "22";
        //                break;
        //            }
        //        case "W":
        //            {
        //                letraFormulario = "23";
        //                break;
        //            }
        //        case "X":
        //            {
        //                letraFormulario = "24";
        //                break;
        //            }
        //        case "Y":
        //            {
        //                letraFormulario = "25";
        //                break;
        //            }
        //        case "Z":
        //            {
        //                letraFormulario = "26";
        //                break;
        //            }
        //        default:
        //            {
        //                letraFormulario = String.Empty;
        //                break;
        //            }
        //    }

        //    #endregion

        //    string codigoBanelco = nroFormulario + letraFormulario + añoFechaImpresion + mesFechaImpresion + urgente;
        //    codigoBanelco = codigoBanelco.PadLeft(19, '0');

        //    return codigoBanelco;

        //    #endregion

        //    ////////////////////////////////////////////////////
        //    // TERMINA LA GENERACION DEL VIEJO CODIGO BANELCO //
        //    ////////////////////////////////////////////////////
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        #endregion "Sin Gateway de Pagos"
        return "";
    }

    public static string GenerarCodigoReferenciaPagoNuevo(bool reserva, bool formapago)
    {
        try
        {
            ////////////////////////////////////////////////////
            // EMPIEZA LA GENERACION DEL CODIGO BANELCO NUEVO //
            ////////////////////////////////////////////////////

            #region "Generacion del Codigo Banelco mediante GateWay de Pagos"

            //TODO_GATEWAY: DESCOMENTADO - Habilito el GATEWAY DE PAGO

            string codigoBanelco = "";
            string codigoBanelcoFinal = "";
            string codigoRespuesta = "";
            int flagUrgente;

            /*-- NUEVA MODIF BANELCO 2014 --*/
            decimal MontoFormulario = Convert.ToDecimal(HttpContext.Current.Session["PrecioForm"]);

            //CONECTANDO AL GATEWAY DE PAGOS - 29/10/2012
            Tramite oTramite = (Tramite)HttpContext.Current.Session["DatosTramite"];
            Operador oDatosOperador = (Operador)HttpContext.Current.Session["DatosOperador"];
            Depositante oDepositante = (Depositante)HttpContext.Current.Session["DatosDepositante"];
            Formulario oFormulario = (Formulario)HttpContext.Current.Session["DatosFormulario"];

            //GateWayPagos.GatewayPagoElectronicoServiciosWebClientes WSPagos = new GateWayPagos.GatewayPagoElectronicoServiciosWebClientes();
            //GatewayPagoElectronicoServiciosWebImplService WSPagos = new GatewayPagoElectronicoServiciosWebImplService();

            /****************************************/
            /*-- EMPIEZA NUEVA MODIF BANELCO 2014 --*/
            /****************************************/
            FD.BusinessLayer.WsPagosBanelco.AltaFactura altafactura = new FD.BusinessLayer.WsPagosBanelco.AltaFactura();


            /****************************************/
            /*-- TERMINA NUEVA MODIF BANELCO 2014 --*/
            /****************************************/

            if (oTramite.FlagUrgente == true)
            {
                flagUrgente = 1; //Tildado
            }
            else
            {
                flagUrgente = 0; //Destildado
            }

            //Pregunto por la forma de pago elegida en entidad.aspx
            if (formapago)
            {
                //Si chkFormaPago fue seleccionado paga por banelco a menos que surga algun error y deba pagar caja de igj
                try
                {
                    //Harcodeo
                    //codigoBanelco = "1234567890123456789";
                    //codigoBanelco = WSPagos.getCodigoReferenciaPago("BBI", "IGJ", oTramite.Id.ToString(), flagUrgente.ToString(), oDatosOperador.CodeBar.ToString());
                    //Test
                    //codigoBanelco = WSPagos.getCodigoReferenciaPago("BBI", "IGJ", "900", flagUrgente.ToString(), oDatosOperador.CodeBar.ToString());

                    /****************************************/
                    /*-- EMPIEZA NUEVA MODIF BANELCO 2014 --*/
                    /****************************************/
                    altafactura.CodigoBanco = oDepositante.banco; //ID de Banco = "BROU";
                    //altafactura.CodigoOrganismo = "ECTY"; //Codigo para IGJ
                    altafactura.CodigoOrganismo = "ENTE"; //Codigo para IGJ
                    altafactura.DescripcionPantalla = "FORMULARIO " + oFormulario.Descripcion.ToString(); //"FORMULARIO X"; //Meter la Letra del Formulario
                    altafactura.DescripcionRecibo = "FORMULARIO " + oFormulario.Descripcion.ToString(); //Lo que sale en el ticket
                    altafactura.FechaVencimiento = DateTime.Now.AddDays(10); // Vence en 10 dias porque el ENTE tiene ese limite
                    altafactura.HoraVencimiento = 0;
                    altafactura.Fecha = DateTime.Now;
                    //altafactura.IdentificacionCliente = "";
                    //altafactura.IdentificacionFactura = oDatosOperador.CodeBar; //Envio Vacio para que lo genere el gateway
                    altafactura.NumeroDocumento = oDepositante.dni; //Hay que sacarlo de la pagina de entidad.aspx
                    altafactura.TipoDocumento = 1; //DNI = 1
                    altafactura.Monto = Convert.ToDecimal(oFormulario.Precio); //OJO que puede necesitar ser multiplicado si es urgente

                    // TODO_BORRAR: EMPIEZA Codigo para probar sin SSL
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback += MyHandler;
                    // TODO_BORRAR: TERMINA Codigo para probar sin SSL

                    FD.BusinessLayer.WsPagosBanelco.FacturaServicioClient wsFS = new FD.BusinessLayer.WsPagosBanelco.FacturaServicioClient();
                    FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse wsAF = new FD.BusinessLayer.WsPagosBanelco.AltaFacturaResponse();

                    wsAF = wsFS.CrearFacturaConAdhesion(altafactura);

                    //CodigoRespuesta tiene un código de respuesta Banelco en 2 dígitos o una descripción del error, según el origen del error según la variable CodigoResultado
                    //CodigoResultado 0=OK, 1=Error en gateway, 2=Error en banelco

                    //TODO: Faltaria devolver "wsAF.CodigoRespuesta" para el caso de que el DNI y BANCO no esten asociados con Banelco
                    //Código de respuesta ISO = 62, Descripcion: Usuario inexistente
                    switch (wsAF.CodigoResultado)
                    {
                        case 0:
                            //OK
                            codigoBanelco = wsAF.IdentificacionFactura; // ejm: "0000000000001384235"
                            break;
                        case 1:
                            //Error en gateway
                            codigoBanelco = "-1";
                            break;
                        case 2:
                            //Error en banelco
                            codigoBanelco = "-2";
                            //Código de respuesta ISO = 62, Descripcion: Usuario inexistente
                            //Código de respuesta ISO = 69, Descripcion: ??
                            codigoRespuesta = wsAF.CodigoRespuesta;
                            HttpContext.Current.Session.Add("codigorespuesta", codigoRespuesta);
                            break;
                    }

                    /****************************************/
                    /*-- TERMINA NUEVA MODIF BANELCO 2014 --*/
                    /****************************************/
                }
                catch (Exception e)
                {
                    //Error en gateway
                    codigoBanelco = "-1";
                    string msg = e.Message.ToString().Trim();
                    //Grabo en la Tabla dbo.LOG la advertencia de que no se puedo generar el codigo de referencia de pago con su mensaje de error

                    if (msg.Length > 300)
                        msg = msg.Substring(0, 296) + " ...";

                    Log.GrabarAdvertencia("Error al generar Codigo de Referencia de Pago via WS: " + msg, "Util.GenerarCodigoReferenciaPagoNuevo()", "LOCAL");
                }

                codigoBanelcoFinal = codigoBanelco;
            }
            else
            {
                //Si chkFormaPago no fue seleccionado paga por caja de igj
                codigoBanelcoFinal = "";
            }

            return codigoBanelcoFinal;

            #endregion

            ////////////////////////////////////////////////////
            // TERMINA LA GENERACION DEL CODIGO BANELCO NUEVO //
            ////////////////////////////////////////////////////
        }
        catch (Exception ex)
        {
            throw ex;
        }

        //return "";
    }

    public static string GenerarImagenCodigoBarra(string NumCodBar, string path)
    {
        try
        {
            //byte[]
            //Cracion de Imagen
            System.Drawing.Image imgCodBar = BarcodeLib.Barcode.DoEncode(BarcodeLib.TYPE.CODE39,
                                                                         NumCodBar.ToString(),
                                                                         Color.Black,
                                                                         Color.White);

            //string pathImg = "CodeBars/" + NumCodBar.ToString() + ".jpg";

            //guardo el codigo de barra en una carpeta temporal
            path += "\\" + NumCodBar.ToString() + ".jpg";
            imgCodBar.Save(path);

            return path;
            //lo pongo en un array de byte para guardarlo en la base de datos.
            //byte[] bImgCodBar = Image2Bytes(imgCodBar);
            // return Image2Bytes(imgCodBar);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string RemoveEnter(string texto)
    {
        texto = texto.Replace("\r", " ");
        texto = texto.Replace("\n", "-");
        return texto;
    }

    public static string ScriptEnter(Control control)
    {
        string script = "if  ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13))";
        script += "{ document.getElementById('" + control.ClientID + "').click(); return false; }";
        script += "else { return true; }";
        return script;
    }

    #region "Descarga de Formulario en PDF"
    public static void DownloadForm()
    {
        string uniq = "";
        string fileName = "";
        //string logoName = "imglink.jpg";
        Stream file = null;
        string codrefpago = (string)HttpContext.Current.Session["codrefpago"];
        //string logoPago = HttpContext.Current.Server.MapPath("Template/" + logoName);        

        try
        {
            DSDatosReporte.DTReporteDataTable DSReporte = new DSDatosReporte.DTReporteDataTable();
            Transaccion oTransaccion;
            Operador oDatosOperador;
            Denominacion oDenominacion;
            Depositante oDepositante;

            Tramite oTramite = (Tramite)HttpContext.Current.Session["DatosTramite"];
            //Tramite oTramite = (Tramite)HttpContext.Current.Session["Tramite"];
            //TODO_RESERVA: variable para saber si el tramite es de reserva

            BLTransaccion oBLTransaccion = new BLTransaccion();
            BLTramite oBLTramite = new BLTramite();
            oDenominacion = (Denominacion)HttpContext.Current.Session["Denominacion"];
            string denom = "";
            oDatosOperador = (Operador)HttpContext.Current.Session["DatosOperador"];
            /*-- NUEVA MODIF BANELCO 2014 --*/
            oDepositante = (Depositante)HttpContext.Current.Session["DatosDepositante"];
            Entidad oEntidad = (Entidad)HttpContext.Current.Session["DatosEntidad"];

            if (oDenominacion != null)
            {
                denom = oDenominacion.Denominacion1;
            }
            else
            {
                denom = "";
            }


            //TODO_RESERVA: Retorno si para este id de tramite es del tipo de reserva
            //1 = Reserva -- 0 = No Reserva
            bool FlagReserva;
            FlagReserva = oBLTramite.RetornarTramiteReserva(oTramite.Id);

            //TODO_ANEXO: Cambiar por el FlagAnexo que viene de la base
            int FlagAnexo = 0; //Anexos 1, 2 y 3

            //posicion 5 = FlagAnexo: 1 = Anexo I - 2 = Anexo II - 3 = Anexo III}
            object[] vInfoTram = (object[])HttpContext.Current.Session["vInfoTram"];

            if (vInfoTram[5] != null)
            {
                FlagAnexo = Convert.ToInt32(vInfoTram[5].ToString().Trim());
            }

            //Cargo los datos para el reporte
            oTransaccion = oBLTransaccion.CargarDSReporte(oDatosOperador.IdTrans, oDatosOperador.Reserva, denom);

            // añadimos un plus de aleatoriedad eligiendo una semilla con cierto grado de pseudoaleatoriedad
            Random r = new Random(DateTime.Now.Millisecond);
            uniq = r.Next().ToString();
            r = new Random();
            uniq += r.Next().ToString();
            fileName = "FormListo" + uniq + ".pdf";


            using (file = File.Open(HttpContext.Current.Server.MapPath("Temp/" + fileName), FileMode.Create))
            {
                string pathPDF = "";
                //object[] vInfoTram = (object[])HttpContext.Current.Session["vInfoTram"];

                //TODO_GATEWAY: AGREGO IDS DE TRAMITES HARCODEADOS ADEMAS DEL 6
                //evaluo Id Tramite
                //if (!vInfoTram[0].ToString().Trim().Equals("6"))
                //if (vInfoTram[0].ToString().Trim().Equals("6") || vInfoTram[0].ToString().Trim().Equals("372") || vInfoTram[0].ToString().Trim().Equals("373"))


                //TODO_RESERVA: Pregunto si este tramite es del tipo Reserva = 1 o NO Reserva = 0                
                if (FlagReserva == true)
                {
                    pathPDF = HttpContext.Current.Server.MapPath("Template/FormBaseReserva.pdf");
                }
                else
                {
                    if (FlagAnexo > 0)
                    {
                        pathPDF = HttpContext.Current.Server.MapPath("Template/FormBaseAnexo.pdf");
                    }
                    else
                    {
                        pathPDF = HttpContext.Current.Server.MapPath("Template/FormBase.pdf");
                    }
                }

                PdfReader reader = new PdfReader(pathPDF);
                PdfStamper stamper = new PdfStamper(reader, file, '7');

                ////////////////////////////////////////////////////
                // EMPIEZA LA GENERACION DEL VIEJO CODIGO BANELCO //
                ////////////////////////////////////////////////////

                //TODO_GATEWAY: COMENTAR DESDE AQUI - Sin GateWay de Pagos
                //codrefpago = GenerarCodigoReferenciaPagoViejo(oTransaccion);

                #region "Generacion del Codigo Banelco Viejo"

                //string nroFormulario = ((Operador)HttpContext.Current.Session["DatosOperador"]).IdTrans.ToString();
                //string letraFormulario;
                //string añoFechaImpresion = oTransaccion.FechaAlta.Year.ToString().Substring(2, 2);

                //string mesFechaImpresion = string.Empty;
                //if (oTransaccion.FechaAlta.Month < 10)
                //    mesFechaImpresion = "0" + oTransaccion.FechaAlta.Month.ToString();
                //else
                //    mesFechaImpresion = oTransaccion.FechaAlta.Month.ToString();

                //string urgente = string.Empty;

                //if (oTransaccion.Tramite.FlagUrgente)
                //    urgente = "01";
                //else
                //    urgente = "00";

                #region "Seleccion Formulario"
                //switch (oTransaccion.Formulario.Descripcion)
                //{
                //    case "A":
                //        {
                //            letraFormulario = "01";
                //            break;
                //        }
                //    case "B":
                //        {
                //            letraFormulario = "02";
                //            break;
                //        }
                //    case "C":
                //        {
                //            letraFormulario = "03";
                //            break;
                //        }
                //    case "D":
                //        {
                //            letraFormulario = "04";
                //            break;
                //        }
                //    case "E":
                //        {
                //            letraFormulario = "05";
                //            break;
                //        }
                //    case "F":
                //        {
                //            letraFormulario = "06";
                //            break;
                //        }
                //    case "G":
                //        {
                //            letraFormulario = "07";
                //            break;
                //        }
                //    case "H":
                //        {
                //            letraFormulario = "08";
                //            break;
                //        }
                //    case "I":
                //        {
                //            letraFormulario = "09";
                //            break;
                //        }
                //    case "J":
                //        {
                //            letraFormulario = "10";
                //            break;
                //        }
                //    case "K":
                //        {
                //            letraFormulario = "11";
                //            break;
                //        }
                //    case "L":
                //        {
                //            letraFormulario = "12";
                //            break;
                //        }
                //    case "M":
                //        {
                //            letraFormulario = "13";
                //            break;
                //        }
                //    case "N":
                //        {
                //            letraFormulario = "14";
                //            break;
                //        }
                //    case "O":
                //        {
                //            letraFormulario = "15";
                //            break;
                //        }
                //    case "P":
                //        {
                //            letraFormulario = "16";
                //            break;
                //        }
                //    case "Q":
                //        {
                //            letraFormulario = "17";
                //            break;
                //        }
                //    case "R":
                //        {
                //            letraFormulario = "18";
                //            break;
                //        }
                //    case "S":
                //        {
                //            letraFormulario = "19";
                //            break;
                //        }
                //    case "T":
                //        {
                //            letraFormulario = "20";
                //            break;
                //        }
                //    case "U":
                //        {
                //            letraFormulario = "21";
                //            break;
                //        }
                //    case "V":
                //        {
                //            letraFormulario = "22";
                //            break;
                //        }
                //    case "W":
                //        {
                //            letraFormulario = "23";
                //            break;
                //        }
                //    case "X":
                //        {
                //            letraFormulario = "24";
                //            break;
                //        }
                //    case "Y":
                //        {
                //            letraFormulario = "25";
                //            break;
                //        }
                //    case "Z":
                //        {
                //            letraFormulario = "26";
                //            break;
                //        }
                //    default:
                //        {
                //            letraFormulario = String.Empty;
                //            break;
                //        }
                //}

                #endregion

                //string codigoBanelco = nroFormulario + letraFormulario + añoFechaImpresion + mesFechaImpresion + urgente;
                //codigoBanelco = codigoBanelco.PadLeft(19, '0');

                //TODO_GATEWAY: COMENTAR HASTA AQUI - Sin GateWay de Pagos

                #endregion

                ////////////////////////////////////////////////////
                // TERMINA LA GENERACION DEL VIEJO CODIGO BANELCO //
                ////////////////////////////////////////////////////

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    //Para las 2 primeras paginas
                    if (i == 1 || i == 2)
                    {
                        //FORMULARIO
                        stamper.AcroFields.SetField("txtLetraFormulario" + i.ToString(), oTransaccion.Formulario.Descripcion.ToString());
                        stamper.AcroFields.SetField("txtValorTimbrado" + i.ToString(), " " + oTransaccion.Formulario.Precio.ToString());
                        stamper.AcroFields.SetField("txtFecha" + i.ToString(), " " + oTransaccion.FechaAlta.ToShortDateString());

                        //FIRMANTE
                        stamper.AcroFields.SetField("txtNombreFirmante" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Firmante.Nombre.ToString()));
                        stamper.AcroFields.SetField("txtApellidoFirmante" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Firmante.Apellido.ToString()));
                        stamper.AcroFields.SetField("txtDNIFirmante" + i.ToString(), " " + oTransaccion.Firmante.DNI.ToString());
                        stamper.AcroFields.SetField("txtMailFirmante" + i.ToString(), " " + oTransaccion.Firmante.Mail.ToString());
                        stamper.AcroFields.SetField("txtTelFirmante" + i.ToString(), " " + oTransaccion.Firmante.Telefono.ToString());
                        //TODO: Empieza nuevos datos del firmate 2017
                        string strTipoDOC = "";
                        switch (oTransaccion.Firmante.TipoDOC.ToString())
                        {
                            case "DNI":
                                strTipoDOC = "Documento Nacional de Identidad";
                                break;
                            case "CDI":
                                strTipoDOC = "Cedula de Identidad";
                                break;
                            case "LIC":
                                strTipoDOC = "Libreta Civica";
                                break;
                            case "LIE":
                                strTipoDOC = "Libreta de Enrolamiento";
                                break;
                            case "PAS":
                                strTipoDOC = "Pasaporte";
                                break;
                            default:
                                strTipoDOC = "";
                                break;
                        }
                        stamper.AcroFields.SetField("txtTipoDocFirmante" + i.ToString(), " " + strTipoDOC);
                        string strCaracter = "";
                        string strAutorizado = "-----";
                        switch (oTransaccion.Firmante.Caracter.ToString())
                        {
                            case "RL":
                                strCaracter = "Representante Legal";
                                break;
                            case "PD":
                                strCaracter = "Dictaminante"; //Profesional Dictaminante
                                break;
                            case "AT":
                                strCaracter = "Autorizado a Tramitar";
                                strAutorizado = oTransaccion.Firmante.Autorizado.ToString();
                                break;
                            default:
                                strCaracter = "";
                                strAutorizado = "-----";
                                break;
                        }
                        stamper.AcroFields.SetField("txtCaracterFirmante" + i.ToString(), " " + strCaracter);
                        stamper.AcroFields.SetField("txtAutorizadoFirmante" + i.ToString(), " " + strAutorizado);

                        //stamper.AcroFields.SetField("txtCodBanelco" + i.ToString(), codigoBanelco);
                        /*-- NUEVA MODIF BANELCO 2014 --*/
                        //stamper.AcroFields.SetField("imgLogoPago" + i.ToString(), logoName);
                        if (codrefpago != "")
                        {
                            //TODO_GATEWAY: Cambio por el codigo de referencia de pago cargado desde el webservice
                            stamper.AcroFields.SetField("txtCodBanelco" + i.ToString(), codrefpago);
                            stamper.AcroFields.SetField("txtDNIBanelco" + i.ToString(), oDepositante.dni.ToString());
                            stamper.AcroFields.SetField("txtBancoBanelco" + i.ToString(), oDepositante.bancodesc.ToString());
                        }
                        else
                        {
                            stamper.AcroFields.SetField("txtCodBanelco" + i.ToString(), " ------");
                            stamper.AcroFields.SetField("txtDNIBanelco" + i.ToString(), " ------");
                            stamper.AcroFields.SetField("txtBancoBanelco" + i.ToString(), " ------");
                        }

                        //TRAMITE
                        stamper.AcroFields.SetField("txtTramDesc" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Tramite.Descripcion.ToString()));

                        //TODO_GATEWAY: AGREGO IDS DE TRAMITES HARCODEADOS ADEMAS DEL 6
                        //RESERVA DE DENOMINACION
                        //if (!((string)vInfoTram.GetValue(0) == "6") && !((string)vInfoTram.GetValue(0) == "372") && !((string)vInfoTram.GetValue(0) == "373"))
                        if (FlagReserva == false)
                        {
                            //ENTIDAD
                            if (oTransaccion.Entidad.NroCorrelativo != 0)
                            {
                                stamper.AcroFields.SetField("txtNroCorrelativo" + i.ToString(), " " + oTransaccion.Entidad.NroCorrelativo.ToString());
                            }
                            else
                            {
                                stamper.AcroFields.SetField("txtNroCorrelativo" + i.ToString(), "");
                            }
                            stamper.AcroFields.SetField("txtNomEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.Nombre.ToString()));
                            stamper.AcroFields.SetField("txtTipoEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.TipoSociedad.Descripcion.ToString()));

                            //Imprimir el Cuit pero hay que preguntar si es null y 0 devuelvo vacio
                            stamper.AcroFields.SetField("txtCUIT" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.Cuit.ToString()));

                            //TRAMITE URGENTE
                            if (oTransaccion.Tramite.FlagUrgente)
                            {
                                stamper.AcroFields.SetField("txtUrgente" + i.ToString(), " SI");
                            }
                            else
                            {
                                stamper.AcroFields.SetField("txtUrgente" + i.ToString(), " NO");
                            }

                            //DATOS ADJUNTOS
                            stamper.AcroFields.SetField("txtMotivo" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Motivo.ToString()));
                        }
                        else
                        {
                            Denominacion oDenominacionRes = (FD.Entities.Denominacion)HttpContext.Current.Session["Denominacion"];

                            //OPCIONES DE DENOMINACION
                            stamper.AcroFields.SetField("txtDenomOp1" + i.ToString(), oDenominacionRes.Denominacion1.ToString());
                            stamper.AcroFields.SetField("txtDenomOp2" + i.ToString(), oDenominacionRes.Denominacion2.ToString());
                            stamper.AcroFields.SetField("txtDenomOp3" + i.ToString(), oDenominacionRes.Denominacion3.ToString());

                            string strpersoneria;

                            switch (oDenominacionRes.Tipo)
                            {
                                case 0:
                                    strpersoneria = "Personas Físicas";
                                    break;
                                case 1:
                                    strpersoneria = "Personas Jurídicas";
                                    break;
                                case 2:
                                    strpersoneria = "Personas Físicas y/o Jurídicas";
                                    break;
                                default:
                                    strpersoneria = "";
                                    break;
                            }

                            stamper.AcroFields.SetField("txtPersoneria" + i.ToString(), strpersoneria);

                            //TIPO DE ENTIDAD
                            stamper.AcroFields.SetField("txtTipoEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode((string)vInfoTram.GetValue(3)));
                            //stamper.AcroFields.SetField("txtTipoEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode(oGrupoEntidad.Descripcion.ToString()));
                            //TODO: Agrego la Descripción del Grupo de Entidad para imprimir en el Formulario
                            //stamper.AcroFields.SetField("txtGrupoEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode(oGrupoEntidad.DescGrupoEntidad.ToString()));


                            //DATOS CONSTITUYENTE
                            switch (oDenominacionRes.Tipo)
                            {
                                case 0:
                                    stamper.AcroFields.SetField("txtNyAConst1" + i.ToString(), oDenominacionRes.Constituyente[0].NombreYApellido.ToString());
                                    stamper.AcroFields.SetField("txtNyAConst2" + i.ToString(), oDenominacionRes.Constituyente[1].NombreYApellido.ToString());
                                    stamper.AcroFields.SetField("txtDNIConst1" + i.ToString(), oDenominacionRes.Constituyente[0].DNI.ToString());
                                    stamper.AcroFields.SetField("txtDNIConst2" + i.ToString(), oDenominacionRes.Constituyente[1].DNI.ToString());
                                    break;
                                case 1:
                                    stamper.AcroFields.SetField("txtDenomCosnt1" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion1.ToString());
                                    stamper.AcroFields.SetField("txtDenomCosnt2" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion2.ToString());
                                    break;
                                case 2:
                                    stamper.AcroFields.SetField("txtDNIConst1" + i.ToString(), oDenominacionRes.Constituyente[0].DNI.ToString());
                                    stamper.AcroFields.SetField("txtNyAConst1" + i.ToString(), oDenominacionRes.Constituyente[0].NombreYApellido.ToString());
                                    stamper.AcroFields.SetField("txtDenomCosnt2" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion1.ToString());
                                    break;
                            }
                        }
                    }

                    //Para las 2 ultimas paginas
                    if (i == 3 || i == 4)
                    {
                        #region "Inserta los Campos del Anexo de Rubrica"
                        if (FlagAnexo > 0)
                        {
                            //Letra del Formulario en Original y Duplicado del Libro de Rubricas
                            stamper.AcroFields.SetField("txtLetraFormulario" + i.ToString(), oTransaccion.Formulario.Descripcion.ToString());

                            //Numero de Anexo y Titulo
                            string Anexo = "";
                            string TituloAnexo = "";

                            switch (FlagAnexo)
                            {
                                case 1:
                                case 4:
                                    Anexo = "ANEXO I";
                                    TituloAnexo = "FORMULARIO DE SOLICITUD RUBRICA SOCIEDADES NO ACCIONARIAS (SRL)";
                                    break;
                                case 2:
                                case 5:
                                    Anexo = "ANEXO II";
                                    TituloAnexo = "FORMULARIO DE SOLICITUD RUBRICA SOCIEDADES ACCIONARIAS (SA y SAU)";
                                    break;
                                case 3:
                                    Anexo = "ANEXO III";
                                    TituloAnexo = "FORMULARIO DE SOLICITUD RUBRICA ASOCIACION CIVIL";
                                    break;
                            }


                            stamper.AcroFields.SetField("txtAnexo_" + i.ToString(), Anexo);
                            stamper.AcroFields.SetField("txtTituloAnexo_" + i.ToString(), TituloAnexo);


                            //Libros
                            List<Libro> lstLibros = new List<Libro>();
                            lstLibros = (List<Libro>)HttpContext.Current.Session["ListaLibros"];

                            //Indices incrementales obligatorio y opcional
                            int nroOB = 0;
                            int nroOP = 0;

                            //RECORRE LOS LIBROS DE RUBRICA
                            foreach (Libro lib in lstLibros)
                            {
                                //Pregunta si es un libro obligatorio 
                                //2 estados: 0 = Obligatorio / 1 = Opcional
                                if (lib.LibroObOp == 0)
                                {
                                    ++nroOB;

                                    stamper.AcroFields.SetField("TipoLibro_OB" + nroOB.ToString() + "_" + i.ToString(), lib.TipoLibro.ToString().ToUpper());
                                    stamper.AcroFields.SetField("txtCopiador_OB" + nroOB.ToString() + "_" + i.ToString(), lib.Copiador.ToString());

                                    if (lib.Paginas != null)
                                    {
                                        stamper.AcroFields.SetField("lblPgFj_OB" + nroOB.ToString() + "_" + i.ToString(), "CANTIDAD DE PAGINAS");
                                        stamper.AcroFields.SetField("txtCantidad_OB" + nroOB.ToString() + "_" + i.ToString(), lib.Paginas.ToString());
                                    }
                                    else if (lib.Fojas != null)
                                    {
                                        stamper.AcroFields.SetField("lblPgFj_OB" + nroOB.ToString() + "_" + i.ToString(), "CANTIDAD DE FOJAS");
                                        stamper.AcroFields.SetField("txtCantidad_OB" + nroOB.ToString() + "_" + i.ToString(), lib.Fojas.ToString());
                                    }
                                }
                                else if (lib.LibroObOp == 1)
                                {
                                    ++nroOP;

                                    stamper.AcroFields.SetField("TipoLibro_OP" + nroOP.ToString() + "_" + i.ToString(), lib.TipoLibro.ToString().ToUpper());
                                    stamper.AcroFields.SetField("txtCopiador_OP" + nroOP.ToString() + "_" + i.ToString(), lib.Copiador.ToString());

                                    if (lib.Paginas != null)
                                    {
                                        stamper.AcroFields.SetField("lblPgFj_OP" + nroOP.ToString() + "_" + i.ToString(), "CANTIDAD DE PAGINAS");
                                        stamper.AcroFields.SetField("txtCantidad_OP" + nroOP.ToString() + "_" + i.ToString(), lib.Paginas.ToString());
                                    }
                                    else if (lib.Fojas != null)
                                    {
                                        stamper.AcroFields.SetField("lblPgFj_OP" + nroOP.ToString() + "_" + i.ToString(), "CANTIDAD DE FOJAS");
                                        stamper.AcroFields.SetField("txtCantidad_OP" + nroOP.ToString() + "_" + i.ToString(), lib.Fojas.ToString());
                                    }
                                }
                            }

                            //Elimina los campos y titulos de los Libros Obligatorios no completados o no pedidos
                            if (nroOB > 0)
                            {
                                //Titulos de Libros Obligatorios
                                stamper.AcroFields.SetField("LibroOB_" + i.ToString(), "LIBROS OBLIGATORIOS");

                                //Elimina los campos de los Libros Obligatorios no completados
                                if (nroOB < 6)
                                {
                                    for (int j = nroOB + 1; j <= 6; j++)
                                    {
                                        stamper.AcroFields.SetField("TipoLibro_OB" + j.ToString() + "_" + i.ToString(), "No Solicitado"); //"--------"
                                        stamper.AcroFields.SetField("txtCopiador_OB" + j.ToString() + "_" + i.ToString(), "--");
                                        stamper.AcroFields.SetField("lblPgFj_OB" + j.ToString() + "_" + i.ToString(), "");
                                        stamper.AcroFields.SetField("txtCantidad_OB" + j.ToString() + "_" + i.ToString(), "--");
                                        
                                        //stamper.AcroFields.SetFieldProperty("TipoLibro_OB" + j.ToString() + "_" + i.ToString(), "setfflags", PdfFormField.FLAGS_HIDDEN, null);
                                        //stamper.AcroFields.SetFieldProperty("txtCopiador_OB" + j.ToString() + "_" + i.ToString(), "setfflags", PdfAnnotation.FLAGS_HIDDEN, null);
                                        //stamper.AcroFields.SetFieldProperty("lblPgFj_OB" + j.ToString() + "_" + i.ToString(), "flags", PdfFormField.FLAGS_HIDDEN, null);
                                        //stamper.AcroFields.SetFieldProperty("txtCantidad_OB" + j.ToString() + "_" + i.ToString(), "setfflags", PdfAnnotation.FLAGS_INVISIBLE, null);
                                        
                                        //stamper.AcroFields.RemoveField("TipoLibro_OB" + j.ToString() + "_" + i.ToString(), i);
                                        //stamper.AcroFields.RemoveField("txtCopiador_OB" + j.ToString() + "_" + i.ToString(), i);
                                        //stamper.AcroFields.RemoveField("lblPgFj_OB" + j.ToString() + "_" + i.ToString(), i);
                                        //stamper.AcroFields.RemoveField("txtCantidad_OB" + j.ToString() + "_" + i.ToString(), i);
                                    }
                                }
                            }
                            else
                            {
                                //Elimina Titulos de Libros Obligatorios si no hay ninguno
                                stamper.AcroFields.SetField("LibroOB_" + i.ToString(), "--------");  
                                //stamper.AcroFields.RemoveField("LibroOB_" + i.ToString(), i);
                            }

                            //Elimina los campos y titulos de los Libros Opcionales no completados o no pedidos
                            if (nroOP > 0)
                            {
                                //Titulos de Libros Opcionales
                                stamper.AcroFields.SetField("LibroOP_" + i.ToString(), "LIBROS OPCIONALES");

                                //Elimina los campos de los Libros Opcionales no completados
                                if (nroOP < 2)
                                {
                                    for (int j = nroOP + 1; j <= 2; j++)
                                    {
                                        stamper.AcroFields.SetField("TipoLibro_OP" + j.ToString() + "_" + i.ToString(), "No Solicitado");
                                        stamper.AcroFields.SetField("txtCopiador_OP" + j.ToString() + "_" + i.ToString(), "--");
                                        stamper.AcroFields.SetField("lblPgFj_OP" + j.ToString() + "_" + i.ToString(), "");
                                        stamper.AcroFields.SetField("txtCantidad_OP" + j.ToString() + "_" + i.ToString(), "--");

                                        //stamper.AcroFields.RemoveField("TipoLibro_OP" + j.ToString() + "_" + i.ToString(), i);
                                        //stamper.AcroFields.RemoveField("txtCopiador_OP" + j.ToString() + "_" + i.ToString(), i);
                                        //stamper.AcroFields.RemoveField("lblPgFj_OP" + j.ToString() + "_" + i.ToString(), i);
                                        //stamper.AcroFields.RemoveField("txtCantidad_OP" + j.ToString() + "_" + i.ToString(), i);
                                    }
                                }
                            }
                            else
                            {
                                //Elimina Titulos de Libros Opcionales si no hay ninguno
                                stamper.AcroFields.SetField("LibroOP_" + i.ToString(), "No Solicitado");                                
                                //stamper.AcroFields.RemoveField("LibroOP_" + i.ToString(), i);
                            }
                        }
                    }
                    #endregion "Inserta los Campos del Anexo de Rubrica"

                    #region Watermark

                    //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("Template/watermark.png"));
                    //img.SetAbsolutePosition(10, 10);
                    //img.Rotation = 45;
                    ////PARA MAS DE UNA PAGINA
                    ////for (int i =1; i <= numberOfPages; i++)  
                    ////{  
                    ////    waterMarkContent = pdfStamper.GetUnderContent(i);  
                    ////    waterMarkContent.AddImage(image);  
                    ////}


                    //PdfContentByte waterMark = stamper.GetUnderContent(i);
                    //waterMark.AddImage(img);

                    #endregion

                    #region Codigo de Barras

                    //CODIGO DE BARRAS
                    PdfContentByte overContent = stamper.GetOverContent(i);
                    Barcode39 codebar = new Barcode39();

                    codebar.Code = oDatosOperador.CodeBar;
                    codebar.StartStopText = false;

                    // Get barcode image placeholder
                    float[] barcodeArea = stamper.AcroFields.GetFieldPositions("codBarras");
                    iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(Convert.ToInt32(barcodeArea[1]), Convert.ToInt32(barcodeArea[2]), Convert.ToInt32(barcodeArea[3]), Convert.ToInt32(barcodeArea[4]));
                    codebar.Size = 10;
                    codebar.BarHeight = codebar.Size * 5f;


                    iTextSharp.text.Image imageBarCode = codebar.CreateImageWithBarcode(overContent, null, null);
                    imageBarCode.ScaleToFit(rect.Width, rect.Height);
                    imageBarCode.SetAbsolutePosition(barcodeArea[1] + (rect.Width - imageBarCode.ScaledWidth) / 2, barcodeArea[2] + (rect.Height - imageBarCode.ScaledHeight) / 2);
                    // Add barcode image
                    overContent.AddImage(imageBarCode);

                    #endregion
                }
                //#endregion

                stamper.SetFullCompression();
                stamper.FormFlattening = true;
                stamper.Close();

                #region Descarga de Formulario

                FileStream oFs;
                string strPath = HttpContext.Current.Server.MapPath("Temp/" + fileName);

                oFs = File.Open(strPath, FileMode.Open);
                byte[] bytBytes = new byte[oFs.Length];

                oFs.Read(bytBytes, 0, Convert.ToInt32(oFs.Length));
                oFs.Close();

                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();

                HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=FormularioIGJ.pdf");
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.BinaryWrite(bytBytes);

                HttpContext.Current.Response.Flush();

                File.Delete(strPath);

                #endregion
            }
        }
        catch (Exception ex)
        {
            throw new Exception("ERROR [" + uniq + "]" + ex.Message);
        }
        finally
        {
            if (file != null)
            {
                try
                {
                    file.Close();
                }
                catch
                {
                    Log.GrabarAdvertencia("ERROR al cerrar el archivo de descarga", "Util.DownloadForm()", "LOCAL");
                }
            }
            try
            {
                //if (File.Exists(HttpContext.Current.Server.MapPath("Template/FormBase.pdf")))
                //    File.Delete(HttpContext.Current.Server.MapPath("Template/FormBase.pdf"));                
            }
            catch
            {
                Log.GrabarAdvertencia("ERROR al borrar el archivo descargado", "Util.DownloadForm()", "LOCAL");
            }
        }
    }
    #endregion "Descarga de Formulario en PDF"

    //TODO_NOCUIT: No se usa el pedido de CUIT
    #region "Descarga de Formulario en PDF para pedido de CUIT"
    //public static void DownloadForm()
    //{
    //    string uniq = "";
    //    string fileName = "";
    //    //string logoName = "imglink.jpg";
    //    Stream file = null;
    //    string codrefpago = (string)HttpContext.Current.Session["codrefpago"];
    //    //string logoPago = HttpContext.Current.Server.MapPath("Template/" + logoName);        

    //    try
    //    {
    //        DSDatosReporte.DTReporteDataTable DSReporte = new DSDatosReporte.DTReporteDataTable();
    //        Transaccion oTransaccion;
    //        Operador oDatosOperador;
    //        Denominacion oDenominacion;
    //        Depositante oDepositante;

    //        Tramite oTramite = (Tramite)HttpContext.Current.Session["DatosTramite"];
    //        //Tramite oTramite = (Tramite)HttpContext.Current.Session["Tramite"];
    //        //TODO_RESERVA: variable para saber si el tramite es de reserva

    //        BLTransaccion oBLTransaccion = new BLTransaccion();
    //        BLTramite oBLTramite = new BLTramite();
    //        oDenominacion = (Denominacion)HttpContext.Current.Session["Denominacion"];
    //        string denom = "";
    //        oDatosOperador = (Operador)HttpContext.Current.Session["DatosOperador"];
    //        /*-- NUEVA MODIF BANELCO 2014 --*/
    //        oDepositante = (Depositante)HttpContext.Current.Session["DatosDepositante"];
    //        Entidad oEntidad = (Entidad)HttpContext.Current.Session["DatosEntidad"];

    //        if (oDenominacion != null)
    //        {
    //            denom = oDenominacion.Denominacion1;
    //        }
    //        else
    //        {
    //            denom = "";
    //        }


    //        //TODO_RESERVA: Retorno si para este id de tramite es del tipo de reserva
    //        //1 = Reserva -- 0 = No Reserva
    //        bool FlagReserva;
    //        FlagReserva = oBLTramite.RetornarTramiteReserva(oTramite.Id);

    //        //TODO_CUIT: Lleno los campos del CUIT para cargar los datos para el reporte dependiendo si es reserva o no
    //        bool flagCUIT = false;
    //        int tipoOpeCuit = 0;

    //        //Pregunto si no es de reserva
    //        if (FlagReserva == false)
    //        {
    //            //Traigo de la session el dato de si se pidio CUIT o no
    //            if (HttpContext.Current.Session["FlagCUIT"] != null)
    //            {
    //                flagCUIT = (bool)HttpContext.Current.Session["FlagCUIT"];
    //            }
    //            else
    //            {
    //                flagCUIT = false;
    //            }

    //            tipoOpeCuit = oEntidad.IdTipoOpeCuit;
    //        }            

    //        //Cargo los datos para el reporte
    //        //oTransaccion = oBLTransaccion.CargarDSReporte(oDatosOperador.IdTrans, oDatosOperador.Reserva, denom);
    //        //TODO_NOCUIT: No se usa el pedido de CUIT
    //        oTransaccion = oBLTransaccion.CargarDSReporte(oDatosOperador.IdTrans, oDatosOperador.Reserva, denom, flagCUIT, tipoOpeCuit);

    //        // añadimos un plus de aleatoriedad eligiendo una semilla con cierto grado de pseudoaleatoriedad
    //        Random r = new Random(DateTime.Now.Millisecond);
    //        uniq = r.Next().ToString();
    //        r = new Random();
    //        uniq += r.Next().ToString();
    //        fileName = "FormListo" + uniq + ".pdf";


    //        using (file = File.Open(HttpContext.Current.Server.MapPath("Temp/" + fileName), FileMode.Create))
    //        {
    //            string pathPDF = "";
    //            object[] vInfoTram = (object[])HttpContext.Current.Session["vInfoTram"];

    //            //TODO_GATEWAY: AGREGO IDS DE TRAMITES HARCODEADOS ADEMAS DEL 6
    //            //evaluo Id Tramite
    //            //if (!vInfoTram[0].ToString().Trim().Equals("6"))
    //            //if (vInfoTram[0].ToString().Trim().Equals("6") || vInfoTram[0].ToString().Trim().Equals("372") || vInfoTram[0].ToString().Trim().Equals("373"))


    //            //TODO_RESERVA: Pregunto si este tramite es del tipo Reserva = 1 o NO Reserva = 0                
    //            if (FlagReserva == true)
    //            {
    //                pathPDF = HttpContext.Current.Server.MapPath("Template/FormBaseReserva.pdf");
    //            }
    //            else
    //            {
    //                pathPDF = HttpContext.Current.Server.MapPath("Template/FormBase.pdf");
    //                //pathPDF = HttpContext.Current.Server.MapPath("Template/FormBaseCUIT.pdf");
    //            }

    //            //TODO_CUIT: Pregunto si el formulario se realizo con alta o modificacion del CUIT
    //            //if (tipoOpeCuit == 1 || tipoOpeCuit == 2)
    //            //{
    //            //    pathPDF = HttpContext.Current.Server.MapPath("Template/FormBaseCUIT.pdf");
    //            //}


    //            PdfReader reader = new PdfReader(pathPDF);
    //            PdfStamper stamper = new PdfStamper(reader, file, '7');

    //            ////////////////////////////////////////////////////
    //            // EMPIEZA LA GENERACION DEL VIEJO CODIGO BANELCO //
    //            ////////////////////////////////////////////////////

    //            //TODO_GATEWAY: COMENTAR DESDE AQUI - Sin GateWay de Pagos
    //            //codrefpago = GenerarCodigoReferenciaPagoViejo(oTransaccion);

    //            #region "Generacion del Codigo Banelco Viejo"

    //            //string nroFormulario = ((Operador)HttpContext.Current.Session["DatosOperador"]).IdTrans.ToString();
    //            //string letraFormulario;
    //            //string añoFechaImpresion = oTransaccion.FechaAlta.Year.ToString().Substring(2, 2);

    //            //string mesFechaImpresion = string.Empty;
    //            //if (oTransaccion.FechaAlta.Month < 10)
    //            //    mesFechaImpresion = "0" + oTransaccion.FechaAlta.Month.ToString();
    //            //else
    //            //    mesFechaImpresion = oTransaccion.FechaAlta.Month.ToString();

    //            //string urgente = string.Empty;

    //            //if (oTransaccion.Tramite.FlagUrgente)
    //            //    urgente = "01";
    //            //else
    //            //    urgente = "00";

    //            #region "Seleccion Formulario"
    //            //switch (oTransaccion.Formulario.Descripcion)
    //            //{
    //            //    case "A":
    //            //        {
    //            //            letraFormulario = "01";
    //            //            break;
    //            //        }
    //            //    case "B":
    //            //        {
    //            //            letraFormulario = "02";
    //            //            break;
    //            //        }
    //            //    case "C":
    //            //        {
    //            //            letraFormulario = "03";
    //            //            break;
    //            //        }
    //            //    case "D":
    //            //        {
    //            //            letraFormulario = "04";
    //            //            break;
    //            //        }
    //            //    case "E":
    //            //        {
    //            //            letraFormulario = "05";
    //            //            break;
    //            //        }
    //            //    case "F":
    //            //        {
    //            //            letraFormulario = "06";
    //            //            break;
    //            //        }
    //            //    case "G":
    //            //        {
    //            //            letraFormulario = "07";
    //            //            break;
    //            //        }
    //            //    case "H":
    //            //        {
    //            //            letraFormulario = "08";
    //            //            break;
    //            //        }
    //            //    case "I":
    //            //        {
    //            //            letraFormulario = "09";
    //            //            break;
    //            //        }
    //            //    case "J":
    //            //        {
    //            //            letraFormulario = "10";
    //            //            break;
    //            //        }
    //            //    case "K":
    //            //        {
    //            //            letraFormulario = "11";
    //            //            break;
    //            //        }
    //            //    case "L":
    //            //        {
    //            //            letraFormulario = "12";
    //            //            break;
    //            //        }
    //            //    case "M":
    //            //        {
    //            //            letraFormulario = "13";
    //            //            break;
    //            //        }
    //            //    case "N":
    //            //        {
    //            //            letraFormulario = "14";
    //            //            break;
    //            //        }
    //            //    case "O":
    //            //        {
    //            //            letraFormulario = "15";
    //            //            break;
    //            //        }
    //            //    case "P":
    //            //        {
    //            //            letraFormulario = "16";
    //            //            break;
    //            //        }
    //            //    case "Q":
    //            //        {
    //            //            letraFormulario = "17";
    //            //            break;
    //            //        }
    //            //    case "R":
    //            //        {
    //            //            letraFormulario = "18";
    //            //            break;
    //            //        }
    //            //    case "S":
    //            //        {
    //            //            letraFormulario = "19";
    //            //            break;
    //            //        }
    //            //    case "T":
    //            //        {
    //            //            letraFormulario = "20";
    //            //            break;
    //            //        }
    //            //    case "U":
    //            //        {
    //            //            letraFormulario = "21";
    //            //            break;
    //            //        }
    //            //    case "V":
    //            //        {
    //            //            letraFormulario = "22";
    //            //            break;
    //            //        }
    //            //    case "W":
    //            //        {
    //            //            letraFormulario = "23";
    //            //            break;
    //            //        }
    //            //    case "X":
    //            //        {
    //            //            letraFormulario = "24";
    //            //            break;
    //            //        }
    //            //    case "Y":
    //            //        {
    //            //            letraFormulario = "25";
    //            //            break;
    //            //        }
    //            //    case "Z":
    //            //        {
    //            //            letraFormulario = "26";
    //            //            break;
    //            //        }
    //            //    default:
    //            //        {
    //            //            letraFormulario = String.Empty;
    //            //            break;
    //            //        }
    //            //}

    //            #endregion

    //            //string codigoBanelco = nroFormulario + letraFormulario + añoFechaImpresion + mesFechaImpresion + urgente;
    //            //codigoBanelco = codigoBanelco.PadLeft(19, '0');

    //            //TODO_GATEWAY: COMENTAR HASTA AQUI - Sin GateWay de Pagos

    //            #endregion

    //            ////////////////////////////////////////////////////
    //            // TERMINA LA GENERACION DEL VIEJO CODIGO BANELCO //
    //            ////////////////////////////////////////////////////

    //            //TODO_CUIT: Agrego pagina de CUIT en el reporte.

    //            /*-- NUEVA MODIF CUIT 2016 --*/
    //            #region "REPORTE para CUIT"
    //            //BORRAR PORQUE YA NO SE USA

    //            //if (tipoOpeCuit == 1 || tipoOpeCuit == 2)
    //            //{
    //            //    stamper.AcroFields.SetField("txtNroCorrelativo3", " " + oTransaccion.Entidad.NroCorrelativo.ToString());
    //            //    stamper.AcroFields.SetField("txtNomEntidad3", HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.Nombre.ToString()));
    //            //    stamper.AcroFields.SetField("txtTipoEntidad3", HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.TipoSociedad.Descripcion.ToString()));
    //            //    stamper.AcroFields.SetField("txtCUIT3", HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.Cuit.ToString()));
    //            //    if (oTransaccion.Entidad.IdTipoOpeCuit == 1)
    //            //        stamper.AcroFields.SetField("txtTipoOpeCuit3", HttpContext.Current.Server.HtmlDecode(" " + "Se realizó el alta de un nuevo CUIT en IGJ"));
    //            //    if (oTransaccion.Entidad.IdTipoOpeCuit == 2)
    //            //        stamper.AcroFields.SetField("txtTipoOpeCuit3", HttpContext.Current.Server.HtmlDecode(" " + "Se realizó la modificación del CUIT en IGJ"));
    //            //}

    //            #endregion "REPORTE para CUIT"

    //            for (int i = 1; i <= reader.NumberOfPages; i++)
    //            {
    //                //FORMULARIO
    //                stamper.AcroFields.SetField("txtLetraFormulario" + i.ToString(), oTransaccion.Formulario.Descripcion.ToString());
    //                stamper.AcroFields.SetField("txtValorTimbrado" + i.ToString(), " " + oTransaccion.Formulario.Precio.ToString());
    //                stamper.AcroFields.SetField("txtFecha" + i.ToString(), " " + oTransaccion.FechaAlta.ToShortDateString());

    //                //FIRMANTE
    //                stamper.AcroFields.SetField("txtNombreFirmante" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Firmante.Nombre.ToString()));
    //                stamper.AcroFields.SetField("txtApellidoFirmante" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Firmante.Apellido.ToString()));
    //                stamper.AcroFields.SetField("txtDNIFirmante" + i.ToString(), " " + oTransaccion.Firmante.DNI.ToString());
    //                stamper.AcroFields.SetField("txtMailFirmante" + i.ToString(), " " + oTransaccion.Firmante.Mail.ToString());
    //                stamper.AcroFields.SetField("txtTelFirmante" + i.ToString(), " " + oTransaccion.Firmante.Telefono.ToString());
    //                //TODO_CUIT: Empieza nuevos datos del firmate 2017
    //                string strTipoDOC = "";
    //                switch (oTransaccion.Firmante.TipoDOC.ToString())
    //                {
    //                    case "DNI":
    //                        strTipoDOC = "Documento Nacional de Identidad";
    //                        break;
    //                    case "CDI":
    //                        strTipoDOC = "Cedula de Identidad";
    //                        break;
    //                    case "LIC":
    //                        strTipoDOC = "Libreta Civica";
    //                        break;
    //                    case "LIE":
    //                        strTipoDOC = "Libreta de Enrolamiento";
    //                        break;
    //                    case "PAS":
    //                        strTipoDOC = "Pasaporte";
    //                        break;
    //                    default:
    //                        strTipoDOC = "";
    //                        break;
    //                }
    //                stamper.AcroFields.SetField("txtTipoDocFirmante" + i.ToString(), " " + strTipoDOC);
    //                string strCaracter = "";
    //                string strAutorizado = "-----";
    //                switch (oTransaccion.Firmante.Caracter.ToString())
    //                {
    //                    case "RL":
    //                        strCaracter = "Representante Legal";
    //                        break;
    //                    case "PD":
    //                        strCaracter = "Dictaminante"; //Profesional Dictaminante
    //                        break;
    //                    case "AT":
    //                        strCaracter = "Autorizado a Tramitar";
    //                        strAutorizado = oTransaccion.Firmante.Autorizado.ToString();
    //                        break;
    //                    default:
    //                        strCaracter = "";
    //                        strAutorizado = "-----";
    //                        break;
    //                }
    //                stamper.AcroFields.SetField("txtCaracterFirmante" + i.ToString(), " " + strCaracter);
    //                stamper.AcroFields.SetField("txtAutorizadoFirmante" + i.ToString(), " " + strAutorizado);
    //                //TODO_CUIT: Termina nuevos datos del firmate 2017

    //                //stamper.AcroFields.SetField("txtCodBanelco" + i.ToString(), codigoBanelco);
    //                /*-- NUEVA MODIF BANELCO 2014 --*/
    //                //stamper.AcroFields.SetField("imgLogoPago" + i.ToString(), logoName);
    //                if (codrefpago != "")
    //                {
    //                    //TODO_GATEWAY: Cambio por el codigo de referencia de pago cargado desde el webservice
    //                    stamper.AcroFields.SetField("txtCodBanelco" + i.ToString(), codrefpago);
    //                    stamper.AcroFields.SetField("txtDNIBanelco" + i.ToString(), oDepositante.dni.ToString());
    //                    stamper.AcroFields.SetField("txtBancoBanelco" + i.ToString(), oDepositante.bancodesc.ToString());
    //                }
    //                else
    //                {
    //                    stamper.AcroFields.SetField("txtCodBanelco" + i.ToString(), " ------");
    //                    stamper.AcroFields.SetField("txtDNIBanelco" + i.ToString(), " ------");
    //                    stamper.AcroFields.SetField("txtBancoBanelco" + i.ToString(), " ------");
    //                }

    //                //TRAMITE
    //                stamper.AcroFields.SetField("txtTramDesc" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Tramite.Descripcion.ToString()));

    //                //TODO_GATEWAY: AGREGO IDS DE TRAMITES HARCODEADOS ADEMAS DEL 6
    //                //RESERVA DE DENOMINACION
    //                //if (!((string)vInfoTram.GetValue(0) == "6") && !((string)vInfoTram.GetValue(0) == "372") && !((string)vInfoTram.GetValue(0) == "373"))
    //                if (FlagReserva == false)
    //                {
    //                    //ENTIDAD
    //                    if (oTransaccion.Entidad.NroCorrelativo != 0)
    //                    {
    //                        stamper.AcroFields.SetField("txtNroCorrelativo" + i.ToString(), " " + oTransaccion.Entidad.NroCorrelativo.ToString());
    //                    }
    //                    else
    //                    {
    //                        stamper.AcroFields.SetField("txtNroCorrelativo" + i.ToString(), "");
    //                    }
    //                    stamper.AcroFields.SetField("txtNomEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.Nombre.ToString()));
    //                    stamper.AcroFields.SetField("txtTipoEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.TipoSociedad.Descripcion.ToString()));

    //                    //TODO_CUIT: Nuevo Dato Nro de CUIT 2017
    //                    //Imprimir el viejo si el usuario no lo rectifico
    //                    if ((oTransaccion.Entidad.CuitViejo > 0) && (oTransaccion.Entidad.IdTipoOpeCuit == 0))
    //                    {
    //                        stamper.AcroFields.SetField("txtCUIT" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.CuitViejo.ToString()));
    //                    }
    //                    else
    //                    {
    //                        stamper.AcroFields.SetField("txtCUIT" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Entidad.Cuit.ToString()));
    //                    }

    //                    //TRAMITE URGENTE
    //                    if (oTransaccion.Tramite.FlagUrgente)
    //                    {
    //                        stamper.AcroFields.SetField("txtUrgente" + i.ToString(), " SI");
    //                    }
    //                    else
    //                    {
    //                        stamper.AcroFields.SetField("txtUrgente" + i.ToString(), " NO");
    //                    }

    //                    //DATOS ADJUNTOS
    //                    stamper.AcroFields.SetField("txtMotivo" + i.ToString(), HttpContext.Current.Server.HtmlDecode(" " + oTransaccion.Motivo.ToString()));
    //                }
    //                else
    //                {
    //                    Denominacion oDenominacionRes = (FD.Entities.Denominacion)HttpContext.Current.Session["Denominacion"];

    //                    //OPCIONES DE DENOMINACION
    //                    stamper.AcroFields.SetField("txtDenomOp1" + i.ToString(), oDenominacionRes.Denominacion1.ToString());
    //                    stamper.AcroFields.SetField("txtDenomOp2" + i.ToString(), oDenominacionRes.Denominacion2.ToString());
    //                    stamper.AcroFields.SetField("txtDenomOp3" + i.ToString(), oDenominacionRes.Denominacion3.ToString());

    //                    string strpersoneria;

    //                    switch (oDenominacionRes.Tipo)
    //                    {
    //                        case 0:
    //                            strpersoneria = "Personas Físicas";
    //                            break;
    //                        case 1:
    //                            strpersoneria = "Personas Jurídicas";
    //                            break;
    //                        case 2:
    //                            strpersoneria = "Personas Físicas y/o Jurídicas";
    //                            break;
    //                        default:
    //                            strpersoneria = "";
    //                            break;
    //                    }

    //                    stamper.AcroFields.SetField("txtPersoneria" + i.ToString(), strpersoneria);

    //                    //TIPO DE ENTIDAD
    //                    stamper.AcroFields.SetField("txtTipoEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode((string)vInfoTram.GetValue(3)));
    //                    //stamper.AcroFields.SetField("txtTipoEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode(oGrupoEntidad.Descripcion.ToString()));
    //                    //TODO: Agrego la Descripción del Grupo de Entidad para imprimir en el Formulario
    //                    //stamper.AcroFields.SetField("txtGrupoEntidad" + i.ToString(), HttpContext.Current.Server.HtmlDecode(oGrupoEntidad.DescGrupoEntidad.ToString()));


    //                    //DATOS CONSTITUYENTE
    //                    switch (oDenominacionRes.Tipo)
    //                    {
    //                        case 0:
    //                            stamper.AcroFields.SetField("txtNyAConst1" + i.ToString(), oDenominacionRes.Constituyente[0].NombreYApellido.ToString());
    //                            stamper.AcroFields.SetField("txtNyAConst2" + i.ToString(), oDenominacionRes.Constituyente[1].NombreYApellido.ToString());
    //                            stamper.AcroFields.SetField("txtDNIConst1" + i.ToString(), oDenominacionRes.Constituyente[0].DNI.ToString());
    //                            stamper.AcroFields.SetField("txtDNIConst2" + i.ToString(), oDenominacionRes.Constituyente[1].DNI.ToString());
    //                            break;
    //                        case 1:
    //                            stamper.AcroFields.SetField("txtDenomCosnt1" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion1.ToString());
    //                            stamper.AcroFields.SetField("txtDenomCosnt2" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion2.ToString());
    //                            break;
    //                        case 2:
    //                            stamper.AcroFields.SetField("txtDNIConst1" + i.ToString(), oDenominacionRes.Constituyente[0].DNI.ToString());
    //                            stamper.AcroFields.SetField("txtNyAConst1" + i.ToString(), oDenominacionRes.Constituyente[0].NombreYApellido.ToString());
    //                            stamper.AcroFields.SetField("txtDenomCosnt2" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion1.ToString());
    //                            break;
    //                    }
    //                }

    //                #region Watermark

    //                //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("Template/watermark.png"));
    //                //img.SetAbsolutePosition(10, 10);
    //                //img.Rotation = 45;
    //                ////PARA MAS DE UNA PAGINA
    //                ////for (int i =1; i <= numberOfPages; i++)  
    //                ////{  
    //                ////    waterMarkContent = pdfStamper.GetUnderContent(i);  
    //                ////    waterMarkContent.AddImage(image);  
    //                ////}


    //                //PdfContentByte waterMark = stamper.GetUnderContent(i);
    //                //waterMark.AddImage(img);

    //                #endregion

    //                #region Codigo de Barras

    //                //CODIGO DE BARRAS
    //                PdfContentByte overContent = stamper.GetOverContent(i);
    //                Barcode39 codebar = new Barcode39();

    //                codebar.Code = oDatosOperador.CodeBar;
    //                codebar.StartStopText = false;

    //                // Get barcode image placeholder
    //                float[] barcodeArea = stamper.AcroFields.GetFieldPositions("codBarras");
    //                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(Convert.ToInt32(barcodeArea[1]), Convert.ToInt32(barcodeArea[2]), Convert.ToInt32(barcodeArea[3]), Convert.ToInt32(barcodeArea[4]));
    //                codebar.Size = 10;
    //                codebar.BarHeight = codebar.Size * 5f;


    //                iTextSharp.text.Image imageBarCode = codebar.CreateImageWithBarcode(overContent, null, null);
    //                imageBarCode.ScaleToFit(rect.Width, rect.Height);
    //                imageBarCode.SetAbsolutePosition(barcodeArea[1] + (rect.Width - imageBarCode.ScaledWidth) / 2, barcodeArea[2] + (rect.Height - imageBarCode.ScaledHeight) / 2);
    //                // Add barcode image
    //                overContent.AddImage(imageBarCode);

    //                #endregion
    //            }
    //            //#endregion

    //            stamper.SetFullCompression();
    //            stamper.FormFlattening = true;
    //            stamper.Close();

    //            #region Descarga de Formulario

    //            FileStream oFs;
    //            string strPath = HttpContext.Current.Server.MapPath("Temp/" + fileName);

    //            oFs = File.Open(strPath, FileMode.Open);
    //            byte[] bytBytes = new byte[oFs.Length];

    //            oFs.Read(bytBytes, 0, Convert.ToInt32(oFs.Length));
    //            oFs.Close();

    //            HttpContext.Current.Response.ClearContent();
    //            HttpContext.Current.Response.ClearHeaders();

    //            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=FormularioIGJ.pdf");
    //            HttpContext.Current.Response.ContentType = "application/pdf";
    //            HttpContext.Current.Response.BinaryWrite(bytBytes);

    //            HttpContext.Current.Response.Flush();

    //            File.Delete(strPath);

    //            #endregion
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("ERROR [" + uniq + "]" + ex.Message);
    //    }
    //    finally
    //    {
    //        if (file != null)
    //        {
    //            try
    //            {
    //                file.Close();
    //            }
    //            catch
    //            {
    //                Log.GrabarAdvertencia("ERROR al cerrar el archivo de descarga", "Util.DownloadForm()", "LOCAL");
    //            }
    //        }
    //        try
    //        {
    //            //if (File.Exists(HttpContext.Current.Server.MapPath("Template/FormBase.pdf")))
    //            //    File.Delete(HttpContext.Current.Server.MapPath("Template/FormBase.pdf"));                
    //        }
    //        catch
    //        {
    //            Log.GrabarAdvertencia("ERROR al borrar el archivo descargado", "Util.DownloadForm()", "LOCAL");
    //        }
    //    }
    //}
    #endregion "Descarga de Formulario en PDF para pedido de CUIT"
}