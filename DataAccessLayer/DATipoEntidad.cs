using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using FD.Entities;

namespace FD.DataAccessLayer
{
    public class DATipoEntidad
    {
        SqlCommand oCmd;
        SqlDataReader oDR;
        TipoEntidad oTipoEnt;

        public void retornatTiposSociedad(List<TipoEntidad> lTipoEnt)
        {
            try
            {
                oCmd = new SqlCommand();
                oCmd.Connection = DAConexion.Conectar();
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "RetornarTiposEntidad";
                oDR = oCmd.ExecuteReader();

                while (oDR.Read())
                {
                    oTipoEnt = new TipoEntidad(int.Parse(oDR[0].ToString()), oDR[1].ToString());
                    lTipoEnt.Add(oTipoEnt);
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al retornar los tipos de entidades", "RetornarTiposEntidad", "LOCAL");
                throw ex;
            }
        }

        public void RetornarTiposEntidadPorGrupo(List<TipoEntidad> lTipoEnt, bool lFlagSocAhorro, bool lFlagArt299)
        {
            try
            {
                oCmd = new SqlCommand();
                oCmd.Connection = DAConexion.Conectar();
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.Parameters.Add("@FlagSocAhorro", SqlDbType.Bit).Value = lFlagSocAhorro;
                oCmd.Parameters.Add("@FlagArt299", SqlDbType.Bit).Value = lFlagArt299;
                oCmd.CommandText = "RetornarTiposEntidadPorGrupo";
                oDR = oCmd.ExecuteReader();

                while (oDR.Read())
                {
                    oTipoEnt = new TipoEntidad(int.Parse(oDR[0].ToString()), oDR[1].ToString());
                    lTipoEnt.Add(oTipoEnt);
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al retornar los tipos de entidades", "RetornarTiposEntidad", "LOCAL");
                throw ex;
            }
        }

        public string getTipoEntidadXIdTramite(int IdTipoEnt)
        {
            try
            {
                using (oCmd = new SqlCommand("RetornarTipoEntidadXIdTramite", DAConexion.Conectar()))
                {
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.Parameters.Add("@IdTipoEntidad", SqlDbType.Int).Value = IdTipoEnt;
                    return oCmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al retornar tipo de entidad mediante el ID de tramite", "getTipoEntidadXIdTramite", "LOCAL");
                throw ex;
            }
        }
    }
}
