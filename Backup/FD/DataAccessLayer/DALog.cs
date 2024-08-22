using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FD.DataAccessLayer
{
    public class DALog
    {
        public static void GrabarLog(string mensaje, string operacion)
        {
            SqlCommand oCmd;
            try
            {
                using (oCmd = new SqlCommand())
                using (oCmd.Connection = DAConexion.Conectar())
                {
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.CommandText = "GrabarLog";
                    oCmd.Parameters.Add("@mensaje", SqlDbType.VarChar).Value = mensaje;
                    oCmd.Parameters.Add("@operacion", SqlDbType.VarChar).Value = operacion;
                    oCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCmd = null;
            }
        }
    }
}
