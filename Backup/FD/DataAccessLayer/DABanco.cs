using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FD.Entities;
using System.Data.SqlClient;

namespace FD.DataAccessLayer
{
    public class DABanco
    {
        SqlCommand oCmd;
        SqlDataReader oDR;
        Banco oBanco;

        public void retornarBancos(List<Banco> lBanco)
        {
            try
            {
                oCmd = new SqlCommand();
                oCmd.Connection = DAConexion.Conectar();
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "RetornarBancos";
                oDR = oCmd.ExecuteReader();

                while (oDR.Read())
                {
                    oBanco = new Banco(oDR[0].ToString(), oDR[1].ToString());
                    lBanco.Add(oBanco);
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR al retornar los bancos", "RetornarBancos", "LOCAL");
                throw ex;
            }
        }
    }
}
