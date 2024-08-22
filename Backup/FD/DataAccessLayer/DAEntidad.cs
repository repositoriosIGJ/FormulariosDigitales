using System;
//using System.Collections.Generic;
using System.Text;
using System.Data;
using FD.Entities;
using System.Data.SqlClient;

namespace FD.DataAccessLayer
{
    public class DAEntidad
    {
        SqlCommand oCmd;

        public DataSet GetSociedad(Int32 NumCorrelativo, Int32 tipoEnt)
        {
            Zidane.TasasIGJ oWSEntidad = new Zidane.TasasIGJ();

            try
            {
                //Mandar como parametro tipoEnt al WS, avisar a laura de crear un stored igual pero con un 
                //parametro de input (tipoEntidad)
                return oWSEntidad.GetSociedadFiltradoTipo(NumCorrelativo, tipoEnt);
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR en WebService", "GetSociedad", "LOCAL");
                throw ex;
            }
            finally
            {
                oWSEntidad = null;
            }
        }

        public DataSet GetSociedades(string RazonSo, Int32 tipoEnt)
        {
            Zidane.TasasIGJ oWSEntidad = new Zidane.TasasIGJ();
            try
            {
                //Mandar como parametro tipoEnt al WS, avisar a laura de crear un stored igual pero con un 
                //parametro de input (tipoEntidad)ghgasdas
                return oWSEntidad.GetSociedadesFiltradoTipo(RazonSo, tipoEnt);
                //return oWSEntidad.GetSociedades(RazonSo);//,tipoEnt
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR en WebService", "GetSociedades", "LOCAL");
                throw ex;
            }
            finally
            {
                oWSEntidad = null;
            }
        }

        public bool ValidarEntidadconTramite(int IdTramite, int IdTipoEntidad)
        {
            try
            {
                bool Resultado = false;

                using (oCmd = new SqlCommand())
                {
                    oCmd.Connection = DAConexion.Conectar();
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "ValidarEntidadconTramite";
                    oCmd.Parameters.Add("IdTramite", SqlDbType.Int).Value = IdTramite;
                    oCmd.Parameters.Add("IdTipoEntidad", SqlDbType.Int).Value = IdTipoEntidad;
                    Resultado = (Convert.ToBoolean(oCmd.ExecuteScalar()));
                }

                return Resultado;
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al Validar la Entidad con el Tramite", "DA ValidarEntidadconTramite", "LOCAL");
                throw ex;
            }
        }
    }
}
