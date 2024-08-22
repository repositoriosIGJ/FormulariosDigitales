using System;
using System.Collections.Generic;
using System.Text;

using FD.DataAccessLayer;

namespace FD.BusinessLayer
{
    public class BLLog
    {
        public static void GrabarLog(string mensaje, string operacion)
        {
            try
            {
                DALog.GrabarLog(mensaje, operacion);
            }
            catch (Exception ex)
            {
                //grabar en file
                throw ex;
            }
        }
        public static void GrabarTxt(string mensaje, string operacion)
        { 
        
        }

    }
}
