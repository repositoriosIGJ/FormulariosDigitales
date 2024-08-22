using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

using log4net;
using FD.Entities;

namespace FD.DataAccessLayer
{
    public class DAConexion
    {
        private static SqlConnection _Con;

        public static SqlConnection Conectar()
        {
            try
            {
                _Con = new SqlConnection((ConfigurationManager.ConnectionStrings["FormDigiCon"].ConnectionString.ToString()).ToString());
                _Con.Open();
                return _Con;
            }
            catch (Exception ex)
            {
                Log.GrabarExcepcion("ERROR AL CONECTAR CON DB ---------> " + ex.Message); 
                throw ex;
            }
        }

        public static void Desconectar()
        {
            try
            {
                if (_Con.State == ConnectionState.Open)
                {
                    _Con.Close();
                    _Con.Dispose();
                }
            }
            catch (Exception na)
            { 
                throw new DAExcepcion("Error al desconectar de la BD. Metodo Desconectar() - " + na.Message.ToString());
            }
        }
    }
}
