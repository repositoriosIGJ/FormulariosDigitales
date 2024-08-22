using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using FD.DataAccessLayer;
using FD.Entities;
using System.Threading;

namespace FD.BusinessLayer
{
    public class BLTransaccion
    {
        #region "Transaccion Comun"
        public void AltaTransaccion(Transaccion oTransaccion, Operador oDatosOperador, Depositante oDepositante, out long IdTransaccion)
        {
            DATransaccion oDATransaccion = new DATransaccion();
            try
            {
                oDATransaccion.AltaTransaccion(oTransaccion, oDatosOperador, oDepositante, out IdTransaccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATransaccion = null;
            }
        }

        public void UpdateTransaccion(int idtransaccion, string codrefpago)
        {
            DATransaccion oDATransaccion = new DATransaccion();
            try
            {
                oDATransaccion.UpdateTransaccion(idtransaccion, codrefpago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATransaccion = null;
            }
        }
        #endregion "Transaccion Comun"

        public void AltaTransaccionLibros(Transaccion oTransaccion, Operador oDatosOperador, Depositante oDepositante, List<Libro> lstLibros, out long IdTransaccion)
        {
            DATransaccion oDATransaccion = new DATransaccion();
            try
            {
                oDATransaccion.AltaTransaccionLibros(oTransaccion, oDatosOperador, oDepositante, lstLibros, out IdTransaccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATransaccion = null;
            }
        }

        #region "Transaccion con Reserva de Nombre"
        public void AltaTransaccionReserva(Transaccion oTransaccion, Operador oDatosOperador, Denominacion oDenominacion, Depositante oDepositante)//, Constituyente oConstituyente)
        {
            DATransaccion oDATransaccion = new DATransaccion();
            try
            {
                oDATransaccion.AltaTransaccionReserva(oTransaccion, oDatosOperador, oDenominacion, oDepositante);//, oConstituyente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATransaccion = null;
            }
        }
        #endregion "Transaccion con Reserva de Nombre"

        #region "Carga el DataSet para Reporte del Formulario"
        public Transaccion CargarDSReporte(double idTrans, bool reserva, string denominacion)
        {
            DATransaccion oDATransaccion = new DATransaccion();
            Transaccion oTransaccion;
            try
            {
                //casteo la variable de session para evaluar la propiedad "Reserva"
                //y poder ver si se trata de la misma o no y en ese caso ver tomar distintos valores
                //del DATASE para evitar el manejor de nulls y tener que agregar mas contstructores.
                //Operador oDatosOperador = SessionData.Session["DatosOperador"];
                //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-AR");

                DataSet oDS = oDATransaccion.CargarDSReporte(idTrans);
                //oDS.Tables["DTReporte"].Rows[0]["PathImagenLocal"] = pathImg;

                if (!reserva)
                {

                        oTransaccion = new Transaccion(new Formulario(oDS.Tables["DTReporte"].Rows[0]["DescripcionForm"].ToString(),
                                                                  Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["Precio"])),
                                                     new Firmante(Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["DNI"].ToString()),
                                                                  oDS.Tables["DTReporte"].Rows[0]["Nombre"].ToString(),
                                                                  oDS.Tables["DTReporte"].Rows[0]["Apellido"].ToString(),
                                                                  oDS.Tables["DTReporte"].Rows[0]["Mail"].ToString(),
                                                                  oDS.Tables["DTReporte"].Rows[0]["Telefono"].ToString(),
                                                                  oDS.Tables["DTReporte"].Rows[0]["TipoDOC"].ToString(), //Cambios en el Presentante 2017
                                                                  oDS.Tables["DTReporte"].Rows[0]["Caracter"].ToString(), //Cambios en el Presentante 2017
                                                                  oDS.Tables["DTReporte"].Rows[0]["Autorizado"].ToString()), //Cambios en el Presentante 2017
                                                     new Entidad(Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["NumCorrelativo"]),
                                                                 oDS.Tables["DTReporte"].Rows[0]["NombreEntidad"].ToString(),
                                                                 new TipoEntidad(oDS.Tables["DTReporte"].Rows[0]["TipoSociedad"].ToString()),
                                                                 //TODO_CUIT: Cargo los datos del CUIT 
                                                                 Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["Cuit"])),
                                                                 //Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["IdTipoOpeCuit"]),
                                                                 //Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["CuitViejo"])),
                                                     new Tramite(oDS.Tables["DTReporte"].Rows[0]["NombreTramite"].ToString(),
                                                                 oDS.Tables["DTReporte"].Rows[0]["Urgente"].ToString()),
                                                                 oDS.Tables["DTReporte"].Rows[0]["Motivo"].ToString(),
                                                                 System.DateTime.Today);                    
                }
                else
                {
                    oTransaccion = new Transaccion(new Formulario(oDS.Tables["DTReporte"].Rows[0]["DescripcionForm"].ToString(),
                                                                  Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["Precio"])),
                                                   new Firmante(Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["DNI"].ToString()),
                                                                oDS.Tables["DTReporte"].Rows[0]["Nombre"].ToString(),
                                                                oDS.Tables["DTReporte"].Rows[0]["Apellido"].ToString(),
                                                                oDS.Tables["DTReporte"].Rows[0]["Mail"].ToString(),
                                                                oDS.Tables["DTReporte"].Rows[0]["Telefono"].ToString(),
                                                                oDS.Tables["DTReporte"].Rows[0]["TipoDOC"].ToString(), //Cambios en el Presentante 2017
                                                                oDS.Tables["DTReporte"].Rows[0]["Caracter"].ToString(), //Cambios en el Presentante 2017
                                                                oDS.Tables["DTReporte"].Rows[0]["Autorizado"].ToString()), //Cambios en el Presentante 2017
                                                   new Tramite(oDS.Tables["DTReporte"].Rows[0]["NombreTramite"].ToString()),
                                                               denominacion,
                                                               System.DateTime.Today);
                    //TODO: Comento porque nose si va
                    //System.DateTime.Today, oDS.Tables["DTReporte"].Rows[0]["CodigoReferenciaPago"].ToString());
                }
                return oTransaccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATransaccion = null;
            }
        }
        #endregion "Carga el DataSet para Reporte del Formulario"

        //TODO_NOCUIT: No se usa el pedido de CUIT
        #region "Carga el DataSet para Reporte del Formulario con CUIT"
        //public Transaccion CargarDSReporte(double idTrans, bool reserva, string denominacion, bool flagcuit, int tipoopecuit)
        //{
        //    DATransaccion oDATransaccion = new DATransaccion();
        //    Transaccion oTransaccion;
        //    try
        //    {
        //        //casteo la variable de session para evaluar la propiedad "Reserva"
        //        //y poder ver si se trata de la misma o no y en ese caso ver tomar distintos valores
        //        //del DATASE para evitar el manejor de nulls y tener que agregar mas contstructores.
        //        //Operador oDatosOperador = SessionData.Session["DatosOperador"];
        //        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-AR");

        //        DataSet oDS = oDATransaccion.CargarDSReporte(idTrans);
        //        //oDS.Tables["DTReporte"].Rows[0]["PathImagenLocal"] = pathImg;

        //        if (!reserva)
        //        {
        //            //TODO_CUIT: Pregunto si es un tramite con pedido de CUIT o y el tipo de operacion es "Sin Cambios"
        //            if (!flagcuit || tipoopecuit == 0)
        //            {
        //                oTransaccion = new Transaccion(new Formulario(oDS.Tables["DTReporte"].Rows[0]["DescripcionForm"].ToString(),
        //                                                              Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["Precio"])),
        //                                                 new Firmante(Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["DNI"].ToString()),
        //                                                              oDS.Tables["DTReporte"].Rows[0]["Nombre"].ToString(),
        //                                                              oDS.Tables["DTReporte"].Rows[0]["Apellido"].ToString(),
        //                                                              oDS.Tables["DTReporte"].Rows[0]["Mail"].ToString(),
        //                                                              oDS.Tables["DTReporte"].Rows[0]["Telefono"].ToString(),
        //                                                              oDS.Tables["DTReporte"].Rows[0]["TipoDOC"].ToString(), //Cambios en el Presentante 2017
        //                                                              oDS.Tables["DTReporte"].Rows[0]["Caracter"].ToString(), //Cambios en el Presentante 2017
        //                                                              oDS.Tables["DTReporte"].Rows[0]["Autorizado"].ToString()), //Cambios en el Presentante 2017
        //                                                 new Entidad(Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["NumCorrelativo"]),
        //                                                             oDS.Tables["DTReporte"].Rows[0]["NombreEntidad"].ToString(),
        //                                                 new TipoEntidad(oDS.Tables["DTReporte"].Rows[0]["TipoSociedad"].ToString())),
        //                                                 new Tramite(oDS.Tables["DTReporte"].Rows[0]["NombreTramite"].ToString(),
        //                                                             oDS.Tables["DTReporte"].Rows[0]["Urgente"].ToString()),
        //                                                             oDS.Tables["DTReporte"].Rows[0]["Motivo"].ToString(),
        //                                                             System.DateTime.Today);
        //                //TODO: Comento porque nose si va
        //                //System.DateTime.Today, oDS.Tables["DTReporte"].Rows[0]["CodigoReferenciaPago"].ToString());
        //            }
        //            else
        //            {
        //            oTransaccion = new Transaccion(new Formulario(oDS.Tables["DTReporte"].Rows[0]["DescripcionForm"].ToString(),
        //                                                      Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["Precio"])),
        //                                         new Firmante(Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["DNI"].ToString()),
        //                                                      oDS.Tables["DTReporte"].Rows[0]["Nombre"].ToString(),
        //                                                      oDS.Tables["DTReporte"].Rows[0]["Apellido"].ToString(),
        //                                                      oDS.Tables["DTReporte"].Rows[0]["Mail"].ToString(),
        //                                                      oDS.Tables["DTReporte"].Rows[0]["Telefono"].ToString(),
        //                                                      oDS.Tables["DTReporte"].Rows[0]["TipoDOC"].ToString(), //Cambios en el Presentante 2017
        //                                                      oDS.Tables["DTReporte"].Rows[0]["Caracter"].ToString(), //Cambios en el Presentante 2017
        //                                                      oDS.Tables["DTReporte"].Rows[0]["Autorizado"].ToString()), //Cambios en el Presentante 2017
        //                                         new Entidad(Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["NumCorrelativo"]),
        //                                                     oDS.Tables["DTReporte"].Rows[0]["NombreEntidad"].ToString(),
        //                                                     new TipoEntidad(oDS.Tables["DTReporte"].Rows[0]["TipoSociedad"].ToString()),
        //                                                     //TODO_CUIT: Cargo los datos del CUIT 
        //                                                     Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["Cuit"]),
        //                                                     Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["IdTipoOpeCuit"]),
        //                                                     Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["CuitViejo"])),
        //                                         new Tramite(oDS.Tables["DTReporte"].Rows[0]["NombreTramite"].ToString(),
        //                                                     oDS.Tables["DTReporte"].Rows[0]["Urgente"].ToString()),
        //                                                     oDS.Tables["DTReporte"].Rows[0]["Motivo"].ToString(),
        //                                                     System.DateTime.Today);
        //        }
        //        }
        //        else
        //        {
        //            oTransaccion = new Transaccion(new Formulario(oDS.Tables["DTReporte"].Rows[0]["DescripcionForm"].ToString(),
        //                                                          Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["Precio"])),
        //                                           new Firmante(Convert.ToInt64(oDS.Tables["DTReporte"].Rows[0]["DNI"].ToString()),
        //                                                        oDS.Tables["DTReporte"].Rows[0]["Nombre"].ToString(),
        //                                                        oDS.Tables["DTReporte"].Rows[0]["Apellido"].ToString(),
        //                                                        oDS.Tables["DTReporte"].Rows[0]["Mail"].ToString(),
        //                                                        oDS.Tables["DTReporte"].Rows[0]["Telefono"].ToString(),
        //                                                        oDS.Tables["DTReporte"].Rows[0]["TipoDOC"].ToString(), //Cambios en el Presentante 2017
        //                                                        oDS.Tables["DTReporte"].Rows[0]["Caracter"].ToString(), //Cambios en el Presentante 2017
        //                                                        oDS.Tables["DTReporte"].Rows[0]["Autorizado"].ToString()), //Cambios en el Presentante 2017
        //                                           new Tramite(oDS.Tables["DTReporte"].Rows[0]["NombreTramite"].ToString()),
        //                                                       denominacion,
        //                                                       System.DateTime.Today);
        //            //TODO: Comento porque nose si va
        //            //System.DateTime.Today, oDS.Tables["DTReporte"].Rows[0]["CodigoReferenciaPago"].ToString());
        //        }
        //        return oTransaccion;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        oDATransaccion = null;
        //    }
        //}
        #endregion "Carga el DataSet para Reporte del Formulario con CUIT"

        //TODO: Ya no se usa
        #region "Carga el DataSet para Reporte del Formulario de Reserva"
        //public Transaccion CargarDSReporteReserva(double idTrans)
        //{
        //    DATransaccion oDATransaccion = new DATransaccion();
        //    try
        //    {

        //        DataSet oDS = oDATransaccion.CargarDSReporteReserva(idTrans);
        //        //oDS.Tables["DTReporte"].Rows[0]["PathImagenLocal"] = pathImg;


        //        Transaccion oTransaccion = new Transaccion(
        //                                                    new Formulario(oDS.Tables["DTReporte"].Rows[0]["DescripcionForm"].ToString(),
        //                                                                   Convert.ToDecimal(oDS.Tables["DTReporte"].Rows[0]["Precio"])),
        //                                                    new Firmante(Convert.ToInt32(oDS.Tables["DTReporte"].Rows[0]["DNI"].ToString()),
        //                                                                 oDS.Tables["DTReporte"].Rows[0]["Nombre"].ToString(),
        //                                                                 oDS.Tables["DTReporte"].Rows[0]["Apellido"].ToString(),
        //                                                                 oDS.Tables["DTReporte"].Rows[0]["Mail"].ToString()),

        //                                                    new Tramite(oDS.Tables["DTReporte"].Rows[0]["NombreTramite"].ToString(),
        //                                                                oDS.Tables["DTReporte"].Rows[0]["Urgente"].ToString()),
        //                                                    oDS.Tables["DTReporte"].Rows[0]["Motivo"].ToString(),
        //                                                    Convert.ToDateTime(oDS.Tables["DTReporte"].Rows[0]["FechaAlta"]));

        //        return oTransaccion;
        //    }
        //    catch (DAExcepcion daex)
        //    {
        //        throw daex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new BLExcepcion("Error en el metodo: BL CargarDSReporte() - " + ex.Message);
        //    }
        //    finally
        //    {
        //        oDATransaccion = null;
        //    }
        //}
        #endregion "Carga el DataSet para Reporte del Formulario de Reserva"


    }
}