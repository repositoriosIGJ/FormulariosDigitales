using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using FD.Entities;

namespace FD.DataAccessLayer
{
    public class DATramite
    {
        Tramite oTramite;
        SqlCommand oCmd;
        SqlDataReader oDR;

        public List<Tramite> RetornarTiposTramites(string tipoEnt)//, string formulario
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    oCmd.Connection = DAConexion.Conectar();
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarTiposTramite";
                    oCmd.Parameters.Add("Entidad", SqlDbType.VarChar).Value = tipoEnt;
                    oDR = oCmd.ExecuteReader();

                    List<Tramite> lTramite = new List<Tramite>();

                    while (oDR.Read())
                    {
                        oTramite = new Tramite(int.Parse(oDR["id"].ToString()), oDR["descripcion"].ToString(), Convert.ToBoolean(oDR["flagCorr"]), Convert.ToBoolean(oDR["flagUrg"]));
                        lTramite.Add(oTramite);
                    }
                    return lTramite;
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR retornando los tipos de tramites", "RetornarTiposTramites", "LOCAL");
                throw ex;
            }
        }

        public int RetornarFactor(int id)
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    oCmd.Connection = DAConexion.Conectar();
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarFactor";
                    oCmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                    return int.Parse(oCmd.ExecuteScalar().ToString());
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al retornar el factor de los tramites", "RetornarFactor", "LOCAL");
                throw ex;
            }
        }

        //Retorna si el tramite es reserva o no a partir del flag en la tabla NomecladorTramite
        public bool RetornarTramiteReserva(int id)
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    bool flagReserva = false;
                    oCmd.Connection = DAConexion.Conectar();
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarTramiteReserva";
                    oCmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                    //return int.Parse(oCmd.ExecuteScalar());
                    flagReserva = ((bool?)oCmd.ExecuteScalar()).GetValueOrDefault();

                    return flagReserva;
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR retornando si el tramite es de reserva", "RetornarTramiteReserva", "LOCAL");
                throw ex;
            }
        }

        //TODO_CUIT: Retorna si el tramite pide CUIT o no a partir del flag en la tabla NomecladorTramite
        public bool RetornarTramiteCUIT(int id)
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    bool flagCUIT = false;
                    oCmd.Connection = DAConexion.Conectar();
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarTramiteCuit";
                    oCmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                    flagCUIT = ((bool?)oCmd.ExecuteScalar()).GetValueOrDefault();

                    return flagCUIT;
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR retornando si al tramite se le pide CUIT", "RetornarTramiteCuit", "LOCAL");
                throw ex;
            }
        }

        //Retorna si el tramite es de Constitucion partir del FlagNumCorrelativo en la tabla NomecladorTramite
        public bool RetornarTramiteConstitucion(int IdTramite)
        {
            try
            {
                using (oCmd = new SqlCommand())
                {
                    bool flagNroCorr = false;
                    oCmd.Connection = DAConexion.Conectar();
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "RetornarTramiteConstitucion";
                    oCmd.Parameters.Add("IdTramite", SqlDbType.Int).Value = IdTramite;
                    flagNroCorr = ((bool?)oCmd.ExecuteScalar()).GetValueOrDefault();

                    //flagNroCorr = 1 No es de Constitución
                    //flagNroCorr = 0 Es de Constitución
                    return !flagNroCorr;
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR retornando si al tramite es de Constitución", "RetornarTramiteConstitucion", "LOCAL");
                throw ex;
            }
        }

    }
}