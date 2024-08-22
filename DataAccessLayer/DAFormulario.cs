using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using FD.Entities;

namespace FD.DataAccessLayer
{
    public class DAFormulario
    {
        SqlCommand oCmd;
        SqlDataReader oDR;
        Formulario oFormulario;
        
        public void RetornarTiposFormulario(List<Formulario> lFormulario,string tramite)
        {
            try
            {
                using (oCmd = new SqlCommand())
                using (oCmd.Connection = DAConexion.Conectar())
                {
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarTiposFormulario";
                    oCmd.Parameters.Add("Tramite", SqlDbType.VarChar).Value = tramite;
                    oDR = oCmd.ExecuteReader();

                    while (oDR.Read())
                    {
                        oFormulario = new Formulario(int.Parse(oDR["id"].ToString()), oDR["descripcion"].ToString(), int.Parse(oDR["precio"].ToString()));
                        lFormulario.Add(oFormulario);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al retornar los tipos de formularios", "RetornarTiposFormulario", "LOCAL");
                throw ex;
            }
        }

        public int RetornarFlagAnexo(Int32 IdTramite, Int32 IdTipoEntidad)
        {
            try
            {
                using (oCmd = new SqlCommand())
                using (oCmd.Connection = DAConexion.Conectar())
                {
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarFlagAnexo";
                    oCmd.Parameters.Add("@IdTramite", SqlDbType.Int).Value = IdTramite;
                    oCmd.Parameters.Add("@IdTipoEntidad", SqlDbType.Int).Value = IdTipoEntidad;
                    oDR = oCmd.ExecuteReader();
                    oDR.Read();

                    int FlagAnexo = 0;

                    if (!oDR.IsDBNull(0))
                    {
                        FlagAnexo = Convert.ToInt32(oDR["FlagAnexo"]);
                    }

                    return FlagAnexo;
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al retornar el FlagAnexo", "RetornarFlagAnexo", "LOCAL");
                throw ex;
            }
        }
    }
}
