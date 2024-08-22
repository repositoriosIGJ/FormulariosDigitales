using System;
using System.Collections.Generic;
using System.Text;
using FD.Entities;
using System.Data.SqlClient;
using System.Data;

namespace FD.DataAccessLayer
{
    public class DALibro
    {
        SqlConnection oCon;
        SqlCommand oCmd;
        SqlDataAdapter oDA;
        DataSet oDS;
        SqlDataReader oDR;

        public void AltaLibro(Libro oLibro, long IdTransaccion)
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
                    oCmd.CommandText = "AltaLibro";
                    //PARAMETROS
                    //this._LibroObOp = p_libroobop;
                    oCmd.Parameters.Add("@LibroObOp", SqlDbType.Int).Value = oLibro.LibroObOp;
                    //IdTransaccion
                    oCmd.Parameters.Add("@IdTransaccion", SqlDbType.BigInt).Value = IdTransaccion;
                    //this._IdTipoLibro = p_idtipolibro;
                    oCmd.Parameters.Add("@IdTipoLibro", SqlDbType.Int).Value = oLibro.IdTipoLibro;
                    //this._TipoLibro = p_tipolibro;
                    oCmd.Parameters.Add("@TipoLibroDesc", SqlDbType.VarChar).Value = oLibro.TipoLibro;
                    //this._Copiador = p_copiador;
                    oCmd.Parameters.Add("@Copiador", SqlDbType.VarChar).Value = oLibro.Copiador;
                    //this._Paginas = p_paginas;
                    oCmd.Parameters.Add("@Paginas", SqlDbType.BigInt).Value = oLibro.Paginas == null ? (object)DBNull.Value : oLibro.Paginas;
                    //this._Fojas = p_fojas;
                    oCmd.Parameters.Add("@Fojas", SqlDbType.BigInt).Value = oLibro.Fojas == null ? (object)DBNull.Value : oLibro.Fojas;                   

                    oDR = oCmd.ExecuteReader();

                    //while (oDR.Read())
                    //{
                    //    //Cargo los datos del operador actual
                    //    oDatosOperador.IdTrans = double.Parse(oDR[0].ToString());
                    //    oDatosOperador.CodeBar = oDR[1].ToString();
                    //}

                    //oTransaccion.Id = Convert.ToInt32(oDatosOperador.IdTrans);

                    oDR.Close();
                    oCmd.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                oCmd.Transaction.Rollback();
                Log.GrabarAdvertencia("ERROR al realizar el alta de un Libro para la Transaccion: " + IdTransaccion, "AltaLibro", "LOCAL");
                throw ex;
            }
            finally
            {
                oDR = null;
                DAConexion.Desconectar();
                oCon.Dispose();
            }
        }

        public List<TipoLibro> RetornarTiposLibros()
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    oCmd.Connection = DAConexion.Conectar();
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarTiposLibro";
                    oDR = oCmd.ExecuteReader();

                    List<TipoLibro> lTipoLibro = new List<TipoLibro>();
                    TipoLibro oTipoLibro = new TipoLibro();

                    while (oDR.Read())
                    {
                        oTipoLibro = new TipoLibro(int.Parse(oDR["id"].ToString()), oDR["descripcion"].ToString(), Convert.ToBoolean(oDR["obligatorio"]), Convert.ToBoolean(oDR["habilitado"]));
                        lTipoLibro.Add(oTipoLibro);
                    }
                    return lTipoLibro;
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR retornando los tipos de libros", "RetornarTiposLibro", "LOCAL");
                throw ex;
            }
        }

    }
}
