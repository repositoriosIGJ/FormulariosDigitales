using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using FD.Entities;

namespace FD.DataAccessLayer
{
    public class DAFirmante
    {
        SqlCommand oCmd;
        SqlDataReader oDR;

        public void ValidarDNI(Firmante oFirmante)
        {
            try
            {
                oCmd = new SqlCommand();
                oCmd.Connection = DAConexion.Conectar();
                oCmd.CommandType = CommandType.StoredProcedure;
                oCmd.CommandText = "ValidarDatosFirmantePorDNI";
                oCmd.Parameters.Add("@DNI", SqlDbType.Int).Value = oFirmante.DNI;

                oDR = oCmd.ExecuteReader();

                while (oDR.Read())
                {
                    if (!(oDR.FieldCount == 1))
                    {
                        oFirmante.DNI = int.Parse(oDR["DNI"].ToString());
                        oFirmante.Nombre = oDR["Nombre"].ToString();
                        oFirmante.Apellido = oDR["Apellido"].ToString();
                        oFirmante.Mail = oDR["Mail"].ToString();
                    }
                    else
                    {
                        oFirmante.DNI = (int)oDR["DNI"];
                        oDR.Close();
                        oCmd.Dispose();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.GrabarAdvertencia("ERROR en Validacion de DNI", "ValidarDNI", "LOCAL");
                throw ex;
            }
            finally
            {
                oDR.Close();
                oCmd.Dispose();
            }
        }
    }
}
