using System;
using System.Collections.Generic;
using System.Text;
using FD.Entities;
using FD.DataAccessLayer;

namespace FD.BusinessLayer
{
    public class BLBanco
    {
        public void RetornarBancos(List<Banco> lBanco)
        {
            try
            {
                DABanco oDLBancos = new DABanco();
                //Retorna todos los Bancos
                //oDLBancos.RetornaBancos(lBanco);
                oDLBancos.retornarBancos(lBanco);
                oDLBancos = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
