using System;
using System.Collections.Generic;
using System.Text;

using FD.Entities;
using FD.DataAccessLayer;

namespace FD.BusinessLayer
{
    public class BLTramite
    {
        public List<Tramite> RetornarTiposTramites(string tipoEnt)//, string formulario
        {
            DATramite oDATramite = new DATramite();
            try
            {
                return oDATramite.RetornarTiposTramites(tipoEnt);//,formulario
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATramite = null;
            }
        }

        public int RetornarFactor(int id)
        {
            DATramite oDATramite = new DATramite();
            try
            {
                return oDATramite.RetornarFactor(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATramite = null;
            }
        }

        public bool RetornarTramiteReserva(int id)
        {
            DATramite oDATramite = new DATramite();
            try
            {
                return oDATramite.RetornarTramiteReserva(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATramite = null;
            }
        }

        //TODO_NOCUIT: No se usa el pedido de CUIT
        //TODO_CUIT: Retorna si el tramite pide CUIT o no a partir del flag en la tabla NomecladorTramite
        /*
        public bool RetornarTramiteCUIT(int id)
        {
            DATramite oDATramite = new DATramite();
            try
            {
                return oDATramite.RetornarTramiteCUIT(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDATramite = null;
            }
        }
        */
    }
}