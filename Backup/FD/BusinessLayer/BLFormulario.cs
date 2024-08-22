using System;
using System.Collections.Generic;
using System.Text;

using FD.DataAccessLayer;
using FD.Entities;

namespace FD.BusinessLayer
{
    public class BLFormulario
    {
        public void RetornarTiposFormulario(List<Formulario> lFormulario,string tramite)
        {
            try
            {
                DAFormulario oDAFormulario = new DAFormulario();
                oDAFormulario.RetornarTiposFormulario(lFormulario, tramite);
                oDAFormulario = null;
            }  
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RetornarFlagAnexo(Int32 IdTramite, Int32 IdTipoEntidad)
        {
            try
            {
                DAFormulario oDAFormulario = new DAFormulario();
                int flagAnexo = oDAFormulario.RetornarFlagAnexo(IdTramite, IdTipoEntidad);
                oDAFormulario = null;

                return flagAnexo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
