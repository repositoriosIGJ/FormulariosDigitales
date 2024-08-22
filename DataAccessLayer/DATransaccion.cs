using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FD.Entities;
using System.Collections;

namespace FD.DataAccessLayer
{
    public class DATransaccion
    {
        SqlConnection oCon;
        SqlCommand oCmd;
        SqlCommand oCmd2;
        SqlDataAdapter oDA;
        DataSet oDS;
        SqlDataReader oDR;
        SqlTransaction oTranSQL;

        public void AltaTransaccion(Transaccion oTransaccion, Operador oDatosOperador, Depositante oDepositante, out long IdTransaccion)
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    oCon = new SqlConnection();
                    //Conexion ya abierta
                    oCon = DAConexion.Conectar();
                    //TRANSACCION
                    oCmd.Transaction = oCon.BeginTransaction();

                    oCmd.Connection = oCon;

                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "AltaTransaccion";
                    //PARAMETROS
                    oCmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = oTransaccion.Estado.Id;
                    //TODO_CUIT: Empieza Cambios en el Presentante 2017
                    oCmd.Parameters.Add("@TipoDOC", SqlDbType.VarChar).Value = oTransaccion.Firmante.TipoDOC.ToString();
                    oCmd.Parameters.Add("@Caracter", SqlDbType.VarChar).Value = oTransaccion.Firmante.Caracter.ToString();
                    oCmd.Parameters.Add("@Autorizado", SqlDbType.VarChar).Value = oTransaccion.Firmante.Autorizado.ToString();
                    //TODO_CUIT: Termina Cambios en el Presentante 2017
                    oCmd.Parameters.Add("@DNI", SqlDbType.BigInt).Value = oTransaccion.Firmante.DNI;
                    oCmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = oTransaccion.Firmante.Nombre.ToString();
                    oCmd.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = oTransaccion.Firmante.Apellido.ToString();
                    oCmd.Parameters.Add("@Mail", SqlDbType.VarChar).Value = oTransaccion.Firmante.Mail.ToString();
                    oCmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = oTransaccion.Firmante.Telefono.ToString();
                    oCmd.Parameters.Add("@IdNomencladorTram", SqlDbType.Int).Value = oTransaccion.Tramite.Id;
                    oCmd.Parameters.Add("@flagUrgente", SqlDbType.Bit).Value = oTransaccion.Tramite.FlagUrgente;
                    oCmd.Parameters.Add("@NumCorrelativo", SqlDbType.Int).Value = oTransaccion.Entidad.NroCorrelativo;
                    oCmd.Parameters.Add("@NombreEntidad", SqlDbType.VarChar).Value = oTransaccion.Entidad.Nombre.ToString();
                    oCmd.Parameters.Add("@IDTipoEnt", SqlDbType.Int).Value = oTransaccion.Entidad.TipoSociedad.Id;

                    //TODO_CUIT: Agrego el campo CUIT de la entidad para insercion en la Base
                    oCmd.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = oTransaccion.Entidad.Cuit;
                    //TODO_CUIT: Agrego el campo Id de Tipo de Operacion CUIT para saber si es un CUIT Nuevo, Actualizado o no se modifico
                    //oCmd.Parameters.Add("@IdTipoOpeCuit", SqlDbType.Int).Value = oTransaccion.Entidad.IdTipoOpeCuit;
                    //TODO_CUIT: Agrego el CUITVIEJO de la entidad para insercion en la Base
                    //oCmd.Parameters.Add("@CuitViejo", SqlDbType.BigInt).Value = oTransaccion.Entidad.CuitViejo;

                    oCmd.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = oTransaccion.Motivo.ToString();
                    oCmd.Parameters.Add("@LetraForm", SqlDbType.VarChar).Value = oTransaccion.Formulario.Descripcion.ToString();

                    //TODO: Agrego el Parametro con el codigo de banelco pero despues lo actualizo
                    //TODO_GATEWAY: Agrego el Parametro de CodigoReferenciaPago
                    oCmd.Parameters.Add("@CodigoReferenciaPago", SqlDbType.VarChar).Value = oTransaccion.CodRefPago;
                    //TODO_GATEWAY: Agrego los parametros DNI y BANCO
                    if (oDepositante == null)
                    {
                        oCmd.Parameters.Add("@DniPago", SqlDbType.BigInt).Value = null;
                        oCmd.Parameters.Add("@BancoPago", SqlDbType.VarChar).Value = null;
                    }
                    else
                    {
                        oCmd.Parameters.Add("@DniPago", SqlDbType.BigInt).Value = oDepositante.dni;
                        oCmd.Parameters.Add("@BancoPago", SqlDbType.VarChar).Value = oDepositante.bancodesc.ToString();
                    }

                    oDR = oCmd.ExecuteReader();

                    while (oDR.Read())
                    {
                        //Cargo los datos del operador actual
                        oDatosOperador.IdTrans = double.Parse(oDR[0].ToString());
                        oDatosOperador.CodeBar = oDR[1].ToString();
                    }

                    //Guarda el ID de la Transaccion en un Parametro de Salida
                    IdTransaccion = Convert.ToInt64(oDatosOperador.IdTrans);

                    oDR.Close();
                    oCmd.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                oCmd.Transaction.Rollback();
                Log.GrabarAdvertencia("ERROR al realizar el alta de la transaccion", "AltaTransaccion", "LOCAL");
                throw ex;
            }
            finally
            {
                oDR = null;
                DAConexion.Desconectar();
                oCon.Dispose();
            }
        }

        public void UpdateTransaccion(int idtransaccion, string codrefpago)//, bool reserva)
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    oCon = new SqlConnection();
                    //Conexion ya abierta
                    oCon = DAConexion.Conectar();
                    //TRANSACCION
                    oCmd.Transaction = oCon.BeginTransaction();

                    oCmd.Connection = oCon;

                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "UpdateTransaccion";
                    //PARAMETROS
                    oCmd.Parameters.Add("@IdTransaccion", SqlDbType.Int).Value = idtransaccion;
                    //TODO: Agrego el Parametro con el codigo de banelco
                    oCmd.Parameters.Add("@CodigoReferenciaPago", SqlDbType.VarChar).Value = codrefpago;

                    oCmd.ExecuteNonQuery();

                    oCmd.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                oCmd.Transaction.Rollback();
                Log.GrabarAdvertencia("ERROR al realizar el update de la transaccion", "UpdateTransaccion", "LOCAL");
                throw ex;
            }
            finally
            {
                //oDR = null;
                DAConexion.Desconectar();
                oCon.Dispose();
            }
        }

        public void AltaTransaccionReserva(Transaccion oTransaccion, Operador oDatosOperador, Denominacion oDenominacion, Depositante oDepositante)
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    oCon = new SqlConnection();
                    //Conexion ya abierta
                    oCon = DAConexion.Conectar();
                    //TRANSACCION
                    oCmd.Transaction = oCon.BeginTransaction();

                    oCmd.Connection = oCon;

                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "AltaTransaccionReserva";

                    //PROCEDURE [dbo].[AltaTransaccionReserva]
                    //@IdEstado		   NUMERIC(2),
                    //@DNI			   BIGINT,
                    //@Nombre			   varchar(150),
                    //@Apellido		   varchar(200),
                    //@Mail			   varchar(255),
                    //@Telefono		   VARCHAR(50),
                    //@IdNomencladorTram numeric(18),--IdNomencladorTramite / de aca saco el formulario y sus datos.
                    //@flagUrgente       bit,
                    //@IDTipoEnt         NUMERIC(5),
                    //@Denominacion1     varchar (250),
                    //@LetraForm		   varchar(2)

                    //PARAMETROS
                    oCmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = oTransaccion.Estado.Id;
                    //TODO_CUIT: Empieza Cambios en el Presentante 2017
                    oCmd.Parameters.Add("@TipoDOC", SqlDbType.VarChar).Value = oTransaccion.Firmante.TipoDOC.ToString();
                    oCmd.Parameters.Add("@Caracter", SqlDbType.VarChar).Value = oTransaccion.Firmante.Caracter.ToString();
                    oCmd.Parameters.Add("@Autorizado", SqlDbType.VarChar).Value = oTransaccion.Firmante.Autorizado.ToString();
                    //TODO_CUIT: Termina Cambios en el Presentante 2017
                    oCmd.Parameters.Add("@DNI", SqlDbType.BigInt).Value = oTransaccion.Firmante.DNI;
                    oCmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = oTransaccion.Firmante.Nombre.ToString();
                    oCmd.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = oTransaccion.Firmante.Apellido.ToString();
                    oCmd.Parameters.Add("@Mail", SqlDbType.VarChar).Value = oTransaccion.Firmante.Mail.ToString();
                    oCmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = oTransaccion.Firmante.Telefono.ToString();
                    oCmd.Parameters.Add("@IdNomencladorTram", SqlDbType.Int).Value = oTransaccion.Tramite.Id;
                    oCmd.Parameters.Add("@flagUrgente", SqlDbType.Bit).Value = oTransaccion.Tramite.FlagUrgente;
                    oCmd.Parameters.Add("@IDTipoEnt", SqlDbType.Int).Value = oTransaccion.Entidad.TipoSociedad.Id;
                    oCmd.Parameters.Add("@Denominacion1", SqlDbType.VarChar).Value = oTransaccion.Denominacion.ToString();
                    oCmd.Parameters.Add("@LetraForm", SqlDbType.VarChar).Value = oTransaccion.Formulario.Descripcion.ToString();
                    //TODO_GATEWAY: Agrego el Parametro de CodigoReferenciaPago
                    oCmd.Parameters.Add("@CodigoReferenciaPago", SqlDbType.VarChar).Value = oTransaccion.CodRefPago;
                    //TODO_GATEWAY: Agrego los parametros DNI y BANCO
                    if (oDepositante == null)
                    {
                        oCmd.Parameters.Add("@DniPago", SqlDbType.BigInt).Value = null;
                        oCmd.Parameters.Add("@BancoPago", SqlDbType.VarChar).Value = null;
                    }
                    else
                    {
                        oCmd.Parameters.Add("@DniPago", SqlDbType.BigInt).Value = oDepositante.dni;
                        oCmd.Parameters.Add("@BancoPago", SqlDbType.VarChar).Value = oDepositante.bancodesc.ToString();
                    }

                    //Parametros de Denominacion
                    //@DenomOp1             varchar(255),
                    oCmd.Parameters.Add("@DenomOp1", SqlDbType.VarChar).Value = oDenominacion.Denominacion1.ToString();
                    //@DenomOp2             varchar(255),
                    oCmd.Parameters.Add("@DenomOp2", SqlDbType.VarChar).Value = oDenominacion.Denominacion2.ToString();
                    //@DenomOp3             varchar(255),
                    oCmd.Parameters.Add("@DenomOp3", SqlDbType.VarChar).Value = oDenominacion.Denominacion3.ToString();

                    //Parametros de Constituyente
                    string strpersoneria = "";

                    //DATOS CONSTITUYENTE
                    switch (oDenominacion.Tipo)
                    {
                        case 0:
                            //@Personeria
                            strpersoneria = "PF"; //"Personas Físicas";

                            //stamper.AcroFields.SetField("txtNyAConst1" + i.ToString(), oDenominacionRes.Constituyente[0].NombreYApellido.ToString());
                            //stamper.AcroFields.SetField("txtNyAConst2" + i.ToString(), oDenominacionRes.Constituyente[1].NombreYApellido.ToString());
                            //stamper.AcroFields.SetField("txtDNIConst1" + i.ToString(), oDenominacionRes.Constituyente[0].DNI.ToString());
                            //stamper.AcroFields.SetField("txtDNIConst2" + i.ToString(), oDenominacionRes.Constituyente[1].DNI.ToString());

                            //@NomApe1              varchar(255),
                            oCmd.Parameters.Add("@NomApe1", SqlDbType.VarChar).Value = oDenominacion.Constituyente[0].NombreYApellido.ToString();
                            //@NomApe2              varchar(255),
                            oCmd.Parameters.Add("@NomApe2", SqlDbType.VarChar).Value = oDenominacion.Constituyente[1].NombreYApellido.ToString();
                            //@Denom1               varchar(255),
                            oCmd.Parameters.Add("@Denom1", SqlDbType.VarChar).Value = ""; //Vacio
                            //@Denom2               varchar(255),
                            oCmd.Parameters.Add("@Denom2", SqlDbType.VarChar).Value = ""; //Vacio
                            //@Docu1                bigint,
                            oCmd.Parameters.Add("@Docu1", SqlDbType.VarChar).Value = oDenominacion.Constituyente[0].DNI;
                            //@Docu2                bigint
                            oCmd.Parameters.Add("@Docu2", SqlDbType.VarChar).Value = oDenominacion.Constituyente[1].DNI;
                            break;
                        case 1:
                            //@Personeria
                            strpersoneria = "PJ"; //"Personas Jurídicas" 

                            //stamper.AcroFields.SetField("txtDenomCosnt1" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion1.ToString());
                            //stamper.AcroFields.SetField("txtDenomCosnt2" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion2.ToString());

                            //@NomApe1              varchar(255),
                            oCmd.Parameters.Add("@NomApe1", SqlDbType.VarChar).Value = ""; //Vacio
                            //@NomApe2              varchar(255),
                            oCmd.Parameters.Add("@NomApe2", SqlDbType.VarChar).Value = ""; //Vacio
                            //@Denom1               varchar(255),
                            oCmd.Parameters.Add("@Denom1", SqlDbType.VarChar).Value = oDenominacion.Constituyente[0].Denominacion1.ToString();
                            //@Denom2               varchar(255),
                            oCmd.Parameters.Add("@Denom2", SqlDbType.VarChar).Value = oDenominacion.Constituyente[0].Denominacion2.ToString();
                            //@Docu1                bigint,
                            oCmd.Parameters.Add("@Docu1", SqlDbType.VarChar).Value = ""; //Vacio
                            //@Docu2                bigint
                            oCmd.Parameters.Add("@Docu2", SqlDbType.VarChar).Value = ""; //Vacio
                            break;
                        case 2:
                            //@Personeria
                            strpersoneria = "MI"; //Mixto "Personas Físicas y/o Jurídicas"

                            //stamper.AcroFields.SetField("txtDNIConst1" + i.ToString(), oDenominacionRes.Constituyente[0].DNI.ToString());
                            //stamper.AcroFields.SetField("txtNyAConst1" + i.ToString(), oDenominacionRes.Constituyente[0].NombreYApellido.ToString());
                            //stamper.AcroFields.SetField("txtDenomCosnt1" + i.ToString(), oDenominacionRes.Constituyente[0].Denominacion1.ToString());

                            //@NomApe1              varchar(255),
                            oCmd.Parameters.Add("@NomApe1", SqlDbType.VarChar).Value = oDenominacion.Constituyente[0].NombreYApellido.ToString();
                            //@NomApe2              varchar(255),
                            oCmd.Parameters.Add("@NomApe2", SqlDbType.VarChar).Value = ""; //Vacio
                            //@Denom1               varchar(255),
                            oCmd.Parameters.Add("@Denom1", SqlDbType.VarChar).Value = ""; //Vacio
                            //@Denom2               varchar(255),
                            oCmd.Parameters.Add("@Denom2", SqlDbType.VarChar).Value = oDenominacion.Constituyente[0].Denominacion1.ToString();
                            //@Docu1                bigint,
                            oCmd.Parameters.Add("@Docu1", SqlDbType.VarChar).Value = oDenominacion.Constituyente[0].DNI;
                            //@Docu2                bigint
                            oCmd.Parameters.Add("@Docu2", SqlDbType.VarChar).Value = ""; //Vacio
                            break;
                    }

                    //@Personeria-varchar(50), --0-PF = "Personas Físicas"/ 1-PJ = "Personas Jurídicas" / 2-MI = Mixto "Personas Físicas y/o Jurídicas"
                    oCmd.Parameters.Add("@Personeria", SqlDbType.VarChar).Value = strpersoneria;

                    oDR = oCmd.ExecuteReader();

                    while (oDR.Read())
                    {
                        //Cargo los datos del operador actual
                        oDatosOperador.IdTrans = double.Parse(oDR[0].ToString());
                        oDatosOperador.CodeBar = oDR[1].ToString();
                    }

                    //oTransaccion.Id = Convert.ToInt32(oDatosOperador.IdTrans);

                    oDR.Close();
                    oCmd.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                oCmd.Transaction.Rollback();
                Log.GrabarAdvertencia("ERROR al realizar el alta de la transaccion por reserva", "AltaTransaccionReserva", "LOCAL");
                throw ex;
            }
            finally
            {
                oDR = null;
                DAConexion.Desconectar();
                oCon.Dispose();
            }
        }

        public void AltaTransaccionLibros(Transaccion oTransaccion, Operador oDatosOperador, Depositante oDepositante, List<Libro> lstLibros, out long IdTransaccion)
        {
            oCon = new SqlConnection();
            //Abre la conexion "_Con.Open()"
            oCon = DAConexion.Conectar();
            //Empieza la Transacción
            oTranSQL = oCon.BeginTransaction();
            //oCmd.Transaction = oTranSQL;
            //oCmd.Transaction = oCon.BeginTransaction();

            try
            {
                #region "Alta de la Transacción"

                //PROCEDURE ALTA DE TRANSACCION
                oCmd = new SqlCommand("AltaTransaccion", oCon);
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Transaction = oTranSQL;
                //oCmd.CommandText = "AltaTransaccion";

                //PARAMETROS
                oCmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = oTransaccion.Estado.Id;
                //TODO: Empieza Cambios en el Presentante 2017
                oCmd.Parameters.Add("@TipoDOC", SqlDbType.VarChar).Value = oTransaccion.Firmante.TipoDOC.ToString();
                oCmd.Parameters.Add("@Caracter", SqlDbType.VarChar).Value = oTransaccion.Firmante.Caracter.ToString();
                oCmd.Parameters.Add("@Autorizado", SqlDbType.VarChar).Value = oTransaccion.Firmante.Autorizado.ToString();
                //TODO: Termina Cambios en el Presentante 2017
                oCmd.Parameters.Add("@DNI", SqlDbType.BigInt).Value = oTransaccion.Firmante.DNI;
                oCmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = oTransaccion.Firmante.Nombre.ToString();
                oCmd.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = oTransaccion.Firmante.Apellido.ToString();
                oCmd.Parameters.Add("@Mail", SqlDbType.VarChar).Value = oTransaccion.Firmante.Mail.ToString();
                oCmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = oTransaccion.Firmante.Telefono.ToString();
                oCmd.Parameters.Add("@IdNomencladorTram", SqlDbType.Int).Value = oTransaccion.Tramite.Id;
                oCmd.Parameters.Add("@flagUrgente", SqlDbType.Bit).Value = oTransaccion.Tramite.FlagUrgente;
                oCmd.Parameters.Add("@NumCorrelativo", SqlDbType.Int).Value = oTransaccion.Entidad.NroCorrelativo;
                oCmd.Parameters.Add("@NombreEntidad", SqlDbType.VarChar).Value = oTransaccion.Entidad.Nombre.ToString();
                oCmd.Parameters.Add("@IDTipoEnt", SqlDbType.Int).Value = oTransaccion.Entidad.TipoSociedad.Id;
                oCmd.Parameters.Add("@CUIT", SqlDbType.BigInt).Value = oTransaccion.Entidad.Cuit;
                oCmd.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = oTransaccion.Motivo.ToString();
                oCmd.Parameters.Add("@LetraForm", SqlDbType.VarChar).Value = oTransaccion.Formulario.Descripcion.ToString();

                //Agrego el Parametro con el codigo de banelco pero despues lo actualizo
                //Agrego el Parametro de CodigoReferenciaPago
                oCmd.Parameters.Add("@CodigoReferenciaPago", SqlDbType.VarChar).Value = oTransaccion.CodRefPago;

                //Agrego los parametros DNI y BANCO
                if (oDepositante == null)
                {
                    oCmd.Parameters.Add("@DniPago", SqlDbType.BigInt).Value = null;
                    oCmd.Parameters.Add("@BancoPago", SqlDbType.VarChar).Value = null;
                }
                else
                {
                    oCmd.Parameters.Add("@DniPago", SqlDbType.BigInt).Value = oDepositante.dni;
                    oCmd.Parameters.Add("@BancoPago", SqlDbType.VarChar).Value = oDepositante.bancodesc.ToString();
                }

                oDR = oCmd.ExecuteReader();
                //oDR = oCmd.ExecuteNonQuery();

                while (oDR.Read())
                {
                    //Cargo los datos del operador actual
                    oDatosOperador.IdTrans = double.Parse(oDR[0].ToString());
                    oDatosOperador.CodeBar = oDR[1].ToString();
                }

                //Guarda el ID de la Transaccion en un Parametro de Salida
                IdTransaccion = Convert.ToInt64(oDatosOperador.IdTrans);

                oDR.Close();

                #endregion "Alta de la Transacción"

                #region "Alta de los Libros de Rúbrica"

                //Alta de todos los libros de Rubrica Asociados al IdTransaccion (Tramite)
                oCmd2 = new SqlCommand("AltaLibro", oCon);
                oCmd2.CommandType = CommandType.StoredProcedure;
                oCmd2.Transaction = oTranSQL;
                //oCmd2.CommandText = "AltaLibro";

                foreach (Libro oLibro in lstLibros)
                {
                    //PARAMETROS
                    oCmd2.Parameters.Add("@IdTransaccion", SqlDbType.BigInt).Value = IdTransaccion;
                    oCmd2.Parameters.Add("@LibroObOp", SqlDbType.Int).Value = oLibro.LibroObOp;
                    oCmd2.Parameters.Add("@IdTipoLibro", SqlDbType.Int).Value = oLibro.IdTipoLibro;
                    oCmd2.Parameters.Add("@TipoLibroDesc", SqlDbType.VarChar).Value = oLibro.TipoLibro;
                    oCmd2.Parameters.Add("@Copiador", SqlDbType.VarChar).Value = oLibro.Copiador;
                    oCmd2.Parameters.Add("@Paginas", SqlDbType.BigInt).Value = oLibro.Paginas == null ? (object)DBNull.Value : oLibro.Paginas;
                    oCmd2.Parameters.Add("@Fojas", SqlDbType.BigInt).Value = oLibro.Fojas == null ? (object)DBNull.Value : oLibro.Fojas;

                    //oDR = oCmd.ExecuteReader(); //Cual usar?
                    oCmd2.ExecuteNonQuery();
                    oCmd2.Parameters.Clear();

                }

                #endregion "Alta de los Libros de Rúbrica"

                //Ejecuta la Transacción
                oTranSQL.Commit();
            }
            catch (Exception ex)
            {
                //Rollback de la Transacción
                oTranSQL.Rollback();
                oDR.Close();
                oDR = null;
                //DAConexion.Desconectar();
                Log.GrabarAdvertencia("ERROR al realizar el alta de la transaccion con libros de rubrica: " + ex.Message, "AltaTransaccionLibros", "LOCAL");
                throw ex;
            }
            finally
            {
                oDR.Close();
                oDR = null;
                DAConexion.Desconectar();
                oCon.Dispose();
            }
        }

        public DataSet CargarDSReporte(double idTrans)//byte[] imagenCodBara
        {
            try
            {
                oDS = new DataSet();
                using (oCmd = new SqlCommand())
                using (oCmd.Connection = DAConexion.Conectar())
                {
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarDatosReporte";
                    oCmd.Parameters.Add("@IdTran", SqlDbType.Int).Value = idTrans;
                    oDA = new SqlDataAdapter(oCmd);
                    oDA.Fill(oDS, "DTReporte");

                    return oDS;
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al cargar el DataSet del reporte", "CargarDSReporte", "LOCAL");
                throw ex;
            }
            finally
            {
                oDA.Dispose();
                oCmd.Dispose();
                oDS = null;
            }
        }

        //public DataSet CargarDSReporteReserva(double idTrans)//byte[] imagenCodBara
        //{
        //    try
        //    {
        //        oDS = new DataSet();
        //        using (oCmd = new SqlCommand())
        //        using (oCmd.Connection = DAConexion.Conectar())
        //        {
        //            oCmd.CommandType = CommandType.StoredProcedure;
        //            oCmd.CommandText = "RetornarDatosReporte";
        //            oCmd.Parameters.Add("@IdTran", SqlDbType.Int).Value = idTrans;
        //            oDA = new SqlDataAdapter(oCmd);
        //            oDA.Fill(oDS, "DTReporte");

        //            return oDS;
        //        }
        //    }
        //    catch (DAExcepcion daex)
        //    {
        //        throw daex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DAExcepcion("Error en el metodo: DA CargarDSReporte() - " + ex.Message);
        //    }
        //    finally
        //    {
        //        oDA.Dispose();
        //        oCmd.Dispose();
        //        oDS = null;
        //    }
        //}
    }
}
