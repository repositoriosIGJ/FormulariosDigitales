using System;
using System.Collections.Generic;
using System.Text;

using FD.Entities;
using FD.DataAccessLayer;

namespace FD.BusinessLayer
{
    public class BLFirmante
    {
        public void ValidarDNI(Firmante oFirmante)
        {
            try
            {
                DAFirmante oDLFirmante = new DAFirmante();
                oDLFirmante.ValidarDNI(oFirmante);
                oDLFirmante = null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
